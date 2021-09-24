using System;
using System.Linq;
using CarAuction.Tests.Shared.DataAccess.Helpers;
using CarAuctionWebAPI;
using Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CarAuction.IntegrationTests.Services
{
    public class TestStartup<T>: WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<CarAuctionContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<CarAuctionContext>(options =>
                {
                    options.UseInMemoryDatabase("CarAuctionContextTest");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<CarAuctionContext>();

                    db.Database.EnsureCreated();

                    try
                    {
                        TestDataSeed.Seed(db);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            });

            builder.UseTestServer();
        }
    }
}
