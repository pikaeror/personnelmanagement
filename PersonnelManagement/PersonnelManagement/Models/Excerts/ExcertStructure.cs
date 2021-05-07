using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class ExcertStructure
    {
        public Structure structure { get; set; }
        public string data_create { get; set; }
        public string data_opened { get; set; }
        public int excert_id { get; set; }

        public ExcertStructure()
        {
            structure = new Structure();
            data_create = "";
            data_opened = "";
            excert_id = 0;
        }
    }
}
