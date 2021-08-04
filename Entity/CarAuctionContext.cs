using Entity.Configurations;
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
        public DbSet<Car> Cars { get; set; }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Bid> Bits { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
        }
    }
    
}
