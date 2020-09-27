using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class PersonphysicalManager: Personphysical
    {
        public Physicalfield[] Physicalfields { get; set; }

        public PersonphysicalManager()
        {

        }

        public PersonphysicalManager(Personphysical personphysical)
        {
            Id = personphysical.Id;
            Person = personphysical.Person;
            Physicaldate = personphysical.Physicaldate;

            Physicalfields = new Physicalfield[0];
        }

    }
}
