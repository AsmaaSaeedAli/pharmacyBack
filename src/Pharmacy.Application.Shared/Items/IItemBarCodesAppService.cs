using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Pharmacy.Items.ItemBarCodeDtos;
using Pharmacy.Dto;

namespace Pharmacy.Items
{
    public  interface IItemBarCodesAppService : IApplicationService
    {
        Task<PagedResultDto<ItemBarCodeListDto>> GetAllItemBarCodes(GetAllItemBarCodeInput input);
        Task CreateOrUpdateItemBarCode(ItemBarCodeDto input);
        Task<ItemBarCodeDto> GetItemBarCodeForEdit(int id);
        Task<GetItemBarCodeForViewDto> GetItemBarCodeForView(int id);
        Task DeleteItemBarCode(int? id);
        Task<FileDto> GetItemBarCodesToExcel(string filter);
    }
}
