using Abp.EntityFrameworkCore;
using Abp.Events.Bus.Entities;
using Abp.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.SeedWork;
using System.Linq;
namespace Shared.Abstractions
{
    public abstract class BaseDbContext<TAuditor, TSelf> : AbpDbContext
         where TAuditor : BaseAuditor<TAuditor>
         where TSelf : BaseDbContext<TAuditor, TSelf>
    {
        protected abstract string DbSchema { get; }
        protected virtual bool IncludeAuditorEntity => true;
        public virtual DbSet<TAuditor> Auditors { get; set; }
        protected BaseDbContext(DbContextOptions<TSelf> options) : base(options)
        {
        }
        protected override long? GetAuditUserId()
        {
            var userId = base.GetAuditUserId();
            return userId ?? AbpSession.UserId;
        }
        protected override EntityChangeReport ApplyAbpConcepts()
        {
            var changeReport = new EntityChangeReport();
            var userId = GetAuditUserId();

            foreach (var entry in ChangeTracker.Entries().ToList())
            {
                if (entry.IsOwnedModifiedOrAdded())
                {
                    Entry(entry.Entity).State = EntityState.Modified;
                }
                ApplyAbpConcepts(entry, userId, changeReport);
            }

            return changeReport;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Add your custom mapping configuration files here
            if (IncludeAuditorEntity)
                modelBuilder.Entity<TAuditor>(ConfigureAuditorEntity);
            else
                modelBuilder.Ignore<TAuditor>();
        }
        protected virtual void ConfigureAuditorEntity(EntityTypeBuilder<TAuditor> builder)
        {
            builder.ToTable("Auditor", DbSchema);
            builder.HasKey(auditor => auditor.UserId);
            builder.Property(auditor => auditor.UserId).IsRequired().ValueGeneratedNever();
            builder.Ignore(auditor => auditor.Id);
            builder.Property(auditor => auditor.Name).IsRequired();
        }

        /// <summary>
        /// Access JSON translation properties while querying.
        /// </summary>
        /// <param name="stringValue">JSON translation string</param>
        /// <param name="language">Two-Letter ISO Language Name</param>
        /// <returns></returns>
        [DbFunction(Name = "GetLocalizedText", Schema = "dbo")]
        public static string GetLocalizedText(string stringValue, string language)
        {
            //This code will be executed only for providers that doesn't support DBFunctions like InMemory Provider
            var localizedText = new LocalizedText(stringValue);
            return localizedText[language];
        }

        [DbFunction(Name = "IsConcatenatedStringsIntersect", Schema = "dbo")]
        public static bool IsConcatenatedStringsIntersect(string firstValue, string secondValue, char separator)
        {
            //This code will be executed only for providers that doesn't support DBFunctions like InMemory Provider
            if (!firstValue.IsNullOrWhiteSpace() && !secondValue.IsNullOrWhiteSpace())
            {
                return firstValue.Split(separator).ToList().Intersect(secondValue.Split(separator).ToList()).Any();
            }

            return false;
        }
    }
    public static class EntityEntryExtensions
    {
        public static bool IsOwnedModifiedOrAdded(this EntityEntry entry)
        {
            if (!entry.Metadata.IsOwned() && entry.State == EntityState.Unchanged)
            {
                return entry.References.Any(r =>
                    r.TargetEntry != null && r.TargetEntry.Metadata.IsOwned()
                                          && (r.TargetEntry.State == EntityState.Modified ||
                                              r.TargetEntry.State == EntityState.Added)) || entry.Collections.Any(r => r.IsModified);
            }

            return false;
        }
    }
}
