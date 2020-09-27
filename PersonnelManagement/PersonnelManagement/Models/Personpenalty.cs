using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Personpenalty
    {
        public int Id { get; set; }
        public int Person { get; set; }
        public int Penalty { get; set; }
        public string Violation { get; set; }
        public string Orderwho { get; set; }
        public string Ordernumber { get; set; }
        public string Ordernumbertype { get; set; }
        public DateTime Orderdate { get; set; }
        public int Orderwhoid { get; set; }
        public int Orderid { get; set; }
    }
}
