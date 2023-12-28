using InventoryManagementAPI.Business.Interfaces;
using InventoryManagementAPI.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementAPI.tests.Controller.Tests
{
    [TestClass]
    public class ProductsControllerTests
    {
        Mock<IReturnProductBusiness> returnProductBusiness;
        Mock<IProductBusiness> productBusiness;
        Mock<IAccountsBusiness> accountsBusiness;
        private ProductsController productsController;

        [TestInitialize]
        public void InitializeController()
        {
            returnProductBusiness = new Mock<IReturnProductBusiness>();
            productBusiness = new Mock<IProductBusiness>();
            accountsBusiness = new Mock<IAccountsBusiness>();
            productsController = new ProductsController(productBusiness.Object, accountsBusiness.Object,returnProductBusiness.Object);
        }
        [TestMethod]
        public void Get_ValidDepartmentId_ReturnProductsList()
        {
            //productBusiness.Setup(x => x.GetAllProducts(It.IsAny<int>())).Returns();

        }
    }
}
