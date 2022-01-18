using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public partial class Rank_request
    {
        public IEnumerable<StructureTree> Current_structure { get; set; }
        public DateTime Last_rank_date { get; set; }
        public bool Corelate_rank { get; set; }
    }
}
