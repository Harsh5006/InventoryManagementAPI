using InventoryManagementAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Core
{
    public interface IAuthRepository
    {
        Task<IdentityResult> CreateNewUser(IdentityUser user,string password);
        Task<IdentityResult> AddRoleToUser(IdentityUser user,string role);
        Task<SignInResult> SignIn(LoginDTO loginModel);
        Task<IdentityResult> DeleteUser(IdentityUser user);
        Task<string> GetUserId(string emailAddress);
    }
}
