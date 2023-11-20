using backend.Application.Consumers;
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
            services.AddSubscrierContext(configuration);

            return services;
        }
        public static IServiceProvider AddMigrations(this IServiceProvider serviceProvider)
        {
            serviceProvider.AddAutoMigration();

            return serviceProvider;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ISubscriberService, SubscriberService>();
            services.AddScoped<IAnswerService, AnswerService>();
            services.AddRepositories();

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {

            services.AddAutoMapper(typeof(SubscriberMapper));
            services.AddAutoMapper(typeof(AnswerMapper));
            services.AddValidatorsFromAssemblyContaining<NewSubscriberModelValidator>();
            services.AddValidatorsFromAssemblyContaining<NewAnswerModelValidator>();
            services.AddFluentValidationAutoValidation();
            return services;
        }

        public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(config =>
            {
                config.AddConsumer<AuthTokenConsumer>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration["RabbitMQ:ConnectionString"]);
                    cfg.ReceiveEndpoint("subscribe-user-queue", c =>
                    {
                        c.ConfigureConsumer<AuthTokenConsumer>(ctx);
                    });
                });
            });
            services.AddMassTransitHostedService();
            return services;
        }
    }
}
