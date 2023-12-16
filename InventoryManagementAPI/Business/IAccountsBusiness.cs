﻿using InventoryManagementAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Business
{
    public interface IAccountsBusiness
    {
        Task<SignInResult> Login(LoginViewModel loginViewModel);
        Task<bool> Register(RegisterViewModel registerViewModel, string role);
    }
}