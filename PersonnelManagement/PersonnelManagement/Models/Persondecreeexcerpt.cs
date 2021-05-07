using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Persondecreeexcerpt
    {
        public int Id { get; set; }
        public int Decree { get; set; }
        public int Structure { get; set; }
        public string Persondecreeoperations { get; set; }
        public int Openflags { get; set; }
        public int CreatorId { get; set; }
        public DateTime? Datacreated { get; set; }
        public DateTime? Datasend { get; set; }
        public int FirstOpensId { get; set; }
        public DateTime? Dataopens { get; set; }
    }
}
