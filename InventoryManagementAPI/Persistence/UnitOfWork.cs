using InventoryManagementAPI.Core;
using InventoryManagementAPI.Core.Repositories;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;

namespace InventoryManagementAPI.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public UnitOfWork(AppDbContext dbContext,UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.signInManager = signInManager;
            Products = new ProductRepository(this.dbContext);
            Departments = new DepartmentRepository(this.dbContext);
            Requests = new RequestRepository(this.dbContext);
            ApplicationUsers = new ApplicationUserRepository(this.dbContext);
            AuthRepository = new AuthRepository(userManager,signInManager);
        }

        public IProductRepository Products { get; private set; }

        public IDepartmentRepository Departments { get; private set; }

        public IRequestRepository Requests {  get; private set; }

        public IApplicationUserRepository ApplicationUsers { get; private set; }

        public IAuthRepository AuthRepository { get; private set; }

        public int Complete()
        {
            return dbContext.SaveChanges();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
