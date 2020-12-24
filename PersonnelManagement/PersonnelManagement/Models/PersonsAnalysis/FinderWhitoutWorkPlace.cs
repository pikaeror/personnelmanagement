using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class FinderWhitoutWorkPlace
    {
        Repository m_repository;
        User m_user;

        List<List<Person>> m_output_resualt;
        List<Person> debug_delited, debug_will_delited;

        public FinderWhitoutWorkPlace(Repository repository, User user)
        {
            m_repository = repository;
            m_user = user;

            m_output_resualt = new List<List<Person>>();
            debug_delited = new List<Person>();
            debug_will_delited = new List<Person>();
        }

        public void Worker(int devizor)
        {
            List<Position> time = new List<Position>();
            time = GetAllPositions(m_user.Structure.GetValueOrDefault(), time);

            List<Position> delited_positions = FilterDelitedPositions(time);
            List<Position> will_delited_positions = FilterWillDelitedPositions(time);
            debug_delited = FindPerson(delited_positions);
            debug_will_delited = FindPerson(will_delited_positions);
            //OutputConfiguration(debug, devizor);
        }

        public List<List<Person>> GetResualt()
        {
            return m_output_resualt;
        }

        public List<Person> GetResualtList()
        {
            return debug_delited;
        }

        private List<Position> GetAllPositions(int actual_structure_id, List<Position> positions)
        {
            List<Structure> time_structure = m_repository.GetChildren(actual_structure_id).ToList();
            foreach(Structure iteration in time_structure)
            {
                foreach(Position position_iteration in m_repository.GetAllPositions(iteration.Id))
                {
                    positions.Add(position_iteration);
                }
                positions = GetAllPositions(iteration.Id, positions);
            }
            return positions;
        }

        private List<Position> FilterDelitedPositions(List<Position> positions)
        {
            List<Position> output = new List<Position>();
            List<Decreeoperation> time = new List<Decreeoperation>();
            foreach (Position i in positions)
            {
                time = m_repository.DecreeoperationsLocal().Values.Where(r => r.Subject == i.Id &&
                                                                              r.Dateactive <= m_user.Date.GetValueOrDefault() &&
                                                                              r.Deleted == 1 &&
                                                                              m_repository.DecreesLocal().Values.FirstOrDefault(t => t.Id == r.Decree).Signed == 1).ToList();
                if (time.Count > 0)
                    output.Add(i);
            }
            return output;
        }

        private List<Position> FilterWillDelitedPositions(List<Position> positions)
        {
            List<Position> output = new List<Position>();
            List<Decreeoperation> time = new List<Decreeoperation>();
            foreach (Position i in positions)
            {
                time = m_repository.DecreeoperationsLocal().Values.Where(r => r.Subject == i.Id &&
                                                                              r.Dateactive > m_user.Date.GetValueOrDefault() &&
                                                                              r.Deleted == 1 &&
                                                                              m_repository.DecreesLocal().Values.FirstOrDefault(t => t.Id == r.Decree).Signed == 1).ToList();
                if (time.Count > 0)
                    output.Add(i);
            }
            return output;
        }

        private List<Position> FilterAllDelitedPositions(List<Position> positions)
        {
            List<Position> output = new List<Position>();
            List<Decreeoperation> time = new List<Decreeoperation>();
            foreach (Position i in positions)
            {
                time = m_repository.DecreeoperationsLocal().Values.Where(r => r.Subject == i.Id &&
                                                                              r.Deleted == 1 &&
                                                                              m_repository.DecreesLocal().Values.FirstOrDefault(t => t.Id == r.Decree).Signed == 1).ToList();
                if (time.Count > 0)
                    output.Add(i);
            }
            return output;
        }

        private List<Person> FindPerson(List<Position> old_positions)
        {
            List<Person> output = new List<Person>();
            foreach(Position position in old_positions)
            {
                Person time = FindPerson(position);
                if (time != null)
                    output.Add(time);
            }
            return output;
        }

        private Person FindPerson(Position old_position)
        {
            Personjob job = m_repository.PersonjobsLocal().Values.FirstOrDefault(r => r.Position == old_position.Id && r.Actual == 1);
            return job != null ? m_repository.PersonsLocal().Values.FirstOrDefault(r => r.Id == job.Person) : null;
        }

        private void OutputConfiguration(List<Person> persons, int devizor)
        {
            List<Person> time = new List<Person>();
            for(int i = 0; i < persons.Count; i += devizor)
            {
                time.Clear();
                for(int j = 0; j < devizor && j + i < persons.Count; j++)
                {
                    time.Add(persons[i + j]);
                }
                m_output_resualt.Add(time);
            }
            return;
        }
    }
}
