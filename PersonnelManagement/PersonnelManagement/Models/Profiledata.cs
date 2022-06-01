using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Profiledata
    {
        public int Id { get; set; }
        public int Cabinetid { get; set; }
        public string Profilesurname { get; set; }
        public string Profilename { get; set; }
        public string Profilepatronymic { get; set; }
        public string Profileothersurnames { get; set; }
        public DateTime Profiledob { get; set; }
        public string Profilepob { get; set; }
        public string Profilenationality { get; set; }
        public string Profileeducation { get; set; }
        public string Profilescholasticdegree { get; set; }
        public string Profiletreatise { get; set; }
        public string Profilelanguage { get; set; }
        public string Profileresponsibility { get; set; }
        public string Profilearmy { get; set; }
        public string Profilesport { get; set; }
        public string Profilepolitics { get; set; }
        public string Profileawards { get; set; }
        public string Profilepassportseries { get; set; }
        public string Profilepassportnumber { get; set; }
        public DateTime Profilepassportdate { get; set; }
        public string Profilepassportwhogive { get; set; }
        public string Profilepassportind { get; set; }
        public string Profiledriverlicenseseries { get; set; }
        public string Profiledriverlicensenumber { get; set; }
        public DateTime Profiledriverlicensedate { get; set; }
        public string Profiledriverlicensecategory { get; set; }
        public string Profiledriverlicenseexperience { get; set; }
        public string Profileaddress { get; set; }
        public string Profilehomephone { get; set; }
        public string Profileworkphone { get; set; }
        public string Profilemobilephone { get; set; }
        public int Profilelockunlock { get; set; }
        public DateTime Profilesignature { get; set; }
    }
}
