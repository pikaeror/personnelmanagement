using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Structure
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Parentstructure { get; set; }
        public sbyte? Featured { get; set; }
        public string Nameshortened { get; set; }
        public int Structuretype { get; set; }
        public int Structureregion { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Rank { get; set; }
        public sbyte Department { get; set; }
        public int Curator { get; set; }
        public int Head { get; set; }
        public int Changeorigin { get; set; }
        public int Changestructurelast { get; set; }
        public sbyte Changestructurerename { get; set; }
        public sbyte Changestructureall { get; set; }
        public sbyte Changestructurerank { get; set; }
        public sbyte Changestructurelocation { get; set; }
        public sbyte Changestructureparent { get; set; }
        public int Priority { get; set; }
        public sbyte Printreward { get; set; }
        public sbyte Main { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Name4 { get; set; }
        public string Name5 { get; set; }
        public string Name6 { get; set; }
        public sbyte Separatestructure { get; set; }
        public int Subject1 { get; set; }
        public int Subject2 { get; set; }
        public int Subject3 { get; set; }
        public int Subject4 { get; set; }
        public int Subject5 { get; set; }
        public int Subject6 { get; set; }
        public int Subject7 { get; set; }
        public int Subject8 { get; set; }
        public int Subject9 { get; set; }
        public int Subject10 { get; set; }
        public int Subject11 { get; set; }
        public int Subject12 { get; set; }
        public int Subject13 { get; set; }
        public int Subject14 { get; set; }
        public int Subject15 { get; set; }
        public int Subjectnumber { get; set; }
        public string Subjectnotice { get; set; }
        public int Subjectgender { get; set; }
    }
}
