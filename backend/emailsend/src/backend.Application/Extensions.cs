using System.Diagnostics.CodeAnalysis;
using backend.Application.Consumers;
using backend.Application.Mappers;
using backend.Application.Services;
using backend.Application.Services.Impl;
using backend.DataAccess;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Application
{
    [ExcludeFromCodeCoverage]
    public static class Extensions
    {
        public static IServiceCollection AddContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEmailContext(configuration);

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
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITemplateService, TemplateService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IApiService, ApiService>();
            services.AddRepositories();

            return services;
        }

        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(SubscriberMapper));
            return services;
        }

        public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(config =>
            {
                config.AddConsumer<SubscriberAddedConsumer>();
                config.AddConsumer<SubscriberDeletedConsumer>();
                config.AddConsumer<AdminEmailConsumer>();
                config.AddConsumer<ApiEmailConsumer>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration["RabbitMQ:ConnectionString"]);
                    cfg.ReceiveEndpoint("email-subscribeadd-queue", c =>
                    {
                        c.ConfigureConsumer<SubscriberAddedConsumer>(ctx);

                    });
                    cfg.ReceiveEndpoint("email-subscribedelete-queue", c =>
                     {
                         c.ConfigureConsumer<SubscriberDeletedConsumer>(ctx);
                     });
                    cfg.ReceiveEndpoint("email-admin-queue",c =>
                    {
                        c.ConfigureConsumer<AdminEmailConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint("email-api-queue",c =>
                    {
                        c.ConfigureConsumer<ApiEmailConsumer>(ctx);
                    });
                });
            });
            services.AddMassTransitHostedService();
            return services;
        }
    }
}
