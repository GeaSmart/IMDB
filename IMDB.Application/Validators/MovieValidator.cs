using FluentValidation;
using IMDB.Application.Models;
using IMDB.Application.Repositories;

namespace IMDB.Application.Validators
{
    public class MovieValidator : AbstractValidator<Movie>
    {
        private readonly IMovieRepository movieRepository;

        public MovieValidator(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }
        public MovieValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x=>x.Genres)
                .NotEmpty();
            
            RuleFor(x=>x.Title)
                .NotEmpty();

            RuleFor(x => x.YearOfRelease)
                .LessThanOrEqualTo(DateTime.UtcNow.Year);

            RuleFor(x => x.Slug)
                .MustAsync(ValidateSlug)
                .WithMessage("The system already contains this movie.");
        }

        private async Task<bool> ValidateSlug(Movie movie, string slug, CancellationToken token)
        {
            var existingMovie = await movieRepository.GetBySlugAsync(slug);
            if (existingMovie is not null)
                return existingMovie.Id == movie.Id;

            return existingMovie is null;            
        }
    }
}
