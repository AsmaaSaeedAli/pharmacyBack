using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Shared.SeedWork;
using System.ComponentModel.DataAnnotations;
namespace Pharmacy.Address
{
   public class City : FullAuditedEntity, IPassivable
    {
        public string Code { get; set; }
        [Required]
        public virtual LocalizedText Name { get; set; }
        public virtual int? RegionId { get; set; }
        public Region Region { get; set; }
        public bool IsActive { get; set; }
    }
}
