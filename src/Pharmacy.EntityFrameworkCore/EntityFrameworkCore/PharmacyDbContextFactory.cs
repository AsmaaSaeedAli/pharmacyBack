using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Pharmacy.Configuration;
using Pharmacy.Web;

namespace Pharmacy.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class PharmacyDbContextFactory : IDesignTimeDbContextFactory<PharmacyDbContext>
    {
        public PharmacyDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<PharmacyDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder(), addUserSecrets: true);

            PharmacyDbContextConfigurer.Configure(builder, configuration.GetConnectionString(PharmacyConsts.ConnectionStringName));

            return new PharmacyDbContext(builder.Options);
        }
    }
}