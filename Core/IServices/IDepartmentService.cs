using Hospital.Core.Dtos;
using Hospital.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.IServices
{
    public interface IDepartmentService
    {
        Task<Department> CreateDepartmentAsync(DepartmentToReturnDoctorsDto departments);
        Task<Department> UpdateDepartmentAsync(int id, DepartmentToReturnDoctorsDto departments);
        Task<Department> DeleteDepartmentAsync(int id);
    }
}
