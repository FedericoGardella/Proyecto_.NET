using DAL.Models;

namespace DAL.IDALs
{
    public interface IUsersDAL
    {
        Task<Users> GetByIdAsync(string userId);
        Task<List<Users>> GetAllAsync();
        Task<bool> AddUserAsync(Users user, string password);
        Task<bool> UpdateUserAsync(Users user);
        Task<bool> DeleteUserAsync(string userId);
        Task<List<string>> GetUserRolesAsync(Users user);
    }
}
