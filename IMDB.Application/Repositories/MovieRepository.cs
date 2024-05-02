using IMDB.Application.Models;

namespace IMDB.Application.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly List<Movie> movies = new();

        public Task<IEnumerable<Movie>> GetAllAsync()
        {
            return Task.FromResult(movies.AsEnumerable());
        }
        public Task<Movie?> GetByIdAsync(Guid id)
        {
            var movie = movies.SingleOrDefault(x => x.Id == id);
            return Task.FromResult(movie);
        }
        public Task<bool> CreateAsync(Movie movie)
        {
            movies.Add(movie);
            return Task.FromResult(true);
        }
        public Task<bool> UpdateAsync(Movie movie)
        {
            var movieIndex = movies.FindIndex(x => x.Id == movie.Id);
            if (movieIndex == -1)
            {
                return Task.FromResult(false);
            }
            movies[movieIndex] = movie;
            return Task.FromResult(true);
        }
        public Task<bool> DeleteByIdAsync(Guid id)
        {
            var removedCount = movies.RemoveAll(x => x.Id == id);
            var movieRemoved = removedCount > 0;
            return Task.FromResult(movieRemoved);
        }
    }
}
