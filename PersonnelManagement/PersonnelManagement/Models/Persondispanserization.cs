using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Persondispanserization
    {
        public int Id { get; set; }
        public int Person { get; set; }
        public int Group { get; set; }
        public string Result { get; set; }
        public DateTime Date { get; set; }
    }
}
