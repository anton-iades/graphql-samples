using System.Threading.Tasks;
using GraphQL;
using MediatR;

namespace MovieStudio.Api
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
        public Task<object> GetMovieById(string id) => mediator.Send(new GetMovieById.Request(id));
    }
}