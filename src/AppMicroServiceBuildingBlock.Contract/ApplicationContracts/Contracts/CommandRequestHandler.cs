using AppMicroServiceBuildingBlock.Contract.ApplicationContracts.Contracts.Interfaces;

namespace AppMicroServiceBuildingBlock.Contract.ApplicationContracts.Contracts;

public abstract class CommandRequestHandler<TRequestCommand> : BaseRequestHandler, IRequestHandler<TRequestCommand, Result>
    where TRequestCommand : ICommandRequest
{
    public abstract Task<Result> Handle(TRequestCommand request, CancellationToken cancellationToken);
}

public abstract class CommandRequestHandler<TRequestCommand, TResponse> : BaseRequestHandler<TResponse>, IRequestHandler<TRequestCommand, Result<TResponse>>
    where TRequestCommand : ICommandRequest<TResponse>
{
    protected CommandRequestHandler(IDataProtectionProvider? dataProtectionProvider = null, string dataProtectorKey = "") : base(dataProtectionProvider, dataProtectorKey)
    {
    }

    public abstract Task<Result<TResponse>> Handle(TRequestCommand request, CancellationToken cancellationToken = default);
}
