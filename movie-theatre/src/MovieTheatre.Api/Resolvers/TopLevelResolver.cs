using System;
using System.Threading.Tasks;
using GraphQL;
using MediatR;

namespace MovieTheatre.Api.Resolvers
{
    [GraphQLMetadata("Query")]
    internal class TopLevelResolver
    {
        private readonly IMediator mediator;

        public TopLevelResolver(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [GraphQLMetadata("nextSession")]
        public object GetNextSessionInfo()
        {
            var now = DateTime.Now;
            var next = new DateTime(now.Year, now.Month, now.Day, 18, 00, 0, DateTimeKind.Local);
            if (now.Hour > 18) { next = next.AddDays(1); }
            return new
            {
                Time = next,
                Theatre = "Broadway",
                Movie = new
                {
                    Id = "111"
                }
            };
        }
    }
}