namespace AppMicroServiceBuildingBlock.Contract.ApplicationContracts.Contracts;

public abstract class BaseRequestHandler
{
    private readonly Result _result = new();

    protected virtual Task<Result> OkAsync()
    {
        return Task.FromResult(_result);
    }

    protected virtual Result Ok()
    {
        return _result;
    }

    protected Result Error(string message)
    {
        return ApplicationResultExtensions.Error(message);
    }

    protected Result Error(List<string> messages)
    {
        return ApplicationResultExtensions.Error(messages);
    }

    protected virtual Task<Result> ErrorAsync(List<string> messages)
    {
        return Task.FromResult(ApplicationResultExtensions.Error(messages));
    }

    protected virtual Task<Result> ErrorAsync(string message)
    {
        return Task.FromResult(ApplicationResultExtensions.Error(message));
    }

    protected void AddMessage(string message)
    {
        _result.AddMessage(message);
        //_result.AddMessage(_translatorService[message]);
    }
    protected void AddMessage(List<string> arguments)
    {
        _result.AddMessages(arguments);
        //_result.AddMessage(_translatorService.Translator[message, arguments]);
    }
}

public abstract class BaseRequestHandler<TResponse> : BaseRequestHandler
{
    private readonly IDataProtector? _dataProtector;
    protected BaseRequestHandler(IDataProtectionProvider? dataProtectionProvider = null, string dataProtectorKey = "")
    {
        if (!string.IsNullOrWhiteSpace(dataProtectorKey) && dataProtectionProvider is not null)
        {
            _dataProtector = dataProtectionProvider.CreateProtector(dataProtectorKey);
        }
    }
    protected IDataProtector DataProtector => _dataProtector ?? throw new NotImplementedException("Invalid key for protector.");

    protected Result<TResponse> Ok(TResponse body)
    {
        return new Result<TResponse>(true, body);
    }

    protected virtual Task<Result<TResponse>> OkAsync(TResponse body)
    {
        return Task.FromResult(new Result<TResponse>(true, body));
    }

    protected Result<TResponse> Error(TResponse body, string message)
    {
        return ApplicationResultExtensions.Error(body, message);
    }

    protected Result<TResponse> Error(TResponse body, List<string> messages)
    {
        return ApplicationResultExtensions.Error(body, messages);
    }

    protected virtual Task<Result<TResponse>> ErrorAsync(TResponse body, string message)
    {
        return Task.FromResult(ApplicationResultExtensions.Error(body, message));
    }

    protected virtual Task<Result<TResponse>> ErrorAsync(TResponse body, List<string> messages)
    {
        return Task.FromResult(ApplicationResultExtensions.Error(body, messages));
    }
}
