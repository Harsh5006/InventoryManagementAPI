using InventoryManagementAPI.Business.Interfaces;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
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

        //public bool AddProductToDepartment1(Product product)
        //{
        //    Department department = appDbContext.Departments.Include("Products").FirstOrDefault(x => x.Id == product.DepartmentId);
        //    if (department == null || product == null)
        //    {
        //        return false;
        //    }
        //    //var productInDb = appDbContext.Products.Where(x => x.DepartmentId == product.DepartmentId && x.Name == product.Name);
        //    //if (productInDb.Any())
        //    //{
        //    //    return false;
        //    //}

        //    appDbContext.Products.Add(product);
        //    department.Products.Add(product);
        //    appDbContext.SaveChanges();

        //    return true;
        //}

        public List<Product> GetAllProducts(int departmentId)
        {

            var products = appDbContext.Products.Where(x => x.DepartmentId == departmentId).ToList();

            return products;

            
        }


        public async Task<bool> DeleteProduct(int id)
        {
            var product = await appDbContext.Products.FindAsync(id);

            if (product == null)
            {
                return false;
            }

            var acceptedRequests = appDbContext.Requests.Where(x => x.ProductId == id);
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

        public List<List<Object>> GetAllEmployeeProducts(string employeeId)
        {
            var requests = appDbContext.Requests.Where(x => x.UserId == employeeId).ToList();

            if(requests.Count == 0)
            {
                return null;
            }

            var productList = new List<Product>();

            foreach (var request in requests)
            {
                productList.Add(appDbContext.Products.Find(request.ProductId));
            }
            List<List<Object>> list = new List<List<object>>();

            for (int i = 0; i < productList.Count; i++)
            {
                List<Object> innerList = new List<object> { productList[i].Name, requests[i].quantity };
                list.Add(innerList);
            }

            return list;
        }


    }
}
