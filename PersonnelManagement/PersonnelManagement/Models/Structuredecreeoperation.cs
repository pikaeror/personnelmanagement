using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Structuredecreeoperation
    {
        public uint Id { get; set; }
        public int Decree { get; set; }
        public uint Currentstructure { get; set; }
        public sbyte Created { get; set; }
        public sbyte Deleted { get; set; }
        public sbyte Changed { get; set; }
        public uint Priveusestructure { get; set; }
        public DateTime? Dateactive { get; set; }
        public sbyte Datecustom { get; set; }
    }
}
