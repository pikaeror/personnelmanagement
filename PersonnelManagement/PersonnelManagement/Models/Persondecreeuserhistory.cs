using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Persondecreeuserhistory
    {
        public int Id { get; set; }
        public int User { get; set; }
        public int Decree { get; set; }
        public DateTime? Date { get; set; }
        public string Status { get; set; }
    }
}
