using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Dto;
using Pharmacy.Lookups.Dtos;
using Pharmacy.Lookups.Exporting;

namespace Pharmacy.Lookups
{
    public class LookupsAppService : PharmacyAppServiceBase, ILookupsAppService
    {
        private readonly IRepository<Lookup> _lookupRepository;
        private readonly IRepository<LookupType> _lookupTypeRepository;
        private readonly ILookupsExcelExporter _lookupsExcelExporter;
        public LookupsAppService(IRepository<Lookup> lookupRepository, IRepository<LookupType> lookupTypeRepository, ILookupsExcelExporter lookupsExcelExporter)
        {
            _lookupRepository = lookupRepository;
            _lookupTypeRepository = lookupTypeRepository;
            _lookupsExcelExporter = lookupsExcelExporter;
        }

        public async Task CreateOrUpdateLookup(LookupDto input)
        {
            if (input.Id == null)
                await CreateAsync(input);
            else
                await UpdateAsync(input);

        }

        public async Task DeleteLookup(int? id)
        {
            if (id.HasValue)
                await _lookupRepository.DeleteAsync(id.Value);
        }

        public async Task<PagedResultDto<LookupListDto>> GetAllLookups(GetAllLookupsInput input)
        {
            var filteredLookups = _lookupRepository.GetAll().AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e =>
                    e.Name.StringValue.ToLower().Contains(input.Filter.ToLower().Trim())
                || !string.IsNullOrEmpty(e.Code) && e.Code.ToLower().Trim().Contains(input.Filter.ToLower().Trim()))
                .WhereIf(input.LookupTypeId != null, e => e.LookupTypeId == input.LookupTypeId);

            var query = from lookup in filteredLookups
                        join lookupType in _lookupTypeRepository.GetAll().AsNoTracking() on lookup.LookupTypeId equals lookupType.Id into lookupTypes
                        from lookupType in lookupTypes.DefaultIfEmpty()
                        select new LookupListDto
                        {
                            Id = lookup.Id,
                            Code = lookup.Code,
                            Name = lookup.Name.CurrentCultureText,
                            IsActive = lookup.IsActive
                        };
            var totalCount = await query.CountAsync();
            var lookups = await query.OrderBy(input.Sorting ?? "id asc").PageBy(input).ToListAsync();
            return new PagedResultDto<LookupListDto>(totalCount, lookups);
        }

        public async Task<List<NameValueDto>> GetAllLookupTypes()
        {
            var lookupTypes = await _lookupTypeRepository.GetAll().AsNoTracking().Where(l => !l.IsSystem).Select(l => new NameValueDto
            {
                Value = l.Id.ToString(),
                Name = l.Name.CurrentCultureText
            }).ToListAsync();

            return lookupTypes;
        }

        public async Task<LookupDto> GetLookupForEdit(int id)
        {
            var lookup = await _lookupRepository.GetAsync(id);
            return ObjectMapper.Map<LookupDto>(lookup);
        }

        public async Task<LookupForViewDto> GetLookupForView(int id)
        {
            var lookup = await _lookupRepository.GetAsync(id);
            var lookupType = await _lookupTypeRepository.GetAsync(lookup.LookupTypeId);
            var output = new LookupForViewDto
            {
                Id = lookup.Id,
                Code = lookup.Code,
                Name = lookup.Name.CurrentCultureText,
                Description = lookup.Description,
                IsActive = lookup.IsActive,
                LookupTypeName = lookupType?.Name?.CurrentCultureText
            };
            return output;
        }

        public async Task<FileDto> GetLookupsToExcel(GetAllLookupsForExcelInput input)
        {
            var lookups = await _lookupRepository.GetAll().AsNoTracking().Where(e => e.LookupTypeId == input.LookupTypeId)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e =>
                    e.Name.StringValue.ToLower().Contains(input.Filter.ToLower().Trim())
                    || !string.IsNullOrEmpty(e.Code) && e.Code.ToLower().Trim().Contains(input.Filter.ToLower().Trim()))
                .Select(lookup => new LookupListDto
            {
                Id = lookup.Id,
                Code = lookup.Code,
                Name = lookup.Name.CurrentCultureText,
                IsActive = lookup.IsActive
            }).ToListAsync();

            var fileName = (await _lookupTypeRepository.GetAsync(input.LookupTypeId))?.Name.CurrentCultureText;
            return _lookupsExcelExporter.ExportToFile(lookups, fileName);
        }

        private async Task CreateAsync(LookupDto input)
        {
            var lookup = ObjectMapper.Map<Lookup>(input);
            await _lookupRepository.InsertAsync(lookup);
        }
        private async Task UpdateAsync(LookupDto input)
        {
            if (input.Id != null)
            {
                var lookup = await _lookupRepository.FirstOrDefaultAsync((int)input.Id);
                ObjectMapper.Map(input, lookup);
            }
        }

    }
}
