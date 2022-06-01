using System;
using System.Collections.Generic;

namespace PersonnelManagement.Udostoverenia
{
    public partial class Blankform
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public int? Agency { get; set; }
    }
}
