using System.Threading.Tasks;
using Abp.Application.Services;
using Pharmacy.Configuration.Tenants.Dto;

namespace Pharmacy.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}
