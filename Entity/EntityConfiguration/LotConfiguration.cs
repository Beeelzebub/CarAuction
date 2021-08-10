using System;
using System.Collections.Generic;
using System.Text;
using Entity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.EntityConfiguration
{
    public class LotConfiguration : IEntityTypeConfiguration<Lot>
    {
        public void Configure(EntityTypeBuilder<Lot> builder)
        {
            builder.HasOne(b => b.Car).WithOne(i => i.Lot).HasForeignKey<Car>(b => b.LotId).OnDelete(DeleteBehavior.Cascade);
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Seller).WithMany(x => x.Lots).HasForeignKey(x => x.SellerId);
            builder.HasMany(x => x.Bids).WithOne(x => x.Lot).HasForeignKey(x => x.Id);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
            builder.Property(x => x.CurrentCost).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(x => x.MinimalStep).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(x => x.StartingPrice).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(x => x.RedemptionPrice).HasColumnType("decimal(10,2)").IsRequired();





            builder.HasData(
                 
                 new Lot
                 {
                     Id = 1,
                     StartingPrice = 25000,
                     MinimalStep = 1000,
                     CurrentCost = 25000,
                     RedemptionPrice = 100000
                 }
             );
        }
    }
    
}
