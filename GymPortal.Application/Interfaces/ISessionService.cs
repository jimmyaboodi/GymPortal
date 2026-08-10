using GymPortal.Domain.Entities;

namespace GymPortal.Application.Interfaces
{
    public interface ISessionService
    {
        Task<IEnumerable<Session>> GetAllSessionsAsync();
        Task<Session?> GetSessionByIdAsync(int id);
    }
}