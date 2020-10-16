using Abp.Application.Services.Dto;
using Pharmacy.Dto;
using Pharmacy.SubCategories.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.SubCategories
{
    public interface ISubCategoriesAppService
    {
        Task<PagedResultDto<SubCategoriesListDto>> GetAllSubCategories(GetAllSubCategoriesInput input);
        Task CreateOrUpdateSubCategory(SubCategoryDto input);
        Task<SubCategoryDto> GetSubCategoryForEdit(int id);
        Task<GetSubCategoryForViewDto> GetSubCategoryForView(int id);
        Task DeleteSubCategory(int? id);
        Task<FileDto> GetSubCategoriesToExcel(string filter);
    }
}
