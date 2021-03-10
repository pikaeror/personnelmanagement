using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Persondecreeoperation
    {
        public int Id { get; set; }
        public int Persondecree { get; set; }
        public int Person { get; set; }
        public int Subjectid { get; set; }
        public int Subjecttype { get; set; }
        public int Creator { get; set; }
        public int Persondecreeblock { get; set; }
        public int Persondecreeblocktype { get; set; }
        public int Persondecreeblockintro { get; set; }
        public int Persondecreeblocksub { get; set; }
        public int Persondecreeblocksubtype { get; set; }
        public string Intro { get; set; }
        public int Priority { get; set; }
        public int Priorityintro { get; set; }
        public int Optionnumber1 { get; set; }
        public int Optionnumber2 { get; set; }
        public int Optionnumber3 { get; set; }
        public int Optionnumber4 { get; set; }
        public int Optionnumber5 { get; set; }
        public int Optionnumber6 { get; set; }
        public int Optionnumber7 { get; set; }
        public int Optionnumber8 { get; set; }
        public int Optionnumber9 { get; set; }
        public int Optionnumber10 { get; set; }
        public int Optionnumber11 { get; set; }
        public string Optionstring1 { get; set; }
        public string Optionstring2 { get; set; }
        public string Optionstring3 { get; set; }
        public string Optionstring4 { get; set; }
        public string Optionstring5 { get; set; }
        public string Optionstring6 { get; set; }
        public string Optionstring7 { get; set; }
        public string Optionstring8 { get; set; }
        public string Optionstring9 { get; set; }
        public DateTime? Optiondate1 { get; set; }
        public DateTime? Optiondate2 { get; set; }
        public DateTime? Optiondate3 { get; set; }
        public DateTime? Optiondate4 { get; set; }
        public DateTime? Optiondate5 { get; set; }
        public DateTime? Optiondate6 { get; set; }
        public DateTime? Optiondate7 { get; set; }
        public DateTime? Optiondate8 { get; set; }
        public int Index { get; set; }
        public int Subvaluenumber1 { get; set; }
        public int Subvaluenumber2 { get; set; }
        public string Subvaluestring1 { get; set; }
        public string Subvaluestring2 { get; set; }
        public string Nonperson { get; set; }
        public string Optionarray1 { get; set; }
        public string Optionarrayperson { get; set; }
    }
}
