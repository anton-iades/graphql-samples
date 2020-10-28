using System;

namespace ReviewService.Api.Models
{
    /// <summary>
    /// This is the backing model for the GraphQL type Review.
    /// This service is responsible for defining and resolving this type so the full type definition exists here.
    /// </summary>
    public class Review
    {
        public string Id { get; set; }
        public string MovieId { get; set; }
        public double Stars { get; set; }
        public string Message { get; set; }
        public bool ContainsSpoilers { get; set; }
        public DateTime Created { get; set; }
    }
}