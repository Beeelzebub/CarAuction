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
            builder.HasOne(x => x.Car).WithMany(x => x.Lots).HasForeignKey(x => x.CarId);
            builder.HasOne(x => x.Seller).WithMany(x => x.Lots).HasForeignKey(x => x.SellerId);
            builder.HasMany(x => x.Bids).WithOne(x => x.Lot).HasForeignKey(x => x.Id);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
            builder.Property(x => x.CurrentCost).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(x => x.MinimalStep).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(x => x.StartingPrice).HasColumnType("decimal(10,2)").IsRequired();





             builder.HasData(
                 
                 new Lot
                 {
                     Id = new Guid("4f7f9628-f4a1-41d0-9d04-e228fdc49eb1"),
                     StartDate = new DateTime(2021,8, 6),
                     EndDate = new DateTime(2021, 8, 13),
                     StartingPrice = 25000,
                     MinimalStep = 1000,
                     CurrentCost = 25000,
                     CarId = new Guid("67645961-17a7-4316-853c-7ea15838c135")
                 }
             );
        }
    }
    
}
