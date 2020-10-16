using Abp.Application.Services.Dto;
namespace Pharmacy.Corporates.Dtos
{
    public class CorporateDto : EntityDto<int?>
    {
        public string Name { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string Logo { get; set; }
        public string Notes { get; set; }
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public bool IsActive { get; set; }
    }
}
