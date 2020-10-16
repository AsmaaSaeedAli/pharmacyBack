using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Pharmacy.Corporates.Dtos;
using Pharmacy.Dto;
using System.Threading.Tasks;
namespace Pharmacy.Corporates
{
    public interface ICorporatesAppService : IApplicationService
    {
        Task<PagedResultDto<CorporatesListDto>> GetAllCorporates(GetAllCorporatesInput input);
        Task CreateOrUpdateCorporate(CorporateDto input);
        Task<CorporateDto> GetCorporateForEdit(int id);
        Task<GetCorporateForViewDto> GetCorporateForView(int id);
        Task DeleteCorporate(int? id);
        Task<FileDto> GetCorporatesToExcel(string filter);
    }
}
