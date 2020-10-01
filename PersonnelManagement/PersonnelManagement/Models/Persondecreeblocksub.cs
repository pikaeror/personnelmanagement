using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Persondecreeblocksub
    {
        public int Id { get; set; }
        public int Persondecree { get; set; }
        public int Persondecreeblock { get; set; }
        public int Persondecreeblockintro { get; set; }
        public int Persondecreeblocksubtype { get; set; }
        public int Intro { get; set; }
        public string Introtext { get; set; }
        public int Index { get; set; }
        public int Priority { get; set; }
        public int Subvaluenumber1 { get; set; }
        public int Subvaluenumber2 { get; set; }
        public string Subvaluestring1 { get; set; }
        public string Subvaluestring2 { get; set; }
        public int Parentpersondecreeblocksub { get; set; }
        public DateTime? Subvaluedate1 { get; set; }
        public DateTime? Subvaluedate2 { get; set; }
    }
}
