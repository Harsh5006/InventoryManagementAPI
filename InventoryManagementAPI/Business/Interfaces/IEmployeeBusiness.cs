using System.Threading.Tasks;

namespace InventoryManagementAPI.Business.Interfaces
{
    public interface IEmployeeBusiness
    {
        Task<bool> DeleteEmployee(string id);
        Task<object> EmployeeDetails(string id);
        Task<object> GetAllEmployees();
    }
}