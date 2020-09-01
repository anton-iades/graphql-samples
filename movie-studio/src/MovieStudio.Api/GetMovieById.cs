using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MovieStudio.Api
{
    internal static class GetMovieById
    {
        public class Request : IRequest<object>
        {
            public string Id { get; }

            public Request(string id)
            {
                Id = id;
            }
        }

        public class Handler : IRequestHandler<Request, object>
        {
            private static IDictionary<string, object> Movies = new Dictionary<string, object>
            {
                ["111"] = new { Id = "111", Title = "The Batman", Year = 2021, Genres = new[] { "Action", "Crime", "Drama" } },
                ["222"] = new { Id = "222", Title = "Dunkirk", Year = 2017, Genres = new[] { "Action", "Drama", "History" } },
                ["333"] = new { Id = "333", Title = "The Prestige", Year = 2006, Genres = new[] { "Drama", "Mystery", "Sci-Fi" } },
            };

            public Task<object> Handle(Request request, CancellationToken cancellationToken)
            {
                Movies.TryGetValue(request.Id, out var movie);
                return Task.FromResult(movie);
            }
        }
    }
}