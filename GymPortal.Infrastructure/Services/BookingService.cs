using GymPortal.Application.Interfaces;
using GymPortal.Domain.Entities;
using GymPortal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymPortal.Infrastructure.Services
{
    public class BookingService : IBookingService
    {
        private readonly GymDbContext _context;

        public BookingService(GymDbContext context)
        {
            _context = context;
        }

        public async Task<bool> BookSessionAsync(string userId, int sessionId)
        {
            bool alreadyBooked = await _context.Bookings
                .AnyAsync(b => b.UserId == userId && b.SessionId == sessionId);

            if (alreadyBooked)
            {
                return false; 
            }

            var booking = new Booking
            {
                UserId = userId,
                SessionId = sessionId
            };

            _context.Bookings.Add(booking);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<IEnumerable<Booking>> GetUserBookingsAsync(string userId)
        {
            return await _context.Bookings
                .Include(b => b.Session)
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> CancelBookingAsync(string userId, int sessionId)
        {
            var booking = await _context.Bookings
                .FirstOrDefaultAsync(b => b.UserId == userId && b.SessionId == sessionId);

            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }

            return false;
        }
    }
}