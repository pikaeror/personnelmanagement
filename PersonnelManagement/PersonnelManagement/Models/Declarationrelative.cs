using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Declarationrelative
    {
        public int Id { get; set; }
        public int Cabinetid { get; set; }
        public string Declarationfullnamerelative { get; set; }
        public DateTime Declarationdobrelative { get; set; }
        public string Declarationrelativerelation { get; set; }
    }
}
