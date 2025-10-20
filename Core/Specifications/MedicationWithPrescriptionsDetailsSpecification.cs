using Hospital.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Specifications
{
    public class MedicationWithprescriptionsDetailsSpecification : BaseSpecification<Medication>
    {
        public MedicationWithprescriptionsDetailsSpecification(string sort)
        {
            Include.Add(p => p.PrescriptionDetails);
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "NameMedicationAsc":
                        AddOrderBy(p => p.Name);
                        break;
                    case "NameMedicationDesc":
                        AddOrderByDesc(p => p.Name);
                        break;
                    default:
                        AddOrderBy(p => p.Id);
                        break;
                }
            }
        }
        public MedicationWithprescriptionsDetailsSpecification(int id)
        {
            Include.Add(p => p.PrescriptionDetails);
        }

    }
}
