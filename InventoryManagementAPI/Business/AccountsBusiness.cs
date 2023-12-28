using InventoryManagementAPI.Business.Interfaces;
using InventoryManagementAPI.Core;
using InventoryManagementAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Business
{
    public class AccountsBusiness : IAccountsBusiness
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAuthRepository authRepository;

        public AccountsBusiness(IUnitOfWork unitOfWork,IAuthRepository authRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authRepository = authRepository;
        }

        public async Task<bool> Register(RegisterDTO registerViewModel, string role)
        {
            var identityUser = new IdentityUser { UserName = registerViewModel.Email, Email = registerViewModel.Email };
            var result1 = await authRepository.CreateNewUser(identityUser, registerViewModel.Password);
            var result2 = await authRepository.AddRoleToUser(identityUser, role);

            if (result1.Succeeded && result2.Succeeded)
            {
                ApplicationUser user = new ApplicationUser { IdentityUserId = identityUser.Id, Name = registerViewModel.UserName };
                unitOfWork.ApplicationUsers.Add(user);
                unitOfWork.Complete();

                return true;
            }

            return false;
        }

        public async Task<SignInResult> Login(LoginDTO loginViewModel)
        {
            var result = await authRepository.SignIn(loginViewModel);

            if (result.Succeeded)
            {
                return result;
            }

            return null;
        }

        public async Task<string> GetUserId(string emailAddress)
        {
            return await authRepository.GetUserId(emailAddress);
        }
    }
}
