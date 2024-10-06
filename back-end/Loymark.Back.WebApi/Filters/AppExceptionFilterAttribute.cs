using Loymark.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Loymark.Back.Api.Filters;

[AttributeUsage(AttributeTargets.All)]
public sealed class AppExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly ILogger<AppExceptionFilterAttribute> logger;

    public AppExceptionFilterAttribute(
        ILogger<AppExceptionFilterAttribute> logger
    )
    {
        this.logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        if (context != null && context.Exception != null)
        {
            HttpStatusCode statusCode;
            string errorMessage = "An unexpected error has occurred";

            switch (context.Exception)
            {
                case AppException:
                case ValidatorException:
                    statusCode = HttpStatusCode.BadRequest;
                    errorMessage = context.Exception.Message;
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            context.HttpContext.Response.StatusCode = (int)statusCode;

            logger.LogError(context.Exception, errorMessage);

            var messageResponse = new
            {
                Message = errorMessage
            };

            context.Result = new ObjectResult(messageResponse);
        }
    }
}
