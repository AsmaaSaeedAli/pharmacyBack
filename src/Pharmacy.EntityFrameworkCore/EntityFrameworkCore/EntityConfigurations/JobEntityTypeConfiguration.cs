using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Jobs;
using Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.EntityFrameworkCore.EntityConfigurations
{
   public  class JobEntityTypeConfiguration: BaseEntityTypeConfiguration<Job>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Job> builder)
        {
            builder.ToTable("Jobs");
            OwnsLocalizedText(builder, j => j.Name);
            
        }
    }
}
