using InventoryManagementAPI.Core;
using InventoryManagementAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Persistence.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AuthRepository(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public async Task<IdentityResult> AddRoleToUser(IdentityUser user, string role)
        {
            return await userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> CreateNewUser(IdentityUser user, string password)
        {
            return await userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> DeleteUser(IdentityUser user)
        {
            return await userManager.DeleteAsync(user);
        }

        public async Task<SignInResult> SignIn(LoginDTO loginModel)
        {
            return await signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, loginModel.RememberMe, false);
        }

        public async Task<string> GetUserId(string emailAddress)
        {
            var User = await userManager.FindByEmailAsync(emailAddress);
            var userId = await userManager.GetUserIdAsync(User);

            return userId;
        }
    }
}
