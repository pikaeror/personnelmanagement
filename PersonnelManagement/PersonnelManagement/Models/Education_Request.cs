using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public partial class Education_Request
    {
        public IEnumerable<StructureTree> Current_structure { get; set; } 
        public List<string> SpecializationList { get; set; } 
        public List<string> CvalificationList { get; set; } 
        public List<string> EducationLevel { get; set; } 
    }
}
