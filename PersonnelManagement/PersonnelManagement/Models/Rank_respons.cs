using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public partial class Rank_respons
    {
        public Person Person { get; set; }
        public Rank Rank { get; set; }
        public DateTime Date_end { get; set; }
    }
}
