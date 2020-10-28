using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ReviewService.Api.Models;

namespace ReviewService.Api.Queries
{
    public static class GetReviewById
    {
        public class Request : IRequest<Review>
        {
            public string ReviewId { get; }

            public Request(string reviewId)
            {
                ReviewId = reviewId;
            }
        }

        public class Handler : IRequestHandler<Request, Review>
        {
            private readonly GetReviewsContext _getReviewsContext;

            public Handler(GetReviewsContext getReviewsContext)
            {
                _getReviewsContext = getReviewsContext;
            }

            public Task<Review> Handle(Request request, CancellationToken cancellationToken)
            {
                var review = _getReviewsContext()
                    .FirstOrDefault(m => m.Id == request.ReviewId);

                return Task.FromResult(review);
            }
        }
    }
}