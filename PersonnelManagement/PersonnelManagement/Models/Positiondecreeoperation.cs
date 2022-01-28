using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Positiondecreeoperation
    {
        public uint Id { get; set; }
        public int Decree { get; set; }
        public uint Currentposition { get; set; }
        public sbyte Created { get; set; }
        public sbyte Deleted { get; set; }
        public sbyte Changed { get; set; }
        public uint Priviuseosition { get; set; }
        public DateTime? Dateactive { get; set; }
        public sbyte Datecustom { get; set; }
        public uint Currentstructure { get; set; }
    }
}
