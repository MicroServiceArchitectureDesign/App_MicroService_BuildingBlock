using FluentValidation;

namespace AppMicroServiceBuildingBlock.Contract.ApplicationContracts.Contracts;

public abstract class BaseValidator<TCommand> : AbstractValidator<TCommand>
{

}
