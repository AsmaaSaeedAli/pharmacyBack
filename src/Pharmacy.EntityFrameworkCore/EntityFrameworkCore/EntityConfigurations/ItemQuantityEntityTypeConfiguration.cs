using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Items;
using Shared.Abstractions;


namespace Pharmacy.EntityFrameworkCore.EntityConfigurations
{
    public class ItemQuantityEntityTypeConfiguration : BaseEntityTypeConfiguration<ItemQuantity>
    {

        public override void ConfigureEntity(EntityTypeBuilder<ItemQuantity> builder)
        {
            builder.ToTable("ItemQuantities");
            builder.Property(b => b.Quantity).IsRequired();
        }

    }
}
