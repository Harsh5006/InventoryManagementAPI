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
        public IActionResult Post([FromBody] Department department)
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
        [Route("{departmentId}")]
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
        public IActionResult Patch(int departmentId, [FromBody] Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            department.Id = departmentId;
            var success = departmentBusiness.Patch(department);

            if (success)
            {
                return Ok("Update Successful");
            }
            return BadRequest();
        }

        
    }
}
