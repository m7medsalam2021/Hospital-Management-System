﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Specifications
{
    public class PrescriptionDetailSpecparams
    {
        private const int MaxPageSize = 10;
        private int pageSize = 5;
        public int PageZize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }
        public int PageIndex { get; set; } = 1;

        public string? Sort { get; set; }
        public int? MedicationId { get; set; }
        public int? MedicalRecordId { get; set; }
    }
}
