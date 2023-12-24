using InventoryManagementAPI.Business.Interfaces;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Business
{
    public class DepartmentBusiness : IDepartmentBusiness
    {
        private readonly AppDbContext appDbContext;


        public DepartmentBusiness(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public List<Department> GetAllDepartments()
        {
            return appDbContext.Departments.ToList();
        }

        public bool AddNewDepartment(Department department)
        {
            var departmentInDb = appDbContext.Departments.Where(x => x.Name.Equals(department.Name));

            if (departmentInDb.Any())
            {
                return false;
            }

            appDbContext.Departments.Add(department);
            appDbContext.SaveChanges();
            return true;
        }

        public async Task<bool> Patch(Department department)
        {
            var departmentInDb = await appDbContext.Departments.FindAsync(department.Id);
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



        public async Task<bool> DeleteDepartment(int departmentId)
        {
            var department = await appDbContext.Departments.FindAsync(departmentId);

            if (department == null)
            {
                return false;
            }

            var products = appDbContext.Products.Where(x => x.DepartmentId == departmentId);
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
