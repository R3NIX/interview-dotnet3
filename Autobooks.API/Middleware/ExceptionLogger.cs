using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Autobooks.API.Middleware
{
    public class ExceptionLogger : ExceptionFilterAttribute, IExceptionFilter
    {
        private readonly ILogger _logger;
        private readonly IHostEnvironment _hostEnvironment;

        public ExceptionLogger(ILoggerFactory loggerFactory, IHostEnvironment hostEnvironment)
        {
            _logger = loggerFactory.CreateLogger("ExceptionLogger");
            _hostEnvironment = hostEnvironment;
        }

        /// <summary>
        /// API level custom exception handler
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Exception found in Grocery API: ");

            // If environment is development, include stack trace and error message
            // If environment is not development, display generic error
            if (_hostEnvironment.IsDevelopment())
            {
                context.Result = new JsonResult(
                    new
                    {
                        StatusCode = 500,
                        Message = context.Exception.Message,
                        StackTrace = context.Exception.StackTrace
                    })
                {
                    StatusCode = 500
                };
            }
            else
            {
                context.Result = new JsonResult(
                    new
                    {
                        StatusCode = 500,
                        Message = "An unexpected error has occured! Please contact IT support."
                    })
                {
                    StatusCode = 500
                };
            }
        }
    }
}
