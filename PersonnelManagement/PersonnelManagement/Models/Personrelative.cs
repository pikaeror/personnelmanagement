using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Personrelative
    {
        public int Id { get; set; }
        public int Person { get; set; }
        public int Relativetype { get; set; }
        public string Fio { get; set; }
        public string Fioother { get; set; }
        public DateTime Birthday { get; set; }
        public string Birthplace { get; set; }
        public string Jobplace { get; set; }
        public string Jobposition { get; set; }
        public string Livecountry { get; set; }
        public string Livestate { get; set; }
        public string Livesubstate { get; set; }
        public int Livecitysubstate { get; set; }
        public string Livecitytype { get; set; }
        public string Livecity { get; set; }
        public string Livestreettype { get; set; }
        public string Livestreet { get; set; }
        public string Livehouse { get; set; }
        public string Livehousing { get; set; }
        public string Liveflat { get; set; }
        public string Birthcountry { get; set; }
        public string Birthstate { get; set; }
        public string Birthsubstate { get; set; }
        public int Birthcitysubstate { get; set; }
        public string Birthcitytype { get; set; }
        public string Birthcity { get; set; }
        public string Birthadditional { get; set; }
        public sbyte Nodata { get; set; }
        public sbyte Death { get; set; }
        public sbyte Deathnodata { get; set; }
        public string Deathcountry { get; set; }
        public string Deathstate { get; set; }
        public string Deathsubstate { get; set; }
        public int Deathcitysubstate { get; set; }
        public string Deathcitytype { get; set; }
        public string Deathcity { get; set; }
        public string Deathadditional { get; set; }
    }
}
