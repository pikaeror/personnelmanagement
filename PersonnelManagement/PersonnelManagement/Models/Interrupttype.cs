using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Interrupttype
    {
        public int Id { get; set; }
        public string Pointsubpoint { get; set; }
        public int Point { get; set; }
        public int Subpoint { get; set; }
        public string Description { get; set; }
        public string Selectdescription { get; set; }
    }
}
