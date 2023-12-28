using InventoryManagementAPI.Models;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Core.Repositories
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser> GetAsync(string id);
    }
}
