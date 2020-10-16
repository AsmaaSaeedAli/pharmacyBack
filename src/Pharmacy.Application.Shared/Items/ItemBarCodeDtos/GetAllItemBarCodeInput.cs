using Abp.Application.Services.Dto;
namespace Pharmacy.Items.ItemBarCodeDtos
{
    public class GetAllItemBarCodeInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
