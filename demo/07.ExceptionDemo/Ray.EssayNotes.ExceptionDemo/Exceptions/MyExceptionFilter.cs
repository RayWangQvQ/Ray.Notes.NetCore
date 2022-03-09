using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ray.EssayNotes.ExceptionDemo.Exceptions
{
    public class MyExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            IKnownException knownException = context.Exception as IKnownException;

            if (knownException != null)
            {
                knownException = KnownException.Build(knownException);
                context.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            }
            else
            {
                var logger = context.HttpContext.RequestServices.GetService<ILogger<MyExceptionFilter>>();
                logger.LogError(context.Exception, context.Exception.Message);

                knownException = KnownException.Unknown;
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            context.Result = new JsonResult(knownException)
            {
                ContentType = "application/json; charset=utf-8"
            };
        }
    }
}
