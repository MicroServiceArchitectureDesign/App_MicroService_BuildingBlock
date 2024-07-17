namespace AppMicroServiceBuildingBlock.Contract.ApplicationContracts.Contracts.Interfaces;

public interface IPagedQueryRequest<TResponse> : IRequest<Result<TResponse>>
{
}
