using Abp.AspNetCore.Mvc.ViewComponents;

namespace Pharmacy.Web.Public.Views
{
    public abstract class PharmacyViewComponent : AbpViewComponent
    {
        protected PharmacyViewComponent()
        {
            LocalizationSourceName = PharmacyConsts.LocalizationSourceName;
        }
    }
}