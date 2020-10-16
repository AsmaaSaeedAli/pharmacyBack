using Abp.AspNetCore.Localization;
using Abp.Configuration;
using Abp.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Hangfire.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System.Linq;
using System.Threading.Tasks;


namespace Pharmacy.Web.Startup
{
    public class PharmacyRequestCultureProvider : RequestCultureProvider
    {
        public CookieRequestCultureProvider CookieProvider { get; set; }
        public AbpLocalizationHeaderRequestCultureProvider HeaderProvider { get; set; }

        public override async Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            var currentUiCulture = await FindCurrentUiCulture(httpContext); // You need to implement this method to find the UI culture
            var result = new ProviderCultureResult(culture: (StringSegment)"en-US", uiCulture: (StringSegment)currentUiCulture);
            return await Task.FromResult(result);
        }

        private async Task<StringSegment> FindCurrentUiCulture(HttpContext httpContext)
        {
            var abpSession = httpContext.RequestServices.GetRequiredService<IAbpSession>();
            if (abpSession.UserId == null)
                return null;

            var settingManager = httpContext.RequestServices.GetRequiredService<ISettingManager>();
            var culture = await settingManager.GetSettingValueForUserAsync(
                LocalizationSettingNames.DefaultLanguage,
                abpSession.TenantId,
                abpSession.UserId.Value,
                fallbackToDefault: false
            );

            if (!culture.IsNullOrEmpty())
                return culture;

            var result = await GetResultOrNull(httpContext, CookieProvider) ??
                         await GetResultOrNull(httpContext, HeaderProvider);

            if (result == null || !result.Cultures.Any())
                return null;

            //Try to set user's language setting from cookie if available.
            await settingManager.ChangeSettingForUserAsync(abpSession.ToUserIdentifier(), LocalizationSettingNames.DefaultLanguage,
                result.Cultures.First().Value);

            return result.UICultures.First();
        }

        protected virtual async Task<ProviderCultureResult> GetResultOrNull([NotNull] HttpContext httpContext, [CanBeNull] IRequestCultureProvider provider)
        {
            if (provider == null)
                return null;

            return await provider.DetermineProviderCultureResult(httpContext);
        }

	}
}
