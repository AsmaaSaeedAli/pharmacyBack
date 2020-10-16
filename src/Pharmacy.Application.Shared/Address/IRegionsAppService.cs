using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Pharmacy.Address.RegionDtos;
using Pharmacy.Dto;

namespace Pharmacy.Address
{
    public  interface IRegionsAppService : IApplicationService
    {
        Task<PagedResultDto<RegionListDto>> GetAllRegions(GetAllRegionInput input);
        Task CreateOrUpdateRegion(RegionDto input);
        Task<RegionDto> GetRegionForEdit(int id);
        Task DeleteRegion(int? id);
        Task<FileDto> GetRegionsToExcel(string filter);
    }
}
