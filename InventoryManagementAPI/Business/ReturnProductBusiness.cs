using InventoryManagementAPI.Business.Interfaces;
using InventoryManagementAPI.Core;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Business
{
    public class ReturnProductBusiness : IReturnProductBusiness
    {
        private readonly IUnitOfWork unitOfWork;

        public ReturnProductBusiness(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<bool> ReturnProduct(string userId, int requestId)
        {
            var request = await unitOfWork.Requests.GetAsync(requestId);
            if (request == null) return false;
            if (!request.RequestStatus.Equals("Accepted") || !request.UserId.Equals(userId)) return false;

            int productId = request.ProductId;
            var product = await unitOfWork.Products.GetAsync(productId);
            if (product == null) return false;

            product.Quantity += request.quantity;
            request.RequestStatus = "Returned";
            unitOfWork.Complete();

            return true;
        }
    }
}
