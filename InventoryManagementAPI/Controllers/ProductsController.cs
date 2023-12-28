using InventoryManagementAPI.Business.Interfaces;
using InventoryManagementAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductBusiness productBusiness;
        private readonly IAccountsBusiness accountsBusiness;
        private readonly IReturnProductBusiness returnProductBusiness;

        public ProductsController(IProductBusiness productBusiness,IAccountsBusiness accountsBusiness,IReturnProductBusiness returnProductBusiness)
        {
            this.productBusiness = productBusiness;
            this.returnProductBusiness = returnProductBusiness;
            this.accountsBusiness = accountsBusiness;
        }
        

        [HttpGet]
        [Route("{departmentId}")]
        [Authorize(Roles ="Admin,Employee")]
        public IActionResult Get(int departmentId)
        {
            try
            {
                var list = productBusiness.GetAllProducts(departmentId);
                return Ok(list);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                bool success = await productBusiness.AddProductToDepartment(product);

                if (success == false) return BadRequest();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpDelete("{productId}")]
        [Authorize(Roles = "Admin")]
        public async  Task<IActionResult> Delete(int productId)
        {
            try
            {
                var success = await productBusiness.DeleteProduct(productId);

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

        [HttpPatch("{productId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Patch(int productId, [FromBody] Product product)
        {
            try
            {
                product.Id = productId;

                var success = await productBusiness.Put(product);

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
        [HttpGet]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GetEmployeeProducts()
        {
            try
            {
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
                var userId = await accountsBusiness.GetUserId(userEmail);


                var employeeProducts = productBusiness.GetAllEmployeeProducts(userId);

                if (employeeProducts == null)
                {
                    return Ok("No Products are allocated yet.");
                }
                return Ok(employeeProducts);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch]
        [Route("[action]/{id}")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> ReturnProduct(int id)
        {
            try
            {
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
                var userId = await accountsBusiness.GetUserId(userEmail);

                var success = await returnProductBusiness.ReturnProduct(userId, id);

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
