using Modules.Auth.Application.Interfaces;
using Modules.Auth.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.Auth;

public static class AuthModuleExtensions
{
    public static IServiceCollection AddAuthModule(this IServiceCollection services, IConfiguration config)
    {
        var provider = config["Auth:Provider"];

        switch (provider)
        {
            case "SqlServer":
                services = Infrastructure.Services.Auth.SqlServer.ServiceCollectionExtensions.AddSqlServerAuth(services, config);
                break;

            case "PostgreSql":
                services = Infrastructure.Services.Auth.PostgreSQL.ServiceCollectionExtensions.AddPostgreSqlAuth(services, config);
                break;

            case "MongoDb":
                services = Infrastructure.Services.Auth.Mongo.ServiceCollectionExtensions.AddMongoDbAuth(services, config);
                break;

            default:
                throw new System.Exception("Invalid Auth Provider configured");
        }

        // Add other Auth services regardless of DB provider (e.g. AuthService)
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}