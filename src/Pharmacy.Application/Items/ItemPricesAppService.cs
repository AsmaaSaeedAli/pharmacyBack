using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Pharmacy.Dto;
using System.Threading.Tasks;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Abp.Linq.Extensions;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Authorization;
using Pharmacy.Authorization;
using Pharmacy.Items.ItemPriceDtos;
using Pharmacy.Corporates;
using Pharmacy.Items.Exporting;
using System;

namespace Pharmacy.Items
{
    public class ItemPricesAppService : PharmacyAppServiceBase, IItemPricesAppService
    {
        private readonly IRepository<ItemPrice> _itemPriceRepository;
        private readonly IRepository<Item> _itemRepository;
        private readonly IRepository<Corporate> _corporateRepository;

        private readonly IItemsExcelExporter _itemsExcelExporter;

        public ItemPricesAppService(IRepository<Item> itemRepository, IRepository<Corporate> corporateRepository, IRepository<ItemPrice> itemPriceRepository, IItemsExcelExporter itemsExcelExporter)
        {
            _itemPriceRepository = itemPriceRepository;
            _itemRepository = itemRepository;
            _corporateRepository = corporateRepository;
            _itemsExcelExporter = itemsExcelExporter;
        }
        [AbpAuthorize(AppPermissions.Pages_Administration_Items_Manage)]
        public async Task CreateOrUpdateItemPrice(ItemPriceDto input)
        {
            if (input.Id == null)
                await CreateAsync(input);
            else
                await UpdateAsync(input);
        }
        [AbpAuthorize(AppPermissions.Pages_Administration_Items_Manage)]
        public async Task DeleteItemPrice(int? id)
        {
            if (id.HasValue)
                await _itemPriceRepository.DeleteAsync(id.Value);
        }
        [AbpAuthorize(AppPermissions.Pages_Administration_Items)]
        public async Task<PagedResultDto<ItemPriceListDto>> GetAllItemPrices(GetAllItemPriceInput input)
        {
            var filteredItemPrices = _itemPriceRepository.GetAll().AsNoTracking();

            var query = from itemPrice in filteredItemPrices
                        join item in _itemRepository.GetAll().IgnoreQueryFilters().AsNoTracking().Where(i=>!i.IsDeleted) on itemPrice.ItemId equals item.Id into items
                        from item in items.DefaultIfEmpty()
                        select new ItemPriceListDto
                        {
                            Id = itemPrice.Id,
                            Price = itemPrice.Price,
                            Discount = itemPrice.Discount,
                            IsActive = itemPrice.IsActive,
                            ItemName = item == null ? "" : item.Name.CurrentCultureText

                        };
            var totalCount = await query.CountAsync();
            var itemPrices = await query.OrderBy(input.Sorting ?? "id desc").PageBy(input).ToListAsync();
            return new PagedResultDto<ItemPriceListDto>(totalCount, itemPrices);
        }
        [AbpAuthorize(AppPermissions.Pages_Administration_Items_Manage)]
        public async Task<ItemPriceDto> GetItemPriceForEdit(int id)
        {
            var itemPrice = await _itemPriceRepository.GetAll().IgnoreQueryFilters().AsNoTracking().FirstOrDefaultAsync(i=>!i.IsDeleted &&  i.Id == id);
            return ObjectMapper.Map<ItemPriceDto>(itemPrice);
        }
        [AbpAuthorize(AppPermissions.Pages_Administration_Items_Manage)]
        public async Task<GetItemPriceForViewDto> GetItemPriceForView(int id)
        {
            var itemPrice = await _itemPriceRepository.GetAllIncluding(b => b.Item).FirstOrDefaultAsync(b => b.Id == id);
            if (itemPrice == null)
                throw new UserFriendlyException($"No ItemPrice With Id {id}");

            var output = new GetItemPriceForViewDto
            {
                Id = itemPrice.Id,
                Price = itemPrice.Price,
                Discount = itemPrice.Discount,
                IsActive = itemPrice.IsActive,
                ItemName = itemPrice.Item.Name.CurrentCultureText

            };
            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Items_Export)]
        public async Task<FileDto> GetItemPricesToExcel(string filter)
        {
            var itemPrices = await _itemPriceRepository.GetAllIncluding(r => r.Item).AsNoTracking()
                .Select(itemPrice => new ItemPriceListDto
                {
                    Id = itemPrice.Id,
                    Price = itemPrice.Price,
                    Discount = itemPrice.Discount,
                    IsActive = itemPrice.IsActive,
                    ItemName = itemPrice.Item.Name.CurrentCultureText

                }).ToListAsync();

            return _itemsExcelExporter.ExportItemPricesToFile(itemPrices);
        }

        private async Task CreateAsync(ItemPriceDto input)
        {
            var itemPrice = ObjectMapper.Map<ItemPrice>(input);
            itemPrice.TenantId = AbpSession.TenantId;
            await _itemPriceRepository.InsertAsync(itemPrice);
            if (input.ItemId > 0)
            {
                var lastItem = await _itemPriceRepository.GetAll().AsNoTracking().Where(b => b.ItemId == input.ItemId).OrderByDescending(c=>c.CreationTime).FirstOrDefaultAsync();
                if (lastItem != null)
                {
                    lastItem.DateTo = input.DateFrom;
                    await _itemPriceRepository.UpdateAsync(lastItem);
                }
            }

        }
        private async Task UpdateAsync(ItemPriceDto input)
        {
            if (input.Id != null)
            {
                var itemPrice = await _itemPriceRepository.FirstOrDefaultAsync((int)input.Id);
                ObjectMapper.Map(input, itemPrice);
            }
        }
        public async Task<PagedResultDto<ItemPriceDto>> GetItemPricesByItemId(int itemId)
        {
            var filteredItemPrices = _itemPriceRepository.GetAll().AsNoTracking().Where(i => i.ItemId == itemId);

            if (!AbpSession.TenantId.HasValue)
            {
                var query = from itemPrice in filteredItemPrices
                            join item in _itemRepository.GetAll().IgnoreQueryFilters().AsNoTracking().Where(r => r.TenantId == null) on itemPrice.ItemId equals item.Id into items
                            from item in items.DefaultIfEmpty()
                            select new ItemPriceDto
                            {
                                Id = itemPrice.Id,
                                Price = itemPrice.Price,
                                Discount = itemPrice.Discount,
                                IsActive = itemPrice.IsActive,
                                ItemId = itemPrice.ItemId,
                                DateFrom = itemPrice.DateFrom,
                                DateTo = itemPrice.DateTo,

                            };
                var totalCount = await query.CountAsync();
                var itemPrices = await query.ToListAsync();
                return new PagedResultDto<ItemPriceDto>(totalCount, itemPrices);
            }
            else
            {
                var query = from itemPrice in filteredItemPrices
                            join item in _itemRepository.GetAll().IgnoreQueryFilters().AsNoTracking().Where(r => r.TenantId == null || r.TenantId == AbpSession.TenantId) on itemPrice.ItemId equals item.Id into items
                            from item in items.DefaultIfEmpty()
                            select new ItemPriceDto
                            {
                                Id = itemPrice.Id,
                                Price = itemPrice.Price,
                                Discount = itemPrice.Discount,
                                IsActive = itemPrice.IsActive,
                                ItemId = itemPrice.ItemId,
                                DateFrom = itemPrice.DateFrom,
                                DateTo = itemPrice.DateTo,

                            };
                var totalCount = await query.CountAsync();
                var itemPrices = await query.ToListAsync();
                return new PagedResultDto<ItemPriceDto>(totalCount, itemPrices);
            }


        }


    }
}
