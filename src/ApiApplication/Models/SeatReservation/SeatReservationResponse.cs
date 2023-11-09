using System.Collections.Generic;

namespace Cinema.Api.Models.SeatReservation
{
    public class SeatReservationResponse
    {
        public string Id { get; set; }

        public IEnumerable<int> SeatsNumber { get; set; }

        public int AuditoriumId { get; set; }

        public string MovieTitle { get; set; }

    }
}
