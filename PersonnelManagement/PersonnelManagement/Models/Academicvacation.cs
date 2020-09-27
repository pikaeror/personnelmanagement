using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Academicvacation
    {
        public int Id { get; set; }
        public int Personeducation { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public DateTime? Orderdate { get; set; }
        public string Ordernumber { get; set; }
        public int PersoneducationId { get; set; }
        public string Ordernumbertype { get; set; }
        public string Orderwho { get; set; }
    }
}
