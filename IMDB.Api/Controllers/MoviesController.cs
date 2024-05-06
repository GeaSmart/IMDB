using IMDB.Api.Mapping;
using IMDB.Application.Models;
using IMDB.Application.Repositories;
using IMDB.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Api.Controllers
{
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }
        [HttpGet(ApiEndpoints.Movies.GetAll)]
        public async Task<IActionResult> Get()
        {
            var movies = await movieRepository.GetAllAsync();
            var moviesResponse = movies.MapToResponse();
            return Ok(moviesResponse);
        }

        [HttpGet(ApiEndpoints.Movies.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var movie = await movieRepository.GetByIdAsync(id);
            if(movie is null)
            {
                return NotFound();
            }
            var response = movie.MapToResponse();
            return Ok(response);
        }

        [HttpPost(ApiEndpoints.Movies.Create)]
        public async Task<IActionResult> Create([FromBody]CreateMovieRequest request)
        {
            var movie = request.MapToMovie();
            await movieRepository.CreateAsync(movie);
            return Created($"/api/movies/{movie.Id}", movie);
        }
    }
}
