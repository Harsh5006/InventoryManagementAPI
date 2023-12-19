using InventoryManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Business
{
    public interface IProductBusiness
    {
        bool AddProductToDepartment(Product product);
        Task<bool> DeleteProduct(int id);
        List<List<object>> GetAllEmployeeProducts(string employeeId);
        List<Product> GetAllProducts(int departmentId);
        Task<bool> Put(Product product);
    }
}