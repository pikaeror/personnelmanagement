using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Personlanguage
    {
        public int Id { get; set; }
        public int Person { get; set; }
        public int Languagetype { get; set; }
        public int Languageskill { get; set; }
    }
}
