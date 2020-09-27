using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Educationtypeblock
    {
        public int Id { get; set; }
        public int Personeducation { get; set; }
        public int Educationtype { get; set; }
        public int PersoneducationId { get; set; }
    }
}
