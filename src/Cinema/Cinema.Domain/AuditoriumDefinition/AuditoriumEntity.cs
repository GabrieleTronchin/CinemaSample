using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Domain
{
    public class AuditoriumEntity
    {
        public int Id { get; set; }
        public ICollection<SeatEntity> Seats { get; set; }
    }

}
