using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Pharmacy
{
    [DependsOn(typeof(PharmacyCoreSharedModule))]
    public class PharmacyApplicationSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PharmacyApplicationSharedModule).GetAssembly());
        }
    }
}