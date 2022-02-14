using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Position
    {
        public int Id { get; set; }
        public int? Cap { get; set; }
        public DateTime? Dateactive { get; set; }
        public DateTime? Dateinactive { get; set; }
        public int Sourceoffinancing { get; set; }
        public int Positiontype { get; set; }
        public string Notice { get; set; }
        public int Positioncategory { get; set; }
        public sbyte Replacedbycivil { get; set; }
        public int Replacedbycivilpositioncategory { get; set; }
        public int Replacedbycivilpositiontype { get; set; }
        public sbyte Altrank { get; set; }
        public int Origin { get; set; }
        public sbyte Decertificate { get; set; }
        public DateTime? Decertificatedate { get; set; }
        public int Civilranklow { get; set; }
        public int Civilrankhigh { get; set; }
        public sbyte Replacedbycivildatelimit { get; set; }
        public DateTime? Replacedbycivildate { get; set; }
        public int Structure { get; set; }
        public sbyte Civildecree { get; set; }
        public string Civildecreenumber { get; set; }
        public DateTime? Civildecreedate { get; set; }
        public sbyte Curator { get; set; }
        public sbyte Head { get; set; }
        public string Curatorlist { get; set; }
        public int Headid { get; set; }
        public sbyte Opchs { get; set; }
        public sbyte Part { get; set; }
        public double Partval { get; set; }
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
        public int Subject16 { get; set; }
        public int Subject17 { get; set; }
        public int Subject18 { get; set; }
        public int Subject19 { get; set; }
        public int Subject20 { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Name4 { get; set; }
        public string Name5 { get; set; }
        public string Name6 { get; set; }
        public uint Subjectindex { get; set; }
    }
}
