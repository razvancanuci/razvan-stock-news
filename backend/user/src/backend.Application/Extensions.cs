using backend.Application.Mappers;
using backend.Application.Services;
using backend.Application.Services.Impl;
using backend.Application.Validators;
using backend.DataAccess;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace backend.Application
{
    [ExcludeFromCodeCoverage]
    public static class Extensions
    {
        public static IServiceCollection AddContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddUserContext(configuration);

            return services;
        }
        public static IServiceProvider AddMigrations(this IServiceProvider serviceProvider)
        {
            serviceProvider.AddAutoMigration();

            return serviceProvider;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddRepositories();

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {

            services.AddAutoMapper(typeof(UserMapper));
            services.AddValidatorsFromAssemblyContaining<NewUserModelValidator>();
            services.AddFluentValidationAutoValidation();
            return services;
        }

        public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration["RabbitMQ:ConnectionString"]);
                });
            });
            services.AddMassTransitHostedService();
            return services;
        }
    }
}
