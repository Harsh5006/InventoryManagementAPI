using InventoryManagementAPI.Business;
using InventoryManagementAPI.Business.Interfaces;
using InventoryManagementAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


namespace InventoryManagementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class AdministrationController : ControllerBase
    {
        private readonly IAdministrationBusiness administrationBusiness;

        public AdministrationController(IAdministrationBusiness administrationBusiness)
        {
            this.administrationBusiness = administrationBusiness;
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddRole([FromBody] CreateRoleDTO createRoleViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                if (await administrationBusiness.AddNewRole(createRoleViewModel))
                {
                    return StatusCode(StatusCodes.Status201Created);
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
