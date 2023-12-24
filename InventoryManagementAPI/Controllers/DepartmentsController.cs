using InventoryManagementAPI.Business.Interfaces;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            
            return Ok(departmentBusiness.GetAllDepartments());
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] Department department)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var success = departmentBusiness.AddNewDepartment(department);

            if (success)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            else return BadRequest();
        }

        [HttpDelete("{departmentId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Delete(int departmentId)
        {
            var success = await departmentBusiness.DeleteDepartment(departmentId);

            if (success)
            {
                return Ok("Department Successfully Deleted.");
            }
            return BadRequest();
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
            department.Id = departmentId;
            var success = await departmentBusiness.Patch(department);

            if (success)
            {
                return Ok("Update Successful");
            }
            return BadRequest();
        }
    }
}
