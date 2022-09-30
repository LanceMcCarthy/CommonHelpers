using CommonHelpers.Middleware.Interfaces;
using CommonHelpers.Middleware.Services.Models;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Builder;

public static class CommonHelpersApplicationBuilderExtensions
{
    public static IApplicationBuilder UseNugetServerCore(
        this IApplicationBuilder app)
    {
        var options = app.ApplicationServices.GetRequiredService<IOptions<CommonHelperServiceOptions>>().Value;

        var cacheProvider = app.ApplicationServices.GetRequiredService<ICacheProvider>();
        
        return app;
    }
}
