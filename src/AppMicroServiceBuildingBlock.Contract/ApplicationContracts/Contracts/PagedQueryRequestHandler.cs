namespace AppMicroServiceBuildingBlock.Contract.ApplicationContracts.Contracts;

public abstract class PagedQueryRequestHandler<TRequestCommand, TResponse>(
    IDataProtectionProvider dataProtectionProvider,
    string dataProtectorKey = "")
    : BaseRequestHandler<PagedQueryResponse<TResponse>>(dataProtectionProvider, dataProtectorKey),
    IRequestHandler<TRequestCommand, Result<PagedQueryResponse<TResponse>>>
        where TRequestCommand : IRequest<Result<PagedQueryResponse<TResponse>>>
{
    public abstract Task<Result<PagedQueryResponse<TResponse>>> Handle(TRequestCommand request, CancellationToken cancellationToken);

}
