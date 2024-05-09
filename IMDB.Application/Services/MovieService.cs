using IMDB.Application.Models;
using IMDB.Application.Repositories;

namespace IMDB.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly MovieRepository movieRepository;

        public MovieService(MovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
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
            return await movieRepository.CreateAsync(movie);
        }

        public async Task<Movie?> UpdateAsync(Movie movie)
        {            
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
