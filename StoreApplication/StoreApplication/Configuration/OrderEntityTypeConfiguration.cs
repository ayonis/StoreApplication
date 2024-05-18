using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Store.Configuration
{
    internal class OrderEntityTypeConfiguration: IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder) 
        {
            builder.Property(o => o.Id).ValueGeneratedOnAdd();
            builder.Property(o => o.DateOfOrder).HasDefaultValueSql("GETDATE()");
            builder.Ignore(o => o.Items);
            builder.Property(o => o.CustomerId);
        }
    }
}
