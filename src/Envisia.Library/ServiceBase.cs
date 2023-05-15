namespace Envisia.Library;

public abstract class ServiceBase
{
    protected async Task<ServiceResult<TResult>> ExecuteAsync<TResult>(Func<Task<ServiceResult<TResult>>> func, Func<Task>? handleException = null)
    {
        try
        {
            ServiceResult<TResult> result = await func();

            return result;
        }
        catch (Exception ex)
        {
            if (handleException != null)
            {
                await handleException();
            }

            return new ServiceExceptionResult<TResult>(ex);
        }
    }

    protected async Task<ServiceResult> ExecuteAsync(Func<Task<ServiceResult>> action, Func<Task>? handleException = null)
    {
        try
        {
            ServiceResult result = await action();

            return result;
        }
        catch (Exception ex)
        {
            if (handleException != null)
            {
                await handleException();
            }

            return new ServiceExceptionResult(ex);
        }
    }
}
