using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Personworktrip
    {
        public int Id { get; set; }
        public int Person { get; set; }
        public int Country { get; set; }
        public DateTime Tripdate { get; set; }
        public string Reason { get; set; }
        public string Ordernumber { get; set; }
        public string Ordernumbertype { get; set; }
        public DateTime? Orderdate { get; set; }
        public string Orderwho { get; set; }
        public int Orderwhoid { get; set; }
        public int Orderid { get; set; }
        public sbyte Privelege { get; set; }
        public int Days { get; set; }
    }
}
