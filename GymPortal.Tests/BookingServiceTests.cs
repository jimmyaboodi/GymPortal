using GymPortal.Domain.Entities;
using GymPortal.Infrastructure.Data;
using GymPortal.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GymPortal.Tests
{
    public class BookingServiceTests
    {
        private DbContextOptions<GymDbContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<GymDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task BookSessionAsync_ShouldReturnFalse_WhenUserIsAlreadyBooked()
        {
            var options = CreateNewContextOptions();
            var userId = "test-user-id";
            var sessionId = 1;

            using (var context = new GymDbContext(options))
            {
                context.Bookings.Add(new Booking { UserId = userId, SessionId = sessionId });
                await context.SaveChangesAsync();
            }

            bool result;

            using (var context = new GymDbContext(options))
            {
                var service = new BookingService(context);
                result = await service.BookSessionAsync(userId, sessionId);
            }

            Assert.False(result);
        }

        [Fact]
        public async Task BookSessionAsync_ShouldReturnTrue_WhenBookingIsNew()
        {
            var options = CreateNewContextOptions();
            var userId = "test-user-id";
            var sessionId = 1;

            bool result;

            using (var context = new GymDbContext(options))
            {
                var service = new BookingService(context);
                result = await service.BookSessionAsync(userId, sessionId);
            }

            Assert.True(result);
        }
    }
}