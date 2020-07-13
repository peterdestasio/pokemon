using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Pokemon.Data.Configuration;
using Pokemon.Data.Http;
using Pokemon.Data.Model;
using Pokemon.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pokemon.Data.UnitTest
{
    [TestClass]
    public class PokeAPIRepositoryUnitTest
    {

        private PokeAPIRepository _pokeAPIRepository;
        private Mock<IHttpClientWrapper> _httpClientWrapperMock;
        private IOptions<PokemonAPIConfig> _config;

        [TestInitialize]
        public void Initialize()
        {
            _config = Options.Create(new PokemonAPIConfig(){ EndPoint = "http://localhost/pokemon/{0}" });
            _httpClientWrapperMock = new Mock<IHttpClientWrapper>();
            _pokeAPIRepository = new PokeAPIRepository(_config, _httpClientWrapperMock.Object);
        }

        [TestMethod]
        public async Task Test_GetSpeciesAsync_Return_Species()
        {
            //arrange
            var pokemonName = "bulbasaur";
            var flavor = new FlavorTextEntries { FlavorText = "abc", Language = new Language {Name = "someLanguage" }};
            var species = new Species{ FlavorTextEntries = new List<FlavorTextEntries> { flavor } };
            _httpClientWrapperMock.Setup(x => x.GetAsync(It.Is<string>(s => s.Contains(string.Format(_config.Value.EndPoint, pokemonName)))))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(species))
                });

            //act
            var actual = await _pokeAPIRepository.GetSpeciesAsync(pokemonName);

            //assert
            Assert.AreEqual(species.FlavorTextEntries.Count, actual.FlavorTextEntries.Count);
            species.FlavorTextEntries.ToList().ForEach(fe => actual.FlavorTextEntries.Contains(It.Is<FlavorTextEntries>(item => item.FlavorText == fe.FlavorText)));
        }

        [TestMethod]
        public async Task Test_GetSpeciesAsync_Return_Null()
        {
            //arrange
            var pokemonName = "blah";
            _httpClientWrapperMock.Setup(x => x.GetAsync(It.Is<string>(s => s.Contains(string.Format(_config.Value.EndPoint, pokemonName)))))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.NotFound
                });

            //act
            var actual = await _pokeAPIRepository.GetSpeciesAsync(pokemonName);

            //assert
            Assert.IsNull(actual);
        }

    }
}
