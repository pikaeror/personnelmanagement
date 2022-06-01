using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Declarationtabledata
    {
        public int Id { get; set; }
        public int Cabinetid { get; set; }
        public int Declarationcabinetid { get; set; }
        public int Declarationform { get; set; }
        public int Declarationsection { get; set; }
        public int Declarationpoint { get; set; }
        public string Declarationnameorganization { get; set; }
        public string Declarationtypeincome { get; set; }
        public string Declarationamount { get; set; }
        public string Declarationcurrency { get; set; }
        public int Declarationdatein { get; set; }
        public string Declarationlocation { get; set; }
        public DateTime? Declarationfulldate { get; set; }
        public string Declarationarea { get; set; }
        public string Declarationcarmodel { get; set; }
        public int Declarationcardate { get; set; }
        public string Declarationwhoprovided { get; set; }
        public string Declarationwhattime { get; set; }
    }
}
