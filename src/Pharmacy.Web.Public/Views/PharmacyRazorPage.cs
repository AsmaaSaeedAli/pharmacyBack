using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace Pharmacy.Web.Public.Views
{
    public abstract class PharmacyRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected PharmacyRazorPage()
        {
            LocalizationSourceName = PharmacyConsts.LocalizationSourceName;
        }
    }
}
