using InventoryManagementAPI.Core.Repositories;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;

namespace InventoryManagementAPI.Persistence.Repositories
{
    public class DepartmentRepository : Repository<Department>,IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext context) : base(context) { }
    }
}
