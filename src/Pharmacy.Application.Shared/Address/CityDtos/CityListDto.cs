using Abp.Application.Services.Dto;
namespace Pharmacy.Address.CityDtos
{
    public class CityListDto : EntityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string RegionName { get; set; }
        public bool IsActive { get; set; }
    }
}
