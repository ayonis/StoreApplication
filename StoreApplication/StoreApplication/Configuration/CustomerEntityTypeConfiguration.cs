using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Configuration
{
    internal class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Name).IsRequired();
            
            builder.Property(c => c.Name).HasColumnType("nvarchar(100)");
            builder.Property(c => c.Address).HasColumnType("nvarchar(200)");
            
            builder.HasMany(c => c.Orders).WithOne(o => o.Customer).HasForeignKey(o => o.CustomerId).OnDelete(DeleteBehavior.NoAction); ;
        }
    }
}
