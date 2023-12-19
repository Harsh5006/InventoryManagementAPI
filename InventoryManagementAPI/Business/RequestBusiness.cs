using InventoryManagementAPI.Business.Interfaces;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Business
{
    public class RequestBusiness : IRequestBusiness
    {
        private readonly IAppDbContext appDbContext;

        public RequestBusiness(IAppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<List<List<object>>> GetAllEmployeeRequests(string userId)
        {

            var applicationUser = await appDbContext.ApplicationUser.FindAsync(userId);
            if(applicationUser == null)
            {
                return null;
            }
            var requests = appDbContext.Requests.Where(x => x.UserId == applicationUser.IdentityUserId).ToList();
            var products = new List<Product>();

            foreach (var request in requests)
            {
                products.Add(await appDbContext.Products.FindAsync(request.ProductId));
            }

            List<List<object>> list = new List<List<object>>();
            for (int i = 0; i < products.Count; i++)
            {
                List<object> innerList = new List<object> { requests[i].Id, products[i].Name, requests[i].quantity };
                list.Add(innerList);
            }

            return list;
        }

        public async Task<bool> AddNewRequest(string userId, Request request)
        {
            var applicationUser = await appDbContext.ApplicationUser.FindAsync(userId);
            if (request == null || applicationUser == null)
            {
                return false;
            }

            var product = await appDbContext.Products.FindAsync(request.ProductId);
            if (product == null)
            {
                return false;
            }

            request.RequestStatus = "Unaddressed";
            request.UserId = applicationUser.IdentityUserId;

            appDbContext.Requests.Add(request);
            appDbContext.SaveChanges();
            return true;
        }


        public async Task<bool> PutRequest( Request request)
        {
            var applicationUser = await appDbContext.ApplicationUser.FindAsync(request.UserId);
            if(applicationUser == null)
            {
                return false;
            }

            var requestInDb = await appDbContext.Requests.FindAsync(request.Id);

            if (requestInDb == null || request.RequestStatus != "Unaddressed" || request.UserId != applicationUser.IdentityUserId)
            {
                return false;
            }

            var product = await appDbContext.Products.FindAsync(request.ProductId);
            if (product == null)
            {
                return false;
            }
            requestInDb.ProductId = request.ProductId;
            requestInDb.quantity = request.quantity;
            appDbContext.SaveChanges();

            return true;
        }

        public async Task<bool> DeleteRequest(string userId, int id)
        {
            var applicationUser = await appDbContext.ApplicationUser.FindAsync(userId);
            if (applicationUser == null)
            {
                return false;
            }

            var request = await appDbContext.Requests.FindAsync(id);

            if (request == null || request.UserId != applicationUser.IdentityUserId || request.RequestStatus != "Unaddressed")
            {
                return false;
            }

            appDbContext.Requests.Remove(request);
            appDbContext.SaveChanges();
            return true;
        }

        public async Task<bool> ReviewRequest(int requestId, bool accept)
        {
            var request = await appDbContext.Requests.FindAsync(requestId);
            if (request == null || request.RequestStatus != "Unaddressed")
            {
                return false;
            }
            if (accept == false)
            {
                request.RequestStatus = "Rejected";
                appDbContext.SaveChanges();
                return true;
            }
            else
            {
                var product = await appDbContext.Products.FindAsync(request.ProductId);
                if (product == null || product.Quantity < request.quantity)
                {
                    return false;
                }
                request.RequestStatus = "Accepted";
                product.Quantity -= request.quantity;
                appDbContext.SaveChanges();
                return true;
            }
        }
    }
}
