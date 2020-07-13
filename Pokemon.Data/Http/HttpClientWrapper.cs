using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pokemon.Data.Http
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapper()
        {
            _httpClient = HttpClientFactory.Create();
        }

        public async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, requestUri))
            {
                return await _httpClient.SendAsync(request);
            }
        }
    }
}
