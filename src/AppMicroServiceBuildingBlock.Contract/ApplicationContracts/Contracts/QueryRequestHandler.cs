using AppMicroServiceBuildingBlock.Contract.ApplicationContracts.Contracts.Interfaces;

namespace AppMicroServiceBuildingBlock.Contract.ApplicationContracts.Contracts;

public abstract class QueryRequestHandler<TRequestCommand, TResponse>(IDataProtectionProvider dataProtectionProvider, string dataProtectorKey = "")
    : BaseRequestHandler<TResponse>(dataProtectionProvider, dataProtectorKey), IRequestHandler<TRequestCommand, Result<TResponse>>
    where TRequestCommand : IQueryRequest<TResponse>
{
    public abstract Task<Result<TResponse>> Handle(TRequestCommand request, CancellationToken cancellationToken);
}
