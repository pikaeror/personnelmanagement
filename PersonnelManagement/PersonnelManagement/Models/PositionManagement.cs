using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonnelManagement.Models;

namespace PersonnelManagement.Models
{
    public class PositionManagement
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public int? Id { get; set; }
        public int? Department { get; set; }
        public int? RankCap { get; set; }
        public int? Sof { get; set; }
        public int Positiontype { get; set; }
        public double Quantity { get; set; }
        public DateTime Dateactive { get; set; }
        public sbyte Datecustom { get; set; }
        public string Notice { get; set; }
        public int Positioncategory { get; set; }
        public sbyte Replacedbycivil { get; set; }
        public int Replacedbycivilpositioncategory { get; set; }
        public int Replacedbycivilpositiontype { get; set; }
        public string Mrd { get; set; } // Ids of mrd listed as comma separated values "3,11,8"
        public int Altrankconditiongroup { get; set; } // 0 if no alt ranks.
        public string Altranks { get; set; } // If altrankconditiongroup is not null, contains next information "conditionid=rank;conditionid2=rank2;..". For example, "3=7;4=8"
        public sbyte Decertificate { get; set; }
        public DateTime? Decertificatedate { get; set; }
        public int Civilranklow { get; set; }
        public int Civilrankhigh { get; set; }
        public sbyte Replacedbycivildatelimit { get; set; }
        public DateTime? Replacedbycivildate { get; set; }
        public string PositionsCode { get; set; }
        public List<PositionPart> PositionParts { get; set; }
        public sbyte Curator { get; set; }
        public sbyte Head { get; set; }
        public string Curatorlist { get; set; }
        public int Headid { get; set; }
        public sbyte Opchs { get; set; }
        public sbyte Part { get; set; }
        public double Partval { get; set; }
        public int Nodecree { get; set; }
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
    }
}
