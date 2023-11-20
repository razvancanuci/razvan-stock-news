using backend.DataAccess.Entities;
using backend.DataAccess.Repository;
using backend.DataAccess.Repository.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace backend.DataAccess
{
    public static class Extensions
    {
        public static IServiceCollection AddEmailContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EmailContext>(options =>
            {
                options.UseNpgsql(configuration["Database:ConnectionString"]);
            });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Subscriber>, Repository<Subscriber>>();
            return services;
        }

        public static IServiceProvider AddAutoMigration(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dataContext = scope.ServiceProvider.GetRequiredService<EmailContext>();
                if (dataContext.Database.IsRelational())
                {
                    dataContext.Database.Migrate();
                }
            }

            return serviceProvider;
        }
    }
}
