using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Personadditionalagreement
    {
        public int Id { get; set; }
        public int Person { get; set; }
        public int Contract { get; set; }
        public int Duration { get; set; }
        public DateTime? Datestart { get; set; }
        public int? PersoncontractId { get; set; }
    }
}
