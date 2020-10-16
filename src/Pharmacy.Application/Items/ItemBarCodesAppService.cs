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
using Pharmacy.Items.Exporting;
using Pharmacy.Items.ItemBarCodeDtos;

namespace Pharmacy.Items
{
    public class ItemBarCodesAppService : PharmacyAppServiceBase, IItemBarCodesAppService
    {
        private readonly IRepository<ItemBarCode> _itemBarCodeRepository;
        private readonly IRepository<Item> _itemRepository;
        private readonly IItemsExcelExporter _itemsExcelExporter;

        public ItemBarCodesAppService(IRepository<Item> itemRepository, IRepository<ItemBarCode> itemBarCodeRepository, IItemsExcelExporter itemsExcelExporter)
        {
            _itemBarCodeRepository = itemBarCodeRepository;
            _itemRepository = itemRepository;
            _itemsExcelExporter = itemsExcelExporter;
        }
        [AbpAuthorize(AppPermissions.Pages_Administration_ItemBarCodes_Manage)]
        public async Task CreateOrUpdateItemBarCode(ItemBarCodeDto input)
        {
            if (input.Id == null)
                await CreateAsync(input);
            else
                await UpdateAsync(input);
        }
        [AbpAuthorize(AppPermissions.Pages_Administration_ItemBarCodes_Manage)]
        public async Task DeleteItemBarCode(int? id)
        {
            if (id.HasValue)
                await _itemBarCodeRepository.DeleteAsync(id.Value);
        }
        [AbpAuthorize(AppPermissions.Pages_Administration_ItemBarCodes)]
        public async Task<PagedResultDto<ItemBarCodeListDto>> GetAllItemBarCodes(GetAllItemBarCodeInput input)
        {
            var filteredItemBarCodes = _itemBarCodeRepository.GetAll().AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.BarCode.ToLower().Trim().Contains(input.Filter.ToLower().Trim()));

            var query = from ItemBarCode in filteredItemBarCodes
                        join item in _itemRepository.GetAll().AsNoTracking() on ItemBarCode.ItemId equals item.Id into items
                        from item in items.DefaultIfEmpty()
                        select new ItemBarCodeListDto
                        {
                            Id = ItemBarCode.Id,
                            BarCode = ItemBarCode.BarCode,
                            IsActive = ItemBarCode.IsActive,
                            ItemName = item == null ? "" : item.Name.CurrentCultureText,
                            
                        };
            var totalCount = await query.CountAsync();
            var itemBarCodes = await query.OrderBy(input.Sorting ?? "id desc").PageBy(input).ToListAsync();
            return new PagedResultDto<ItemBarCodeListDto>(totalCount, itemBarCodes);
        }
        [AbpAuthorize(AppPermissions.Pages_Administration_ItemBarCodes_Manage)]
        public async Task<ItemBarCodeDto> GetItemBarCodeForEdit(int id)
        {
            var itemBarCode = await _itemBarCodeRepository.GetAsync(id);
            return ObjectMapper.Map<ItemBarCodeDto>(itemBarCode);
        }
        [AbpAuthorize(AppPermissions.Pages_Administration_ItemBarCodes_Manage)]
        public async Task<GetItemBarCodeForViewDto> GetItemBarCodeForView(int id)
        {
            var itemBarCode = await _itemBarCodeRepository.GetAll().FirstOrDefaultAsync(b => b.Id == id);
            if (itemBarCode == null)
                throw new UserFriendlyException($"No ItemBarCode With Id {id}");

            var output = new GetItemBarCodeForViewDto
            {
                Id = itemBarCode.Id,
                BarCode = itemBarCode.BarCode,
                IsActive = itemBarCode.IsActive,
                //ItemName = itemBarCode.Item.Name.CurrentCultureText,
            };
            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_ItemBarCodes_Export)]
        public async Task<FileDto> GetItemBarCodesToExcel(string filter)
        {
            var itemBarCodes = await _itemBarCodeRepository.GetAll().AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(filter), e => e.BarCode.ToLower().Trim().Contains(filter.ToLower().Trim()))
                .Select(itemBarCode => new ItemBarCodeListDto
                {
                    Id = itemBarCode.Id,
                    BarCode = itemBarCode.BarCode,
                    IsActive = itemBarCode.IsActive,
                  //  ItemName = itemBarCode.Item.Name.CurrentCultureText,
                }).ToListAsync();

            return _itemsExcelExporter.ExportItemBarCodesToFile(itemBarCodes);
        }

        private async Task CreateAsync(ItemBarCodeDto input)
        {
            var itemBarCode = ObjectMapper.Map<ItemBarCode>(input);
            await _itemBarCodeRepository.InsertAsync(itemBarCode);
        }
        private async Task UpdateAsync(ItemBarCodeDto input)
        {
            if (input.Id != null)
            {
                var itemBarCode = await _itemBarCodeRepository.FirstOrDefaultAsync((int)input.Id);
                ObjectMapper.Map(input, itemBarCode);
            }
        }

    }
}
