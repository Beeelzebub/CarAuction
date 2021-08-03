using System;
using System.Collections.Generic;
using System.Text;
using Entity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entity
{
    public class CarAuctionContext: IdentityDbContext<User>
    {
        public CarAuctionContext(DbContextOptions<CarAuctionContext> options)
            : base(options)
        {

        }
        public DbSet<Model> Models { get; set; }
        public DbSet<Lot> Lots { get; set; }

        public DbSet<Fuel> Fuels { get; set; }

        public DbSet<DriveUnit> DriveUnits { get; set; }

        public DbSet<CarBody> CarBodies { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Bit> Bits { get; set; }


    }
}
