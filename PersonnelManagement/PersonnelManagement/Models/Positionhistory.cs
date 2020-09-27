using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Positionhistory
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public int Previous { get; set; }
        public int Decreeoperation { get; set; }
        public int Decree { get; set; }
        public sbyte Created { get; set; }
        public sbyte Deleted { get; set; }
    }
}
