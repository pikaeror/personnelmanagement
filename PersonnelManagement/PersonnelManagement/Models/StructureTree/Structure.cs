using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PersonnelManagement.Models
{
    public partial class Structure
    {
        public Structure() { }

        public Structure(Structure structure)
        {
            Id = structure.Id;
            Name = structure.Name;
            Parentstructure = structure.Parentstructure;
            Featured = structure.Featured;
            Nameshortened = structure.Nameshortened;
            Structuretype = structure.Structuretype;
            Structureregion = structure.Structureregion;
            City = structure.City;
            Street = structure.Street;
            Rank = structure.Rank;
            Department = structure.Department;
            Curator = structure.Curator;
            Head = structure.Head;
            Changeorigin = structure.Changeorigin;
            Changestructurelast = structure.Changestructurelast;
            Changestructurerename = structure.Changestructurerename;
            Changestructureall = structure.Changestructureall;
            Changestructurerank = structure.Changestructurerank;
            Changestructurelocation = structure.Changestructurelocation;
            Changestructureparent = structure.Changestructureparent;
            Priority = structure.Priority;
            Printreward = structure.Printreward;
            Main = structure.Main;
            Name1 = structure.Name1;
            Name2 = structure.Name2;
            Name3 = structure.Name3;
            Name4 = structure.Name4;
            Name5 = structure.Name5;
            Name6 = structure.Name6;
            Separatestructure = structure.Separatestructure;
            Subject1 = structure.Subject1;
            Subject2 = structure.Subject2;
            Subject3 = structure.Subject3;
            Subject4 = structure.Subject4;
            Subject5 = structure.Subject5;
            Subject6 = structure.Subject6;
            Subject7 = structure.Subject7;
            Subject8 = structure.Subject8;
            Subject9 = structure.Subject9;
            Subject10 = structure.Subject10;
            Subject11 = structure.Subject11;
            Subject12 = structure.Subject12;
            Subject13 = structure.Subject13;
            Subject14 = structure.Subject14;
            Subject15 = structure.Subject15;
            Subjectnumber = structure.Subjectnumber;
            Subjectnotice = structure.Subjectnotice;
            Subjectgender = structure.Subjectgender;
            Subjectindex = structure.Subjectindex;
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
                Subject15 };
            return output;
        }

        public string getfullname(orgContext orgContext, DateTime current_date)
        {

            return "";
        }

        public Tuple<bool, Structure> getActual(USERS.User user, orgContext orgContext)
        {
            long tiks = user.Date.GetValueOrDefault().Ticks;
            Dictionary<int, Decree> decrees = orgContext.Decree.Where(r => r.Signed == 1).ToDictionary(d => d.Id);
            Dictionary<uint, Structure> structures = this.getAll(orgContext).Values.ToDictionary(d => (uint)d.Id);
            List<Structuredecreeoperation> structuredecreeoperations = orgContext.Structuredecreeoperation.Where(r => (
                structures.ContainsKey(r.Currentstructure) &&
                decrees.ContainsKey(r.Decree) /*&&
                    ((r.Created == 1 && r.Dateactive.GetValueOrDefault().Ticks <= tiks) ||
                    *//*(r.Deleted == 1 && r.Dateactive.GetValueOrDefault().Ticks > tiks) ||*//*
                    (r.Changed == 1 && r.Dateactive.GetValueOrDefault().Ticks <= tiks))*/
            )).ToList();
            structuredecreeoperations.Sort((a, b) => a.Dateactive.GetValueOrDefault().Ticks.CompareTo(b.Dateactive.GetValueOrDefault().Ticks));
            structuredecreeoperations = structuredecreeoperations.Where(r => r.Dateactive.GetValueOrDefault().Ticks <= tiks).ToList();
            if(structuredecreeoperations.Count == 0 ||
                !structures.ContainsKey(structuredecreeoperations.Last().Currentstructure)) { return Tuple.Create(false, this); }
            /*long min_delta = long.MaxValue, deltadist;
            uint outputId = structuredecreeoperations.First().Currentstructure;
            foreach(Structuredecreeoperation operation in structuredecreeoperations)
            {
                deltadist = tiks - operation.Dateactive.GetValueOrDefault().Ticks;
                if(deltadist < min_delta || (operation.Changed == 1 && deltadist <= min_delta))
                {
                    min_delta = deltadist;
                    outputId = operation.Currentstructure;
                }
            }*/
            return Tuple.Create(structures.ContainsKey(structuredecreeoperations.Last().Currentstructure), structures[structuredecreeoperations.Last().Currentstructure]);
        }

        public Tuple<bool, Structure> getActual(USERS.User user, in List<Structure> all_structures, in List<Decree> all_decrees, in List<Structuredecreeoperation> all_operations)
        {
            long tiks = user.Date.GetValueOrDefault().Ticks;
            Dictionary<int, Decree> decrees = all_decrees.Where(r => r.Signed == 1).ToDictionary(d => d.Id);
            Dictionary<uint, Structure> structures = this.getAll(all_structures).Values.ToDictionary(d => (uint)d.Id);
            List<Structuredecreeoperation> structuredecreeoperations = all_operations.Where(r => (
                structures.ContainsKey(r.Currentstructure) &&
                decrees.ContainsKey(r.Decree)
            )).ToList();
            structuredecreeoperations.Sort((a, b) => a.Dateactive.GetValueOrDefault().Ticks.CompareTo(b.Dateactive.GetValueOrDefault().Ticks));
            structuredecreeoperations = structuredecreeoperations.Where(r => r.Dateactive.GetValueOrDefault().Ticks <= tiks).ToList();
            if (structuredecreeoperations.Count == 0 ||
                !structures.ContainsKey(structuredecreeoperations.Last().Currentstructure)) { return Tuple.Create(false, this); }
            return Tuple.Create(structures.ContainsKey(structuredecreeoperations.Last().Currentstructure), structures[structuredecreeoperations.Last().Currentstructure]);
        }

        public Tuple<bool, Structure> getActual(USERS.User user, in List<Structure> all_structures, in Dictionary<int, Decree> signed_decrees, in List<Structuredecreeoperation> all_operations)
        {
            long tiks = user.Date.GetValueOrDefault().Ticks;
            Dictionary<int, Decree> decrees = signed_decrees;
            Dictionary<uint, Structure> structures = this.getAll(all_structures).Values.ToDictionary(d => (uint)d.Id);
            List<Structuredecreeoperation> structuredecreeoperations = all_operations.Where(r => (
                structures.ContainsKey(r.Currentstructure) &&
                decrees.ContainsKey(r.Decree)
            )).ToList();
            structuredecreeoperations.Sort((a, b) => a.Dateactive.GetValueOrDefault().Ticks.CompareTo(b.Dateactive.GetValueOrDefault().Ticks));
            structuredecreeoperations = structuredecreeoperations.Where(r => r.Dateactive.GetValueOrDefault().Ticks <= tiks).ToList();
            if (structuredecreeoperations.Count == 0 ||
                !structures.ContainsKey(structuredecreeoperations.Last().Currentstructure)) { return Tuple.Create(false, this); }
            return Tuple.Create(structures.ContainsKey(structuredecreeoperations.Last().Currentstructure), structures[structuredecreeoperations.Last().Currentstructure]);
        }

        public Structure getOriginal(orgContext orgContext)
        {
            if(this.Changeorigin == 0) { return this; }
            IEnumerable<Structure> time = orgContext.Structure.Where(r => r.Id == this.Changeorigin);
            if (time.Count() == 0) { return this; }
            return time.First(r => r.Id == this.Changeorigin);
        }

        public Structure getOriginal(in List<Structure> datebase_structures)
        {
            if (this.Changeorigin == 0) { return this; }
            IEnumerable<Structure> time = datebase_structures.Where(r => r.Id == this.Changeorigin);
            if (time.Count() == 0) { return this; }
            return time.First(r => r.Id == this.Changeorigin);
        }

        public Dictionary<int, Structure> getAll(orgContext orgContext)
        {
            Structure origin = this.getOriginal(orgContext);
            Dictionary<int, Structure> structures = orgContext.Structure.Where(r => r.Changeorigin == origin.Id).ToDictionary(d => d.Id);
            structures.Add(origin.Id, origin);
            return structures;
        }

        public Dictionary<int, Structure> getAll(in List<Structure> datebase_structures)
        {
            Structure origin = getOriginal(datebase_structures);
            Dictionary<int, Structure> structures = datebase_structures.Where(r => r.Changeorigin == origin.Id).ToDictionary(d => d.Id);
            structures.Add(origin.Id, origin);
            return structures;
        }
    }
}
