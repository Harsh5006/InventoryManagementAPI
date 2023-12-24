using InventoryManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Business.Interfaces
{
    public interface IProductBusiness
    {
        Task<bool> AddProductToDepartment(Product product);
        Task<bool> DeleteProduct(int productId);
        Task<List<EmployeeProductDTO>> GetAllEmployeeProducts(string employeeId);
        List<Product> GetAllProducts(int departmentId);
        Task<bool> Put(Product product);
    }
}