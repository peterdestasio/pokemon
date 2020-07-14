using Microsoft.AspNetCore.Mvc;
using Pokemon.Business.Interfaces;
using Pokemon.Model;
using System.Net;
using System.Threading.Tasks;

namespace Pokemon.Controllers
{
    [ApiController]
    [Route("pokemon")]
    public class PokemonShakespeareController : ControllerBase
    {
        private readonly IPokeShakespeareTranslationService _pokeShakespeareService;
       
        public PokemonShakespeareController(IPokeShakespeareTranslationService pokeShakespeareService)
        {
            _pokeShakespeareService = pokeShakespeareService;
        }

        /// <summary>
        /// Get pokemon description in a Shakespeare translation
        /// </summary>
        /// <param name="name">The pokemon name</param>
        /// <returns>The pokemon name and description in a Shakespeare translation</returns>
        [HttpGet]
        [Route("{name}")]
        [ProducesResponseType(typeof(PokemonResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(HttpErrorResponse), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(HttpErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetPokemonWithShakespeareDescriptionAsync(string name)
        {
            
            var description = await _pokeShakespeareService.GetPokemonAsync(name);

            if (string.IsNullOrEmpty(description))
            {
                return NotFound(new PokemonResponse{ Name = name });
            }

            var translation = await _pokeShakespeareService.GetShakespeareTranslationAsync(description);

            return Ok(new PokemonResponse { Name = name, Description = translation });
        }

    }
}
