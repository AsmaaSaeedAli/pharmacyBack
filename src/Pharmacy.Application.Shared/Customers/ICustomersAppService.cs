using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Pharmacy.Customers.Dtos;
using Pharmacy.Dto;
using System.Threading.Tasks;

namespace Pharmacy.Customers
{
    public interface ICustomersAppService : IApplicationService
    {
        Task<PagedResultDto<CustomerListDto>> GetAllCustomers(GetAllCustomerInput input);
        Task CreateOrUpdateCustomer(CustomerDto input);
        Task<CustomerDto> GetCustomerForEdit(int id);
        Task<GetCustomerForViewDto> GetCustomerForView(int id);
        Task DeleteCustomer(int? id);
        Task<FileDto> GetCustomersToExcel(GetAllCustomerForExcelInput filter);
        Task<GetLiteCustomerData> GetLiteCustomerData(string filter);
        Task<PagedResultDto<GetCustomerLoyaltyOutput>> GetCustomerLoyalty(GetCustomerLoyaltyInput input);
    }
}
