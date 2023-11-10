﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Api.Models.SeatReservation
{
    public class SeatReservationRequest
    {
        [Required]
        public string ShowtimeId { get; set; }
        [Required]
        public IEnumerable<Seat> Seats { get; set; }
        [Required]
        public int AuditoriumId { get; set; }

    }
}
