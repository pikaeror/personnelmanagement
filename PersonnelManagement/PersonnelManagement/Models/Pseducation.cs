using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Pseducation
    {
        public int Id { get; set; }
        public int Cabinetid { get; set; }
        public string Organization { get; set; }
        public string Organizationcity { get; set; }
        public string Facultyeducation { get; set; }
        public int Yearstart { get; set; }
        public int Yearend { get; set; }
        public string Formeducation { get; set; }
        public string Specialtyeducation { get; set; }
    }
}
