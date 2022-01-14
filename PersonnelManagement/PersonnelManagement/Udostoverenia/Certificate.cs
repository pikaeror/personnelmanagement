using System;
using System.Collections.Generic;

namespace PersonnelManagement.Udostoverenia
{
    public partial class Certificate
    {
        public int Id { get; set; }
        public int? Blank { get; set; }
        public int? Agency { get; set; }
        public string NumPersonal { get; set; }
        public string FullName { get; set; }
        public int? Post { get; set; }
        public int? NameOrganNazn { get; set; }
        public string NumOrderNazn { get; set; }
        public DateTime? DateOrderNazn { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? Rank { get; set; }
        public int? NameConferment { get; set; }
        public string NumOrderConferment { get; set; }
        public DateTime? DateConferment { get; set; }
        public int? Base { get; set; }
        public int? Status { get; set; }
        public int? NumUd { get; set; }
        public int? NumBlank { get; set; }
        public string CertificateCommitedBy1 { get; set; }
        public string CertificateCommitedBy2 { get; set; }
        public DateTime? CertificateCommitedDate { get; set; }
        public int? Issuingauthority { get; set; }
        public int? Rejectreason { get; set; }
        public string RankInspector { get; set; }
        public string Extra { get; set; }
        public sbyte? Exclusive { get; set; }
        public sbyte? Byadmin { get; set; }
    }
}
