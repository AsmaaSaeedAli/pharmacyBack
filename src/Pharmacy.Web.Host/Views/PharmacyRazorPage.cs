using Abp.AspNetCore.Mvc.Views;

namespace Pharmacy.Web.Views
{
    public abstract class PharmacyRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected PharmacyRazorPage()
        {
            LocalizationSourceName = PharmacyConsts.LocalizationSourceName;
        }
    }
}
