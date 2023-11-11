using Cinema.Domain;

namespace Cinema.Persistence;

    public class SampleData
    {
        public static void Initialize(IApplicationBuilder app)
        {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = serviceScope.ServiceProvider.GetService<CinemaDbContext>();
        context.Database.EnsureCreated();

        context.Auditoriums.Add(AuditoriumEntity.Create(1, GenerateSeats(1,28, 22)));
        context.Auditoriums.Add(AuditoriumEntity.Create(2, GenerateSeats(2,21, 18)));
        context.Auditoriums.Add(AuditoriumEntity.Create(3, GenerateSeats(3,15, 21)));

        context.SaveChanges();
    }

    private static List<SeatEntity> GenerateSeats(int auditoriumId, short rows, short seatsPerRow)
    {
        var seats = new List<SeatEntity>();
        for (short r = 1; r <= rows; r++)
            for (short s = 1; s <= seatsPerRow; s++)
                seats.Add(SeatEntity.Create(auditoriumId, r, s ));
        return seats;
    }
}
    
