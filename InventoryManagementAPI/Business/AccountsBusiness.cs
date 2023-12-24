using InventoryManagementAPI.Business.Interfaces;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Business
{
    public class AccountsBusiness : IAccountsBusiness
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IAppDbContext appDbContext;

        public AccountsBusiness(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IAppDbContext appDbContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.appDbContext = appDbContext;
        }

        public async Task<bool> Register(RegisterDTO registerViewModel, string role)
        {
            var identityUser = new IdentityUser { UserName = registerViewModel.Email, Email = registerViewModel.Email };
            var result1 = await userManager.CreateAsync(identityUser, registerViewModel.Password);
            var result2 = await userManager.AddToRoleAsync(identityUser, role);

            if (result1.Succeeded && result2.Succeeded)
            {
                ApplicationUser user = new ApplicationUser { IdentityUserId = identityUser.Id, Name = registerViewModel.UserName };
                appDbContext.ApplicationUser.Add(user);
                appDbContext.SaveChanges();

                return true;
            }

            return false;
        }

        public async Task<SignInResult> Login(LoginDTO loginViewModel)
        {
            var result = await signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, loginViewModel.RememberMe, false);



            if (result.Succeeded)
            {
                return result;
            }

            return null;
        }
    }
}
