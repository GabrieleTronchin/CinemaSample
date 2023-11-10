using MediatR;

namespace Cinema.Application.Commands
{
    public class AssignShowtimeCommand : IRequest<AssignShowtimeCommandComplete>
    {

        public DateTime SessionDate { get; set; }

        public int AuditoriumId { get; set; }

        public Movie Movie { get; set; }

    }

    public record Movie
    {
        public string Title { get; set; }
        public string ImdbId { get; set; }
        public string Stars { get; set; }
        public DateTime ReleaseDate { get; set; }
    }

    public record AssignShowtimeCommandComplete
    {
        public int? Id { get; set; }
    }
}
