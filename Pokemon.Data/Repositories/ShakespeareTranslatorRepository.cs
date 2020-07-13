using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Pokemon.Data.Configuration;
using Pokemon.Data.Http;
using Pokemon.Data.Intefaces;
using Pokemon.Data.Model;
using System.Threading.Tasks;

namespace Pokemon.Data.Repositories
{
    public class ShakespeareTranslatorRepository : IShakespeareTranslatorRepository
    {
        private readonly IOptions<TranslationAPIConfig> _config;
        private readonly IHttpClientWrapper _httpClientWrapper;

        public ShakespeareTranslatorRepository(IOptions<TranslationAPIConfig> config, IHttpClientWrapper httpClientWrapper)
        {
            _config = config;
            _httpClientWrapper = httpClientWrapper;
        }

        public async Task<Translation> GetShakespeareTranslationAsync(string text)
        {
            var endpoint = string.Format(_config.Value.EndPoint, text);
            var response = await _httpClientWrapper.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Translation>(content);
            }

            return null;
        }
    }
}
