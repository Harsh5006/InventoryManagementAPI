using InventoryManagementAPI.Models;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Business.Interfaces
{
    public interface IAdministrationBusiness
    {
        Task<bool> AddNewRole(CreateRoleDTO createRoleDTO);
    }
}