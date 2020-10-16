using Abp.Application.Services.Dto;
namespace Pharmacy.Address.CountryDtos
{
    public class GetAllCountryInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
