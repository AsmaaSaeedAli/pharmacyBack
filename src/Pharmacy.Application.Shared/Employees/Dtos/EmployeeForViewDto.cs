using Abp.Application.Services.Dto;
namespace Pharmacy.Employees.Dtos
{
    public class EmployeeForViewDto : EntityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string NationalId { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PersonalPhoto { get; set; }
        public string Nationality { get; set; }
        public string JobName { get; set; }
        public string GenderName { get; set; }
        public string BranchName { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }



    }
}
