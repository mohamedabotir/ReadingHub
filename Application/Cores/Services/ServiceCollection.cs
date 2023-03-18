using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ReadingHub.Persistence;
using ReadingHub.Persistence.Abstract;
using ReadingHub.Persistence.Models;
using ReadingHub.Unit;
using ReadingHub.Unit.Abstracts;
using System.Reflection;
using System.Text;

namespace ReadingHub.Cores.Services
{
    public static class ServiceCollection
    {
         
        public static IServiceCollection AddApplication(this IServiceCollection services,IConfiguration configuration) {


            services.AddDbContext<ApplicationDbContext>(e =>
            {
                e.UseSqlServer(configuration["ConnectionStrings"]).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }).AddIdentity<User, IdentityRole>()
.AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
            services.PlugDIService  ();
            services.AddSwaggerService();
            services.AddAuthenticationService(configuration);
            services.AddControllers(op =>
            {
                op.Filters.Add<ExceptionResponseFilter>();
            });

            return services;
        }
        public static IServiceCollection PlugDIService(this IServiceCollection services) {

            services.AddTransient<ApplicationDbContext>();
            services.AddTransient<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ISharedService, SharedService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

             
            return services;
        }
        public static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                      Array.Empty<string>()
                    }
                }) ;
            });
            return services;
        }
        public static IServiceCollection AddAuthenticationService(this IServiceCollection service,IConfiguration config)
        { 
        

            service.AddAuthentication(op =>
             {
                 op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                 op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

             }).AddJwtBearer(op =>
             {
                 op.RequireHttpsMetadata = false;
                 op.SaveToken = true;
                 op.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["SecretKey"])),
                     ValidateIssuer = false,
                     ValidateAudience = false,
                 };
             });
            service.AddAuthorization();

            return service;
        }
            
    }
}
