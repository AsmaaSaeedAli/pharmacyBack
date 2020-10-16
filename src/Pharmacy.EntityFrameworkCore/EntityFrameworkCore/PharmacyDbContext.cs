using Abp.IdentityServer4;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Address;
using Pharmacy.Authorization.Roles;
using Pharmacy.Authorization.Users;
using Pharmacy.Branches;
using Pharmacy.Chat;
using Pharmacy.Editions;
using Pharmacy.Friendships;
using Pharmacy.MultiTenancy;
using Pharmacy.MultiTenancy.Payments;
using Pharmacy.Storage;
using Pharmacy.Lookups;
using Pharmacy.EntityFrameworkCore.EntityConfigurations;
using Pharmacy.Employees;
using Pharmacy.Corporates;
using Pharmacy.Customers;
using Pharmacy.Jobs;
using Pharmacy.Categories;
using Pharmacy.Invoices;
using Pharmacy.SubCategories;
using Pharmacy.Items;
using Pharmacy.ItemClasses;
using Invoice = Pharmacy.MultiTenancy.Accounting.Invoice;
using Pharmacy.ManuFactories;

namespace Pharmacy.EntityFrameworkCore
{
    public class PharmacyDbContext : AbpZeroDbContext<Tenant, Role, User, PharmacyDbContext>, IAbpPersistedGrantDbContext
    {

        public virtual DbSet<Corporate> Corporates { get; set; }
        public virtual DbSet<ManuFactory> ManuFactories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Lookup> Lookups { get; set; }
        public virtual DbSet<LookupType> LookupTypes { get; set; }
        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public virtual DbSet<Invoice> AppInvoices { get; set; }

        public virtual DbSet<PersistedGrantEntity> PersistedGrants { get; set; }

        public virtual DbSet<SubscriptionPaymentExtensionData> SubscriptionPaymentExtensionDatas { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<ItemClass> ItemClasses { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemBarCode> ItemBarCodes { get; set; }
        public virtual DbSet<ItemPrice> ItemPrices { get; set; }
        public virtual DbSet<ItemQuantity> ItemQuantities { get; set; }
        public virtual DbSet<Invoices.Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceItem> InvoiceItems{ get; set; }
        public virtual DbSet<GeneralCategory> GeneralCategories{ get; set; }

        public PharmacyDbContext(DbContextOptions<PharmacyDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BinaryObject>(b =>
            {
                b.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.FriendUserId });
                b.HasIndex(e => new { e.FriendTenantId, e.UserId });
                b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { PaymentId = e.ExternalPaymentId, e.Gateway });
            });

            modelBuilder.Entity<SubscriptionPaymentExtensionData>(b =>
            {
                b.HasQueryFilter(m => !m.IsDeleted)
                    .HasIndex(e => new { e.SubscriptionPaymentId, e.Key, e.IsDeleted })
                    .IsUnique();
            });

            modelBuilder.ConfigurePersistedGrantEntity();
            modelBuilder.ApplyConfiguration(new LookupTypeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LookupEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CountryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RegionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CityEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BranchEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CorporateEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new JobEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SubCategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ItemClassEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ItemBarCodeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ItemPriceEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ItemQuantityEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ManuFactoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GeneralCategoryEntityTypeConfiguration());
        }
    }
}
