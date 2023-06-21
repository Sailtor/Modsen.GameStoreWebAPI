using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using BLL.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/genres")]
    [Authorize]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreForResponceDto>>> GetGenres()
        {
            return Ok(await _genreService.GetAllGenresAsync());
        }

        [HttpGet("{genreid}")]
        public async Task<ActionResult<GenreForResponceDto>> GetGenre(int genreid)
        {
            return Ok(await _genreService.GetGenreByIdAsync(genreid));
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public async Task<IActionResult> PostGenre(GenreForCreationDto genreForCreation)
        {
            await _genreService.AddGenreAsync(genreForCreation);
            return NoContent();

        }

        [Authorize(Roles = "1")]
        [HttpPut("{genreid}")]
        public async Task<IActionResult> PutGenre(int genreid, GenreForCreationDto genreForCreation)
        {
            await _genreService.UpdateGenreAsync(genreid, genreForCreation);
            return NoContent();
        }

        [Authorize(Roles = "1")]
        [HttpDelete("{genreid}")]
        public async Task<IActionResult> DeleteGenre(int genreid)
        {
            await _genreService.DeleteGenreAsync(genreid);
            return NoContent();
        }
    }
}
