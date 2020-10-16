using Abp.Application.Services.Dto;

namespace Pharmacy.Address.CountryDtos
{
    public class CountryDto : EntityDto<int?>
    {
        public string Name { get; set; }
        public string Nationality { get; set; }
        public string Code { get; set; }
        public int? CurrencyId { get; set; }
        public bool IsActive { get; set; }
    }
}
