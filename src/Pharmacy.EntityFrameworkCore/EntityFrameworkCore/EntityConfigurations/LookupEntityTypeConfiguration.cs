using Pharmacy.Lookups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Abstractions;

namespace Pharmacy.EntityFrameworkCore.EntityConfigurations
{
   public class LookupEntityTypeConfiguration  : BaseEntityTypeConfiguration<Lookup>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Lookup> builder)
        {
            builder.ToTable("Lookups");
            OwnsLocalizedText(builder, l => l.Name);
        }
    }
}
