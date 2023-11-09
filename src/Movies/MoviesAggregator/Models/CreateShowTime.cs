using System.ComponentModel.DataAnnotations;

namespace MoviesAggregator.Models
{
    public class CreateShowTime
    {
        [Required]
        public string ImdbId { get; set; }

        [Required]
        public DateTime SessionDate { get; set; }

        [Required]
        public int AuditoriumId { get; set; }
    }
}
