using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Shared.SeedWork;

namespace Pharmacy.Lookups
{
    public class Lookup : FullAuditedEntity, IPassivable
    {
        public LocalizedText Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int LookupTypeId { get; set; }
        public bool IsActive { get; set; }
    }
}
