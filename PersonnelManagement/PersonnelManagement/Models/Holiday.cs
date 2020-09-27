using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Holiday
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public sbyte Permanent { get; set; }
    }
}
