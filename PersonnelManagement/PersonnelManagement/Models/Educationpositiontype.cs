using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Educationpositiontype
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public sbyte Fulltimeonly { get; set; }
    }
}
