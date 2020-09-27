using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Persondecreeblockintro
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Persondecree { get; set; }
        public int Persondecreeblock { get; set; }
        public int Index { get; set; }
        public int Creator { get; set; }
        public int Priority { get; set; }
    }
}
