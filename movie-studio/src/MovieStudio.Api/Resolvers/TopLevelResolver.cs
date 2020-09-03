using System.Threading.Tasks;
using GraphQL;
using MediatR;
using MovieStudio.Api.RequestHandlers;

namespace MovieStudio.Api.Resolvers
{
    [GraphQLMetadata("Query")]
    internal class TopLevelResolver
    {
        private readonly IMediator mediator;

        public TopLevelResolver(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [GraphQLMetadata("movie")]
        public async Task<object> GetMovieById(string id) => await mediator.Send(new GetMovieById.Request(id));
    }
}