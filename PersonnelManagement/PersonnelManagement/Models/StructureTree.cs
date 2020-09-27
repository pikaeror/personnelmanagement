using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class StructureTree
    {
        public int Id { get; set; }
        public string Tree { get; set; }
        public int Ingoresiblings { get; set; } // 0 - false, 1 true
    }
}
