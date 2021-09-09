using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.EntityConfiguration
{
    public class UserConfiguration:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(p => p.Lots).WithOne(x => x.Seller).HasForeignKey(x => x.SellerId);
            builder.HasMany(p => p.Bids).WithOne(x => x.Buyer).HasForeignKey(x => x.BuyerId);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.UserName).IsRequired();

        }
    }
}
