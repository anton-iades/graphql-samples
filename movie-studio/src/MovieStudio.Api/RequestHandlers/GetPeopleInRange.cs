using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MovieStudio.Api.DomainModels;

namespace MovieStudio.Api.RequestHandlers
{
    internal static class GetPeopleInRange
    {
        public class Request : IRequest<Person[]>
        {
            public string[] Ids { get; }

            public Request(IEnumerable<string> ids)
            {
                Ids = ids.ToArray();
            }
        }

        public class Handler : IRequestHandler<Request, Person[]>
        {
            private readonly IDictionary<string, Person> people;

            public Handler()
            {
                var rnd = new Random(2020);
                people = Enumerable.Range(1, 9)
                    .Select(i => (i * 111).ToString())
                    .Select(id => new Person
                    {
                        Id = id,
                        Name = $"Person #{id}",
                        BirthDate = DateTime.Now.Date.AddYears(-rnd.Next(23, 59))

                    }).ToDictionary(i => i.Id);
            }

            public Task<Person[]> Handle(Request request, CancellationToken cancellationToken)
            {
                var result = people.Values.Where(p => request.Ids.Contains(p.Id)).ToArray();
                return Task.FromResult(result);
            }
        }
    }
}