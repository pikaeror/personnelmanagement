using System;
using System.Collections.Generic;

namespace PersonnelManagement.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public sbyte? Admin { get; set; }
        public int? Structure { get; set; }
        public sbyte? Structureeditor { get; set; }
        public sbyte? Masterpersonneleditor { get; set; }
        public sbyte? Personneleditor { get; set; }
        public int? Decree { get; set; }
        public sbyte? Positioncompact { get; set; }
        public DateTime? Date { get; set; }
        public sbyte? Sidebardisplay { get; set; }
        public string Currentstructuretree { get; set; }
        public sbyte Structureread { get; set; }
        public sbyte Personnelread { get; set; }
        public sbyte Mode { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int Positiontype { get; set; }
        public int Fullmode { get; set; }
    }
}
