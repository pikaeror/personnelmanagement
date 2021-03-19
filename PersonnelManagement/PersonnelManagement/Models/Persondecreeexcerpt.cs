using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Persondecreeexcerpt
    {
        public int Id { get; set; }
        public int Decree { get; set; }
        public int Structure { get; set; }
        public string Persondecreeoperations { get; set; }
    }
}
