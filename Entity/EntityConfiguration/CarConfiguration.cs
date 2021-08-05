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
            builder.HasOne<Lot>().WithOne().OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Car
                {
                    Id = new Guid("67645961-17a7-4316-853c-7ea15838c135"),
                    Year = 2018,
                    BrandId = new Guid("c360b9e4-455c-4f96-ae93-66d5411a2654"),
                    Fuel = Fuel.Petrol,
                    CarBody = CarBody.Sedan,
                    ModelId = new Guid("e3566a95-b1fd-4547-8721-887a9adcf32b"),
                    DriveUnit = DriveUnit.FrontWheelDrive
                }
            );

        }
    }
}
