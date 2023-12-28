using InventoryManagementAPI.Business;
using InventoryManagementAPI.Core;
using InventoryManagementAPI.Core.Repositories;
using InventoryManagementAPI.Models;
using InventoryManagementAPI.tests.MockData;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementAPI.tests.Business
{
    [TestClass]
    public class DepartmentBusinessTests
    {
        Mock<IUnitOfWork> unitOfWork;
        public DepartmentBusinessTests()
        {
            unitOfWork = new Mock<IUnitOfWork>();
        }
        [TestMethod]
        public void GetAllDepartments_NoInput_ReturnsListOfDepartments()
        {
            unitOfWork.Setup(x => x.Departments.GetAll()).Returns(MockData.MockDepartments.listOfDepartments);
            DepartmentBusiness departmentBusiness = new DepartmentBusiness(unitOfWork.Object);

            var result = departmentBusiness.GetAllDepartments();

            Assert.AreEqual(MockData.MockDepartments.listOfDepartments, result);
        }

        [TestMethod]
        public void AddNewDepartment_ValidDepartment_ReturnsTrue()
        {
            unitOfWork.Setup(x => x.Departments.Add(It.IsAny<Department>()));
            unitOfWork.Setup(x => x.Complete()).Returns(1);

            DepartmentBusiness departmentBusiness = new DepartmentBusiness(unitOfWork.Object);

            var result = departmentBusiness.AddNewDepartment(MockData.MockDepartments.listOfDepartments.ElementAt(0));
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Patch_ValidDepartmentData_ReturnsTrue()
        {
            unitOfWork.Setup(x => x.Departments.GetAsync(It.IsAny<int>())).ReturnsAsync(MockDepartments.listOfDepartments.ElementAt(0));
            unitOfWork.Setup(x => x.Departments.SingleOrDefault(It.IsAny<Func<Department, bool>>())).Returns<Department>(null);

            unitOfWork.Setup(x => x.Complete()).Returns(1);

            DepartmentBusiness departmentBusiness = new DepartmentBusiness(unitOfWork.Object);
            var result = departmentBusiness.Patch(MockDepartments.listOfDepartments.ElementAt(0));
            Assert.IsTrue(result.Result);
        }

        [TestMethod]
        public void DeleteDepartment_ValidDepartmentId_ReturnsTrue()
        {
            unitOfWork.Setup(x => x.Departments.GetAsync(It.IsAny<int>())).ReturnsAsync(MockDepartments.listOfDepartments.ElementAt(0));
            unitOfWork.Setup(x => x.Products.Find(It.IsAny<Func<Product, bool>>())).Returns(new List<Product>());
            unitOfWork.Setup(x => x.Departments.Remove(It.IsAny<Department>()));
            unitOfWork.Setup(x => x.Complete()).Returns(1);

            DepartmentBusiness departmentBusiness = new DepartmentBusiness(unitOfWork.Object);
            var result = departmentBusiness.DeleteDepartment(1);

            Assert.IsTrue(result.Result);
        }

    }
}
