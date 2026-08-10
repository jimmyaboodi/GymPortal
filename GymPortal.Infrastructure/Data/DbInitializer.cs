using GymPortal.Domain.Entities;

namespace GymPortal.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(GymDbContext context)
        {
            // Kolla om det redan finns pass i databasen.
            if (context.Sessions.Any())
            {
                return;
            }

            // Skapa testpass med dina specifika egenskaper
            var sessions = new Session[]
            {
                new Session { Name = "Yoga", DateAndTime = DateTime.Now.AddDays(1), Instructor = "Anna" },
                new Session { Name = "Crossfit", DateAndTime = DateTime.Now.AddDays(2), Instructor = "Per" },
                new Session { Name = "Spinning", DateAndTime = DateTime.Now.AddDays(3), Instructor = "Johan" }
            };

            // Lägg till passen och spara
            context.Sessions.AddRange(sessions);
            context.SaveChanges();
        }
    }
}