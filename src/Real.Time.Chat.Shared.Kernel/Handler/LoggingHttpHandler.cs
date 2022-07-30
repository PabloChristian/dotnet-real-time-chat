using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Real.Time.Chat.Shared.Kernel.Handler
{
    public class LoggingHttpHandler : DelegatingHandler
    {
        private const string DEFAULT_HEADER_NAME_REFERENCE_ID = "reference-id";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string requestBody, responseBody, referenceId;

            var response = await base.SendAsync(request, cancellationToken);

            if (request.Content != null)
                requestBody = await request.Content.ReadAsStringAsync(cancellationToken);

            if (response.Content != null)
                responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

            if (request.Headers.TryGetValues(DEFAULT_HEADER_NAME_REFERENCE_ID, out var values))
                referenceId = values?.FirstOrDefault();

            return response;
        }
    }
}
