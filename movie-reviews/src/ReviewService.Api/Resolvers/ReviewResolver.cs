using GraphQL;
using ReviewService.Api.Models;

namespace ReviewService.Api.Resolvers
{
    /// <summary>
    /// This resolver tells the gateway how to attach a movie reference to a given Review type.
    /// In this case, we just need to resolve a stub of the Movie type with the @key fields
    /// The gateway will fill in the rest of the fields which it gets from the movie service.
    /// </summary>
    [GraphQLMetadata("Review", IsTypeOf = typeof(Review))]
    public class ReviewResolver
    {
        [GraphQLMetadata("movie")]
        public Movie GetMovie(Review src) => new Movie { Id = src.MovieId };
    }
}