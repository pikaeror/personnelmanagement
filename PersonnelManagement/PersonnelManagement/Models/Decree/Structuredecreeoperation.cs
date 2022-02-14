using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonnelManagement.Models
{
    public partial class Structuredecreeoperation
    {
        [NotMapped]
        public string full_structure_name { get; set; }
        [NotMapped]
        public string priveuse_name { get; set; }

        public bool generateByDecree(ref Decree decree, IEnumerable<Structure> structures, IEnumerable<Position> positions, Repository repo, DateTime time)
        {
            if (decree.Id != this.Decree)
                return false;
            full_structure_name = repo.GetStructureName(structures.FirstOrDefault(r => r.Id == this.Currentstructure), time);
            priveuse_name = "";
            return true;
        }

        public Structuredecreeoperation() { }

        public Structuredecreeoperation(Decreeoperation input)
        {
            this.Decree = input.Decree;
            this.Currentstructure = (uint)Math.Abs(input.Subject);
            this.Created = input.Created;
            this.Deleted = input.Deleted;
            this.Changed = input.Changed;
            this.Priveusestructure = (uint)input.Changedtype;
            this.Dateactive = input.Dateactive;
            this.Datecustom = input.Datecustom;
        }

        public Structuredecreeoperation(Decreeoperation input, ref Structure structure)
        {
            this.Decree = input.Decree;
            this.Currentstructure = (uint)Math.Abs(input.Subject);
            this.Created = input.Created;
            this.Deleted = input.Deleted;
            this.Changed = input.Changed;
            this.Priveusestructure = (uint)structure.Id;
            this.Dateactive = input.Dateactive;
            this.Datecustom = input.Datecustom;
        }
    }
}
