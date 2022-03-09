using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public partial class Position
    {
        [NotMapped]
        public Positiontype positiontype_data { get; set; }
        [NotMapped]
        public Decree last_decree { get; set; }
        [NotMapped]
        public Positiondecreeoperation last_decreeoperation { get; set; }
        public Position() { }

        public Position(Position position)
        {
            Id = position.Id;
            Cap = position.Cap;
            Dateactive = position.Dateactive;
            Dateinactive = position.Dateinactive;
            Sourceoffinancing = position.Sourceoffinancing;
            Positiontype = position.Positiontype;
            Notice = position.Notice;
            Positioncategory = position.Positioncategory;
            Replacedbycivil = position.Replacedbycivil;
            Replacedbycivilpositioncategory = position.Replacedbycivilpositioncategory;
            Replacedbycivilpositiontype = position.Replacedbycivilpositiontype;
            Altrank = position.Altrank;
            Origin = position.Origin;
            Decertificate = position.Decertificate;
            Decertificatedate = position.Decertificatedate;
            Civilranklow = position.Civilranklow;
            Civilrankhigh = position.Civilrankhigh;
            Replacedbycivildatelimit = position.Replacedbycivildatelimit;
            Replacedbycivildate = position.Replacedbycivildate;
            Structure = position.Structure;
            Civildecree = position.Civildecree;
            Civildecreenumber = position.Civildecreenumber;
            Civildecreedate = position.Civildecreedate;
            Curator = position.Curator;
            Head = position.Head;
            Curatorlist = position.Curatorlist;
            Headid = position.Headid;
            Opchs = position.Opchs;
            Part = position.Part;
            Partval = position.Partval;
            Subject1 = position.Subject1;
            Subject2 = position.Subject2;
            Subject3 = position.Subject3;
            Subject4 = position.Subject4;
            Subject5 = position.Subject5;
            Subject6 = position.Subject6;
            Subject7 = position.Subject7;
            Subject8 = position.Subject8;
            Subject9 = position.Subject9;
            Subject10 = position.Subject10;
            Subject11 = position.Subject11;
            Subject12 = position.Subject12;
            Subject13 = position.Subject13;
            Subject14 = position.Subject14;
            Subject15 = position.Subject15;
            Subject16 = position.Subject16;
            Subject17 = position.Subject17;
            Subject18 = position.Subject18;
            Subject19 = position.Subject19;
            Subject20 = position.Subject20;
            Name1 = position.Name1;
            Name2 = position.Name2;
            Name3 = position.Name3;
            Name4 = position.Name4;
            Name5 = position.Name5;
            Name6 = position.Name6;
            Subjectindex = position.Subjectindex;

            positiontype_data = position.positiontype_data;
            last_decree = position.last_decree;
            last_decreeoperation = position.last_decreeoperation;
        }

        public List<int> getSubjectsList()
        {
            List<int> output = new List<int> { Subject1,
                Subject2,
                Subject3,
                Subject4,
                Subject5,
                Subject6,
                Subject7,
                Subject8,
                Subject9,
                Subject10,
                Subject11,
                Subject12,
                Subject13,
                Subject14,
                Subject15,
                Subject16,
                Subject17,
                Subject18,
                Subject19,
                Subject20 };
            return output;
        }

        /*public Position getActual(in USERS.User user, in List<Position> all_positions, in List<Decree> all_decree, in List<Positiondecreeoperation> all_operations)
        {

        }*/
        public bool isActual(in USERS.User user, in List<Positiondecreeoperation> all_decree_operation, in Dictionary<int, Decree> decrees, in Dictionary<int, Positiontype> positiontype)
        {
            long current_tick = user.Date.GetValueOrDefault().Ticks;
            IEnumerable<Positiondecreeoperation> operation_by_pos = all_decree_operation.Where(r => r.Currentposition == (uint)this.Id);
            /*if(operation_by_pos.Count > 1)
            {
                int k = 0;
            }*/
            List<Positiondecreeoperation> operation_by_poss = operation_by_pos.Where(r => r.Dateactive.GetValueOrDefault().Ticks < current_tick).ToList();
            if (operation_by_poss.Count() == 0) { return false; }
            if (operation_by_poss.Count() > 1) { operation_by_poss.Sort((a, b) => a.Dateactive.GetValueOrDefault().Ticks.CompareTo(b.Dateactive.GetValueOrDefault().Ticks)); }
            bool output = operation_by_poss.Last().Deleted == 0;
            if(output) {
                this.setType(positiontype);
                this.setDecree(operation_by_poss.Last(), decrees);
            }
            return output;
        }

        public bool isActual(in USERS.User user, in List<Positiondecreeoperation> all_decree_operation, in List<Decree> decrees, in Dictionary<int, Positiontype> positiontype)
        {
            long current_tick = user.Date.GetValueOrDefault().Ticks;
            IEnumerable<Positiondecreeoperation> operation_by_pos = all_decree_operation.Where(r => r.Currentposition == (uint)this.Id);
            /*if(operation_by_pos.Count > 1)
            {
                int k = 0;
            }*/
            List<Positiondecreeoperation> operation_by_poss = operation_by_pos.Where(r => r.Dateactive.GetValueOrDefault().Ticks < current_tick).ToList();
            if (operation_by_poss.Count() == 0) { return false; }
            if (operation_by_poss.Count() > 1) { operation_by_poss.Sort((a, b) => a.Dateactive.GetValueOrDefault().Ticks.CompareTo(b.Dateactive.GetValueOrDefault().Ticks)); }
            bool output = operation_by_poss.Last().Deleted == 0;
            if (output)
            {
                this.setType(positiontype);
                this.setDecree(operation_by_poss.Last(), decrees.ToDictionary(r => r.Id));
            }
            return output;
        }
        public Position getOriginal(orgContext orgContext)
        {
            if (this.Origin == this.Id) { return this; }
            IEnumerable<Position> time = orgContext.Position.Where(r => r.Id == this.Origin);
            if (time.Count() == 0) { return this; }
            return time.First(r => r.Id == this.Origin);
        }

        public Position getOriginal(in List<Position> datebase_positions)
        {
            if (this.Origin == this.Id) { return this; }
            IEnumerable<Position> time = datebase_positions.Where(r => r.Id == this.Origin);
            if (time.Count() == 0) { return this; }
            return time.First(r => r.Id == this.Origin);
        }

        private void setType(in Dictionary<int, Positiontype> positiontype)
        {
            if (positiontype.ContainsKey(this.Positiontype)) { positiontype_data = positiontype[this.Positiontype]; }
            else { positiontype_data = new Positiontype(); }
            return;
        }

        private void setDecree(in Positiondecreeoperation positiondecreeoperation, in Dictionary<int, Decree> decrees)
        {
            last_decreeoperation = positiondecreeoperation;
            if(decrees.ContainsKey(positiondecreeoperation.Decree)) { last_decree = decrees[positiondecreeoperation.Decree]; }
            return;
        }
    }
}
