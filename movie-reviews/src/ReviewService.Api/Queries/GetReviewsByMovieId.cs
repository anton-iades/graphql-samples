using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ReviewService.Api.Models;

namespace ReviewService.Api.Queries
{
    public static class GetReviewsByMovieId
    {
        public class Request : IRequest<IReadOnlyList<Review>>
        {
            public string MovieId { get; }

            public Request(string movieId)
            {
                MovieId = movieId;
            }
        }

        public class Handler : IRequestHandler<Request, IReadOnlyList<Review>>
        {
            private readonly GetReviewsContext _getReviewsContext;

            public Handler(GetReviewsContext getReviewsContext)
            {
                _getReviewsContext = getReviewsContext;
            }

            public Task<IReadOnlyList<Review>> Handle(Request request, CancellationToken cancellationToken)
            {
                var review = _getReviewsContext()
                    .Where(m => m.MovieId == request.MovieId)
                    .ToList();

                return Task.FromResult<IReadOnlyList<Review>>(review);
            }
        }
    }
}