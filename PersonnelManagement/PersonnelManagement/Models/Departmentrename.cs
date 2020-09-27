using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Departmentrename
    {
        public int Id { get; set; }
        public string Oldname { get; set; }
        public string Newname { get; set; }
        public int Decreeoperation { get; set; }
        public int Decree { get; set; }
    }
}
