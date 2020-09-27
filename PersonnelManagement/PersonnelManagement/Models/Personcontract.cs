using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Personcontract
    {
        public int Id { get; set; }
        public int Person { get; set; }
        public DateTime Datestart { get; set; }
        public DateTime Dateend { get; set; }
        public sbyte Pay { get; set; }
        public string Ordernumber { get; set; }
        public string Ordernumbertype { get; set; }
        public DateTime? Orderdate { get; set; }
        public string Orderwho { get; set; }
        public int Orderwhoid { get; set; }
        public int Orderid { get; set; }
        public int Sourceoffinancing { get; set; }
        public int Payvalue { get; set; }
        public int Stateserviceyears { get; set; }
        public int Stateservicemonths { get; set; }
        public int Stateservicedays { get; set; }
        public int Vacationdays { get; set; }
    }
}
