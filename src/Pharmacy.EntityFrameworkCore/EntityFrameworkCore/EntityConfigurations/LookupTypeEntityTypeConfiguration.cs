using Pharmacy.Lookups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Abstractions;

namespace Pharmacy.EntityFrameworkCore.EntityConfigurations
{
   public class LookupTypeEntityTypeConfiguration  : BaseEntityTypeConfiguration<LookupType>
    {
        public override void ConfigureEntity(EntityTypeBuilder<LookupType> builder)
        {
            builder.ToTable("LookupTypes");

            //Properties
            builder.Property(l => l.Id).ValueGeneratedNever();
            builder.Property(l => l.IsSystem).IsRequired();

            //Complex properties (Localized Text)
            OwnsLocalizedText(builder, l => l.Name);
        }
    }
}
