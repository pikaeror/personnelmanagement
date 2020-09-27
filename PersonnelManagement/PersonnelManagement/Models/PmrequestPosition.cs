using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class PmrequestPosition
    {
        public Position position { get; set; }
        public int Id { get; set; }
        public string Positiontype { get; set; }
        public string Tree { get; set; }
        public string Rank { get; set; }
        public string Positioncategory { get; set; }
        public string Sof { get; set; }
        public string Mrds { get; set; }
        public string Structuremrd { get; set; }
        public string Signed { get; set; }
        public string ReplacedByCivil { get; set; }
        public string ReplacedByCivilPositiontype { get; set; }
        public string ReplacedByCivilPositioncategory { get; set; }
        public string ReplacedByCivilDate { get; set; }
        public string ReplacedByCivilRankLow { get; set; }
        public string ReplacedByCivilRankHigh { get; set; }
        public string ReplacedByCivilDecree { get; set; }
        public string ReplacedByCivilDecreeDate { get; set; }
        public string CivilClassHigh { get; set; }
        public string CivilClassLow { get; set; }
        public string DecertificateDate { get; set; }
        public string DateCreated { get; set; }
        public string DateChanged { get; set; }
        public AltrankPrintable AltrankPrintable { get; set; } 
        public string Curation { get; set; }
        public string Heading { get; set; }
        public sbyte Part { get; set; }
        public double Partval { get; set; }

        public string DecreeCreationName { get; set; }
        public string DecreeCreationDate { get; set; }
        public string DecreeCreationNumber { get; set; }
        public string DecreeCreationUnofficialName { get; set; }

        public string Notice { get; set; }
        //public List<KeyValuePair<Altrankconditiongroup, List<KeyValuePair<string, string>>>> Altranks { get; set; } // Contains alt rank and list of alt rank condition name/rank name
    }
}
