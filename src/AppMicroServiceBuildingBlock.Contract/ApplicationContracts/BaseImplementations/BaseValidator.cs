using FluentValidation;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;

namespace AppMicroServiceBuildingBlock.Contract.ApplicationContracts.BaseImplementations;

public class BaseValidator<TCommand> : AbstractValidator<TCommand>
{
    private readonly ILogger<TCommand>? _logger = null!;
    private readonly IDataProtector? _dataProtector = null;

    public BaseValidator(IDataProtectionProvider? dataProtectionProvider = null, string? protectorKey = null, ILogger<TCommand>? logger = null)
    {
        if (!string.IsNullOrEmpty(protectorKey) && dataProtectionProvider is not null)
        {
            _dataProtector = dataProtectionProvider.CreateProtector(protectorKey);
        }

        _logger = logger;
    }

    protected IDataProtector DataProtector => _dataProtector ?? throw new Exception("Please inject IDataProtectionProvider into base constructor and pass a protectorKey to create a DataProtector instance.");
    protected ILogger<TCommand> Logger => _logger ?? throw new Exception("Please inject ILogger<TCommand> into base constructor.");
}

