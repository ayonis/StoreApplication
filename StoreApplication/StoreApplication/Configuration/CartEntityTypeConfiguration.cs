using Store.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Store.Configuration
{
    public class CartEntityTypeConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(cart => cart.Id);  
            builder.HasOne(cart => cart.Customer).WithOne(c => c.Cart);
        }
    }
}
