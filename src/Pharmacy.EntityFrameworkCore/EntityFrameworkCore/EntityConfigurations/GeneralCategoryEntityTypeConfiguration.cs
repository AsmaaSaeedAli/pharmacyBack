using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Categories;
using Shared.Abstractions;
namespace Pharmacy.EntityFrameworkCore.EntityConfigurations
{
    public class GeneralCategoryEntityTypeConfiguration : BaseEntityTypeConfiguration<GeneralCategory>
    {
        public override void ConfigureEntity(EntityTypeBuilder<GeneralCategory> builder)
        {
            builder.ToTable("GeneralCategories");
            OwnsLocalizedText(builder, l => l.Name);
        }
    }
}
