using IMDB.Api.Mapping;
using IMDB.Application.Models;
using IMDB.Application.Repositories;
using IMDB.Application.Services;
using IMDB.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Api.Controllers
{
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService movieService;

        public MoviesController(IMovieService movieService)
        {
            this.movieService = movieService;
        }
        [HttpGet(ApiEndpoints.Movies.GetAll)]
        public async Task<IActionResult> Get()
        {
            var movies = await movieService.GetAllAsync();
            var moviesResponse = movies.MapToResponse();
            return Ok(moviesResponse);
        }

        [HttpGet(ApiEndpoints.Movies.Get)]
        public async Task<IActionResult> Get([FromRoute] string idOrSlug)
        {
            //get by id or slug
            var movie = Guid.TryParse(idOrSlug, out var id) ?
                await movieService.GetByIdAsync(id) :
                await movieService.GetBySlugAsync(idOrSlug);

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
            await movieService.CreateAsync(movie);
            return CreatedAtAction(nameof(Get), new { idOrSlug = movie.Id }, movie);            
        }

        [HttpPut(ApiEndpoints.Movies.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateMovieRequest request)
        {
            var movie = request.MapToMovie(id);
            var updatedMovie = await movieService.UpdateAsync(movie);
            if (updatedMovie is null)
                return NotFound();

            var response = updatedMovie.MapToResponse();
            return Ok(response);
        }        
        
        [HttpDelete(ApiEndpoints.Movies.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await movieService.DeleteByIdAsync(id);
            if(!deleted)
                return NotFound();
            return Ok();
        }
    }
}
