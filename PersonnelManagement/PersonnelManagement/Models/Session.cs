using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Session
    {
        public string Id { get; set; }
        public int Userid { get; set; }
        public DateTime Expires { get; set; }
    }
}
