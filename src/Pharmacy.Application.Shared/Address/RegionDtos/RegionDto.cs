using Abp.Application.Services.Dto;
namespace Pharmacy.Address.RegionDtos
{
    public class RegionDto : EntityDto<int?>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int? CountryId { get; set; }
        public bool IsActive { get; set; }
    }
}
