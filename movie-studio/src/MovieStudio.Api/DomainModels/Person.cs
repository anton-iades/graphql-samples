using System;

namespace MovieStudio.Api.DomainModels
{
    public class Person
    {
        public string Id { get; internal set; }
        public string Name { get; internal set; }
        public DateTime BirthDate { get; internal set; }
    }
}
