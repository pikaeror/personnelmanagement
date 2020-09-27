using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Personjobprivelege
    {
        public int Id { get; set; }
        public int Person { get; set; }
        public int Personjob { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public double Coef { get; set; }
        public int Prooftype { get; set; }
        public DateTime? Proofdate { get; set; }
        public string Proofnumber { get; set; }
        public string Prooftext { get; set; }
        public string Documentorder { get; set; }
        public DateTime? Documentdate { get; set; }
        public string Documentnumber { get; set; }
        public string Ordernumbertype { get; set; }
        public string Orderwho { get; set; }
        public int Orderwhoid { get; set; }
        public int Orderid { get; set; }
        public int Ordertype { get; set; }
    }
}
