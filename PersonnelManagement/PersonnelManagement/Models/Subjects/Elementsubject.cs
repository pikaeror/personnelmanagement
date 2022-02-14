using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public partial class Elementsubject
    {
        public Elementsubject()
        {
            Subject1 = 0;
            Subject2 = 0;
            Subject3 = 0;
            Subject4 = 0;
            Subject5 = 0;
            Subject6 = 0;
            Subject7 = 0;
            Subject8 = 0;
            Subject9 = 0;
            Subject10 = 0;
            Subject11 = 0;
            Subject12 = 0;
            Subject13 = 0;
            Subject14 = 0;
            Subject15 = 0;
            Subject16 = 0;
            Subject17 = 0;
            Subject18 = 0;
            Subject19 = 0;
            Subject20 = 0;
        }
        public Elementsubject(Elementsubject elementsubject)
        {
            Id = elementsubject.Id;
            Subject1 = elementsubject.Subject1;
            Subject2 = elementsubject.Subject2;
            Subject3 = elementsubject.Subject3;
            Subject4 = elementsubject.Subject4;
            Subject5 = elementsubject.Subject5;
            Subject6 = elementsubject.Subject6;
            Subject7 = elementsubject.Subject7;
            Subject8 = elementsubject.Subject8;
            Subject9 = elementsubject.Subject9;
            Subject10 = elementsubject.Subject10;
            Subject11 = elementsubject.Subject11;
            Subject12 = elementsubject.Subject12;
            Subject13 = elementsubject.Subject13;
            Subject14 = elementsubject.Subject14;
            Subject15 = elementsubject.Subject15;
            Subject16 = elementsubject.Subject16;
            Subject17 = elementsubject.Subject17;
            Subject18 = elementsubject.Subject18;
            Subject19 = elementsubject.Subject19;
            Subject20 = elementsubject.Subject20;
        }
        public Elementsubject(Structure structure)
        {
            Subject1 = (uint)structure.Subject1;
            Subject2 = (uint)structure.Subject2;
            Subject3 = (uint)structure.Subject3;
            Subject4 = (uint)structure.Subject4;
            Subject5 = (uint)structure.Subject5;
            Subject6 = (uint)structure.Subject6;
            Subject7 = (uint)structure.Subject7;
            Subject8 = (uint)structure.Subject8;
            Subject9 = (uint)structure.Subject9;
            Subject10 = (uint)structure.Subject10;
            Subject11 = (uint)structure.Subject11;
            Subject12 = (uint)structure.Subject12;
            Subject13 = (uint)structure.Subject13;
            Subject14 = (uint)structure.Subject14;
            Subject15 = (uint)structure.Subject15;
        }

        public Elementsubject(Position position)
        {
            Subject1 = (uint)position.Subject1;
            Subject2 = (uint)position.Subject2;
            Subject3 = (uint)position.Subject3;
            Subject4 = (uint)position.Subject4;
            Subject5 = (uint)position.Subject5;
            Subject6 = (uint)position.Subject6;
            Subject7 = (uint)position.Subject7;
            Subject8 = (uint)position.Subject8;
            Subject9 = (uint)position.Subject9;
            Subject10 = (uint)position.Subject10;
            Subject11 = (uint)position.Subject11;
            Subject12 = (uint)position.Subject12;
            Subject13 = (uint)position.Subject13;
            Subject14 = (uint)position.Subject14;
            Subject15 = (uint)position.Subject15;
            Subject16 = (uint)position.Subject16;
            Subject17 = (uint)position.Subject17;
            Subject18 = (uint)position.Subject18;
            Subject19 = (uint)position.Subject19;
            Subject20 = (uint)position.Subject20;
        }

        /*public string getFullName*/
    }
}
