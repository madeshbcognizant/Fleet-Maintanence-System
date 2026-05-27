using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace UserService.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            _context = context;
        }
        
        public async Task<User?> GetUserByEmployeeIdAsync(string employeeId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.EmployeeId == employeeId);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> EmployeeIdExistsAsync(string employeeId)
        {
            return await _context.Users.AnyAsync(u => u.EmployeeId == employeeId);
        }

        public async Task UpdateLastLoginAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.LastLoginAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
    }
}
