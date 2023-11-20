using System.Diagnostics.CodeAnalysis;
using backend.Application.CronJob;
using backend.Application.Mappers;
using backend.Application.Services;
using backend.Application.Services.Impl;
using backend.DataAccess;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Application;

[ExcludeFromCodeCoverage]
public static class Extensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMongoClient(configuration).AddRepositories();
        services.AddSingleton<ITrainService, TrainService>();
        services.AddSingleton<IApiService, ApiService>();

        return services;
    }

    public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMassTransit(config =>
        {
            config.UsingRabbitMq((ctx, cfg) => { cfg.Host(configuration["RabbitMQ:ConnectionString"]); });
            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
        });

        services.AddMassTransitHostedService();
        return services;
    }

    public static IServiceCollection AddCronJob(this IServiceCollection services)
    {
        services.AddHostedService<CronJobService>();
        return services;
    }

    public static IServiceCollection AddAutoMappers(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ApiDataMapper));

        return services;
    }
}