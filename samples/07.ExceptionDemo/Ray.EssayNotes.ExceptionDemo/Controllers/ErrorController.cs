using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Ray.EssayNotes.ExceptionDemo.Exceptions;

namespace Ray.EssayNotes.ExceptionDemo.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        [Route("/error")]
        public IActionResult Index()
        {
            //从http请求中获取Exception异常对象
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            Exception ex = exceptionHandlerPathFeature?.Error;

            var knownException = ex as IKnownException;

            if (knownException != null)//实现了IKnownException接口，即是我们自定义的异常
            {
                knownException = KnownException.Build(knownException);
            }
            else//未实现IKnownException接口，即不是我们自定义的已知异常
            {
                var logger = HttpContext.RequestServices.GetService<ILogger<ErrorController>>();
                logger.LogError(ex, ex?.Message);
                knownException = KnownException.Unknown;
            }

            return View(knownException);
        }
    }
}