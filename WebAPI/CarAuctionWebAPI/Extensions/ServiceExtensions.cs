using System;
using System.Text;
using Entity;
using Entity.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositories;
using Services.Authentication;
using Services.Auction;
using CarAuctionWebAPI.Filters;
using Services.Administration;
using Services.Profile;

namespace CarAuctionWebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User,IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 4;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole),
                builder.Services);
            builder.AddEntityFrameworkStores<CarAuctionContext>()
                .AddDefaultTokenProviders();
        }
        public static void ConfigureJwt(this IServiceCollection services, IConfiguration
            configuration)
        {
            var key = "secret123456789secret!!!!!";
            services.AddAuthentication(opt => {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "CarAuctionWebApi",
                        ValidAudience = "https://localhost:5001",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };
                });
        }

        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CarAuctionContext>(options =>
                options.UseLoggerFactory(LoggerFactory.Create(builder =>
                    {
                        builder.AddConsole();
                    }))
                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly("Entity")));
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAuctionService, AuctionService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IAdministrationService, AdministrationService>();
        }

        public static void AddFilters(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilterAttribute<Car>>();
            services.AddScoped<ValidationFilterAttribute<Lot>>();
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme ",
                    Name = "Authorization",
                    In = ParameterLocation.Cookie,
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }
    }
}
