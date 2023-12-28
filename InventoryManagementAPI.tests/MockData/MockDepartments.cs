using InventoryManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementAPI.tests.MockData
{
    public static class MockDepartments
    {
        public static IEnumerable<Department> listOfDepartments = new List<Department>()
        {
            new Department(){Id = 1,Name = "IT"},
            new Department(){Id = 2,Name = "Stationary"},
            new Department(){Id = 3,Name = "Sales" }
        };

    }
}
