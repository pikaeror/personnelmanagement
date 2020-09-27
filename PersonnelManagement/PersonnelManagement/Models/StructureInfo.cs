using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class StructureInfo
    {
        public string Name { get; set; }
        public int Id { get; set; }


        public int Head { get; set; }
        public double PositionCountSigned { get; set; }
        public double PositionCountUnsigned { get; set; }
        public string SofNameList { get; set; } // Array of string "," splitted
        public string PositionCountSofSigned { get; set; } // Array of number "," splitted
        public string PositionCountSofUnsigned { get; set; } // Array of number "," splitted
        public double VarCountSigned { get; set; }
        public double VarCountUnsigned { get; set; }
        public int Priority { get; set; }

        public bool HasChildren { get; set; }
        public bool Previous { get; set; } = false;
        public string Grandparent { get; set; }

        public double PositionAddFuture { get; set; }
        public double PositionRemoveFuture { get; set; }
        public string[] PositionFutureDetailed { get; set; }

        public SortedDictionary<int, KeyValuePair<double, double>> SofValues { get; set; } // Only for c#
        public SortedDictionary<string, double> FutureAddDetailed { get; set; } = new SortedDictionary<string, double>();
        public SortedDictionary<string, double> FutureRemoveDetailed { get; set; } = new SortedDictionary<string, double>();
        public List<Position> Positions { get; set; }
    }
}
