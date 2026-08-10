namespace GymPortal.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int SessionId { get; set; }
        public Session Session { get; set; } = null!;
    }
}