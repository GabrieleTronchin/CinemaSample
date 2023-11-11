
namespace Cinema.Domain.Showtime;

public class MovieEntity
{
    public static MovieEntity Create(string title, string stars, string imdbId, DateTime releaseDate)
    {
        return new MovieEntity
        {
            Id = Guid.NewGuid(),
            Title = title,
            ImdbId = imdbId,
            Stars = stars,
            ReleaseDate = releaseDate
        };
    }

    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string ImdbId { get; private set; }
    public string Stars { get; private set; }
    public DateTime ReleaseDate { get; private set; }
}

