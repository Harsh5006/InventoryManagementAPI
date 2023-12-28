using InventoryManagementAPI.Business.Interfaces;
using InventoryManagementAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestBusiness requestBusiness;
        private readonly UserManager<IdentityUser> userManager;

        public RequestsController(IRequestBusiness requestBusiness, UserManager<IdentityUser> userManager)
        {
            this.requestBusiness = requestBusiness;
            this.userManager = userManager;
        }

        [HttpGet("{userId?}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Get(string userId)
        {
            if (userId == null)
            {
                var user = await userManager.GetUserAsync(User);
                userId = await userManager.GetUserIdAsync(user);
                

                var list = await requestBusiness.GetAllEmployeeRequests(userId);

                if (list == null)
                {
                    return Ok("No Request are made by employee yet.");
                }
                return Ok(list);
            }
            else
            {
                var user = await userManager.GetUserAsync(User);
                var roles = await userManager.GetRolesAsync(user);
                if (!roles.Contains("Admin"))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }
                var list = requestBusiness.GetAllEmployeeRequests(userId);
                if (list == null)
                {
                    return Ok("No Request are made by employee yet.");
                }
                return Ok(list);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Post([FromBody] Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var user = await userManager.GetUserAsync(User);
                var userId = await userManager.GetUserIdAsync(user);


                var success = await requestBusiness.AddNewRequest(userId, request);

                if (success)
                {
                    return Ok("Request added successfully.");
                }
                return BadRequest();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                
            }
        }

        [HttpPatch]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Patch([FromBody] Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var user = await userManager.GetUserAsync(User);
                var userId = await userManager.GetUserIdAsync(user);

                request.UserId = userId;
                var success = await requestBusiness.PutRequest(request);

                if (success)
                {
                    return Ok("Request Updated successfully.");
                }
                return BadRequest();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
                
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var user = await userManager.GetUserAsync(User);
                var userId = await userManager.GetUserIdAsync(user);

                var success = await requestBusiness.DeleteRequest(userId, id);

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

        [HttpPatch]
        [Route("/reviewRequest/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ReviewRequest(int id,bool accept)
        {
            try
            {
                var success = await requestBusiness.ReviewRequest(id, accept);

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
