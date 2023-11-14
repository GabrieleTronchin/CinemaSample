using System.ComponentModel.DataAnnotations;

namespace Movies.Aggregator.Api.Models
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
