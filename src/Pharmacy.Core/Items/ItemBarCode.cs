using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
namespace Pharmacy.Items
{
   public class ItemBarCode : FullAuditedEntity, IMustHaveTenant, IPassivable
    {
        public int TenantId { get; set; }
        public bool IsActive { get; set; }
        public int ItemId { get; set; }
       // public Item Item { get; set; }
        public string BarCode { get; set; }

    }
}
