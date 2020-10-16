using System;
using Abp.Application.Services.Dto;

namespace Pharmacy.Customers.Dtos
{
    public class CustomerDto : EntityDto<int?>
    {
        public bool IsActive { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public string PersonalPhoto { get; set; }
        public string Email { get; set; }
        public string PrimaryMobileNumber { get; set; }
        public string SecondaryMobileNumber { get; set; }
        public int GenderId { get; set; }
        public int? MaritalStatusId { get; set; }
        public int NoOfDependencies { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Notes { get; set; }
        public int NationalityId { get; set; }
        public int CountryId { get; set; }
        public int? RegionId { get; set; }
        public int? CityId { get; set; }
        public string Address { get; set; }
    }
}
