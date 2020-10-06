using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using UL.Calculator.Data;
using UL.Calculator.Services.Mapper;
using UL.Calculator.Validators;

namespace UL.Calculator.Services.Dependencies
{
    public static class ServiceConfigurationManager
    {
        public static void ConfigureServiceLifeTime(IServiceCollection services)
        {
            services.AddTransient<ICalculatorService, CalculatorService>();
            services.AddTransient<IExpressionValidator, ExpressionValidator>();
            services.AddTransient<IExpressionCalculator, ExpressionCalculator>();
            services.AddTransient<IUserService, UserService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddAutoMapper(typeof(CalculatorMapper)); //Registring Mapper
        }

        public static void ConfigurePersistence(IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<CalculatorDBContext>(options =>
                        options.UseSqlServer(config.GetConnectionString("CalculatorDBContext")));
        }

        public static void ConfigureAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            var appSecret = configuration.GetValue<string>("AppSettings:Secret");
            var key = Encoding.ASCII.GetBytes(appSecret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}