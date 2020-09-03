using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MovieStudio.Api.DomainModels;

namespace MovieStudio.Api.RequestHandlers
{
    internal static class GetMovieById
    {
        public class Request : IRequest<Movie>
        {
            public string Id { get; }

            public Request(string id)
            {
                Id = id;
            }
        }

        public class Handler : IRequestHandler<Request, Movie>
        {
            private static IDictionary<string, Movie> Movies = new Dictionary<string, Movie>
            {
                ["111"] = new Movie { Id = "111", Title = "The Batman", Released = new DateTime(2021, 01, 01), Genres = new[] { "Action", "Crime", "Drama" }, Directors = new[] { "111" }, Writers = new[] { "111" }, Cast = new[] { "666", "777" } },
                ["222"] = new Movie { Id = "222", Title = "Dunkirk", Released = new DateTime(2017, 01, 01), Genres = new[] { "Action", "Drama", "History" }, Directors = new[] { "222", "333" }, Writers = new[] { "444" }, Cast = new[] { "888", "999" } },
                ["333"] = new Movie { Id = "333", Title = "The Prestige", Released = new DateTime(2006, 01, 01), Genres = new[] { "Drama", "Mystery", "Sci-Fi" }, Directors = new[] { "222", "333" }, Writers = new[] { "222" }, Cast = new[] { "555", "666", "111" } },
            };

            public Task<Movie> Handle(Request request, CancellationToken cancellationToken)
            {
                Movies.TryGetValue(request.Id, out var movie);
                return Task.FromResult(movie);
            }
        }
    }
}