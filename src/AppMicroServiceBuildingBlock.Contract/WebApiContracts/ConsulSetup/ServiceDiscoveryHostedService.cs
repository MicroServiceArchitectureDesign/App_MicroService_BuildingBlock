using Consul;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace AppMicroServiceBuildingBlock.Contract.WebApiContracts.ConsulSetup;

internal class ServiceDiscoveryHostedService(IConsulClient consulClient, IOptions<ServiceDiscoveryConfig> serviceDiscoveryConfigOption)
    : IHostedService
{
    private ServiceDiscoveryConfig ServiceDiscoveryConfig => serviceDiscoveryConfigOption.Value;
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var registerModel = new AgentServiceRegistration()
        {
            Name = ServiceDiscoveryConfig.ServiceName,
            ID = ServiceDiscoveryConfig.ServiceId,
            Address = ServiceDiscoveryConfig.ServiceAddress.Host,
            Port = ServiceDiscoveryConfig.ServiceAddress.Port,
        };
        await consulClient.Agent.ServiceRegister(registerModel, cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await consulClient.Agent.ServiceDeregister(ServiceDiscoveryConfig.ServiceId, cancellationToken);
    }
}
