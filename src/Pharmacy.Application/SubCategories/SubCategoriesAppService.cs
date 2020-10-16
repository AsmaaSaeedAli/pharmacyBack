using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Authorization;
using Pharmacy.Categories;
using Pharmacy.Dto;
using Pharmacy.SubCategories.Dtos;
using Pharmacy.SubCategories.Exporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Pharmacy.SubCategories
{
    public class SubCategoriesAppService : PharmacyAppServiceBase, ISubCategoriesAppService
    {
        private readonly IRepository<SubCategory> _subcategoryRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly ISubCategoriesExcelExporter _subcategoryExcelExporter;


        public SubCategoriesAppService(IRepository<Category> categoryRepository, IRepository<SubCategory> subcategoryRepository,
                ISubCategoriesExcelExporter subcategoryExcelExporter)
        {
            _subcategoryRepository = subcategoryRepository;
            _categoryRepository = categoryRepository;
            _subcategoryExcelExporter = subcategoryExcelExporter;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Host_SubCategories_Manage)]
        public async Task CreateOrUpdateSubCategory(SubCategoryDto input)
        {
            if (input.Id == null)
                await CreateAsync(input);
            else
                await UpdateAsync(input);

        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Host_SubCategories_Manage)]
        public async Task DeleteSubCategory(int? id)
        {
            if (id.HasValue)
                await _subcategoryRepository.DeleteAsync(id.Value);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Host_SubCategories)]
        public async Task<PagedResultDto<SubCategoriesListDto>> GetAllSubCategories(GetAllSubCategoriesInput input)
        {
            var filteredSubCategories = _subcategoryRepository.GetAll().AsNoTracking()
               .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.Name.StringValue.ToLower().Contains(input.Filter.ToLower().Trim())
                   || !string.IsNullOrEmpty(e.Code) && e.Code.ToLower().Trim().Contains(input.Filter.ToLower().Trim()));

            var query = from subcategory in filteredSubCategories
                        join category in _categoryRepository.GetAll().AsNoTracking() on subcategory.CategoryId equals category.Id into categories
                        from category in categories.DefaultIfEmpty()
                        select new SubCategoriesListDto
                        {
                            Id = subcategory.Id,
                            Code = subcategory.Code,
                            Name = subcategory.Name.CurrentCultureText,
                            CategoryName = category == null ? "" : category.Name.CurrentCultureText,
                            IsActive = subcategory.IsActive
                        };
            var totalCount = await query.CountAsync();
            var subCategories = await query.OrderBy(input.Sorting ?? "id desc").PageBy(input).ToListAsync();
            return new PagedResultDto<SubCategoriesListDto>(totalCount, subCategories);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Host_SubCategories_Manage)]
        public async Task<SubCategoryDto> GetSubCategoryForEdit(int id)
        {
            var subCategory = await _subcategoryRepository.GetAsync(id);
            return ObjectMapper.Map<SubCategoryDto>(subCategory);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Host_SubCategories_Manage)]
        public async Task<GetSubCategoryForViewDto> GetSubCategoryForView(int id)
        {
            var subcategory = await _subcategoryRepository.GetAllIncluding( b => b.Category).FirstOrDefaultAsync(b => b.Id == id);
            if (subcategory == null)
                throw new UserFriendlyException($"No SubCategory With Id {id}");

            var output = new GetSubCategoryForViewDto
            {
                Id = subcategory.Id,
                Code = subcategory.Code,
                Name = subcategory.Name.CurrentCultureText,
                IsActive = subcategory.IsActive,
                CategoryName = subcategory.Category.Name.CurrentCultureText
            };
            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Host_SubCategories_Export)]
        public async Task<FileDto> GetSubCategoriesToExcel(string filter)
        {
            var filteredSubCategories = _subcategoryRepository.GetAll().AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(filter), e => e.Name.StringValue.ToLower().Contains(filter.ToLower().Trim())
                                                                        || !string.IsNullOrEmpty(e.Code) && e.Code.ToLower().Trim().Contains(filter.ToLower().Trim()));

            var query = from subcategory in filteredSubCategories
                        join category in _categoryRepository.GetAll().AsNoTracking() on subcategory.CategoryId equals category.Id into categories
                        from category in categories.DefaultIfEmpty()
                        select new SubCategoriesListDto
                        {
                            Id = subcategory.Id,
                            Code = subcategory.Code,
                            Name = subcategory.Name.CurrentCultureText,
                            CategoryName = category == null ? "" : category.Name.CurrentCultureText,
                            IsActive = subcategory.IsActive
                        };
            var subCategories = await query.ToListAsync();
            return _subcategoryExcelExporter.ExportSubCategoriesToFile(subCategories);
        }


        private async Task CreateAsync(SubCategoryDto input)
        {
            var subcategory = ObjectMapper.Map<SubCategory>(input);
            await _subcategoryRepository.InsertAsync(subcategory);
        }
        private async Task UpdateAsync(SubCategoryDto input)
        {
            if (input.Id != null)
            {
                var subcategory = await _subcategoryRepository.FirstOrDefaultAsync((int)input.Id);
                ObjectMapper.Map(input, subcategory);
            }
        }
    }
}
