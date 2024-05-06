﻿namespace IMDB.Contracts.Requests
{
    public class UpdateMovieRequest
    {
        public required int Title { get; init; }
        public required int YearOfRelease { get; init; }
        public required IEnumerable<string> Genres { get; init; } = Enumerable.Empty<string>();
    }
}