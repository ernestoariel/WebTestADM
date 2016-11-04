using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using NLog;

namespace Food.WebApi.Plumbing
{
    public class GlobalExceptionLogger : ExceptionLogger
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public override Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            var exception = context.Exception;
            _logger.Error(exception);

            string logMessage = string.Empty;
            if (context.ExceptionContext?.Request?.RequestUri != null)
            {
                logMessage +=
                    $"AbsolutePath:{context.ExceptionContext.Request.RequestUri.AbsolutePath}, Uri:{context.ExceptionContext.Request.RequestUri.AbsoluteUri}, Scheme:{context.ExceptionContext.Request.RequestUri.Scheme}" + Environment.NewLine;
            }
            if (context.ExceptionContext?.Request?.Method != null)
            {
                logMessage += $"HttpVerb:{context.ExceptionContext.Request.Method.Method}" + Environment.NewLine;
            }
            if (context.ExceptionContext?.Request?.Headers != null)
            {
                logMessage += "Headers:";
                foreach (var header in context.ExceptionContext.Request.Headers)
                {
                    string values = header.Value.Aggregate("", (current, value) => current + "," + value);
                    logMessage += $"Key:{header.Key}, Value:{values}";
                }
                logMessage += Environment.NewLine;
            }
            if (context.ExceptionContext?.ControllerContext?.Controller != null)
            {
                logMessage += $"Controller:{context.ExceptionContext.ControllerContext.Controller}" + Environment.NewLine;
            }
            if (context.Request?.Content != null)
            {
                var requestContent = context.Request.Content.ReadAsStringAsync().Result;
                logMessage += $"Request:{requestContent}" + Environment.NewLine;
            }
            if (context.ExceptionContext?.Response?.Content != null)
            {
                var responseContent = context.ExceptionContext.Response.Content.ReadAsStringAsync().Result;
                logMessage +=  $"Response:{responseContent}"  + Environment.NewLine;
            }
            _logger.Error(logMessage);
            return base.LogAsync(context, cancellationToken);
        }

    }
}
