using Abp.Application.Services.Dto;
namespace Pharmacy.Items.ItemQuantityDtos
{
    public class GetAllItemQuantityInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
