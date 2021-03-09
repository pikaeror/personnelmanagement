using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class NewVzvod
    {
        public string name;
        public int newIdStructure;
        public int oldIdStructure;

        public NewVzvod(int newIdStructure, int oldIdStructure, string name)
        {
            this.newIdStructure = newIdStructure;
            this.oldIdStructure = oldIdStructure;
            this.name = name;
        }
    }
}
