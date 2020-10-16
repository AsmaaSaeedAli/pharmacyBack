using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Address;
using Shared.Abstractions;

namespace Pharmacy.EntityFrameworkCore.EntityConfigurations
{
   public class CountryEntityTypeConfiguration : BaseEntityTypeConfiguration<Country>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countries");
            OwnsLocalizedText(builder, l => l.Name);
            OwnsLocalizedText(builder, l => l.Nationality);
        }
    }
}
