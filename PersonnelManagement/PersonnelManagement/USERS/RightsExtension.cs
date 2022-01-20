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
    }
}
