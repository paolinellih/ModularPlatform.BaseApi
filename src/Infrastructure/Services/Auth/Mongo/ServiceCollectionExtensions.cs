using Modules.Auth.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Infrastructure.Services.Auth.Mongo;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongoDbAuth(this IServiceCollection services, IConfiguration config)
    {
        var mongoSettings = config.GetSection("MongoDbSettings");
        var connectionString = mongoSettings.GetValue<string>("ConnectionString");
        var databaseName = mongoSettings.GetValue<string>("DatabaseName");

        // Register Mongo client as singleton
        services.AddSingleton<IMongoClient>(sp => new MongoClient(connectionString));

        // Register a Mongo database instance
        services.AddScoped(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(databaseName);
        });

        // You need to implement your own IUserStore and RoleStore for MongoDB,
        // or use a community MongoDB Identity implementation.
        // For now, just register an interface for your custom Mongo Auth repository here:
        services.AddScoped<IAuthRepository, MongoAuthRepository>();

        return services;
    }
}