using System;
using System.Collections.Generic;
using System.Text;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.Configurations
{
    class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            //builder.HasMany<Car>().WithOne(t => t.Model).OnDelete(DeleteBehavior.ClientSetNull);
            //builder.HasOne<Brand>().WithMany(p => p.Models).OnDelete(DeleteBehavior.Client);

            builder.HasMany<Car>().WithOne(t => t.Model).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(p => p.Brand).WithMany(t => t.Models).OnDelete(DeleteBehavior.Cascade);

            builder.HasData(new Model
            {
                Id = new Guid("e3566a95-b1fd-4547-8721-887a9adcf32b"),
                Name = "A6",
                BrandId = new Guid("c360b9e4-455c-4f96-ae93-66d5411a2654")
            });
        }
    }
}
