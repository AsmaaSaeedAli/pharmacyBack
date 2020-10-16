using Abp.Dependency;

namespace Pharmacy
{
    public class AppFolders : IAppFolders, ISingletonDependency
    {
        public string SampleProfileImagesFolder { get; set; }

        public string WebLogsFolder { get; set; }

        public string TempFileDownloadFolder { get; set; }
    }
}