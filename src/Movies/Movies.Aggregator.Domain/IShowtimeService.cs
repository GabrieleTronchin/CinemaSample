using Movies.Aggregator.Domain.Models;

namespace Movies.Aggregator.Domain
{
    internal interface IShowtimeService
    {
        Task<CreateShowTimeResponse> Create(CreateShowTime createRequest);
    }
}