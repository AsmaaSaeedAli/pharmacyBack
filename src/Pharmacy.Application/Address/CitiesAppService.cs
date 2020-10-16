using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Address.Exporting;
using Pharmacy.Dto;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using Pharmacy.Address.CityDtos;

namespace Pharmacy.Address
{
    public class CitiesAppService : PharmacyAppServiceBase, ICitiesAppService
    {

        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Region> _regionRepository;
        private readonly IAddressExcelExporter _addressExcelExporter;

        public CitiesAppService(IRepository<Region> regionRepository, IRepository<City> cityRepository, IAddressExcelExporter addressExcelExporter)
        {
            _regionRepository = regionRepository;
            _cityRepository = cityRepository;
            _addressExcelExporter = addressExcelExporter;
        }
        public async Task CreateOrUpdateCity(CityDto input)
        {
            if (input.Id == null)
                await CreateAsync(input);
            else
                await UpdateAsync(input);
        }
        public async Task DeleteCity(int? id)
        {
            if (id.HasValue)
                await _cityRepository.DeleteAsync(id.Value);
        }

        public async Task<PagedResultDto<CityListDto>> GetAllCities(GetAllCityInput input)
        {
            var filteredCities= _cityRepository.GetAll().AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.Name.StringValue.ToLower().Contains(input.Filter.ToLower().Trim())
                || !string.IsNullOrEmpty(e.Code) && e.Code.ToLower().Trim().Contains(input.Filter.ToLower().Trim()));

            var query = from city in filteredCities
                        join region in _regionRepository.GetAll().AsNoTracking() on city.RegionId equals region.Id into regions
                        from region in regions.DefaultIfEmpty()
                        select new CityListDto
                        {
                            Id = city.Id,
                            Code = city.Code,
                            Name = city.Name.CurrentCultureText,
                            IsActive = city.IsActive,
                            RegionName = region == null ? "" : region.Name.CurrentCultureText
                        };
            var totalCount = await query.CountAsync();
            var cities = await query.OrderBy(input.Sorting ?? "id desc").PageBy(input).ToListAsync();
            return new PagedResultDto<CityListDto>(totalCount, cities);
        }

        public async Task<CityDto> GetCityForEdit(int id)
        {
            var city = await _cityRepository.GetAsync(id);
            return ObjectMapper.Map<CityDto>(city);
        }

        public async Task<FileDto> GetCitiesToExcel(string filter)
        {
            var cities = await _cityRepository.GetAllIncluding(r => r.Region).AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(filter), e => e.Name.StringValue.ToLower().Contains(filter.ToLower().Trim())
                    || !string.IsNullOrEmpty(e.Code) && e.Code.ToLower().Trim().Contains(filter.ToLower().Trim()))
                .Select(city => new CityListDto
                {
                    Id = city.Id,
                    Code = city.Code,
                    Name = city.Name.CurrentCultureText,
                    IsActive = city.IsActive,
                    RegionName = city.Region.Name.CurrentCultureText
                }).ToListAsync();

            return _addressExcelExporter.ExportCitiesToFile(cities);
        }

        private async Task CreateAsync(CityDto input)
        {
            var city = ObjectMapper.Map<City>(input);
            await _cityRepository.InsertAsync(city);
        }
        private async Task UpdateAsync(CityDto input)
        {
            if (input.Id != null)
            {
                var city = await _cityRepository.FirstOrDefaultAsync((int)input.Id);
                ObjectMapper.Map(input, city);
            }
        }

    }
}
