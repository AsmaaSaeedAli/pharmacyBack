using Abp.Modules;
using Abp.Reflection.Extensions;

namespace DemoDemo
{
    [DependsOn(typeof(DemoDemoXamarinSharedModule))]
    public class DemoDemoXamarinIosModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DemoDemoXamarinIosModule).GetAssembly());
        }
    }
}