using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entity.EntityConfiguration
{
    class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.HasOne(x => x.Brand).WithMany(x => x.Models).HasForeignKey(x => x.BrandId);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

            builder.HasData(new Model
            {
                Id = new Guid("d360b9e4-455c-4f96-ae93-66d5411a2654"),
                Name = "A6",
                BrandId = new Guid("c360b9e4-455c-4f96-ae93-66d5411a2654")
            });

        }
    }
}
