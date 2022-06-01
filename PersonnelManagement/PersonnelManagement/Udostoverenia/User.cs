using System;
using System.Collections.Generic;

namespace PersonnelManagement.Udostoverenia
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public int? Agency { get; set; }
        public sbyte Admin { get; set; }
        public sbyte Weapon { get; set; }
    }
}
