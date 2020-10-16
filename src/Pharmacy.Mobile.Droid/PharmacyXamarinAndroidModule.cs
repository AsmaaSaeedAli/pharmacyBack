using Abp.Modules;
using Abp.Reflection.Extensions;

namespace DemoDemo
{
    [DependsOn(typeof(DemoDemoXamarinSharedModule))]
    public class DemoDemoXamarinAndroidModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DemoDemoXamarinAndroidModule).GetAssembly());
        }
    }
}