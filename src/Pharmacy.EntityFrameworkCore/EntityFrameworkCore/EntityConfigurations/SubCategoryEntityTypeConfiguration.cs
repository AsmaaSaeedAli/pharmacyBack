using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.SubCategories;
using Shared.Abstractions;

namespace Pharmacy.EntityFrameworkCore.EntityConfigurations
{
    public class SubCategoryEntityTypeConfiguration: BaseEntityTypeConfiguration<SubCategory>
    {
        public override void ConfigureEntity(EntityTypeBuilder<SubCategory> builder)
        {
            builder.ToTable("SubCategories");
            OwnsLocalizedText(builder, l => l.Name);
        }
    }
}
