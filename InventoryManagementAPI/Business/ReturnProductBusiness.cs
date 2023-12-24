using InventoryManagementAPI.Business.Interfaces;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Business
{
    public class ReturnProductBusiness : IReturnProductBusiness
    {
        private readonly IAppDbContext appDbContext;

        public ReturnProductBusiness(IAppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<bool> ReturnProduct(string userId, int requestId)
        {
            var request = appDbContext.Requests.Find(requestId);
            if (request == null) return false;
            if (!request.RequestStatus.Equals("Accepted") || !request.UserId.Equals(userId)) return false;

            int productId = request.ProductId;
            var product = await appDbContext.Products.FindAsync(productId);
            if (product == null) return false;

            product.Quantity += request.quantity;
            request.RequestStatus = "Returned";
            appDbContext.SaveChanges();

            return true;
        }
    }
}
