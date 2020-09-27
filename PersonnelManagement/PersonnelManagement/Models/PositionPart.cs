using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class PositionPart
    {
        public int Id { get; set; }
        public bool Custom { get; set; }
        public DateTime Customdate { get; set; }
        public bool Civil { get; set; }
        public bool Civildatelimit { get; set; }
        public DateTime Civildate { get; set; }
        public bool Decertificate { get; set; }
        public DateTime Decertificatedate { get; set; }
        public bool Decree { get; set; }
        public string Decreenumber { get; set; }
        public DateTime Decreedate { get; set; }
    }
}
