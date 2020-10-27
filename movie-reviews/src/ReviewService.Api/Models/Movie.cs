namespace ReviewService.Api.Models
{
    /// <summary>
    /// This is the backing model for the GraphQL type Movie.
    /// The full type definition exists in the Movie service but if we want to be able to reference or extend this type,
    /// we need to define a stub in this service containing only its @key fields.
    /// </summary>
    public class Movie
    {
        public string Id { get; set; }
    }
}