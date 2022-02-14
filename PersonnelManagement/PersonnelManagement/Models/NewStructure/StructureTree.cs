using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using PersonnelManagement.USERS;

namespace PersonnelManagement.Models.NewStructure
{
    public class StructureTree : PersonnelManagement.Models.Structure
    {
        public List<StructureTree> children_structures { get; set; }
        public List<Position> actual_positions { get; set; }
        public StructureTree()
        {
            Id = 0;
            Name = "";
            Parentstructure = 0;
            Featured = 0;
            Nameshortened = "";
            Structuretype = 0;
            Structureregion = 0;
            City = "";
            Street = "";
            Rank = 0;
            Department = 0;
            Curator = 0;
            Head = 0;
            Changeorigin = 0;
            Changestructurelast = 0;
            Changestructurerename = 0;
            Changestructureall = 0;
            Changestructurerank = 0;
            Changestructurelocation = 0;
            Changestructureparent = 0;
            Priority = 0;
            Printreward = 0;
            Main = 0;
            Name1 = "";
            Name2 = "";
            Name3 = "";
            Name4 = "";
            Name5 = "";
            Name6 = "";
            Separatestructure = 0;
            Subject1 = 0;
            Subject2 = 0;
            Subject3 = 0;
            Subject4 = 0;
            Subject5 = 0;
            Subject6 = 0;
            Subject7 = 0;
            Subject8 = 0;
            Subject9 = 0;
            Subject10 = 0;
            Subject11 = 0;
            Subject12 = 0;
            Subject13 = 0;
            Subject14 = 0;
            Subject15 = 0;
            Subjectnumber = 0;
            Subjectnotice = "";
            Subjectgender = 0;
            Subjectindex = 0;

            children_structures = new List<StructureTree>();
            actual_positions = new List<Position>();
        }
        public StructureTree(Structure structure)
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

            children_structures = new List<StructureTree>();
            actual_positions = new List<Position>();
        }

        private void SetByStructure(Structure structure)
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

        public void GenerateCurrentTree(in USERS.User user, in orgContext orgContext, in userContext userContext)
        {
            List<Structure> all_structures = orgContext.Structure.ToList();
            List<Decree> all_decrees = orgContext.Decree.ToList();
            List<Structuredecreeoperation> all_operations = orgContext.Structuredecreeoperation.ToList();
            List<Rights> all_rights = userContext.Rights.ToList();

            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!DATA Base Done!!!!!!!!!!!!!!!!!!!!!!!!!!!!!" + DateTime.Now.ToString());
            this.GenerateCurrentTree(user, all_structures, all_decrees, all_operations, all_rights);
        }

        public void GenerateCurrentTree(in USERS.User user, in List<Structure> all_structures, in List<Decree> all_decrees, in List<Structuredecreeoperation> all_operations, in List<Rights> all_rights)
        {
            if (user.CanReadAllStructures(all_rights))
            {
                foreach (Structure str in all_structures.Where(r => r.Featured == 1))
                {
                    Tuple<bool, Structure> current = str.getActual(user, all_structures, all_decrees, all_operations);
                    if (!current.Item1) { continue; }
                    StructureTree time = new Models.NewStructure.StructureTree(str);
                    time.GenerateTreeById(str.Id, user, all_structures, all_decrees, all_operations);
                    children_structures.Add(time);
                }
                this.Sorted();
                return;
            }
            Tuple<bool, int> current_rules = user.CanReadStructures(all_rights);
            if (current_rules.Item1)
            {
                Tuple<bool, Structure> current = all_structures.First(r => r.Id == current_rules.Item2).getActual(user, all_structures, all_decrees, all_operations);
                if (!current.Item1) { return; }
                StructureTree time = new Models.NewStructure.StructureTree(current.Item2);
                time.GenerateTreeById(current.Item2.Id, user, all_structures, all_decrees, all_operations);
                children_structures.Add(time);
                /*this.SetByStructure(current.Item2);
                this.GenerateTreeByActual(current, user, all_structures, all_decrees, all_operations);*/
            }
            this.Sorted();
            return;
        }

        public void GenerateTreeById(int structure_id, in orgContext orgContext, in USERS.User user)
        {
            Tuple<bool, Structure> actual = orgContext.Structure.First(r => r.Id == structure_id).getActual(user, orgContext);
            this.GenerateTreeByActual(actual, orgContext, user);
            // if(!actual.Item1) { return; }
            // List<Structure> children = orgContext.Structure.Where(r => r.Parentstructure == actual.Item2.Id).ToList();
        }

        public void GenerateTreeById(int structure_id, in USERS.User user, in List<Structure> all_structures, in List<Decree> all_decrees, in List<Structuredecreeoperation> all_operations)
        {
            Tuple<bool, Structure> actual = all_structures.First(r => r.Id == structure_id).getActual(user, all_structures, all_decrees, all_operations);
            this.GenerateTreeByActual(actual, user, all_structures, all_decrees, all_operations);
        }

        public void GenerateTreeByActual(Tuple<bool, Structure> tuple, in orgContext orgContext, in USERS.User user)
        {
            if(!tuple.Item1) { return; }
            Dictionary<int, Structure> all = tuple.Item2.getAll(orgContext);
            List<Structure> children = orgContext.Structure.Where(r => all.ContainsKey(r.Parentstructure)).ToList();
            foreach(Structure child in children)
            {
                Tuple<bool, Structure> flag = child.getActual(user, orgContext);
                if(!flag.Item1) { continue; }
                if(this.children_structures.Where(r => r.Id == flag.Item2.Id).Count() != 0) { continue; }
                StructureTree time = new StructureTree(flag.Item2);
                time.GenerateTreeByActual(flag, orgContext, user);
                this.children_structures.Add(time);
            }
            this.Sorted();
        }

        public void GenerateTreeByActual(Tuple<bool, Structure> tuple, in USERS.User user, in List<Structure> all_structures, in List<Decree> all_decrees, in List<Structuredecreeoperation> all_operations)
        {
            if (!tuple.Item1) { return; }
            Dictionary<int, Structure> all = tuple.Item2.getAll(all_structures);
            List<Structure> children = all_structures.Where(r => all.ContainsKey(r.Parentstructure)).ToList();
            foreach (Structure child in children)
            {
                Tuple<bool, Structure> flag = child.getActual(user, all_structures, all_decrees, all_operations);
                if (!flag.Item1) { continue; }
                if (this.children_structures.Where(r => r.Id == flag.Item2.Id).Count() != 0) { continue; }
                StructureTree time = new StructureTree(flag.Item2);
                time.GenerateTreeByActual(flag, user, all_structures, all_decrees, all_operations);
                this.children_structures.Add(time);
            }
            this.Sorted();
        }

        private void Sorted()
        {
            children_structures.Sort((a, b) => a.Priority.CompareTo(b.Priority));
        }
    }
}
