using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class PersondecreeblockManagement: Persondecreeblock
    {

        public int Status { get; set; } // 1 - добавить
                                        // 2 - удалить
        public Personreward SamplePersonreward { get; set; }
        public Structure SampleStructure { get; set; }
        public Position SamplePosition { get; set; }
        public Positiontype SamplePositiontype { get; set; }
        public List<Persondecreeblocksub> Persondecreeblocksubs { get; set; }
        public List<Persondecreeblockintro> Persondecreeblockintros { get; set; }
        public List<PersonManager> OptionarraypersonObjects { get; set; } = new List<PersonManager>(); // ЭЛД, сгрупированные в дополнении к этой операции. 
                                                                        // Единственная группировка на данный момент - командировка нескольких людей в одно место назначения

        public PersondecreeblockManagement()
        {

        }

        public PersondecreeblockManagement(Persondecreeblock persondecreeblock)
        {
            Id = persondecreeblock.Id;
            Persondecree = persondecreeblock.Persondecree;
            Persondecreeblocktype = persondecreeblock.Persondecreeblocktype;
            Intro = persondecreeblock.Intro;
            Persondecreeblocksub = persondecreeblock.Persondecreeblocksub;
            Optionnumber1 = persondecreeblock.Optionnumber1;
            Optionnumber2 = persondecreeblock.Optionnumber2;
            Optionnumber3 = persondecreeblock.Optionnumber3;
            Optionnumber4 = persondecreeblock.Optionnumber4;
            Optionnumber5 = persondecreeblock.Optionnumber5;
            Optionnumber6 = persondecreeblock.Optionnumber6;
            Optionnumber7 = persondecreeblock.Optionnumber7;
            Optionnumber8 = persondecreeblock.Optionnumber8;
            Optionnumber9 = persondecreeblock.Optionnumber9;
            Optionnumber10 = persondecreeblock.Optionnumber10;
            Optionnumber11 = persondecreeblock.Optionnumber11;
            Optionstring1 = persondecreeblock.Optionstring1;
            Optionstring2 = persondecreeblock.Optionstring2;
            Optionstring3 = persondecreeblock.Optionstring3;
            Optionstring4 = persondecreeblock.Optionstring4;
            Optionstring5 = persondecreeblock.Optionstring5;
            Optionstring6 = persondecreeblock.Optionstring6;
            Optionstring7 = persondecreeblock.Optionstring7;
            Optionstring8 = persondecreeblock.Optionstring8;
            Optiondate1 = persondecreeblock.Optiondate1;
            Optiondate2 = persondecreeblock.Optiondate2;
            Optiondate3 = persondecreeblock.Optiondate3;
            Optiondate4 = persondecreeblock.Optiondate4;
            Optiondate5 = persondecreeblock.Optiondate5;
            Optiondate6 = persondecreeblock.Optiondate6;
            Optiondate7 = persondecreeblock.Optiondate7;
            Optiondate8 = persondecreeblock.Optiondate8;
            Subvaluenumber1 = persondecreeblock.Subvaluenumber1;
            Subvaluenumber2 = persondecreeblock.Subvaluenumber2;
            Subvaluestring1 = persondecreeblock.Subvaluestring1;
            Subvaluestring2 = persondecreeblock.Subvaluestring2;
            Optionarrayperson = persondecreeblock.Optionarrayperson;
            Optionarray1 = persondecreeblock.Optionarray1;

            Index = persondecreeblock.Index;

            SamplePersonreward = null;
            SampleStructure = null;
            SamplePosition = null;
            SamplePositiontype = null;
            Persondecreeblocksubs = new List<Persondecreeblocksub>();
            OptionarraypersonObjects = new List<PersonManager>();
        }
    }
}
