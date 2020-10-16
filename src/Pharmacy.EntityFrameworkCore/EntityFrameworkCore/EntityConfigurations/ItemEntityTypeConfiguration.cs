using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Items;
using Shared.Abstractions;


namespace Pharmacy.EntityFrameworkCore.EntityConfigurations
{
    public class ItemEntityTypeConfiguration : BaseEntityTypeConfiguration<Item>
    {

        public override void ConfigureEntity(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Items");
            OwnsLocalizedText(builder, l => l.Name);
            OwnsLocalizedText(builder, l => l.Description, false);
            builder.Property(i => i.HasVat).HasDefaultValue(true);
            builder.Property(i => i.Vat).HasDefaultValue(5);
        }

    }
}
