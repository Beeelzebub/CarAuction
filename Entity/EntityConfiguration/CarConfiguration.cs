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
            builder.HasData(
                new Car
                {
                    Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    Year = 2018,
                    Brand = new Brand
                    {
                        Id = new Guid("p4e2z754-49b6-410c-bc78-2d54a9991870"),
                        BrandName = "Audi"
                    },
                    Fuel = Fuel.Petrol,
                    CarBody = CarBody.Sedan,
                    Model = new Model
                    {
                        Id = new Guid("49b6z754-p4e2-410c-bc78-2d54a9991870"),
                        Name = "A6",
                        BrandId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
                    },
                    DriverUnit = DriverUnit.FrontWheelDrive
                }
            );
        }
    }
}
