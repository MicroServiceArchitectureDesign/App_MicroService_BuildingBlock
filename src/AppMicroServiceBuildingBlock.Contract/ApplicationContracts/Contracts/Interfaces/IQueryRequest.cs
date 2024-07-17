namespace AppMicroServiceBuildingBlock.Contract.ApplicationContracts.Contracts.Interfaces;

public interface IQueryRequest<TResponse> : IRequest<Result<TResponse>>
{
}
