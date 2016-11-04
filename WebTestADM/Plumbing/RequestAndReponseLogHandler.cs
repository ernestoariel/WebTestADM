using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLog;

namespace Food.WebApi.Plumbing
{
    public class RequestAndReponseLogHandler : DelegatingHandler
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (Properties.Settings.Default.LogRequestAndResponse)
            {
                var requestContent = request.Content.ReadAsStringAsync().Result;
                _logger.Error("Request:" + requestContent);
            }
            var response = await base.SendAsync(request, cancellationToken);
            if (Properties.Settings.Default.LogRequestAndResponse)
            {
                _logger.Error("Response:" + response.Content.ReadAsStringAsync().Result);
            }
            return response;
        }
    }
}
