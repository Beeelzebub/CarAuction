using System;
using System.Collections.Generic;
using System.Text;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.EntityConfiguration
{
    class BidConfiguration : IEntityTypeConfiguration<Bid>
    {
        public void Configure(EntityTypeBuilder<Bid> builder)
        {
            builder.HasOne(p => p.Lot).WithMany(t => t.Bids).HasForeignKey(x => x.LotId);
            builder.HasOne(p => p.Buyer).WithMany(x => x.Bids).HasForeignKey(x => x.Id);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.BidStatus).IsRequired();
        }
    }
}
