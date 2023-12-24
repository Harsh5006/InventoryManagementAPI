using InventoryManagementAPI.Models;
using System.Collections.Generic;

namespace InventoryManagementAPI.Business
{
    
        public class EmployeeDetailsDTO
        {
            public string Name { get; set; }
            public List<Request> Requests { get; set; }
            public List<Product> Products { get; set; }
        }

}
