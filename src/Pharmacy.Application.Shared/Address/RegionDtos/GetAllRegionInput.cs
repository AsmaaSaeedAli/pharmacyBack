using Abp.Application.Services.Dto;
namespace Pharmacy.Address.RegionDtos
{
    public class GetAllRegionInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
