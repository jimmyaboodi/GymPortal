using GymPortal.Domain.Entities;

namespace GymPortal.Application.Interfaces
{
    public interface IMembershipService
    {
        Task<Membership?> GetUserMembershipAsync(string userId);
        Task<bool> CreateMembershipAsync(string userId, string type);
    }
}