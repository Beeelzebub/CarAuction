﻿using System;
using System.Collections.Generic;
using System.Text;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.EntityConfiguration
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasOne(x => x.Model).WithMany(x => x.Cars).HasForeignKey(x => x.ModelId);
               
            builder.Property(x => x.Year).IsRequired().HasMaxLength(4);
            builder.Property(x => x.Fuel).IsRequired();
            builder.Property(x => x.CarBody).IsRequired();
            builder.Property(x => x.DriveUnit).IsRequired();




             builder.HasData(
                 new Car
                 {
                     Id = new Guid("67645961-17a7-4316-853c-7ea15838c135"),
                     Year = 2018,
                     ImageUrl = "https://americamotorsby.ams3.digitaloceanspaces.com/2269/38169871_Image_1.JPG",
                     ModelId = new Guid("d360b9e4-455c-4f96-ae93-66d5411a2654"),
                     LotId = new Guid("4f7f9628-f4a1-41d0-9d04-e228fdc49eb1"),
                     Fuel = Fuel.Petrol,
                     CarBody = CarBody.Sedan,
                     DriveUnit = DriveUnit.FrontWheelDrive
                 }
             );

        }
    }
}
