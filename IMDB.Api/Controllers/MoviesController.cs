using IMDB.Application.Repositories;
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
    }
}
