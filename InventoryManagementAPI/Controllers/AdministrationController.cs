using InventoryManagementAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace InventoryManagementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddRole([FromBody] CreateRoleDTO createRoleViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            IdentityRole role = new IdentityRole { Name = createRoleViewModel.RoleName };


            var result = await roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return StatusCode(StatusCodes.Status201Created);
            }

            return BadRequest();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var user = userManager.GetUserAsync(User);
            var id = userManager.GetUserIdAsync(user.Result);

            return Ok(id);
        }
    }
}
