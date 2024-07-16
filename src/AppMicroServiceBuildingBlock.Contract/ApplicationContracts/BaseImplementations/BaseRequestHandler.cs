using AppMicroServiceBuildingBlock.Contract.ApplicationContracts.CqrsModels;
using AppMicroServiceBuildingBlock.Contract.DomainContracts;
using MediatR;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;

namespace AppMicroServiceBuildingBlock.Contract.ApplicationContracts.BaseImplementations;

public abstract class BaseRequestHandler<TEntity, TCommand>
    where TCommand : IRequest<CommandResult>
    where TEntity : AggregateRoot<long>
{
    private readonly ILogger<CommandRequestHandler<TEntity, TCommand>> _logger;
    private readonly IDataProtector? _dataProtector;

    protected BaseRequestHandler(ILogger<CommandRequestHandler<TEntity, TCommand>> logger,
        IDataProtectionProvider? dataProtectionProvider = null,
        string? protectorKey = null)
    {
        _logger = logger;
        if (!string.IsNullOrEmpty(protectorKey) && dataProtectionProvider is not null)
        {
            _dataProtector = dataProtectionProvider.CreateProtector(protectorKey);
        }
    }

    protected IDataProtector? DataProtector => _dataProtector
                                               ?? throw new Exception(
                                                   "Please inject 'IDataProtectionProvider' into base constructor and pass a 'protectorKey' to creating a 'Data Protector' instance.");
}