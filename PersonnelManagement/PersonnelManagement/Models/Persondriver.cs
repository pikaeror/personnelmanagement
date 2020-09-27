using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Persondriver
    {
        public int Id { get; set; }
        public int Person { get; set; }
        public int Drivertype { get; set; }
        public string Series { get; set; }
        public string Number { get; set; }
        public DateTime Datestart { get; set; }
        public DateTime Dateend { get; set; }
        public int Drivercategory { get; set; }
    }
}
