using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Branches;
using Shared.Abstractions;

namespace Pharmacy.EntityFrameworkCore.EntityConfigurations
{
   public class BranchEntityTypeConfiguration : BaseEntityTypeConfiguration<Branch>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Branch> builder)
        {
            builder.ToTable("Branches");
            OwnsLocalizedText(builder, l => l.Name);
        }
    }
}
