using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Food.WebApi.Plumbing
{
    public class FoodActionResult<T> : IHttpActionResult
    {
        private HttpRequestMessage _request;
        private T _content;
        private HttpStatusCode _statusCodeCode;

        public FoodActionResult(HttpRequestMessage request, T content, HttpStatusCode statusCode)
        {
            _request = request;
            _content = content;
            _statusCodeCode = statusCode;
        }

        public FoodActionResult(HttpRequestMessage request, T content)
        {
            _request = request;
            _content = content;
            _statusCodeCode = HttpStatusCode.OK;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = _request.CreateResponse<T>(_statusCodeCode, _content);
            return Task.FromResult(response);

        }
    }
}
