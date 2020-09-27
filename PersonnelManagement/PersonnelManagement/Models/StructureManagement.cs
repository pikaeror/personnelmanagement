using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class StructureManagement
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public int? Id { get; set; }
        public int Parent { get; set; }
        public string NameShortened { get; set; }
        public sbyte? Featured { get; set; }
        public DateTime Dateactive { get; set; }
        public sbyte Datecustom { get; set; }
        public int Structuretype { get; set; }
        public int Structuretypesiblings { get; set; }
        public int Structureregion { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Rank { get; set; }
        public int Nodecree { get; set; }
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
        //public DateTime? Dateactive { get; set; }
        //public DateTime? Dateinactive { get; set; }
        //public sbyte DecreeSigned { get; set; }
        //public string DecreeName { get; set; }

    }
}
