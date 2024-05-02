using IMDB.Application.Models;

namespace IMDB.Application.Repositories
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie?> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(Movie movie);        
        Task<bool> UpdateAsync(Movie movie);
        Task<bool> DeleteByIdAsync(Guid id);
    }
}
