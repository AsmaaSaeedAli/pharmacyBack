using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Pharmacy.Items.ItemDtos;
using Pharmacy.Dto;
using Pharmacy.Items.ItemBarCodeDtos;
using Pharmacy.Invoices.Dtos;

namespace Pharmacy.Items
{
    public  interface IItemsAppService : IApplicationService
    {
        Task<PagedResultDto<ItemListDto>> GetAllItems(GetAllItemInput input);
        Task CreateOrUpdateItem(ItemDto input);
        Task<ItemDto> GetItemForEdit(int id);
        Task<GetItemForViewDto> GetItemForView(int id);
        Task DeleteItem(int? id);
        Task<FileDto> GetItemsToExcel(GetAllItemInputForExcel input);
        Task<ItemBarCodeDto> GetItemBarCodes(int itemId);
        Task SaveItemBarCode(ItemBarCodeDto input);
    }
}
