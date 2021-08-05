using System;
using System.Collections.Generic;
using System.Text;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.Configurations
{
    class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasMany(p => p.Models).WithOne(t => t.Brand).OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<Car>().WithOne(p => p.Brand).OnDelete(DeleteBehavior.SetNull);


        }
    }
}
