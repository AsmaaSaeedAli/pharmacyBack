using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Pharmacy.ItemClasses.Dtos;
using Pharmacy.Dto;
using System.Threading.Tasks;

namespace Pharmacy.ItemClasses
{
    public interface IItemClassesAppService : IApplicationService
    {
        Task<PagedResultDto<ItemClassesListDto>> GetAllItemClasses(GetAllItemClassesInput input);
        Task CreateOrUpdateItemClass(ItemClassDto input);
        Task<ItemClassDto> GetItemClassForEdit(int id);
        Task<GetItemClassForViewDto> GetItemClassForView(int id);
        Task DeleteItemClass(int? id);
        Task<FileDto> GetItemClassesToExcel(string filter);
    }
}
