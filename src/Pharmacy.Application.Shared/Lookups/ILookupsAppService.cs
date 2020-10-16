using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Pharmacy.Dto;
using Pharmacy.Lookups.Dtos;

namespace Pharmacy.Lookups
{
    public interface ILookupsAppService :  IApplicationService
    {
        Task<List<NameValueDto>> GetAllLookupTypes();
        Task<PagedResultDto<LookupListDto>> GetAllLookups(GetAllLookupsInput input);
        Task CreateOrUpdateLookup(LookupDto input);
        Task<LookupDto> GetLookupForEdit(int id);
        Task<LookupForViewDto> GetLookupForView(int id);
        Task DeleteLookup(int? id);
        Task<FileDto> GetLookupsToExcel(GetAllLookupsForExcelInput input);

    }
}
