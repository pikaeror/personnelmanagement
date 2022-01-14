using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class ELDRequestWorker
    {
        private User user;
        private readonly Repository p_repository;

        public ELDRequestWorker(Repository repository, User user)
        {
            p_repository = repository;
            this.user = user;
        }

        public List<string> Get_all_cvalifications()
        {
            List<string> all_cvalifications = new List<string>();
            List<PersonManager> personsUnfiltered = p_repository.GetAllPersons(user, true, true);
            List<PersonManager> persons = new List<PersonManager>();
            foreach (PersonManager person in personsUnfiltered)
            {
                bool isAllowedToReadPerson = p_repository.isAllowedToReadPerson(user, person.Id);
                if (isAllowedToReadPerson)
                {
                    persons.Add(person);
                }
            }
            List<Personeducation> personeducations = p_repository.Personeducations.ToList();

            foreach(Personeducation personeducation in personeducations)
            {
                foreach (PersonManager person in persons)
                {
                    if (personeducation.Person == person.Id)
                        all_cvalifications.Add(personeducation.Qualification.ToLower()); 
                }
            }
            return all_cvalifications.Distinct().ToList();
        }

        public List<string> Get_all_specializations()
        {
            List<string> all_specializations = new List<string>();
            List<PersonManager> personsUnfiltered = p_repository.GetAllPersons(user, true, true);
            List<PersonManager> persons = new List<PersonManager>();
            foreach (PersonManager person in personsUnfiltered)
            {
                bool isAllowedToReadPerson = p_repository.isAllowedToReadPerson(user, person.Id);
                if (isAllowedToReadPerson)
                {
                    persons.Add(person);
                }
            }

            List<Personeducation> personeducations = p_repository.Personeducations.ToList();

            foreach (Personeducation personeducation in personeducations)
            {
                foreach (PersonManager person in persons)
                {
                    if (personeducation.Person == person.Id)
                        all_specializations.Add(personeducation.Speciality.ToLower());
                }
            }
            return all_specializations.Distinct().ToList();
        }

        public List<string> Get_all_levels()
        {
            p_repository.UpdateEducationlevelsLocal();
            List<string> all_levels = new List<string>();
            List<Educationlevel> educationlevels = p_repository.EducationlevelsLocal().Values.ToList();

            foreach (Educationlevel educationlevel in educationlevels)
            {
                all_levels.Add(educationlevel.Levelname);
            }
            return all_levels;
        }

        public void GetEducation(Education_Request education_Request)
        {
            List<Position> positions = new List<Position>();


            foreach (StructureTree structureTree in education_Request.Current_structure)
            {
                positions.Concat(p_repository.GetAllPositions(structureTree.Id));
            }
        }
    }
}

