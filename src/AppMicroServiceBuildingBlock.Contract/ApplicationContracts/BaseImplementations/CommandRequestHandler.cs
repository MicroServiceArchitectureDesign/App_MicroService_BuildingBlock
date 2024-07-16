using AppMicroServiceBuildingBlock.Contract.ApplicationContracts.CqrsModels;
using AppMicroServiceBuildingBlock.Contract.DomainContracts;
using MediatR;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;

namespace AppMicroServiceBuildingBlock.Contract.ApplicationContracts.BaseImplementations;

public abstract class CommandRequestHandler<TEntity, TCommand> : BaseRequestHandler<TEntity, TCommand>, IRequestHandler<TCommand, CommandResult>
    where TCommand : IRequest<CommandResult>
    where TEntity : AggregateRoot<long>
{
    public abstract Task<CommandResult> Handle(TCommand request, CancellationToken cancellationToken);

    protected CommandRequestHandler(ILogger<CommandRequestHandler<TEntity, TCommand>> logger, IDataProtectionProvider? dataProtectionProvider = null, string? protectorKey = null) : base(logger, dataProtectionProvider, protectorKey)
    {
    }
}

