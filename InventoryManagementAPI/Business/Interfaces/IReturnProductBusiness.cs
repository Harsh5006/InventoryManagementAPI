using System.Threading.Tasks;

namespace InventoryManagementAPI.Business.Interfaces
{
    public interface IReturnProductBusiness
    {
        Task<bool> ReturnProduct(string userId, int requestId);
    }
}