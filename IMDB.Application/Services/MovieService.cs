using FluentValidation;
using IMDB.Application.Models;
using IMDB.Application.Repositories;
using IMDB.Application.Validators;

namespace IMDB.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository movieRepository;
        private readonly IValidator<Movie> movieValidator;

        public MovieService(IMovieRepository movieRepository, IValidator<Movie> movieValidator)
        {
            this.movieRepository = movieRepository;
            this.movieValidator = movieValidator;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await movieRepository.GetAllAsync();
        }

        public async Task<Movie?> GetByIdAsync(Guid id)
        {
            return await movieRepository.GetByIdAsync(id);
        }

        public async Task<Movie?> GetBySlugAsync(string slug)
        {
            return await movieRepository.GetBySlugAsync(slug);
        }

        public async Task<bool> CreateAsync(Movie movie)
        {
            await movieValidator.ValidateAndThrowAsync(movie);
            return await movieRepository.CreateAsync(movie);
        }

        public async Task<Movie?> UpdateAsync(Movie movie)
        {
            await movieValidator.ValidateAndThrowAsync(movie);
            var movieExists = await movieRepository.ExistsByIdAsync(movie.Id);
            if (!movieExists)            
                return null;
            
            await movieRepository.UpdateAsync(movie);
            return movie;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            return await movieRepository.DeleteByIdAsync(id);
        }
    }
}
