using InventoryManagementAPI.Business.Interfaces;
using InventoryManagementAPI.Core;
using InventoryManagementAPI.Data;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Business
{
    public class EmployeeBusiness : IEmployeeBusiness
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IUnitOfWork unitOfWork;

        public EmployeeBusiness(UserManager<IdentityUser> userManager,IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<EmployeeDTO>> GetAllEmployees()
        {
            var list = await userManager.GetUsersInRoleAsync("Employee");
            
            
            var result = list.Select(x => new EmployeeDTO { Id = x.Id,EmailAddress = x.UserName}).ToList();


            return result;
        }

        public async Task<EmployeeDetailsDTO> EmployeeDetails(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            var applicationUser = await unitOfWork.ApplicationUsers.GetAsync(id);
            var employeeRequests = unitOfWork.Requests.Find(x => x.UserId == id).ToList();
            var employeeAcceptedRequests = employeeRequests.Where(x => x.RequestStatus == "Accepted").ToList();
            var employeeProductId = employeeAcceptedRequests.Select(x => x.ProductId).ToList();

            var employeeProducts = unitOfWork.Products.Find(x => employeeProductId.Contains(x.Id)).ToList();

            return new EmployeeDetailsDTO { Name = applicationUser.Name, Requests = employeeRequests, Products = employeeProducts };
        }

        public async Task<bool> DeleteEmployee(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            var applicationUser = await unitOfWork.ApplicationUsers.GetAsync(id);
            var employeeRequests = unitOfWork.Requests.Find(x => x.UserId == id).ToList();
            var employeeAcceptedRequests = employeeRequests.Where(x => x.RequestStatus == "Accepted").ToList();

            if (employeeAcceptedRequests.Count > 0)
            {
                return false;
            }

            var result = await userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                unitOfWork.ApplicationUsers.Remove(applicationUser);
                var requestsId = employeeRequests.Select(x => x.Id).ToList();
                if(requestsId.Count == 0) { return true; }
                foreach(var requestId in requestsId)
                {
                    var request = await unitOfWork.Requests.GetAsync(requestId);
                    unitOfWork.Requests.Remove(request);
                }
                unitOfWork.Complete();
                return true;
            }
            return false;
        }



    }
}
