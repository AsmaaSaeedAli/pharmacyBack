using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Pharmacy.Dto;
using Pharmacy.Employees.Dtos;
using System.Threading.Tasks;

namespace Pharmacy.Employees
{
    public interface IEmployeesAppService : IApplicationService
    {

        Task<PagedResultDto<EmployeeListDto>> GetAllEmployees(GetAllEmployeeInput input);
        Task CreateOrUpdateEmployee(EmployeeDto input);
        Task<EmployeeDto> GetEmployeeForEdit(int id);
        Task<EmployeeForViewDto> GetEmployeeForView(int id);
        Task DeleteEmployee(int? id);
        Task<FileDto> GetEmployeesToExcel(string filter);
    }
}
