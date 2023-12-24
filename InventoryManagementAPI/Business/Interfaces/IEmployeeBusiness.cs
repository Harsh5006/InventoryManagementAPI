using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Business.Interfaces
{
    public interface IEmployeeBusiness
    {
        Task<bool> DeleteEmployee(string id);
        Task<EmployeeDetailsDTO> EmployeeDetails(string id);
        Task<List<EmployeeDTO>> GetAllEmployees();
    }
}