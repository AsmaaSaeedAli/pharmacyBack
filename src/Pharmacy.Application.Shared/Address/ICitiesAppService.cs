using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Pharmacy.Address.CityDtos;
using Pharmacy.Dto;

namespace Pharmacy.Address
{
    public  interface ICitiesAppService : IApplicationService
    {
        Task<PagedResultDto<CityListDto>> GetAllCities(GetAllCityInput input);
        Task CreateOrUpdateCity(CityDto input);
        Task<CityDto> GetCityForEdit(int id);
        Task DeleteCity(int? id);
        Task<FileDto> GetCitiesToExcel(string filter);
    }
}
