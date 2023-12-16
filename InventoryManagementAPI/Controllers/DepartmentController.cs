using InventoryManagementAPI.Business;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentBusiness departmentBusiness;

        public DepartmentController(IDepartmentBusiness departmentBusiness)
        {
            this.departmentBusiness = departmentBusiness;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(departmentBusiness.GetAllDepartments());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Put([FromBody] Department department)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var flag = departmentBusiness.AddNewDepartment(department);

            if (flag == true)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            else return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete(int departmentId)
        {
            return Ok();
        }

        [HttpPatch]
        public IActionResult Patch(int departmentId, [FromBody] Department department)
        {
            return Ok();
        }

        
    }
}
