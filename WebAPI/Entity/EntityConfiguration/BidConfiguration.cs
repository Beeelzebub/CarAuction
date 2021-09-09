using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.EntityConfiguration
{
    public class BidConfiguration : IEntityTypeConfiguration<Bid>
    {
        public void Configure(EntityTypeBuilder<Bid> builder)
        {
            builder.HasOne(p => p.Buyer).WithMany(x => x.Bids).HasForeignKey(x => x.BuyerId);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.BidStatus).IsRequired();
            
        }
    }
}
