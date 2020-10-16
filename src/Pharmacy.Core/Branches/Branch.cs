using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Pharmacy.Address;
using Pharmacy.Lookups;
using Shared.SeedWork;

namespace Pharmacy.Branches
{
    public class Branch : FullAuditedEntity , IMustHaveTenant, IPassivable
    {
        public int TenantId { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public LocalizedText Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int? CityId { get; set; }
        public City City { get; set; }
        public int? BranchTypeId { get; set; }
        public Lookup BranchType { get; set; }
    }
}
