using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Pharmacy.Authorization;

namespace Pharmacy
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(
        typeof(PharmacyApplicationSharedModule),
        typeof(PharmacyCoreModule)
        )]
    public class PharmacyApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PharmacyApplicationModule).GetAssembly());
        }
    }
}