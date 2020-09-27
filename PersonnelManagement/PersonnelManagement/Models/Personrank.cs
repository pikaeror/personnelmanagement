using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Personrank
    {
        public int Id { get; set; }
        public int Person { get; set; }
        public int Rank { get; set; }
        public string Rankstring { get; set; }
        public string Structure { get; set; }
        public int Structureid { get; set; }
        public DateTime Decreedate { get; set; }
        public string Decreenumber { get; set; }
        public string Decreenumbertype { get; set; }
        public int Decreeid { get; set; }
        public DateTime? Datestart { get; set; }
    }
}
