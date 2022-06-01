using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Dismissalclauses
    {
        public int Id { get; set; }
        public int? Paragraph { get; set; }
        public int? Subparagraph { get; set; }
        public string Titleofarticles { get; set; }
        public int? Persondecreeblocktype { get; set; }
        public int? Type { get; set; }
    }
}
