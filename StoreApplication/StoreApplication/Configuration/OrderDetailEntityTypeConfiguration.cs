using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Store.Configuration
{
    internal class OrderDetailEntityTypeConfiguration :IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(od => new { od.OrderId , od.ItemId});
            builder.HasOne(od => od.Item).WithMany(b => b.OrderDetails).HasForeignKey(od => od.ItemId);
            builder.HasOne(od => od.Order).WithMany(o => o.OrderDetails).HasForeignKey(od => od.OrderId);
    
        }
    }
}
