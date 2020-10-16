using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Shared.SeedWork;

namespace Pharmacy.ItemClasses
{
    public class ItemClass : FullAuditedEntity, IPassivable
    {
        public bool IsActive { get; set; }
        public LocalizedText Name { get; set; }
        public string Code { get; set; }
        public int ItemNumberStart { get; set; }

    }
}
