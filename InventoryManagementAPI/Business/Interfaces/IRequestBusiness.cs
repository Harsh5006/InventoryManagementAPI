using InventoryManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Business.Interfaces
{
    public interface IRequestBusiness
    {
        Task<bool> AddNewRequest(string userId, Request request);
        Task<bool> DeleteRequest(string userId, int id);
        Task<List<List<object>>> GetAllEmployeeRequests(string userId);
        Task<bool> PutRequest(Request request);
        Task<bool> ReviewRequest(int requestId, bool accept);
    }
}