using Hospital.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Specifications
{
    public class DepartmentWithDoctorsSpecification : BaseSpecification<Department>
    {
        public DepartmentWithDoctorsSpecification(string sort)
        {
            Include.Add(d => d.Doctors);
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "NameDepartmentAsc":
                        AddOrderBy(p => p.Name);
                        break;
                    case "NameDepartmentDesc":
                        AddOrderByDesc(p => p.Name);
                        break;
                    default:
                        AddOrderBy(p => p.Id);
                        break;
                }
            }
        }
        public DepartmentWithDoctorsSpecification(int id) : base(d => d.Id == id)
        {
            Include.Add(d => d.Doctors); 
        }
    }
}
