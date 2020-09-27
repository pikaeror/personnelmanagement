using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class DepartmentManagement
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public int? Parent { get; set; }
        public int? Structure { get; set; }
        public DateTime Dateactive { get; set; }
        public sbyte Datecustom { get; set; }
    }
}
