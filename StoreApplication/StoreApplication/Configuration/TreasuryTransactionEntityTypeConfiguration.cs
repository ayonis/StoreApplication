using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApplication.Models;

namespace StoreApplication.Configuration
{
    public class TreasuryTransactionEntityTypeConfiguration : IEntityTypeConfiguration<TreasuryTransaction>
    {
        public void Configure(EntityTypeBuilder<TreasuryTransaction> builder)
        {
            builder.Property(t => t.Date).IsRequired();
            builder.Property(t => t.Description).HasColumnType("nvarchar(255)");
            builder.Property(t => t.Type).IsRequired();
        }
    }
}
