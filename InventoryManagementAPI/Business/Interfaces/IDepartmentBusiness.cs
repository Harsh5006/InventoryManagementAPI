﻿using InventoryManagementAPI.Models;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementAPI.Business.Interfaces
{
    public interface IDepartmentBusiness
    {
        bool AddNewDepartment(Department department);
        Task<bool> DeleteDepartment(int id);
        IQueryable GetAllDepartments();
        bool Patch(Department department);
    }
}