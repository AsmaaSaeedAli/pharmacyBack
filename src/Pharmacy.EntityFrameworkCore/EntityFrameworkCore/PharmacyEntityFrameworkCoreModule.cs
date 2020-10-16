using Abp;
using Abp.Dependency;
using Abp.EntityFrameworkCore.Configuration;
using Abp.IdentityServer4;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using Pharmacy.Configuration;
using Pharmacy.EntityHistory;
using Pharmacy.Migrations.Seed;

namespace Pharmacy.EntityFrameworkCore
{
    [DependsOn(
        typeof(AbpZeroCoreEntityFrameworkCoreModule),
        typeof(PharmacyCoreModule),
        typeof(AbpZeroCoreIdentityServerEntityFrameworkCoreModule)
        )]
    public class PharmacyEntityFrameworkCoreModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<PharmacyDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        PharmacyDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        PharmacyDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }

            // Set this setting to true for enabling entity history.
            Configuration.EntityHistory.IsEnabled = true;

            // Uncomment below line to write change logs for the entities below:
            Configuration.EntityHistory.Selectors.Add("PharmacyEntities", EntityHistoryHelper.TrackedTypes);
            Configuration.CustomConfigProviders.Add(new EntityHistoryConfigProvider(Configuration));
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PharmacyEntityFrameworkCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            var configurationAccessor = IocManager.Resolve<IAppConfigurationAccessor>();

            using (var scope = IocManager.CreateScope())
            {
                if (!SkipDbSeed && scope.Resolve<DatabaseCheckHelper>().Exist(configurationAccessor.Configuration["ConnectionStrings:Default"]))
                {
                    SeedHelper.SeedHostDb(IocManager);
                }
            }
        }
    }
}
