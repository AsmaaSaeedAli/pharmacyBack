using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Pharmacy.Branches.Dtos;
using Pharmacy.Dto;
using System.Threading.Tasks;
namespace Pharmacy.Branches
{
    public interface IBranchesAppService : IApplicationService
    {
        Task<PagedResultDto<BranchesListDto>> GetAllBranches(GetAllBranchesInput input);
        Task CreateOrUpdateBranch(BranchDto input);
        Task<BranchDto> GetBranchForEdit(int id);
        Task<GetBranchForViewDto> GetBranchForView(int id);
        Task DeleteBranch(int? id);
        Task<FileDto> GetBranchesToExcel(string filter);
    }
}
