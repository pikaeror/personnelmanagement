using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class PmrequestStructureCount
    {
        public Sourceoffinancing Sourceoffinancing { get; set; }
        public double CountSummary { get; set; }
        public Dictionary<int, double> CountPositionCategory { get; set; } 
    }
}
