using InventoryManagementAPI.Business.Interfaces;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Business
{
    public class DepartmentBusiness : IDepartmentBusiness
    {
        private readonly AppDbContext appDbContext;

        //private readonly IAppDbContext appDbContext;

        //public DepartmentBusiness(IAppDbContext appDbContext)
        //{
        //    this.appDbContext = appDbContext;
        //}

        public DepartmentBusiness(AppDbContext appDbContext)
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
            var departmentInDb = appDbContext.Departments.Where(x => x.Name == department.Name);

            if (departmentInDb.Any())
            {
                return false;
            }

            appDbContext.Departments.Add(department);
            appDbContext.SaveChanges();
            return true;
        }

        public bool Patch(Department department)
        {
            var departmentInDb = appDbContext.Departments.Find(department.Id);
            if (departmentInDb == null)
            {
                return false;
            }

            if (departmentInDb.Name.Equals(department.Name))
            {
                return true;
            }
            else
            {
                var departmentInDbWithSameName = appDbContext.Departments.Where(x => x.Name.Equals(department.Name));
                if (departmentInDbWithSameName.Any()) { return false; }

                departmentInDb.Name = department.Name;
                appDbContext.SaveChanges();
                return true;
            }
        }



        public async Task<bool> DeleteDepartment(int id)
        {
            var department = await appDbContext.Departments.FindAsync(id);

            if (department == null)
            {
                return false;
            }

            var products = appDbContext.Products.Where(x => x.DepartmentId == id);
            if (products.Count() > 0)
            {
                return false;
            }

            appDbContext.Departments.Remove(department);
            appDbContext.SaveChanges();

            return true;
        }


    }
}
