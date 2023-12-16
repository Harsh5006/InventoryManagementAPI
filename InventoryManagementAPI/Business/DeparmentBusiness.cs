using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Business
{
    public class DepartmentBusiness : IDepartmentBusiness
    {
        private readonly IAppDbContext appDbContext;

        public DepartmentBusiness(IAppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IQueryable GetAllDepartments()
        {
            var list = appDbContext.Departments.Select(x => new { Id = x.Id, Name = x.Name });
            return list;
        }

        public bool AddNewDepartment(Department department)
        {
            appDbContext.Departments.Add(department);
            appDbContext.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteDepartment(int id)
        {
            var department = await appDbContext.Departments.FindAsync(id);

            if(department == null)
            {
                return false;
            }

            var products = appDbContext.Products.Where(x => x.DepartmentId == id);
            if(products.Count() > 0)
            {
                return false;
            }

            appDbContext.Departments.Remove(department);
            appDbContext.SaveChanges();

            return true;
        }

        
    }
}
