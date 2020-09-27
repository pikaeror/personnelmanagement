using Itenso.TimePeriod;
using PersonnelManagement.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public partial class Personjob
    {
        [NotMapped]
        public List<Personjobprivelege> Personjobpriveleges { get; set; } = new List<Personjobprivelege>();
        // Только для тех случаев, когда мы дробим трудовую деятельность на куски, часть из которых оказывается льготной
        [NotMapped]
        public Personjobprivelege Personjobprivelege { get; set; } = null;

        /// <summary>
        /// Возвращает текущую (реальную) дату, если выставлена метка Actual, а запрос при этом проходит "из прошлого".
        /// Если запрос "из будущего", то возвращает Actual приравненное к будущему 
        /// </summary>
        /// <returns></returns>
        public DateTime GetEndActual(DateTime userDatetime)
        {
            if (Actual > 0)
            {
                DateTime actual = DateTime.Now;
                if (actual >= userDatetime)
                {
                    return actual;
                }
                else
                {
                    return userDatetime;
                }
            } else
            {
                return End.GetValueOrDefault();
            }
            
        }

        /// <summary>
        /// На основании количества периодов делает по записи для вывода в таблицу льгот
        /// </summary>
        /// <returns></returns>
        public List<Personjobprivelege> GeneratePersonjobsByPeriodsPrivelegeOld()
        {
            List<Personjobprivelege> personjobpriveleges = new List<Personjobprivelege>();
            foreach (var personjobprivelege in Personjobpriveleges)
            {
                foreach (var period in personjobprivelege.Personjobprivelegeperiods)
                {
                    Personjobprivelege newPersonjobprivelege = GetPersonjobprivelegeElement(personjobprivelege, period);

                    personjobpriveleges.Add(newPersonjobprivelege);
                }
            }
            return personjobpriveleges;
        }

        /// <summary>
        /// На основании количества периодов делает по записи для вывода в таблицу льгот
        /// </summary>
        /// <returns></returns>
        public List<Personjobprivelege> GeneratePersonjobsByPeriodsPrivelege(DateTime userDate, IEnumerable<Personvacation> personvacations, 
            IEnumerable<Personill> personills, IEnumerable<Personworktrip> personworktrips)
        {
            List<Personjobprivelege> personjobpriveleges = new List<Personjobprivelege>();
            List<Personjob> personjobs = GeneratePersonjobsByPeriods(userDate, personvacations, personills, personworktrips);
            foreach (Personjob personjob in personjobs)
            {
                if (personjob.Personjobprivelege != null)
                {//GetPersonjobprivelegeElement
                    //Period period = new Period(personjob.Start.GetValueOrDefault(), personjob.End.GetValueOrDefault());
                    personjobpriveleges.Add(personjob.Personjobprivelege);
                }
            }

            return personjobpriveleges;
        }

        /// <summary>
        /// Для нескольких периодов трудовой деятельности на основании количества периодов делает по записи для вывода в таблицу льгот
        /// </summary>
        /// <returns></returns>
        public static List<Personjobprivelege> GeneratePersonjobsByPeriodsPrivelege(IEnumerable<Personjob> personjobs, DateTime userDate, IEnumerable<Personvacation> personvacations,
            IEnumerable<Personill> personills, IEnumerable<Personworktrip> personworktrips)
        {
            List<Personjobprivelege> personjobpriveleges = new List<Personjobprivelege>();
            foreach (Personjob personjob in personjobs)
            {
                personjobpriveleges.AddRange(personjob.GeneratePersonjobsByPeriodsPrivelege(userDate, personvacations, personills, personworktrips));
            }
            return personjobpriveleges;
        }

        /// <summary>
        /// Возвращает отдельную запись периода для таблиц и прочего
        /// </summary>
        /// <param name="personjobprivelege"></param>
        /// <param name="personjobprivelegeperiod"></param>
        /// <returns></returns>
        public Personjobprivelege GetPersonjobprivelegeElement(Personjobprivelege personjobprivelege, Personjobprivelegeperiod period)
        {
            Personjobprivelege newPersonjobprivelege = new Personjobprivelege();
            //newPersonjobprivelege.Personjob = Id;
            newPersonjobprivelege.Personjob = personjobprivelege.Personjob;
            newPersonjobprivelege.Start = period.Start;
            newPersonjobprivelege.End = period.End;
            newPersonjobprivelege.Coef = personjobprivelege.Coef;
            newPersonjobprivelege.Prooftype = personjobprivelege.Prooftype;
            newPersonjobprivelege.Proofdate = personjobprivelege.Proofdate;
            newPersonjobprivelege.Proofnumber = personjobprivelege.Proofnumber;
            newPersonjobprivelege.Prooftext = personjobprivelege.Prooftext;
            newPersonjobprivelege.Documentorder = personjobprivelege.Documentorder;
            newPersonjobprivelege.Documentdate = personjobprivelege.Documentdate;
            newPersonjobprivelege.Documentnumber = personjobprivelege.Documentnumber;
            newPersonjobprivelege.Ordernumbertype = personjobprivelege.Ordernumbertype;
            newPersonjobprivelege.Orderwho = personjobprivelege.Orderwho;
            newPersonjobprivelege.Orderwhoid = personjobprivelege.Orderwhoid;
            newPersonjobprivelege.Orderid = personjobprivelege.Orderid;
            newPersonjobprivelege.Ordertype = personjobprivelege.Ordertype;

            DateDiff dateDiff = new DateDiff(newPersonjobprivelege.Start.GetValueOrDefault(), newPersonjobprivelege.End.GetValueOrDefault());
            newPersonjobprivelege.Daysbeforecoef = dateDiff.ElapsedDays;
            newPersonjobprivelege.Monthsbeforecoef = dateDiff.ElapsedMonths;
            newPersonjobprivelege.Yearsbeforecoef = dateDiff.ElapsedYears;

            int daysmultiplied = dateDiff.ElapsedYears * 365 + dateDiff.ElapsedMonths * 30 + dateDiff.ElapsedDays;
            daysmultiplied = (int)(daysmultiplied * newPersonjobprivelege.Coef);
            int afteryears = daysmultiplied / 365;
            daysmultiplied = daysmultiplied - (afteryears * 365);
            int aftermonths = daysmultiplied / 30;
            daysmultiplied = daysmultiplied - (aftermonths * 30);
            newPersonjobprivelege.Daysaftercoef = daysmultiplied;
            newPersonjobprivelege.Monthsaftercoef = aftermonths;
            newPersonjobprivelege.Yearsaftercoef = afteryears;

            return newPersonjobprivelege;
        }

        /// <summary>
        /// Возвращает отдельную запись периода для таблиц и прочего
        /// </summary>
        /// <returns></returns>
        public Personjob GetPersonjobElement()
        {
            Personjob newPersonjob = new Personjob();
            newPersonjob.Person = Person;
            newPersonjob.Jobtype = Jobtype;
            newPersonjob.Start = Start;
            newPersonjob.End = End;
            newPersonjob.Jobplace = Jobplace;
            newPersonjob.Jobposition = Jobposition;
            newPersonjob.Jobpositionplace = Jobpositionplace;
            newPersonjob.Servicetype = Servicetype;
            newPersonjob.Servicetypestr = Servicetypestr;
            newPersonjob.Servicefeature = Servicefeature;
            newPersonjob.Serviceorder = Serviceorder;
            newPersonjob.Servicecoef = Servicecoef;
            newPersonjob.Serviceplace = Serviceplace;
            newPersonjob.Ordernumber = Ordernumber;
            newPersonjob.Ordernumbertype = Ordernumbertype;
            newPersonjob.Orderdate = Orderdate;
            newPersonjob.Orderwho = Orderwho;
            newPersonjob.Orderwhoid = Orderwhoid;
            newPersonjob.Orderid = Orderid;
            newPersonjob.Actual = Actual;
            newPersonjob.Manual = Manual;
            newPersonjob.Mchs = Mchs;
            newPersonjob.Vacationdays = Vacationdays;
            newPersonjob.Position = Position;
            newPersonjob.Positiontoselect = Positiontoselect;
            newPersonjob.Positionnametree = Positionnametree;
            newPersonjob.Fireordernumber = Fireordernumber;
            newPersonjob.Fireordernumbertype = Fireordernumbertype;
            newPersonjob.Fireorderdate = Fireorderdate;
            newPersonjob.Fireorderwho = Fireorderwho;
            newPersonjob.Fireorderwhoid = Fireorderwhoid;
            newPersonjob.Fireorderid = Fireorderid;
            newPersonjob.Statecivil = Statecivil;
            newPersonjob.Statecivilstart = Statecivilstart;
            newPersonjob.Statecivilend = Statecivilend;
            newPersonjob.Startcustom = Startcustom;
            newPersonjob.Privelege = Privelege;

            return newPersonjob;
        }

        /// <summary>
        /// Разбивает одну трудовую деятельность на несколько трудовых деятельностей с учетом наличия льготных периодов, а так же отпусков, заболеваний и командировок в них
        /// </summary>
        /// <param name="personvacations"></param>
        /// <param name="personills"></param>
        /// <param name="personworktrips"></param>
        /// <returns></returns>
        public List<Personjob> GeneratePersonjobsByPeriods(DateTime userDate, IEnumerable<Personvacation> personvacations, IEnumerable<Personill> personills, IEnumerable<Personworktrip> personworktrips)
        {
            // Получаем конец периода работы
            DateTime endJob = End.GetValueOrDefault();
            if (Actual > 0)
            {
                endJob = GetEndActual(userDate);
            }

            List<Personjob> personjobs = new List<Personjob>();
            List<Period> periods = new List<Period>();
            Period initialJobPeriod = new Period(Start.GetValueOrDefault(), endJob);
            Personjob initialPersonjob = GetPersonjobElement();
            initialPersonjob.Start = initialJobPeriod.Start;
            initialPersonjob.End = initialJobPeriod.End;
            initialJobPeriod.Personjob = initialPersonjob;

            List<Period> jobPeriodParts = new List<Period>();
            jobPeriodParts.Add(initialJobPeriod);


            /// Дробим при необходимости на льготные и не-льготные.
            foreach (var personjobprivelege in Personjobpriveleges)
            {
                foreach (var personjobprivelegeperiod in personjobprivelege.Personjobprivelegeperiods)
                {
                    Period privelegePeriod = new Period(personjobprivelegeperiod.Start.GetValueOrDefault(), personjobprivelegeperiod.End.GetValueOrDefault());
                    List<Period> newJobPeriodParts = new List<Period>();
                    Period splittedPeriod = null;
                    foreach (Period period in jobPeriodParts)
                    {
                        List<Period> splittedParts = period.Split(privelegePeriod.Start.GetValueOrDefault(), privelegePeriod.End.GetValueOrDefault());
                        
                        // Что-то да раздробило
                        if (splittedParts.Count > 0)
                        {
                            foreach (Period splittedPart in splittedParts)
                            {
                                // Добавляем льготный период в список работ
                                if (splittedPart.Splitter)
                                {
                                    newJobPeriodParts.Add(splittedPart);
                                    //periods.Add(splittedPart);
                                    Personjob personjob = GetPersonjobElement();
                                    personjob.Start = splittedPart.Start;
                                    personjob.End = splittedPart.End;
                                    personjob.Personjobprivelege = GetPersonjobprivelegeElement(personjobprivelege, personjobprivelegeperiod);
                                    splittedPart.Personjob = personjob;
                                    //personjobs.Add(personjob);
                                } else
                                {
                                    Personjob personjob = GetPersonjobElement();
                                    personjob.Start = splittedPart.Start;
                                    personjob.End = splittedPart.End;
                                    splittedPart.Personjob = personjob;

                                    newJobPeriodParts.Add(splittedPart);
                                }
                            }
                            splittedPeriod = period;
                            break;
                        }
                    }
                    // Удаляем раздробленный период
                    if (splittedPeriod != null)
                    {
                        jobPeriodParts.Remove(splittedPeriod);
                    }
                    // Добавляем осколки раздробленного периода
                    jobPeriodParts.AddRange(newJobPeriodParts);
                    //jobPeriodParts = newJobPeriodParts;


                    //foreach (Personvacation personvacation in personvacations)
                    //{
                    //    // Отпуск входит в льготный период.
                    //    if (periodToSplit.InnerPart(personvacation.Date.GetValueOrDefault(), personvacation.Date.GetValueOrDefault().AddDays(personvacation.Duration)) != null)
                    //    {

                    //    }
                    //}
                }
            }

            // Эти списки нам нужны, чтобы когда периоды, мы имели представление, какой период убрать и вместо него оставить осколок.
            List<Period> jobPeriodPartsAddition = new List<Period>();
            List<Period> jobPeriodPartsRemove = new List<Period>();
            // Смотрим по отпускам, есть ли те, которые дробят трудовую деятельность
            foreach (Personvacation personvacation in personvacations)
            {
                DateTime vacationStart = personvacation.Date.GetValueOrDefault();
                DateTime vacationEnd = DateTime.Now;
                // Был случай, когда в длительность отпуска прописали 500 000, поэтому выдавало ошибку вычисления. Проверка
                if (personvacation.Duration < 5000)
                {
                    vacationEnd = personvacation.Date.GetValueOrDefault().AddDays(personvacation.Duration - 1);
                }
                 

                // Проходимся по периодам, чтобы посмотреть, в какой из них попадает отпуск, чтобы убрать оттуда не-льготный отпуск.
                foreach (Period jobPeriodPart in jobPeriodParts)
                {
                    // Является льготным периодом
                    if (jobPeriodPart.Personjob != null && jobPeriodPart.Personjob.Personjobprivelege != null)
                    {
                        // Отпуск входит в льготный период.
                        if (jobPeriodPart.InnerPart(vacationStart, vacationEnd) != null)
                        {
                            // Отображать в списке отпусков, входящих в льготные периоды
                            personvacation.DisplayPrivelege = true; 

                            List<Period> newJobPeriodParts = new List<Period>();
                            List<Period> splittedParts = jobPeriodPart.Split(vacationStart, vacationEnd);
                            // Что-то да раздробило
                            if (splittedParts.Count > 0)
                            {
                                foreach (Period splittedPart in splittedParts)
                                {
                                    // Не должно быть делителем, так как делитель это отпуск/больничный/командировка, которые выпадают 
                                    if (!splittedPart.Splitter)
                                    {
                                        //periods.Add(splittedPart);
                                        Personjob personjob = GetPersonjobElement();
                                        personjob.Start = splittedPart.Start;
                                        personjob.End = splittedPart.End;
                                        personjob.Personjobprivelege = jobPeriodPart.Personjob.Personjobprivelege.Clone(personjob.Start, personjob.End);
                                        
                                        splittedPart.Personjob = personjob;
                                        //personjobs.Add(personjob);
                                        jobPeriodPartsAddition.Add(splittedPart);
                                    }
                                    // Засчитывается как обычный период
                                    else
                                    {
                                        Personjob personjob = GetPersonjobElement();
                                        personjob.Start = splittedPart.Start;
                                        personjob.End = splittedPart.End;

                                        splittedPart.Personjob = personjob;
                                        jobPeriodPartsAddition.Add(splittedPart);
                                    }

                                }
                                jobPeriodPartsRemove.Add(jobPeriodPart);
                                break;
                            }
                        }
                    }
                }
                // Здесь мы из списка периодов удаляем цельный период и добавляем осколки после того, как дробили льготный период отпуском/командировкой/больничным
                jobPeriodParts.AddRange(jobPeriodPartsAddition);
                jobPeriodPartsAddition = new List<Period>();
                foreach (Period removePeriod in jobPeriodPartsRemove)
                {
                    jobPeriodParts.Remove(removePeriod);
                }
                jobPeriodPartsRemove = new List<Period>();
            }

            jobPeriodPartsAddition = new List<Period>();
            jobPeriodPartsRemove = new List<Period>();
            // Смотрим по больничным, есть ли те, которые дробят трудовую деятельность
            foreach (Personill personill in personills)
            {
                
                DateTime illStart = personill.Datestart;
                DateTime illEnd = personill.Dateend;

                // Проходимся по периодам, чтобы посмотреть, в какой из них попадает отпуск, чтобы убрать оттуда не-льготный отпуск.
                foreach (Period jobPeriodPart in jobPeriodParts)
                {
                    // Является льготным периодом
                    if (jobPeriodPart.Personjob != null && jobPeriodPart.Personjob.Personjobprivelege != null)
                    {
                        // Отпуск входит в льготный период.
                        if (jobPeriodPart.InnerPart(illStart, illEnd) != null)
                        {
                            // Отображать в списке отпусков, входящих в льготные периоды
                            personill.DisplayPrivelege = true;
                            // Если болезнь зачитывается как льготный период, то пропускаем
                            if (personill.Privelege > 0)
                            {
                                break;
                            }

                            List<Period> newJobPeriodParts = new List<Period>();

                            List<Period> splittedParts = jobPeriodPart.Split(illStart, illEnd);
                            // Что-то да раздробило
                            if (splittedParts.Count > 0)
                            {
                                foreach (Period splittedPart in splittedParts)
                                {
                                    // Не должно быть делителем, так как делитель это отпуск/больничный/командировка, которые выпадают 
                                    if (!splittedPart.Splitter)
                                    {
                                        //periods.Add(splittedPart);
                                        Personjob personjob = GetPersonjobElement();
                                        personjob.Start = splittedPart.Start;
                                        personjob.End = splittedPart.End;
                                        personjob.Personjobprivelege = jobPeriodPart.Personjob.Personjobprivelege.Clone(personjob.Start, personjob.End);
                                        splittedPart.Personjob = personjob;
                                        //personjobs.Add(personjob);
                                        jobPeriodPartsAddition.Add(splittedPart);
                                    } else
                                    {
                                        Personjob personjob = GetPersonjobElement();
                                        personjob.Start = splittedPart.Start;
                                        personjob.End = splittedPart.End;
                                        splittedPart.Personjob = personjob;
                                        jobPeriodPartsAddition.Add(splittedPart);
                                    }

                                }
                                jobPeriodPartsRemove.Add(jobPeriodPart);
                                break;
                            }
                        }
                    }
                }
                // Здесь мы из списка периодов удаляем цельный период и добавляем осколки после того, как дробили льготный период отпуском/командировкой/больничным
                jobPeriodParts.AddRange(jobPeriodPartsAddition);
                jobPeriodPartsAddition = new List<Period>();
                foreach (Period removePeriod in jobPeriodPartsRemove)
                {
                    jobPeriodParts.Remove(removePeriod);
                }
                jobPeriodPartsRemove = new List<Period>();
            }

            jobPeriodPartsAddition = new List<Period>();
            jobPeriodPartsRemove = new List<Period>();
            // Смотрим по командировка, есть ли те, которые дробят трудовую деятельность
            foreach (Personworktrip personworktrip in personworktrips)
            {
                
                DateTime worktripStart = personworktrip.Tripdate;
                DateTime worktripEnd = personworktrip.Tripdate.AddDays(personworktrip.Days - 1);

                // Проходимся по периодам, чтобы посмотреть, в какой из них попадает отпуск, чтобы убрать оттуда не-льготный отпуск.
                foreach (Period jobPeriodPart in jobPeriodParts)
                {
                    // Является льготным периодом
                    if (jobPeriodPart.Personjob != null && jobPeriodPart.Personjob.Personjobprivelege != null)
                    {
                        // Отпуск входит в льготный период.
                        if (jobPeriodPart.InnerPart(worktripStart, worktripEnd) != null)
                        {
                            // Отображать в списке отпусков, входящих в льготные периоды
                            personworktrip.DisplayPrivelege = true;
                            // Если болезнь зачитывается как льготный период, то пропускаем
                            if (personworktrip.Privelege > 0)
                            {
                                break;
                            }

                            List<Period> newJobPeriodParts = new List<Period>();
                            List<Period> splittedParts = jobPeriodPart.Split(worktripStart, worktripEnd);
                            // Что-то да раздробило
                            if (splittedParts.Count > 0)
                            {
                                foreach (Period splittedPart in splittedParts)
                                {
                                    // Не должно быть делителем, так как делитель это отпуск/больничный/командировка, которые выпадают 
                                    if (!splittedPart.Splitter)
                                    {
                                        //periods.Add(splittedPart);
                                        Personjob personjob = GetPersonjobElement();
                                        personjob.Start = splittedPart.Start;
                                        personjob.End = splittedPart.End;
                                        personjob.Personjobprivelege = jobPeriodPart.Personjob.Personjobprivelege.Clone(personjob.Start, personjob.End);
                                        splittedPart.Personjob = personjob;
                                        //personjobs.Add(personjob);
                                        jobPeriodPartsAddition.Add(splittedPart);
                                    } else
                                    {
                                        Personjob personjob = GetPersonjobElement();
                                        personjob.Start = splittedPart.Start;
                                        personjob.End = splittedPart.End;
                                        splittedPart.Personjob = personjob;
                                        jobPeriodPartsAddition.Add(splittedPart);
                                    }

                                }
                                jobPeriodPartsRemove.Add(jobPeriodPart);
                                break;
                            }
                        }
                    }
                }
                // Здесь мы из списка периодов удаляем цельный период и добавляем осколки после того, как дробили льготный период отпуском/командировкой/больничным
                jobPeriodParts.AddRange(jobPeriodPartsAddition);
                jobPeriodPartsAddition = new List<Period>();
                foreach (Period removePeriod in jobPeriodPartsRemove)
                {
                    jobPeriodParts.Remove(removePeriod);
                }
                jobPeriodPartsRemove = new List<Period>();
            }

            periods.AddRange(jobPeriodParts);
            // Добавляем в список работ оставшиеся не-льготные куски
            foreach(Period jobPeriodPart in periods)
            {
                if (jobPeriodPart.Personjob != null)
                {
                    personjobs.Add(jobPeriodPart.Personjob);
                }
                //// Если это не льготный период, то мы еще его не вносили. Льготные периоды мы вносили ранее "по факту"
                //if (!jobPeriodPart.Splitter)
                //{
                //    Personjob personjob = GetPersonjobElement();
                //    personjob.Start = jobPeriodPart.Start;
                //    personjob.End = jobPeriodPart.End;
                //    personjobs.Add(personjob);
                //}
            }

            return personjobs;
        }

        /// <summary>
        /// Чей приказ, дата и номер на вывод для фронта
        /// </summary>
        /// <returns></returns>
        public string OrderString()
        {
            string orderstring = Orderwho + " " + Orderdate.GetValueOrDefault().ToString("yyyy.MM.dd") + " " + Ordernumber + " " + Ordernumbertype;
            orderstring = orderstring.Trim();
            return orderstring;
        }
    }
}
