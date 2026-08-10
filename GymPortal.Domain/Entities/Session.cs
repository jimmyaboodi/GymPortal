namespace GymPortal.Domain.Entities
{
    public class Session
    {
        public int Id { get; set; } 

        public string Name { get; set; } = string.Empty;

        public DateTime DateAndTime { get; set; }

        public string? Instructor { get; set; }

        public string? Category { get; set; }
    }
}