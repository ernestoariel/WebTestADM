using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;


namespace Food.WebApi.Plumbing
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            var genericMessage = "Unexpected error";
            var response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, genericMessage);
            context.Result = new ResponseMessageResult(response);
            return base.HandleAsync(context, cancellationToken);
        }
    }
}
