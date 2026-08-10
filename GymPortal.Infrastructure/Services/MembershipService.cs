using GymPortal.Application.Interfaces;
using GymPortal.Domain.Entities;
using GymPortal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymPortal.Infrastructure.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly GymDbContext _context;

        public MembershipService(GymDbContext context)
        {
            _context = context;
        }

        public async Task<Membership?> GetUserMembershipAsync(string userId)
        {
            return await _context.Memberships
                .FirstOrDefaultAsync(m => m.UserId == userId);
        }

        public async Task<bool> CreateMembershipAsync(string userId, string type)
        {
            var existing = await GetUserMembershipAsync(userId);

            if (existing != null)
            {
                return false;
            }

            var membership = new Membership
            {
                UserId = userId,
                Type = type,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddYears(1)
            };

            _context.Memberships.Add(membership);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}