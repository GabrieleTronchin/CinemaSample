namespace Movies.Aggregator.Domain.Models;

public class CreateShowTime
{
    public string ImdbId { get; set; }

    public DateTime SessionDate { get; set; }

    public int AuditoriumId { get; set; }
}
