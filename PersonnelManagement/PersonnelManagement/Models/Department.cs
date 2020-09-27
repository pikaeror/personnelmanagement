using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Structure { get; set; }
        public int? Department1 { get; set; }
        public DateTime? Dateactive { get; set; }
        public DateTime? Dateinactive { get; set; }
        public sbyte? Notlogged { get; set; }
        public string Nameshort { get; set; }
        public int Position { get; set; }
    }
}
