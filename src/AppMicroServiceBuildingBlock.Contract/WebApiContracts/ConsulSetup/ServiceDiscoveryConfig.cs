namespace AppMicroServiceBuildingBlock.Contract.WebApiContracts.ConsulSetup;

public class ServiceDiscoveryConfig
{
    public Uri ServiceDiscoveryAddress {get;set;} = null!;
    public Uri ServiceAddress {get;set;} = null!;
    public Uri ServiceHealthCheckEndpoint {get;set;} = null!;
    public string ServiceName {get;set;} = null!;
    public string ServiceId {get;set;} = null!;
}