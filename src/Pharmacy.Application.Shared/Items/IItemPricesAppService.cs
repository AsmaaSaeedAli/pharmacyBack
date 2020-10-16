using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Pharmacy.Items.ItemPriceDtos;
using Pharmacy.Dto;

namespace Pharmacy.Items
{
    public  interface IItemPricesAppService : IApplicationService
    {
        Task<PagedResultDto<ItemPriceListDto>> GetAllItemPrices(GetAllItemPriceInput input);
        Task CreateOrUpdateItemPrice(ItemPriceDto input);
        Task<ItemPriceDto> GetItemPriceForEdit(int id);
        Task<GetItemPriceForViewDto> GetItemPriceForView(int id);
        Task DeleteItemPrice(int? id);
        Task<FileDto> GetItemPricesToExcel(string filter);
        Task<PagedResultDto<ItemPriceDto>> GetItemPricesByItemId(int itemId);
    }
}
