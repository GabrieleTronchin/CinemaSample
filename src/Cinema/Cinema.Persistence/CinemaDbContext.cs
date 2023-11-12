using Cinema.Domain;
using Cinema.Domain.Showtime;
using Cinema.Domain.Ticket;

namespace Cinema.Persistence
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options)
        {
        }

        public DbSet<AuditoriumEntity> Auditoriums { get; set; }
        public DbSet<MovieEntity> Movies { get; set; }
        public DbSet<ShowtimeEntity> Showtimes { get; set; }
        public DbSet<ShowtimeSeatEntity> ShowtimesSeats { get; set; }
        public DbSet<TicketEntity> Tickets { get; set; }
        public DbSet<DomainEvent> DomainEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CinemaDbContext).Assembly);
        }

    }
}
