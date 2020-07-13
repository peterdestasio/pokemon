using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pokemon.Business.Interfaces;
using Pokemon.Business.Services;
using Pokemon.Data.Intefaces;
using Pokemon.Data.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemon.Business.Test
{
    [TestClass]
    public class PokemonShakespeareTranslationServiceUnitTest
    {
        private PokeShakespeareTranslationService _pokeShakespeareTranslationService;
        private Mock<IPokeAPIRepository> _pokeAPIRepositoryMock;
        private Mock<IShakespeareTranslatorRepository> _shakespeareTranslatorRepositoryMock;
        private Mock<IEscapeService> _escapeServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            _pokeAPIRepositoryMock = new Mock<IPokeAPIRepository>();
            _shakespeareTranslatorRepositoryMock = new Mock<IShakespeareTranslatorRepository>();
            _escapeServiceMock = new Mock<IEscapeService>();
            _pokeShakespeareTranslationService = new PokeShakespeareTranslationService(_pokeAPIRepositoryMock.Object, _shakespeareTranslatorRepositoryMock.Object, _escapeServiceMock.Object);
        }


        [TestMethod]
        public async Task Test_GetSpeciesAsync_Language_DoNot_Exist_Return_Null()
        {
            //arrange
            string pokemonName = "bulbasaur";
            string language = "fr";
            var flavor = new FlavorTextEntries { FlavorText = "abc", Language = new Language { Name = language } };
            var species = new Species { FlavorTextEntries = new List<FlavorTextEntries> { flavor } };
            _pokeAPIRepositoryMock.Setup(x => x.GetSpeciesAsync(pokemonName.ToLower())).ReturnsAsync(species);

            //act
            var description = await _pokeShakespeareTranslationService.GetPokemonAsync(pokemonName);

            //assert
            Assert.IsNull(description);
        }


        [TestMethod]
        public async Task Test_GetSpeciesAsync_Return_Pokemon()
        {
            //arrange
            string pokemonName = "bulbasaur";
            string language = "en";
            var flavor = new FlavorTextEntries { FlavorText = "abc", Language = new Language { Name = language } };
            var species = new Species { FlavorTextEntries = new List<FlavorTextEntries> { flavor } };
            _pokeAPIRepositoryMock.Setup(x => x.GetSpeciesAsync(pokemonName.ToLower())).ReturnsAsync(species);

            //act
            var actual = await _pokeShakespeareTranslationService.GetPokemonAsync(pokemonName);

            //assert
            Assert.AreEqual(species.FlavorTextEntries.First(x => x.Language.Name == language).FlavorText, actual);
        }

        [TestMethod]
        public async Task Test_GetSpeciesAsync_Pokemon_DoNot_Exist_Return_Null()
        {
            //arrange
            string pokemonName = "a pokemon";
            _pokeAPIRepositoryMock.Setup(x => x.GetSpeciesAsync(pokemonName.ToLower())).ReturnsAsync((Species)null);

            //act
            var actual = await _pokeShakespeareTranslationService.GetPokemonAsync(pokemonName);

            //assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public async Task Test_GetShakespeareTranslationAsync_Return_Translation()
        {
            //arrange
            string description = "a description";
            Translation translation = new Translation { Contents = new Contents { Translated = "a translation" } };
            _shakespeareTranslatorRepositoryMock.Setup(x => x.GetShakespeareTranslationAsync(description))
                .ReturnsAsync(translation);
            _escapeServiceMock.Setup(x => x.EscapeString(description)).Returns(description);

            //act
            var actual = await _pokeShakespeareTranslationService.GetShakespeareTranslationAsync(description);

            //assert
            Assert.AreEqual(translation.Contents.Translated, actual);
        }

        [TestMethod]
        public async Task Test_GetShakespeareTranslationAsync_WhenTranslatedPropertyIsNull_Return_Null()
        {
            //arrange
            string description = "description";
            Translation translation = new Translation { Contents = new Contents { Translated = null } };

            _shakespeareTranslatorRepositoryMock.Setup(x => x.GetShakespeareTranslationAsync(description))
                .ReturnsAsync(translation);
            _escapeServiceMock.Setup(x => x.EscapeString(description)).Returns(description);

            //act
            var actual = await _pokeShakespeareTranslationService.GetShakespeareTranslationAsync(description);

            //assert
            Assert.IsNull(actual);
        }


        [TestMethod]
        public async Task Test_GetShakespeareTranslationAsync_TranslationIsNull_Return_Null()
        {
            //arrange
            string description = "description";
            _shakespeareTranslatorRepositoryMock.Setup(x => x.GetShakespeareTranslationAsync(description))
                .ReturnsAsync((Translation)null);
            _escapeServiceMock.Setup(x => x.EscapeString(description)).Returns(description);

            //act
            var translation = await _pokeShakespeareTranslationService.GetShakespeareTranslationAsync(description);

            //assert
            Assert.IsNull(translation);
        }

     

    }
}
