using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class PositionDecertificate
    {
        public int Id { get; set; }
        public sbyte Decertificate { get; set; }
        public DateTime? Decertificatedate { get; set; }
    }
}
