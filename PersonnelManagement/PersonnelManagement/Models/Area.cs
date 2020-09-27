using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Area
    {
        public int Id { get; set; }
        public int Region { get; set; }
        public string Name { get; set; }
        public sbyte Other { get; set; }
    }
}
