using MediatR;

namespace Cinema.Application.Commands;

public class CreateShowtimeCommand : IRequest<CreateShowtimeCommandComplete>
{

    public DateTime SessionDate { get; set; }

    public int AuditoriumId { get; set; }

    public Movie Movie { get; set; }

}

public class Movie
{
    public string Title { get; set; }
    public string ImdbId { get; set; }
    public string Stars { get; set; }
    public DateTime ReleaseDate { get; set; }
}

public record CreateShowtimeCommandComplete
{
    public Guid? Id { get; set; }
}
