using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class PmresultSinglerank
    {
        public Rank Rank { get; set; }
        public int Defaultcount { get; set; }
        public int Defaultcountvar { get; set; }
        public int Maxcount { get; set; }
        public int Absolutecount { get; set; }
        public int Mincount { get; set; }
        public int Reduce { get; set; } // how much will be reduced after all positions upped
        public Dictionary<int, Dictionary<int, int>> CameFrom { get; set; } // type, level of upping, count  — Если имеется классная квалификация, на 1 степень придут на должность 5 человек.
        public Dictionary<int, Dictionary<int, int>> Upcount { get; set; } // type, level of upping, count — Если имеется классная квалификация, на 1 степень 20, на 2 степень - 15
        public Dictionary<int, int> CameFromUnited { get; set; } // level of upping, count — на 1 степень придут на должность 5 человек.
        public Dictionary<int, int> UpcountUnited { get; set; } // level of upping, count — на 1 степень 20, на 2 степень - 15
        public Dictionary<int,int> ComethroughUnited { get; set; } // Сколько переменных должностей прошлой мимо этой должности.  1) order начального звания 2) order конечного звания 3) количество таких прошедших 
        public Dictionary<int, int> SumUnited { get; set; } // level of upping, count — на 1 степень 100, на 2 степень - 80
        public Dictionary<int, int> Civil { get; set; } // Civil by categories : category id - quantity 

        public PmresultSinglerank()
        {
            Rank = null;
            Defaultcount = 0;
            Maxcount = 0;
            Absolutecount = 0;
            Mincount = 0;
            Reduce = 0;
            CameFrom = new Dictionary<int, Dictionary<int, int>>();
            Upcount = new Dictionary<int, Dictionary<int, int>>();
            CameFromUnited = new Dictionary<int, int>();
            UpcountUnited = new Dictionary<int, int>();
            ComethroughUnited = new Dictionary<int, int>();
            SumUnited = new Dictionary<int, int>();
            Civil = new Dictionary<int, int>();
        }
    }
}
