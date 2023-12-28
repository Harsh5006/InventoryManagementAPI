using InventoryManagementAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Business.Interfaces
{
    public interface IAccountsBusiness
    {
        Task<SignInResult> Login(LoginDTO loginViewModel);
        Task<bool> Register(RegisterDTO registerViewModel, string role);
        Task<string> GetUserId(string emailAddress);
    }
}