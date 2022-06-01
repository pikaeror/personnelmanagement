using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Rightsstructure
    {
        public int Id { get; set; }
        public int Rights { get; set; }
        public int User { get; set; }
        public int Structure { get; set; }
        public sbyte Allowed { get; set; }
        public sbyte Org { get; set; }
        public sbyte People { get; set; }
        public sbyte Peopleorg { get; set; }
    }
}
