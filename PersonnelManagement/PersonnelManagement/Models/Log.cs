using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Log
    {
        public int Id { get; set; }
        public int? PersonId { get; set; }
        public string Text { get; set; }

        public Person Person { get; set; }
    }
}
