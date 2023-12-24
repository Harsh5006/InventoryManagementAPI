using InventoryManagementAPI.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReturnProductController : ControllerBase
    {
        private readonly IReturnProductBusiness returnProductBusiness;
        private readonly UserManager<IdentityUser> userManager;

        public ReturnProductController(IReturnProductBusiness returnProductBusiness,UserManager<IdentityUser> userManager)
        {
            this.returnProductBusiness = returnProductBusiness;
            this.userManager = userManager;
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> ReturnProduct(int id)
        {
            var user = await userManager.GetUserAsync(User);
            var userId = await userManager.GetUserIdAsync(user);

            var success = await returnProductBusiness.ReturnProduct(userId, id);

            if (success)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
