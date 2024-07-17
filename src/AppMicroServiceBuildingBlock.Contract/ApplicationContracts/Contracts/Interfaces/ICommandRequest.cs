namespace AppMicroServiceBuildingBlock.Contract.ApplicationContracts.Contracts.Interfaces;

public interface ICommandRequest : IRequest<Result>
{
}

public interface ICommandRequest<T> : IRequest<Result<T>>
{
}
