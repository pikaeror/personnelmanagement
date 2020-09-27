using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Persondecreeblocksubtype
    {
        public int Id { get; set; }
        public int Persondecreeblocktype { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }
}
