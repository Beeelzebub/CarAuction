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
            builder.HasOne(p => p.Lot).WithMany(t => t.Bids).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Buyer).WithMany().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
