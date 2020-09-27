using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class StaffManagement
    {
        public int Id { get; set; } // Id is origin id.
        public int Realid { get; set; } // Real id is current id.
        public int Type { get; set; } // 0 - everything signed and unsigned.
        public int Head { get; set; } // head positions assigned to structures where they are heading
    }
}
