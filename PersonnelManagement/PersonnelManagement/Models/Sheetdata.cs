using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Sheetdata
    {
        public int Id { get; set; }
        public int Cabinetid { get; set; }
        public string Sheetsurname { get; set; }
        public string Sheetname { get; set; }
        public string Sheetpatronymic { get; set; }
        public string Sheetsex { get; set; }
        public DateTime Sheetdob { get; set; }
        public string Sheetpob { get; set; }
        public string Sheetnationality { get; set; }
        public string Sheeteducation { get; set; }
        public string Sheetlanguage { get; set; }
        public string Sheetdegree { get; set; }
        public string Sheetinventions { get; set; }
        public string Sheettradeunion { get; set; }
        public string Sheetaward { get; set; }
        public string Sheetarmy { get; set; }
        public string Sheetarmyeducation { get; set; }
        public string Sheetfamily { get; set; }
        public string Sheetotherdata { get; set; }
        public string Sheetaddress { get; set; }
        public string Sheethomephone { get; set; }
        public string Sheetworkphone { get; set; }
        public string Sheetmobilephone { get; set; }
        public string Sheetpassportseries { get; set; }
        public string Sheetpassportnumber { get; set; }
        public string Sheetwhogivepassport { get; set; }
        public DateTime Sheetpassportdatein { get; set; }
        public string Sheetpassportind { get; set; }
        public int Sheetlockunlock { get; set; }
        public DateTime Sheetsignature { get; set; }
    }
}
