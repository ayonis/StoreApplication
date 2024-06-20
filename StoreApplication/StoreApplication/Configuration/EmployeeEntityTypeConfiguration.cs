using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;


namespace Store.Configuration
{
    internal class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK_Employee");
            
            builder.Property(e => e.Name).IsRequired();
			builder.HasOne(e => e.ApplicationUser)
				   .WithOne(u => u.Employee)
				   .HasForeignKey<Employee>(e => e.UserFK)
				   .OnDelete(DeleteBehavior.Cascade);
		}
    }
}
