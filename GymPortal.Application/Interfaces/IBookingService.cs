using GymPortal.Domain.Entities;

namespace GymPortal.Application.Interfaces
{
    public interface IBookingService
    {
        Task<bool> BookSessionAsync(string userId, int sessionId);
        Task<IEnumerable<Booking>> GetUserBookingsAsync(string userId);
        Task<bool> CancelBookingAsync(string userId, int sessionId);
    }
}