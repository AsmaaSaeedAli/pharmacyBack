using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Pharmacy.Items.ItemQuantityDtos;
using Pharmacy.Dto;

namespace Pharmacy.Items
{
    public  interface IItemQuantitiesAppService : IApplicationService
    {
        Task<PagedResultDto<ItemQuantityListDto>> GetAllItemQuantities(GetAllItemQuantityInput input);
        Task CreateOrUpdateItemQuantity(ItemQuantityDto input);
        Task<ItemQuantityDto> GetItemQuantityForEdit(int id);
        Task<GetItemQuantityForViewDto> GetItemQuantityForView(int id);
        Task DeleteItemQuantity(int? id);
        Task<FileDto> GetItemQuantitiesToExcel(string filter);
        Task<GetItemQuantityForViewDto> GetItemByData(string filter);
    }
}
