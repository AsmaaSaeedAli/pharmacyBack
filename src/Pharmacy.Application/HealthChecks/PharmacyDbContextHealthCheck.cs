using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Pharmacy.EntityFrameworkCore;

namespace Pharmacy.HealthChecks
{
    public class PharmacyDbContextHealthCheck : IHealthCheck
    {
        private readonly DatabaseCheckHelper _checkHelper;

        public PharmacyDbContextHealthCheck(DatabaseCheckHelper checkHelper)
        {
            _checkHelper = checkHelper;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            if (_checkHelper.Exist("db"))
            {
                return Task.FromResult(HealthCheckResult.Healthy("PharmacyDbContext connected to database."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("PharmacyDbContext could not connect to database"));
        }
    }
}
