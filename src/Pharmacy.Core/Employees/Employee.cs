using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Pharmacy.Address;
using Pharmacy.Branches;
using Pharmacy.Lookups;
using Shared.SeedWork;

namespace Pharmacy.Employees
{
    public class Employee : FullAuditedEntity, IMustHaveTenant, IPassivable
    {
        public string Code { get; set; }
        public LocalizedText FullName { get; set; }
        public string NationalId { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PersonalPhone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int NationalityId { get; set; }
        public Country Nationality { get; set; }
        public int? BranchId { get; set; }
        public Branch Branch { get; set; }
        public int? JobId { get; set; }
        public int GenderId { get; set; }
        public Lookup Gender { get; set; }
        public int TenantId { get; set; }
        public bool IsActive { get; set; }

        public string Email { get; set; }

    }
}
