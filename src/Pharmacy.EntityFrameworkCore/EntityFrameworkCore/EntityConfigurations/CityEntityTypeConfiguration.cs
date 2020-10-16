using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Address;
using Shared.Abstractions;

namespace Pharmacy.EntityFrameworkCore.EntityConfigurations
{
   public class CityEntityTypeConfiguration : BaseEntityTypeConfiguration<City>
    {
        public override void ConfigureEntity(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("Cities");
            OwnsLocalizedText(builder, l => l.Name);
        }
    }
}
