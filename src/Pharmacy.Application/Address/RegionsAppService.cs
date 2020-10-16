using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Address.Exporting;
using Pharmacy.Address.RegionDtos;
using Pharmacy.Dto;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
namespace Pharmacy.Address
{
    public class RegionsAppService : PharmacyAppServiceBase, IRegionsAppService
    {
        private readonly IRepository<Region> _regionRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly IAddressExcelExporter _addressExcelExporter;
        public RegionsAppService(IRepository<Region> regionRepository, IAddressExcelExporter addressExcelExporter,
            IRepository<Country> countryRepository)
        {
            _regionRepository = regionRepository;
            _addressExcelExporter = addressExcelExporter;
            _countryRepository = countryRepository;
        }

        public async Task CreateOrUpdateRegion(RegionDto input)
        {
            if (input.Id == null)
                await CreateAsync(input);
            else
                await UpdateAsync(input);

        }

        public async Task DeleteRegion(int? id)
        {
            if (id.HasValue)
                await _regionRepository.DeleteAsync(id.Value);
        }

        public async Task<PagedResultDto<RegionListDto>> GetAllRegions(GetAllRegionInput input)
        {
            var filteredRegions = _regionRepository.GetAll().AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.Name.StringValue.ToLower().Contains(input.Filter.ToLower().Trim())
                || !string.IsNullOrEmpty(e.Code) && e.Code.ToLower().Trim().Contains(input.Filter.ToLower().Trim()));

            var query = from region in filteredRegions
                        join country in _countryRepository.GetAll().AsNoTracking() on region.CountryId equals country.Id into countries
                        from country in countries.DefaultIfEmpty()
                        select new RegionListDto
                        {
                            Id = region.Id,
                            Code = region.Code,
                            Name = region.Name.CurrentCultureText,
                            IsActive = region.IsActive,
                            CountryName = country == null ? "" : country.Name.CurrentCultureText
                        };
            var totalCount = await query.CountAsync();
            var regions = await query.OrderBy(input.Sorting ?? "id desc").PageBy(input).ToListAsync();
            return new PagedResultDto<RegionListDto>(totalCount, regions);
        }
        
        public async Task<RegionDto> GetRegionForEdit(int id)
        {
            var region = await _regionRepository.GetAsync(id);
            return ObjectMapper.Map<RegionDto>(region);
        }

        public async Task<FileDto> GetRegionsToExcel(string filter)
        {
            var regions = await _regionRepository.GetAllIncluding(r=>r.Country).AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(filter), e =>e.Name.StringValue.ToLower().Contains(filter.ToLower().Trim())
                    || !string.IsNullOrEmpty(e.Code) && e.Code.ToLower().Trim().Contains(filter.ToLower().Trim()))
                .Select(region => new RegionListDto
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name.CurrentCultureText,
                    IsActive = region.IsActive,
                    CountryName =region.Country.Name.CurrentCultureText 
                }).ToListAsync();

            return _addressExcelExporter.ExportRegionsToFile(regions);
        }

        private async Task CreateAsync(RegionDto input)
        {
            var region = ObjectMapper.Map<Region>(input);
            await _regionRepository.InsertAsync(region);
        }
        private async Task UpdateAsync(RegionDto input)
        {
            if (input.Id != null)
            {
                var region = await _regionRepository.FirstOrDefaultAsync((int)input.Id);
                ObjectMapper.Map(input, region);
            }
        }

    }
}
