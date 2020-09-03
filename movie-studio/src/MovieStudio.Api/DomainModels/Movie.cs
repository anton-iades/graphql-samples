using System;

namespace MovieStudio.Api.DomainModels
{
    public class Movie
    {
        public string Id { get; internal set; }
        public string Title { get; internal set; }
        public DateTime Released { get; internal set; }
        public string[] Genres { get; internal set; }
        public string[] Directors { get; internal set; }
        public string[] Writers { get; internal set; }
        public string[] Cast { get; internal set; }
    }
}
