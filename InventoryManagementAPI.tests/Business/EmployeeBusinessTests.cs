using InventoryManagementAPI.Business;
using InventoryManagementAPI.Core;
using InventoryManagementAPI.tests.MockData;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementAPI.tests.Business
{
    [TestClass]
    public class EmployeeBusinessTests
    {
        Mock<IUnitOfWork> unitOfWork;
        Mock<UserManager<IdentityUser>> userManager;
        public EmployeeBusinessTests()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            userManager = new Mock<UserManager<IdentityUser>>();
        }

        //[TestMethod]
        //public void GetAllEmployees_NoInput_ReturnsListOfEmployees()
        //{
        //    userManager.Setup(x => x.GetUsersInRoleAsync("Employee")).ReturnsAsync(MockEmployees.listOfEmployees);

        //    EmployeeBusiness employeeBusiness = new EmployeeBusiness(userManager.Object,unitOfWork.Object);

        //    var result = employeeBusiness.GetAllEmployees();
        //    Assert.AreEqual(result, MockEmployees.listOfEmployees);
        //}
    }
}
