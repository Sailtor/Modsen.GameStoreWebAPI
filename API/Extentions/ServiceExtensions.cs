using BLL.Services.Contracts;
using BLL.Services.Implementation;
using DAL.Data;
using DAL.Repository.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Extentions
{
    public static class ServiceExtensions
    {
        public static void ConfigureMsSqlServerContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GameStoreDBContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("MS SQL Database"));
            });
        }
        public static void ConfigureEntityServices(this IServiceCollection services)
        {
            //Inject new services here
            services.AddScoped<IPlatformService, PlatformService>();
        }
        public static void ConfigureUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
        public static void ConfigureTokenService(this IServiceCollection services)
        {
            services.AddTransient<ITokenService, TokenService>();
        }
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(configuration.GetSection("Jwt Settings:Key").Value)),
                    };
                });
        }
        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization();
        }
        public static void ConfigureAutomapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}