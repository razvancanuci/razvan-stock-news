using backend.DataAccess.Repositories;
using backend.DataAccess.Repositories.Impl;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace backend.DataAccess;

public static class Extensions
{
    public static IServiceCollection AddMongoClient(this IServiceCollection services, IConfiguration configuration)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        services.AddSingleton<IMongoClient>(new MongoClient(configuration["Database:ConnectionString"]));
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IMongoRepository, MongoRepository>();
        return services;
    }
    
}