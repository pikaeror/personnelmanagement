using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class PersondecreeoperationManagement: Persondecreeoperation
    {


        public int Status { get; set; } // 1 - добавить, 2 - удалить, 3 - обновить
                                        //
        public Personreward Personreward { get; set; }
        public PersonManager Personobject { get; set; }
        public Personpenalty Personpenalty { get; set; }
        public Position Positionobject { get; set; }
        public Positiontype Positiontypeobject { get; set; }
        public Structure Structureobject { get; set; }
        public Fire Fireobject { get; set; }
        public List<Person> personFromStructure { get; set; }
        public List<PersonManager> OptionarraypersonObjects { get; set; } = new List<PersonManager>(); // ЭЛД, сгрупированные в дополнении к этой операции. 
                                                                        // Единственная группировка на данный момент - командировка нескольких людей в одно место назначения

        public PersondecreeoperationManagement(Persondecreeoperation persondecreeoperation)
        {
            Id = persondecreeoperation.Id;
            Persondecree = persondecreeoperation.Persondecree;
            Person = persondecreeoperation.Person;
            Subjectid = persondecreeoperation.Subjectid;
            Subjecttype = persondecreeoperation.Subjecttype; // Тип операции. 1 - Награды.
            Creator = persondecreeoperation.Creator;
            Persondecreeblock = persondecreeoperation.Persondecreeblock;
            Persondecreeblocktype = persondecreeoperation.Persondecreeblocktype;
            Persondecreeblocksub = persondecreeoperation.Persondecreeblocksub;
            Persondecreeblocksubtype = persondecreeoperation.Persondecreeblocksubtype;
            Intro = persondecreeoperation.Intro;
            //persondecreeoperation
            Optionnumber1 = persondecreeoperation.Optionnumber1;
            Optionnumber2 = persondecreeoperation.Optionnumber2;
            Optionnumber3 = persondecreeoperation.Optionnumber3;
            Optionnumber4 = persondecreeoperation.Optionnumber4;
            Optionnumber5 = persondecreeoperation.Optionnumber5;
            Optionnumber6 = persondecreeoperation.Optionnumber6;
            Optionnumber7 = persondecreeoperation.Optionnumber7;
            Optionnumber8 = persondecreeoperation.Optionnumber8;
            Optionnumber9 = persondecreeoperation.Optionnumber9;
            Optionnumber10 = persondecreeoperation.Optionnumber10;
            Optionnumber11 = persondecreeoperation.Optionnumber11;
            Optionstring1 = persondecreeoperation.Optionstring1;
            Optionstring2 = persondecreeoperation.Optionstring2;
            Optionstring3 = persondecreeoperation.Optionstring3;
            Optionstring4 = persondecreeoperation.Optionstring4;
            Optionstring5 = persondecreeoperation.Optionstring5;
            Optionstring6 = persondecreeoperation.Optionstring6;
            Optionstring7 = persondecreeoperation.Optionstring7;
            Optionstring8 = persondecreeoperation.Optionstring8;
            Optiondate1 = persondecreeoperation.Optiondate1;
            Optiondate2 = persondecreeoperation.Optiondate2;
            Optiondate3 = persondecreeoperation.Optiondate3;
            Optiondate4 = persondecreeoperation.Optiondate4;
            Optiondate5 = persondecreeoperation.Optiondate5;
            Optiondate6 = persondecreeoperation.Optiondate6;
            Optiondate7 = persondecreeoperation.Optiondate7;
            Optiondate8 = persondecreeoperation.Optiondate8;
            Index = persondecreeoperation.Index;
            Subvaluenumber1 = persondecreeoperation.Subvaluenumber1;
            Subvaluenumber2 = persondecreeoperation.Subvaluenumber2;
            Subvaluestring1 = persondecreeoperation.Subvaluestring1;
            Subvaluestring2 = persondecreeoperation.Subvaluestring2;
            Nonperson = persondecreeoperation.Nonperson;
            Optionarrayperson = persondecreeoperation.Optionarrayperson;
            Optionarray1 = persondecreeoperation.Optionarray1;

            Personreward = null;
            Personpenalty = null;
            Personobject = null;
            Positionobject = null;
            Positiontypeobject = null;
            Structureobject = null;
            Fireobject = null;
            OptionarraypersonObjects = new List<PersonManager>();
        }

        public PersondecreeoperationManagement()
        {

        }

    }
}
