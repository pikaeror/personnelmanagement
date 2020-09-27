using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Personelection
    {
        public int Id { get; set; }
        public int Person { get; set; }
        public string Location { get; set; }
        public string Electionwho { get; set; }
        public DateTime Electiondate { get; set; }
        public string Electionwhat { get; set; }
        public DateTime Electiondateend { get; set; }
    }
}
