using System.Threading.Tasks;
using Abp.Application.Services;
using Pharmacy.Configuration.Host.Dto;

namespace Pharmacy.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
