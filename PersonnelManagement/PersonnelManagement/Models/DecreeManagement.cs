using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class DecreeManagement
    {

        public int DecreeManagementStatus { get; set; } //  1 - create new decree, 2 - decline decree, 3 - accept decree, 4 - print decree. 5 - save decree info (date) changes. 6 - show filtered decrees

        public int Id { get; set; }
        public string Name { get; set; }
        public sbyte Signed { get; set; }
        public sbyte Declined { get; set; }
        public DateTime? Dateactive { get; set; }
        public DateTime? Datesigned { get; set; }
        public int? User { get; set; }
        public string Nickname { get; set; } // unofficial name
        public string Number { get; set; }
        public int Historycal { get; set; }

        public string Dateactivestart { get; set; }
        public string Dateactiveend { get; set; }
        public string Datesignedstart { get; set; }
        public string Datesignedend { get; set; }
    }
}
