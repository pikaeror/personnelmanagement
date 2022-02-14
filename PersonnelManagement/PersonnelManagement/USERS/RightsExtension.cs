using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.USERS
{
    public partial class Rights
    {
        [NotMapped]
        public List<Rightsstructure> Rightsstructures = new List<Rightsstructure>();

        /// <summary>
        /// Копирует поля из этих прав в указанные
        /// </summary>
        /// <param name="otherRights"></param>
        public void CopyFields(Rights otherRights)
        {
            otherRights.User = User;
            otherRights.Position = Position;
            otherRights.Role = Role;
            otherRights.Admin = Admin;
            otherRights.Orgedit = Orgedit;
            otherRights.Orgread = Orgread;
            otherRights.Orgreadall = Orgreadall;
            otherRights.Peopleedit = Peopleedit;
            otherRights.Peopleread = Peopleread;
            otherRights.Peoplereadall = Peoplereadall;
            otherRights.Candidateedit = Candidateedit;
            otherRights.Candidateblock = Candidateblock;
            otherRights.Candidateread = Candidateread;
            otherRights.Peopleorgread = Peopleorgread;
            otherRights.Peopleorgreadall = Peopleorgreadall;
            otherRights.Peopledecreeread = Peopledecreeread;
            otherRights.Peopledecreeedit = Peopledecreeedit;
            otherRights.Peopleeditmain = Peopleeditmain;
            otherRights.Peoplereadmain = Peoplereadmain;
            otherRights.Peopleeditpassport = Peopleeditpassport;
            otherRights.Peoplereadpassport = Peoplereadpassport;
            otherRights.Peopleeditphoto = Peopleeditphoto;
            otherRights.Peoplereadphoto = Peoplereadphoto;
            otherRights.Peopleeditcertificate = Peopleeditcertificate;
            otherRights.Peoplereadcertificate = Peoplereadcertificate;
            otherRights.Peopleeditrelative = Peopleeditrelative;
            otherRights.Peoplereadrelative = Peoplereadrelative;
            otherRights.Peopleediteducation = Peopleediteducation;
            otherRights.Peoplereadeducation = Peoplereadeducation;
            otherRights.Peopleediteducationucp = Peopleediteducationucp;
            otherRights.Peoplereadeducationucp = Peoplereadeducationucp;
            otherRights.Peopleeditjob = Peopleeditjob;
            otherRights.Peoplereadjob = Peoplereadjob;
            otherRights.Peopleeditjobprivelege = Peopleeditjobprivelege;
            otherRights.Peoplereadjobprivelege = Peoplereadjobprivelege;
            otherRights.Peopleeditjobpension = Peopleeditjobpension;
            otherRights.Peoplereadjobpension = Peoplereadjobpension;
            otherRights.Peopleeditill = Peopleeditill;
            otherRights.Peoplereadill = Peoplereadill;
            otherRights.Peopleeditdispanserization = Peopleeditdispanserization;
            otherRights.Peoplereaddispanserization = Peoplereaddispanserization;
            otherRights.Peopleeditvvk = Peopleeditvvk;
            otherRights.Peoplereadvvk = Peoplereadvvk;
            otherRights.Peopleeditpfl = Peopleeditpfl;
            otherRights.Peoplereadpfl = Peoplereadpfl;
            otherRights.Peopleeditrank = Peopleeditrank;
            otherRights.Peoplereadrank = Peoplereadrank;
            otherRights.Peopleeditcontract = Peopleeditcontract;
            otherRights.Peoplereadcontract = Peoplereadcontract;
            otherRights.Peopleeditcontractvacation = Peopleeditcontractvacation;
            otherRights.Peoplereadcontractvacation = Peoplereadcontractvacation;
            otherRights.Peopleeditcontractstate = Peopleeditcontractstate;
            otherRights.Peoplereadcontractstate = Peoplereadcontractstate;
            otherRights.Peopleeditvacation = Peopleeditvacation;
            otherRights.Peoplereadvacation = Peoplereadvacation;
            otherRights.Peopleeditreward = Peopleeditreward;
            otherRights.Peoplereadreward = Peoplereadreward;
            otherRights.Peopleeditattestation = Peopleeditattestation;
            otherRights.Peoplereadattestation = Peoplereadattestation;
            otherRights.Peopleeditlanguage = Peopleeditlanguage;
            otherRights.Peoplereadlanguage = Peoplereadlanguage;
            otherRights.Peopleeditscience = Peopleeditscience;
            otherRights.Peoplereadscience = Peoplereadscience;
            otherRights.Peopleeditelection = Peopleeditelection;
            otherRights.Peoplereadelection = Peoplereadelection;
            otherRights.Peopleeditworktrip = Peopleeditworktrip;
            otherRights.Peoplereadworktrip = Peoplereadworktrip;
            otherRights.Peopleeditpenalty = Peopleeditpenalty;
            otherRights.Peoplereadpenalty = Peoplereadpenalty;
            otherRights.Peopleeditphysical = Peopleeditphysical;
            otherRights.Peoplereadphysical = Peoplereadphysical;
            otherRights.Peopleeditdriver = Peopleeditdriver;
            otherRights.Peoplereaddriver = Peoplereaddriver;
            otherRights.Peopleeditpermission = Peopleeditpermission;
            otherRights.Peoplereadpermission = Peoplereadpermission;
            otherRights.Peopleeditprivelege = Peopleeditprivelege;
            otherRights.Peoplereadprivelege = Peoplereadprivelege;
            otherRights.Peopleeditwound = Peopleeditwound;
            otherRights.Peoplereadwound = Peoplereadwound;
        }

        public Rights()
        {
            User = 0;
            Position = 0;
            Role = 0;
            Admin = 0;
            Orgedit = 0;
            Orgread = 0;
            Orgreadall = 0;
            Peopleedit = 0;
            Peopleread = 0;
            Peoplereadall = 0;
            Candidateedit = 0;
            Candidateblock = 0;
            Candidateread = 0;
            Peopleorgread = 0;
            Peopleorgreadall = 0;
            Peopledecreeread = 0;
            Peopledecreeedit = 0;
            Peopleeditmain = 0;
            Peoplereadmain = 0;
            Peopleeditpassport = 0;
            Peoplereadpassport = 0;
            Peopleeditphoto = 0;
            Peoplereadphoto = 0;
            Peopleeditcertificate = 0;
            Peoplereadcertificate = 0;
            Peopleeditrelative = 0;
            Peoplereadrelative = 0;
            Peopleediteducation = 0;
            Peoplereadeducation = 0;
            Peopleediteducationucp = 0;
            Peoplereadeducationucp = 0;
            Peopleeditjob = 0;
            Peoplereadjob = 0;
            Peopleeditjobprivelege = 0;
            Peoplereadjobprivelege = 0;
            Peopleeditjobpension = 0;
            Peoplereadjobpension = 0;
            Peopleeditill = 0;
            Peoplereadill = 0;
            Peopleeditdispanserization = 0;
            Peoplereaddispanserization = 0;
            Peopleeditvvk = 0;
            Peoplereadvvk = 0;
            Peopleeditpfl = 0;
            Peoplereadpfl = 0;
            Peopleeditrank = 0;
            Peoplereadrank = 0;
            Peopleeditcontract = 0;
            Peoplereadcontract = 0;
            Peopleeditcontractvacation = 0;
            Peoplereadcontractvacation = 0;
            Peopleeditcontractstate = 0;
            Peoplereadcontractstate = 0;
            Peopleeditvacation = 0;
            Peoplereadvacation = 0;
            Peopleeditreward = 0;
            Peoplereadreward = 0;
            Peopleeditattestation = 0;
            Peoplereadattestation = 0;
            Peopleeditlanguage = 0;
            Peoplereadlanguage = 0;
            Peopleeditscience = 0;
            Peoplereadscience = 0;
            Peopleeditelection = 0;
            Peoplereadelection = 0;
            Peopleeditworktrip = 0;
            Peoplereadworktrip = 0;
            Peopleeditpenalty = 0;
            Peoplereadpenalty = 0;
            Peopleeditphysical = 0;
            Peoplereadphysical = 0;
            Peopleeditdriver = 0;
            Peoplereaddriver = 0;
            Peopleeditpermission = 0;
            Peoplereadpermission = 0;
            Peopleeditprivelege = 0;
            Peoplereadprivelege = 0;
            Peopleeditwound = 0;
            Peoplereadwound = 0;
        }
    }
}
