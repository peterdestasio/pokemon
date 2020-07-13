using Pokemon.Data.Intefaces;
using Microsoft.Extensions.Options;
using Pokemon.Data.Http;
using Pokemon.Data.Configuration;
using System.Threading.Tasks;
using Pokemon.Data.Model;
using Newtonsoft.Json;

namespace Pokemon.Data.Repositories
{
    public class PokeAPIRepository : IPokeAPIRepository
    {
        private readonly IOptions<PokemonAPIConfig> _config;
        private readonly IHttpClientWrapper _httpClientWrapper;

        public PokeAPIRepository(IOptions<PokemonAPIConfig> config, IHttpClientWrapper httpClientWrapper)
        {
            _config = config;
            _httpClientWrapper = httpClientWrapper;
        }

        public async Task<Species> GetSpeciesAsync(string name)
        {
            var endpoint = string.Format(_config.Value.EndPoint, name);
            var response = await _httpClientWrapper.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Species>(content);
            }

            return null;
        }
    }
}
