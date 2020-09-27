using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class Pmrequest
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Positiontypes { get; set; }
        public string Structures { get; set; }
        public string Structuretypes { get; set; }
        public string Positioncategories { get; set; }
        public string Sofs { get; set; }
        public string Mrds { get; set; }
        public string Ranks { get; set; }
        public int Ranksexpanded { get; set; }
        public int Replacedbycivil { get; set; }
        public DateTime Replacedbycivildate { get; set; }
        public int Replacedbycivildateavailable { get; set; }
        public int Replacedbycivildateexpired { get; set; }
        public int Replacedbycivilnot { get; set; }
        public string Replacedbycivilpositiontypes { get; set; }
        public string Replacedbycivilpositioncategories { get; set; }
        public int Signed { get; set; }
        public int Notsigned { get; set; }
        public int Willbenotsigned { get; set; }
        public int Willbesigned { get; set; }
        public int Decertificate { get; set; }
        public int Decertificateexpired { get; set; }
        public DateTime Date { get; set; }
        public int Civilclasslow { get; set; }
        public int Civilclasshigh { get; set; }

        public string Structurerank { get; set; }
        public string Structureregion { get; set; }
        public string Structurecity { get; set; }
        public string Structurestreet { get; set; }

        public int Displaytreeseparately { get; set; }
        public int Displaytree { get; set; }
        public int Displaystructureparent { get; set; }
        public int Displaypositionchildren { get; set; }
        public int Displaymrds { get; set; }
        public int Displayreplacedbycivilinfo { get; set; }
        public int Civil { get; set; }
        public int Notopchs { get; set; }
        public int Allopchs { get; set; }
        public int Structurecountmode { get; set; }
        public int Structurecountallinclusive { get; set; }
        public int Structuresub { get; set; } // Включать подчиненные подразделения тех, кто прошел фильтрацию
        public int Structuresublevel { get; set; } // Уровень вложенности для пункта выше
        public int Structureselfcount { get; set; } // Учитывать исключительно собственную численность

        /**
         * Back-end only
         */
        public List<AltrankPrintable> AltrankPrintables { get; set; }
        public bool AnyAltranks { get; set; }
        public bool AddRemove { get; set; } = false;
        public Structure StructureMain { get; set; } // Пока что нужно только для сведений о штатной численности органов и подразделений
    }
}
