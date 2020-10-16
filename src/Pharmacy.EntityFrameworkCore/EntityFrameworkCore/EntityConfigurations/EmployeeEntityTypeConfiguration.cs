using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Employees;
using Shared.Abstractions;

namespace Pharmacy.EntityFrameworkCore.EntityConfigurations
{
   public class EmployeeEntityTypeConfiguration : BaseEntityTypeConfiguration<Employee>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            OwnsLocalizedText(builder, l => l.FullName);
            builder.Property(b => b.NationalId).IsRequired();
            builder.Property(b => b.GenderId).IsRequired();
            builder.Property(b => b.NationalityId).IsRequired();
            builder.Property(b => b.Email).IsRequired();
        }
    }
}
