using InventoryManagementAPI.Business.Interfaces;
using InventoryManagementAPI.Core;
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
        private readonly IUnitOfWork unitOfWork;

        public DepartmentBusiness(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return unitOfWork.Departments.GetAll();
        }

        public bool AddNewDepartment(Department department)
        {
            var departmentInDb = unitOfWork.Departments.SingleOrDefault(x => x.Name.Equals(department.Name));

            if (departmentInDb != null)
            {
                return false;
            }

            unitOfWork.Departments.Add(department);
            unitOfWork.Complete();
            return true;
        }

        public async Task<bool> Patch(Department department)
        {
            var departmentInDb = await unitOfWork.Departments.GetAsync(department.Id);
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
                var departmentInDbWithSameName = unitOfWork.Departments.SingleOrDefault(x => x.Name.Equals(department.Name));
                if (departmentInDbWithSameName != null) { return false; }

                departmentInDb.Name = department.Name;
                unitOfWork.Complete();
                return true;
            }
        }



        public async Task<bool> DeleteDepartment(int departmentId)
        {
            var department = await unitOfWork.Departments.GetAsync(departmentId);

            if (department == null)
            {
                return false;
            }

            var products = unitOfWork.Products.Find(x => x.DepartmentId == departmentId);
            if (products.Count() > 0)
            {
                return false;
            }

            unitOfWork.Departments.Remove(department);
            unitOfWork.Complete();

            return true;
        }
    }
}
