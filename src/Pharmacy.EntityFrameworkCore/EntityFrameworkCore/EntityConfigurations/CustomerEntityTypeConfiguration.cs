using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Customers;
using Shared.Abstractions;
namespace Pharmacy.EntityFrameworkCore.EntityConfigurations
{
    public class CustomerEntityTypeConfiguration : BaseEntityTypeConfiguration<Customer>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            OwnsLocalizedText(builder, l => l.FullName);
            builder.Property(c => c.GenderId).IsRequired();
            builder.Property(c => c.PrimaryMobileNumber).IsRequired();
        }
    }
}
