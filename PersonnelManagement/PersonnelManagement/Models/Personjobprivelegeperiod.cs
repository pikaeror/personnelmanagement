using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Personjobprivelegeperiod
    {
        public int Id { get; set; }
        public int Personjobprivelege { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}
