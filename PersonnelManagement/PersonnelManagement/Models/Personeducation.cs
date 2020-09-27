using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Personeducation
    {
        public int Id { get; set; }
        public int Person { get; set; }
        public int Main { get; set; }
        public int Educationlevel { get; set; }
        public int Educationstage { get; set; }
        public string Name { get; set; }
        public string Name2 { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public string Faculty { get; set; }
        public int Educationtype { get; set; }
        public int Datestart { get; set; }
        public int Dateend { get; set; }
        public string Speciality { get; set; }
        public string Documentseries { get; set; }
        public string Documentnumber { get; set; }
        public sbyte Cadet { get; set; }
        public string Qualification { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public sbyte Interrupted { get; set; }
        public DateTime? Interruptorderdate { get; set; }
        public string Interruptorderwho { get; set; }
        public string Interruptordernumber { get; set; }
        public string Interruptordernumbertype { get; set; }
        public string Interruptorderreason { get; set; }
        public int Educationdocument { get; set; }
        public string Ordernumber { get; set; }
        public string Ordernumbertype { get; set; }
        public DateTime? Orderdate { get; set; }
        public string Orderwho { get; set; }
        public int Orderwhoid { get; set; }
        public int Orderid { get; set; }
        public string Nameasjobfull { get; set; }
        public string Nameasjobposition { get; set; }
        public string Nameasjobplace { get; set; }
        public int Educationadditionaltype { get; set; }
        public int Ucp { get; set; }
        public sbyte Academicvacation { get; set; }
        public sbyte Maternityvacation { get; set; }
        public decimal Rating { get; set; }
        public string State { get; set; }
        public string Citytype { get; set; }
    }
}
