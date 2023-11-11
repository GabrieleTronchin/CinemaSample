using Cinema.Domain;

namespace Cinema.Persistence;

public class SampleData
{
    public static void Initialize(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = serviceScope.ServiceProvider.GetService<CinemaDbContext>();
        context.Database.EnsureCreated();

        context.Auditoriums.Add(AuditoriumEntity.Create(1, GenerateSeats(28, 22)));
        context.Auditoriums.Add(AuditoriumEntity.Create(2, GenerateSeats(21, 18)));
        context.Auditoriums.Add(AuditoriumEntity.Create(3, GenerateSeats(15, 21)));

        context.SaveChanges();
    }

    private static List<Seat> GenerateSeats(short rows, short seatsPerRow)
    {
        var seats = new List<Seat>();
        for (short r = 1; r <= rows; r++)
            for (short s = 1; s <= seatsPerRow; s++)
                seats.Add(new Seat(r, s));
        return seats;
    }
}

