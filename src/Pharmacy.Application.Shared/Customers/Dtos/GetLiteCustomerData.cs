using Abp.Application.Services.Dto;

namespace Pharmacy.Customers.Dtos
{
    public class GetLiteCustomerData : EntityDto
    {
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
    }
}
