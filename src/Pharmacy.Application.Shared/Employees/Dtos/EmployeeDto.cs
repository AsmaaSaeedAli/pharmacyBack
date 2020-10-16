using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Pharmacy.Employees.Dtos
{
    public class EmployeeDto : EntityDto<int?>
    {
        public string Code { get; set; }
        public string FullName { get; set; }
        
        [Required]
        public string NationalId { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        public string PersonalPhoto { get; set; }
        public string DateOfBirth { get; set; }
        [Required]
        public int NationalityId { get; set; }
        public int? JobId { get; set; }
        [Required]
        public int GenderId { get; set; }

        public int? BranchId { get; set; }
        public bool IsActive { get; set; }
        public bool HasUser { get; set; }

    }
}
