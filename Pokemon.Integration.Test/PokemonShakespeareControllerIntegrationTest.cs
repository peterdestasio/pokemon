using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pokemon.Business.Interfaces;
using Pokemon.Controllers;
using Pokemon.Model;
using System.Net;
using System.Threading.Tasks;

namespace Pokemon.Integration.Test
{
    [TestClass]
    public class PokemonShakespeareControllerIntegrationTest : ConfigTest
    {
        private PokemonShakespeareController _pokemonShakespeareController;

        [TestInitialize]
        public void Initialize()
        {
            var services = new ServiceCollection();
            new Startup(base._configurationRoot).ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            _pokemonShakespeareController = new PokemonShakespeareController(serviceProvider.GetService<IPokeShakespeareTranslationService>());
        }

        [TestMethod]
        public async Task GivenPokemonName_WhenGetPokemonWithShakespeareDescription_TheReturns200OK()
        {
            //arrange
            var pokemonName = "pikachu";
            //act
            var response = (await _pokemonShakespeareController.GetPokemonWithShakespeareDescriptionAsync(pokemonName)) as ObjectResult;

            //assert
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(((PokemonResponse)response.Value).Description);
        }

    }
}