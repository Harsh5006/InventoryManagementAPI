using InventoryManagementAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Data
{
    public class AppDbContext : IdentityDbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

    }
}
