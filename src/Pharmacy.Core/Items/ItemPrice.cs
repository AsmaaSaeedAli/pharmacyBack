using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Pharmacy.Items
{
   public class ItemPrice : FullAuditedEntity, IMayHaveTenant, IPassivable
    {
        public int? TenantId { get; set; }
        public bool IsActive { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        

    }
}
