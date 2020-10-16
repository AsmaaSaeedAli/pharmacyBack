using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Pharmacy.Lookups;
using Shared.SeedWork;

namespace Pharmacy.Address
{
    public class Country : FullAuditedEntity , IPassivable
    {
        public string Code { get; set; }
        public virtual LocalizedText Name { get; set; }
        public virtual LocalizedText Nationality { get; set; }
        public virtual int? CurrencyId { get; set; }
        public Lookup Currency { get; set; }
        public bool IsActive { get; set; }
    }
}
