using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Pharmacy
{
    public class PharmacyCoreSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PharmacyCoreSharedModule).GetAssembly());
        }
    }
}