using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Items;
using Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.EntityFrameworkCore.EntityConfigurations
{
    public class ItemPriceEntityTypeConfiguration : BaseEntityTypeConfiguration<ItemPrice>
    {

        public override void ConfigureEntity(EntityTypeBuilder<ItemPrice> builder)
        {
            builder.ToTable("ItemPrices");
            builder.Property(b => b.Price).IsRequired();
        }

    }
}
