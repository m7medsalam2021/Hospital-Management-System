using Hospital.Core.Dtos;
using Hospital.Core.Entities;
using Hospital.Core.IServices;
using Hospital.Repository.Data;

namespace Hospital.Service.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HospitalContext _hospitalContext;

        public DepartmentService(HospitalContext hospitalContext)
        {
            _hospitalContext = hospitalContext;
        }
        public async Task<Department> CreateDepartmentAsync(DepartmentToReturnDoctorsDto departments)
        {
            var department = new Department
            {
                Name = departments.Name,
                Location = departments.Location
            };
            _hospitalContext.Add(department);
            await _hospitalContext.SaveChangesAsync();
            return department;
        }

        public async Task<Department> DeleteDepartmentAsync(int id)
        {
            var department = await _hospitalContext.Departments.FindAsync(id);
            if (department == null)
                return null;

            _hospitalContext.Departments.Remove(department);
            await _hospitalContext.SaveChangesAsync();
            return department;
        }

        public async Task<Department> UpdateDepartmentAsync(int id, DepartmentToReturnDoctorsDto departments)
        {
            var department = await _hospitalContext.Departments.FindAsync(id);
            if (department == null)
                return null;

            department.Name = departments.Name;
            department.Location = departments.Location;

            await _hospitalContext.SaveChangesAsync();
            return department;
        }
    }
}
