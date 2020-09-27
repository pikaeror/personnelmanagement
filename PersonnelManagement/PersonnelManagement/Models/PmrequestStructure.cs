using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class PmrequestStructure
    {
        public string Name { get; set; }
        public string Tree { get; set; }
        public string Type { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Rank { get; set; }
        public string Curator { get; set; }
        public string Head { get; set; }
        public StructureInfoInner StructureInfoInner { get; set; }
        public List<KeyValuePair<Sourceoffinancing, double>> SofSigned { get; set; }
        public List<KeyValuePair<Sourceoffinancing, double>> SofUnsigned { get; set; }
        public string DateActive { get; set; }
        public string Signed { get; set; }
        public string DateCreated { get; set; }

        public string DecreeCreationName { get; set; }
        public string DecreeCreationDate { get; set; }
        public string DecreeCreationNumber { get; set; }
        public string DecreeCreationUnofficialName { get; set; }

        /**
         * For Structure count only
         */
        public List<PmrequestStructureCount> PmrequestStructureCounts { get; set; }
    }
}
