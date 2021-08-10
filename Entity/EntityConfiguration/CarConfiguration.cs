using System;
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
            builder.HasOne(x => x.Model).WithMany(x => x.Cars).HasForeignKey(x => x.ModelId).OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Year).IsRequired().HasMaxLength(4);
            builder.Property(x => x.Fuel).IsRequired();
            builder.Property(x => x.CarBody).IsRequired();
            builder.Property(x => x.DriveUnit).IsRequired();




             builder.HasData(
                 new Car
                 {
                     Id = 1,
                     Year = 2018,
                     ImageUrl = "https://americamotorsby.ams3.digitaloceanspaces.com/2269/38169871_Image_1.JPG",
                     ModelId = 1,
                     LotId = 1,
                     Fuel = Fuel.Petrol,
                     CarBody = CarBody.Sedan,
                     DriveUnit = DriveUnit.FrontWheelDrive
                 }
             );

        }
    }
}
