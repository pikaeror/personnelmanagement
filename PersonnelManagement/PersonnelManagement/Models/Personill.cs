using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Personill
    {
        public int Id { get; set; }
        public int Person { get; set; }
        public int Illtype { get; set; }
        public int Illcode { get; set; }
        public DateTime Datestart { get; set; }
        public DateTime Dateend { get; set; }
        public int Illregime { get; set; }
        public string Illwho { get; set; }
        public sbyte Privelege { get; set; }
    }
}
