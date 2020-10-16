using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Shared.SeedWork;
using System.ComponentModel.DataAnnotations;
namespace Pharmacy.Address
{
    public class Region : FullAuditedEntity, IPassivable
    {
        public string Code { get; set; }
        [Required]
        public virtual LocalizedText Name { get; set; }

        public virtual int? CountryId { get; set; }

        public Country Country { get; set; }
        public bool IsActive { get; set; }

    }
}
