using IMDB.Api.Mapping;
using IMDB.Application.Models;
using IMDB.Application.Repositories;
using IMDB.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        [HttpPost("movies")]
        public async Task<IActionResult> Create([FromBody]CreateMovieRequest request)
        {
            var movie = request.MapToMovie();
            await movieRepository.CreateAsync(movie);
            return Created($"/api/movies/{movie.Id}", movie);
        }
    }
}
