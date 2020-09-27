using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Personjob
    {
        public int Id { get; set; }
        public int Person { get; set; }
        public int Jobtype { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string Jobplace { get; set; }
        public string Jobposition { get; set; }
        public string Jobpositionplace { get; set; }
        public int Servicetype { get; set; }
        public string Servicetypestr { get; set; }
        public int Servicefeature { get; set; }
        public string Serviceorder { get; set; }
        public int Servicecoef { get; set; }
        public string Serviceplace { get; set; }
        public string Ordernumber { get; set; }
        public string Ordernumbertype { get; set; }
        public DateTime? Orderdate { get; set; }
        public string Orderwho { get; set; }
        public int Orderwhoid { get; set; }
        public int Orderid { get; set; }
        public sbyte Actual { get; set; }
        public sbyte Manual { get; set; }
        public sbyte Mchs { get; set; }
        public int Vacationdays { get; set; }
        public int Position { get; set; }
        public int Positiontoselect { get; set; }
        public string Positionnametree { get; set; }
        public string Fireordernumber { get; set; }
        public string Fireordernumbertype { get; set; }
        public DateTime? Fireorderdate { get; set; }
        public string Fireorderwho { get; set; }
        public int Fireorderwhoid { get; set; }
        public int Fireorderid { get; set; }
        public int Statecivil { get; set; }
        public DateTime? Statecivilstart { get; set; }
        public DateTime? Statecivilend { get; set; }
        public DateTime? Startcustom { get; set; }
        public sbyte Privelege { get; set; }
    }
}
