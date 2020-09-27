using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Decreeoperation
    {
        public int Id { get; set; }
        public int Decree { get; set; }
        public int Subject { get; set; }
        public sbyte Created { get; set; }
        public sbyte Deleted { get; set; }
        public sbyte Changed { get; set; }
        public int Changedtype { get; set; }
        public DateTime? Dateactive { get; set; }
        public sbyte Datecustom { get; set; }
    }
}
