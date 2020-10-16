using Abp.Application.Services;
using Pharmacy.Dto;
using Pharmacy.Logging.Dto;

namespace Pharmacy.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
