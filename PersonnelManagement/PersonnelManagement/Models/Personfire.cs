using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Personfire
    {
        public int Id { get; set; }
        public int Person { get; set; }
        public int Firetype { get; set; }
        public string Reason { get; set; }
        public DateTime? Date { get; set; }
        public string Ordernumber { get; set; }
        public string Ordernumbertype { get; set; }
        public DateTime? Orderdate { get; set; }
        public string Orderwho { get; set; }
        public int Orderwhoid { get; set; }
        public int Orderid { get; set; }
        public int Reward { get; set; }
        public int Cloth { get; set; }
        public int Rank { get; set; }
    }
}
