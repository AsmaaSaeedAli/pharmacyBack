using Microsoft.Extensions.Configuration;

namespace Pharmacy.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
