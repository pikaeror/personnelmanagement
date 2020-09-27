using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Rank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Order { get; set; }
        public DateTime? Dateactive { get; set; }
        public DateTime? Dateinactive { get; set; }
        public sbyte? Notlogged { get; set; }
        public int Positioncategory { get; set; }
        public sbyte Decreeupone { get; set; }
        public sbyte Decreeupfast { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Name4 { get; set; }
        public string Name5 { get; set; }
        public string Name6 { get; set; }
    }
}
