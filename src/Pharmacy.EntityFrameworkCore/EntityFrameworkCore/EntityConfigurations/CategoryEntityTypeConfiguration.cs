using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Categories;
using Shared.Abstractions;
namespace Pharmacy.EntityFrameworkCore.EntityConfigurations
{
    public class CategoryEntityTypeConfiguration : BaseEntityTypeConfiguration<Category>
    {

        public override void ConfigureEntity(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            OwnsLocalizedText(builder, l => l.Name);
            OwnsLocalizedText(builder, l => l.Description, false);
        }

    }
}
