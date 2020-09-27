using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Civildecree
    {
        public int Id { get; set; }
        public sbyte Civildecree1 { get; set; }
        public string Civildecreenumber { get; set; }
        public DateTime Civildecreedate { get; set; }
        public int Position { get; set; }
        public DateTime Date { get; set; }
        public sbyte Delete { get; set; }
        public sbyte Replacedbycivil { get; set; }
        public int Civilranklow { get; set; }
        public int Civilrankhigh { get; set; }
        public sbyte Replacedbycivildatelimit { get; set; }
        public DateTime? Replacedbycivildate { get; set; }
        public sbyte Decertificate { get; set; }
        public DateTime? Decertificatedate { get; set; }
    }
}
