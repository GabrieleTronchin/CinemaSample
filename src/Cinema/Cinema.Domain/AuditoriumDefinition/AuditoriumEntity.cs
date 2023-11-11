﻿using Cinema.Domain.Showtime;

namespace Cinema.Domain.AuditoriumDefinition;
public class AuditoriumEntity
{
    public Guid Id { get; private set; }
    public IEnumerable<Seat> Seats { get; private set; } = Enumerable.Empty<Seat>();
}
