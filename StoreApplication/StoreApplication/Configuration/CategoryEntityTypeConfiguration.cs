using Store.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Store.Configuration
{
    internal class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new Category { Id = 1, Name = "Programming", Description = "" }, new Category { Id = 2, Name = "Arts", Description = "" }, new Category { Id = 3, Name = "Food", Description = "" }, new Category { Id = 4, Name = "Sceince", Description = "" }, new Category { Id = 5, Name = "Philosophy", Description = "" });


        }
    }
}
