using Entity.DbInitializer;
using Entity.EntityConfiguration;
using Entity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Entity
{
    public class CarAuctionContext : IdentityDbContext<User>
    {
        public CarAuctionContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Model> Models { get; set; }
        public DbSet<Lot> Lots { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Bid> Bids { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CarConfiguration());
            builder.ApplyConfiguration(new BrandConfiguration());
            builder.ApplyConfiguration(new ModelConfiguration());
            builder.ApplyConfiguration(new LotConfiguration());
            builder.ApplyConfiguration(new BidConfiguration());
            builder.ApplyConfiguration(new BrandConfiguration());
            builder.ApplyConfiguration(new ModelConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());

            AdminRoleInit.SeedRoles(builder);
            AdminRoleInit.SeedUser(builder);
            AdminRoleInit.SeedUserRoles(builder);
            base.OnModelCreating(builder);
        }
    }

}