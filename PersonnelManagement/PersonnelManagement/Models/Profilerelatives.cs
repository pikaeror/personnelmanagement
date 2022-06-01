using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Profilerelatives
    {
        public int Id { get; set; }
        public int Cabinetid { get; set; }
        public string Profilerelativesdegree { get; set; }
        public string Profilerelativesfullname { get; set; }
        public DateTime? Profilerelativesdob { get; set; }
        public string Profilerelativespob { get; set; }
        public string Profilerelativeswork { get; set; }
        public string Profilerelativeslocation { get; set; }
    }
}
