using InventoryManagementAPI.Core.Repositories;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;

namespace InventoryManagementAPI.Persistence.Repositories
{
    public class ProductRepository : Repository<Product>,IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {

        }
    }
}
