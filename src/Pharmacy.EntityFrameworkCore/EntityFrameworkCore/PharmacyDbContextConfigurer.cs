using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Pharmacy.EntityFrameworkCore
{
    public static class PharmacyDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<PharmacyDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<PharmacyDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}