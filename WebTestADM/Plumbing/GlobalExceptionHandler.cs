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
using Castle.MicroKernel.Registration;
using FluentValidation;
using Microsoft.Ajax.Utilities;
using Food.Common;

namespace Food.WebApi.Plumbing
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            var valException = context.Exception as ValidationException;
            if (valException != null)
            {
                var statusCode = HttpStatusCode.BadRequest;
                if (valException.Message == ErrorCode.ENTITY_WAS_NOT_FOUND)
                {
                    statusCode = HttpStatusCode.NotFound;
                }
                context.Result =
                    new ResponseMessageResult(context.Request.CreateResponse(statusCode, valException.ToGeneralError()));
                return base.HandleAsync(context, cancellationToken);
            }
            var genericMessage = "Unexpected error";
            context.Result =
                new ResponseMessageResult(context.Request.CreateResponse(HttpStatusCode.InternalServerError,
                    genericMessage));
            return base.HandleAsync(context, cancellationToken);
        }
    }
}
