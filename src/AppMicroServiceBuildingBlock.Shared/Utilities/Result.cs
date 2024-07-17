namespace AppMicroServiceBuildingBlock.Shared.Utilities;

public class Result<T> : Result
{
    protected readonly T? _body;
    public Result()
    {
    }

    public Result(bool succeded, T? body)
        : base(succeded)
    {
        _body = body;
    }
    public Result(bool succeded, T? body, List<ResultMessage> messages)
        : base(succeded, messages)
    {
        _body = body;
    }

    public T? Body => _body;
}

public static class ApplicationResultExtensions
{
    public static Result<T> Success<T>(T body = default!)
    {
        return new Result<T>(true, body);
    }

    public static Result<T> Success<T>(T body, List<ResultMessage> messages)
    {
        return new Result<T>(
            !messages.Any(p => p.Type == ResultMessageType.Error),
            body,
            messages);

    }

    public static Result<object> Error(ResultMessage message)
    {
        return new Result<object>(false, new { }, [message]);
    }

    public static Result Error(string message)
    {
        return new Result(false, [ResultMessage.Error(message)]);
    }

    public static Result Error(List<string> messages)
    {
        return new Result(false, messages.Select(ResultMessage.Error).ToList());
    }

    public static Result<object> Error(List<ResultMessage> messages)
    {
        return new Result<object>(false, new { }, messages);
    }

    public static Result<T> Error<T>(T body, List<ResultMessage> messages)
    {
        return new Result<T>(false, body, messages);
    }

    public static Result<T> Error<T>(T body, List<string> messages)
    {
        return new Result<T>(false, body, messages.Select(ResultMessage.Error).ToList());
    }

    public static Result<T> Error<T>(T body, string message)
    {
        return new Result<T>(false, body, [ResultMessage.Error(message)]);
    }
}

public class Result
{
    public Result()
    {
    }
    public Result(bool succeded)
    {
        _succeded = succeded;
    }
    public Result(bool succeded, List<ResultMessage> messages)
    {
        _succeded = succeded;
        _messages = messages;
    }

    protected readonly List<ResultMessage> _messages = [];
    protected readonly bool _succeded = true;



    public bool Succeded => _succeded;
    public IEnumerable<ResultMessage> Messages => _messages;
    public void AddMessage(string error) => _messages.Add(ResultMessage.Error(error));
    public void AddMessages(IEnumerable<string> errors) => _messages.AddRange(ResultMessage.Error(errors.ToArray()));
    public void AddMessages(IEnumerable<ResultMessage> errors) => _messages.AddRange(errors);
    public void ClearMessages() => _messages.Clear();


    public override string ToString()
    {
        return JsonSerializerUtilities.Serialize(this);
    }
}


public class ResultMessage
{
    private ResultMessage(ResultMessageType type, string message)
    {
        Type = type;
        Message = message;
    }

    public ResultMessageType Type { get; set; }
    public string Message { get; set; }

    public static ResultMessage Warrning(string message) => new(ResultMessageType.Warning, message);

    public static ResultMessage Error(string message) => new(ResultMessageType.Error, message);
    public static List<ResultMessage> Error(params string[] messages) => messages.Select(p => new ResultMessage(ResultMessageType.Error, p)).ToList();

    public static ResultMessage Success(string message) => new(ResultMessageType.Success, message);
    public static List<ResultMessage> Success(params string[] messages) => messages.Select(p => new ResultMessage(ResultMessageType.Success, p)).ToList();

    public static ResultMessage Info(string message) => new(ResultMessageType.Info, message);
}

public enum ResultMessageType
{
    Error = 1,
    Success = 2,
    Warning = 3,
    Info = 4,
}