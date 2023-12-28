using InventoryManagementAPI.Business.Interfaces;
using InventoryManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Business
{
    public class AdministrationBusiness : IAdministrationBusiness
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public AdministrationBusiness(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public async Task<bool> AddNewRole(CreateRoleDTO createRoleDTO)
        {
            IdentityRole role = new IdentityRole { Name = createRoleDTO.RoleName };


            var result = await roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }
    }
}
