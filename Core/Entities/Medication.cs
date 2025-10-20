using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Entities
{
    public class Medication : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }

        public ICollection<PrescriptionDetail> PrescriptionDetails { get; set; } = new List<PrescriptionDetail>();
    }
}
