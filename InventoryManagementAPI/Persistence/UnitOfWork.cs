using InventoryManagementAPI.Core;
using InventoryManagementAPI.Core.Repositories;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Persistence.Repositories;

namespace InventoryManagementAPI.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbContext;
        public UnitOfWork(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            ProductRepository = new ProductRepository(this.dbContext);
            DepartmentRepository = new DepartmentRepository(this.dbContext);
            RequestRepository = new RequestRepository(this.dbContext);
            ApplicationUserRepository = new ApplicationUserRepository(this.dbContext);
        }

        public IProductRepository ProductRepository { get; private set; }

        public IDepartmentRepository DepartmentRepository { get; private set; }

        public IRequestRepository RequestRepository {  get; private set; }

        public IApplicationUserRepository ApplicationUserRepository { get; private set; }

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
