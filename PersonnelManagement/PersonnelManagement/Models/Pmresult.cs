using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class Pmresult
    {
        public double Count { get; set; }
        public double Countvar { get; set; }
        public double Ranksexpanded { get; set; }
        public string Ranks { get; set; }  // names of ranks separated by @
        public string Ranksvar { get; set; } // names of ranks separated by @
        public string Maxcount { get; set; } // 160 separated by @
        public string Absolutecount { get; set; } // 180 separated by @ 
        public string Defaultcount { get; set; } // 100, separated by @
        public string Defaultcountvar { get; set; } // 100, separated by @
        public string Mincount { get; set; } // 80, separated by @
        public string Uprank { get; set; } // При наличии ученой степени=1:5,при наличии классности=1:10;2:20@
        public string Uprankready { get; set; } // Из них при наличии ученой степени степени получить звание майор внутренней службы 10 ед., подполковник внутренней службы 20 ед. & ...
        public string Civil { get; set; } // Государственные служащие=1@Cлужащие=5
        public string Uppedmap { get; set; } // Карта: Название=Айди;На сколько ранков максимум доступен подъем.  При наличии классности=1;2
        public string Uppedcount { get; set; } //При наличии классности=1:100;2:120&При наличии ученой степени=1:5@ 
        public string Uprankunited { get; set; } // Капитан внутренней службы:1:5;Майор внутренней службы:2:5;
        public string Comefromunited { get; set; } // Старший лейтенант внутренней службы:1:5;Капитан внутренней службы:2:5;
        public string Sumunited { get; set; } // 100:120:100,100:100:100@
        public string Unitedlengthmax { get; set; } // Наибольшая из длин uprankunited или comefromunited. 0@3@4@0@1

        public byte[] File { get; set; }

        //public Pmresult()
        //{
        //    Ranks = "";

        //}

    }
}
