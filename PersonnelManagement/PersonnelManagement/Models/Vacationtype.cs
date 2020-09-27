using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Vacationtype
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name2 { get; set; }
        public sbyte Social { get; set; }
        public sbyte Cadet { get; set; }
        public sbyte Military { get; set; }
        public sbyte Civil { get; set; }
        public sbyte Transferworkyear { get; set; }
        public sbyte Main { get; set; }
        public sbyte Maternity { get; set; }
        public int Durationmax { get; set; }
        public int Order { get; set; }
    }
}
