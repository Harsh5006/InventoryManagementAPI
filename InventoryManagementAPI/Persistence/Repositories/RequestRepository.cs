using InventoryManagementAPI.Core.Repositories;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;

namespace InventoryManagementAPI.Persistence.Repositories
{
    public class RequestRepository : Repository<Request>,IRequestRepository
    {
        public RequestRepository(AppDbContext context) : base(context) { }
    }
}
