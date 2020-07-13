using Pokemon.Data.Model;
using System.Threading.Tasks;

namespace Pokemon.Data.Intefaces
{
    public interface IShakespeareTranslatorRepository
    {
        Task<Translation> GetShakespeareTranslationAsync(string text);
    }
}
