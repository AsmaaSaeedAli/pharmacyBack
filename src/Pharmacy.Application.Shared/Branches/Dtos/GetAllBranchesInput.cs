using Abp.Application.Services.Dto;
namespace Pharmacy.Branches.Dtos
{
    public class GetAllBranchesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
