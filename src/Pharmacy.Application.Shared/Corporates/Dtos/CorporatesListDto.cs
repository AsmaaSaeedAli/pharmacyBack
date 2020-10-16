using Abp.Application.Services.Dto;

namespace Pharmacy.Corporates.Dtos
{
    public class CorporatesListDto : EntityDto
    {
        public string Name { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string Notes { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public bool IsActive { get; set; }
    }
}
