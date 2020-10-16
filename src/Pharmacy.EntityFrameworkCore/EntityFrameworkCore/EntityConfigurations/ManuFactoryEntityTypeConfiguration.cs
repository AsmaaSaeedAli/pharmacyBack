using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.ManuFactories;
using Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.EntityFrameworkCore.EntityConfigurations
{
    public class ManuFactoryEntityTypeConfiguration : BaseEntityTypeConfiguration<ManuFactory>
    {
        public override void ConfigureEntity(EntityTypeBuilder<ManuFactory> builder)
        {
            builder.ToTable("ManuFactories");
            OwnsLocalizedText(builder, l => l.Name);
            builder.Property(b => b.ContactEmail).IsRequired();
            builder.Property(b => b.ContactPhone).IsRequired();
        }
    }
}
