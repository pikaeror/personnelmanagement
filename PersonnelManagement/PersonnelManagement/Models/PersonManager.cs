using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonnelManagement.Udostoverenia;

namespace PersonnelManagement.Models
{
    public class PersonManager: Person
    {

        // Краткое наименование подразделения сотрудника - например, управление кадров
        public string Structurename { get; set; }
        public string Structurename1 { get; set; }
        public string Structurename2 { get; set; }
        public string Structurename3 { get; set; }
        public string Structurename4 { get; set; }
        public string Structurename5 { get; set; }
        public string Structurename6 { get; set; }
        // Полное наименование подразделения сотрудника - например, управление кадров Министерства по чрезвычайным ситуациям
        public string Structuretree { get; set; }
        public string Structuretree1 { get; set; }
        public string Structuretree2 { get; set; }
        public string Structuretree3 { get; set; }
        public string Structuretree4 { get; set; }
        public string Structuretree5 { get; set; }
        public string Structuretree6 { get; set; }
        // Полное наименование должности сотрудника - например, главный специалист отдела организационно-штатной работы управления кадров Министерства по чрезвычайным ситуациям
        public string Positiontree { get; set; }
        public string Positiontree1 { get; set; }
        public string Positiontree2 { get; set; }
        public string Positiontree3 { get; set; }
        public string Positiontree4 { get; set; }
        public string Positiontree5 { get; set; }
        public string Positiontree6 { get; set; }
        // Наименование должности сотрудника - например, главный специалист
        public string Positiontypestring { get; set; }
        public string Positiontype1string { get; set; }
        public string Positiontype2string { get; set; }
        public string Positiontype3string { get; set; }
        public string Positiontype4string { get; set; }
        public string Positiontype5string { get; set; }
        public string Positiontype6string { get; set; }
        public Personrank[] Personranks { get; set; }
        public Personcontract[] Personcontracts { get; set; }
        public Personrelative[] Personrelatives { get; set; }
        public Personattestation[] Personattestations { get; set; }
        public Personvacation[] Personvacations { get; set; }
        public Personlanguage[] Personlanguages { get; set; }
        public Personjob[] Personjobs { get; set; }
        public Personpenalty[] Personpenalties { get; set; }
        public Personworktrip[] Personworktrips { get; set; }
        public Personelection[] Personelections { get; set; }
        public Personscience[] Personsciences { get; set; }
        public Personreward[] Personrewards { get; set; }
        public Personill[] Personills { get; set; }
        public Personeducation[] Personeducations { get; set; }
        public CertificateManager[] Certificates { get; set; }
        public PersonphysicalManager[] Personphysicals { get; set; }
        public Persondriver[] Persondrivers { get; set; }
        public Personpermission[] Personpermissions { get; set; }
        public Personprivelege[] Personpriveleges { get; set; }
        public Persondispanserization[] Persondispanserizations { get; set; }
        public Personvvk[] Personvvks { get; set; }
        public Personjobprivelege[] Personjobpriveleges { get; set; }
        public Persontransfer[] Persontransfers { get; set; }
        public Pension[] Pensions { get; set; }


        // Здесь хранится вычисляемая инфа. То есть информация, не хранящая в базе данных, но вычисляющаяся динамически на ее основе
        public double Experience { get; set; } = 0; // Стаж 
        public int ExperienceYears { get; set; } = 0; // выслуга в годах на текущий период
        public int ExperienceMonths { get; set; } = 0; // выслуга в месяцах за вычетом лет на текущий период
        public int ExperienceDays { get; set; } = 0; // выслуга в днях за вычетом лет и месяцев на текущий период
        public double Experienceprivelege { get; set; } = 0; // Суммарное количество льготных дней 
        public int Vacationdayscurrentyear { get; set; } = 0; // Для военных лиц, сколько дней отпуска выделено в этом году, с учетом возможных льготных условий
        public int Vacationdaysleft { get; set; } = 0; // Cколько дней отпуска осталось с прошлого года
        public int Vacationdaysused { get; set; } = 0; //  Сколько в этом году дней отпуска уже использовано из положенного
        public DateTime? Vacationshiftdate { get; set; } = null; // Если в текущем году происходит увеличение дней отпуска, записывать сюда дату
        public int Vacationshiftbefore { get; set; } = 0; // Если в текущем году происходит увеличение дней отпуска, сколько дней до увеличения.
        public int Vacationshiftafter { get; set; } = 0; // Если в текущем году происходит увеличение дней отпуска, сколько дней после увеличения.
        public int Privelegesmissed { get; set; } = 0; // Сколько дней льготной выслуги было потеряно из-за больничных 
        public bool Military { get; set; } = false; // Высчитывает, является ли человек военным или нет
        public Personjob Currentjob { get; set; } // Текущая должность/работа. То есть которая по настоящее время.
        public DateTime? Jobstart { get; set; } = null; // С какой даты вообще считать, откуда могли пойти первые переносы отпуска. Если гражданский, то еще считать откуда начинается рабочий год.
        public Jobperiod[] Jobperiods { get; set; }
        public Jobperiod Jobperiodcurrent { get; set; }
        public Jobperiod Jobperiodprevious { get; set; }
        public Rank ActualRank { get; set; }
        public int ActualRankExperience { get; set; } // стаж в днях на последней занимаемой должности.
        public int Major { get; set; } // Может ли капитан быть повышен до майора при увольнении.
                                       //public int ContractYears 

        public int Stateserviceyears { get; set; } = 0; // Выслуга лет госслужащим за все периоды работы
        public int Stateservicemonths { get; set; } = 0; // Выслуга месяцев госслужащим за все периоды работы
        public int Stateservicedays { get; set; } = 0; // Выслуга дней госслужащим за все периоды работы

        public int Pensioncivilyears { get; set; } = 0;
        public int Pensioncivilmonths { get; set; } = 0;
        public int Pensioncivildays { get; set; } = 0;
        public int Pensionmilitaryyears { get; set; } = 0;
        public int Pensionmilitarymonths { get; set; } = 0;
        public int Pensionmilitarydays { get; set; } = 0;


        public string pension_A { get; set; } = null;
        public string pension_B { get; set; } = null;

        public int appending_days { get; set; } = 0;
        
        public string pension_A_with { get; set; } = null;
        public string pension_B_with { get; set; } = null;


        public PersonManager(Person person)
        {

            Id = person.Id;
            Position = person.Position;
            Structure = person.Structure;
            Name = person.Name;
            Surname = person.Surname;
            Fathername = person.Fathername;
            Birthdate = person.Birthdate;
            Photo = person.Photo;
            Gender = person.Gender;
            Passportid = person.Passportid;
            Passportnum = person.Passportnum;
            Passportdatestart = person.Passportdatestart;
            Passportdateend = person.Passportdateend;
            Birthlocation = person.Birthlocation;
            Registercountry = person.Registercountry;
            Registerstate = person.Registerstate;
            Registersubstate = person.Registersubstate;
            Registercitysubstate = person.Registercitysubstate;
            Registercitytype = person.Registercitytype;
            Registercity = person.Registercity;
            Registerstreettype = person.Registerstreettype;
            Registerstreet = person.Registerstreet;
            Registerhouse = person.Registerhouse;
            Registerhousing = person.Registerhousing;
            Registerflat = person.Registerflat;
            Livecountry = person.Livecountry;
            Livestate = person.Livestate;
            Livesubstate = person.Livesubstate;
            Livecitysubstate = person.Livecitysubstate;
            Livecitytype = person.Livecitytype;
            Livecity = person.Livecity;
            Livestreettype = person.Livestreettype;
            Livestreet = person.Livestreet;
            Livehouse = person.Livehouse;
            Livehousing = person.Livehousing;
            Liveflat = person.Liveflat;
            Nationality = person.Nationality;
            Maritalstatus = person.Maritalstatus;
            Science = person.Science;
            Numpersonal = person.Numpersonal;
            Wound = person.Wound;
            Sciencerank = person.Sciencerank;
            Surnameother = person.Surnameother;
            Name2 = person.Name2;
            Surname2 = person.Surname2;
            Fathername2 = person.Fathername2;
            Name3 = person.Name3;
            Surname3 = person.Surname3;
            Fathername3 = person.Fathername3;
            Name4 = person.Name4;
            Surname4 = person.Surname4;
            Fathername4 = person.Fathername4;
            Name5 = person.Name5;
            Surname5 = person.Surname5;
            Fathername5 = person.Fathername5;
            Name6 = person.Name6;
            Surname6 = person.Surname6;
            Fathername6 = person.Fathername6;
            Removed = person.Removed;
            Registerstatenum = person.Registerstatenum;
            Registersubstatenum = person.Registersubstatenum;
            Livestatenum = person.Livestatenum;
            Livesubstatenum = person.Livesubstatenum;
            Birthcountry = person.Birthcountry;
            Birthstate = person.Birthstate;
            Birthsubstate = person.Birthsubstate;
            Birthcitysubstate = person.Birthcitysubstate;
            Birthcitytype = person.Birthcitytype;
            Birthcity = person.Birthcity;
            Birthadditional = person.Birthadditional;
            Namesubject = person.Namesubject;
            Fathernamesubject = person.Fathernamesubject;
            Surnamesubject = person.Surnamesubject;
            Gendersubject = person.Gendersubject;

            Structurename = "";
            Structurename1 = "";
            Structurename2 = "";
            Structurename3 = "";
            Structuretree = "";
            Structuretree1 = "";
            Structuretree2 = "";
            Structuretree3 = "";
            Positiontypestring = "";
            Personranks = new Personrank[0];
            Personcontracts = new Personcontract[0];
            Personrelatives = new Personrelative[0];
            Personattestations = new Personattestation[0];
            Personvacations = new Personvacation[0];
            Personlanguages = new Personlanguage[0];
            Personjobs = new Personjob[0];
            Personpenalties = new Personpenalty[0];
            Personworktrips = new Personworktrip[0];
            Personelections = new Personelection[0];
            Personsciences = new Personscience[0];
            Personrewards = new Personreward[0];
            Personills = new Personill[0];
            Personeducations = new Personeducation[0];
            Certificates = new CertificateManager[0];
            Personphysicals = new PersonphysicalManager[0];
            Persondrivers = new Persondriver[0];
            Personpermissions = new Personpermission[0];
            Personpriveleges = new Personprivelege[0];
            Persondispanserizations = new Persondispanserization[0];
            Personvvks = new Personvvk[0];
            Personjobpriveleges = new Personjobprivelege[0];
            Jobperiods = new Jobperiod[0];
            Persontransfers = new Persontransfer[0];
            Pensions = new Pension[0];

            Jobperiodcurrent = null;
            Jobperiodprevious = null;
        }

        public static List<PersonManager> PersonsToPersonManagers(Repository repository, User user, IEnumerable<Person> persons, bool fastSearch = false)
        {
            List<PersonManager> personManagers = new List<PersonManager>();
            foreach (Person person in persons)
            {
                personManagers.Add(repository.GetPersonManager(user, person, fastSearch));
            }
            return personManagers;
        }

        /// <summary>
        /// Мы определяем, является ли сотрудник атестованным (военным) или гражданским.
        /// Определяем по текущей занимаемой должности
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool IsMilitary(User user)
        {
            DateTime date = DateTime.Now;
            if (user != null && user.Date != null)
            {
                date = user.Date.GetValueOrDefault();
            }
            if (Personjobs == null)
            {
                return false;
            }
            foreach (Personjob personjob in Personjobs)
            {
                DateTime start = personjob.Start.GetValueOrDefault();
                DateTime end = personjob.GetEndActual(date);
                // Если служба и 
                if (personjob.Jobtype == 2 && start <= date && end >= date)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
