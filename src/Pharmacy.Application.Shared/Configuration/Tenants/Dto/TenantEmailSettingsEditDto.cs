using Abp.Auditing;
using Pharmacy.Configuration.Dto;

namespace Pharmacy.Configuration.Tenants.Dto
{
    public class TenantEmailSettingsEditDto : EmailSettingsEditDto
    {
        public bool UseHostDefaultEmailSettings { get; set; }
    }
}