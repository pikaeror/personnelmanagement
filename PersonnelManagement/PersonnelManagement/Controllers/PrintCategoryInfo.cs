using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Controllers
{
    public class PrintCategoryInfo
    {
        public double Count { get; set; } = 0;
        public Dictionary<int, double> Sofs { get; set; } = new Dictionary<int, double>();
    }
}
