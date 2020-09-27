using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Cabinetdata
    {
        public int Id { get; set; }
        public int Employeesid { get; set; }
        public int Creatorid { get; set; }
        public int Reasonid { get; set; }
        public string Usersurname { get; set; }
        public string Username { get; set; }
        public string Userpatronymic { get; set; }
        public string Userind { get; set; }
        public string Accesscode { get; set; }
        public int Status { get; set; }
        public string Denyreason { get; set; }
        public int Consent { get; set; }
        public DateTime Creationdate { get; set; }
    }
}
