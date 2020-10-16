using System.Threading.Tasks;
using Abp.Application.Services;
using Pharmacy.Install.Dto;

namespace Pharmacy.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}