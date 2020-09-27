using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Personprivelege
    {
        public int Id { get; set; }
        public int Person { get; set; }
        public string Name { get; set; }
    }
}
