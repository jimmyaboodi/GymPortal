namespace GymPortal.Domain.Entities
{
    public class Membership
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}