using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Pharmacy.Startup
{
    [DependsOn(typeof(PharmacyCoreModule))]
    public class PharmacyGraphQLModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PharmacyGraphQLModule).GetAssembly());
        }

        public override void PreInitialize()
        {
            base.PreInitialize();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }
    }
}