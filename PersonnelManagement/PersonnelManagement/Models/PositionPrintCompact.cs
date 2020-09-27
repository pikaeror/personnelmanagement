using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class PositionPrintCompact
    {
        public string Name { get; set; }
        public int Sof { get; set; }
        public int RankCap { get; set; }
        public int Added { get; set; }
        public int Deleted { get; set; }
        public bool Datecustom { get; set; }
        public DateTime Dateactive { get; set; }
        public bool ReplacedByCivil { get; set; }
        public string CivilName { get; set; }
    }
}
