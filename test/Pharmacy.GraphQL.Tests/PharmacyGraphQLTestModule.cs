using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Pharmacy.Configure;
using Pharmacy.Startup;
using Pharmacy.Test.Base;

namespace Pharmacy.GraphQL.Tests
{
    [DependsOn(
        typeof(PharmacyGraphQLModule),
        typeof(PharmacyTestBaseModule))]
    public class PharmacyGraphQLTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            IServiceCollection services = new ServiceCollection();
            
            services.AddAndConfigureGraphQL();

            WindsorRegistrationHelper.CreateServiceProvider(IocManager.IocContainer, services);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PharmacyGraphQLTestModule).GetAssembly());
        }
    }
}