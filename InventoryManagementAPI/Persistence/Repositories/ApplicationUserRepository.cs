using InventoryManagementAPI.Core.Repositories;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Persistence.Repositories
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(AppDbContext context) : base(context) { }

        public async Task<ApplicationUser> GetAsync(string id)
        {
            return await AppDbContext.ApplicationUser.FindAsync(id);
        }

        public AppDbContext AppDbContext
        {
            get { return Context as AppDbContext; }

        }
    }
}
