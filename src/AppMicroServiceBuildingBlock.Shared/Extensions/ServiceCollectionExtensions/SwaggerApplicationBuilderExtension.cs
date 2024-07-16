using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace AppMicroServiceBuildingBlock.Shared.Extensions.ServiceCollectionExtensions;

public static class SwaggerApplicationBuilderExtension
{
    public static IApplicationBuilder UseSwaggerBuildingBlock(this WebApplication app, string environment)
    {
        if (!app.Environment.IsEnvironment(environment)) return app;
        
        
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "HangfireApplication v1");
            c.DocExpansion(DocExpansion.None);
            c.EnableDeepLinking();
        });

        return app;
    }
}