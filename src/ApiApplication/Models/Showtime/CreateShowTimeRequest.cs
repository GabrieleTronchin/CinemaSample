using System;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Api.Models.Showtime
{
    public class CreateShowTimeRequest
    {
        [Required]
        public string ImdbId { get; set; }

        [Required]
        public DateTime SessionDate { get; set; }

        [Required]
        public int AuditoriumId { get; set; }
    }
}
