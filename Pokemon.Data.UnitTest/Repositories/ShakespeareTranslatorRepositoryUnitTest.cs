using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Pokemon.Data.Configuration;
using Pokemon.Data.Http;
using Pokemon.Data.Model;
using Pokemon.Data.Repositories;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pokemon.Data.UnitTest.Repositories
{
    [TestClass]
    public class ShakespeareTranslatorRepositoryUnitTest
    {
        private ShakespeareTranslatorRepository _translationRepository;
        private Mock<IHttpClientWrapper> _httpClientWrapperMock;
        private IOptions<TranslationAPIConfig> _config;

        [TestInitialize]
        public void Initialize()
        {
            _config = Options.Create(new TranslationAPIConfig() { EndPoint = "http://localhost/translation/{0}" });
            _httpClientWrapperMock = new Mock<IHttpClientWrapper>();
            _translationRepository = new ShakespeareTranslatorRepository(_config, _httpClientWrapperMock.Object);
        }

        [TestMethod]
        public async Task Test_GetShakespeareTranslationAsync_Return_Translations()
        {
            //arrange
            var text = "some text to translate";
            var translation = "a tranlastion";
            _httpClientWrapperMock
                .Setup(x => x.GetAsync(It.Is<string>(s => s.Contains(string.Format(_config.Value.EndPoint, text)))))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(new Translation{ Contents = new Contents() { Translated = translation } }))
                });

            //act
            var actual = await _translationRepository.GetShakespeareTranslationAsync(text);

            //assert
            Assert.AreEqual(translation, actual.Contents.Translated);
        }

        [TestMethod]
        public async Task Test_GetShakespeareTranslationAsync_Return_Null()
        {
            //arrange
            var text = "some text to translate";
            _httpClientWrapperMock
                .Setup(x => x.GetAsync(It.Is<string>(s => s.Contains(string.Format(_config.Value.EndPoint, text)))))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.NotFound
                });

            //act
            var actual = await _translationRepository.GetShakespeareTranslationAsync(text);

            //assert
            Assert.IsNull(actual);
        }
    }
}
