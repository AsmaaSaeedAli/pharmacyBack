using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Pharmacy.Address.CountryDtos;
using Pharmacy.Dto;

namespace Pharmacy.Address
{
    public  interface ICountriesAppService : IApplicationService
    {
        Task<PagedResultDto<CountryListDto>> GetAllCountries(GetAllCountryInput input);
        Task CreateOrUpdateCountry(CountryDto input);
        Task<CountryDto> GetCountryForEdit(int id);
        Task DeleteCountry(int? id);
        Task<FileDto> GetCountriesToExcel(string filter);
    }
}
