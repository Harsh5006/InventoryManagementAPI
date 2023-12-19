using InventoryManagementAPI.Business.Interfaces;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Business
{
    public class EmployeeBusiness : IEmployeeBusiness
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IAppDbContext appDbContext;

        public EmployeeBusiness(UserManager<IdentityUser> userManager, IAppDbContext appDbContext)
        {
            this.userManager = userManager;
            this.appDbContext = appDbContext;
        }

        public async Task<object> GetAllEmployees()
        {
            var list = await userManager.GetUsersInRoleAsync("Employee");
            
            
            var result = list.Select(x => new EmployeeReturnModel { Id=x.Id,EmailAddress=x.UserName}).ToList();


            return result;
        }
        class ReturnEmployee
        {
            string Id;
            public string EmailAddress { get; set; }
        }

        public async Task<object> EmployeeDetails(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            var applicationUser = appDbContext.ApplicationUser.Find(id);
            var employeeRequests = appDbContext.Requests.Where(x => x.UserId == id).ToList();
            var employeeAcceptedRequests = employeeRequests.Where(x => x.RequestStatus == "Accepted").ToList();
            var employeeProductId = employeeAcceptedRequests.Select(x => x.ProductId).ToList();

            var employeeProducts = appDbContext.Products.Where(x => employeeProductId.Contains(x.Id)).ToList();

            return new { applicationUser.Name, Requests = employeeRequests, Products = employeeProducts };
        }

        public async Task<bool> DeleteEmployee(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            var applicationUser = appDbContext.ApplicationUser.Find(id);
            var employeeRequests = appDbContext.Requests.Where(x => x.UserId == id).ToList();
            var employeeAcceptedRequests = employeeRequests.Where(x => x.RequestStatus == "Accepted").ToList();

            if (employeeAcceptedRequests.Count > 0)
            {
                return false;
            }

            var result = await userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                appDbContext.ApplicationUser.Remove(applicationUser);
                var requestsId = employeeRequests.Select(x => x.Id).ToList();
                if(requestsId.Count == 0) { return true; }
                foreach(var requestId in requestsId)
                {
                    var request = await appDbContext.Requests.FindAsync(requestId);
                    appDbContext.Requests.Remove(request);
                }
                appDbContext.SaveChanges();
                return true;
            }
            return false;
        }



    }
}
