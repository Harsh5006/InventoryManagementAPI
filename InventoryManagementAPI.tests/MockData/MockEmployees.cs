using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementAPI.tests.MockData
{
    public class MockEmployees
    {
        public static List<IdentityUser> listOfEmployees = new List<IdentityUser>()
        {
            new IdentityUser{Id = "1",UserName = "harshbhargava@gmail.com"},
            new IdentityUser{Id = "2",UserName = "ayush@gmail.com"}
        };
    }
}
