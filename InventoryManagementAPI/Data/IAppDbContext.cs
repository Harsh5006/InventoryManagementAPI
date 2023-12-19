using InventoryManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Data
{
    public interface IAppDbContext
    {
        DbSet<ApplicationUser> ApplicationUser { get; set; }
        DbSet<Department> Departments { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Request> Requests { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}