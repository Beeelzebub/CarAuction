using System;
using System.Collections.Generic;
using System.Text;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.EntityConfiguration
{
    class UserConfiguration:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(p => p.Lots).WithOne(x => x.Seller).HasForeignKey(x => x.Id);
            builder.HasMany(p => p.Bids).WithOne(x => x.Buyer).HasForeignKey(x => x.Id);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            
            
        }
    }
}
