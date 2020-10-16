using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Authorization;
using Pharmacy.Invoices.Dtos;
using Pharmacy.Items;
using Abp.Linq.Extensions;
using Abp.Application.Services.Dto;
using Pharmacy.Customers;
using Pharmacy.Lookups;
using System.Linq.Dynamic.Core;
using Pharmacy.Authorization.Users;

namespace Pharmacy.Invoices
{
    public class InvoiceAppService : PharmacyAppServiceBase, IInvoiceAppService
    {
        private readonly IRepository<Item> _itemRepository;
        private readonly IRepository<ItemPrice> _itemPriceRepository;
        private readonly IRepository<ItemQuantity> _itemQuantityRepository;
        private readonly IRepository<Invoice> _invoiceRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Lookup> _lookupRepository;
        private readonly IRepository<User, long> _userRepository;
        public InvoiceAppService(IRepository<Invoice> invoiceRepository, IRepository<ItemPrice> itemPriceRepository, IRepository<Item> itemRepository, IRepository<ItemQuantity> itemQuantityRepository, IRepository<Customer> customerRepository, IRepository<Lookup> lookupRepository, IRepository<User, long> userRepository)
        {
            _invoiceRepository = invoiceRepository;
            _itemPriceRepository = itemPriceRepository;
            _itemRepository = itemRepository;
            _itemQuantityRepository = itemQuantityRepository;
            _customerRepository = customerRepository;
            _lookupRepository = lookupRepository;
            _userRepository = userRepository;
        }
        [AbpAuthorize(AppPermissions.Pages_Administration_Invoices)]
        public async Task<PagedResultDto<InvoiceListDto>> GetAllInvoices(GetAllInvoiceInput input)
        {
            var filteredInvoices = _invoiceRepository.GetAll().AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.InvoiceNo.ToLower().Contains(input.Filter.ToLower().Trim()))
                .WhereIf(input.StatusIds != null && input.StatusIds.Count > 0, i => input.StatusIds.Contains(i.StatusId))
                .WhereIf(input.TypeIds != null && input.TypeIds.Count > 0, i => input.TypeIds.Contains(i.InvoiceTypeId))
                .WhereIf(input.FromDate.HasValue, i => i.CreationTime >= input.FromDate)
                .WhereIf(input.ToDate.HasValue, i => i.CreationTime <= input.ToDate);

            var query = from invoice in filteredInvoices
                       
                        join customer in _customerRepository.GetAll().AsNoTracking() on invoice.CustomerId equals customer.Id into nullableCustomers
                        from customer in nullableCustomers.DefaultIfEmpty()

                        join invoiceType in _lookupRepository.GetAll().AsNoTracking() on invoice.InvoiceTypeId equals invoiceType.Id
                        join status in _lookupRepository.GetAll().AsNoTracking() on invoice.StatusId equals status.Id
                       
                        join user in _userRepository.GetAll().AsNoTracking() on invoice.CreatorUserId equals  user.Id into nullableUsers
                        from user in nullableUsers.DefaultIfEmpty()
                        select new InvoiceListDto
                        {
                            Id = invoice.Id,
                            InvoiceNo = invoice.InvoiceNo,
                            CustomerName = customer == null ? "" : customer.FullName.CurrentCultureText,
                            StatusId = invoice.StatusId,
                            StatusName = status.Name.CurrentCultureText,
                            InvoiceType = invoiceType.Name.CurrentCultureText,
                            NetAmount = invoice.NetAmount,
                            Notes = invoice.Notes,
                            CreatedOn= invoice.CreationTime,
                            CreatedBy = user.FullName
                        };
            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting ?? "id desc").PageBy(input).ToListAsync();
            return new PagedResultDto<InvoiceListDto>(totalCount, items);
        }


        [AbpAuthorize(AppPermissions.Pages_Administration_Invoices_Manage)]
        public async Task CreateInvoice(InvoiceDto input)
        {
            var invoice = ObjectMapper.Map<Invoice>(input);
            invoice.InvoiceNo = GetInvoiceNo();
            await _invoiceRepository.InsertAsync(invoice);
        }

        private string GetInvoiceNo()
        {
            var maxSeq = 1;

            var allInvoicesNo = _invoiceRepository.GetAll().AsNoTracking()
                .Where(obj => obj.InvoiceNo.StartsWith("INV")).OrderBy(x => x.Id)
                .Select(w => w.InvoiceNo).ToList();

            if (allInvoicesNo.Count > 0)
            {
                var lastInvoicesNo = allInvoicesNo.LastOrDefault();
                if (lastInvoicesNo != null) maxSeq = Convert.ToInt32(lastInvoicesNo.Split('-')[1]) + 1;
            }

            var requestCode = "INV-" + maxSeq.ToString("00000000");
            return requestCode;

        }

        public async Task<InvoiceItemDto> GetItemDetails(GetItemForInvoiceInput input)
        {
            var query = from item in _itemRepository.GetAll().AsNoTracking().IgnoreQueryFilters().WhereIf(!string.IsNullOrEmpty(input.Filter),
                    i => i.ItemNumber.Contains(input.Filter.ToLower().Trim()) || i.BarCode.Contains(input.Filter.ToLower().Trim()))
                        .WhereIf(input.IsInsurance, i => i.IsInsurance)
                        join itemPrice in _itemPriceRepository.GetAll().AsNoTracking() on item.Id equals itemPrice.ItemId
                        join itemQuantity in _itemQuantityRepository.GetAll().AsNoTracking() on itemPrice.Id equals itemQuantity.ItemPriceId
                        where  itemQuantity.Quantity > 0
                        select new InvoiceItemDto
                        {
                            ItemId = item.Id,
                            Quantity = 0,
                            ItemNameAr = item.Name["ar"],
                            ItemNameEn = item.Name["en"],
                            ItemPrice = itemPrice.Price,
                            Vat = item.Vat
                        };
            var output = await query.FirstOrDefaultAsync();
            return output;
        }
    }
}
