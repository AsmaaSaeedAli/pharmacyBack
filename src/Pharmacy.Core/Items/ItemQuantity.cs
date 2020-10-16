using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Pharmacy.Lookups;
using Pharmacy.Branches;

namespace Pharmacy.Items
{
   public class ItemQuantity : FullAuditedEntity
    {
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public int ItemPriceId { get; set; }
        public ItemPrice ItemPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal QuantityLimit { get; set; }
        public int UnitId { get; set; }
        public Lookup Unit { get; set; }
      
    }
}
