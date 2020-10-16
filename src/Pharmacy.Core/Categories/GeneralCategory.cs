using Abp.Domain.Entities.Auditing;
using Shared.SeedWork;
namespace Pharmacy.Categories
{

   public class GeneralCategory : FullAuditedEntity
    {
        public LocalizedText Name { get; set; }
        public int? ParentId { get; set; }
    }
}
