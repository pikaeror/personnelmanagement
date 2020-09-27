using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Declarationdata
    {
        public int Id { get; set; }
        public int Cabinetid { get; set; }
        public int Declarationcabinetid { get; set; }
        public int Declarationtypeform { get; set; }
        public string Declarationsurname { get; set; }
        public string Declarationname { get; set; }
        public string Declarationpatronymic { get; set; }
        public DateTime Declarationdob { get; set; }
        public string Declarationind { get; set; }
        public string Declarationtypedoc { get; set; }
        public string Declarationdocseries { get; set; }
        public string Declarationdocnumber { get; set; }
        public string Declarationwhogivedoc { get; set; }
        public DateTime Declarationdateindocument { get; set; }
        public string Declarationaddress { get; set; }
        public string Declarationwork { get; set; }
        public string Declarationhomephonenumber { get; set; }
        public string Declarationworkphonenumber { get; set; }
        public string Declarationmobilephonenumber { get; set; }
        public string F1s1p31 { get; set; }
        public string F1s1p32 { get; set; }
        public string F1s1p33 { get; set; }
        public string F1s3p1 { get; set; }
        public int F1s3p2 { get; set; }
        public string F2s4p1 { get; set; }
        public string F2s5p1 { get; set; }
        public string F3s3p1 { get; set; }
        public int Declarationlockunlock { get; set; }
        public DateTime Declarationsignature { get; set; }
    }
}
