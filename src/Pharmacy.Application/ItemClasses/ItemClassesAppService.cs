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
using Pharmacy.ItemClasses.Dtos;
using Pharmacy.ItemClasses.Exporting;
namespace Pharmacy.ItemClasses
{
    public class ItemClassesAppService : PharmacyAppServiceBase, IItemClassesAppService
    {
        private readonly IRepository<ItemClass> _itemClassRepository;
        private readonly IItemClassesExcelExporter _itemClassExcelExporter;
        public ItemClassesAppService(IRepository<ItemClass> itemClassRepository, IItemClassesExcelExporter itemClassExcelExporter)
        {
            _itemClassRepository = itemClassRepository;
            _itemClassExcelExporter = itemClassExcelExporter;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Host_ItemClasses_Manage)]
        public async Task CreateOrUpdateItemClass(ItemClassDto input)
        {
            if (input.Id == null)
                await CreateAsync(input);
            else
                await UpdateAsync(input);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Host_ItemClasses_Manage)]
        public async Task DeleteItemClass(int? id)
        {
            if (id.HasValue)
                await _itemClassRepository.DeleteAsync(id.Value);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Host_ItemClasses)]
        public async Task<PagedResultDto<ItemClassesListDto>> GetAllItemClasses(GetAllItemClassesInput input)
        {
            var filteredItemClasses = _itemClassRepository.GetAll().AsNoTracking()
              .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e =>
                    e.Name.StringValue.ToLower().Contains(input.Filter.ToLower().Trim())
                    || !string.IsNullOrEmpty(e.Code) && e.Code.ToLower().Trim().Contains(input.Filter.ToLower().Trim()));

            var query = from itemClass in filteredItemClasses
                        select new ItemClassesListDto
                        {
                            Id = itemClass.Id,
                            Code = itemClass.Code,
                            Name = itemClass.Name.CurrentCultureText,
                            ItemNumberStart = itemClass.ItemNumberStart,
                            IsActive = itemClass.IsActive
                        };
            var totalCount = await query.CountAsync();
            var itemClasses = await query.OrderBy(input.Sorting ?? "id desc").PageBy(input).ToListAsync();
            return new PagedResultDto<ItemClassesListDto>(totalCount, itemClasses);
        }
        [AbpAuthorize(AppPermissions.Pages_Administration_Host_ItemClasses_Export)]
        public async Task<GetItemClassForViewDto> GetItemClassForView(int id)
        {
            var itemClass = await _itemClassRepository.GetAll().FirstOrDefaultAsync(b => b.Id == id);
            if (itemClass == null)
                throw new UserFriendlyException($"No ItemClass With Id {id}");

            var output = new GetItemClassForViewDto
            {
                Id = itemClass.Id,
                Code = itemClass.Code,
                Name = itemClass.Name.CurrentCultureText,
                ItemNumberStart = itemClass.ItemNumberStart,
                IsActive = itemClass.IsActive,
              
            };
            return output;
        }
        [AbpAuthorize(AppPermissions.Pages_Administration_Host_ItemClasses_Export)]
        public async Task<FileDto> GetItemClassesToExcel(string filter)
        {
            var filteredItemClasses = _itemClassRepository.GetAll().AsNoTracking()
                 .WhereIf(!string.IsNullOrWhiteSpace(filter), e => e.Name.StringValue.ToLower().Contains(filter.ToLower().Trim())
                 || !string.IsNullOrEmpty(e.Code) && e.Code.ToLower().Trim().Contains(filter.ToLower().Trim()));

            var query = from itemClass in filteredItemClasses

                        select new ItemClassesListDto
                        {
                            Id = itemClass.Id,
                            Code = itemClass.Code,
                            Name = itemClass.Name.CurrentCultureText,
                            ItemNumberStart = itemClass.ItemNumberStart,
                            IsActive = itemClass.IsActive
                        };
            var itemClasses = await query.ToListAsync();
            return _itemClassExcelExporter.ExportItemClassesToFile(itemClasses);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Host_Categories_Manage)]
        public async Task<ItemClassDto> GetItemClassForEdit(int id)
        {
            var itemClass = await _itemClassRepository.GetAsync(id);
            return ObjectMapper.Map<ItemClassDto>(itemClass);
        }

        private async Task CreateAsync(ItemClassDto input)
        {
            var itemClass = ObjectMapper.Map<ItemClass>(input);
            await _itemClassRepository.InsertAsync(itemClass);
        }
        private async Task UpdateAsync(ItemClassDto input)
        {
            if (input.Id != null)
            {
                var itemClass = await _itemClassRepository.FirstOrDefaultAsync((int)input.Id);
                ObjectMapper.Map(input, itemClass);
            }
        }

    }
}
