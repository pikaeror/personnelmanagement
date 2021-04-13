using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class ExcertComposition
    {
        public PersondecreeManagement decree { get; set; }
        public List<PersondecreeblockManagement> decreeblocks { get; set; }
        public List<Persondecreeblocktype> decreeblocktypes { get; set; }
        public List<Persondecreeblocksub> decreeblocksubs { get; set; }
        public List<Persondecreeblocksubtype> decreeblocksubtypes { get; set; }
        public List<PersondecreeoperationManagement> decreeoperations { get; set; }

        public ExcertComposition()
        {
            decree = new PersondecreeManagement();
            decreeblocks = new List<PersondecreeblockManagement>();
            decreeblocktypes = new List<Persondecreeblocktype>();
            decreeblocksubs = new List<Persondecreeblocksub>();
            decreeblocksubtypes = new List<Persondecreeblocksubtype>();
            decreeoperations = new List<PersondecreeoperationManagement>();
        }
    }
}
