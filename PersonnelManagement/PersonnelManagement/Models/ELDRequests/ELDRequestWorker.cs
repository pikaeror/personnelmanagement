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
        private List<int> structuresId = new List<int>();
        private List<int> temporaryStructuresIdList = new List<int>();

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
        public List<Education_Respons> GetEducation(Education_Request education_Request)
        {
            List<Education_Respons> education_Respons = new List<Education_Respons>();

            List<Person> allpeople = GetAllPersonFromStructure(education_Request.Current_structure.ToList());

            if (education_Request.CvalificationList.Count == 0)
                education_Request.CvalificationList.Add(" ");

            if (education_Request.EducationLevel.Count == 0)
                education_Request.EducationLevel.Add(" ");

            if (education_Request.SpecializationList.Count == 0)
                education_Request.SpecializationList.Add(" ");

            foreach (Person person in allpeople)
            {
                List<Personeducation> personeducations = p_repository.GetContext().Personeducation.Where(el => el.Person == person.Id).ToList();

                foreach(Personeducation personeducation in personeducations) {
                    foreach (string specializationList in education_Request.SpecializationList)
                    {
                        if (personeducation.Speciality == specializationList || specializationList == " ")
                            foreach (string cvalificationList in education_Request.CvalificationList)
                            {
                                if (personeducation.Qualification == cvalificationList || cvalificationList == " ")
                                    foreach (string educationLevelstring in education_Request.EducationLevel)
                                    {
                                        Educationlevel educationlevel = p_repository.GetContext().Educationlevel.First(el => el.Id == personeducation.Educationlevel);
                                        if (educationlevel.Levelname == educationLevelstring || educationLevelstring == " ")
                                        {
                                            education_Respons.Add(new Education_Respons(person, personeducation));
                                        }
                                    }
                            }
                    }
                }
            }
            return education_Respons;
        }

        public List<Rank_respons> GetRank(Rank_request rank_Request)
        {
            List<Rank_respons> rank_Respons = new List<Rank_respons>();

            List<Person> allpeople = GetAllPersonFromStructure(rank_Request.Current_structure.ToList());

            Personrank actualRank;

            DateTime datestartrank = new DateTime();

            DateTime dateuprank = new DateTime();

            DateTime datenow = DateTime.Now;

            Rank rankPerson = new Rank();

            foreach (Person person in allpeople)
            {
                actualRank = GetActualRank(person);
                if (actualRank == null)
                    continue;

                rankPerson = p_repository.GetContext().Rank.ToList().First(el => el.Name == actualRank.Rankstring);

                if (rank_Request.Corelate_rank)
                {
                    Position position = p_repository.GetContext().Position.ToList().First(el => el.Id == person.Position);
                    Rank rankPosition = p_repository.GetContext().Rank.ToList().First(el => el.Id == position.Cap);
                    
                    if (rankPosition.Order >= rankPerson.Order)
                        continue;
                }

                if(actualRank.Datestart != null)
                {
                    datestartrank = (DateTime)actualRank.Datestart;
                    dateuprank = datestartrank.AddYears((int)rankPerson.MaxPeriod);


                }
                else
                {
                    datestartrank = actualRank.Decreedate;
                    dateuprank = datestartrank.AddYears((int)rankPerson.MaxPeriod);

                }
            }

            return rank_Respons;
        }

        public List<Person> GetAllPersonFromStructure(List<StructureTree> structuresTrees)
        {
            List<Position> allpositions = new List<Position>();

            List<Person> allpeople = new List<Person>();

            foreach (StructureTree structureTree in structuresTrees)
            {
                temporaryStructuresIdList.Add(structureTree.Id);
            }

            GetAllPositions();

            foreach (int structureId in structuresId)
            {
                allpositions = allpositions.Union(p_repository.GetAllPositions(structureId)).ToList();
            }

            foreach (Person person in p_repository.PersonsLocal().Values.ToList())
            {
                foreach (Position position in allpositions)
                {
                    if (person.Position == position.Id)
                        allpeople.Add(person);
                }
            }

            return allpeople;
        }

        public void GetAllPositions()
        {
            List<Structure> structures = new List<Structure>();

            foreach (int structureId in temporaryStructuresIdList)
            {
                if(p_repository.GetContext().Structure.FirstOrDefault(el => el.Parentstructure == structureId) != null)
                {
                    structures = p_repository.StructuresLocal().Values.Where(el => el.Parentstructure == structureId).ToList();
                }

                structuresId.Add(structureId);
            }

            temporaryStructuresIdList = temporaryStructuresIdList.Except(structuresId).ToList();

            foreach (Structure structure in structures)
            {
                temporaryStructuresIdList.Add(structure.Id);
            }

            if (temporaryStructuresIdList.Count != 0)
                GetAllPositions();
            else
                return;
        }

        public Personrank GetActualRank(Person person)
        {
            List<Personrank> ranks = p_repository.GetContext().Personrank.Where(el => el.Person == person.Id).ToList();

            if (ranks.Count == 0)
                return null;

            List<Personrank> sortRanks = ranks.OrderBy(el => el.Decreedate).ToList();

            return sortRanks.Last();
        }
    }
}

