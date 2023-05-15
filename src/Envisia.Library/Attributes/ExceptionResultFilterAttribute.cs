using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Envisia.Library.Attributes
{
    public class ExceptionResultFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionResultFilterAttribute> logger;
        private readonly IHostEnvironment hostingEnvironment;

        public ExceptionResultFilterAttribute(ILogger<ExceptionResultFilterAttribute> logger, IHostEnvironment hostingEnvironment)
        {
            this.logger = logger;
            this.hostingEnvironment = hostingEnvironment;
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);
        }

        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            await HandleException(context);
        }

        public virtual Task HandleException(ExceptionContext context)
        {
            if (context.Exception != null)
            {
                logger.LogError(context.Exception, "Api endpoint threw exception");


                var errors = new
                {
                    message = context.Exception.Message,
                    exception = hostingEnvironment.IsDevelopment() ? context.Exception : null,
                };

                context.Result = new ObjectResult(errors)
                {
                    StatusCode = GetCodeFromException(context.Exception)
                };
            }

            return Task.CompletedTask;
        }

        private int? GetCodeFromException(Exception exception)
        {
            var status = exception switch
            {
                NotFoundException apiEx => 404,
                _ => 500
            };
            return status;
        }
    }
}
