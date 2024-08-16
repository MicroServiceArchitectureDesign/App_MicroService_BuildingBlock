using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AppMicroServiceBuildingBlock.Contract.WebApiContracts.ConsulSetup;

public static class ConsulServiceExtension
{
    public static IServiceCollection AddBuildingBlockConsulService(this IServiceCollection services, Action<ServiceDiscoveryConfig> setupAction)
    {
        services.Configure(setupAction);
        services.AddHostedService<ServiceDiscoveryHostedService>();
        var option = new ServiceDiscoveryConfig();
        setupAction.Invoke(option);
        services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(q =>
        {
            q.Address = option.ServiceDiscoveryAddress;
        }));
        return services;
    }
}
