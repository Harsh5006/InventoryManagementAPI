using InventoryManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementAPI.tests.MockData
{
    public static class MockProducts
    {
        public static List<Product> listOfProducts = new List<Product>() { new Product() { DepartmentId = 1, Name = "Laptop", Quantity = 2, Id = 1 },
        new Product() { DepartmentId = 1, Name = "Screen", Quantity = 2, Id = 2 },new Product() { DepartmentId = 2, Name = "Printer", Quantity = 2, Id = 3 }};
    }
}
