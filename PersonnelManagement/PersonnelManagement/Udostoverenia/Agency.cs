using System;
using System.Collections.Generic;

namespace PersonnelManagement.Udostoverenia
{
    public partial class Agency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Parent { get; set; }
        public string PreferredSigner1 { get; set; }
        public string PreferredSigner2 { get; set; }
    }
}
