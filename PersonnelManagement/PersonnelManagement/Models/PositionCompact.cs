using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class PositionCompact
    {
        public Position Position { get; set; }
        public int Count { get; set; }
        public Dictionary<Sourceoffinancing, int> Sofs { get; set; } = new Dictionary<Sourceoffinancing, int>();
    }
}
