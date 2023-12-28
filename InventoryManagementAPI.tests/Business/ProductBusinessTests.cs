using InventoryManagementAPI.Business;
using InventoryManagementAPI.Core;
using InventoryManagementAPI.Models;
using InventoryManagementAPI.tests.MockData;
using Moq;

namespace InventoryManagementAPI.tests.Business
{
    [TestClass]
    public class ProductBusinessTests
    {
        Mock<IUnitOfWork> unitOfWork;
        public ProductBusinessTests()
        {
            unitOfWork = new Mock<IUnitOfWork>();
        }
        [TestMethod]
        public void AddProductToDepartment_ValidProduct_ReturnsTrue()
        {
            unitOfWork.Setup(x => x.Departments.GetAsync(It.IsAny<int>())).ReturnsAsync(MockData.MockDepartments.listOfDepartments.ElementAt(0));
            unitOfWork.Setup(x => x.Products.Find(It.IsAny<Func<Product, bool>>())).Returns(new List<Product>());
            unitOfWork.Setup(x => x.Products.Add(It.IsAny<Product>()));
            unitOfWork.Setup(x => x.Complete());

            ProductBusiness productBusiness = new ProductBusiness(unitOfWork.Object);

            var result =  productBusiness.AddProductToDepartment(MockProducts.listOfProducts[0]);

            Assert.IsTrue(result.Result);
        }

        [TestMethod]
        public void GetAllProducts_ValidDepartmentId_ReturnsTrue()
        {
            unitOfWork.Setup(x => x.Products.Find(It.IsAny<Func<Product, bool>>())).Returns(MockProducts.listOfProducts);

            var expected = MockProducts.listOfProducts.Where(x => x.DepartmentId == 1).ToList();

            ProductBusiness productBusiness = new ProductBusiness(unitOfWork.Object);

            var actual = productBusiness.GetAllProducts(1);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteProduct_ValidProductId_ReturnsTrue()
        {
            unitOfWork.Setup(x => x.Products.GetAsync(It.IsAny<int>())).ReturnsAsync(MockProducts.listOfProducts[0]);
            unitOfWork.Setup(x => x.Requests.Find(It.IsAny<Func<Request, bool>>())).Returns(new List<Request>());
            unitOfWork.Setup(x => x.Products.Remove(It.IsAny<Product>()));


            ProductBusiness productBusiness = new ProductBusiness(unitOfWork.Object);
            var result = productBusiness.DeleteProduct(1);

            Assert.IsTrue(result.Result);
        }
    }
}
