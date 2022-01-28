using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonnelManagement.Models
{
    public partial class Positiondecreeoperation
    {
        [NotMapped]
        public string full_structure_name { get; set; }
        [NotMapped]
        public string current_name { get; set; }
        [NotMapped]
        public string priveuse_name { get; set; }

        public bool generateByDecree(ref Decree decree, IEnumerable<Structure> structures, IEnumerable<Position> positions, Repository repo, DateTime time)
        {
            if (decree.Id != this.Decree)
                return false;
            full_structure_name = repo.GetStructureName(structures.FirstOrDefault(r => r.Id == this.Currentstructure), time);
            current_name = "";
            priveuse_name = "";
            return true;
        }

        public Positiondecreeoperation(Decreeoperation input, ref Structure structure)
        {
            this.Decree = input.Decree;
            this.Currentposition = (uint)input.Subject;
            this.Created = input.Created;
            this.Deleted = input.Deleted;
            this.Changed = 0;
            this.Priviuseosition = (uint)0;
            this.Dateactive = input.Dateactive;
            this.Datecustom = input.Datecustom;
            this.Currentstructure = (uint)structure.Id;
        }

        public Positiondecreeoperation(Decreeoperation input, ref Structure structure, ref Position position)
        {
            this.Decree = input.Decree;
            this.Currentposition = (uint)input.Subject;
            this.Created = 0;
            this.Deleted = 0;
            this.Changed = 1;
            this.Priviuseosition = (uint)position.Id;
            this.Dateactive = input.Dateactive;
            this.Datecustom = input.Datecustom;
            this.Currentstructure = (uint)structure.Id;
        }
    }
}
