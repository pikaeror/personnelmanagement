using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class StructureInfoInner
    {
        public int Head { get; set; }
        public double PositionCountSigned { get; set; }
        public double PositionCountUnsigned { get; set; }
        public Dictionary<int, KeyValuePair<double, double>> sofValues { get; set; }// Sof name - count signed - count unsigned
    }
}
