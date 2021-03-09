using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class ListsEmptiPosModels
    {
        private List<Person> allPersonFromStructure;
        public int structureId;
        private Stack<Position> emptiPosCadets;
        private Stack<Position> emptiPosStudents;
        private Repository p_repository;
        public ListsEmptiPosModels(Repository repository, int structureId, List<Person> persons)
        {
            p_repository = repository;
            this.structureId = structureId;
            allPersonFromStructure = persons;
        }

        public void FillStacks(Persondecreeoperation persondecreeoperation)
        {
            emptiPosCadets = new Stack<Position>(GetAllFreePositionsFromStructure(structureId, allPersonFromStructure, persondecreeoperation, "Курсант").Reverse().ToList());
            emptiPosStudents = new Stack<Position>(GetAllFreePositionsFromStructure(structureId, allPersonFromStructure, persondecreeoperation, "Студент").Reverse().ToList());
        }

        public Position GetPosition(string str)
        {
            switch (str)
            {
                case "Курсант":
                    if(emptiPosCadets.ToList().Count > 0)
                    return emptiPosCadets.Pop();
                    break;
                case "Студент":
                    if (emptiPosStudents.ToList().Count > 0)
                        return emptiPosStudents.Pop();
                    break;
            }
            return null;    
        }

        public IEnumerable<Position> GetAllFreePositionsFromStructure(int structureId, List<Person> persons, Persondecreeoperation persondecreeoperation, string positionRank)
        {
            IEnumerable<int> positions = new List<int> { };
            persons.ForEach(el =>
            {
                positions = positions.Append(el.Position);
            });
            IEnumerable<int> enumerableId = new List<int> { };
            List<Position> output = p_repository.GetPositions(structureId, DateTime.Now).ToList();
            output.ForEach(el => enumerableId = enumerableId.Append(el.Id));
            IEnumerable<int> output_list_id = enumerableId.Except(positions);

            return output.Where(r => GetFreePositionsFromStructure(structureId, persondecreeoperation, positions, output_list_id, output, enumerableId, positionRank).Contains(r.Id));
        }

        public IEnumerable<int> GetFreePositionsFromStructure(int StructureId, Persondecreeoperation persondecreeoperation, IEnumerable<int> positions, IEnumerable<int> output_list_id, List<Position> output, IEnumerable<int> enumerableId, string positionRank)
        {
            IEnumerable<Persondecreeoperation> persondecreeoperations = p_repository.PersondecreeoperationsLocal().Values.Where(el => el.Persondecreeblock == persondecreeoperation.Persondecreeblock);
            persondecreeoperations.ToList().ForEach(el =>
            {
                positions = positions.Append(el.Optionnumber3);
            });

            enumerableId = new List<int> { };
            output = p_repository.GetPositions(StructureId, DateTime.Now).ToList();

            List<Position> positions1 = new List<Position> { };
            output.ForEach(el => {
                if (p_repository.GetPositionRank(el.Positiontype) == positionRank)
                {
                    positions1.Add(el);
                }
            });
            output = positions1;
            output.ForEach(el => enumerableId = enumerableId.Append(el.Id));
            output_list_id = enumerableId.Except(positions);

            return output_list_id;
        }

        public bool checkStructureForEmptess()
        {
            return true;
        }
    }
}
