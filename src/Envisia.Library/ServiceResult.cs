namespace Envisia.Library;

public class ServiceResult<TResult>
{
    public ServiceResult(TResult? result, bool success)
    {
        Result = result;
        Success = success;
    }

    public TResult? Result { get; set; }

    public bool Success { get; set; }
}

public class ServiceSuccessResult<TResult> : ServiceResult<TResult>
{
    public ServiceSuccessResult(TResult result) : base(result, true)
    {
    }
}

public class ServiceFailResult<TResult> : ServiceResult<TResult>
{
    public ServiceFailResult() : base(default, false)
    {
    }
}

public class ServiceExceptionResult<TResult> : ServiceResult<TResult>
{
    public ServiceExceptionResult(Exception exception) : base(default, false)
    {
        Exception = exception;
    }

    public Exception Exception { get; set; }
}


public class ServiceResult
{
    public ServiceResult(bool success)
    {
        Success = success;
    }

    public bool Success { get; set; }
}

public class ServiceSuccessResult : ServiceResult
{
    public ServiceSuccessResult() : base(true)
    {
    }
}

public class ServiceFailResult : ServiceResult
{
    public ServiceFailResult() : base(false)
    {
    }
}

public class ServiceExceptionResult : ServiceResult
{
    public ServiceExceptionResult(Exception exception) : base(false)
    {
        Exception = exception;
    }

    public Exception Exception { get; set; }
}
