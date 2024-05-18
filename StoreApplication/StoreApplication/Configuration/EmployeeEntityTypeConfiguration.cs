using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Store.Configuration
{
    internal class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK_Employee");
            
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Password).HasDefaultValue("0000");
            
        }
    }
}
