using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Personreward
    {
        public int Id { get; set; }
        public int Person { get; set; }
        public int Rewardtype { get; set; }
        public int Reward { get; set; }
        public string Reason { get; set; }
        public string Order { get; set; }
        public string Ordernumbertype { get; set; }
        public DateTime Rewarddate { get; set; }
        public string Optionstring1 { get; set; }
        public int Optionnumber1 { get; set; }
        public string Optionstring2 { get; set; }
        public int Optionnumber2 { get; set; }
        public string Orderwho { get; set; }
        public int Orderwhoid { get; set; }
        public int Orderid { get; set; }
        public string Externalorderwhotype { get; set; }
        public int Externalordertype { get; set; }
        public int Area { get; set; }
        public int Areaother { get; set; }
    }
}
