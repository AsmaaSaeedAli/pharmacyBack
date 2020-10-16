using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.ItemClasses;
using Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.EntityFrameworkCore.EntityConfigurations
{
    public class ItemClassEntityTypeConfiguration: BaseEntityTypeConfiguration<ItemClass>
    {
        public override void ConfigureEntity(EntityTypeBuilder<ItemClass> builder)
        {
            builder.ToTable("ItemClasses");
            OwnsLocalizedText(builder, l => l.Name);
        }
    }
}
