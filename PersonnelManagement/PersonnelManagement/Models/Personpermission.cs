using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Personpermission
    {
        public int Id { get; set; }
        public int Person { get; set; }
        public int Permissiontype { get; set; }
        public string Number { get; set; }
        public DateTime Datestart { get; set; }
        public DateTime Dateend { get; set; }
    }
}
