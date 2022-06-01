using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Decree
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public sbyte Signed { get; set; }
        public sbyte Declined { get; set; }
        public DateTime? Dateactive { get; set; }
        public DateTime? Datesigned { get; set; }
        public int? User { get; set; }
        public string Nickname { get; set; }
        public string Number { get; set; }
        public int Historycal { get; set; }
    }
}
