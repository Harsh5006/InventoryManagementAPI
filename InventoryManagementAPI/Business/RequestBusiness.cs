using InventoryManagementAPI.Business.Interfaces;
using InventoryManagementAPI.Core;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Business
{
    public class RequestBusiness : IRequestBusiness
    {
        private readonly IUnitOfWork unitOfWork;

        public RequestBusiness(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<RequestDTO>> GetAllEmployeeRequests(string userId)
        {

            var applicationUser = await unitOfWork.ApplicationUsers.GetAsync(userId);
            if (applicationUser == null)
            {
                return null;
            }
            var requests = unitOfWork.Requests.Find(x => x.UserId == applicationUser.IdentityUserId).ToList();
            var products = new List<Product>();

            foreach (var request in requests)
            {
                products.Add(await unitOfWork.Products.GetAsync(request.ProductId));
            }

            List<RequestDTO> employeeRequests = new List<RequestDTO>();
            for (int i = 0; i < products.Count; i++)
            {

                employeeRequests.Add(new RequestDTO { RequestId = requests[i].Id, ProductName = products[i].Name, ProductQuantity = requests[i].quantity, RequestStatus = requests[i].RequestStatus });
            }

            return employeeRequests;
        }



        public async Task<bool> AddNewRequest(string userId, Request request)
        {
            var applicationUser = await unitOfWork.ApplicationUsers.GetAsync(userId);
            if (request == null || applicationUser == null)
            {
                return false;
            }

            var product = await unitOfWork.Products.GetAsync(request.ProductId);
            if (product == null || product.Quantity < request.quantity)
            {
                return false;
            }

            request.RequestStatus = "Unaddressed";
            request.UserId = applicationUser.IdentityUserId;

            unitOfWork.Requests.Add(request);
            unitOfWork.Complete();
            return true;
        }


        public async Task<bool> PutRequest(Request request)
        {
            var applicationUser = await unitOfWork.ApplicationUsers.GetAsync(request.UserId);
            if (applicationUser == null)
            {
                return false;
            }

            var requestInDb = await unitOfWork.Requests.GetAsync(request.Id);

            if (requestInDb == null || request.RequestStatus != "Unaddressed" || request.UserId != applicationUser.IdentityUserId)
            {
                return false;
            }

            var product = await unitOfWork.Products.GetAsync(request.ProductId);
            if (product == null || product.Quantity < request.quantity)
            {
                return false;
            }
            requestInDb.ProductId = request.ProductId;
            requestInDb.quantity = request.quantity;
            unitOfWork.Complete();

            return true;
        }

        public async Task<bool> DeleteRequest(string userId, int requestId)
        {
            var applicationUser = await unitOfWork.ApplicationUsers.GetAsync(userId);
            if (applicationUser == null)
            {
                return false;
            }

            var request = await unitOfWork.Requests.GetAsync(requestId);

            if (request == null || request.UserId != applicationUser.IdentityUserId || request.RequestStatus != "Unaddressed")
            {
                return false;
            }

            unitOfWork.Requests.Remove(request);
            unitOfWork.Complete();
            return true;
        }

        public async Task<bool> ReviewRequest(int requestId, bool accept)
        {
            var request = await unitOfWork.Requests.GetAsync(requestId);
            if (request == null || request.RequestStatus != "Unaddressed")
            {
                return false;
            }
            if (accept == false)
            {
                request.RequestStatus = "Rejected";
                unitOfWork.Complete();
                return true;
            }
            else
            {
                var product = await unitOfWork.Products.GetAsync(request.ProductId);
                if (product == null || product.Quantity < request.quantity)
                {
                    return false;
                }
                request.RequestStatus = "Accepted";
                product.Quantity -= request.quantity;
                unitOfWork.Complete();
                return true;
            }
        }
    }
}
