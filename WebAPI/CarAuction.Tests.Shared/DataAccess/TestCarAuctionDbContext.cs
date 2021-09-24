using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CarAuction.IntegrationTests.DataAccess
{
    public class TestCarAuctionDbContext : CarAuctionContext
    {
        public TestCarAuctionDbContext(DbContextOptions<CarAuctionContext> options) 
            : base(options)
        {
        }
        
    }
}
