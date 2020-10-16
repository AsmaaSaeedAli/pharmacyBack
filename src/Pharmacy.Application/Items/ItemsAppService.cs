using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;
using Abp.Authorization;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Authorization;
using Pharmacy.Dto;
using Pharmacy.ItemClasses;
using Pharmacy.Categories;
using Pharmacy.Invoices.Dtos;
using Pharmacy.SubCategories;
using Pharmacy.Items.ItemDtos;
using Pharmacy.Corporates;
using Pharmacy.Items.Exporting;
using Pharmacy.Items.ItemBarCodeDtos;
using Pharmacy.ManuFactories;
using System;
using System.Linq.Expressions;
using Pharmacy.Web.Helpers;

namespace Pharmacy.Items
{
    public class ItemsAppService : PharmacyAppServiceBase, IItemsAppService
    {

        private readonly IRepository<Item> _itemRepository;
        private readonly IRepository<ItemClass> _itemClassRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<SubCategory> _subcategoryRepository;
        private readonly IRepository<ItemBarCode> _itemBarCodeRepository;
        private readonly IRepository<ManuFactory> _manuFactoryRepository;
        private readonly IRepository<Corporate> _corporateRepository;

        private readonly IItemsExcelExporter _itemsExcelExporter;

        public ItemsAppService(IRepository<ItemClass> itemClassRepository,
            IRepository<Category> categoryRepository, IRepository<SubCategory> subcategoryRepository,
            IRepository<Item> itemRepository, IItemsExcelExporter itemsExcelExporter,
            IRepository<ItemBarCode> itemBarCodeRepository, IRepository<Corporate> corporateRepository, IRepository<ItemPrice> itemPriceRepository,
            IRepository<ManuFactory> manuFactoryRepository)
        {
            _itemClassRepository = itemClassRepository;
            _categoryRepository = categoryRepository;
            _subcategoryRepository = subcategoryRepository;
            _itemRepository = itemRepository;
            _itemsExcelExporter = itemsExcelExporter;
            _itemBarCodeRepository = itemBarCodeRepository;
            _manuFactoryRepository = manuFactoryRepository;
            _corporateRepository = corporateRepository;
        }
        [AbpAuthorize(AppPermissions.Pages_Administration_Items_Manage)]
        public async Task CreateOrUpdateItem(ItemDto input)
        {
            if (input.Id == null)
                await CreateAsync(input);
            else
                await UpdateAsync(input);
        }
        [AbpAuthorize(AppPermissions.Pages_Administration_Items_Manage)]
        public async Task DeleteItem(int? id)
        {
            if (id.HasValue)
                await _itemRepository.DeleteAsync(id.Value);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Items)]
        public async Task<PagedResultDto<ItemListDto>> GetAllItems(GetAllItemInput input)
        {

            var filteredItems = _itemRepository.GetAll().IgnoreQueryFilters().Where(i => !i.IsDeleted).AsNoTracking().SearchItemName(input)

            // .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.Name.StringValue.ToLower().Contains(input.Filter.ToLower().Trim())
            .WhereIf(!string.IsNullOrEmpty(input.Filter), e => e.ItemNumber.ToLower().Trim().Contains(input.Filter.ToLower().Trim())
            ||!string.IsNullOrEmpty(e.BarCode) && e.BarCode.ToLower().Trim().Contains(input.Filter.ToLower().Trim()))
            .WhereIf(input.ClassIds != null && input.ClassIds.Count > 0, i => input.ClassIds.Contains(i.ItemClassId))
            .WhereIf(input.CategoryIds != null && input.CategoryIds.Count > 0, i => input.CategoryIds.Contains(i.CategoryId))
            .WhereIf(input.SubCategoryIds != null && input.SubCategoryIds.Count > 0, i => input.SubCategoryIds.Contains(i.SubCategoryId))
            .WhereIf(input.FromDate.HasValue, i => i.CreationTime >= input.FromDate)
            .WhereIf(input.ToDate.HasValue, i => i.CreationTime <= input.ToDate)
            .WhereIf(input.ManuFactoryIds != null && input.ManuFactoryIds.Count > 0, i => input.ManuFactoryIds.Contains(i.ItemClassId));

            var query = from item in filteredItems
                        join itemClass in _itemClassRepository.GetAll().AsNoTracking()
                        on item.ItemClassId equals itemClass.Id into nullableItemClasses
                        from itemClass in nullableItemClasses.DefaultIfEmpty()
                        join category in _categoryRepository.GetAll().AsNoTracking()
                        on item.CategoryId equals category.Id into nullableCategories
                        from category in nullableCategories.DefaultIfEmpty()
                        join subcategory in _subcategoryRepository.GetAll().AsNoTracking()
                        on item.SubCategoryId equals subcategory.Id into subcategories
                        from subcategory in subcategories.DefaultIfEmpty()
                        join manuFactory in _manuFactoryRepository.GetAll().AsNoTracking()
                        on item.ManuFactoryId equals manuFactory.Id into nullableManuFactories
                        from manuFactory in nullableManuFactories.DefaultIfEmpty()
                        join corporatefavorite in _corporateRepository.GetAll().AsNoTracking() on item.CorporateFavoriteId equals corporatefavorite.Id into corporatesfavorite
                        from corporatefavorite in corporatesfavorite.DefaultIfEmpty()

                        select new ItemListDto
                        {
                            Id = item.Id,
                            ItemNumber = item.ItemNumber,
                            NameAr = item.Name["ar"],
                            NameEn = item.Name["en"],
                            Description = item.Description == null ? "" : item.Description.CurrentCultureText,
                            BarCode = item.BarCode,
                            IsActive = item.IsActive,
                            IsInsurance = item.IsInsurance,
                            ItemClassName = itemClass == null ? "" : itemClass.Name.CurrentCultureText,
                            CategoryName = category == null ? "" : category.Name.CurrentCultureText,
                            SubCategoryName = subcategory == null ? "" : subcategory.Name.CurrentCultureText,
                            CorporateFavoriteName = corporatefavorite == null ? "" : corporatefavorite.Name.CurrentCultureText,
                            CreatedOn = item.CreationTime,
                            ManuFactoryName = manuFactory == null ? "" : manuFactory.Name.CurrentCultureText
                        };

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting ?? "id desc").PageBy(input).ToListAsync();
            return new PagedResultDto<ItemListDto>(totalCount, items);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Items_Manage)]
        public async Task<ItemDto> GetItemForEdit(int id)
        {
            var item = await _itemRepository.GetAll().IgnoreQueryFilters().FirstOrDefaultAsync(i => i.Id == id);
            return ObjectMapper.Map<ItemDto>(item);
        }
        [AbpAuthorize(AppPermissions.Pages_Administration_Items_Manage)]
        public async Task<GetItemForViewDto> GetItemForView(int id)
        {
            var item = await _itemRepository.GetAllIncluding(b => b.ItemClass, b => b.Category, b => b.SubCategory, b => b.ManuFactory).IgnoreQueryFilters().FirstOrDefaultAsync(b => b.Id == id);
            if (item == null)
                throw new UserFriendlyException($"No Item With Id {id}");

            var output = new GetItemForViewDto
            {
                Id = item.Id,
                ItemNumber = item.ItemNumber,
                Name = item.Name.CurrentCultureText,
                Description = item.Description?.CurrentCultureText,
                BarCode = item.BarCode,
                IsActive = item.IsActive,
                IsInsurance = item.IsInsurance,
                HasVat = item.HasVat,
                Vat = item.Vat,
                ItemClassName = item.ItemClass.Name.CurrentCultureText,
                CategoryName = item.Category.Name.CurrentCultureText,
                SubCategoryName = item.SubCategory.Name.CurrentCultureText,
                ManuFactoryName = item.ManuFactory.Name.CurrentCultureText,
                CorporateFavoriteName = item.CorporateFavorite.Name.CurrentCultureText
            };
            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Items_Export)]
        public async Task<FileDto> GetItemsToExcel(GetAllItemInputForExcel input)
        {
            var items = await _itemRepository.GetAllIncluding(r => r.ItemClass, r => r.Category, r => r.SubCategory, r => r.ManuFactory).AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.Name.StringValue.ToLower().Contains(input.Filter.ToLower().Trim())
                    || !string.IsNullOrEmpty(e.ItemNumber) && e.ItemNumber.ToLower().Trim().Contains(input.Filter.ToLower().Trim()) || !string.IsNullOrEmpty(e.BarCode) && e.BarCode.ToLower().Trim().Contains(input.Filter.ToLower().Trim()))
                .Select(item => new ItemListDto
                {
                    Id = item.Id,
                    ItemNumber = item.ItemNumber,
                    NameAr = item.Name["ar"],
                    NameEn = item.Name["en"],
                    Description = item.Description.CurrentCultureText,
                    BarCode = item.BarCode,
                    IsActive = item.IsActive,
                    ItemClassName = item.ItemClass.Name.CurrentCultureText,
                    CategoryName = item.Category.Name.CurrentCultureText,
                    SubCategoryName = item.SubCategory.Name.CurrentCultureText,
                    ManuFactoryName = item.ManuFactory.Name.CurrentCultureText,
                    CorporateFavoriteName = item.CorporateFavorite.Name.CurrentCultureText
                }).ToListAsync();

            return _itemsExcelExporter.ExportItemsToFile(items);
        }

        private async Task CreateAsync(ItemDto input)
        {
            var itemclass = await _itemClassRepository.FirstOrDefaultAsync((int)input.ItemClassId);
            var itemlist = await _itemRepository.GetAllIncluding(b => b.ItemClass, b => b.Category, b => b.SubCategory).FirstOrDefaultAsync(b => b.Name.StringValue == input.Name || b.BarCode == input.BarCode);
            if (itemlist != null && itemlist.BarCode != null /*&& AbpSession.TenantId.HasValue*/ && itemlist.TenantId == null)
                throw new UserFriendlyException($"Item with Bar code {input.BarCode} Already Exists ");

            if (itemlist != null && !AbpSession.TenantId.HasValue && itemlist.TenantId != null)
            {
                var item = await _itemRepository.FirstOrDefaultAsync(itemlist.Id);
                item.TenantId = null;
                if (itemclass != null)
                {

                    if (int.Parse(item.ItemNumber.Substring(0, 1)) != itemclass.ItemNumberStart)
                        throw new UserFriendlyException($"Item with ItemNumber {input.ItemNumber} Must Be Start With {itemclass.ItemNumberStart} ");
                }
                ObjectMapper.Map(input, item);
            }
            else
            {
                var item = ObjectMapper.Map<Item>(input);
                
                if (itemclass !=null)
                {

                    if (int.Parse(item.ItemNumber.Substring(0, 1)) != itemclass.ItemNumberStart)
                        throw new UserFriendlyException($"Item with ItemNumber {input.ItemNumber} Must Be Start With {itemclass.ItemNumberStart} ");
                }
               
                await _itemRepository.InsertAsync(item);
            }
        }
        private async Task UpdateAsync(ItemDto input)
        {
            if (input.Id != null)
            {
                var itemclass = await _itemClassRepository.FirstOrDefaultAsync((int)input.ItemClassId);
                var item = await _itemRepository.FirstOrDefaultAsync((int)input.Id);
                if (itemclass != null)
                {

                    if (int.Parse(input.ItemNumber.Substring(0, 1)) != itemclass.ItemNumberStart)
                        throw new UserFriendlyException($"Item with ItemNumber {input.ItemNumber} Must Be Start With {itemclass.ItemNumberStart} ");
                }
                ObjectMapper.Map(input, item);
            }
        }



        public async Task<ItemBarCodeDto> GetItemBarCodes(int itemId)
        {
            var itemBarCode = await _itemBarCodeRepository.FirstOrDefaultAsync(i => i.ItemId == itemId && i.IsActive);
            //if(itemBarCode == null)
            //    return new ItemBarCodeDto();
            return ObjectMapper.Map<ItemBarCodeDto>(itemBarCode);
        }
        public async Task SaveItemBarCode(ItemBarCodeDto input)
        {
            if (input.Id.HasValue)
            {
                var item = await _itemBarCodeRepository.FirstOrDefaultAsync((int)input.Id);
                ObjectMapper.Map(input, item);
            }
            else
            {
                var itemBarCode = ObjectMapper.Map<ItemBarCode>(input);
                await _itemBarCodeRepository.InsertAsync(itemBarCode);
            }
        }



    }
}
