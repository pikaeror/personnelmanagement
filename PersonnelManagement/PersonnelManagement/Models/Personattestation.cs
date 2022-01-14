using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Personattestation
    {
        public int Id { get; set; }
        public int Person { get; set; }
        public int Attestationtype { get; set; }
        public DateTime Date { get; set; }
        public string Result { get; set; }
        public string Recomendation { get; set; }
        public string Ordernumber { get; set; }
        public string Ordernumbertype { get; set; }
        public DateTime? Orderdate { get; set; }
        public string Orderwho { get; set; }
        public int Orderwhoid { get; set; }
        public int Orderid { get; set; }
        public string Validity { get; set; }
    }
}
