using System;
using System.Collections.Generic;
using System.Text;
using Entity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.EntityConfiguration
{
    public class LotConfiguration : IEntityTypeConfiguration<Lot>
    {
        private readonly UserManager<User> _userManager;

        public LotConfiguration(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async void Configure(EntityTypeBuilder<Lot> builder)
        {
            var user = new User {UserName = "test1", Name = "Name"};
            await _userManager.CreateAsync(new User {UserName = "test1", Name = "Name"}, "1234");


            var user1 = await _userManager.FindByNameAsync("test1");

            builder.HasData(
                
                new Lot
                {
                    Id = new Guid("bc78c053-49b6-410c-bc78-2d54a9991870"),
                    StartDate = new DateTime(2021,8, 5),
                    EndDate = new DateTime(2021, 8, 12),
                    StartingPrice = 25000,
                    MinimalStep = 1000,
                    CurrentCost = 25000,
                    CarId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    Seller = user

                }
            );
        }
    }
    
}
