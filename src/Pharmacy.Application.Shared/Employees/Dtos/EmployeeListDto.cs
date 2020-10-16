using System;
using Abp.Application.Services.Dto;

namespace Pharmacy.Employees.Dtos
{
    public class EmployeeListDto : EntityDto
    {
        public string Code { get; set; }
        public string FullName { get; set; }
        public string NationalId { get; set; }
        public string Nationality { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public string BranchName { get; set; }
        public string Email { get; set; }
    }
}
