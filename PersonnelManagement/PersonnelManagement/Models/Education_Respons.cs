using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public partial class Education_Respons
    {
        public Education_Respons(Person person, Personeducation personeducation)
        {
            Person = person;
            Education = personeducation;
        }
        public Person Person { get; set; }
        public Personeducation Education { get; set; }
    }
}
