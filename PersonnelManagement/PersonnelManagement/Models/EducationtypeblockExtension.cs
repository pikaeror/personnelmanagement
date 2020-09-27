using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public partial class Educationtypeblock
    {
        public List<Educationperiod> Educationperiods { get; set; } = new List<Educationperiod>();
    }
}
