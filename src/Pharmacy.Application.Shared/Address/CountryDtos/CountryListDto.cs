using Abp.Application.Services.Dto;
namespace Pharmacy.Address.CountryDtos
{
    public class CountryListDto : EntityDto
    {
        public string Name { get; set; }
        public string Nationality { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public string CurrencyName { get; set; }
    }
}
