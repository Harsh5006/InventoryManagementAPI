using InventoryManagementAPI.Business.Interfaces;
using InventoryManagementAPI.Core;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;
using InventoryManagementAPI.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentBusiness departmentBusiness;

        public DepartmentsController(IDepartmentBusiness departmentBusiness)
        {
            this.departmentBusiness = departmentBusiness;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [AllowAnonymous]
        public IActionResult Get()
        {
            try
            {
                return Ok(departmentBusiness.GetAllDepartments());
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Authorize(Roles = "Admin")]
        public IActionResult Post([FromBody] Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try {
                var success = departmentBusiness.AddNewDepartment(department);

                if (success)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                else return BadRequest();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{departmentId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Delete(int departmentId)
        {
            try
            {
                var success = await departmentBusiness.DeleteDepartment(departmentId);

                if (success)
                {
                    return Ok("Department Successfully Deleted.");
                }
                return BadRequest();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }

        [HttpPatch]
        [Route("{departmentId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Patch(int departmentId, [FromBody] Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                department.Id = departmentId;
                var success = await departmentBusiness.Patch(department);

                if (success)
                {
                    return Ok("Update Successful");
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
