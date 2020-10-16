using Abp.Domain.Entities.Auditing;
using Shared.SeedWork;

namespace Pharmacy.Lookups
{
    public class LookupType : FullAuditedEntity
    {
        public LocalizedText Name { get; set; }
        public bool IsSystem { get; set; }
        public string Description { get; set; }
    }
}
