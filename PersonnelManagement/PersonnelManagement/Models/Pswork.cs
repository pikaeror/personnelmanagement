using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Pswork
    {
        public int Id { get; set; }
        public int Cabinetid { get; set; }
        public DateTime Workstart { get; set; }
        public DateTime? Workend { get; set; }
        public string Workstartday { get; set; }
        public string Workstartmonth { get; set; }
        public int Workstartyear { get; set; }
        public string Workendday { get; set; }
        public string Workendmonth { get; set; }
        public int Workendyear { get; set; }
        public string Organizationwork { get; set; }
        public string Locationwork { get; set; }
    }
}
