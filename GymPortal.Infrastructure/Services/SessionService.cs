using GymPortal.Application.Interfaces;
using GymPortal.Domain.Entities;
using GymPortal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymPortal.Infrastructure.Services
{
    public class SessionService : ISessionService
    {
        private readonly GymDbContext _context;

        public SessionService(GymDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Session>> GetAllSessionsAsync()
        {
            return await _context.Sessions.ToListAsync();
        }

        public async Task<Session?> GetSessionByIdAsync(int id)
        {
            return await _context.Sessions.FindAsync(id);
        }
    }
}