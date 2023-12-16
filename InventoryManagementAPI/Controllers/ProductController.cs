using InventoryManagementAPI.Business;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;

namespace InventoryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductBusiness productBusiness;

        public ProductController(IProductBusiness productBusiness)
        {
            this.productBusiness = productBusiness;
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
        //[Authorize(Roles = "Admin")]
        public IActionResult Put([FromBody] Product product)
        {
            if (!ModelState.IsValid) return BadRequest();

            bool success = productBusiness.AddProductToDepartment(product);

            if (success == false) return BadRequest();
            return StatusCode(StatusCodes.Status201Created);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int productId)
        {
            return Ok();
        }

        [HttpPut]
        public IActionResult Patch(string productId, [FromBody] Product product)
        {
            return Ok();
        }
    }
}
