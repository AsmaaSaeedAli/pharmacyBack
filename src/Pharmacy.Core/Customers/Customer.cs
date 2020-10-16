using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Shared.SeedWork;

namespace Pharmacy.Customers
{
    public class Customer : FullAuditedEntity, IMustHaveTenant, IPassivable
    {
        public int TenantId { get; set; }
        public bool IsActive { get; set; }
        public string Code { get; set; }
        public LocalizedText  FullName { get; set; }
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
