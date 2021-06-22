using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class PersonDecreeHistory
    {
        public string name { get; set; }
        public DateTime date { get; set; }
        public string action { get; set; }
    }
}
