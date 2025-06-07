using Microsoft.Extensions.DependencyInjection;
using Modules.Logging.Application.Interfaces;
using Modules.Logging.Infrastrucure.Services;

namespace Modules.Logging.Infrastrucure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLoggingModule(this IServiceCollection services)
    {
        services.AddScoped(typeof(ILoggingService<>), typeof(LoggingService<>));
        return services;
    }
}