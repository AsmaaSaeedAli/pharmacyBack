using System;
using Abp.Application.Services.Dto;

namespace Pharmacy.Customers.Dtos
{
   public class GetCustomerForViewDto : EntityDto
    {
        public string Code { get; set; }
        public string FullName { get; set; }
        public string PersonalPhoto { get; set; }

        public string Email { get; set; }
        public string PrimaryMobileNumber { get; set; }
        public string SecondaryMobileNumber { get; set; }

        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public int NoOfDependencies { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Notes { get; set; }

        public string Nationality { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public bool IsActive { get; set; }
        public string Address { get; set; }

    }
}
