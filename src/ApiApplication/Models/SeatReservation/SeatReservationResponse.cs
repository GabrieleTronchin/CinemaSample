using System.Collections.Generic;

namespace Cinema.Api.Models.SeatReservation
{
    public class SeatReservationResponse
    {
        public Guid Id { get; set; }

        public IEnumerable<Seat> Seats { get; set; }

        public int AuditoriumId { get; set; }

        public string MovieTitle { get; set; }

    }

}
