using System.Threading.Tasks;
using Abp.Application.Services;
using Pharmacy.MultiTenancy.Payments.Dto;
using Pharmacy.MultiTenancy.Payments.Stripe.Dto;

namespace Pharmacy.MultiTenancy.Payments.Stripe
{
    public interface IStripePaymentAppService : IApplicationService
    {
        Task ConfirmPayment(StripeConfirmPaymentInput input);

        StripeConfigurationDto GetConfiguration();

        Task<SubscriptionPaymentDto> GetPaymentAsync(StripeGetPaymentInput input);

        Task<string> CreatePaymentSession(StripeCreatePaymentSessionInput input);
    }
}