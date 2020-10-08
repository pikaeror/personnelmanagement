using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Persondecree
    {
        public int Id { get; set; }
        public DateTime Datecreated { get; set; }
        public DateTime? Datesigned { get; set; }
        public int Creator { get; set; }
        public int Owner { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string Number { get; set; }
        public string Numbertype { get; set; }
        public sbyte Transfer { get; set; }
        public sbyte Signed { get; set; }
        public int Persondecreelevel { get; set; }
        public int Mailexplorerid { get; set; }
    }
}
