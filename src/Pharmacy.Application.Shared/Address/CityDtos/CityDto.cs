using Abp.Application.Services.Dto;
namespace Pharmacy.Address.CityDtos
{
    public class CityDto : EntityDto<int?>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int? RegionId { get; set; }
        public bool IsActive { get; set; }
    }
}
