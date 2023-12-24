using InventoryManagementAPI.Business.Interfaces;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Business
{
    public class ProductBusiness : IProductBusiness
    {
        private readonly IAppDbContext appDbContext;

        public ProductBusiness(IAppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<bool> AddProductToDepartment(Product product)
        {
            Department department = await appDbContext.Departments.FindAsync(product.DepartmentId);

            if (department == null || product == null)
            {
                return false;
            }
            var productInDb = appDbContext.Products.Where(x => x.DepartmentId == product.DepartmentId && x.Name == product.Name);
            if (productInDb.Any())
            {
                return false;
            }

            appDbContext.Products.Add(product);
            appDbContext.SaveChanges();

            return true;
        }


        public List<Product> GetAllProducts(int departmentId)
        {
            var products = appDbContext.Products.Where(x => x.DepartmentId == departmentId).ToList();
            return products;
        }
        public async Task<bool> DeleteProduct(int productId)
        {
            var product = await appDbContext.Products.FindAsync(productId);

            if (product == null)
            {
                return false;
            }

            var acceptedRequests = appDbContext.Requests.Where(x => x.ProductId == productId);
            if (acceptedRequests.Count() > 0)
            {
                return false;
            }

            appDbContext.Products.Remove(product);
            appDbContext.SaveChanges();


            return true;
        }

        public async Task<bool> Put(Product product)
        {
            var productInDb = await appDbContext.Products.FindAsync(product.Id);
            if (productInDb == null || productInDb.DepartmentId != product.DepartmentId)
            {
                return false;
            }

            if (product.Name == productInDb.Name)
            {
                productInDb.Quantity = product.Quantity;
                appDbContext.SaveChanges();
                return true;
            }
            else
            {
                var productWithSameName = appDbContext.Products.Where(x => x.DepartmentId == product.DepartmentId && x.Name == product.Name);
                if (productWithSameName.Any())
                {
                    return false;
                }
                productInDb.Name = product.Name;
                productInDb.Quantity = product.Quantity;
                appDbContext.SaveChanges();
                return true;
            }
        }

        public async Task<List<EmployeeProductDTO>> GetAllEmployeeProducts(string employeeId)
        {
            var requests = appDbContext.Requests.Where(x => x.UserId == employeeId).ToList();

            if(requests.Count == 0)
            {
                return null;
            }

            var productList = new List<Product>();

            foreach (var request in requests)
            {
                productList.Add(await appDbContext.Products.FindAsync(request.ProductId));
            }
            List<EmployeeProductDTO> allocatedProducts = new List<EmployeeProductDTO>();

            for (int i = 0; i < productList.Count; i++)
            {
                allocatedProducts.Add(new EmployeeProductDTO { ProductName = productList[i].Name,Quantity = requests[i].quantity });
            }

            return allocatedProducts;
        }
        
    }
}
