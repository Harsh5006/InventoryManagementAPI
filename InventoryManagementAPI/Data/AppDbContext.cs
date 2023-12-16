using InventoryManagementAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System.Reflection.Metadata;

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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //base.OnConfiguring(optionsBuilder);
            
        //    optionsBuilder.UseSqlServer(@"Data Source = (localDb)\ProjectModels;Initial Catalog=InventoryManagementDatabase");
        //}
        
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

    }
}
