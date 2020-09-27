using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Personvvk
    {
        public int Id { get; set; }
        public int Person { get; set; }
        public string Number { get; set; }
        public string Result { get; set; }
        public DateTime Date { get; set; }
    }
}
