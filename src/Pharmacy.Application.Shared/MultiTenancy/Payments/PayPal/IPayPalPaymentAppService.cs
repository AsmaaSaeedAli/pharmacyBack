using System.Threading.Tasks;
using Abp.Application.Services;
using Pharmacy.MultiTenancy.Payments.PayPal.Dto;

namespace Pharmacy.MultiTenancy.Payments.PayPal
{
    public interface IPayPalPaymentAppService : IApplicationService
    {
        Task ConfirmPayment(long paymentId, string paypalOrderId);

        PayPalConfigurationDto GetConfiguration();
    }
}
