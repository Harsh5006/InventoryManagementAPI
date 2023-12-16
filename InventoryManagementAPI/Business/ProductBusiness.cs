using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Permissions;
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

        public bool AddProductToDepartment(Product product)
        {
            Department department = appDbContext.Departments.Find(product.DepartmentId);

            if (department == null || product ==  null)
            {
                return false;
            }

            product.DepartmentId = department.Id;


            appDbContext.Products.Add(product);
            appDbContext.SaveChanges();

            return true;
        }

        public List<Product> GetAllProducts(int departmentId)
        {
            var products = appDbContext.Products.Where(x => x.DepartmentId == departmentId).ToList();

            return products;
        }


        public async Task<bool> DeleteProduct(int id)
        {
            var product = await appDbContext.Products.FindAsync(id);

            if(product == null)
            {
                return false;
            }

            var acceptedRequests = appDbContext.Requests.Where(x => x.ProductId == id);
            if(acceptedRequests.Count() > 0)
            {
                return false;
            }

            appDbContext.Products.Remove(product);
            appDbContext.SaveChanges();


            return true;
        }

        public List<List<Object>> GetAllEmployeeProducts(ApplicationUser user)
        {
            var requests = appDbContext.Requests.Where(x => x.UserId == user.IdentityUserId).ToList();

            var productList = new List<Product>();

            foreach(var request in requests)
            {
                productList.Add(appDbContext.Products.Find(request.ProductId));
            }
            List<List<Object>> list = new List<List<object>>();

            for(int i = 0; i < productList.Count; i++)
            {
                List<Object> innerList = new List<object> { productList[i].Name, requests[i].quantity };
                list.Add(innerList);
            }

            return list;
        }


    }
}
