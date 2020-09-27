using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public partial class Personcontract
    {
        public List<Personadditionalagreement> Personadditionalagreements { get; set; } = new List<Personadditionalagreement>();
    }
}
