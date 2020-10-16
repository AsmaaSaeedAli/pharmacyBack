using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace DemoDemo
{
    [DependsOn(typeof(DemoDemoClientModule), typeof(AbpAutoMapperModule))]
    public class DemoDemoXamarinSharedModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Localization.IsEnabled = false;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DemoDemoXamarinSharedModule).GetAssembly());
        }
    }
}