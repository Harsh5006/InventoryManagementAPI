using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;
using Microsoft.AspNetCore.Builder;
using System.Security.Cryptography.X509Certificates;

namespace InventoryManagementAPI.Business
{
    public class RequestBusiness
    {
        private readonly IAppDbContext appDbContext;

        public RequestBusiness(IAppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public void GetAllEmployeeRequests(ApplicationUser applicationUser)
        {

        }

        public void AddNewRequest(ApplicationUser applicationUser,Request request)
        {

        }


        public void PutRequest(ApplicationUser applicationUser,Request request)
        {

        }

        public void DeleteRequest(ApplicationUser applicationUser,int id)
        {

        }
    }
}
