using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL;
using MediatR;
using ReviewService.Api.Models;
using ReviewService.Api.Queries;

namespace ReviewService.Api.Resolvers
{
    /// <summary>
    /// This is the root resolver for the queries.
    /// </summary>
    [GraphQLMetadata("Query")]
    public class TopLevelResolver
    {
        private readonly IMediator _mediator;

        public TopLevelResolver(IMediator mediator)
        {
            _mediator = mediator;
        }

        public TopLevelResolver()
        {
        }

        [GraphQLMetadata("review")]
        public Task<Review> GetReview(string id) => _mediator.Send(new GetReviewById.Request(id));

        [GraphQLMetadata("reviewsForMovie")]
        public Task<IReadOnlyList<Review>> GetMovieReviews(string movieId) =>
            _mediator.Send(new GetReviewsByMovieId.Request(movieId));
    }
}