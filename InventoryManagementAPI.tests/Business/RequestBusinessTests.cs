using InventoryManagementAPI.Core;
using Moq;

namespace InventoryManagementAPI.tests.Business
{
    [TestClass]
    public class RequestBusinessTests
    {
        Mock<IUnitOfWork> unitOfWork;
        public RequestBusinessTests()
        {
            unitOfWork = new Mock<IUnitOfWork>();
        }


    }
}
