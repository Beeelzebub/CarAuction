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
            builder.HasOne(p => p.Car).WithOne().OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                
                new Lot
                {
                    Id = new Guid("4f7f9628-f4a1-41d0-9d04-e228fdc49eb1"),
                    StartDate = new DateTime(2021,8, 5),
                    EndDate = new DateTime(2021, 8, 12),
                    StartingPrice = 25000,
                    MinimalStep = 1000,
                    CurrentCost = 25000,
                    CarId = new Guid("67645961-17a7-4316-853c-7ea15838c135"),
                    SellerId = "12345"
                }
            );
        }
    }
    
}
