using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace AppMicroServiceBuildingBlock.Shared.Extensions.ApplicationBuilderExtensions;

public static class SwaggerServiceExtension
{
    public static IServiceCollection AddSwaggerBuildingBlock(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "",
                Version = "1.0.1",
                Description = "<a href='/'> Back to home page </a>",
            });
        });


        return services;
    }
}