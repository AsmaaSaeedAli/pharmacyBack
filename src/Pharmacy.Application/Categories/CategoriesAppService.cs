using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Authorization;
using Pharmacy.Categories.Dtos;
using Pharmacy.Categories.Exporting;
using Pharmacy.Dto;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.ItemClasses;

namespace Pharmacy.Categories
{
    public class CategoriesAppService : PharmacyAppServiceBase, ICategoriesAppService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly ICategoriesExcelExporter _categoryExcelExporter;
        private readonly IRepository<ItemClass> _itemclassRepository;
        public CategoriesAppService(IRepository<Category> categoryRepository, ICategoriesExcelExporter categoryExcelExporter
            , IRepository<ItemClass> itemclassRepository)
        {
            _categoryRepository = categoryRepository;
            _categoryExcelExporter = categoryExcelExporter;
            _itemclassRepository = itemclassRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Host_Categories_Manage)]
        public async Task CreateOrUpdateCategory(CategoryDto input)
        {
            if (input.Id == null)
                await CreateAsync(input);
            else
                await UpdateAsync(input);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Host_Categories_Manage)]
        public async Task DeleteCategory(int? id)
        {
            if (id.HasValue)
                await _categoryRepository.DeleteAsync(id.Value);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Host_Categories)]
        public async Task<PagedResultDto<CategoriesListDto>> GetAllCategories(GetAllCategoriesInput input)
        {
            var filteredCategories = _categoryRepository.GetAll().AsNoTracking()
              .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e =>
                    e.Name.StringValue.ToLower().Contains(input.Filter.ToLower().Trim())
                    || !string.IsNullOrEmpty(e.Code) && e.Code.ToLower().Trim().Contains(input.Filter.ToLower().Trim()));

            var query = from category in filteredCategories
                        join itemclass in _itemclassRepository.GetAll().AsNoTracking() on category.Id equals itemclass.Id into itemclasses
                        from itemclass in itemclasses.DefaultIfEmpty()
                        select new CategoriesListDto
                        {
                            Id = category.Id,
                            Code = category.Code,
                            Name = category.Name.CurrentCultureText,
                            Description = category.Description.CurrentCultureText,
                            ItemClassName = itemclass == null ? "" : itemclass.Name.CurrentCultureText,
                            IsActive = category.IsActive
                        };
            var totalCount = await query.CountAsync();
            var categories = await query.OrderBy(input.Sorting ?? "id desc").PageBy(input).ToListAsync();
            return new PagedResultDto<CategoriesListDto>(totalCount, categories);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Host_Categories_Export)]
        public async Task<FileDto> GetCategoriesToExcel(string filter)
        {
            var filteredCategories = _categoryRepository.GetAll().AsNoTracking()
                 .WhereIf(!string.IsNullOrWhiteSpace(filter), e => e.Name.StringValue.ToLower().Contains(filter.ToLower().Trim())
                                                                         || !string.IsNullOrEmpty(e.Code) && e.Code.ToLower().Trim().Contains(filter.ToLower().Trim()));

            var query = from category in filteredCategories
                        join itemclass in _itemclassRepository.GetAll().AsNoTracking() on category.Id equals itemclass.Id into itemclasses
                        from itemclass in itemclasses.DefaultIfEmpty()

                        select new CategoriesListDto
                        {
                            Id = category.Id,
                            Code = category.Code,
                            Name = category.Name.CurrentCultureText,
                            Description = category.Description.CurrentCultureText,
                            ItemClassName=itemclass == null ? "": itemclass.Name.CurrentCultureText,
                            IsActive = category.IsActive
                        };
            var categories = await query.ToListAsync();
            return _categoryExcelExporter.ExportCategoriesToFile(categories);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Host_Categories_Manage)]
        public async Task<CategoryDto> GetCategoryForEdit(int id)
        {
            var category = await _categoryRepository.GetAsync(id);
            return ObjectMapper.Map<CategoryDto>(category);
        }

        private async Task CreateAsync(CategoryDto input)
        {
            var category = ObjectMapper.Map<Category>(input);
            await _categoryRepository.InsertAsync(category);
        }
        private async Task UpdateAsync(CategoryDto input)
        {
            if (input.Id != null)
            {
                var category = await _categoryRepository.FirstOrDefaultAsync((int)input.Id);
                ObjectMapper.Map(input, category);
            }
        }

    }
}
