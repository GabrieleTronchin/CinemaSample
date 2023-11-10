using System.Collections.Generic;

namespace Cinema.Api.Models.SeatReservation
{
    public class SeatReservationResponse
    {
        public string Id { get; set; }

        public IEnumerable<Seat> SeatsNumber { get; set; }

        public int AuditoriumId { get; set; }

        public string MovieTitle { get; set; }

    }

}
