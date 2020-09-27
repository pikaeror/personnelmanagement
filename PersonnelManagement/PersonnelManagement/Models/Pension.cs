using PersonnelManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    /// <summary>
    /// Трудовая деятельность и образование, входящие или которые могут входить в выслугу лет в выслугу лет
    /// </summary>
    public class Pension
    {
        public string Positionplace { get; set; }
        public string Orderstring { get; set; }
        public string Orderwho { get; set; }
        public DateTime Orderdate { get; set; }
        public string Ordernumber { get; set; }
        public string Ordernumbertype { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public double Coef { get; set; }
        public int Daysbeforecoef { get; set; }
        public int Monthsbeforecoef { get; set; }
        public int Yearsbeforecoef { get; set; }
        public int Daysaftercoef { get; set; }
        public int Monthsaftercoef { get; set; }
        public int Yearsaftercoef { get; set; }

        // Является ли период выслуги лет образованием
        public bool Education { get; set; } = false;
        // Засчитывается ли данное образование в выслугу лет. Если нет - то если и записывается, то только в не засчитываемые 
        public bool EducationConsider { get; set; } = false;
        // Является ли очным (true) или заочным, очным вечерним, дистанционным, соискательством (false)
        public bool EducationFulltime { get; set; } = false;
        // Являлось ли образование службой
        public bool EducationMilitary { get; set; } = false;
        // Является ли период выслуги лет трудовой деятельностью
        public bool Job { get; set; } = false;
        // Период выслуги лет является службой.
        public bool JobMilitary { get; set; } = false;

        public static List<Pension> GetPensions(DateTime userDate, Repository repository, PersonManager person, IEnumerable<Personjob> personjobs, IEnumerable<Personeducation> personeducations,
            IEnumerable<Personvacation> personvacations, IEnumerable<Personill> personills, IEnumerable<Personworktrip> personworktrips)
        {
            List<Pension> pensions = new List<Pension>();

            foreach (Personjob personjobBase in personjobs)
            {
                List<Personjob> personjobsSplitted = personjobBase.GeneratePersonjobsByPeriods(userDate, personvacations, personills, personworktrips);
                foreach (Personjob personjob in personjobsSplitted)
                {

                    Pension pension = new Pension();
                    pension.Job = true;
                    pension.Start = personjob.Start.GetValueOrDefault();
                    pension.End = personjob.End.GetValueOrDefault();

                    // Служба
                    if (personjob.Jobtype == 2)
                    {
                        pension.JobMilitary = true;
                    }
                    if (personjob.Jobpositionplace.Length > 0 && personjob.Manual == 0)
                    {
                        pension.Positionplace = personjob.Jobpositionplace;
                    }
                    else
                    {
                        pension.Positionplace = personjob.Jobposition + " " + personjob.Jobplace;
                    }
                    pension.Orderstring = personjob.OrderString();

                    int days = Period.DayDiff(personjob.Start.GetValueOrDefault(), personjob.End.GetValueOrDefault());
                    int months = Period.MonthDiff(personjob.Start.GetValueOrDefault(), personjob.End.GetValueOrDefault());
                    int years = Period.YearDiff(personjob.Start.GetValueOrDefault(), personjob.End.GetValueOrDefault());
                    if (personjob.Personjobprivelege != null)
                    {
                        pension.Coef = personjob.Personjobprivelege.Coef;
                    } else
                    {
                        pension.Coef = 1;
                    }
                    
                    pension.Daysbeforecoef = days;
                    pension.Monthsbeforecoef = months;
                    pension.Yearsbeforecoef = years;
                    pension.Daysaftercoef = (int)(days * pension.Coef);
                    pension.Monthsaftercoef = (int)(months * pension.Coef);
                    pension.Yearsaftercoef = (int)(years * pension.Coef);

                    pensions.Add(pension);
                    // Содержит льготные периоды
                }


            }

            foreach (Personeducation personeducation in personeducations)
            {
                List<Personeducation> personeducationParts = personeducation.GeneratePersoneducationsByPeriods(userDate, personjobs);

                foreach(Personeducation personeducationPart in personeducationParts)
                {
                    Pension pension = new Pension();
                    Educationpositiontype educationpositiontype = repository.Educationpositiontypes.FirstOrDefault(e => e.Id == personeducationPart.Educationpositiontype);
                    string positionString = "";
                    if (educationpositiontype != null)
                    {
                        positionString = educationpositiontype.Name;
                    }

                    // Если основное образование, то в выслуге лет пишем должность (студент, курсант, слушатель) + наименование учреждения в родительном роде
                    if (personeducationPart.Main == 1)
                    {
                        pension.Positionplace = positionString + " " + personeducationPart.Name2;
                    // Если образование дополнительное, то просто наименование учреждения образования в именительном падеже, так как дополнительное образование мы пока не обновляли
                    // и там нету поля должность.
                    } else
                    {
                        pension.Positionplace = positionString + " " + personeducationPart.Name;
                    }
                    
                    pension.Orderstring = "";

                    pension.Education = true;
                    //pension.EducationConsider = personeducationPart.Consider;
                    pension.Start = personeducationPart.Start.GetValueOrDefault();
                    pension.End = personeducationPart.End.GetValueOrDefault();
                    //pension.Start = personeducation.Start.GetValueOrDefault();
                    //pension.End = personeducation.End.GetValueOrDefault();
                    pension.EducationMilitary = personeducationPart.Military;
                    pension.EducationConsider = personeducationPart.Consider;
                    pension.Coef = 1;

                    // Если образование не-военное, то засчитывается только с 17 лет
                    DateTime countStart = personeducationPart.Start.GetValueOrDefault();
                    DateTime countEnd = personeducationPart.End.GetValueOrDefault();
                    DateTime years17 = person.Birthdate.AddYears(17);
                    if (years17 > countStart && !pension.EducationMilitary)
                    {
                        countStart = years17;
                    }

                    int days = Period.DayDiff(countStart, countEnd);
                    int months = Period.MonthDiff(countStart, countEnd);
                    int years = Period.YearDiff(countStart, countEnd);
                    pension.Daysbeforecoef = days;
                    pension.Monthsbeforecoef = months;
                    pension.Yearsbeforecoef = years;
                    pension.Daysaftercoef = days;
                    pension.Monthsaftercoef = months;
                    pension.Yearsaftercoef = years;

                    pensions.Add(pension);
                }
            }

            pensions = pensions.OrderBy(p => p.Start).ToList();
            return pensions;
        }
    }
}
