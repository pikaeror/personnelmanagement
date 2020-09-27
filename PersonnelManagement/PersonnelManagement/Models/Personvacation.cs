using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Personvacation
    {
        public int Id { get; set; }
        public int Person { get; set; }
        public int Vacationmilitary { get; set; }
        public int Vacationtype { get; set; }
        public DateTime? Date { get; set; }
        public int Duration { get; set; }
        public int Trip { get; set; }
        public sbyte Compensation { get; set; }
        public DateTime Compensationdate { get; set; }
        public string Compensationnumber { get; set; }
        public int Compensationdays { get; set; }
        public sbyte Cancel { get; set; }
        public DateTime Canceldate { get; set; }
        public DateTime Allowstart { get; set; }
        public DateTime Allowend { get; set; }
        public int Holidays { get; set; }
        public DateTime? Canceldateend { get; set; }
        public sbyte Cancelcontinue { get; set; }
        public string Ordernumber { get; set; }
        public string Ordernumbertype { get; set; }
        public DateTime? Orderdate { get; set; }
        public string Orderwho { get; set; }
        public int Orderwhoid { get; set; }
        public int Orderid { get; set; }
    }
}
