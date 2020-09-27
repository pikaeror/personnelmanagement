using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Positioncategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public sbyte Civil { get; set; }
        public sbyte Officer { get; set; }
        public sbyte Replaceofficer { get; set; }
        public sbyte Replacenonofficer { get; set; }
        public int Classcap { get; set; }
        public sbyte Variable { get; set; }
        public int Categoryranklink { get; set; }
    }
}
