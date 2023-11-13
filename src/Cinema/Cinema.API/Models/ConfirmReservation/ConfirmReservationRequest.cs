namespace Cinema.Api.Models.ConfirmReservation
{
    public class ConfirmReservationRequest
    {
        [Required]
        public string ReservationId { get; set; }
    }
}
