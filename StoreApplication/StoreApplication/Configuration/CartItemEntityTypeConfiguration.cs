using Store.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Store.Configuration
{
    public class CartItemEntityTypeConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(cartItem => new { cartItem.CartId , cartItem.ItemId });
            builder.HasOne(cartItem => cartItem.Cart).WithMany(cart => cart.CartItems).HasForeignKey(cartIem => cartIem.CartId);
        }
    }
}
