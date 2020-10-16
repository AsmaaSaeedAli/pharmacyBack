using Abp.Application.Services.Dto;

namespace Pharmacy.Customers.Dtos
{
    public class CustomerListDto : EntityDto
    {
        public string Code { get; set; }
        public string FullName { get; set; }
        public string PrimaryPhoneNumber { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }
        public int NoOfDependencies { get; set; }
        public bool IsActive { get; set; }
    }
}
