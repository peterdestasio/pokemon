using Pokemon.Data.Model;
using System.Threading.Tasks;

namespace Pokemon.Data.Intefaces
{
    public interface IPokeAPIRepository
    {
        Task<Species> GetSpeciesAsync(string name);
    }
}
