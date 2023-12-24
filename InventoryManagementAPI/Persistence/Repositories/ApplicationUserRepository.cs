using InventoryManagementAPI.Core.Repositories;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;

namespace InventoryManagementAPI.Persistence.Repositories
{
    public class ApplicationUserRepository : Repository<ApplicationUser>,IApplicationUserRepository
    {
        public ApplicationUserRepository(AppDbContext context) : base(context) { }
    }

}
