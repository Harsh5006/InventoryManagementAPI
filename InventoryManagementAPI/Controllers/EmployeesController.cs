using InventoryManagementAPI.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeBusiness employeeBusiness;
        private readonly UserManager<IdentityUser> userManager;

        public EmployeesController(IEmployeeBusiness employeeBusiness, UserManager<IdentityUser> userManager)
        {
            this.employeeBusiness = employeeBusiness;
            this.userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await employeeBusiness.GetAllEmployees());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetDetails(string id)
        {
            try
            {
                if (id == null)
                {
                    var user = await userManager.GetUserAsync(User);
                    var userId = await userManager.GetUserIdAsync(user);
                    var roles = await userManager.GetRolesAsync(user);
                    if (!roles.Contains("Employee"))
                    {
                        return BadRequest("You need to login as Employee to view allocated products.");
                    }

                    return Ok(await employeeBusiness.EmployeeDetails(userId));
                }
                return Ok(await employeeBusiness.EmployeeDetails(id));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var success = await employeeBusiness.DeleteEmployee(id);

                if (success)
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }
    }
}
