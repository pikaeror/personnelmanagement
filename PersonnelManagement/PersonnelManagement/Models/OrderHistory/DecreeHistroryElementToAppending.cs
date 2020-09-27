using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class DecreeHistroryElementToAppending
    {
        public int structure_id { set; get; }
        public string number { set; get; }
        public DateTime? date { set; get; }
        public int history { set; get; }

        public bool CheckObject()
        {
            if (structure_id != 0 && number != null && date != null)
                return true;
            return false;
        }
    }
}
