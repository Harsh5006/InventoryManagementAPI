using InventoryManagementAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Business.Interfaces
{
    public interface IDepartmentBusiness
    {
        bool AddNewDepartment(Department department);
        Task<bool> DeleteDepartment(int id);
        List<Department> GetAllDepartments();
        Task<bool> Patch(Department department);
    }
}