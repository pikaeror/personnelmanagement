using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class StructureToPositionsFinder
    {
        public Structure m_actual_structure { get; set; }
        public List<Structure> m_all_structures { get; set; }
    }
    public class StructureCustomEditor
    {
        private Repository m_repository { get; set; }
        private User m_user { get; set; }
        private pmContext m_context { get; set; }
        private DateTime m_date { get; set; }

        public StructureCustomEditor(Repository repository, User user)
        {
            m_repository = repository;
            m_user = user;
            m_context = m_repository.GetContext();
            m_date = m_user.Date.GetValueOrDefault();
        }

        public void PasteStructure(int structure_insert_id, int structure_to_insert_id)
        {
            List<StructureToPositionsFinder> structures_to_copy = getStructuresToCopy(structure_insert_id);
            Dictionary<int, List<Position>> positions_to_copy = getPositionsToCopy(structures_to_copy);

            insertStructure(structures_to_copy,
                positions_to_copy,
                m_user.Decree.GetValueOrDefault(),
                structure_insert_id,
                structure_to_insert_id);
        }

        private List<StructureToPositionsFinder> getStructuresToCopy(int structure_id)
        {
            IEnumerable<Structure> structures = m_context.Structure.ToArray();
            //Dictionary<int, Structure> all_structures = m_context.Structure.ToDictionary(r => r.Id);
            Structure structure = m_repository.GetActualStructureInfo(structure_id, m_date, structures),
                time_structure;
            int origin = structure.Changeorigin != 0 ? structure.Changeorigin : structure.Id;
            List<Structure> all_structures = structures.Where(r => r.Id != structure.Id && (r.Changeorigin == origin || r.Id == origin)).ToList();
            Dictionary<int, StructureToPositionsFinder> output = new Dictionary<int, StructureToPositionsFinder>();
            output.Add(structure.Id, new StructureToPositionsFinder()
            {
                m_actual_structure = structure,
                m_all_structures = new List<Structure>(all_structures)
            });
            foreach(Structure str in m_repository.GetChildrenList(structure.Id))
            {
                all_structures.Clear();
                time_structure = m_repository.GetActualStructureInfo(str.Id, m_date, structures);
                if (time_structure == null)
                    continue;
                origin = time_structure.Changeorigin != 0 ? time_structure.Changeorigin : time_structure.Id;
                all_structures = structures.Where(r => r.Id != time_structure.Id && (r.Changeorigin == origin || r.Id == origin)).ToList();
                if (time_structure != null && !output.ContainsKey(time_structure.Id))
                    output.Add(time_structure.Id, new StructureToPositionsFinder()
                    {
                        m_actual_structure = time_structure,
                        m_all_structures = new List<Structure>(all_structures)
                    });
            }
            return output.Values.ToList();
        }

        private Dictionary<int, List<Position>> getPositionsToCopy(List<StructureToPositionsFinder> structures)
        {
            Dictionary<int, List<Position>> output = new Dictionary<int, List<Position>>();
            Dictionary<int, Decree> decrees = m_context.Decree.Where(r => r.Signed == 1).ToDictionary(d => d.Id);
            Dictionary<int, int> operations_dic = new Dictionary<int, int>();
            foreach(Decreeoperation i in m_context.Decreeoperation.Where(
                r => decrees.ContainsKey(r.Decree) &&
                r.Subject > 0 &&
                r.Deleted == 1 &&
                r.Dateactive.GetValueOrDefault().Ticks <= m_date.Ticks))
            {
                if (!operations_dic.ContainsKey(i.Subject))
                    operations_dic[i.Subject] = i.Subject;
            }
            Dictionary<int, Position> positions = m_context.Position.Where(r => !operations_dic.ContainsKey(r.Id)).ToDictionary(r => r.Id);
            IEnumerable<Position> time_positions = new List<Position>();
            foreach (StructureToPositionsFinder i in structures)
            {
                time_positions = positions.Values.Where(r => r.Structure == i.m_actual_structure.Id);
                foreach(Structure j in i.m_all_structures)
                {
                    time_positions = time_positions.Concat(positions.Values.Where(r => r.Structure == j.Id));
                }
                output[i.m_actual_structure.Id] = time_positions.ToList();

            }
            return output;
        }

        private List<Structure> insertStructure(List<StructureToPositionsFinder> copyStructure,
            Dictionary<int, List<Position>> copyPositions,
            int actual_decree,
            int old_struct_copy,
            int new_struct_parent)
        {
            List<Structure> output = new List<Structure>();
            if (actual_decree == 0)
                return output;
            List<Decreeoperation> output_decree_operation = new List<Decreeoperation>();
            List<Position> output_positions = new List<Position>(), time_output_position = new List<Position>();
            Dictionary<int, List<Position>> test = new Dictionary<int, List<Position>>();
            Structure time_structure, time_structure_old;
            Position time_position, time_position_old;
            Decreeoperation time_decreeoperation;
            foreach(StructureToPositionsFinder i in copyStructure)
            {
                time_structure = new Structure(i.m_actual_structure);
                time_structure.Id = 0;
                time_structure.Parentstructure = 0;
                time_structure.Curator = 0;

                time_decreeoperation = new Decreeoperation() {
                    Id = 0,
                    Decree = actual_decree,
                    Subject = 0,
                    Created = 1
                };

                output.Add(new Structure(time_structure));
                output_decree_operation.Add(new Decreeoperation(time_decreeoperation));

                time_output_position.Clear();
                foreach (Position j in copyPositions[i.m_actual_structure.Id])
                {
                    time_position = new Position(j);
                    time_position.Id = 0;
                    time_position.Origin = 0;
                    time_position.Structure = 0;
                    time_position.Curator = 0;
                    time_position.Head = 0;
                    time_position.Curatorlist = "";
                    time_position.Headid = 0;
                    time_decreeoperation = new Decreeoperation()
                    {
                        Id = 0,
                        Decree = actual_decree,
                        Subject = 0,
                        Created = 1
                    };

                    output_positions.Add(new Position(time_position));
                    time_output_position.Add(new Position(time_position));
                    output_decree_operation.Add(new Decreeoperation(time_decreeoperation));
                }
                test.Add(i.m_actual_structure.Id, time_output_position);
                m_context.Position.AddRange(test[i.m_actual_structure.Id]);
            }

            m_context.Structure.AddRange(output);
            // m_context.Decreeoperation.AddRange(output_decree_operation);
            m_context.Position.AddRange(output_positions);
            m_context.SaveChanges();

            int count = 0;
            for(int i = 0; i < copyStructure.Count; i++)
            {
                time_structure = output.ElementAt(i);
                time_structure_old = copyStructure.ElementAt(i).m_actual_structure;
                time_decreeoperation = output_decree_operation.ElementAt(count);
                if(time_structure_old.Id == old_struct_copy)
                {
                    time_structure.Parentstructure = new_struct_parent;
                } else
                {
                    int parent = copyStructure.FindIndex(r => r.m_actual_structure.Id == time_structure_old.Parentstructure);
                    time_structure_old.Parentstructure = output.ElementAt(parent).Id;
                }



                time_decreeoperation.Subject = -time_structure.Id;

                output_decree_operation[count] = time_decreeoperation;
                count++;
            }
            return output;
        }
    }
}
