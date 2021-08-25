using CarAuctionWebAPI.BackgroundTask;
using CarAuctionWebAPI.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Contracts;
using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories;

namespace CarAuctionWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddAuthentication();
            services.ConfigureIdentity();
            services.ConfigureJwt(Configuration);
            services.AddDbContext(Configuration);
            services.AddHostedService<DerivedBackgroundPrinter>();
            services.AddScoped<ICarRepository, CarRepository >();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
            services.AddScoped<IWorker, Worker>();
            services.AddControllers();

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllers();
            });
        }
    }
}
