using InventoryManagementAPI.Models;
using System.Linq;

namespace InventoryManagementAPI.Business
{
    public interface IDepartmentBusiness
    {
        IQueryable GetAllDepartments();
        bool AddNewDepartment(Department department);
    }
}