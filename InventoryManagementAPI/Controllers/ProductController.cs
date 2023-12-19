using InventoryManagementAPI.Business;
using InventoryManagementAPI.Business.Interfaces;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductBusiness productBusiness;
        private readonly UserManager<IdentityUser> userManager;

        public ProductController(IProductBusiness productBusiness,UserManager<IdentityUser> userManager)
        {
            this.productBusiness = productBusiness;
            this.userManager = userManager;
        }
        

        [HttpGet]
        [Route("{departmentId}")]
        public IActionResult Get(int departmentId)
        {
            var list = productBusiness.GetAllProducts(departmentId);
            if(list.Count == 0)
            {
                return Ok("No product are present in given department");
            }
            return Ok(list);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            if (!ModelState.IsValid) return BadRequest();

            bool success = productBusiness.AddProductToDepartment(product);

            if (success == false) return BadRequest();
            return StatusCode(StatusCodes.Status201Created);
        }


        [HttpDelete("{productId}")]
        public async  Task<IActionResult> Delete(int productId)
        {
            var success = await productBusiness.DeleteProduct(productId);

            if(success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPatch("{productId}")]
        public async Task<IActionResult> Patch(int productId, [FromBody] Product product)
        {
            product.Id = productId;

            var success = await productBusiness.Put(product);

            if (success)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployeeProducts()
        {
            var user = await userManager.GetUserAsync(User);
            var userId = await userManager.GetUserIdAsync(user);

            var employeeProducts = productBusiness.GetAllEmployeeProducts(userId);

            if(employeeProducts == null)
            {
                return Ok("No Products are allocated yet.");
            }
            return Ok(employeeProducts);
        }


    }
}
