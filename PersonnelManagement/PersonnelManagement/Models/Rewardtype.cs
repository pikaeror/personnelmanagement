using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Rewardtype
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public sbyte Medal { get; set; }
        public sbyte Chestsign { get; set; }
        public sbyte Certificate { get; set; }
        public sbyte Honorcertificate { get; set; }
        public sbyte Money { get; set; }
        public sbyte Thanks { get; set; }
        public sbyte Penalty { get; set; }
        public sbyte Ranknext { get; set; }
        public sbyte Rankfast { get; set; }
        public sbyte Mchs { get; set; }
    }
}
