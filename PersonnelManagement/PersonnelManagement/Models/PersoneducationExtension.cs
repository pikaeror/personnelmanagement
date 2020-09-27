using Newtonsoft.Json;
using PersonnelManagement.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public partial class Personeducation
    {

        [NotMapped]
        public List<Educationtypeblock> Educationtypeblocks { get; set; } = new List<Educationtypeblock>();
        [NotMapped]
        public List<Academicvacation> Academicvacations { get; set; } = new List<Academicvacation>();
        [NotMapped]
        public List<Educationmaternity> Educationmaternities { get; set; } = new List<Educationmaternity>();
        // Образование делится на части с учетом периодов. Для вкладки УГЗ
        [NotMapped]
        public List<Personeducationpart> Personeducationparts { get; set; } = new List<Personeducationpart>();
        // Образование делится на части с учетом одного большого периода + возможно отдельно академ отпуск и декрет
        [NotMapped]
        public List<Personeducationpart> PersoneducationpartsCommon { get; set; } = new List<Personeducationpart>(); 
        
        // Для выслуги лет. Учитывается ли образование в выслуге лет в принципе
        [NotMapped]
        public bool Consider { get; set; } = true;
        // Для выслуги лет. Являлся ли курсантом (очный военное образование)
        [NotMapped]
        public bool Military { get; set; } = true;
        [NotMapped]
        public bool Fulltime { get; set; } = true;
        [NotMapped]
        public int Educationpositiontype { get; set; } = 0;
        /// <summary>
        /// Номер группы для слияния. Используется для выслуги лет, когда разные периоды УГЗ сливаются в одну запись
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public int Group { get; set; } = 0;


        /// <summary>
        /// Создает части personeducation для таблицы на фронте. Например, если УГЗ, то делит на периоды обучения по годам, добавляя туда академ и декрет в случае чего.
        /// В общей вкладке образования делит на один суммарный единый период и академы/декреты
        /// </summary>
        public void GeneratePersoneducationparts()
        {
            List<Personeducationpart> personeducationparts = new List<Personeducationpart>();
            List<Personeducationpart> personeducationpartsCommon = new List<Personeducationpart>();


            foreach (var educationtypeblock in Educationtypeblocks)
            {
                foreach (var educationperiod in educationtypeblock.Educationperiods)
                {
                    Personeducationpart personeducationpartUcp = new Personeducationpart();
                    personeducationpartUcp.Start = educationperiod.Start;
                    personeducationpartUcp.End = educationperiod.End;
                    personeducationpartUcp.Educationperiod = educationperiod;
                    personeducationpartUcp.Educationtypeblock = educationtypeblock;
                    personeducationparts.Add(personeducationpartUcp);

                    // Новым способ разделения обучения на строки для таблицы
                    personeducationpartsCommon.Add(personeducationpartUcp);
                }
            }

            // Старый способ разделения обучения на строки для таблицы
            //Personeducationpart personeducationpartCommon = new Personeducationpart();
            //personeducationpartCommon.Start = Start;
            //personeducationpartCommon.End = End;
            //personeducationpartCommon.Educationtypeblock = Educationtypeblocks.LastOrDefault();
            //personeducationpartsCommon.Add(personeducationpartCommon);

            if (Academicvacation > 0)
            {
                foreach (var academicvacation in Academicvacations)
                {
                    Personeducationpart personeducationpart = new Personeducationpart();
                    personeducationpart.Start = academicvacation.Start;
                    personeducationpart.End = academicvacation.End;
                    personeducationpart.Academicvacation = academicvacation;
                    personeducationparts.Add(personeducationpart);
                    personeducationpartsCommon.Add(personeducationpart);
                }
            }
            
            if (Maternityvacation > 0)
            {
                foreach (var educationmaternity in Educationmaternities)
                {
                    Personeducationpart personeducationpart = new Personeducationpart();
                    personeducationpart.Start = educationmaternity.Start;
                    personeducationpart.End = educationmaternity.End;
                    personeducationpart.Educationmaternity = educationmaternity;
                    personeducationparts.Add(personeducationpart);
                    personeducationpartsCommon.Add(personeducationpart);
                }
            }

            personeducationpartsCommon = personeducationpartsCommon.OrderBy(p => p.Start).ToList();
            // Оставляем только последний период обучения и периоды обучения перед академическими отпусками и декретами
            List<Personeducationpart> personeducationpartsToRemove = new List<Personeducationpart>();
            Personeducationpart previous = null;
            foreach (Personeducationpart personeducationpartCommon in personeducationpartsCommon)
            {
                if (personeducationpartCommon.Educationperiod != null && previous != null && previous.Educationperiod != null)
                {
                    personeducationpartsToRemove.Add(previous);
                }

                previous = personeducationpartCommon;
            }
            foreach (Personeducationpart personeducationpartCommon in personeducationpartsToRemove)
            {
                personeducationpartsCommon.Remove(personeducationpartCommon);
            }



            

            personeducationparts = personeducationparts.OrderBy(p => p.Start).ToList();
            // К каждому периоду прикрепляем ссылку на предшествующий период, 
            Personeducationpart personeducationpartPrevious = null;
            foreach (Personeducationpart personeducationpartElement in personeducationparts)
            {
                if (personeducationpartPrevious != null)
                {
                    personeducationpartElement.PreviousEducationpartWithEducationperiod = personeducationpartPrevious;
                }

                if (personeducationpartElement.Educationtypeblock != null && personeducationpartElement.Educationperiod != null)
                {
                    personeducationpartPrevious = personeducationpartElement;
                }
            }



            personeducationpartsCommon = personeducationpartsCommon.OrderBy(p => p.Start).ToList();
            Personeducationparts = personeducationparts;
            PersoneducationpartsCommon = personeducationpartsCommon;
        }

        /// <summary>
        /// Разбивает одну трудовую деятельность на несколько трудовых деятельностей с учетом наличия льготных периодов, а так же отпусков, заболеваний и командировок в них
        /// </summary>
        /// <param name="userDate"></param>
        /// <param name="personjobs"></param>
        /// <returns></returns>
        public List<Personeducation> GeneratePersoneducationsByPeriods(DateTime userDate, IEnumerable<Personjob> personjobs)
        {
            List<Personeducation> personeducations = new List<Personeducation>();
            // Получаем периоды образования, включая академ отпуски и декреты в Personeducationparts
            GeneratePersoneducationparts();
            List<Personeducationpart> personeducationparts = GetConcatedPersoneducationparts(Personeducationparts);
            // По отформатированным периодам смотрим, являются ли очными/заочными и пересекаются ли с работой заочные
            foreach (Personeducationpart personeducationpart in personeducationparts)
            {
                if (personeducationpart.Educationtypeblock != null && personeducationpart.Educationperiod != null)
                {
                    // Учитывается ли оно вообще.
                    bool consider = false;
                    // Учитываем только среднее специальное и высшее.
                    if (Educationlevel == 4 || Educationlevel == 5)
                    {
                        consider = true;
                    }
                    // Является средним специальным/высшим и прописана ли информация о дипломе, есть ли дата окончания 
                    if (consider && (String.IsNullOrWhiteSpace(Documentnumber) || String.IsNullOrWhiteSpace(Documentseries)
                        || Educationdocument <= 0 || End == null))
                    {
                        consider = false;
                    }

                    //bool military = false;
                    //if (Ucp > 0)
                    //{
                    //    military = true;
                    //} else
                    //{
                    //    military = MilitaryPension(personeducationpart.Educationperiod);
                    //}
                    bool military = MilitaryPension(personeducationpart);

                    if (FulltimePension(personeducationpart.Educationtypeblock))
                    {
                        Personeducation personeducation = 
                            ClonePension(personeducationpart.Start, personeducationpart.End, personeducationpart.Educationtypeblock.Educationtype, military, consider,
                            personeducationpart.Educationperiod.Educationpositiontype);
                        personeducations.Add(personeducation);
                    } else
                    {
                        Personeducation personeducation =
                            ClonePension(personeducationpart.Start, personeducationpart.End, personeducationpart.Educationtypeblock.Educationtype, military, consider,
                            personeducationpart.Educationperiod.Educationpositiontype);
                        //personeducations.Add(personeducation);
                        List<Personeducation> personeducationSubparts = new List<Personeducation>();
                        personeducationSubparts.Add(personeducation);
                        List<Personeducation> subpartsToRemove = new List<Personeducation>();
                        List<Personeducation> subpartsToAdd = new List<Personeducation>();
                        

                        // В не-очном образовании смотрим, пересекается ли с работой. Если пересекается, то эти куски вычленяем.
                        foreach (Personjob personjob in personjobs)
                        {
                            Period jobPeriod = new Period(personjob.Start.GetValueOrDefault(), personjob.End.GetValueOrDefault());
                            foreach (Personeducation personeducationSubpart in personeducationSubparts)
                            {
                                // Если период обучения не учитываем в выслуге лет, то пропускаем его
                                if (!personeducationSubpart.Consider)
                                {
                                    continue;
                                }
                                // Работа пересекается с образованием
                                if (jobPeriod.InnerPart(personeducationSubpart.Start.GetValueOrDefault(), personeducationSubpart.End.GetValueOrDefault()) != null)
                                {
                                    Period subpartPeriod = new Period(personeducationSubpart.Start.GetValueOrDefault(), personeducationSubpart.End.GetValueOrDefault());
                                    List<Period> splittedParts = subpartPeriod.Split(jobPeriod.Start.GetValueOrDefault(), jobPeriod.End.GetValueOrDefault());
                                    // Что-то да раздробило
                                    if (splittedParts.Count > 0)
                                    {
                                        foreach (Period splittedPart in splittedParts)
                                        {
                                            // Так как сплиттером у нас являлась работа, то мы ее сразу записываем в неучитываемые
                                            if (splittedPart.Splitter)
                                            {
                                                bool considerSplitted = false;
                                                Personeducation subpartPersoneducation =
                                                    ClonePension(splittedPart.Start, splittedPart.End, personeducationpart.Educationtypeblock.Educationtype, military, considerSplitted,
                                                    personeducationpart.Educationperiod.Educationpositiontype);

                                                subpartsToAdd.Add(subpartPersoneducation);
                                            }
                                            else
                                            {

                                                Personeducation subpartPersoneducation =
                                                    ClonePension(splittedPart.Start, splittedPart.End, personeducationpart.Educationtypeblock.Educationtype, military, consider,
                                                    personeducationpart.Educationperiod.Educationpositiontype);

                                                subpartsToAdd.Add(subpartPersoneducation);
                                            }
                                        }
                                        subpartsToRemove.Add(personeducationSubpart);
                                        //subpartToRemove = personeducationSubpart;
                                        // Теоретически одна работа может затронуть несколько образований 
                                        //break;
                                    }
                                }
                            }
                            // После того, как прошлись по кускам обучения, добавляем раздробленные части, если есть, и убираем целую, если раздробило.
                            personeducationSubparts.AddRange(subpartsToAdd);
                            subpartsToAdd = new List<Personeducation>();
                            if (subpartsToRemove.Count > 0)
                            {
                                foreach (Personeducation subpartToRemove in subpartsToRemove)
                                {
                                    personeducationSubparts.Remove(subpartToRemove);
                                }
                            }
                            subpartsToRemove = new List<Personeducation>();
                            
                        }
                        // Наконец, добавляем все части образования, раздробленные работой.
                        personeducations.AddRange(personeducationSubparts);
                    }
                }
                else if (personeducationpart.Academicvacation != null)
                {
                    // Никогда не учитывается
                    bool consider = false;
                    // Никогда не может быть военным
                    bool military = false;
                    Personeducation personeducation =
                            ClonePension(personeducationpart.Start, personeducationpart.End, 0, military, consider,
                            0);
                    personeducations.Add(personeducation);
                } 
                else if (personeducationpart.Educationmaternity != null)
                {
                    // Никогда не учитывается
                    //bool consider = false;
                    //bool military = false;
                    //if (Ucp > 0)
                    //{
                    //    military = true;
                    //    consider = true;
                    //}
                    bool military = MilitaryPension(personeducationpart);
                    bool consider = military;
                    int educationpositiontype = 0;
                    if (military && personeducationpart.PreviousEducationpartWithEducationperiod != null 
                        && personeducationpart.PreviousEducationpartWithEducationperiod.Educationperiod != null)
                    {
                        educationpositiontype = personeducationpart.PreviousEducationpartWithEducationperiod.Educationperiod.Educationpositiontype;
                    }

                    //if (personeducationpart.PreviousEducationpartWithEducationperiod != null && personeducationpart)
                    Personeducation personeducation =
                            ClonePension(personeducationpart.Start, personeducationpart.End, 0, military, consider,
                            educationpositiontype);
                    personeducations.Add(personeducation);
                }
            }

            // Если вдруг снова появились в процессе дробления одинаковые периоды, следующие друг за другом.
            personeducations = GetConcatedPersoneducations(personeducations);

            return personeducations;
        }

        /// <summary>
        /// Собирает вместе периоды идущие друг за другом, но являющиеся при этом одной формы обучения (как в УГЗ)
        /// </summary>
        /// <returns></returns>
        public List<Personeducationpart> GetConcatedPersoneducationparts(IEnumerable<Personeducationpart> personeducationpartsToConcate)
        {
            List<(int, int)> x = new List<(int, int)>();
            List<Personeducationpart> personeducationparts = new List<Personeducationpart>();
            Personeducationpart previous = null;

            // Совпадающие personeducationpart сортируем по группам, чтобы потом объединить их
            int lastgroupId = 0;
            // Список должен быть отсортирован.
            List<Personeducationpart> personeducationpartsSorted = personeducationpartsToConcate.OrderBy(p => p.Start.GetValueOrDefault()).ToList();
            foreach(Personeducationpart personeducationpart in personeducationpartsSorted)
            {
                // Совпадает с предыдущим 
                if (personeducationpart.Educationtypeblock != null && previous != null && previous.Educationtypeblock != null
                        && personeducationpart.Educationtypeblock.Educationtype == previous.Educationtypeblock.Educationtype)
                {
                    // Идет сразу после
                    if (personeducationpart.Start.GetValueOrDefault() == previous.End.GetValueOrDefault() 
                        || personeducationpart.Start.GetValueOrDefault().AddDays(-1) == previous.End.GetValueOrDefault())
                    {
                        // Если у предыдущего не было группы, то мы создаем новую
                        if (previous.Group == 0)
                        {
                            lastgroupId += 1;
                            previous.Group = lastgroupId;
                            personeducationpart.Group = lastgroupId;
                        } else
                        {
                            // Если у предыдущего была группа, то мы ее продолжаему
                            personeducationpart.Group = previous.Group;
                        }
                    }
                }
                previous = personeducationpart;
            }

            List<Personeducationpart> concatedGroup = new List<Personeducationpart>();
            // Проходимся по всем группам 
            while (lastgroupId >= 0)
            {
                // Группа (на слияние)
                if (lastgroupId > 0)
                {
                    concatedGroup = new List<Personeducationpart>();
                    foreach (Personeducationpart personeducationpart in personeducationpartsSorted)
                    {
                        if (personeducationpart.Group == lastgroupId)
                        {
                            concatedGroup.Add(personeducationpart);
                        }
                    }
                    concatedGroup = concatedGroup.OrderBy(p => p.Start).ToList();
                    // Создаем объединенный элемент
                    if (concatedGroup.Count > 0)
                    {
                        Personeducationpart concatedPersoneducationpart = new Personeducationpart();
                        concatedPersoneducationpart.Academicvacation = concatedGroup.First().Academicvacation;
                        concatedPersoneducationpart.Educationmaternity = concatedGroup.First().Educationmaternity;
                        concatedPersoneducationpart.Educationperiod = concatedGroup.First().Educationperiod;
                        concatedPersoneducationpart.Educationtypeblock = concatedGroup.First().Educationtypeblock;
                        concatedPersoneducationpart.Start = concatedGroup.First().Start;
                        concatedPersoneducationpart.End = concatedGroup.Last().End;
                        personeducationparts.Add(concatedPersoneducationpart);
                    }
                    
                // Одиночки (все прочие)
                } else
                {
                    foreach (Personeducationpart personeducationpart in personeducationpartsSorted)
                    {
                        if (personeducationpart.Group == 0)
                        {
                            personeducationparts.Add(personeducationpart);
                        }
                    }
                }
                

                lastgroupId -= 1;
            }

            personeducationparts = personeducationparts.OrderBy(p => p.Start.GetValueOrDefault()).ToList();
            return personeducationparts;
        }

        /// <summary>
        /// Собирает вместе периоды идущие друг за другом, но являющиеся при этом одной формы обучения (как в УГЗ)
        /// </summary>
        /// <returns></returns>
        public List<Personeducation> GetConcatedPersoneducations(IEnumerable<Personeducation> personeducationsToConcate)
        {
            List<(int, int)> x = new List<(int, int)>();
            List<Personeducation> personeducations = new List<Personeducation>();
            Personeducation previous = null;

            // Совпадающие personeducationpart сортируем по группам, чтобы потом объединить их
            int lastgroupId = 0;
            // Список должен быть отсортирован.
            List<Personeducation> personeducationsSorted = personeducationsToConcate.OrderBy(p => p.Start.GetValueOrDefault()).ToList();
            foreach (Personeducation personeducation in personeducationsSorted)
            {
                // Совпадает с предыдущим 
                if (previous != null && personeducation.Educationtype == previous.Educationtype && personeducation.Military == previous.Military
                        && personeducation.Consider == previous.Consider)
                {
                    // Идет сразу после
                    if (personeducation.Start.GetValueOrDefault() == previous.End.GetValueOrDefault()
                        || personeducation.Start.GetValueOrDefault().AddDays(-1) == previous.End.GetValueOrDefault())
                    {
                        // Если у предыдущего не было группы, то мы создаем новую
                        if (previous.Group == 0)
                        {
                            lastgroupId += 1;
                            previous.Group = lastgroupId;
                            personeducation.Group = lastgroupId;
                        }
                        else
                        {
                            // Если у предыдущего была группа, то мы ее продолжаему
                            personeducation.Group = previous.Group;
                        }
                    }
                }
                previous = personeducation;
            }

            List<Personeducation> concatedGroup = new List<Personeducation>();
            // Проходимся по всем группам 
            while (lastgroupId >= 0)
            {
                // Группа (на слияние)
                if (lastgroupId > 0)
                {
                    concatedGroup = new List<Personeducation>();
                    foreach (Personeducation personeducation in personeducationsSorted)
                    {
                        if (personeducation.Group == lastgroupId)
                        {
                            concatedGroup.Add(personeducation);
                        }
                    }
                    concatedGroup = concatedGroup.OrderBy(p => p.Start).ToList();
                    // Создаем объединенный элемент
                    if (concatedGroup.Count > 0)
                    {
                        Personeducation concatedPersoneducation =
                            ClonePension(concatedGroup.First().Start, concatedGroup.Last().End, concatedGroup.First().Educationtype, concatedGroup.First().Military,
                                concatedGroup.First().Consider, concatedGroup.First().Educationpositiontype);

                        //Personeducationpart concatedPersoneducationpart = new Personeducationpart();
                        //concatedPersoneducationpart.Academicvacation = concatedGroup.First().Academicvacation;
                        //concatedPersoneducationpart.Educationmaternity = concatedGroup.First().Educationmaternity;
                        //concatedPersoneducationpart.Educationperiod = concatedGroup.First().Educationperiod;
                        //concatedPersoneducationpart.Educationtypeblock = concatedGroup.First().Educationtypeblock;
                        //concatedPersoneducationpart.Start = concatedGroup.First().Start;
                        //concatedPersoneducationpart.End = concatedGroup.Last().End;
                        personeducations.Add(concatedPersoneducation);
                    }

                    // Одиночки (все прочие)
                }
                else
                {
                    foreach (Personeducation personeducation in personeducationsSorted)
                    {
                        if (personeducation.Group == 0)
                        {
                            personeducations.Add(personeducation);
                        }
                    }
                }

                lastgroupId -= 1;
            }
            personeducations = personeducations.OrderBy(p => p.Start.GetValueOrDefault()).ToList();
            return personeducations;
        }

        /// <summary>
        /// Очный дневной.
        /// </summary>
        /// <param name="educationtypeblock"></param>
        /// <returns></returns>
        public bool FulltimePension(Educationtypeblock educationtypeblock)
        {
            if (educationtypeblock == null)
            {
                return false;
            }
            if (educationtypeblock.Educationtype == 1)
            {
                return true;
            } else
            {
                return false;
            }
            
        }

        //public bool MilitaryPension(Educationperiod educationperiod)
        //{
        //    // Являлся курсантом по периоду
        //    if (educationperiod.Service > 0)
        //    {
        //        return true;
        //    } else
        //    {
        //        return false;
        //    }
        //}

        /// <summary>
        /// Считается ли период службой (входит в выслугу лет для военных). Да, если очный
        /// </summary>
        /// <param name="personeducationpart"></param>
        /// <returns></returns>
        public bool MilitaryPension(Personeducationpart personeducationpart)
        {
            if (personeducationpart.Educationperiod != null && FulltimePension(personeducationpart.Educationtypeblock))
            {
                // Являлся курсантом по периоду
                if (personeducationpart.Educationperiod.Service > 0
                    || personeducationpart.Educationperiod.Rank.Length > 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            } else if (personeducationpart.Educationmaternity != null && personeducationpart.PreviousEducationpartWithEducationperiod != null)
            {
                return MilitaryPension(personeducationpart.PreviousEducationpartWithEducationperiod);
            } else
            {
                return false;
            }
        }

        /// <summary>
        /// Клонирование для выслуги лет
        /// </summary>
        /// <returns></returns>
        public Personeducation ClonePension(DateTime? start = null, DateTime? end = null, int educationtype = 0, bool military = false, bool consider = true, int educationpositiontype = 0)
        {
            Personeducation newPersoneducation = new Personeducation();
            newPersoneducation.Person = Person;
            newPersoneducation.Main = Main;
            newPersoneducation.Educationlevel = Educationlevel;
            newPersoneducation.Educationstage = Educationstage;
            newPersoneducation.Name = Name;
            newPersoneducation.Name2 = Name2;
            newPersoneducation.Location = Location;
            newPersoneducation.City = City;
            newPersoneducation.Faculty = Faculty;
            newPersoneducation.Educationtype = Educationtype;
            if (educationtype > 0)
            {
                newPersoneducation.Educationtype = educationtype;
                if (educationtype == 1)
                {
                    newPersoneducation.Fulltime = true;
                }
            }
            newPersoneducation.Datestart = Datestart;
            newPersoneducation.Dateend = Dateend;
            newPersoneducation.Speciality = Speciality;
            newPersoneducation.Documentseries = Documentseries;
            newPersoneducation.Documentnumber = Documentnumber;
            newPersoneducation.Cadet = Cadet;
            newPersoneducation.Qualification = Qualification;
            newPersoneducation.Start = Start;
            if (start != null)
            {
                newPersoneducation.Start = start;
            }
            newPersoneducation.End = End;
            if (end != null)
            {
                newPersoneducation.End = end;
            }
            newPersoneducation.Interrupted = Interrupted;
            newPersoneducation.Interruptorderdate = Interruptorderdate;
            newPersoneducation.Interruptorderwho = Interruptorderwho;
            newPersoneducation.Interruptordernumber = Interruptordernumber;
            newPersoneducation.Interruptordernumbertype = Interruptordernumbertype;
            newPersoneducation.Interruptorderreason = Interruptorderreason;
            newPersoneducation.Educationdocument = Educationdocument;
            newPersoneducation.Ordernumber = Ordernumber;
            newPersoneducation.Ordernumbertype = Ordernumbertype;
            newPersoneducation.Orderdate = Orderdate;
            newPersoneducation.Orderwho = Orderwho;
            newPersoneducation.Orderwhoid = Orderwhoid;
            newPersoneducation.Orderid = Orderid;
            newPersoneducation.Nameasjobfull = Nameasjobfull;
            newPersoneducation.Nameasjobposition = Nameasjobposition;
            newPersoneducation.Nameasjobplace = Nameasjobplace;
            newPersoneducation.Educationadditionaltype = Educationadditionaltype;
            newPersoneducation.Ucp = Ucp;
            newPersoneducation.Academicvacation = Academicvacation;
            newPersoneducation.Maternityvacation = Maternityvacation;
            newPersoneducation.Rating = Rating;
            newPersoneducation.State = State;
            newPersoneducation.Citytype = Citytype;
            newPersoneducation.Military = military;
            newPersoneducation.Consider = consider;
            newPersoneducation.Educationpositiontype = educationpositiontype;

            return newPersoneducation;
        }
    }
}
