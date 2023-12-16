using InventoryManagementAPI.Models;
using System.Collections.Generic;

namespace InventoryManagementAPI.Business
{
    public interface IProductBusiness
    {
        bool AddProductToDepartment(Product product);
        List<Product> GetAllProducts(int departmentId);
    }
}