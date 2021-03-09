using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace PersonnelManagement.Models
{
    public class MovePersonBetweenCourses
    {
        private Dictionary<int, NewVzvod> new_vzvods;
        private Structure structure;
        private User user;
        private Position position;
        private Dictionary<int, ListsEmptiPosModels> listEmptiPos = new Dictionary<int, ListsEmptiPosModels>(); 
        private User autoUser;
        private Decree decree;
        private Repository p_repository;
        public MovePersonBetweenCourses(Repository repository, User user, Dictionary<int, NewVzvod> keyValues)
        {
            new_vzvods = keyValues;
            p_repository = repository;
            autoUser = new User() {Name = "УГЗ", Firstname = "Робот", Id = 5050, Date = DateTime.Now.Date, Admin = 1};
            decree = new Decree();
            this.user = user;
            CreateNewDecree(decree, autoUser);
        }
        public void Worker(IEnumerable<Persondecreeoperation> persondecreeoperations, bool flag = true)
        {
            List<int> deletedStructure = new List<int>();
            DateTime date = user.Date.GetValueOrDefault();
            pmContext context = p_repository.GetContext();
            if (flag && new_vzvods.Count() != 0)
            {
                foreach (KeyValuePair<int, NewVzvod> vzvod in new_vzvods)
                {
                    if (vzvod.Value.newIdStructure == 0)
                        continue;
                    structure = GetStructure(vzvod.Value.newIdStructure, vzvod.Value.name);
                    if (structure == null)
                    {
                        CreateStructure(vzvod.Value.newIdStructure, vzvod.Value.oldIdStructure, date);
                    }
                }
            }

            for (int count = 0; count < persondecreeoperations.Count(); count++)
            {
                Persondecreeoperation decreeoperation = persondecreeoperations.ElementAt(count);
                try
                {
                    if (decreeoperation.Optionnumber1 == 18)
                    {

                        structure = GetStructure(decreeoperation);

                        Personjob personjob = new Personjob();

                        personjob = p_repository.PersonjobsLocal().Values.ToList().FirstOrDefault(p => p.End == null && p.Person == decreeoperation.Person);

                        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

                        decreeoperation.Optionstring5 = textInfo.ToTitleCase(personjob.Jobposition);

                        if (structure != null)
                        {
                            if (!listEmptiPos.ContainsKey(structure.Id))
                            {
                                listEmptiPos.Add(structure.Id, new ListsEmptiPosModels(p_repository, structure.Id, p_repository.GetPersonsForStructure(structure.Id)));
                                listEmptiPos[structure.Id].FillStacks(decreeoperation);
                            }
                            position = listEmptiPos[structure.Id].GetPosition(decreeoperation.Optionstring5);

                            decreeoperation.Optionnumber3 = position.Id;
                            personjob.Position = decreeoperation.Optionnumber3;
                            p_repository.ChangePersonJob(user, personjob);
                        }
                    }

                    Personeducation personeducation = new Personeducation();

                    Educationtypeblock educationtypeblock = new Educationtypeblock();

                    Educationperiod educationperiod = new Educationperiod();

                    personeducation = p_repository.Personeducations.ToList().FirstOrDefault(p => p.End == null && p.Person == decreeoperation.Person);

                    //educationtypeblock = p_repository.Educationtypeblocks.ToList().FirstOrDefault(e => e.Personeducation == personeducation.Id && e.IsEnded == 0);
                    educationtypeblock = p_repository.EducationtypeblocksLocal().Values.FirstOrDefault(e => e.Personeducation == personeducation.Id && e.IsEnded == 0);

                    educationperiod = p_repository.Educationperiods.FirstOrDefault(e => e.Educationtypeblock == educationtypeblock.Id && e.End == null);
                    educationperiod.End = decreeoperation.Optiondate1;


                    List<Educationperiod> educationperiods = new List<Educationperiod>(educationtypeblock.Educationperiods);
                    p_repository.SaveChanges();


                    Rank rank = p_repository.RanksLocal().GetValue(decreeoperation.Optionnumber9);

                    Educationperiod newEducationperiod = new Educationperiod();
                    newEducationperiod.Educationtypeblock = educationtypeblock.Id;
                    newEducationperiod.Service = 0;
                    newEducationperiod.Start = decreeoperation.Optiondate1;
                    newEducationperiod.Educationpositiontype = decreeoperation.Optionnumber5;
                    newEducationperiod.Rank = educationperiod.Rank;
                    newEducationperiod.Platoon = educationperiod.Platoon;
                    newEducationperiod.Course = educationperiod.Course + 1;
                    newEducationperiod.Educationpositiontype = educationperiod.Educationpositiontype;
                    context.Educationperiod.Add(newEducationperiod);
                    p_repository.SaveChanges();

                    List<Person> persons = p_repository.GetPersonsForStructure(decreeoperation.Optionnumber6);

                    if (decreeoperation.Optionnumber1 == 18 && persons.Count == 0)
                    {
                        deletedStructure.Add(decreeoperation.Optionnumber6);
                        System.Threading.Thread.Sleep(1000);
                    }
                } catch
                {
                    count--;
                }
            }

            for (int i = 0; i < deletedStructure.Count; i++)
            {
                Structure oldStructure = p_repository.GetActualStructureInfo(deletedStructure[i], user.Date.GetValueOrDefault());
                Decreeoperation operation = new Decreeoperation();
                operation.Decree = decree.Id;
                operation.Deleted = 1;
                operation.Subject = -deletedStructure[i]; // У подразделений subject имеет знак минуса
                operation.Dateactive = decree.Dateactive;
                operation.Datecustom = 1;

                context.Decreeoperation.Add(operation);

                int structureid = deletedStructure[i];
                List<StructureInfo> structureInfos = p_repository.GetStructureInfos(structureid, user, false, false, false, true);
                List<Position> positionsToDelete = new List<Position>();
                foreach (StructureInfo structureInfo in structureInfos)
                {
                    Structure structure = p_repository.GetActualStructureInfo(structureInfo.Id, user.Date.GetValueOrDefault());
                    if (structure != null)
                    {
                        int id = structure.Id;
                        if (structure.Changeorigin > 0)
                        {
                            id = structure.Changeorigin;
                        }
                        IEnumerable<Position> positions = p_repository.GetPositions(id, user.Date.GetValueOrDefault(), false);
                        positionsToDelete.AddRange(positions);

                        Decreeoperation operationStruct = new Decreeoperation();
                        operationStruct.Decree = decree.Id;
                        operationStruct.Deleted = 1;
                        operationStruct.Subject = -structure.Id; // У подразделений subject имеет знак минуса
                        operationStruct.Dateactive = decree.Dateactive;
                        operationStruct.Datecustom = 1;
                        context.Decreeoperation.Add(operationStruct);
                    }
                }
                /**
                 * Удаляем все подчиненные должности
                 */
                foreach (Position position in positionsToDelete)
                {
                    //Decree decreePosition = GetDecreeByUser(user);

                    Decreeoperation operationPosition = new Decreeoperation();
                    operationPosition.Decree = decree.Id;
                    operationPosition.Deleted = 1;
                    operationPosition.Subject = position.Id;
                    operationPosition.Dateactive = decree.Dateactive;
                    operationPosition.Datecustom = 1;

                    context.Decreeoperation.Add(operationPosition);
                    //context.SaveChanges();
                }
                //RemovePosition
                context.SaveChanges();
                p_repository.UpdateStructuresLocal();
                p_repository.UpdateDecreeoperationsLocal();
            }
        }

        public void CreateStructure(int newIdStructure, int oldIdStructure, DateTime date)
        {
            Structure oldStructure = p_repository.GetActualStructureInfo(oldIdStructure, user.Date.GetValueOrDefault());
            Structure newStructure = new Structure(oldStructure);
            newStructure.Id = 0;
            if (newIdStructure > 0)
            {
                newStructure.Parentstructure = newIdStructure;
                int lowPriority = 0;
                if (p_repository.StructuresLocal().Values.Where(s => newStructure.Parentstructure == s.Parentstructure).Count() > 0)
                {
                    lowPriority = p_repository.StructuresLocal().Values.Where(s => newStructure.Parentstructure == s.Parentstructure).OrderBy(st => st.Priority).Last().Priority + 1;
                }
                newStructure.Priority = lowPriority;
                Structure parentActualStructure = p_repository.GetActualStructureInfo(newStructure.Parentstructure, date);
                if (newStructure.Structuretype == 0 && parentActualStructure.Structuretype > 0)
                {

                    newStructure.Structuretype = parentActualStructure.Structuretype;
                }
            }
            p_repository.GetContext().Structure.Add(newStructure);
            p_repository.GetContext().SaveChanges();

            structure = new Structure(newStructure);

            DecreeManagement decreeManagement1 = new DecreeManagement();

            decreeManagement1.Nickname = " ";

            Decreeoperation operation = new Decreeoperation();
            operation.Decree = decree.Id;
            operation.Created = 1;
            // У подразделений subject имеет знак минуса
            operation.Subject = -structure.Id;


            decreeManagement1.Id = decree.Id;
            List<Position> positions = new List<Position>();

            positions = p_repository.PositionsLocal().Values.Where(e => e.Structure == oldIdStructure).ToList();

            for (int i = 0; i < positions.Count; i++)
            {
                Position position = new Position();

                position.Cap = positions[i].Cap;
                position.Dateactive = decree.Dateactive;
                position.Sourceoffinancing = positions[i].Sourceoffinancing;
                position.Positiontype = positions[i].Positiontype;
                position.Notice = positions[i].Notice;
                position.Positioncategory = positions[i].Positioncategory;
                position.Replacedbycivil = positions[i].Replacedbycivil;
                position.Replacedbycivilpositioncategory = positions[i].Replacedbycivilpositioncategory;
                position.Replacedbycivilpositiontype = positions[i].Replacedbycivilpositiontype;
                position.Altrank = positions[i].Altrank;
                position.Origin = positions[i].Origin;
                position.Decertificate = positions[i].Decertificate;
                position.Decertificatedate = positions[i].Decertificatedate;
                position.Civilranklow = positions[i].Civilranklow;
                position.Civilrankhigh = positions[i].Civilrankhigh;
                position.Replacedbycivildatelimit = positions[i].Replacedbycivildatelimit;
                position.Replacedbycivildate = positions[i].Replacedbycivildate;
                position.Structure = structure.Id;
                position.Civildecreedate = decree.Dateactive;
                position.Curator = positions[i].Curator;
                position.Head = positions[i].Head;
                position.Curatorlist = positions[i].Curatorlist;
                position.Headid = positions[i].Headid;
                position.Opchs = positions[i].Opchs;
                position.Part = positions[i].Part;
                position.Partval = positions[i].Partval;
                position.Subject1 = positions[i].Subject1;
                position.Subject2 = positions[i].Subject2;
                position.Subject3 = positions[i].Subject3;
                position.Subject4 = positions[i].Subject4;
                position.Subject5 = positions[i].Subject5;
                position.Subject6 = positions[i].Subject6;
                position.Subject7 = positions[i].Subject7;
                position.Subject8 = positions[i].Subject8;
                position.Subject9 = positions[i].Subject9;
                position.Subject10 = positions[i].Subject10;
                position.Subject11 = positions[i].Subject11;
                position.Subject12 = positions[i].Subject12;
                position.Subject13 = positions[i].Subject13;
                position.Subject14 = positions[i].Subject14;
                position.Subject15 = positions[i].Subject15;
                position.Subject16 = positions[i].Subject16;
                position.Subject17 = positions[i].Subject17;
                position.Subject18 = positions[i].Subject18;
                position.Subject19 = positions[i].Subject19;
                position.Subject20 = positions[i].Subject20;
                position.Name1 = positions[i].Name1;
                position.Name2 = positions[i].Name2;
                position.Name3 = positions[i].Name3;
                position.Name4 = positions[i].Name4;
                position.Name5 = positions[i].Name5;
                position.Name6 = positions[i].Name6;

                p_repository.GetContext().Position.Add(position);
                p_repository.GetContext().SaveChanges();

                Decreeoperation positionOperation = new Decreeoperation();
                positionOperation.Decree = decree.Id;
                positionOperation.Created = 1;

                // У подразделений subject имеет знак минуса
                positionOperation.Subject = position.Id;
                p_repository.GetContext().Decreeoperation.Add(positionOperation);
            }

            
            p_repository.GetContext().Decreeoperation.Add(operation);
            p_repository.GetContext().SaveChanges();
            
        }

        public Structure GetStructure(Persondecreeoperation decreeoperation)
        {
            List<Structure> structures = new List<Structure>();

            System.Threading.Thread.Sleep(5000);

            p_repository.StructuresLocal().Values.ToList().ForEach(s => { if (s.Parentstructure == decreeoperation.Optionnumber7 && s.Name == decreeoperation.Optionstring5) { structures.Add(s); }});

            if (structures.Count > 0)
            {
                structure = structures.FirstOrDefault(s => s.Changeorigin == 0);

                return structure;
            }
            return null;
        }

        public Structure GetStructure(int parentId, string name)
        {
            List<Structure> structures = new List<Structure>();

            p_repository.StructuresLocal().Values.ToList().ForEach(s => { if (s.Parentstructure == parentId && s.Name == name) { structures.Add(s); } });

            if (structures.Count > 0)
            {
                structure = structures.FirstOrDefault(s => s.Changeorigin == 0);

                return structure;
            }
            return null;
        }

        public void CreateNewDecree(Decree decree ,User user)
        {
            pmContext context = p_repository.GetContext();
            DateTime date = user.Date.GetValueOrDefault();

            decree.User = user.Id;
            decree.Nickname = " ";
            decree.Datesigned = date;
            decree.Dateactive = date;
            decree.Signed = 1;
            context.Decree.Add(decree);
            context.SaveChanges();
        }
    }
}
