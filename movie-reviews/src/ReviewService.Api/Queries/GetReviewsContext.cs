using System.Collections.Generic;
using ReviewService.Api.Models;

namespace ReviewService.Api.Queries
{
    public delegate IEnumerable<Review> GetReviewsContext();
}