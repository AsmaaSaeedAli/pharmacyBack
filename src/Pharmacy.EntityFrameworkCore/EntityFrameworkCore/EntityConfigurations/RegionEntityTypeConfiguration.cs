using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Address;
using Shared.Abstractions;

namespace Pharmacy.EntityFrameworkCore.EntityConfigurations
{
   public class RegionEntityTypeConfiguration : BaseEntityTypeConfiguration<Region>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Region> builder)
        {
            builder.ToTable("Regions");
            OwnsLocalizedText(builder, l => l.Name);
        }
    }
}
