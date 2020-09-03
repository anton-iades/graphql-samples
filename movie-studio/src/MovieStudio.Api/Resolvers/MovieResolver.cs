using System.Threading.Tasks;
using GraphQL;
using MediatR;
using MovieStudio.Api.DomainModels;
using MovieStudio.Api.RequestHandlers;

namespace MovieStudio.Api.Resolvers
{
    [GraphQLMetadata("Movie", IsTypeOf = typeof(Movie))]
    public class MovieResolver
    {
        private readonly IMediator mediator;

        public MovieResolver(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [GraphQLMetadata("directedBy")]
        public async Task<object[]> GetDirectors(Movie movie) => await mediator.Send(new GetPeopleInRange.Request(movie.Directors));

        [GraphQLMetadata("writtenBy")]
        public async Task<object[]> GetWriters(Movie movie) => await mediator.Send(new GetPeopleInRange.Request(movie.Writers));

        [GraphQLMetadata("cast")]
        public async Task<object[]> GetCast(Movie movie) => await mediator.Send(new GetPeopleInRange.Request(movie.Cast));
    }
}
