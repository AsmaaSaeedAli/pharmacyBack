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
using Pharmacy.Items.Exporting;
using Pharmacy.Items.ItemQuantityDtos;
using Pharmacy.Branches;
using Pharmacy.Lookups;

namespace Pharmacy.Items
{
    public class ItemQuantitiesAppService : PharmacyAppServiceBase, IItemQuantitiesAppService
    {
        private readonly IRepository<ItemQuantity> _itemQuantityRepository;
        private readonly IRepository<ItemPrice> _itemPriceRepository;
        private readonly IRepository<Item> _itemRepository;
        private readonly IRepository<Branch> _branchRepository;
        private readonly IRepository<Lookup> _lookupRepository;
        private readonly IItemsExcelExporter _itemsExcelExporter;

        public ItemQuantitiesAppService(IRepository<ItemPrice> itemPriceRepository, IRepository<Branch> branchRepository, IRepository<Lookup> lookupRepository, IRepository<ItemQuantity> itemQuantityRepository, IItemsExcelExporter itemsExcelExporter, IRepository<Item> itemRepository)
        {
            _itemQuantityRepository = itemQuantityRepository;
            _itemPriceRepository = itemPriceRepository;
            _branchRepository = branchRepository;
            _lookupRepository = lookupRepository;
            _itemsExcelExporter = itemsExcelExporter;
            _itemRepository = itemRepository;
        }
        [AbpAuthorize(AppPermissions.Pages_Administration_ItemQuantities_Manage)]
        public async Task CreateOrUpdateItemQuantity(ItemQuantityDto input)
        {
            if (input.Id == null)
                await CreateAsync(input);
            else
                await UpdateAsync(input);
        }
        [AbpAuthorize(AppPermissions.Pages_Administration_ItemQuantities_Manage)]
        public async Task DeleteItemQuantity(int? id)
        {
            if (id.HasValue)
                await _itemQuantityRepository.DeleteAsync(id.Value);
        }
        [AbpAuthorize(AppPermissions.Pages_Administration_ItemQuantities)]
        public async Task<PagedResultDto<ItemQuantityListDto>> GetAllItemQuantities(GetAllItemQuantityInput input)
        {
            var filteredItemQuantities = _itemQuantityRepository.GetAll().AsNoTracking();
            var query = from itemQuantity in filteredItemQuantities
                        join itemPrice in _itemPriceRepository.GetAll().AsNoTracking() on itemQuantity.ItemPriceId equals itemPrice.Id into itemPrices
                        from itemPrice in itemPrices.DefaultIfEmpty()
                        join item in _itemRepository.GetAll().IgnoreQueryFilters().Where(i => !i.IsDeleted).AsNoTracking() on itemPrice.ItemId equals item.Id into items
                        from item in items.DefaultIfEmpty()
                        join branch in _branchRepository.GetAll().AsNoTracking() on itemQuantity.BranchId equals branch.Id into branches
                        from branch in branches.DefaultIfEmpty()
                        join unit in _lookupRepository.GetAll().Where(l => !l.IsDeleted).AsNoTracking() on itemQuantity.UnitId equals unit.Id into unites
                        from unit in unites.DefaultIfEmpty()
                        select new ItemQuantityListDto
                        {
                            Id = itemQuantity.Id,
                            Quantity = itemQuantity.Quantity,
                            ItemName = item == null ? "" : item.Name.CurrentCultureText,
                            BranchName = branch == null ? "" : branch.Name.CurrentCultureText,
                            UnitName = unit == null ? "" : unit.Name.CurrentCultureText,
                            QuantityLimit = itemQuantity.QuantityLimit,

                        };
            var totalCount = await query.CountAsync();
            var itemQuantities = await query.OrderBy(input.Sorting ?? "id desc").PageBy(input).ToListAsync();
            return new PagedResultDto<ItemQuantityListDto>(totalCount, itemQuantities);
        }
        [AbpAuthorize(AppPermissions.Pages_Administration_ItemQuantities_Manage)]
        public async Task<ItemQuantityDto> GetItemQuantityForEdit(int id)
        {
            var itemQuantity = await _itemQuantityRepository.GetAsync(id);
            return ObjectMapper.Map<ItemQuantityDto>(itemQuantity);
        }
        [AbpAuthorize(AppPermissions.Pages_Administration_ItemQuantities_Manage)]
        public async Task<GetItemQuantityForViewDto> GetItemQuantityForView(int id)
        {
            var itemQuantity = await _itemQuantityRepository.GetAllIncluding(b => b.Branch, b => b.ItemPrice.Item, b => b.ItemPrice, b => b.Unit).FirstOrDefaultAsync(b => b.Id == id);
            if (itemQuantity == null)
                throw new UserFriendlyException($"No ItemQuantity With Id {id}");

            var output = new GetItemQuantityForViewDto
            {
                Id = itemQuantity.Id,
                Quantity = itemQuantity.Quantity,
                ItemName = itemQuantity.ItemPrice.Item.Name.CurrentCultureText,
                BranchName = itemQuantity.Branch.Name.CurrentCultureText,
                UnitName = itemQuantity.Unit.Name.CurrentCultureText,
                QuantityLimit = itemQuantity.QuantityLimit,
            };
            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_ItemQuantities_Export)]
        public async Task<FileDto> GetItemQuantitiesToExcel(string filter)
        {
            var itemQuantities = await _itemQuantityRepository.GetAllIncluding(r => r.Branch, r => r.ItemPrice).AsNoTracking()
                .Select(itemQuantity => new ItemQuantityListDto
                {
                    Id = itemQuantity.Id,
                    Quantity = itemQuantity.Quantity,
                    ItemName = itemQuantity.ItemPrice.Item.Name.CurrentCultureText,
                    BranchName = itemQuantity.Branch.Name.CurrentCultureText,
                    UnitName = itemQuantity.Unit.Name.CurrentCultureText,
                    QuantityLimit = itemQuantity.QuantityLimit,
                }).ToListAsync();

            return _itemsExcelExporter.ExportItemQuantitiesToFile(itemQuantities);
        }

        private async Task CreateAsync(ItemQuantityDto input)
        {
            var itemQuantity = ObjectMapper.Map<ItemQuantity>(input);
            await _itemQuantityRepository.InsertAsync(itemQuantity);
        }
        private async Task UpdateAsync(ItemQuantityDto input)
        {
            if (input.Id != null)
            {
                var itemQuantity = await _itemQuantityRepository.FirstOrDefaultAsync((int)input.Id);
                ObjectMapper.Map(input, itemQuantity);
            }
        }
        [AbpAuthorize(AppPermissions.Pages_Administration_ItemQuantities_Manage)]
        public async Task<GetItemQuantityForViewDto> GetItemByData(string filter)
        {
            //var query = from itemQuantity in _itemQuantityRepository.GetAll().AsNoTracking()
            //    join itemPrice in _itemPriceRepository.GetAll().AsNoTracking() on itemQuantity.ItemPriceId equals itemPrice.ItemId
            //    join/
            var query = from item in _itemRepository.GetAll().AsNoTracking().IgnoreQueryFilters().Where(i => !i.IsDeleted).WhereIf(!string.IsNullOrEmpty(filter),

                    i => i.ItemNumber.Contains(filter))
                        join itemPrice in _itemPriceRepository.GetAll().AsNoTracking() on item.Id equals itemPrice.ItemId
                        select new GetItemQuantityForViewDto
                        {
                            Id = item.Id,
                            Quantity = 0,
                            ItemName = item.Name.CurrentCultureText,
                            ItemNameAr = item.Name["ar"],
                            ItemNameEn = item.Name["en"],
                            ItemPrice = itemPrice.Price
                        };

            var output = await query.FirstOrDefaultAsync();
            return output;

        }
    }
}
