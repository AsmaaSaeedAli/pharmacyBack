using Abp.Application.Services.Dto;
namespace Pharmacy.Address.CityDtos
{
    public class GetAllCityInput :PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
