using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Persontransfer
    {
        public int Id { get; set; }
        public int Person { get; set; }
        public int Transfertype { get; set; }
        public string Reason { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public int Structure { get; set; }
        public string Place { get; set; }
        public string Ordernumber { get; set; }
        public string Ordernumbertype { get; set; }
        public DateTime? Orderdate { get; set; }
        public string Orderwho { get; set; }
        public int Orderwhoid { get; set; }
        public int Orderid { get; set; }
        public int Structuretoselect { get; set; }
    }
}
