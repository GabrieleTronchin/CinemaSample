using Movies.Aggregator.Domain.Models;

namespace Movies.Aggregator.Domain
{
    public interface IShowtimeService
    {
        Task<CreateShowTimeResponse> Create(CreateShowTime createRequest);
    }
}