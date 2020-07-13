using System.Threading.Tasks;

namespace Pokemon.Business.Interfaces
{
    public interface IPokeShakespeareTranslationService
    {
        Task<string> GetPokemonAsync(string pokemonName);

        Task<string> GetShakespeareTranslationAsync(string description);
    }
}
