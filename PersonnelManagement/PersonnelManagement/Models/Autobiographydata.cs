using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Autobiographydata
    {
        public int Id { get; set; }
        public int Cabinetid { get; set; }
        public string Autobiographypassport { get; set; }
        public string Autobiographywhogivepassport { get; set; }
        public string Autobiographyregistration { get; set; }
        public string Autobiographyworkphone { get; set; }
        public string Autobiographyhomephone { get; set; }
        public string Autobiographymobilephone { get; set; }
        public string Autobiographymilitaryid { get; set; }
        public string Autobiographywhogivemilitaryid { get; set; }
        public string Autobiographyeducationdocnum { get; set; }
        public string Autobiographywhogiveeducationdoc { get; set; }
        public string Autobiographybiography { get; set; }
        public int Autobiographylockunlock { get; set; }
        public DateTime Autobiographysignature { get; set; }
    }
}
