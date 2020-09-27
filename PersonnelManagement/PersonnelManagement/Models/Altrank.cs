using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Altrank
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public int Rank { get; set; }
        public int Altrankcondition { get; set; }
        public sbyte Primary { get; set; }
    }
}
