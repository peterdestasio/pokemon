using System;
using System.Net.Http;

namespace Pokemon.Data.Http
{
    public class HttpClientFactory
    {
        private static HttpClient _httpClient;

        public static HttpClient Create()
        {
            return _httpClient ?? (_httpClient = new HttpClient{ Timeout = TimeSpan.FromSeconds(20) });
        }
    }
}
