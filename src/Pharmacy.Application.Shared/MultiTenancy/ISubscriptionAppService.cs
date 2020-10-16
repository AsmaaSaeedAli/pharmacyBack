using System.Threading.Tasks;
using Abp.Application.Services;

namespace Pharmacy.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task DisableRecurringPayments();

        Task EnableRecurringPayments();
    }
}
