using System;
using CarAuction.Tests.Shared.DataAccess;
using CarAuctionWebAPI.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CarAuction.Tests.Shared
{
    public class TestStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TestCarAuctionDbContext>(options =>
            {
                options.UseInMemoryDatabase("CarAuctionTestDb");
            });

            services.AddServices();
            services.AddRepositories();
        }

        public void Configure()
        {

        }
    }
}
