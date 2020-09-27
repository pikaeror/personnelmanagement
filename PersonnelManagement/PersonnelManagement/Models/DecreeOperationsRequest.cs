using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{

    /**
     * Request actual Decree Operations for specified subject
     */
    public class DecreeOperationsRequest
    {

        public int SubjectID { get; set; } // У подразделений subject имеет знак минуса
        public int Subjectidstructureupdate { get; set; } // STRUCTURES ONLY. If structure was modified.
        public DateTime RequestedDate { get; set; }
        public int Detailed { get; set; } // 1 - true, 0 - false
        public int Padding { get; set; }

        public bool full_output_flag = false;
    }
}
