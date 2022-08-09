using Microsoft.AspNetCore.Identity;
using ReadingHub.Persistence;
using ReadingHub.Persistence.Abstract;
using ReadingHub.Persistence.Models;
using ReadingHub.Unit;
using System.Reflection;
 namespace ReadingHub.Cores.Services
{
    public static class ServiceCollection
    {
        public static IServiceCollection PlugDIService(this IServiceCollection services) {

            services.AddTransient<ApplicationDbContext>();
            services.AddTransient<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
        public static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen();
            return services;
        }
        
            
    }
}
