using Abp.Application.Services.Dto;

namespace Pharmacy.Address.RegionDtos
{
    public class RegionListDto : EntityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        public bool IsActive { get; set; }
    }
}
