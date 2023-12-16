using InventoryManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementAPI.Data
{
    public interface IAppDbContext
    {
        DbSet<Department> Departments { get; set; }
        DbSet<Product> Products { get; set; }

        DbSet<Request> Requests { get; set; }

        DbSet<ApplicationUser> ApplicationUser { get; set; }

        int SaveChanges();
    }
}