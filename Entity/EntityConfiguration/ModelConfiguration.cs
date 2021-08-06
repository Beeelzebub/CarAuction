using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.EntityConfiguration
{
    class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            //builder.Property(x => x.Id).HasDefaultValueSql("LOWER(NEWID())").ValueGeneratedOnAdd();
            //builder.HasMany<Car>().WithOne(t => t.Model).OnDelete(DeleteBehavior.ClientSetNull);
            //builder.HasOne<Brand>().WithMany(p => p.Models).OnDelete(DeleteBehavior.Client);
           
            
            
           // builder.HasMany(x => x.Cars).WithOne(t => t.Model).HasForeignKey(x => x.ModelId);
            builder.HasOne(p => p.Brand).WithMany(t => t.Models).HasForeignKey(x => x.BrandId);

        }
    }
}
