using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{

    public class StructureExpanded
    {
        public const int OrderMultiplier = 40;
        public const int OrderMax = 8;

        public int Id { get; set; }
        public string Name { get; set; }
        public int Parentstructure { get; set; }
        public sbyte? Featured { get; set; }
        public string Nameshortened { get; set; }
        public long Order { get; set; }
        public int Level { get; set; }
        public int ChildrenNumber { get; set; }
        public DateTime? Dateactive { get; set; }
        public DateTime? Dateinactive { get; set; }
        public sbyte DecreeSigned { get; set; }
        public string DecreeName { get; set; }
        public int Structuretype { get; set; }
        public int Structureregion { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Rank { get; set; }
        public string Grandparent { get; set; }
        public int Priority { get; set; }
        public int Subject1 { get; set; }
        public int Subject2 { get; set; }
        public int Subject3 { get; set; }
        public int Subject4 { get; set; }
        public int Subject5 { get; set; }
        public int Subject6 { get; set; }
        public int Subject7 { get; set; }
        public int Subject8 { get; set; }
        public int Subjectnumber { get; set; }
        public string Subjectnotice { get; set; }
        public int Subjectgender { get; set; }

        public int Changeorigin { get; set; }
        public int Changestructurelast { get; set; }
        public sbyte Changestructurerename { get; set; }
        public sbyte Changestructureall { get; set; }
        public sbyte Changestructurerank { get; set; }
        public sbyte Changestructurelocation { get; set; }
        public sbyte Changestructureparent { get; set; }

        public StructureExpanded(Structure structure)
        {
            Id = structure.Id;
            Name = structure.Name;
            Parentstructure = structure.Parentstructure;
            Featured = structure.Featured;
            Nameshortened = structure.Nameshortened;
            Structuretype = structure.Structuretype;
            Structureregion = structure.Structureregion;
            Rank = structure.Rank;
            City = structure.City;
            Street = structure.Street;
            Changeorigin = structure.Changeorigin;
            Changestructurelast = structure.Changestructurelast;
            Changestructurerename = structure.Changestructurerename;
            Changestructureall = structure.Changestructureall;
            Changestructurerank = structure.Changestructurerank;
            Changestructurelocation = structure.Changestructurelocation;
            Changestructureparent = structure.Changestructureparent;
            Priority = structure.Priority;
            Subject1 = structure.Subject1;
            Subject2 = structure.Subject2;
            Subject3 = structure.Subject3;
            Subject4 = structure.Subject4;
            Subject5 = structure.Subject5;
            Subject6 = structure.Subject6;
            Subject7 = structure.Subject7;
            Subject8 = structure.Subject8;
            Subjectnumber = structure.Subjectnumber;
            Subjectnotice = structure.Subjectnotice;
            Subjectgender = structure.Subjectgender;

            Order = 0;
            Level = 0;
            ChildrenNumber = 0;
        }

        public static List<Structure> StructuresExpandedToStructures(List<StructureExpanded> structureExpandeds)
        {
            List<Structure> structures = new List<Structure>();

            foreach (StructureExpanded structure in structureExpandeds)
            {
                Structure newStructure = new Structure();

                newStructure.Id = structure.Id;
                newStructure.Name = structure.Name;
                newStructure.Parentstructure = structure.Parentstructure;
                newStructure.Featured = structure.Featured;
                newStructure.Nameshortened = structure.Nameshortened;
                newStructure.Structuretype = structure.Structuretype;
                newStructure.Structureregion = structure.Structureregion;
                newStructure.Rank = structure.Rank;
                newStructure.City = structure.City;
                newStructure.Street = structure.Street;
                newStructure.Changeorigin = structure.Changeorigin;
                newStructure.Changestructurelast = structure.Changestructurelast;
                newStructure.Changestructurerename = structure.Changestructurerename;
                newStructure.Changestructureall = structure.Changestructureall;
                newStructure.Changestructurerank = structure.Changestructurerank;
                newStructure.Changestructurelocation = structure.Changestructurelocation;
                newStructure.Changestructureparent = structure.Changestructureparent;
                newStructure.Priority = structure.Priority;
                newStructure.Subject1 = newStructure.Subject1;
                newStructure.Subject2 = newStructure.Subject2;
                newStructure.Subject3 = newStructure.Subject3;
                newStructure.Subject4 = newStructure.Subject4;
                newStructure.Subject5 = newStructure.Subject5;
                newStructure.Subject6 = newStructure.Subject6;
                newStructure.Subject7 = newStructure.Subject7;
                newStructure.Subject8 = newStructure.Subject8;
                newStructure.Subjectnumber = newStructure.Subjectnumber;
                newStructure.Subjectnotice = newStructure.Subjectnotice;
                newStructure.Subjectgender = newStructure.Subjectgender;

                structures.Add(newStructure);
            }

            return structures;
        }

        public static List<StructureExpanded> StructuresToStructuresExpanded(List<Structure> structures, int featured, Repository repository)
        {
            List<StructureExpanded> structureExpandeds = new List<StructureExpanded>();

            structures = structures.GroupBy(x => x.Id).Select(x => x.First()).ToList(); // Clear duplicates.
            structures = structures.OrderBy(s => s.Id).ToList();
            repository.FilterStructuresByPriority(structures, featured);
            Dictionary<int, StructureExpanded> dictionary = new Dictionary<int, StructureExpanded>();
            int minLevel = 999;
            int maxLevel = 0;
            StructureExpandedOrder orderCurrent = new StructureExpandedOrder();

            foreach (Structure structure in structures)
            {
                StructureExpanded structureExpanded = new StructureExpanded(structure);
                int realID = structureExpanded.Id;
                if (structureExpanded.Changeorigin > 0)
                {
                    realID = structureExpanded.Changeorigin;
                }
                if (!dictionary.ContainsKey(realID))
                {
                    dictionary.Add(realID, structureExpanded); // duplicates
                } 
                
            }
            foreach (KeyValuePair<int, StructureExpanded> entry in dictionary)
            {
                int currentLevel = FindLevel(dictionary, entry.Value);
                if (currentLevel < minLevel)
                {
                    minLevel = currentLevel;
                }
                if (currentLevel > maxLevel)
                {
                    maxLevel = currentLevel;
                }
            }
            for (int level = minLevel; level <= maxLevel; level++)
            {
                OrderLevel(dictionary, level, orderCurrent);
            }
            structureExpandeds.AddRange(dictionary.Values);
            return structureExpandeds;
        }

        public static List<StructureExpanded> StructuresToStructuresExpandedStructureBlock(List<Structure> structures, int featured, Repository repository)
        {
            List<StructureExpanded> structureExpandeds = new List<StructureExpanded>();

            structures = structures.GroupBy(x => x.Id).Select(x => x.First()).ToList(); // Clear duplicates.
            structures = structures.OrderBy(s => s.Id).ToList();
            repository.FilterStructuresByPriority(structures, featured);
            Dictionary<int, StructureExpanded> dictionary = new Dictionary<int, StructureExpanded>();
            int minLevel = 999;
            int maxLevel = 0;
            StructureExpandedOrder orderCurrent = new StructureExpandedOrder();

            foreach (Structure structure in structures)
            {
                StructureExpanded structureExpanded = new StructureExpanded(structure);
                int realID = structureExpanded.Id;
                if (structureExpanded.Changeorigin > 0)
                {
                    realID = structureExpanded.Changeorigin;
                }
                if (!dictionary.ContainsKey(realID))
                {
                    dictionary.Add(realID, structureExpanded); // duplicates
                }

            }
            foreach (KeyValuePair<int, StructureExpanded> entry in dictionary)
            {
                int currentLevel = FindLevel(dictionary, entry.Value);
                if (currentLevel < minLevel)
                {
                    minLevel = currentLevel;
                }
                if (currentLevel > maxLevel)
                {
                    maxLevel = currentLevel;
                }
            }
            for (int level = minLevel; level <= maxLevel; level++)
            {
                OrderLevelStructureBlock(dictionary, level, orderCurrent);
            }
            structureExpandeds.AddRange(dictionary.Values);
            return structureExpandeds;
        }

        public static int FindLevel(Dictionary<int, StructureExpanded> dictionary, StructureExpanded structureExpanded)
        {
            if (structureExpanded.Parentstructure == 0)
            {
                structureExpanded.Level = 0;
                return 0;
            }
            int currentLevel = 0;
            foreach(KeyValuePair<int, StructureExpanded> entry in dictionary)
            {
                if (entry.Key == structureExpanded.Parentstructure)
                {
                    currentLevel = FindLevel(dictionary, entry.Value);
                    currentLevel++;
                }
            }
            structureExpanded.Level = currentLevel;
            return currentLevel;
        }

        // Дебажим структурный блок
        public static void OrderLevelStructureBlock(Dictionary<int, StructureExpanded> dictionary, int level, StructureExpandedOrder orderCurrent)
        {
            foreach (KeyValuePair<int, StructureExpanded> entry in dictionary)
            {
                if (entry.Value.Level == level)
                {
                    if (level == 0)
                    {
                        entry.Value.Order = orderCurrent.Order;
                        orderCurrent.Order += GenerateOrderBase(level);
                    }
                    else
                    {
                        StructureExpanded parent = dictionary[entry.Value.Parentstructure]; // Probably keep list of childs to order it?
                        parent.ChildrenNumber += 1;
                        entry.Value.Order = (GenerateOrderBase(level) * parent.ChildrenNumber) + parent.Order;
                    }
                }
            }
        }

        public static void OrderLevel(Dictionary<int, StructureExpanded> dictionary, int level, StructureExpandedOrder orderCurrent)
        {
            foreach (KeyValuePair<int, StructureExpanded> entry in dictionary)
            {
                if (entry.Value.Level == level)
                {
                    if (level == 0)
                    {
                        entry.Value.Order = orderCurrent.Order;
                        orderCurrent.Order += GenerateOrderBase(level);
                    }
                    else
                    {
                        StructureExpanded parent = dictionary[entry.Value.Parentstructure]; // Probably keep list of childs to order it?
                        parent.ChildrenNumber += 1;
                        entry.Value.Order = (GenerateOrderBase(level) * parent.ChildrenNumber) + parent.Order;
                    }
                }
            }
        }

        public static long GenerateOrderBase(int level)
        {
            int power = OrderMax - level;
            long val = 1;
            for (int iteration = 0; iteration <= power; iteration++)
            {
                val *= OrderMultiplier;
            }
            return val;
        }
    }
}
