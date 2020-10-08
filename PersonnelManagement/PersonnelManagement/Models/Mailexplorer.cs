using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class Mailexplorer
    {
        public int Id { get; set; }
        public uint FolderCreator { get; set; }
        public uint FolderOwner { get; set; }
        public string AccessForReading { get; set; }
    }
}
