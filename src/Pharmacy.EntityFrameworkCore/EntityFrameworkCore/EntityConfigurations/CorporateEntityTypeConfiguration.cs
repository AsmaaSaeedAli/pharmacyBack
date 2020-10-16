using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Corporates;
using Shared.Abstractions;

namespace Pharmacy.EntityFrameworkCore.EntityConfigurations
{
    public class CorporateEntityTypeConfiguration : BaseEntityTypeConfiguration<Corporate>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Corporate> builder)
        {
            builder.ToTable("Corporates");
            OwnsLocalizedText(builder, l => l.Name);
            builder.Property(b => b.ContactEmail).IsRequired();
            builder.Property(b => b.ContactPhone).IsRequired();
        }
    }
}
