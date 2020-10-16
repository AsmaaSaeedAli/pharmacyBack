using Abp;

namespace Pharmacy
{
    /// <summary>
    /// This class can be used as a base class for services in this application.
    /// It has some useful objects property-injected and has some basic methods most of services may need to.
    /// It's suitable for non domain nor application service classes.
    /// For domain services inherit <see cref="PharmacyDomainServiceBase"/>.
    /// For application services inherit PharmacyAppServiceBase.
    /// </summary>
    public abstract class PharmacyServiceBase : AbpServiceBase
    {
        protected PharmacyServiceBase()
        {
            LocalizationSourceName = PharmacyConsts.LocalizationSourceName;
        }
    }
}