using CommonHelpers.Middleware.Services.Models;
using CommonHelpers.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

public static class CommonHelpersServicesExtensions
{
    public static IServiceCollection AddCommonHelpersServices(
        this IServiceCollection services,
        Action<CommonHelperServiceOptions> setupAction = null)
    {
        //services.Configure<CommonHelperServiceOptions>(options =>{});

        services.TryAddSingleton<SampleDataService>();
        services.TryAddSingleton<XkcdApiService>();
        services.TryAddSingleton<BingImageService>();

        services.TryAddSingleton<ComicVineApiService>();
        
        return services;
    }

    public static IServiceCollection ConfigureNugetServer(
        this IServiceCollection services,
        Action<CommonHelperServiceOptions> setupAction)
    {
        services.PostConfigure(setupAction ?? throw new ArgumentNullException(nameof(setupAction)));

        return services;
    }
}