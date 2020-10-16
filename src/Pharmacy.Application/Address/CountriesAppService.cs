using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Address.Exporting;
using Pharmacy.Dto;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using Pharmacy.Lookups;
using Pharmacy.Address.CountryDtos;

namespace Pharmacy.Address
{
    public class CountriesAppService : PharmacyAppServiceBase, ICountriesAppService
    {
        private readonly IRepository<Lookup> _lookupRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly IAddressExcelExporter _addressExcelExporter;
        public CountriesAppService(IAddressExcelExporter addressExcelExporter,
            IRepository<Country> countryRepository, IRepository<Lookup> lookupRepository)
        {
            _addressExcelExporter = addressExcelExporter;
            _countryRepository = countryRepository;
            _lookupRepository = lookupRepository;
        }

        public async Task CreateOrUpdateCountry(CountryDto input)
        {
            if (input.Id == null)
                await CreateAsync(input);
            else
                await UpdateAsync(input);

        }

        public async Task DeleteCountry(int? id)
        {
            if (id.HasValue)
                await _countryRepository.DeleteAsync(id.Value);
        }

        public async Task<PagedResultDto<CountryListDto>> GetAllCountries(GetAllCountryInput input)
        {
            var filteredCountries = _countryRepository.GetAll().AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.Name.StringValue.ToLower().Contains(input.Filter.ToLower().Trim())
                || !string.IsNullOrEmpty(e.Code) && e.Code.ToLower().Trim().Contains(input.Filter.ToLower().Trim()));

            var query = from country in filteredCountries
                        join currency in _lookupRepository.GetAll().AsNoTracking() on country.CurrencyId equals currency.Id into currencies
                        from currency in currencies.DefaultIfEmpty()
                        select new CountryListDto
                        {
                            Id = country.Id,
                            Code = country.Code,
                            Name = country.Name.CurrentCultureText,
                            Nationality = country.Nationality.CurrentCultureText,
                            IsActive = country.IsActive,
                            CurrencyName = currency == null ? "" : currency.Name.CurrentCultureText
                        };
            var totalCount = await query.CountAsync();
            var countries = await query.OrderBy(input.Sorting ?? "id desc").PageBy(input).ToListAsync();
            return new PagedResultDto<CountryListDto>(totalCount, countries);
        }

        public async Task<CountryDto> GetCountryForEdit(int id)
        {
            var country = await _countryRepository.GetAsync(id);
            return ObjectMapper.Map<CountryDto>(country);
        }

        public async Task<FileDto> GetCountriesToExcel(string filter)
        {
            var countries = await _countryRepository.GetAllIncluding(r => r.Currency).AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(filter), e => e.Name.StringValue.ToLower().Contains(filter.ToLower().Trim())
                    || !string.IsNullOrEmpty(e.Code) && e.Code.ToLower().Trim().Contains(filter.ToLower().Trim()))
                .Select(country => new CountryListDto
                {
                    Id = country.Id,
                    Code = country.Code,
                    Name = country.Name.CurrentCultureText,
                    Nationality = country.Nationality.CurrentCultureText,
                    IsActive = country.IsActive,
                    CurrencyName = country.Currency.Name.CurrentCultureText
                }).ToListAsync();

            return _addressExcelExporter.ExportCountriesToFile(countries);
        }

        private async Task CreateAsync(CountryDto input)
        {
            var country = ObjectMapper.Map<Country>(input);
            await _countryRepository.InsertAsync(country);
        }
        private async Task UpdateAsync(CountryDto input)
        {
            if (input.Id != null)
            {
                var region = await _countryRepository.FirstOrDefaultAsync((int)input.Id);
                ObjectMapper.Map(input, region);
            }
        }
    }
}
