using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Personscience
    {
        public int Id { get; set; }
        public int Person { get; set; }
        public int Sciencetype { get; set; }
        public string Sciencedescription { get; set; }
        public DateTime Sciencedate { get; set; }
        public string Sciencediplom { get; set; }
    }
}
