
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        /// <summary>
        /// Extends HTTP GET request with query string parameters.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="uri"></param>
        /// <param name="queryStringParams"></param>
        /// <returns></returns>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="uri" /> is <see langword="null" />.</exception>
        /// <exception cref="T:System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.</exception>
        public static async Task<HttpResponseMessage> GetWithQueryStringAsync(this HttpClient client, string uri,
            Dictionary<string, string> queryStringParams)
        {
            var url = QueryHelpers.AddQueryString(uri, queryStringParams);
            return await client.GetAsync(url);
        }
    }
}
