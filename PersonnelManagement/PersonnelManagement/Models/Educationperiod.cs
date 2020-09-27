using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Educationperiod
    {
        public int Id { get; set; }
        public int Educationtypeblock { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public int Educationpositiontype { get; set; }
        public sbyte Service { get; set; }
        public int EducationtypeblockId { get; set; }
        public string Rank { get; set; }
        public string Ranktype { get; set; }
        public int Platoon { get; set; }
        public int Course { get; set; }
        public DateTime? Orderdate { get; set; }
        public string Ordernum { get; set; }
        public string Ordernumbertype { get; set; }
    }
}
