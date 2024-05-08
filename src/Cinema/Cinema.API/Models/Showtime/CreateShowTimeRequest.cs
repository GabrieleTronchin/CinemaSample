namespace Cinema.Api.Models.Showtime
{
    public class CreateShowTimeRequest
    {
        [Required]
        public MovieApiModel Movie { get; set; }

        [Required]
        public DateTime SessionDate { get; set; }

        [Required]
        public int AuditoriumId { get; set; }
    }

    public class MovieApiModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string ImdbId { get; set; }

        [Required]
        public string Stars { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }
    }
}
