using System.ComponentModel.DataAnnotations;
using Abp.Organizations;

namespace Pharmacy.Organizations.Dto
{
    public class CreateOrganizationUnitInput
    {
        public long? ParentId { get; set; }

        [Required]
        [StringLength(OrganizationUnit.MaxDisplayNameLength)]
        public string DisplayName { get; set; } 
    }
}