using InventoryManagementAPI.Business.Interfaces;
using InventoryManagementAPI.Core;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Business
{
    public class ProductBusiness : IProductBusiness
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductBusiness(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> AddProductToDepartment(Product product)
        {
            if (product == null) return false;
            Department department = await unitOfWork.Departments.GetAsync(product.DepartmentId);

            if (department == null)
            {
                return false;
            }
            var productInDb = unitOfWork.Products.Find(x => x.DepartmentId == product.DepartmentId && x.Name == product.Name);
            if (productInDb.Any())
            {
                return false;
            }

            unitOfWork.Products.Add(product);
            unitOfWork.Complete();

            return true;
        }


        public List<Product> GetAllProducts(int departmentId)
        {
            var products = unitOfWork.Products.Find(x => x.DepartmentId == departmentId).ToList();
            return products;
        }
        public async Task<bool> DeleteProduct(int productId)
        {
            var product = await unitOfWork.Products.GetAsync(productId);

            if (product == null)
            {
                return false;
            }

            var acceptedRequests = unitOfWork.Requests.Find(x => x.ProductId == productId);
            if (acceptedRequests.Count() > 0)
            {
                return false;
            }

            unitOfWork.Products.Remove(product);
            unitOfWork.Complete();


            return true;
        }

        public async Task<bool> Put(Product product)
        {
            var productInDb = await unitOfWork.Products.GetAsync(product.Id);
            if (productInDb == null || productInDb.DepartmentId != product.DepartmentId)
            {
                return false;
            }

            if (product.Name == productInDb.Name)
            {
                productInDb.Quantity = product.Quantity;
                unitOfWork.Complete();
                return true;
            }
            else
            {
                var productWithSameName = unitOfWork.Products.Find(x => x.DepartmentId == product.DepartmentId && x.Name == product.Name);
                if (productWithSameName.Any())
                {
                    return false;
                }
                productInDb.Name = product.Name;
                productInDb.Quantity = product.Quantity;
                unitOfWork.Complete();
                return true;
            }
        }

        public async Task<List<EmployeeProductDTO>> GetAllEmployeeProducts(string employeeId)
        {
            var requests = unitOfWork.Requests.Find(x => x.UserId == employeeId).ToList();

            if(requests.Count == 0)
            {
                return null;
            }

            var productList = new List<Product>();

            foreach (var request in requests)
            {
                productList.Add(await unitOfWork.Products.GetAsync(request.ProductId));
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
