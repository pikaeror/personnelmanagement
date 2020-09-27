using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Sheetpolitics
    {
        public int Id { get; set; }
        public int Cabinetid { get; set; }
        public string Sheetpoliticslocation { get; set; }
        public string Sheetpoliticsnameorganization { get; set; }
        public string Sheetpoliticspost { get; set; }
        public DateTime Sheetpoliticsstart { get; set; }
        public DateTime? Sheetpoliticsend { get; set; }
    }
}
