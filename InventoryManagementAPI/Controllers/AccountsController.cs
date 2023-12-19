using InventoryManagementAPI.Business.Interfaces;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsBusiness accountsBusiness;

        public AccountsController(IAccountsBusiness accountsBusiness)
        {
            this.accountsBusiness = accountsBusiness;
        }


        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await accountsBusiness.Register(model,"Employee");


            if (result == true)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            else return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await accountsBusiness.Login(loginViewModel);

            if(result != null)
            {
                return Ok(result);
            }
            
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> AddAdmin([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await accountsBusiness.Register(model, "Admin");


            if (result == true)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            else return BadRequest();
        }
    }
}
