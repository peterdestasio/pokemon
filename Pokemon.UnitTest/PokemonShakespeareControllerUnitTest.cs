using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pokemon.Business.Interfaces;
using Pokemon.Controllers;
using Pokemon.Model;
using System.Net;
using System.Threading.Tasks;

namespace Pokemon.UnitTest
{
    [TestClass]
    public class PokemonShakespeareControllerUnitTest
    {
        private PokemonShakespeareController _pokemonShakespeareController;
        private Mock<IPokeShakespeareTranslationService> _pokemonShakespeareTranslationServiceMock;
        [TestInitialize]
        public void Initialize()
        {
            _pokemonShakespeareTranslationServiceMock = new Mock<IPokeShakespeareTranslationService>();
            _pokemonShakespeareController = new PokemonShakespeareController(_pokemonShakespeareTranslationServiceMock.Object);
        }


        [TestMethod]
        public async Task Test_GetPokemonWithShakespeareDescriptionAsync_Return200_Translation()
        {
            //act
            string pokemonName = "pikachu";
            string description = "pikachu description";
            string translatedDescription = "skakespeare description";
            _pokemonShakespeareTranslationServiceMock.Setup(x =>
                x.GetPokemonAsync(pokemonName)).ReturnsAsync(description);
            _pokemonShakespeareTranslationServiceMock.Setup(x => x.GetShakespeareTranslationAsync(description))
                .ReturnsAsync(translatedDescription);

            //arrange
            var result = (ObjectResult)await _pokemonShakespeareController.GetPokemonWithShakespeareDescriptionAsync(pokemonName);
            var response = (PokemonResponse)result.Value;

            //assert
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(pokemonName, response.Name);
            Assert.AreEqual(translatedDescription, response.Description);
        }

        [TestMethod]
        public async Task Test_GetPokemonWithShakespeareDescriptionAsync_Return404()
        {
            //arrange
            string pokemonName = "pokemonname";
            _pokemonShakespeareTranslationServiceMock.Setup(x =>
                x.GetPokemonAsync(pokemonName)).ReturnsAsync(default(string));

            //act
            var result = (ObjectResult)await _pokemonShakespeareController.GetPokemonWithShakespeareDescriptionAsync(pokemonName);
            var response = (PokemonResponse)result.Value;

            //assert
            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode);
            Assert.AreEqual(pokemonName, response.Name);
            Assert.IsNull(response.Description);
        }
    }
}
