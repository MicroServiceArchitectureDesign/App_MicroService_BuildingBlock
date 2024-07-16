using AppMicroServiceBuildingBlock.Contract.ApplicationContracts.CqrsModels;
using MediatR;

namespace AppMicroServiceBuildingBlock.Contract.ApplicationContracts.BaseImplementations;

public interface ICommandRequest<TData> : IRequest<CommandResult<TData>> { }
public interface ICommandRequest : IRequest<CommandResult> { }