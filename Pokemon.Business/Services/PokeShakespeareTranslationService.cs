using Pokemon.Business.Interfaces;
using Pokemon.Data.Intefaces;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemon.Business.Services
{
    public class PokeShakespeareTranslationService : IPokeShakespeareTranslationService
    {
        private readonly IPokeAPIRepository _pokeAPIRepository;
        private readonly IShakespeareTranslatorRepository _shakespeareTranslatorRepository;
        private readonly IEscapeService _escapeService;
        private const string DefaultLanguage = "en";
        public PokeShakespeareTranslationService(IPokeAPIRepository pokeAPIRepository, IShakespeareTranslatorRepository shakespeareTranslatorRepository, IEscapeService escapeService)
        {
            _pokeAPIRepository = pokeAPIRepository;
            _shakespeareTranslatorRepository = shakespeareTranslatorRepository;
            _escapeService = escapeService;
        }

        public async Task<string> GetPokemonAsync(string pokemonName)
        {
            var pokemonSpecies = await _pokeAPIRepository.GetSpeciesAsync(pokemonName.ToLower());
            return pokemonSpecies?.FlavorTextEntries.FirstOrDefault(fl => fl.Language.Name == DefaultLanguage)?.FlavorText;
        }

        public async Task<string> GetShakespeareTranslationAsync(string description)
        {
            var escaped = _escapeService.EscapeString(description);
            var translation = await _shakespeareTranslatorRepository.GetShakespeareTranslationAsync(escaped);
            return translation?.Contents?.Translated;
        }

    }
}
