using InventoryManagementAPI.Core.Repositories;
using System;

namespace InventoryManagementAPI.Core
{
    public interface IUnitOfWork : IDisposable
    {
        public IProductRepository Products { get; }
        public IDepartmentRepository Departments { get; }
        public IRequestRepository Requests { get; }
        public IApplicationUserRepository ApplicationUsers { get; }

        public IAuthRepository AuthRepository { get; }

        int Complete();
    }
}
