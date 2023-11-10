using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Domain.Showtime
{
    public class ShowtimeSeatEntity
    {
        public short Row { get; set; }
        public short SeatNumber { get; set; }
        public TimeSpan ReservationCooldown { get; set; }
        public DateTime ReservationTime { get; set; }
        public bool Purchased { get; set; }

    }
}
