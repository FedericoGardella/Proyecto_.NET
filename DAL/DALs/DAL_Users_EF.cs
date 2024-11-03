using DAL.IDALs;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.DALs
{
    public class UsersDAL : IUsersDAL
    {
        private readonly UserManager<Users> _userManager;

        public UsersDAL(UserManager<Users> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Users> GetByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<List<Users>> GetAllAsync()
        {
            return _userManager.Users.ToList();
        }

        public async Task<bool> AddUserAsync(Users user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<bool> UpdateUserAsync(Users user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await GetByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<List<string>> GetUserRolesAsync(Users user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }
    }
}
