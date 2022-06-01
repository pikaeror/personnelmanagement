using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Udostoverenia
{
    public class CertificateManager
    {

        public string Numud { get; set; }
        public int Status { get; set; }
        public DateTime Certificatecommiteddate { get; set; }
        public DateTime Expirationdate { get; set; }
        public string Blank { get; set; }
        public string Issuingauthority { get; set; }
        public string Post { get; set; }
        public string Agency { get; set; }
        public string FullName { get; set; }

        public CertificateManager()
        {

        }

    }
}
