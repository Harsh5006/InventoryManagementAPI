using InventoryManagementAPI.Business.Interfaces;
using InventoryManagementAPI.Controllers;
using InventoryManagementAPI.Models;
using InventoryManagementAPI.tests.MockData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.EventHandlers;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementAPI.tests.Controller.Tests
{
    [TestClass]
    public class DepartmentsControllerTests
    {
        Mock<IDepartmentBusiness> departmentBusiness;
        public DepartmentsControllerTests()
        {
            departmentBusiness = new Mock<IDepartmentBusiness>();
        }
        [TestMethod]
        public void Post_AddValidDepartment_Return201StatusCode()
        {
            var userClaims = new Claim[] { new Claim(ClaimTypes.Name, "testUser"), new Claim(ClaimTypes.NameIdentifier, "aaayyu") };
            var user = new ClaimsPrincipal(new ClaimsIdentity(userClaims));

            departmentBusiness.Setup(x => x.AddNewDepartment(It.IsAny<Department>())).Returns(true);
            var controller = new DepartmentsController(departmentBusiness.Object);

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
            var result = controller.Post(new Department() { Id = 1, Name = "IT" }) as StatusCodeResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status201Created, result.StatusCode);
        }
        //[TestMethod]
        //public void Post_ReturnsBadRequest_GivenInvalidModel()
        //{
        //    departmentBusiness.Setup(x => x.AddNewDepartment(It.IsAny<Department>()));
        //    var controller = new DepartmentsController(departmentBusiness.Object);

        //    controller.ModelState.AddModelError("error", "some error");
        //    var result = controller.Post(new Department() { Id = 1, Name = "IT"}) as StatusCodeResult;

        //    Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
        //}

        //[TestMethod]
        //public void Post_InvalidDepartment_Returns400StatusCode()
        //{
        //    var userClaims = new Claim[] { new Claim(ClaimTypes.Name, "testUser"), new Claim(ClaimTypes.NameIdentifier, "aaayyu") };
        //    var user = new ClaimsPrincipal(new ClaimsIdentity(userClaims));

        //    departmentBusiness.Setup(x => x.AddNewDepartment(It.IsAny<Department>())).Returns(true);
        //    var controller = new DepartmentsController(departmentBusiness.Object);

        //    controller.ControllerContext = new ControllerContext()
        //    {
        //        HttpContext = new DefaultHttpContext { User = user, }
        //    };
           
        //    var result = controller.Post() as StatusCodeResult;
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
        //}

        [TestMethod]
        public void Delete_DeleteValidDepartment_Returns200StatusCode()
        {
            var userClaims = new Claim[] { new Claim(ClaimTypes.Name, "testUser"), new Claim(ClaimTypes.NameIdentifier, "aaayyu") };
            var user = new ClaimsPrincipal(new ClaimsIdentity(userClaims));

            departmentBusiness.Setup(x => x.DeleteDepartment(It.IsAny<int>())).ReturnsAsync(true);
            var controller = new DepartmentsController(departmentBusiness.Object);
            
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
            var result =  controller.Delete(1).Result as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }
        [TestMethod]
        public void Patch_ModifyValidDeparment_Returns200StatusCode()
        {
            var userClaims = new Claim[] { new Claim(ClaimTypes.Name, "testUser"), new Claim(ClaimTypes.NameIdentifier, "aaayyu") };
            var user = new ClaimsPrincipal(new ClaimsIdentity(userClaims));

            departmentBusiness.Setup(x => x.DeleteDepartment(It.IsAny<int>())).ReturnsAsync(true);
            var controller = new DepartmentsController(departmentBusiness.Object);

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
            var result = controller.Delete(1).Result as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }

    }
}
