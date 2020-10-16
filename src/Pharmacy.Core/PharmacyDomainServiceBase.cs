using Abp.Domain.Services;

namespace Pharmacy
{
    public abstract class PharmacyDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected PharmacyDomainServiceBase()
        {
            LocalizationSourceName = PharmacyConsts.LocalizationSourceName;
        }
    }
}
