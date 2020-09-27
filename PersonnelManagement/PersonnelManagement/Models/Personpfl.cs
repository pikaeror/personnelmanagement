using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Personpfl
    {
        public int Id { get; set; }
        public int Person { get; set; }
        public DateTime Date { get; set; }
        public byte[] Document { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Person64header { get; set; }
    }
}
