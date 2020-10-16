using Abp.Application.Services.Dto;
namespace Pharmacy.Items.ItemPriceDtos
{
    public class GetAllItemPriceInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
