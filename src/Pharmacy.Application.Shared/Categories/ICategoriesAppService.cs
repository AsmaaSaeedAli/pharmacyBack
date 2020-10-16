using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Pharmacy.Categories.Dtos;
using Pharmacy.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Categories
{
    public interface ICategoriesAppService : IApplicationService
    {
        Task<PagedResultDto<CategoriesListDto>> GetAllCategories(GetAllCategoriesInput input);
        Task CreateOrUpdateCategory(CategoryDto input);
        Task<CategoryDto> GetCategoryForEdit(int id);

        Task DeleteCategory(int? id);
        Task<FileDto> GetCategoriesToExcel(string filter);
    }
}
