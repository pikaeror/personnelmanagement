using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class CabinetdataManager: Cabinetdata
    {
        public Autobiographydata[] Autobiographydatalist { get; set; }
        public Declarationdata[] Declarationdatalist { get; set; }
        public Declarationrelative[] Declarationrelativelist { get; set; }
        public Declarationtabledata[] Declarationtabledatalist { get; set; }
        public Profiledata[] Profiledatalist { get; set; }
        public Profilerelatives[] Profilerelativeslist { get; set; }
        public Pseducation[] Pseducationlist { get; set; }
        public Pswork[] Psworklist { get; set; }
        public Sheetdata[] Sheetdatalist { get; set; }
        public Sheetpolitics[] Sheetpoliticslist { get; set; }

        public int Action { get; set; } // 0 - создать
        public int Declarationid { get; set; } // используется для создания личного кабинета, но не используется в одноименной таблице.

        //public string Structure { get; set; }
        public UserCompact UserCompact { get; set; }

        public CabinetdataManager()
        {

        }

        public CabinetdataManager(Cabinetdata cabinetdata)
        {
            Id = cabinetdata.Id;
            Employeesid = cabinetdata.Employeesid;
            Creatorid = cabinetdata.Creatorid;
            Reasonid = cabinetdata.Reasonid;
            Usersurname = cabinetdata.Usersurname;
            Username = cabinetdata.Username;
            Userpatronymic = cabinetdata.Userpatronymic;
            Userind = cabinetdata.Userind;
            Accesscode = cabinetdata.Accesscode;
            Status = cabinetdata.Status;
            Denyreason = cabinetdata.Denyreason;
            Creationdate = cabinetdata.Creationdate;

            UserCompact = new UserCompact();

            Autobiographydatalist = new Autobiographydata[0];
            Declarationdatalist = new Declarationdata[0];
            Declarationrelativelist = new Declarationrelative[0];
            Declarationtabledatalist = new Declarationtabledata[0];
            Profiledatalist = new Profiledata[0];
            Profilerelativeslist = new Profilerelatives[0];
            Pseducationlist = new Pseducation[0];
            Psworklist = new Pswork[0];
            Sheetdatalist = new Sheetdata[0];
            Sheetpoliticslist = new Sheetpolitics[0];
        }

        public static List<CabinetdataManager> CabinetsToCabinetManagers(Repository repository, User user, IEnumerable<Cabinetdata> cabinetdatas)
        {
            List<CabinetdataManager> cabinetdataManagers = new List<CabinetdataManager>();
            foreach (Cabinetdata cabinetdata in cabinetdatas)
            {
                cabinetdataManagers.Add(repository.GetCabinetdataManager(user, cabinetdata));
            }
            return cabinetdataManagers;
        }
    }
}
