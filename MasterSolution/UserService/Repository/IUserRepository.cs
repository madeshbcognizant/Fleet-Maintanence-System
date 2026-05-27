using UserService.Models;

namespace UserService.Repository
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmployeeIdAsync(string employeeId);
        Task<User> CreateUserAsync(User user);
        Task<bool> EmployeeIdExistsAsync(string employeeId);
        Task UpdateLastLoginAsync(int userId);
    }
}
