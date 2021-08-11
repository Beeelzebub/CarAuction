using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.EntityConfiguration
{
    class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasMany(x => x.Models).WithOne(x => x.Brand).HasForeignKey(x => x.BrandId).OnDelete(DeleteBehavior.Cascade);
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.BrandName).IsRequired().HasMaxLength(50);
            

            builder.HasData(new Brand
            {
                    Id = 1,
                    BrandName = "Audi"
            });
        }
    }
}
