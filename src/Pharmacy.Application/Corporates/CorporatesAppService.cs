using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Address;
using Pharmacy.Authorization;
using Pharmacy.Corporates.Dtos;
using Pharmacy.Corporates.Exporting;
using Pharmacy.Dto;
namespace Pharmacy.Corporates
{
    public class CorporatesAppService : PharmacyAppServiceBase, ICorporatesAppService
    {
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<Corporate> _corporateRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly ICorporatesExcelExporter _corporatexcelExporter;
        public CorporatesAppService(IRepository<Country> countryRepository, IRepository<Corporate> corporateRepository, 
            ICorporatesExcelExporter corporatexcelExporter, IRepository<City> cityRepository)
        {
            _countryRepository = countryRepository;
            _corporateRepository = corporateRepository;
            _corporatexcelExporter = corporatexcelExporter;
            _cityRepository = cityRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Corporates_Manage)]
        public async Task CreateOrUpdateCorporate(CorporateDto input)
        {
            if (input.Id == null)
                await CreateAsync(input);
            else
                await UpdateAsync(input);

        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Corporates_Manage)]
        public async Task DeleteCorporate(int? id)
        {
            if (id.HasValue)
                await _corporateRepository.DeleteAsync(id.Value);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Corporates)]
        public async Task<PagedResultDto<CorporatesListDto>> GetAllCorporates(GetAllCorporatesInput input)
        {
            var filteredCorporates = _corporateRepository.GetAll().AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.Name.StringValue.ToLower().Contains(input.Filter.ToLower().Trim()));

            var query = from Corporate in filteredCorporates
                        join Country in _countryRepository.GetAll().AsNoTracking() on Corporate.CountryId equals Country.Id into countries
                        from Country in countries.DefaultIfEmpty()
                        join city in _cityRepository.GetAll().AsNoTracking() on Corporate.CityId equals city.Id into cities
                        from city in cities.DefaultIfEmpty()
                        select new CorporatesListDto
                        {
                            Id = Corporate.Id,
                            Name = Corporate.Name.CurrentCultureText,
                            ContactName = Corporate.ContactName,
                            ContactPhone = Corporate.ContactPhone,
                            ContactEmail = Corporate.ContactEmail,
                            Notes = Corporate.Notes,
                            CountryName = Country == null ? "" : Country.Name.CurrentCultureText,
                            CityName = city == null ? "" : city.Name.CurrentCultureText,
                            IsActive = Corporate.IsActive
                        };
            var totalCount = await query.CountAsync();
            var corporates = await query.OrderBy(input.Sorting ?? "id desc").PageBy(input).ToListAsync();
            return new PagedResultDto<CorporatesListDto>(totalCount, corporates);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Corporates_Manage)]
        public async Task<CorporateDto> GetCorporateForEdit(int id)
        {
            var Corporate = await _corporateRepository.GetAsync(id);
            return ObjectMapper.Map<CorporateDto>(Corporate);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Corporates_Manage)]
        public async Task<GetCorporateForViewDto> GetCorporateForView(int id)
        {
            var corporate = await _corporateRepository.GetAllIncluding(b => b.Country, b => b.City).FirstOrDefaultAsync(b => b.Id == id);
            if (corporate == null)
                throw new UserFriendlyException($"No Corporate With Id {id}");
            
            var output = new GetCorporateForViewDto
            {
                Id = corporate.Id,
                Name = corporate.Name.CurrentCultureText,
                IsActive = corporate.IsActive,
                ContactName = corporate.ContactName,
                ContactPhone = corporate.ContactPhone,
                ContactEmail = corporate.ContactEmail,
                Logo = corporate.Logo,
                Notes = corporate.Notes,
                CountryName = corporate.Country.Name.CurrentCultureText,
                CityName = corporate.City.Name.CurrentCultureText
            };
            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Corporates_Export)]
        public async Task<FileDto> GetCorporatesToExcel(string filter)
        {
            var filteredCorporates = _corporateRepository.GetAll().AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(filter), e => e.Name.StringValue.ToLower().Contains(filter.ToLower().Trim()));

            var query = from Corporate in filteredCorporates
                join Country in _countryRepository.GetAll().AsNoTracking() on Corporate.CountryId equals Country.Id into CorporateTypes
                from Country in CorporateTypes.DefaultIfEmpty()
                join city in _cityRepository.GetAll().AsNoTracking() on Corporate.CityId equals city.Id into cities
                from city in cities.DefaultIfEmpty()
                select new CorporatesListDto
                {
                    Id = Corporate.Id,
                    Name = Corporate.Name.CurrentCultureText,
                    ContactName = Corporate.ContactName,
                    ContactPhone = Corporate.ContactPhone,
                    ContactEmail = Corporate.ContactEmail,
                    Notes = Corporate.Notes,
                    CountryName = Corporate.Country.Name.CurrentCultureText,
                    CityName = city == null ? "" : city.Name.CurrentCultureText,
                    IsActive = Corporate.IsActive
                };
            var corporates = await query.ToListAsync();
            return _corporatexcelExporter.ExportCorporatesToFile(corporates);
        }

        private async Task CreateAsync(CorporateDto input)
        {
            var corporate = ObjectMapper.Map<Corporate>(input);
            await _corporateRepository.InsertAsync(corporate);
        }
        private async Task UpdateAsync(CorporateDto input)
        {
            if (input.Id != null)
            {
                var corporate = await _corporateRepository.FirstOrDefaultAsync((int)input.Id);
                ObjectMapper.Map(input, corporate);
            }
        }
    }
}
