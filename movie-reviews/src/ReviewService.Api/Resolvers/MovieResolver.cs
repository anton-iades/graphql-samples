using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL;
using MediatR;
using ReviewService.Api.Models;
using ReviewService.Api.Queries;

namespace ReviewService.Api.Resolvers
{
    /// <summary>
    /// This resolver tells the gateway how to extend the Movie type with additional fields e.g. add the reviews field
    /// It does not need to worry about resolving the core Movie fields because that is done by the Movie service. 
    /// </summary>
    [GraphQLMetadata("Movie")]
    public class MovieResolver
    {
        private readonly IMediator _mediator;

        public MovieResolver(IMediator mediator)
        {
            _mediator = mediator;
        }

        [GraphQLMetadata("reviews")]
        public Task<IReadOnlyList<Review>> GetMovieReviews(ReadonlyResolveFieldContext ctx)
        {
            var movie = ctx.Source.GetPropertyValue<Movie>();
            return _mediator.Send(new GetReviewsByMovieId.Request(movie.Id));
        }
    }
}