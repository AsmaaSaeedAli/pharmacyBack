using Abp.Application.Services.Dto;
namespace Pharmacy.Employees.Dtos
{
    public class GetAllEmployeeInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
