namespace Cinema.Application.Queries.Models
{
    public class ShowTimeReadModel
    {
        public int Id { get; set; }
        public MovieReadModel Movie { get; set; }
        public DateTime SessionDate { get; set; }
        public int AuditoriumId { get; set; }
    }


}
