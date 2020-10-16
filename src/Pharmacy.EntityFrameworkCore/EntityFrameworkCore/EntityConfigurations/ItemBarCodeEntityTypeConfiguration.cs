using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Items;
using Shared.Abstractions;


namespace Pharmacy.EntityFrameworkCore.EntityConfigurations
{
    public class ItemBarCodeEntityTypeConfiguration : BaseEntityTypeConfiguration<ItemBarCode>
    {

        public override void ConfigureEntity(EntityTypeBuilder<ItemBarCode> builder)
        {
            builder.ToTable("ItemBarCodes");
            builder.Property(b => b.BarCode).IsRequired();

        }

    }
}
