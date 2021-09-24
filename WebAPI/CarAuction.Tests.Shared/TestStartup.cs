using System;
using CarAuction.IntegrationTests.DataAccess;
using CarAuctionWebAPI.Extensions;
using CarAuctionWebAPI.Middleware;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CarAuction.IntegrationTests
{
    public class TestStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TestCarAuctionDbContext>(options =>
            {
                options.UseInMemoryDatabase("CarAuctionTestDb");
                options.
            });

            services.AddServices();
            services.AddRepositories();
            services.AddAuthentication();
            services.ConfigureIdentity();
            services.AddRouting();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseAuthentication();
            //app.UseAuthorization();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
