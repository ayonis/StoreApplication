using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Store.Configuration
{
    internal class ItemEntityTypeConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder) {

            builder.Property(b => b.Name).IsRequired();
            builder.Property(b => b.Price).IsRequired();
            builder.Property(b => b.Name).HasColumnType("nvarchar(200)");
            builder.Property(b => b.Author).HasColumnType("nvarchar(100)");
            builder.Property(b => b.Image).HasColumnType("nvarchar(200)");
            builder.Property(b => b.Price).HasColumnType("decimal(6,2)");
            builder.Property(b => b.Type).HasColumnType("nvarchar(100)");
            builder.Property(b => b.Description).HasColumnType("nvarchar(500)");
            builder.Ignore(b => b.Orders);
         
            builder.HasOne(b => b.Category).WithMany(c => c.Items).HasForeignKey(b => b.CategoryId);
        }
    }
}
