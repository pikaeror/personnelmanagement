using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Diagnostics;
using PersonnelManagement.Services;
using System.Runtime.CompilerServices;
using PersonnelManagement.Udostoverenia;
using System.Collections;
using Itenso.TimePeriod;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;
using DocumentFormat.OpenXml.InkML;
using PersonnelManagement.Utils;
using System.Globalization;

namespace PersonnelManagement.Models
{
    public class Repository
    {
        private pmContext context;
        private certContext certContext;

        public Repository(pmContext ctx, certContext cctx)
        {
            context = ctx;
            certContext = cctx;
        }

        public pmContext GetContext()
        {
            return this.context;
        }

        // Поля для быстрого доступка к таблицам из контекста базы данных

        public IQueryable<User> Users => context.User;
        public IQueryable<Session> Sessions => context.Session;
        public IQueryable<Structure> Structures => context.Structure;
        public IQueryable<Department> Departments => context.Department;
        public IQueryable<Position> Positions => context.Position;
        public IQueryable<Rank> Ranks => context.Rank;
        public IQueryable<Sourceoffinancing> Sourcesoffinancings => context.Sourceoffinancing;
        public IQueryable<Positiontype> Positiontypes => context.Positiontype;
        public IQueryable<Positioncategory> Positioncategories => context.Positioncategory;
        public IQueryable<Positioncategoryrank> Positioncategoryranks => context.Positioncategoryrank;
        public IQueryable<Decree> Decrees => context.Decree;
        public IQueryable<Decreeoperation> Decreeoperations => context.Decreeoperation;
        public IQueryable<Mailexplorer> Mailexplorers => context.Mailexplorer;
        public IQueryable<Mailfolder> Mailfolders => context.Mailfolder;
        public IQueryable<Mrd> Mrds => context.Mrd;
        public IQueryable<Positionmrd> Positionmrds => context.Positionmrd;
        public IQueryable<Altrankcondition> Altrankconditions  => context.Altrankcondition;
        public IQueryable<Altrankconditiongroup> Altrankconditiongroups => context.Altrankconditiongroup;
        public IQueryable<Altrank> Altranks => context.Altrank;
        public IQueryable<Positionhistory> Positionhistories => context.Positionhistory;
        public IQueryable<Departmentrename> Departmentrenames => context.Departmentrename;
        public IQueryable<Structureregion> Structureregions => context.Structureregion;
        public IQueryable<Structuretype> Structuretypes => context.Structuretype;
        public IQueryable<Person> Persons => context.Person;
        public IQueryable<Personphoto> Personphotos => context.Personphoto;
        public IQueryable<Personrank> Personranks => context.Personrank;
        public IQueryable<Personcontract> Personcontracts => context.Personcontract;
        public IQueryable<Personrelative> Personrelatives => context.Personrelative;
        public IQueryable<Relativetype> Relativetypes => context.Relativetype;
        public IQueryable<Personvacation> Personvacations => context.Personvacation;
        public IQueryable<Personattestation> Personattestations => context.Personattestation;
        public IQueryable<Attestationtype> Attestationtypes => context.Attestationtype;
        public IQueryable<Vacationmilitary> Vacationmilitaries => context.Vacationmilitary;
        public IQueryable<Vacationtype> Vacationtypes => context.Vacationtype;
        public IQueryable<Languagetype> Languagetypes => context.Languagetype;
        public IQueryable<Languageskill> Languageskills => context.Languageskill;
        public IQueryable<Jobtype> Jobtypes => context.Jobtype;
        public IQueryable<Servicetype> Servicetypes => context.Servicetype;
        public IQueryable<Servicefeature> Servicefeatures => context.Servicefeature;
        public IQueryable<Servicecoef> Servicecoefs => context.Servicecoef;
        public IQueryable<Personjob> Personjobs => context.Personjob;
        public IQueryable<Personlanguage> Personlanguages => context.Personlanguage;
        public IQueryable<Autobiographydata> Autobiographydatas => context.Autobiographydata;
        public IQueryable<Cabinetdata> Cabinetdatas => context.Cabinetdata;
        public IQueryable<Declarationdata> Declarationdatas => context.Declarationdata;
        public IQueryable<Declarationrelative> Declarationrelatives => context.Declarationrelative;
        public IQueryable<Declarationtabledata> Declarationtabledatas => context.Declarationtabledata;
        public IQueryable<Profilerelatives> Profilerelatives => context.Profilerelatives;
        public IQueryable<Profiledata> Profiledatas => context.Profiledata;
        public IQueryable<Pseducation> Pseducations => context.Pseducation;
        public IQueryable<Pswork> Psworks => context.Pswork;
        public IQueryable<Sheetdata> Sheetdatas => context.Sheetdata;
        public IQueryable<Sheetpolitics> Sheetpolitics => context.Sheetpolitics;
        public IQueryable<Personpenalty> Personpenalties => context.Personpenalty;
        public IQueryable<Dismissalclauses> Dismissalclauses => context.Dismissalclauses;
        public IQueryable<Personworktrip> Personworktrips => context.Personworktrip;
        public IQueryable<Penalty> Penalties => context.Penalty;
        public IQueryable<Country> Countries => context.Country;
        public IQueryable<Personelection> Personelections => context.Personelection;
        public IQueryable<Personscience> Personsciences => context.Personscience;
        public IQueryable<Personreward> Personrewards => context.Personreward;
        public IQueryable<Reward> Rewards => context.Reward;
        public IQueryable<Rewardtype> Rewardtypes => context.Rewardtype;
        public IQueryable<Personill> Personills => context.Personill;
        public IQueryable<Illcode> Illcodes => context.Illcode;
        public IQueryable<Illregime> Illregimes => context.Illregime;
        public IQueryable<Personeducation> Personeducations => context.Personeducation;
        public IQueryable<Educationlevel> Educationlevels => context.Educationlevel;
        public IQueryable<Educationtype> Educationtypes => context.Educationtype;
        public IQueryable<Personphysical> Personphysicals => context.Personphysical;
        public IQueryable<Physicalfield> Physicalfields => context.Physicalfield;
        public IQueryable<Normativ> Normativs => context.Normativ;
        public IQueryable<Agency> Agencies => certContext.Agency;
        public IQueryable<Base> Bases => certContext.Base;
        public IQueryable<Blankform> Blankforms => certContext.Blankform;
        public IQueryable<Issuingauthority> Issuingauthorities => certContext.Issuingauthority;
        public IQueryable<Post> Posts => certContext.Post;
        public IQueryable<Udostoverenia.Rank> cRanks => certContext.Rank;
        public IQueryable<Certificate> Certificates => certContext.Certificate;
        public IQueryable<Rejectreason> Rejectreasons => certContext.Rejectreason;
        public IQueryable<Persondriver> Persondrivers => context.Persondriver;
        public IQueryable<Drivertype> Drivertypes => context.Drivertype;
        public IQueryable<Drivercategory> Drivercategories => context.Drivercategory;
        public IQueryable<Personpermission> Personpermissions => context.Personpermission;
        public IQueryable<Permissiontype> Permissiontypes => context.Permissiontype;
        public IQueryable<Personprivelege> Personpriveleges => context.Personprivelege;
        public IQueryable<Persondispanserization> Persondispanserizations => context.Persondispanserization;
        public IQueryable<Personvvk> Personvvks => context.Personvvk;
        public IQueryable<Educationdocument> Educationdocuments => context.Educationdocument;
        public IQueryable<Personjobprivelege> Personjobpriveleges => context.Personjobprivelege;
        public IQueryable<Persontransfer> Persontransfers => context.Persontransfer;
        public IQueryable<Prooftype> Prooftypes => context.Prooftype;
        public IQueryable<Holiday> Holidays => context.Holiday;
        public IQueryable<Persondecree> Persondecrees => context.Persondecree;
        public IQueryable<Persondecreeoperation> Persondecreeoperations => context.Persondecreeoperation;
        public IQueryable<Persondecreeblock> Persondecreeblocks => context.Persondecreeblock;
        public IQueryable<Persondecreeblocktype> Persondecreeblocktypes => context.Persondecreeblocktype;
        public IQueryable<Persondecreeblocksub> Persondecreeblocksubs => context.Persondecreeblocksub;
        public IQueryable<Persondecreeblocksubtype> Persondecreeblocksubtypes => context.Persondecreeblocksubtype;
        public IQueryable<Region> Regions => context.Region;
        public IQueryable<Area> Areas => context.Area;
        public IQueryable<Fire> Fires => context.Fire;
        public IQueryable<Appointtype> Appointtypes => context.Appointtype;
        public IQueryable<Transfertype> Transfertypes => context.Transfertype;
        public IQueryable<Subject> Subjects => context.Subject;
        public IQueryable<Subjectgender> Subjectgenders => context.Subjectgender;
        public IQueryable<Subjectcategory> Subjectcategories => context.Subjectcategory;
        public IQueryable<Interrupttype> Interrupttypes => context.Interrupttype;
        public IQueryable<Changedocumentstype> Changedocumentstypes => context.Changedocumentstype;
        public IQueryable<Personchangedocuments> Personchangedocuments => context.Personchangedocuments;
        public IQueryable<Setpersondatatype> Setpersondatatypes => context.Setpersondatatype;
        public IQueryable<Rewardmoney> Rewardmoneys => context.Rewardmoney;
        public IQueryable<Persondecreeblockintro> Persondecreeblockintros => context.Persondecreeblockintro;
        public IQueryable<Persondecreelevel> Persondecreelevels => context.Persondecreelevel;
        public IQueryable<Ordernumbertype> Ordernumbertypes => context.Ordernumbertype;
        public IQueryable<Personadditionalagreement> Personadditionalagreements => context.Personadditionalagreement;
        public IQueryable<Streettype> Streettypes => context.Streettype;
        public IQueryable<Citytype> Citytypes => context.Citytype;
        public IQueryable<Areaother> Areaothers => context.Areaother;
        public IQueryable<Externalorderwhotype> Externalorderwhotypes => context.Externalorderwhotype;
        public IQueryable<Persondecreetype> Persondecreetypes => context.Persondecreetype;
        public IQueryable<Educationadditionaltype> Educationadditionaltypes => context.Educationadditionaltype;
        public IQueryable<Citysubstate> Citysubstates => context.Citysubstate;
        public IQueryable<Educationstage> Educationstages => context.Educationstage;
        public IQueryable<Educationpositiontype> Educationpositiontypes => context.Educationpositiontype;
        public IQueryable<Educationtypeblock> Educationtypeblocks => context.Educationtypeblock;
        public IQueryable<Educationperiod> Educationperiods => context.Educationperiod;
        public IQueryable<Academicvacation> Academicvacations => context.Academicvacation;
        public IQueryable<Educationmaternity> Educationmaternities => context.Educationmaternity;
        public IQueryable<Personjobprivelegeperiod> Personjobprivelegeperiods => context.Personjobprivelegeperiod;
        public IQueryable<Role> Roles => context.Role;
        public IQueryable<Rights> Rights => context.Rights;
        public IQueryable<Rightsstructure> Rightsstructures => context.Rightsstructure;

        public Dictionary<int, Personeducation> PersoneducationsLocalObjectNew { get; private set; }

        // Таймер. Используется для подсчета времени необходимого для следующего обновления локальных версий таблиц (StructuresLocal и иные). 
        private static Stopwatch stopWatch = Stopwatch.StartNew();

        private static long StructuresGetLastTime = -STRUCTURES_GET_DELAY - 90000;
        private static long StructuresOriginGetLastTime = (3 * -STRUCTURES_GET_DELAY) - 90000;
        private const long STRUCTURES_GET_DELAY = 11000; // in ms
        private static Dictionary<int, Structure> StructuresLocalObject = null;
        private static IEnumerable<Structure> ActualStructuresListWhithOrigin = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Structure> StructuresLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > StructuresGetLastTime + STRUCTURES_GET_DELAY)
            {
                UpdateStructuresLocal();
            }
            return StructuresLocalObject;
        }

        /// <summary>
        /// Обновляем локальную версию таблицы
        /// </summary>
        public void UpdateStructuresLocal()
        {
            StructuresLocalObject = Structures.ToDictionary(structure => structure.Id);
            if (stopWatch.ElapsedMilliseconds > StructuresOriginGetLastTime + 3 * STRUCTURES_GET_DELAY)
            {
                UpdateStructuresOriginLocal();
            }
            StructuresGetLastTime = stopWatch.ElapsedMilliseconds;
        }
        public void UpdateStructuresOriginLocal()
        {
            StructuresOriginGetLastTime = stopWatch.ElapsedMilliseconds;
            ActualStructuresListWhithOrigin = StructuresLocalObject.Values.Where(r => r.Changeorigin != 0);
        }


        private static long DecreesGetLastTime = -DECREES_GET_DELAY - 1;
        private const long DECREES_GET_DELAY = 9000; // in ms
        private static Dictionary<int, Decree> DecreesLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Decree> DecreesLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > DecreesGetLastTime + DECREES_GET_DELAY)
            {
                UpdateDecreesLocal();
            }
            return DecreesLocalObject;
        }
        public void UpdateDecreesLocal()
        {
            DecreesGetLastTime = stopWatch.ElapsedMilliseconds;
            DecreesLocalObject = Decrees.ToDictionary(decree => decree.Id);
        }

        private static long RanksGetLastTime = -RANKS_GET_DELAY - 1;
        private const long RANKS_GET_DELAY = 90000; // in ms, 90 секунд, так как явление крайне редкое
        private static Dictionary<int, Rank> RanksLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Rank> RanksLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > RanksGetLastTime + RANKS_GET_DELAY)
            {
                UpdateRanksLocal();
            }
            return RanksLocalObject;
        }
        public void UpdateRanksLocal()
        {
            RanksGetLastTime = stopWatch.ElapsedMilliseconds;
            RanksLocalObject = Ranks.ToDictionary(rank => rank.Id);
        }

        private static long AltranksGetLastTime = -ALTRANKS_GET_DELAY - 1;
        private const long ALTRANKS_GET_DELAY = 9000; // in ms
        private static Dictionary<int, Altrank> AltranksLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Altrank> AltranksLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > AltranksGetLastTime + ALTRANKS_GET_DELAY)
            {
                UpdateAltranksLocal();
            }
            return AltranksLocalObject;
        }
        public void UpdateAltranksLocal()
        {
            AltranksGetLastTime = stopWatch.ElapsedMilliseconds;
            AltranksLocalObject = Altranks.ToDictionary(altrank => altrank.Id);
        }


        private static long AltrankconditionsGetLastTime = -ALTRANKCONDITIONS_GET_DELAY - 1;
        private const long ALTRANKCONDITIONS_GET_DELAY = 90000; // in ms, 90 секунд, так как явление крайне редкое
        private static Dictionary<int, Altrankcondition> AltrankconditionsLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Altrankcondition> AltrankconditionsLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > AltrankconditionsGetLastTime + ALTRANKCONDITIONS_GET_DELAY)
            {
                UpdateAltrankconditionsLocal();
            }
            return AltrankconditionsLocalObject;
        }
        public void UpdateAltrankconditionsLocal()
        {
            AltrankconditionsGetLastTime = stopWatch.ElapsedMilliseconds;
            AltrankconditionsLocalObject = Altrankconditions.ToDictionary(altrankcondition => altrankcondition.Id);
        }


        private static long AltrankconditiongroupsGetLastTime = -ALTRANKCONDITIONGROUPS_GET_DELAY - 1;
        private const long ALTRANKCONDITIONGROUPS_GET_DELAY = 90000; // in ms, 90 секунд, так как явление крайне редкое
        private static Dictionary<int, Altrankconditiongroup> AltrankconditiongroupsLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Altrankconditiongroup> AltrankconditiongroupsLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > AltrankconditiongroupsGetLastTime + ALTRANKCONDITIONGROUPS_GET_DELAY)
            {
                UpdateAltrankconditiongroupsLocal();
            }
            return AltrankconditiongroupsLocalObject;
        }
        public void UpdateAltrankconditiongroupsLocal()
        {
            AltrankconditiongroupsGetLastTime = stopWatch.ElapsedMilliseconds;
            AltrankconditiongroupsLocalObject = Altrankconditiongroups.ToDictionary(altrankconditiongroup => altrankconditiongroup.Id);
        }


        private static long PositionmrdsGetLastTime = -POSITIONMRDS_GET_DELAY - 1;
        private const long POSITIONMRDS_GET_DELAY = 9000; // in ms
        private static Dictionary<int, Positionmrd> PositionmrdsLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Positionmrd> PositionmrdsLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > PositionmrdsGetLastTime + POSITIONMRDS_GET_DELAY)
            {
                UpdatePositionmrdsLocal();
            }
            return PositionmrdsLocalObject;
        }
        public void UpdatePositionmrdsLocal()
        {
            PositionmrdsGetLastTime = stopWatch.ElapsedMilliseconds;
            PositionmrdsLocalObject = Positionmrds.ToDictionary(positionmrd => positionmrd.Id);
        }


        private static long EducationtypeblocksGetLastTime = -EducationtypeblockS_GET_DELAY - 1;
        private const long EducationtypeblockS_GET_DELAY = 14000; // in ms
        private static Dictionary<int, Educationtypeblock> EducationtypeblocksLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Educationtypeblock> EducationtypeblocksLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > EducationtypeblocksGetLastTime + EducationtypeblockS_GET_DELAY)
            {
                UpdateEducationtypeblocksLocal();
            }
            return EducationtypeblocksLocalObject;
        }
        public void UpdateEducationtypeblocksLocal()
        {
            EducationtypeblocksGetLastTime = stopWatch.ElapsedMilliseconds;
            EducationtypeblocksLocalObject = Educationtypeblocks.ToDictionary(Educationtypeblock => Educationtypeblock.Id);
        }

        private static long PositiontypesGetLastTime = -POSITIONTYPES_GET_DELAY - 1;
        private const long POSITIONTYPES_GET_DELAY = 14000; // in ms
        private static Dictionary<int, Positiontype> PositiontypesLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Positiontype> PositiontypesLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > PositiontypesGetLastTime + POSITIONTYPES_GET_DELAY)
            {
                UpdatePositiontypesLocal();
            }
            return PositiontypesLocalObject;
        }
        public void UpdatePositiontypesLocal()
        {
            PositiontypesGetLastTime = stopWatch.ElapsedMilliseconds;
            PositiontypesLocalObject = Positiontypes.ToDictionary(positiontype => positiontype.Id);
        }

        private static long SourceoffinancingsGetLastTime = -SOURCEOFFINANCINGS_GET_DELAY - 1;
        private const long SOURCEOFFINANCINGS_GET_DELAY = 90000; // in ms, 90 секунд, так как явление крайне редкое
        private static Dictionary<int, Sourceoffinancing> SourceoffinancingsLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Sourceoffinancing> SourceoffinancingsLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > SourceoffinancingsGetLastTime + SOURCEOFFINANCINGS_GET_DELAY)
            {
                UpdateSourceoffinancingsLocal();
            }
            return SourceoffinancingsLocalObject;
        }
        public void UpdateSourceoffinancingsLocal()
        {
            SourceoffinancingsGetLastTime = stopWatch.ElapsedMilliseconds;
            SourceoffinancingsLocalObject = Sourcesoffinancings.ToDictionary(sourceoffinancing => sourceoffinancing.Id);
        }

        
        private static long MailexplorerGetLastTime = -MAILEXPLORER_GET_DELAY - 1;
        private const long MAILEXPLORER_GET_DELAY = 90000; // in ms, 90 секунд, так как явление крайне редкое
        private static Dictionary<int, Mailexplorer> MailexplorersLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Mailexplorer> MailexplorersLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > MailexplorerGetLastTime + MAILEXPLORER_GET_DELAY)
            {
                UpdateMailexplorersLocal();
            }
            return MailexplorersLocalObject;
        }
        public void UpdateMailexplorersLocal()
        {
            MailexplorerGetLastTime = stopWatch.ElapsedMilliseconds;
            MailexplorersLocalObject = Mailexplorers.ToDictionary(mrd => mrd.Id);
        }


        private static long MailfolderGetLastTime = -MAILFOLDER_GET_DELAY - 1;
        private const long MAILFOLDER_GET_DELAY = 140000; // in ms, 90 секунд, так как явление крайне редкое
        private static Dictionary<int, Mailfolder> MailfoldersLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Mailfolder> MailfoldersLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > MailfolderGetLastTime + MAILFOLDER_GET_DELAY)
            {
                UpdateMailfoldersLocal();
            }
            return MailfoldersLocalObject;
        }
        public void UpdateMailfoldersLocal()
        {
            MailfolderGetLastTime = stopWatch.ElapsedMilliseconds;
            MailfoldersLocalObject = Mailfolders.ToDictionary(mrd => mrd.Idmailfolder);
        }


        private static long MrdsGetLastTime = -MRDS_GET_DELAY - 1;
        private const long MRDS_GET_DELAY = 90000; // in ms, 90 секунд, так как явление крайне редкое
        private static Dictionary<int, Mrd> MrdsLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Mrd> MrdsLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > MrdsGetLastTime + MRDS_GET_DELAY)
            {
                UpdateMrdsLocal();
            }
            return MrdsLocalObject;
        }
        public void UpdateMrdsLocal()
        {
            MrdsGetLastTime = stopWatch.ElapsedMilliseconds;
            MrdsLocalObject = Mrds.ToDictionary(mrd => mrd.Id);
        }


        private static long StructuretypesGetLastTime = -STRUCTURETYPES_GET_DELAY - 1;
        private const long STRUCTURETYPES_GET_DELAY = 90000; // in ms, 90 секунд, так как явление крайне редкое
        private static Dictionary<int, Structuretype> StructuretypesLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Structuretype> StructuretypesLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > StructuretypesGetLastTime + STRUCTURETYPES_GET_DELAY)
            {
                UpdateStructuretypesLocal();
            }
            return StructuretypesLocalObject;
        }
        public void UpdateStructuretypesLocal()
        {
            StructuretypesGetLastTime = stopWatch.ElapsedMilliseconds;
            StructuretypesLocalObject = Structuretypes.ToDictionary(structuretype => structuretype.Id);
        }

        private static long DecreeoperationsGetLastTime = -DECREEOPERATIONS_GET_DELAY - 30000;
        private const long DECREEOPERATIONS_GET_DELAY = 10000; // in ms
        private static Dictionary<int, Decreeoperation> DecreeoperationsLocalObject = null;
        private static Dictionary<int, List<Decreeoperation>> DecreeoperationsLocalSubjectAsKeyObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Decreeoperation> DecreeoperationsLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > DecreeoperationsGetLastTime + DECREEOPERATIONS_GET_DELAY)
            {
                UpdateDecreeoperationsLocal();
            }
            return DecreeoperationsLocalObject;
        }
        public Dictionary<int, List<Decreeoperation>> DecreeoperationsSubjectAsKeyLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > DecreeoperationsGetLastTime + DECREEOPERATIONS_GET_DELAY)
            {
                UpdateDecreeoperationsLocal();
            }
            return DecreeoperationsLocalSubjectAsKeyObject;
        }
        public void UpdateDecreeoperationsLocal()
        {
            DecreeoperationsGetLastTime = stopWatch.ElapsedMilliseconds;
            DecreeoperationsLocalObject = Decreeoperations.ToDictionary(decreeoperation => decreeoperation.Id);
            Dictionary<int, List<Decreeoperation>> DecreeoperationsLocalSubjectAsKeyObjectNew = new Dictionary<int, List<Decreeoperation>>();
            foreach (Decreeoperation decreeoperation in Decreeoperations.ToList())
            {
                if (!DecreeoperationsLocalSubjectAsKeyObjectNew.ContainsKey(decreeoperation.Subject))
                {
                    DecreeoperationsLocalSubjectAsKeyObjectNew.Add(decreeoperation.Subject, new List<Decreeoperation>());
                }
                DecreeoperationsLocalSubjectAsKeyObjectNew[decreeoperation.Subject].Add(decreeoperation);
            }
            DecreeoperationsLocalSubjectAsKeyObject = DecreeoperationsLocalSubjectAsKeyObjectNew;
        }


        //
        private static long PositioncategoriesGetLastTime = -POSITIONCATEGORIES_GET_DELAY - 1;
        private const long POSITIONCATEGORIES_GET_DELAY = 90000; // in ms, 90 секунд, так как явление крайне редкое
        private static Dictionary<int, Positioncategory> PositioncategoriesLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Positioncategory> PositioncategoriesLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > PositioncategoriesGetLastTime + POSITIONCATEGORIES_GET_DELAY)
            {
                UpdatePositioncategoriesLocal();
            }
            return PositioncategoriesLocalObject;
        }
        public void UpdatePositioncategoriesLocal()
        {
            PositioncategoriesGetLastTime = stopWatch.ElapsedMilliseconds;
            PositioncategoriesLocalObject = Positioncategories.ToDictionary(positioncategory => positioncategory.Id);
        }



        private static long PositioncategoryRankGetLastTime = -POSITIONCATEGORYRANK_GET_DELAY - 1;
        private const long POSITIONCATEGORYRANK_GET_DELAY = 90000; // in ms, 90 секунд, так как явление крайне редкое
        private static Dictionary<int, Positioncategoryrank> PositioncategoryRanksLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Positioncategoryrank> PositioncategoryRanksLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > PositioncategoryRankGetLastTime + POSITIONCATEGORYRANK_GET_DELAY)
            {
                UpdatePositioncategoryRankLocal();
            }
            return PositioncategoryRanksLocalObject;
        }
        public void UpdatePositioncategoryRankLocal()
        {
            PositioncategoryRankGetLastTime = stopWatch.ElapsedMilliseconds;
            PositioncategoryRanksLocalObject = Positioncategoryranks.ToDictionary(positioncategoryrank => positioncategoryrank.Id);
        }



        /**
         * POSITIONS
         */
        private static long PositionsGetLastTime = -POSITIONS_GET_DELAY - 10000;
        private const long POSITIONS_GET_DELAY = 14000; // in ms
        private static Dictionary<int, Position> PositionsLocalObject = new Dictionary<int, Position>();
        private static Dictionary<int, List<Position>> PositionsLocalStructureAsKeyObject = new Dictionary<int, List<Position>>();
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Dictionary<int, Position> PositionsLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > PositionsGetLastTime + POSITIONS_GET_DELAY)
            {
                UpdatePositionsLocal();
            }
            return PositionsLocalObject;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Dictionary<int, List<Position>> PositionsStructureAsKeyLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > PositionsGetLastTime + POSITIONS_GET_DELAY)
            {
                UpdatePositionsLocal();
            }
            return PositionsLocalStructureAsKeyObject;
        }

        public List<Position> PositionsList()
        {
            return Positions.ToList();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdatePositionsLocal()
        {
            Dictionary<int, List<Position>> PositionsLocalStructureAsKeyObjectNew = new Dictionary<int, List<Position>>();
            foreach (Position position in PositionsList())
            {
                if (!PositionsLocalStructureAsKeyObjectNew.ContainsKey(position.Structure))
                {
                    PositionsLocalStructureAsKeyObjectNew.Add(position.Structure, new List<Position>());
                }
                PositionsLocalStructureAsKeyObjectNew[position.Structure].Add(position);
            }
            PositionsLocalStructureAsKeyObject = PositionsLocalStructureAsKeyObjectNew;
            PositionsLocalObject = Positions.ToDictionary(position => position.Id);
            PositionsGetLastTime = stopWatch.ElapsedMilliseconds;
        }

        /**
         * PERSONS
         */
        private static long PersonsGetLastTime = -PERSONS_GET_DELAY - 1;
        private const long PERSONS_GET_DELAY = 14000; // in ms
        private static Dictionary<int, Person> PersonsLocalObject = null;
        private static Dictionary<int, List<Person>> PersonsLocalPositionAsKeyObject = null;
        private static Dictionary<int, List<Person>> PersonsLocalStructureAsKeyObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Person> PersonsLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > PersonsGetLastTime + PERSONS_GET_DELAY)
            {
                UpdatePersonsLocal();
            }
            return PersonsLocalObject;
        }
        public Dictionary<int, List<Person>> PersonsPositionAsKeyLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > PersonsGetLastTime + PERSONS_GET_DELAY)
            {
                UpdatePersonsLocal();
            }
            return PersonsLocalPositionAsKeyObject;
        }
        public Dictionary<int, List<Person>> PersonsStructureAsKeyLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > PersonsGetLastTime + PERSONS_GET_DELAY)
            {
                UpdatePersonsLocal();
            }
            return PersonsLocalStructureAsKeyObject;
        }
        public void UpdatePersonsLocal()
        {
            PersonsGetLastTime = stopWatch.ElapsedMilliseconds;
            List<Person> personsList = Persons.ToList();
            Dictionary<int, Person> PersonsLocalObjectNew = new Dictionary<int, Person>();
            Dictionary<int, List<Person>> PersonsLocalPositionAsKeyObjectNew = new Dictionary<int, List<Person>>();
            Dictionary<int, List<Person>> PersonsLocalStructureAsKeyObjectNew = new Dictionary<int, List<Person>>();
            foreach (Person person in personsList)
            {
                PersonsLocalObjectNew.Add(person.Id, person);
                if (person.Position > 0)
                {
                    if (!PersonsLocalPositionAsKeyObjectNew.ContainsKey(person.Position))
                    {
                        PersonsLocalPositionAsKeyObjectNew.Add(person.Position, new List<Person>());
                    }
                    PersonsLocalPositionAsKeyObjectNew[person.Position].Add(person);
                }
                if (person.Position < 0)
                {

                    if (!PersonsLocalStructureAsKeyObjectNew.ContainsKey(person.Position))
                    {
                        PersonsLocalStructureAsKeyObjectNew.Add(person.Position, new List<Person>());
                    }
                    PersonsLocalStructureAsKeyObjectNew[person.Position].Add(person);
                }
            }
            PersonsLocalObject = PersonsLocalObjectNew;
            PersonsLocalPositionAsKeyObject = PersonsLocalPositionAsKeyObjectNew;
            PersonsLocalStructureAsKeyObject = PersonsLocalStructureAsKeyObjectNew;
        }



        private static long PersonranksGetLastTime = -PERSONRANKS_GET_DELAY - 1;
        private const long PERSONRANKS_GET_DELAY = 14000; // in ms
        private static Dictionary<int, Personrank> PersonranksLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Personrank> PersonranksLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > PersonranksGetLastTime + PERSONRANKS_GET_DELAY)
            {
                UpdatePersonranksLocal();
            }
            return PersonranksLocalObject;
        }
        public void UpdatePersonranksLocal()
        {
            PersonranksGetLastTime = stopWatch.ElapsedMilliseconds;
            PersonranksLocalObject = Personranks.ToDictionary(personrank => personrank.Id);
        }




        private static long PersoncontractsGetLastTime = -PERSONCONTRACTS_GET_DELAY - 1;
        private const long PERSONCONTRACTS_GET_DELAY = 14000; // in ms
        private static Dictionary<int, Personcontract> PersoncontractsLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Personcontract> PersoncontractsLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > PersoncontractsGetLastTime + PERSONCONTRACTS_GET_DELAY)
            {
                UpdatePersoncontractsLocal();
            }
            return PersoncontractsLocalObject;
        }
        public void UpdatePersoncontractsLocal()
        {
            PersoncontractsGetLastTime = stopWatch.ElapsedMilliseconds;
            PersoncontractsLocalObject = Personcontracts.ToDictionary(personcontract => personcontract.Id);
        }


        private static long PersonrelativesGetLastTime = -PERSONRELATIVES_GET_DELAY - 1;
        private const long PERSONRELATIVES_GET_DELAY = 14000; // in ms
        private static Dictionary<int, Personrelative> PersonrelativesLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Personrelative> PersonrelativesLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > PersonrelativesGetLastTime + PERSONRELATIVES_GET_DELAY)
            {
                UpdatePersonrelativesLocal();
            }
            return PersonrelativesLocalObject;
        }
        public void UpdatePersonrelativesLocal()
        {
            PersonrelativesGetLastTime = stopWatch.ElapsedMilliseconds;
            PersonrelativesLocalObject = Personrelatives.ToDictionary(personrelative => personrelative.Id);
        }




        private static long PersonvacationsGetLastTime = -PERSONVACATIONS_GET_DELAY - 1;
        private const long PERSONVACATIONS_GET_DELAY = 14000; // in ms
        private static Dictionary<int, Personvacation> PersonvacationsLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Personvacation> PersonvacationsLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > PersonvacationsGetLastTime + PERSONVACATIONS_GET_DELAY)
            {
                UpdatePersonvacationsLocal();
            }
            return PersonvacationsLocalObject;
        }
        public void UpdatePersonvacationsLocal()
        {
            PersonvacationsGetLastTime = stopWatch.ElapsedMilliseconds;
            PersonvacationsLocalObject = Personvacations.ToDictionary(personvacation => personvacation.Id);
        }

        private static long PersonjobsGetLastTime = -PERSONJOBS_GET_DELAY - 1;
        private const long PERSONJOBS_GET_DELAY = 14000; // in ms
        private static Dictionary<int, Personjob> PersonjobsLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Personjob> PersonjobsLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > PersonjobsGetLastTime + PERSONJOBS_GET_DELAY)
            {
                UpdatePersonjobsLocal();
            }
            return PersonjobsLocalObject;
        }
        public void UpdatePersonjobsLocal()
        {
            PersonjobsGetLastTime = stopWatch.ElapsedMilliseconds;
            PersonjobsLocalObject = Personjobs.ToDictionary(personjob => personjob.Id);
        }

        private static long PersoneducationsGetLastTime = -PERSONEDUCATIONS_GET_DELAY - 1;
        private const long PERSONEDUCATIONS_GET_DELAY = 14000; // in ms
        private static Dictionary<int, Personeducation> PersoneducationsLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Personeducation> PersoneducationsLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > PersonrewardsGetLastTime + PERSONREWARDS_GET_DELAY)
            {
                UpdatePersoneducationsLocal();
            }
            return PersoneducationsLocalObject;
        }
        public void UpdatePersoneducationsLocal()
        {
            PersonrewardsGetLastTime = stopWatch.ElapsedMilliseconds;
            PersoneducationsLocalObject = Personeducations.ToDictionary(personeducation => personeducation.Id);
        }

        private static long PersonrewardsGetLastTime = -PERSONREWARDS_GET_DELAY - 1;
        private const long PERSONREWARDS_GET_DELAY = 14000; // in ms
        private static Dictionary<int, Personreward> PersonrewardsLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Personreward> PersonrewardsLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > PersonrewardsGetLastTime + PERSONREWARDS_GET_DELAY)
            {
                UpdatePersonrewardsLocal();
            }
            return PersonrewardsLocalObject;
        }
        public void UpdatePersonrewardsLocal()
        {
            PersonrewardsGetLastTime = stopWatch.ElapsedMilliseconds;
            PersonrewardsLocalObject = Personrewards.ToDictionary(personreward => personreward.Id);
        }



        private static long PersondecreesGetLastTime = -PERSONDECREES_GET_DELAY - 1;
        private const long PERSONDECREES_GET_DELAY = 14000; // in ms
        private static Dictionary<int, Persondecree> PersondecreesLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Persondecree> PersondecreesLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > PersondecreesGetLastTime + PERSONDECREES_GET_DELAY)
            {
                UpdatePersondecreesLocal();
            }
            return PersondecreesLocalObject;
        }
        public void UpdatePersondecreesLocal()
        {
            PersondecreesGetLastTime = stopWatch.ElapsedMilliseconds;
            PersondecreesLocalObject = Persondecrees.ToDictionary(persondecree => persondecree.Id);
        }

        private static long PersondecreeoperationsGetLastTime = -PERSONDECREEOPERATIONS_GET_DELAY - 1;
        private const long PERSONDECREEOPERATIONS_GET_DELAY = 14000; // in ms
        private static Dictionary<int, Persondecreeoperation> PersondecreeoperationsLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Persondecreeoperation> PersondecreeoperationsLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > PersondecreeoperationsGetLastTime + PERSONDECREEOPERATIONS_GET_DELAY)
            {
                UpdatePersondecreeoperationsLocal();
            }
            return PersondecreeoperationsLocalObject;
        }
        public void UpdatePersondecreeoperationsLocal()
        {
            PersondecreeoperationsGetLastTime = stopWatch.ElapsedMilliseconds;
            PersondecreeoperationsLocalObject = Persondecreeoperations.ToDictionary(persondecreeoperation => persondecreeoperation.Id);
        }


        private static long SessionsGetLastTime = -SESSIONS_GET_DELAY - 1;
        private const long SESSIONS_GET_DELAY = 28000; // in ms
        private static Dictionary<string, Session> SessionsLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Session> SessionsLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > SessionsGetLastTime + SESSIONS_GET_DELAY)
            {
                UpdateSessionsLocal();
            }
            return SessionsLocalObject;
        }
        public void UpdateSessionsLocal()
        {
            SessionsGetLastTime = stopWatch.ElapsedMilliseconds;
            SessionsLocalObject = Sessions.ToDictionary(session => session.Id);
        }



        private static long UsersGetLastTime = -USERS_GET_DELAY - 1;
        private const long USERS_GET_DELAY = 14000; // in ms
        private static Dictionary<int, User> UsersLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, User> UsersLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > UsersGetLastTime + USERS_GET_DELAY)
            {
                UpdateUsersLocal();
            }
            return UsersLocalObject;
        }
        public void UpdateUsersLocal()
        {
            UsersGetLastTime = stopWatch.ElapsedMilliseconds;
            UsersLocalObject = Users.ToDictionary(user => user.Id);
        }

        private static long RightsGetLastTime = -RIGHTS_GET_DELAY - 1; 
        private const long RIGHTS_GET_DELAY = 14000; // in ms
        private static Dictionary<int, Rights> RightsLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Rights> RightsLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > RightsGetLastTime + RIGHTS_GET_DELAY)
            {
                UpdateRightsLocal();
            }
            return RightsLocalObject;
        }
        public void UpdateRightsLocal()
        {
            RightsGetLastTime = stopWatch.ElapsedMilliseconds;
            RightsLocalObject = Rights.ToDictionary(rights => rights.Id);
        }

        private static long RolesGetLastTime = -ROLES_GET_DELAY - 1;
        private const long ROLES_GET_DELAY = 14000; // in ms
        private static Dictionary<int, Role> RolesLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Role> RolesLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > RolesGetLastTime + ROLES_GET_DELAY)
            {
                UpdateRolesLocal();
            }
            return RolesLocalObject;
        }
        public void UpdateRolesLocal()
        {
            RolesGetLastTime = stopWatch.ElapsedMilliseconds;
            RolesLocalObject = Roles.ToDictionary(role => role.Id);
        }


        private static long SubjectsGetLastTime = -SUBJECTS_GET_DELAY - 1;
        private const long SUBJECTS_GET_DELAY = 21000; // in ms
        private static Dictionary<int, Subject> SubjectsLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Subject> SubjectsLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > SubjectsGetLastTime + SUBJECTS_GET_DELAY)
            {
                UpdateSubjectsLocal();
            }
            return SubjectsLocalObject;
        }
        public void UpdateSubjectsLocal()
        {
            SubjectsGetLastTime = stopWatch.ElapsedMilliseconds;
            SubjectsLocalObject = Subjects.ToDictionary(subject => subject.Id);
        }



        private static long CertificatesGetLastTime = -CERTIFICATES_GET_DELAY - 1;
        private const long CERTIFICATES_GET_DELAY = 21000; // in ms
        private static Dictionary<int, Certificate> CertificatesLocalObject = null;
        /// <summary>
        /// Локальная версия таблицы из базы данных, которую мы или периодически обновляем или обновляем после внесения изменений. Необходима для улучшения быстродействия (уменьшения запросов в базу данных)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Certificate> CertificatesLocal() // 
        {
            if (stopWatch.ElapsedMilliseconds > CertificatesGetLastTime + CERTIFICATES_GET_DELAY)
            {
                UpdateCertificatesLocal();
            }
            return CertificatesLocalObject;
        }
        public void UpdateCertificatesLocal()
        {
            CertificatesGetLastTime = stopWatch.ElapsedMilliseconds;
            CertificatesLocalObject = Certificates.ToDictionary(certificate => certificate.Id);
        }

        /// <summary>
        /// Добавляет нового пользователя
        /// </summary>
        /// <param name="user"></param>
        public void SaveUser(User user)
        {
            context.User.Add(user);
            context.SaveChanges();
            UpdateUsersLocal();
            Rights rights = user.Rights;
            if (rights != null)
            {
                rights.User = user.Id;
                context.Rights.Add(rights);
                context.SaveChanges();
                UpdateRightsLocal();

                // Раньше права доступа хранились в user напрямую, а теперь в rights, привязанному к этому пользователю
                // Этот код неоходим для методов, которые используют старые привязки к полям в user.
                user.Admin = user.Rights.Admin;
                user.Masterpersonneleditor = user.Rights.Peopleorgreadall;
                user.Structureeditor = user.Rights.Orgedit;
                user.Structureread = user.Rights.Orgread;
                user.Personneleditor = user.Rights.Peopleedit;
                user.Personnelread = user.Rights.Peopleread;
                context.SaveChanges();
                UpdateUsersLocal();
            }
        }

        /// <summary>
        /// Удаляет пользователя
        /// </summary>
        /// <param name="user"></param>
        public void DeleteUser(User user)
        {
            User contextUser = Users.First(u => u.Id == user.Id);
            int userId = contextUser.Id;
            context.User.Remove(contextUser);
            context.SaveChanges();
            UpdateUsersLocal();

            Rights contextRights = Rights.FirstOrDefault(r => r.User == userId);
            if (contextRights != null)
            {
                context.Rights.Remove(contextRights);
                context.SaveChanges();

            }
        }

        /// <summary>
        /// Обнуляет пароль пользователя. Новый пароль сохраняется после первого ввода
        /// </summary>
        /// <param name="user"></param>
        public void NullifyPassUser(User user)
        {
            User contextUser = Users.First(u => u.Id == user.Id);
            contextUser.Salt = null;
            contextUser.Password = null;
            context.SaveChanges();
            UpdateUsersLocal();
        }

        public void AddRights(User user, Rights rights)
        {
            context.Rights.Add(rights);
            SaveChanges();
            UpdateRightsLocal();
        }

        public void ChangeRights(User user, Rights rights)
        {
            Rights rightsContext = context.Rights.FirstOrDefault(p => p.Id == rights.Id);
            if (rights == null)
            {
                return;
            }
            
            //rightsContext.User = rights.User;
            //rightsContext.Position = rights.Position;
            //rightsContext.Role = rights.Role;
            //rightsContext.Admin = rights.Admin;
            //rightsContext.Orgedit = rights.Orgedit;
            //rightsContext.Orgread = rights.Orgread;
            //rightsContext.Orgreadall = rights.Orgreadall;
            //rightsContext.Peopleedit = rights.Peopleedit;
            //rightsContext.Peopleread = rights.Peopleread;
            //rightsContext.Peoplereadall = rights.Peoplereadall;
            //rightsContext.Candidateedit = rights.Candidateedit;
            //rightsContext.Candidateread = rights.Candidateread;
            //rightsContext.Peopleorgread = rights.Peopleorgread;
            //rightsContext.Peopleorgreadall = rights.Peopleorgreadall;
            //rightsContext.Peopledecreeread = rights.Peopledecreeread;
            //rightsContext.Peopledecreeedit = rights.Peopledecreeedit;
            rights.CopyFields(rightsContext);

            SaveChanges();
            UpdateRightsLocal();
        }

        /// <summary>
        /// Возможно, стоит проводить операцию на фронтэнде
        /// </summary>
        /// <param name="positiontypeid"></param>
        /// <param name="userid"></param>
        public void TransferPositiontypeRightsToUser(int positiontypeid, int userid)
        {
            Rights rights = RightsLocal().Values.FirstOrDefault(r => r.User == userid);
            // Права до этого не существовали
            if (rights == null)
            {

            // Права существовали до этого
            } else
            {

            }
        }

        public Rights GenerateBaseUserRights(User user)
        {
            Rights rights = RightsLocal().Values.FirstOrDefault(r => r.User == user.Id && user.Id > 0);
            // Права уже существовали для пользователя, так что мы не создаем новые. 
            if (rights != null)
            {
                return rights;
            }
            rights = new Rights();
            rights.User = user.Id;
            if (user.Positiontype > 0)
            {
                Rights positiontypeRights = RightsLocal().Values.FirstOrDefault(r => r.Position == user.Positiontype);
                if (positiontypeRights != null)
                {
                    rights.Role = positiontypeRights.Role;
                }
            }
            rights.Admin = user.Admin.GetValueOrDefault();
            rights.Orgedit = user.Structureeditor.GetValueOrDefault();
            rights.Orgread = user.Structureread;
            rights.Orgreadall = user.Structureeditor.GetValueOrDefault();
            rights.Peopleread = user.Personnelread;
            rights.Peopleedit = user.Personneleditor.GetValueOrDefault();
            rights.Peoplereadall = user.Personneleditor.GetValueOrDefault();
            rights.Candidateedit = user.Personneleditor.GetValueOrDefault();
            rights.Candidateread = user.Personnelread;
            rights.Peopleorgread = user.Personnelread;
            rights.Peopleorgreadall = user.Masterpersonneleditor.GetValueOrDefault();
            rights.Peopledecreeread = user.Personnelread;
            rights.Peopledecreeedit = user.Personneleditor.GetValueOrDefault();

            context.Rights.Add(rights);
            SaveChanges();
            UpdateRightsLocal();

            return rights;
        }

        public void DeleteRights(User user, Rights rights)
        {
            int minusid = -rights.Id;
            Rights rightsContext = context.Rights.FirstOrDefault(p => p.Id == minusid);
            if (rightsContext == null)
            {
                return;
            }
            context.Rights.Remove(rightsContext);

            SaveChanges();
            UpdateRightsLocal();
        }

        

        /// <summary>
        /// Запомнить текущее положение пользователя в оргштатной структуре. Какие подразделения открыты.
        /// </summary>
        /// <param name="getString"></param>
        /// <param name="user"></param>
        public void RememberUserStructureTree(string getString, User user)
        {
            context.User.First(u => u.Id == user.Id).Currentstructuretree = getString;
            context.SaveChanges();
            UpdateUsersLocal();
        }

        /// <summary>
        /// Возвращает орг-штатный проект приказа, над которым работает пользователь
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Decree GetDecreeByUser(User user)
        {
            Decree decree = null;
            if (DecreesLocal().ContainsKey(user.Decree.GetValueOrDefault()))
            {
                decree = DecreesLocal()[user.Decree.GetValueOrDefault()];
            }
            return decree;
        }

        /// <summary>
        /// Возвращает подразделение, в которое непосредственно входит данная должность
        /// </summary>
        /// <param name="positionid"></param>
        /// <returns></returns>
        public Structure GetStructureByPositionID(int positionid)
        {

            //Position position = Positions.First(p => p.Id == positionid);
            Position position = PositionsLocal()[positionid];
            return Structures.First(s => s.Id == position.Structure);
        }

        /// <summary>
        /// Обновляет настройки пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userSettings"></param>
        public void UpdateUserSettings(User user, UserSettings userSettings)
        {
            User contextUser = Users.First(u => u.Id == user.Id);
            if (userSettings.UpdatePositioncompact > 0)
            {
                contextUser.Positioncompact = Convert.ToSByte(userSettings.PositioncompactValue);
            }

            if (userSettings.UpdateDate > 0)
            {
                contextUser.Date = userSettings.DateValue;
            }
            if (userSettings.UpdateSidebarDisplay > 0)
            {
                contextUser.Sidebardisplay = Convert.ToSByte(userSettings.SidebarDisplayValue);
            }

           
            context.SaveChanges();
            UpdateUsersLocal();
        }

        /// <summary>
        /// Устанавливает орг-штатный проект приказа, над которым работает пользователь
        /// </summary>
        /// <param name="user"></param>
        /// <param name="id">ID приказа</param>
        public void DecreesSelectActive(User user, int id)
        {
            User contextUser = Users.First(u => u.Id == user.Id);
            contextUser.Decree = id;
            context.SaveChanges();
            UpdateUsersLocal();
        }

        /// <summary>
        /// Возвращает список актуальных проектов орг-штатных приказов (не удалены, но и не подписаны).
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<Decree> GetDecreesActive(User user)
        {
            //return Decrees.Where(d => d.User.GetValueOrDefault() == user.Id).Where(d => d.Declined == 0).Where(d => d.Signed == 0);
            return DecreesLocal().Values.Where(d => d.Declined == 0).Where(d => d.Signed == 0);
        }

        /// <summary>
        /// Добавляет новое подразделение
        /// </summary>
        /// <param name="structureManagement"></param>
        /// <param name="user"></param>
        public void AddStructure(StructureManagement structureManagement, User user)
        {
            DateTime date = user.Date.GetValueOrDefault();

            Structure structure = new Structure();
            structure.Name = structureManagement.Name;
            if (structureManagement.Name1 != null && structureManagement.Name1.Length > 0)
            {
                structure.Name1 = structureManagement.Name1;
            } else
            {
                structure.Name1 = structureManagement.Name;
            }
            if (structureManagement.Name2 != null && structureManagement.Name2.Length > 0)
            {
                structure.Name2 = structureManagement.Name2;
            }
            else
            {
                structure.Name2 = structureManagement.Name;
            }
            if (structureManagement.Name3 != null && structureManagement.Name3.Length > 0)
            {
                structure.Name3 = structureManagement.Name3;
            }
            else
            {
                structure.Name3 = structureManagement.Name;
            }
            structure.Nameshortened = structureManagement.NameShortened;
            structure.Featured = structureManagement.Featured;
            structure.Structureregion = structureManagement.Structureregion;
            structure.Structuretype = structureManagement.Structuretype;
            structure.Street = structureManagement.Street;
            structure.City = structureManagement.City;
            structure.Rank = structureManagement.Rank;
            structure.Separatestructure = structureManagement.Separatestructure;
            if (structureManagement.Parent > 0)
            {
                structure.Parentstructure = structureManagement.Parent;
                int lowPriority = 0;
                if (StructuresLocal().Values.Where( s => structure.Parentstructure == s.Parentstructure).Count() > 0)
                {
                    lowPriority = StructuresLocal().Values.Where(s => structure.Parentstructure == s.Parentstructure).OrderBy(st => st.Priority).Last().Priority + 1;
                }
                structure.Priority = lowPriority;

                Structure parentActualStructure = GetActualStructureInfo(structure.Parentstructure, date);
                if (structure.Structuretype == 0 && parentActualStructure.Structuretype > 0)
                {
                    structure.Structuretype = parentActualStructure.Structuretype;
                }
            }

            /**
             * Для прохождения службы
             */

            structure.Subject1 = structureManagement.Subject1;
            structure.Subject2 = structureManagement.Subject2;
            structure.Subject3 = structureManagement.Subject3;
            structure.Subject4 = structureManagement.Subject4;
            structure.Subject5 = structureManagement.Subject5;
            structure.Subject6 = structureManagement.Subject6;
            structure.Subject7 = structureManagement.Subject7;
            structure.Subject8 = structureManagement.Subject8;
            structure.Subject9 = structureManagement.Subject9;
            structure.Subject10 = structureManagement.Subject10;
            structure.Subject11 = structureManagement.Subject11;
            structure.Subject12 = structureManagement.Subject12;
            structure.Subject13 = structureManagement.Subject13;
            structure.Subject14 = structureManagement.Subject14;
            structure.Subject15 = structureManagement.Subject15;
            structure.Subjectgender = structureManagement.Subjectgender;
            structure.Subjectnumber = structureManagement.Subjectnumber;
            structure.Subjectnotice = structureManagement.Subjectnotice;

            context.Structure.Add(structure);
            context.SaveChanges();
            UpdateStructuresLocal();

            Decree decree = GetDecreeByUser(user);
            Decreeoperation operation = new Decreeoperation();
            operation.Decree = decree.Id;
            operation.Created = 1;
            // У подразделений subject имеет знак минуса
            operation.Subject = -structure.Id;
            if (structureManagement.Datecustom > 0)
            {
                operation.Dateactive = structureManagement.Dateactive;
                operation.Datecustom = 1;
            }

            context.Decreeoperation.Add(operation);
            context.SaveChanges();
            UpdateDecreeoperationsLocal();

        }

        /// <summary>
        /// Клонирует подразделение. Используется неглубокое клонирование - без клонирования входящих должностей и подразделений.
        /// </summary>
        /// <param name="structureToClone"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public Structure CloneStructure(Structure structureToClone, User user)
        {
            Structure structure = new Structure();
            structure.Name = structureToClone.Name;
            structure.Name1 = structureToClone.Name1;
            structure.Name2 = structureToClone.Name2;
            structure.Name3 = structureToClone.Name3;
            structure.Nameshortened = structureToClone.Nameshortened;
            structure.Featured = structureToClone.Featured;
            structure.Structureregion = structureToClone.Structureregion;
            structure.Structuretype = structureToClone.Structuretype;
            structure.Street = structureToClone.Street;
            structure.City = structureToClone.City;
            structure.Rank = structureToClone.Rank;
            structure.Separatestructure = structureToClone.Separatestructure;
            structure.Id = 0;

            structure.Parentstructure = 0;

            /**
             * Для прохождения службы
             */
            structure.Subject1 = structureToClone.Subject1;
            structure.Subject2 = structureToClone.Subject2;
            structure.Subject3 = structureToClone.Subject3;
            structure.Subject4 = structureToClone.Subject4;
            structure.Subject5 = structureToClone.Subject5;
            structure.Subject6 = structureToClone.Subject6;
            structure.Subject7 = structureToClone.Subject7;
            structure.Subject8 = structureToClone.Subject8;
            structure.Subject9 = structureToClone.Subject9;
            structure.Subject10 = structureToClone.Subject10;
            structure.Subject11 = structureToClone.Subject11;
            structure.Subject12 = structureToClone.Subject12;
            structure.Subject13 = structureToClone.Subject13;
            structure.Subject14 = structureToClone.Subject14;
            structure.Subject15 = structureToClone.Subject15;
            structure.Subjectgender = structureToClone.Subjectgender;
            structure.Subjectnumber = structureToClone.Subjectnumber;
            structure.Subjectnotice = structureToClone.Subjectnotice;

            context.Structure.Add(structure);
            context.SaveChanges();
            UpdateStructuresLocal();

            Decree decree = GetDecreeByUser(user);
            Decreeoperation operation = new Decreeoperation();
            operation.Decree = decree.Id;
            operation.Created = 1;
            operation.Subject = -structure.Id; // У подразделений subject имеет знак минуса

            context.Decreeoperation.Add(operation);
            context.SaveChanges();
            UpdateDecreeoperationsLocal();
            return structure;
        }

        /// <summary>
        /// Редактирует подразделение. Не только переименование, но допускается редактирование разрядности и прочего.
        /// По факту создается новое подразделение с обновленной информацией, которое сохраняет связь со своим предшественником. Это необходимо для работы
        /// "машины времени"/таймлайна.
        /// </summary>
        /// <param name="structureManagement"></param>
        /// <param name="user"></param>
        public void RenameStructure(StructureManagement structureManagement, User user)
        {
            //IEnumerable<Structure> structures = Structures.Where(s => s.Id == structureManagement.Id || s.Changeorigin == structureManagement.Id);
            Structure structure = GetActualStructureInfo(structureManagement.Id.GetValueOrDefault(), user.Date.GetValueOrDefault());
            Structure origin = null;
            DateTime date = user.Date.GetValueOrDefault();
            if (structure.Changeorigin == 0)
            {
                origin = structure;
            }
            else
            {
                origin = Structures.First(s => s.Id == structure.Changeorigin);
            }

            if (structureManagement.Nodecree > 0)
            {
                structure = Structures.First(s => s.Id == structure.Id);
                structure.Structuretype = structureManagement.Structuretype;

                structure.Name = structureManagement.Name;
                structure.Name1 = structureManagement.Name1;
                structure.Name2 = structureManagement.Name2;
                structure.Name3 = structureManagement.Name3;
                structure.Nameshortened = structureManagement.NameShortened;
                structure.Featured = structureManagement.Featured;
                structure.Structureregion = structureManagement.Structureregion;
                structure.Street = structureManagement.Street;
                structure.City = structureManagement.City;
                structure.Rank = structureManagement.Rank;
                structure.Parentstructure = structureManagement.Parent;
                structure.Separatestructure = structureManagement.Separatestructure;

                int structuretype = structureManagement.Structuretype;
                if (structureManagement.Structuretypesiblings > 0)
                {
                    List<int> siblings = GetStructuresSiblings(origin.Id, null, date);
                    foreach (int sibling in siblings)
                    {
                        Structure actualStructure = GetActualStructureInfo(sibling, date, null);
                        Structure actualContextStructure = null;
                        if (actualStructure != null)
                        {
                            actualContextStructure = context.Structure.FirstOrDefault(s => s.Id == actualStructure.Id);
                        }
                        Structure contextStructure = context.Structure.FirstOrDefault(s => s.Id == sibling);
                        if (contextStructure != null)
                        {
                            contextStructure.Structuretype = structuretype;
                            if (actualContextStructure != null)
                            {
                                actualContextStructure.Structuretype = structuretype;
                            }
                        }
                    }
                }

                int minusstructureid = -structure.Id;
                Decreeoperation contextOperation = Decreeoperations.FirstOrDefault(d => d.Subject == minusstructureid);
                if (contextOperation != null)
                {
                    if (structureManagement.Datecustom > 0)
                    {
                        contextOperation.Dateactive = structureManagement.Dateactive;
                        contextOperation.Datecustom = 1;
                    }
                    else
                    {
                        contextOperation.Datecustom = 0;
                    }
                }

                /**
                 * Для прохождения службы
                 */
                bool changeSubject = false;
                if (structure.Subject1 != structureManagement.Subject1 || structure.Subject2 != structureManagement.Subject2 || structure.Subject3 != structureManagement.Subject3 ||
                    structure.Subject4 != structureManagement.Subject4 || structure.Subject5 != structureManagement.Subject5 || structure.Subject6 != structureManagement.Subject6 ||
                    structure.Subject7 != structureManagement.Subject7 || structure.Subject8 != structureManagement.Subject8 || structure.Subject9 != structureManagement.Subject9 ||
                    structure.Subject10 != structureManagement.Subject10 || structure.Subject11 != structureManagement.Subject11 || structure.Subject12 != structureManagement.Subject12 ||
                    structure.Subject13 != structureManagement.Subject13 || structure.Subject14 != structureManagement.Subject14 || structure.Subject15 != structureManagement.Subject15)
                {
                    changeSubject = true;
                }

                structure.Subject1 = structureManagement.Subject1;
                structure.Subject2 = structureManagement.Subject2;
                structure.Subject3 = structureManagement.Subject3;
                structure.Subject4 = structureManagement.Subject4;
                structure.Subject5 = structureManagement.Subject5;
                structure.Subject6 = structureManagement.Subject6;
                structure.Subject7 = structureManagement.Subject7;
                structure.Subject8 = structureManagement.Subject8;
                structure.Subject9 = structureManagement.Subject9;
                structure.Subject10 = structureManagement.Subject10;
                structure.Subject11 = structureManagement.Subject11;
                structure.Subject12 = structureManagement.Subject12;
                structure.Subject13 = structureManagement.Subject13;
                structure.Subject14 = structureManagement.Subject14;
                structure.Subject15 = structureManagement.Subject15;
                structure.Subjectgender = structureManagement.Subjectgender;
                structure.Subjectnumber = structureManagement.Subjectnumber;
                structure.Subjectnotice = structureManagement.Subjectnotice;

                if (changeSubject)
                {
                    IQueryable<Structure> structuresChangeSubject = Structures.Where(s => s.Name.ToLower().Equals(structure.Name.ToLower()));
                    foreach (Structure structureChangeSubject in structuresChangeSubject)
                    {
                        structureChangeSubject.Subject1 = structureManagement.Subject1;
                        structureChangeSubject.Subject2 = structureManagement.Subject2;
                        structureChangeSubject.Subject3 = structureManagement.Subject3;
                        structureChangeSubject.Subject4 = structureManagement.Subject4;
                        structureChangeSubject.Subject5 = structureManagement.Subject5;
                        structureChangeSubject.Subject6 = structureManagement.Subject6;
                        structureChangeSubject.Subject7 = structureManagement.Subject7;
                        structureChangeSubject.Subject8 = structureManagement.Subject8;
                        structureChangeSubject.Subject9 = structureManagement.Subject9;
                        structureChangeSubject.Subject10 = structureManagement.Subject10;
                        structureChangeSubject.Subject11 = structureManagement.Subject11;
                        structureChangeSubject.Subject12 = structureManagement.Subject12;
                        structureChangeSubject.Subject13 = structureManagement.Subject13;
                        structureChangeSubject.Subject14 = structureManagement.Subject14;
                        structureChangeSubject.Subject15 = structureManagement.Subject15;
                        structureChangeSubject.Subjectgender = structureManagement.Subjectgender;
                        structureChangeSubject.Subjectnumber = structureManagement.Subjectnumber;
                        structureChangeSubject.Subjectnotice = structureManagement.Subjectnotice;
                    }
                }

                context.SaveChanges();
                UpdateStructuresLocal();
                UpdateDecreeoperationsLocal();
                return; // Если мы меняем что-то без изменения в приказе, то меняем оригинал и прерываем дальнейшие действия.
            }

            /**
             * Creating new structure.
             */
            Structure newStructure = new Structure();
            newStructure.Name = structureManagement.Name;
            newStructure.Name1 = structureManagement.Name1;
            newStructure.Name2 = structureManagement.Name2;
            newStructure.Name3 = structureManagement.Name3;
            newStructure.Nameshortened = structureManagement.NameShortened;
            newStructure.Featured = structureManagement.Featured;
            newStructure.Structureregion = structureManagement.Structureregion;
            newStructure.Structuretype = structureManagement.Structuretype;
            newStructure.Street = structureManagement.Street;
            newStructure.City = structureManagement.City;
            newStructure.Rank = structureManagement.Rank;
            newStructure.Parentstructure = structureManagement.Parent;
            newStructure.Priority = structure.Priority; // Ставим прежний приоритет
            newStructure.Separatestructure = structure.Separatestructure;

            if (!structureManagement.Name.Equals(origin.Name))
            {
                newStructure.Changestructurerename = 1;
            }

            // Переносим заодно кураторство
            if (origin.Curator > 0)
            {
                newStructure.Curator = origin.Curator;
            }
            if (origin.Head > 0)
            {
                newStructure.Head = origin.Head;
            }

            /**
             * Для прохождения службы
             */

            structure.Subject1 = structureManagement.Subject1;
            structure.Subject2 = structureManagement.Subject2;
            structure.Subject3 = structureManagement.Subject3;
            structure.Subject4 = structureManagement.Subject4;
            structure.Subject5 = structureManagement.Subject5;
            structure.Subject6 = structureManagement.Subject6;
            structure.Subject7 = structureManagement.Subject7;
            structure.Subject8 = structureManagement.Subject8;
            structure.Subject9 = structureManagement.Subject9;
            structure.Subject10 = structureManagement.Subject10;
            structure.Subject11 = structureManagement.Subject11;
            structure.Subject12 = structureManagement.Subject12;
            structure.Subject13 = structureManagement.Subject13;
            structure.Subject14 = structureManagement.Subject14;
            structure.Subject15 = structureManagement.Subject15;
            structure.Subjectgender = structureManagement.Subjectgender;
            structure.Subjectnumber = structureManagement.Subjectnumber;
            structure.Subjectnotice = structureManagement.Subjectnotice;

            newStructure.Changeorigin = origin.Id;
            context.Structure.Add(newStructure);
            context.SaveChanges();
            UpdateStructuresLocal();

            Decree decree = GetDecreeByUser(user);
            Decreeoperation operation = new Decreeoperation();
            operation.Decree = decree.Id;
            operation.Changed = 1;
            operation.Changedtype = structureManagement.Id.GetValueOrDefault();
            operation.Subject = -newStructure.Id; // У подразделений subject имеет знак минуса
            //if (structureManagement.Datecustom > 0)
            //{
            //    operation.Dateactive = structureManagement.Dateactive;
            //    operation.Datecustom = 1;
            //}

            origin.Changestructurelast = newStructure.Id;
            context.Decreeoperation.Add(operation);
            context.SaveChanges();
            UpdateDecreeoperationsLocal();
        }



        /// <summary>
        /// Возвращает актуальную информацию для пользователя о подразделении. То есть возвращает ту запись о подразделении, которая располагалось на введенную дату
        /// </summary>
        /// <param name="structure"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public Structure GetActualStructureInfo(Structure structure, DateTime date)
        {
            return GetActualStructureInfo(structure.Id, date);
        }

        /// <summary>
        /// Возвращает актуальную информацию для пользователя о подразделении. То есть возвращает ту запись о подразделении, которая располагалось на введенную дату
        /// </summary>
        /// <param name="structureid"></param>
        /// <param name="date"></param>
        /// <param name="structures"></param>
        /// <returns></returns>
        public Structure GetActualStructureInfo(int structureid, DateTime date, IEnumerable<Structure> structures = null, IEnumerable<Structure> actual_structures_list_whith_origin = null)
        {
            actual_structures_list_whith_origin = actual_structures_list_whith_origin == null ? ActualStructuresListWhithOrigin : actual_structures_list_whith_origin;
            Dictionary<int, Structure> actual_structures = StructuresLocal();
            IEnumerable<Structure> actual_structures_list = actual_structures.Values;
            if (structureid == 0)
            {
                return null;
            }
            if (structures == null)
            {
                if (actual_structures == null)
                {
                    UpdateStructuresLocal();
                    actual_structures_list_whith_origin = actual_structures_list_whith_origin == null ? ActualStructuresListWhithOrigin : actual_structures_list_whith_origin;
                    actual_structures = StructuresLocal();
                    actual_structures_list = actual_structures.Values;
                }
                structures = actual_structures_list_whith_origin.Where(s => s.Changeorigin == structureid).Append(actual_structures[structureid]);
                structures = FilterDeletedStructures(structures, date);
            } // ????
            Structure originalStructure = GetOriginalStructure(structureid);
            if (originalStructure == null)
            {
                return null;
            }
            int originalStructure_id = originalStructure.Id;
            structures = actual_structures_list_whith_origin.Where(s => s.Changeorigin == originalStructure_id).Append(originalStructure);
            
            structures = FilterDeletedStructures(structures, date);

            // Удалять "гостей из будущего"
            int structures_count = structures.Count();
            if (structures_count == 0)
            {
                return null;
            }

            if (structures_count == 1)
            {
                Structure firstStructure = structures.First();
                int minusid = -firstStructure.Id;
                //Decreeoperation decreeoperation = DecreeoperationsLocal().Values.FirstOrDefault(deco => deco.Subject == minusid
                //&& (deco.Changed > 0 || deco.Created > 0)); // У подразделений subject имеет знак минуса
                Decreeoperation decreeoperation = null;
                List<Decreeoperation> decreeoperations = DecreeoperationsSubjectAsKeyLocal().GetValueOrDefault(minusid);
                if (decreeoperations != null)
                {
                    decreeoperation = decreeoperations.FirstOrDefault(v => v.Changed > 0 || v.Created > 0);
                }
                
                
                if (decreeoperation == null)
                {
                    return null;// Может быть Null
                }
                if (decreeoperation.Datecustom > 0)
                {
                    if (decreeoperation.Dateactive.GetValueOrDefault() > date)
                    {
                        //return null; -- don't know why it wasn't displayed 
                    }
                }
                else
                {
                        Decree decree = DecreesLocal()[decreeoperation.Decree];
                        //if (decree.Dateactive.GetValueOrDefault() > date)
                        if (decree.Datesigned.GetValueOrDefault() > date)
                        {
                            return null;
                        }
                }
                if (firstStructure.Changeorigin > 0 && !actual_structures.ContainsKey(firstStructure.Changeorigin))
                {
                    return null; // Новая встроенная проверка
                }
                return firstStructure; // If structure hasn't change we return it by itself; СЮДА ПОЧЕМУ-ТО ПОПАДАЕТ ОВПО
            }
            
            //structures = structures.Where(s => s.Changeorigin != 0); // Remove original;
            Dictionary<int, DateTime> changeDate = new Dictionary<int, DateTime>();
            List<Structure> structuresFiltered = new List<Structure>();
            foreach (Structure structure in structures)
            {
                if (structure.Changeorigin > 0 && !actual_structures.ContainsKey(structure.Changeorigin))
                {
                    
                } else
                {
                    structuresFiltered.Add(structure); // Новая встроенная проверка.
                }
            }
            structures = structuresFiltered;

            

            foreach (Structure structure in structures)
            {
                int minusid = -structure.Id;
                Decreeoperation decreeoperation = null;
                List<Decreeoperation> decreeoperations = DecreeoperationsSubjectAsKeyLocal().GetValueOrDefault(minusid);
                if (decreeoperations != null)
                {
                    decreeoperation = decreeoperations.FirstOrDefault(v => v.Changed > 0 || v.Created > 0);
                }

                if (decreeoperation != null)
                {
                    if (decreeoperation.Datecustom > 0)
                    {
                        if (decreeoperation.Dateactive.GetValueOrDefault() <= date)
                        {
                            changeDate.Add(structure.Id, decreeoperation.Dateactive.GetValueOrDefault()); // put date of decreeoperation
                        }
                    }
                    else
                    {
                        Decree decree = DecreesLocal()[decreeoperation.Decree];
                        if (decree.Dateactive.GetValueOrDefault() <= date)
                        {
                            changeDate.Add(structure.Id, decree.Dateactive.GetValueOrDefault()); // put date of decree
                        }

                    }

                }

            }

            

            if (changeDate.Count > 0)
            {
                int id = changeDate.OrderByDescending(r => r.Value).First().Key;
                Structure finalStructure = structures.FirstOrDefault(s => s.Id == id); // ИСПРАВИТЬ: не всегда находит структуру с нужным id
                if (finalStructure == null)
                {
                    return null;
                }
                return finalStructure;
            } else
            {
               
                return structures.Last();
            }
            
        }

        /// <summary>
        /// Повышает приоритет подразделения в списке подразделений. Чем выше, тем раньше оно будет встречаться в сортировке.
        /// </summary>
        /// <param name="structureid"></param>
        /// <param name="user"></param>
        public void UpStructure(int structureid, User user)
        {
            Structure structure = Structures.First(s => s.Id == structureid);
            bool hasParent = true;
            if (structure.Parentstructure == 0)
            {
                hasParent = false;
            }
            Structure parentStructure = null;
            if (hasParent)
            {
                parentStructure = StructuresLocal()[structure.Parentstructure];
            }
            List<Structure> structures = new List<Structure>();
            if (parentStructure != null)
            {
                structures = FilterDeletedStructures(Structures.Where(s => parentStructure.Id == s.Parentstructure), user.Date.GetValueOrDefault()).ToList();
            }
            else
            {
                structures = FilterDeletedStructures(Structures.Where(s => 0 == s.Parentstructure), user.Date.GetValueOrDefault()).ToList();
            }
            if (structures.FirstOrDefault(s => s.Id == structure.Id) == null)
            {
                return;
            }

            structures = structures.OrderBy(s => s.Priority).ToList();
            if (structures.First().Id == structureid)
            {
                return; // If first, we do nothing.
            }

            List<Structure> structuresSamePriority = structures.Where(s => s.Priority == structure.Priority).ToList();
            int posInSame = structuresSamePriority.FindIndex(s => s.Id == structure.Id);
            List<Structure> structuresLowerPriority = structures.Where(s => s.Priority < structure.Priority).ToList();
            Structure structureBitLowerPriority = structures.LastOrDefault(s => s.Priority < structure.Priority);
            if (structureBitLowerPriority == null || structuresSamePriority.First().Id != structure.Id) // means there are two or more elements with same level but it is the highest at the same time.
            {
                int ind = structuresSamePriority.Count;
                foreach (Structure str in structuresSamePriority)
                {
                    str.Priority += ind;
                    ind -= 1;
                }
                //structure.Priority += ind + 1;
                structuresSamePriority[posInSame].Priority -= 1;
                structuresSamePriority[posInSame - 1].Priority += 1;
                context.SaveChanges();
                return;
            }

            // major cases
            List<Structure> structuresBitLowerPriority = structures.Where(s => s.Priority == structureBitLowerPriority.Priority).ToList();
            List<Structure> structuresMuchLowerPriority = structures.Where(s => s.Priority < structureBitLowerPriority.Priority).ToList();
            foreach (Structure str in structuresLowerPriority)
            {
                str.Priority += -structuresSamePriority.Count - 1;
            }
            int index = structuresSamePriority.Count;
            foreach (Structure str in structuresSamePriority)
            {
                if (str.Id != structure.Id)
                {
                    str.Priority += index;
                    index -= 1;
                }
            }
            int additionalPriority = structureBitLowerPriority.Priority - 1 - structure.Priority;
            foreach (Structure str in structuresMuchLowerPriority)
            {
                str.Priority -= 1;
            }
            structure.Priority += additionalPriority;
            int newpriority = structure.Priority;

            context.SaveChanges();
            UpdateStructuresLocal();
        }

        /// <summary>
        /// Понижает приоритет подразделения в списке подразделений. Чем ниже, тем позже оно будет встречаться в сортировке.
        /// </summary>
        /// <param name="structureid"></param>
        /// <param name="user"></param>
        public void DownStructure(int structureid, User user)
        {

            //Structure structure = Structures.First(s => s.Id == structureid);
            Structure structure = Structures.First(s => s.Id == structureid);
            if (structure.Changeorigin > 0)
            {
                //DownStructure(structure.Changeorigin, user);
            }

            bool hasParent = true;
            if (structure.Parentstructure == 0)
            {
                hasParent = false;
            }
            Structure parentStructure = null;
            if (hasParent)
            {
                parentStructure = StructuresLocal()[structure.Parentstructure];
            }
            List<Structure> structures = new List<Structure>();
            if (parentStructure != null)
            {
                structures = FilterDeletedStructures(Structures.Where(s => parentStructure.Id == s.Parentstructure), user.Date.GetValueOrDefault()).ToList();
            } else
            {
                structures = FilterDeletedStructures(Structures.Where(s => 0 == s.Parentstructure), user.Date.GetValueOrDefault()).ToList();
            }
            if (structures.FirstOrDefault(s => s.Id == structure.Id) == null)
            {
                return;
            }

            structures = structures.OrderBy(s => s.Priority).ToList();
            if (structures.Last().Id == structureid)
            {
                return; // If last, we do nothing.
            }

            List<Structure> structuresSamePriority = structures.Where(s => s.Priority == structure.Priority).ToList();
            int posInSame = structuresSamePriority.FindIndex(s => s.Id == structure.Id);
            List<Structure> structuresHigherPriority = structures.Where(s => s.Priority > structure.Priority).ToList();
            Structure structureBitHigherPriority = structures.FirstOrDefault(s => s.Priority > structure.Priority);
            if (structureBitHigherPriority == null || structuresSamePriority.Last().Id != structure.Id) // means there are two or more elements with same level but it is the highest at the same time.
            {
                int ind = 1;
                foreach (Structure str in structuresSamePriority)
                {
                        str.Priority += ind;
                        ind += 1;
                }
                //structure.Priority += ind + 1;
                structuresSamePriority[posInSame].Priority += 1;
                structuresSamePriority[posInSame+1].Priority -= 1;
                context.SaveChanges();
                return;
            }

            // major cases
            List<Structure> structuresBitHigherPriority = structures.Where(s => s.Priority == structureBitHigherPriority.Priority).ToList();
            List<Structure> structuresMuchHigherPriority = structures.Where(s => s.Priority > structureBitHigherPriority.Priority).ToList();
            foreach (Structure str in structuresHigherPriority)
            {
                str.Priority += structuresSamePriority.Count - 1;
            }
            int index = 1;
            foreach (Structure str in structuresSamePriority)
            {
                if (str.Id != structure.Id)
                {
                    str.Priority += index;
                    index += 1;
                }
            }
            int additionalPriority = structureBitHigherPriority.Priority + 1 - structure.Priority;
            foreach (Structure str in structuresMuchHigherPriority)
            {
                str.Priority += 1;
            }
            structure.Priority += additionalPriority;
            

            context.SaveChanges();
            UpdateStructuresLocal();
        }

        /// <summary>
        /// Меняет приоритет у подразделения на указанный. Чем выше приоритет, тем раньше подразделение будет встречаться в сортировке/списках и наоборот.
        /// </summary>
        /// <param name="structureid"></param>
        /// <param name="value"></param>
        /// <param name="user"></param>
        public void PrioritychangeStructure(int structureid, int value, User user)
        {

            //Structure structure = Structures.First(s => s.Id == structureid);
            Structure structure = Structures.First(s => s.Id == structureid);
            if (structure.Changeorigin > 0)
            {
                //DownStructure(structure.Changeorigin, user);
            }

            structure.Priority = value;


            context.SaveChanges();
            UpdateStructuresLocal();
        }

        /// <summary>
        /// Редактирование подразделения в режиме работы с проектом приказа. В этом случае новое подразделение не создается, а меняется текущее напрямую.
        /// </summary>
        /// <param name="structureManagement"></param>
        /// <param name="user"></param>
        public void RenameStructureDecree(StructureManagement structureManagement, User user)
        {
            Structure structure = Structures.First(s => s.Id == structureManagement.Id);
            int id = structureManagement.Id.GetValueOrDefault();
            if (StructuresLocal().ContainsKey(id))
            {
                structure = GetActualStructureInfo(structureManagement.Id.GetValueOrDefault(), user.Date.GetValueOrDefault());
                id = structure.Id;
                structure = Structures.First(s => s.Id == id);
            }


            
            structure.Name = structureManagement.Name;
            structure.Nameshortened = structureManagement.NameShortened;
            structure.Featured = structureManagement.Featured;
            structure.Parentstructure = structureManagement.Parent;
            structure.Structureregion = structureManagement.Structureregion;
            structure.Structuretype = structureManagement.Structuretype;
            structure.Street = structureManagement.Street;
            structure.City = structureManagement.City;
            structure.Rank = structureManagement.Rank;
            structure.Separatestructure = structureManagement.Separatestructure;

            int minusstructureid = -structure.Id;
            Decreeoperation contextOperation = Decreeoperations.FirstOrDefault(d => d.Subject == minusstructureid);
            if (contextOperation != null)
            {
                if (structureManagement.Datecustom > 0)
                {
                    contextOperation.Dateactive = structureManagement.Dateactive;
                    contextOperation.Datecustom = 1;
                }
                else
                {
                    contextOperation.Datecustom = 0;
                }
            }
            

            /*if (structureManagement.Parent.GetValueOrDefault(0) > 0) Currently disabled
            {
                structure.Parentstructure = structureManagement.Parent.GetValueOrDefault(0);
            }*/

            //context.Structure.Add(structure);
            context.SaveChanges();
            UpdateDecreeoperationsLocal();
            UpdateStructuresLocal();
        }

        /// <summary>
        /// Удаляет подразделение. При этом само подразделение остается в базе, но создается пункт приказа, который свидетельствует о том, что подразделение более неактивно.
        /// Помечает к удалению все подчиненные подразделения и должности в них.
        /// </summary>
        /// <param name="structureManagement"></param>
        /// <param name="user"></param>
        public void RemoveStructure(StructureManagement structureManagement, User user)
        {
            Decree decree = GetDecreeByUser(user);
            Decreeoperation operation = new Decreeoperation();
            operation.Decree = decree.Id;
            operation.Deleted = 1;
            operation.Subject = -structureManagement.Parent; // У подразделений subject имеет знак минуса
            if (structureManagement.Datecustom > 0)
            {
                operation.Dateactive = structureManagement.Dateactive;
                operation.Datecustom = 1;
            }
            context.Decreeoperation.Add(operation);

            int structureid = structureManagement.Parent;
            List<StructureInfo> structureInfos = GetStructureInfos(structureid, user, false, false , false, true);
            List<Position> positionsToDelete = new List<Position>();
            foreach (StructureInfo structureInfo in structureInfos)
            {
                Structure structure = GetActualStructureInfo(structureInfo.Id, user.Date.GetValueOrDefault());
                if (structure != null)
                {
                    int id = structure.Id;
                    if (structure.Changeorigin > 0)
                    {
                        id = structure.Changeorigin;
                    }
                    IEnumerable<Position> positions = GetPositions(id, user.Date.GetValueOrDefault(), false);
                    positionsToDelete.AddRange(positions);

                    Decreeoperation operationStruct = new Decreeoperation();
                    operationStruct.Decree = decree.Id;
                    operationStruct.Deleted = 1;
                    operationStruct.Subject = -structure.Id; // У подразделений subject имеет знак минуса
                    if (structureManagement.Datecustom > 0)
                    {
                        operationStruct.Dateactive = structureManagement.Dateactive;
                        operationStruct.Datecustom = 1;
                    }
                    context.Decreeoperation.Add(operationStruct);
                }
            }

            bool custom = false;
            DateTime dateCustom = DateTime.Now;
            if (structureManagement.Datecustom > 0)
            {
                dateCustom = structureManagement.Dateactive;
                custom = true;
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
                if (custom)
                {
                    operationPosition.Dateactive = dateCustom;
                    operationPosition.Datecustom = 1;
                }
                context.Decreeoperation.Add(operationPosition);
                //context.SaveChanges();
            }
            //RemovePosition
            context.SaveChanges();
            UpdateStructuresLocal();
            UpdateDecreeoperationsLocal();

            //context.Structure.Remove(Structures.First(u => u.Id == structureManagement.Parent.GetValueOrDefault(0)));
            //context.SaveChanges();
        }

        /// <summary>
        /// Удаляет подразделение, еще не существующее и находящееся в проекте приказа. Удаляется из базы данных в принципе. 
        /// </summary>
        /// <param name="structureManagement"></param>
        /// <param name="user"></param>
        public void RemoveStructureDecree(StructureManagement structureManagement, User user)
        {
            int id = structureManagement.Id.GetValueOrDefault();
            if (StructuresLocal().ContainsKey(id))
            {
                Structure structure = GetActualStructureInfo(structureManagement.Id.GetValueOrDefault(), user.Date.GetValueOrDefault());
                id = structure.Id;
            }

            int minusid = -id;
            Decreeoperation contextDecreeoperation = Decreeoperations.LastOrDefault(d => d.Subject == minusid); // У подразделений subject имеет знак минуса
            if (contextDecreeoperation != null)
            {
                if (contextDecreeoperation.Created == 1)
                {
                    int structureid = contextDecreeoperation.Subject;
                    if (structureid < 0)
                    {
                        structureid = -structureid;
                    }
                    Structure contextStructure = Structures.First(p => p.Id == structureid);
                    context.Structure.Remove(contextStructure);
                    context.SaveChanges();
                }
                


                context.Decreeoperation.Remove(contextDecreeoperation);
                context.SaveChanges();
                UpdateDecreeoperationsLocal();
            }
        }

        /// <summary>
        /// Создает новый орг-штатный проект приказа.
        /// </summary>
        /// <param name="decreeManagement"></param>
        /// <param name="user"></param>
        public void AddNewDecree(DecreeManagement decreeManagement, User user)
        {
            User contextUser = Users.First(u => u.Id == user.Id);
            DateTime date = contextUser.Date.GetValueOrDefault();

            Decree decree = new Decree();
            decree.User = user.Id;
            decree.Nickname = decreeManagement.Nickname;
            decree.Datesigned = date;
            decree.Dateactive = date;
            //decree.Datesigned = DateTime.Now.Date;
            //decree.Dateactive = DateTime.Now.Date;
            context.Decree.Add(decree);
            context.SaveChanges();
            contextUser.Decree = decree.Id;
            context.SaveChanges();

            UpdateDateactives(decree.Id);
            UpdateDecreesLocal();
            UpdateUsersLocal();
        }

        /// <summary>
        ///  Создает новый орг-штатный проект приказа.
        /// </summary>
        /// <param name="decree_from_history"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public Decree AddNewDecree(DecreeHistroryElementToAppending decree_from_history, User user)
        {
            DateTime date = decree_from_history.date.GetValueOrDefault();

            Decree decree = new Decree();
            decree.User = user.Id;
            decree.Name = "Исторический приказ";
            decree.Nickname = decree.Name;
            decree.Number = decree_from_history.number;
            decree.Datesigned = date;
            decree.Dateactive = date;
            decree.Historycal = decree_from_history.history;
            decree.Signed = 1;
            context.Decree.Add(decree);
            context.SaveChanges();

            UpdateDateactives(decree.Id);
            UpdateDecreesLocal();
            return decree;
        }

        public Decreeoperation AddNewDecreeOperation(Decreeoperation decree_from_history)
        {
            context.Decreeoperation.Add(decree_from_history);
            context.SaveChanges();

            UpdateDateactives(decree_from_history.Id);
            UpdateDecreeoperationsLocal();
            UpdateUsersLocal();
            return decree_from_history;
        }

        /// <summary>
        /// Удаляет орг-штатный проект приказа. Вместе с ним удаляются все пункты приказа и внесенные ими изменения.
        /// </summary>
        /// <param name="decreeManagement"></param>
        /// <param name="user"></param>
        public void RemoveDecree(DecreeManagement decreeManagement, User user)
        {
            User contextUser = Users.First(u => u.Id == user.Id);
            Decree decree = Decrees.First(d => d.Id == decreeManagement.Id);
            contextUser.Decree = 0;
            decree.Declined = 1;

            List<Decreeoperation> decreeoperationsToRemove = Decreeoperations.Where(d => d.Decree == decreeManagement.Id).ToList();
            foreach(Decreeoperation decreeoperation in decreeoperationsToRemove)
            {
                DecreeoperationManagement decreeoperationManagement = new DecreeoperationManagement();
                decreeoperationManagement.Id = decreeoperation.Id;
                RemoveDecreeOperationWithItsSubject(decreeoperationManagement);
            }
            context.SaveChanges();

            UpdateDecreesLocal();
            UpdateUsersLocal();
        }

        public void RemoveDecreeOperationWithItsSubject(DecreeoperationManagement decreeoperation)
        {
            Decreeoperation contextDecreeoperation = Decreeoperations.First(d => d.Id == decreeoperation.Id);
            if (contextDecreeoperation.Subject > 0)
            {
                    /**
                     * Reverting position creation
                     */
                    if (contextDecreeoperation.Created > 0)
                {
                    Position contextPosition = Positions.First(p => p.Id == contextDecreeoperation.Subject);
                    context.Position.Remove(contextPosition);
                    context.SaveChanges();
                }

                /**
                 * Reverting position deletion
                 */
                if (contextDecreeoperation.Deleted > 0)
                {

                }
            }
            else { 
                    /**
                     * Reverting structure creation
                     */
                    if (contextDecreeoperation.Created > 0)
                    {
                        int structureid = contextDecreeoperation.Subject;
                        if (structureid < 0)
                        {
                            structureid = -structureid;
                        }
                        Structure contextStructure = Structures.First(s => s.Id == structureid);
                        context.Structure.Remove(contextStructure);
                        context.SaveChanges();
                    }

                    /**
                     * Reverting stucture deletion
                     */
                    if (contextDecreeoperation.Deleted > 0)
                    {
                        
                    }

                    if (contextDecreeoperation.Changed > 0)
                    {
                        int structureid = contextDecreeoperation.Subject;
                        if (structureid < 0)
                        {
                            structureid = -structureid;
                        }
                        Structure contextStructure = Structures.First(s => s.Id == structureid);
                        context.Structure.Remove(contextStructure);
                        context.SaveChanges();
                    }
            }

            context.Decreeoperation.Remove(contextDecreeoperation);
            context.SaveChanges();
            UpdateDecreeoperationsLocal();
        }

        public void AcceptDecree(DecreeManagement decreeManagement, User user)
        {
            User contextUser = Users.First(u => u.Id == user.Id);
            Decree decree = Decrees.First(d => d.Id == decreeManagement.Id);
            UpdateStructuresLocal();
            foreach (Decreeoperation p in DecreeoperationsLocal().Values.ToList().FindAll(r => r.Decree == decree.Id))
            {
                if(p.Changed == 1 && StructuresLocal().ContainsKey(p.Changedtype) && p.Subject < 0 && p.Dateactive.GetValueOrDefault().Date <= contextUser.Date)
                {
                    foreach (Decreeoperation d in DecreeoperationsLocal().Values.ToList().FindAll(r => r.Changedtype == p.Changedtype))
                    {
                        if(StructuresLocal().ContainsKey(d.Subject * -1) && StructuresLocal()[d.Subject * -1].Featured == 1 && d.Id != p.Id)
                        {
                            Structure structure = Structures.First(r => r.Id == d.Subject * -1);
                            structure.Featured = 0;
                            context.SaveChanges();
                        }
                    }
                    if (StructuresLocal()[p.Changedtype].Featured == 1)
                    {
                        Structure structure = Structures.First(r => r.Id == p.Changedtype);
                        structure.Featured = 0;
                        context.SaveChanges();
                    }
                }
            }
            contextUser.Decree = 0;
            decree.Name = decreeManagement.Name;
            decree.Number = decreeManagement.Number;
            decree.Dateactive = decreeManagement.Dateactive;
            decree.Datesigned = decreeManagement.Datesigned;
            decree.Signed = 1;
            context.SaveChanges();

            UpdateDateactives(decree.Id);
            UpdateUsersLocal();
            UpdateStructuresLocal();
        }

        public void UpdateDecree(DecreeManagement decreeManagement, User user)
        {
            User contextUser = Users.First(u => u.Id == user.Id);
            Decree decree = Decrees.First(d => d.Id == decreeManagement.Id);
            //contextUser.Decree = 0;
            decree.Name = decreeManagement.Name;
            decree.Number = decreeManagement.Number;
            decree.Nickname = decreeManagement.Nickname;
            decree.Dateactive = decreeManagement.Dateactive;
            decree.Datesigned = decreeManagement.Datesigned;
            context.SaveChanges();

            UpdateDateactives(decree.Id);
            UpdateUsersLocal();
        }

        public List<DecreeManagement> GetFilteredDecrees(User user, DecreeManagement filter)
        {
            //Decreemanagement[]
            List<DecreeManagement> decreeManagements = new List<DecreeManagement>();
            foreach (Decree decree in DecreesLocal().Values)
            {
                if (decree.Signed > 0)
                {
                    DecreeManagement decreeManagement = new DecreeManagement();
                    decreeManagement.Id = decree.Id;
                    decreeManagement.Signed = 1;
                    decreeManagement.User = decree.User.GetValueOrDefault();
                    decreeManagement.Nickname = decree.Nickname;
                    decreeManagement.Name = decree.Name;
                    decreeManagement.Number = decree.Number;
                    decreeManagement.Dateactive = decree.Dateactive;
                    decreeManagement.Datesigned = decree.Datesigned;
                    decreeManagements.Add(decreeManagement);
                }
            }
            if (filter.Nickname.Length > 0)
            {
                decreeManagements = decreeManagements.Where(d => d.Nickname != null && d.Nickname.ToLower().Contains(filter.Nickname.ToLower())).ToList();
            }
            if (filter.Name.Length > 0)
            {
                decreeManagements = decreeManagements.Where(d => d.Name != null && d.Name.ToLower().Contains(filter.Name.ToLower())).ToList();
            }
            if (filter.Number.Length > 0)
            {
                decreeManagements = decreeManagements.Where(d =>
                {
                    if (d.Number == null)
                    {
                        return false;
                    }
                    if (filter.Number == null)
                    {
                        return false;
                    }
                    return d.Number.ToLower().Contains(filter.Number.ToLower());
                }).ToList();
            }
            if (filter.Dateactivestart.Length > 0)
            {
                DateTime date = Convert.ToDateTime(filter.Dateactivestart);
                decreeManagements = decreeManagements.Where(d => date <= d.Dateactive).ToList();
            }
            if (filter.Dateactiveend.Length > 0)
            {
                DateTime date = Convert.ToDateTime(filter.Dateactiveend);
                decreeManagements = decreeManagements.Where(d => date >= d.Dateactive).ToList();
            }
            if (filter.Datesignedstart.Length > 0)
            {
                DateTime date = Convert.ToDateTime(filter.Datesignedstart);
                decreeManagements = decreeManagements.Where(d => date <= d.Dateactive).ToList();
            }
            if (filter.Dateactiveend.Length > 0)
            {
                DateTime date = Convert.ToDateTime(filter.Dateactiveend);
                decreeManagements = decreeManagements.Where(d => date >= d.Dateactive).ToList();
            }
            return decreeManagements;

        }

        public List<Decreeoperation> GetDecreeoperations(Decree decree)
        {
            return DecreeoperationsLocal().Values.Where(d => d.Decree == decree.Id).ToList();
        }


        /**
         * Return decree operation ids. Map: positionid - decreeoperationid;
         * Origin - id of ancestor. If 0, means this position will be ancestor.
         */
        public Dictionary<int, int> AddPosition(PositionManagement positionManagement, User user, int origin)
        {
            Dictionary<int, int> decreeoperationsids = new Dictionary<int, int>();
            List<PositionPart> positionParts = new List<PositionPart>();
            string[] parts = positionManagement.PositionsCode.Split(';');
            foreach (string part in parts)
            {
                PositionPart positionPart = new PositionPart();
                string[] elements = part.Split('&');
                foreach (string element in elements)
                {
                    string[] pair = element.Split("=");
                    switch (pair[0])
                    {
                        case "id":
                            if (pair[1].Length > 0)
                            {
                                positionPart.Id = Int32.Parse(pair[1]);
                            }
                            break;
                        case "civil":
                            if (pair[1].Length > 0)
                            {
                                positionPart.Civil = Boolean.Parse(pair[1]);
                            }
                            break;
                        case "civildatelimit":
                            if (pair[1].Length > 0)
                            {
                                positionPart.Civildatelimit = Boolean.Parse(pair[1]);
                            }
                            break;
                        case "civildate":
                            if (pair[1].Length > 0)
                            {
                                string[] dates = pair[1].Split('-');
                                positionPart.Civildate = new DateTime(Int32.Parse(dates[0]), Int32.Parse(dates[1]), Int32.Parse(dates[2]));
                            }
                            break;
                        case "custom":
                            if (pair[1].Length > 0)
                            {
                                positionPart.Custom = Boolean.Parse(pair[1]);
                            }
                            break;
                        case "customdate":
                            if (pair[1].Length > 0)
                            {
                                string[] dates = pair[1].Split('-');
                                positionPart.Customdate = new DateTime(Int32.Parse(dates[0]), Int32.Parse(dates[1]), Int32.Parse(dates[2]));
                            }
                            break;
                        case "decertificate":
                            if (pair[1].Length > 0)
                            {
                                positionPart.Decertificate = Boolean.Parse(pair[1]);
                            }
                            break;
                        case "decertificatedate":
                            if (pair[1].Length > 0)
                            {
                                string[] dates = pair[1].Split('-');
                                positionPart.Decertificatedate = new DateTime(Int32.Parse(dates[0]), Int32.Parse(dates[1]), Int32.Parse(dates[2]));
                            }
                            break;
                        case "decree":
                            if (pair[1].Length > 0)
                            {
                                positionPart.Decree = Boolean.Parse(pair[1]);
                            }
                            break;
                        case "decreenumber":
                            if (pair[1].Length > 0)
                            {
                                positionPart.Decreenumber = pair[1];
                            }
                            break;
                        case "decreedate":
                            if (pair[1].Length > 0)
                            {
                                string[] dates = pair[1].Split('-');
                                positionPart.Decreedate = new DateTime(Int32.Parse(dates[0]), Int32.Parse(dates[1]), Int32.Parse(dates[2]));
                            }
                            break;
                        default:
                            break;
                    }
                }
                positionParts.Add(positionPart);
            }
            positionManagement.PositionParts = positionParts;



            positionManagement.PositionParts.Last();
            foreach (PositionPart part in positionManagement.PositionParts)
            {
                Position position = new Position();
                position.Positiontype = positionManagement.Positiontype;
                position.Positioncategory = positionManagement.Positioncategory;
                bool fullCivilPosition = false;
                Positioncategory positionCategory = PositioncategoriesLocal().Values.First(pc => pc.Id == position.Positioncategory);
                /**
                 * If civil position or not
                 */
                if (positionCategory.Civil > 0)
                {
                    fullCivilPosition = true;
                }

                int departmentID = positionManagement.Department.GetValueOrDefault(0);
                if (departmentID < 0)
                {
                    departmentID = -departmentID;
                }
                position.Structure = departmentID;

                
                position.Cap = positionManagement.RankCap;
                position.Sourceoffinancing = positionManagement.Sof.GetValueOrDefault(1);
                position.Replacedbycivil = 0;
                position.Replacedbycivilpositioncategory = positionManagement.Replacedbycivilpositioncategory;
                position.Replacedbycivilpositiontype = positionManagement.Replacedbycivilpositiontype;
                position.Opchs = positionManagement.Opchs;
                if (part.Civil)
                {
                    position.Replacedbycivil = 1;
                    position.Civilrankhigh = positionManagement.Civilrankhigh;
                    position.Civilranklow = positionManagement.Civilranklow;
                    position.Replacedbycivildatelimit = 0;
                    if (part.Civildatelimit)
                    {
                        position.Replacedbycivildatelimit = 1;
                        position.Replacedbycivildate = part.Civildate;
                    }
                    if (part.Decree)
                    {
                        position.Civildecree = 1;
                        position.Civildecreenumber = part.Decreenumber;
                        position.Civildecreedate = part.Decreedate;
                    }
                } else
                {
                    position.Civilrankhigh = 0;
                    position.Civilranklow = 0;
                    position.Replacedbycivildatelimit = 0;
                    position.Civildecree = 0;
                }

                /**
                 * Для прохождения службы
                 */
                position.Subject1 = positionManagement.Subject1;
                position.Subject2 = positionManagement.Subject2;
                position.Subject3 = positionManagement.Subject3;
                position.Subject4 = positionManagement.Subject4;
                position.Subject5 = positionManagement.Subject5;
                position.Subject6 = positionManagement.Subject6;
                position.Subject7 = positionManagement.Subject7;
                position.Subject8 = positionManagement.Subject8;
                position.Subject9 = positionManagement.Subject9;
                position.Subject10 = positionManagement.Subject10;
                position.Subject11 = positionManagement.Subject11;
                position.Subject12 = positionManagement.Subject12;
                position.Subject13 = positionManagement.Subject13;
                position.Subject14 = positionManagement.Subject14;
                position.Subject15 = positionManagement.Subject15;
                position.Subject16 = positionManagement.Subject16;
                position.Subject17 = positionManagement.Subject17;
                position.Subject18 = positionManagement.Subject18;
                position.Subject19 = positionManagement.Subject19;
                position.Subject20 = positionManagement.Subject20;

                /**
                 * Contains classes.
                 */
                if (fullCivilPosition && positionCategory.Classcap > 0)
                {
                    position.Civilrankhigh = positionManagement.Civilrankhigh;
                    position.Civilranklow = positionManagement.Civilranklow;
                }

                position.Notice = positionManagement.Notice;
                position.Decertificate = 0;

                if (part.Decertificate)
                {
                    position.Decertificate = 1;
                    position.Decertificatedate = part.Decertificatedate;
                }

                /**
                 * Mark it has altranks.
                 */
                if (positionManagement.Altrankconditiongroup > 0)
                {
                    position.Altrank = 1;
                }
                else
                {
                    position.Altrank = 0;
                }

                /**
                 * For curation
                 */
                position.Curator = 0;
                int[] structureIds = null;
                if (positionManagement.Curator > 0)
                {
                    position.Curator = 1;
                    position.Curatorlist = positionManagement.Curatorlist;
                    structureIds = position.Curatorlist.Split(',').Select(int.Parse).ToArray(); // Кураторку отделяем запятыми
                }

                /**
                 * For heading
                 */
                position.Head = 0;
                if (positionManagement.Head > 0)
                {
                    position.Head = 1;
                    position.Headid = positionManagement.Headid;
                }



                /**
                 * If partly
                 */
                if (part == positionManagement.PositionParts.Last())
                //if (positionManagement.Quantity < 1)
                {
                    
                    double partval = positionManagement.Quantity - Math.Truncate(positionManagement.Quantity);
                    if (partval > 0)
                    {
                        position.Part = 1;
                        position.Partval = partval;
                    }
                    
                }

               
                /**
                 * FINALLY ADD
                 */
                context.Position.Add(position);
                context.SaveChanges();
                position.Opchs = positionManagement.Opchs; // Потому что до создания криво записывает
                if (origin == 0)
                {
                    position.Origin = position.Id;
                    context.SaveChanges();
                } else
                {
                    position.Origin = origin;
                    context.SaveChanges();
                }
                //UpdatePositionsLocal();

                /**
                 * For curation 2
                 */
                if (structureIds != null)
                {
                    foreach (int i in structureIds)
                    {
                        Structure structure = context.Structure.First(s => s.Id == i);
                        structure.Curator = position.Id;
                    }
                    context.SaveChanges();
                }

                /**
                 * For heading 2
                 */
                 if (position.Head > 0 && position.Headid > 0)
                {
                    Structure structure = context.Structure.First(s => s.Id == position.Headid);
                    structure.Head = position.Id;
                    context.SaveChanges();
                }

                /**
                 * Adding MRD array now
                 */
                if (!String.IsNullOrEmpty(positionManagement.Mrd))
                {
                    int[] mrds = Array.ConvertAll(positionManagement.Mrd.Split(','), s => int.Parse(s));
                    int positionid = position.Id;
                    foreach (int mrd in mrds)
                    {
                        Positionmrd positionmrd = new Positionmrd();
                        positionmrd.Mrd = mrd;
                        positionmrd.Position = positionid;
                        context.Positionmrd.Add(positionmrd);
                    }
                    context.SaveChanges();
                }

                /**
                 * If altranks enabled.
                 */
                if(positionManagement.Altrankconditiongroup > 0)
                {
                    foreach(string altrankStr in positionManagement.Altranks.Split(";"))
                    {
                        Altrank altrank = new Altrank();
                        string[] pair = altrankStr.Split("=");
                        altrank.Position = position.Id;
                        altrank.Altrankcondition = Int32.Parse(pair[0]);
                        altrank.Rank = Int32.Parse(pair[1]);
                        context.Altrank.Add(altrank);
                    }
                    context.SaveChanges();
                }

                
                /**
                 * Putting changes to a decree.
                 */
                Decree decree = GetDecreeByUser(user);
                Decreeoperation operation = new Decreeoperation();
                operation.Decree = decree.Id;
                operation.Created = 1;
                operation.Subject = position.Id; 
                
                if (part.Custom)
                {
                    operation.Dateactive = part.Customdate;
                    operation.Datecustom = 1;
                }

                context.Decreeoperation.Add(operation);
                context.SaveChanges();
                //UpdateDecreeoperationsLocal();

                decreeoperationsids.Add(position.Id, operation.Id);
            }
            UpdatePositionsLocal();
            UpdatePositionmrdsLocal();
            UpdateDecreeoperationsLocal();
            return decreeoperationsids;
        }

        public Position ClonePosition(Position positionToClone, User user)
        {
            Position position = new Position();
            position.Id = 0;
            position.Cap = positionToClone.Cap;
            position.Dateactive = positionToClone.Dateactive;
            position.Dateinactive = positionToClone.Dateinactive;
            position.Sourceoffinancing = positionToClone.Sourceoffinancing;
            position.Positiontype = positionToClone.Positiontype;
            position.Notice = positionToClone.Notice;
            position.Positioncategory = positionToClone.Positioncategory;
            position.Replacedbycivil = positionToClone.Replacedbycivil;
            position.Replacedbycivilpositioncategory = positionToClone.Replacedbycivilpositioncategory;
            position.Replacedbycivilpositiontype = positionToClone.Replacedbycivilpositiontype;
            position.Opchs = positionToClone.Opchs;
            position.Altrank = positionToClone.Altrank;
            //position.Altrank = positionToClone.Altrank;
            position.Origin = 0;
            position.Decertificate = positionToClone.Decertificate;
            position.Decertificatedate = positionToClone.Decertificatedate;
            position.Civilranklow = positionToClone.Civilranklow;
            position.Civilrankhigh = positionToClone.Civilrankhigh;
            position.Replacedbycivildatelimit = positionToClone.Replacedbycivildatelimit;
            position.Replacedbycivildate = positionToClone.Replacedbycivildate;
            position.Structure = 0; // Put parent later
            position.Positiontype = positionToClone.Positiontype;
            position.Civildecree = positionToClone.Civildecree;
            position.Civildecreenumber = positionToClone.Civildecreenumber;
            position.Civildecreedate = positionToClone.Civildecreedate;
            position.Curator = 0;
            position.Head = 0;
            position.Curatorlist = "";
            position.Headid = 0;

            /**
             * Для прохождения службы
             */
            position.Subject1 = positionToClone.Subject1;
            position.Subject2 = positionToClone.Subject2;
            position.Subject3 = positionToClone.Subject3;
            position.Subject4 = positionToClone.Subject4;
            position.Subject5 = positionToClone.Subject5;
            position.Subject6 = positionToClone.Subject6;
            position.Subject7 = positionToClone.Subject7;
            position.Subject8 = positionToClone.Subject8;
            position.Subject9 = positionToClone.Subject9;
            position.Subject10 = positionToClone.Subject10;
            position.Subject11 = positionToClone.Subject11;
            position.Subject12 = positionToClone.Subject12;
            position.Subject13 = positionToClone.Subject13;
            position.Subject14 = positionToClone.Subject14;
            position.Subject15 = positionToClone.Subject15;
            position.Subject16 = positionToClone.Subject16;
            position.Subject17 = positionToClone.Subject17;
            position.Subject18 = positionToClone.Subject18;
            position.Subject19 = positionToClone.Subject19;
            position.Subject20 = positionToClone.Subject20;

            context.Position.Add(position);
            context.SaveChanges();
            position.Origin = position.Id;
            context.SaveChanges();

            List<Positionmrd> positionmrdsToClone = Positionmrds.Where(pm => pm.Position == positionToClone.Id).ToList();
            foreach(Positionmrd positionmrdToClone in positionmrdsToClone)
            {
                Positionmrd positionmrd = new Positionmrd();
                positionmrd.Mrd = positionmrdToClone.Mrd;
                positionmrd.Position = position.Id;
                context.Positionmrd.Add(positionmrd);
            }
            context.SaveChanges();



            if (position.Altrank > 0)
            {
                List<Altrank> altranksToClone = Altranks.Where(ar => ar.Position == positionToClone.Id).ToList();
                foreach (Altrank altrankToClone in altranksToClone)
                {
                    Altrank altrank = new Altrank();
                    altrank.Primary = altrankToClone.Primary;
                    altrank.Altrankcondition = altrankToClone.Altrankcondition;
                    altrank.Rank = altrankToClone.Rank;
                    altrank.Position = position.Id;
                    context.Altrank.Add(altrank);
                }
            }
            context.SaveChanges();

            

            /**
             * Putting changes to a decree.
             */
            Decree decree = GetDecreeByUser(user);
            Decreeoperation operation = new Decreeoperation();
            operation.Decree = decree.Id;
            operation.Created = 1;
            operation.Subject = position.Id;

            context.Decreeoperation.Add(operation);
            context.SaveChanges();
            UpdateDecreeoperationsLocal();
            UpdatePositionsLocal();
            UpdatePositionmrdsLocal();
            return position;
        }

        /**
         * Return decree operation ids. Map: originid - decreeoperationid;
         */
        public Dictionary<int, int> RemovePosition(PositionManagement positionManagement, User user)
        {
            
            Decree decree = GetDecreeByUser(user);

            List<PositionPart> positionParts = new List<PositionPart>();
            string[] parts = positionManagement.PositionsCode.Split(';');
            foreach (string part in parts)
            {
                PositionPart positionPart = new PositionPart();
                string[] elements = part.Split('&');
                foreach (string element in elements)
                {
                    string[] pair = element.Split("=");
                    switch (pair[0])
                    {
                        case "id":
                            if (pair[1].Length > 0)
                            {
                                positionPart.Id = Int32.Parse(pair[1]);
                            }
                            break;
                        case "civil":
                            if (pair[1].Length > 0)
                            {
                                positionPart.Civil = Boolean.Parse(pair[1]);
                            }
                            break;
                        case "civildatelimit":
                            if (pair[1].Length > 0)
                            {
                                positionPart.Civildatelimit = Boolean.Parse(pair[1]);
                            }
                            break;
                        case "civildate":
                            if (pair[1].Length > 0)
                            {
                                string[] dates = pair[1].Split('-');
                                positionPart.Civildate = new DateTime(Int32.Parse(dates[0]), Int32.Parse(dates[1]), Int32.Parse(dates[2]));
                            }
                            break;
                        case "custom":
                            if (pair[1].Length > 0)
                            {
                                positionPart.Custom = Boolean.Parse(pair[1]);
                            }
                            break;
                        case "customdate":
                            if (pair[1].Length > 0)
                            {
                                string[] dates = pair[1].Split('-');
                                positionPart.Customdate = new DateTime(Int32.Parse(dates[0]), Int32.Parse(dates[1]), Int32.Parse(dates[2]));
                            }
                            break;
                        case "decertificate":
                            if (pair[1].Length > 0)
                            {
                                positionPart.Decertificate = Boolean.Parse(pair[1]);
                            }
                            break;
                        case "decertificatedate":
                            if (pair[1].Length > 0)
                            {
                                string[] dates = pair[1].Split('-');
                                positionPart.Decertificatedate = new DateTime(Int32.Parse(dates[0]), Int32.Parse(dates[1]), Int32.Parse(dates[2]));
                            }
                            break;
                        case "decree":
                            if (pair[1].Length > 0)
                            {
                                positionPart.Decree = Boolean.Parse(pair[1]);
                            }
                            break;
                        case "decreenumber":
                            if (pair[1].Length > 0)
                            {
                                positionPart.Decreenumber = pair[1];
                            }
                            break;
                        case "decreedate":
                            if (pair[1].Length > 0)
                            {
                                string[] dates = pair[1].Split('-');
                                positionPart.Decreedate = new DateTime(Int32.Parse(dates[0]), Int32.Parse(dates[1]), Int32.Parse(dates[2]));
                            }
                            break;
                        default:
                            break;
                    }
                }
                positionParts.Add(positionPart);
            }
            positionManagement.PositionParts = positionParts;


            foreach(PositionPart positionPart in positionParts)
            {
                if (positionPart.Custom)
                {
                    positionManagement.Datecustom = 1;
                    positionManagement.Dateactive = positionPart.Customdate;
                }
                
            }


            List<int> equalPositions = null;
            if (positionManagement.Quantity > 1)
            {
                equalPositions = GetEqualPositions(positionManagement, user); // Если больше одной должности
            } else
            {
                equalPositions = new List<int>();
                equalPositions.Add(positionManagement.Id.GetValueOrDefault());
            }
            
            

            int limit = 1;
            if (positionManagement.Quantity > 0.99)
            {
                limit = (int)positionManagement.Quantity;
            } else
            {

            }

            Dictionary<int, int> decreeoperationsids = new Dictionary<int, int>();
            foreach (int id in equalPositions)
            {
                if (limit > 0)
                {
                    Decreeoperation operation = new Decreeoperation();
                    operation.Decree = decree.Id;
                    operation.Deleted = 1;
                    operation.Subject = id;
                    if (positionManagement.Datecustom > 0)
                    {
                        operation.Dateactive = positionManagement.Dateactive;
                        operation.Datecustom = 1;
                    }

                    context.Decreeoperation.Add(operation);
                    context.SaveChanges();
                    
                    limit--;
                    decreeoperationsids.Add(Positions.First(p => p.Id == id).Origin, operation.Id);
                } else
                {
                    break;
                }
            }
            UpdateDecreeoperationsLocal();
            UpdatePositionsLocal();
            UpdatePositionmrdsLocal();
            return decreeoperationsids;
        }

        public void RemovePosition(Position position, User user, bool datecustom, DateTime dateactive)
        {
            Decree decree = GetDecreeByUser(user);

            List<PositionPart> positionParts = new List<PositionPart>();

            Decreeoperation operation = new Decreeoperation();
            operation.Decree = decree.Id;
            operation.Deleted = 1;
            operation.Subject = position.Id;
            if (datecustom)
            {
                operation.Dateactive = dateactive;
                operation.Datecustom = 1;
            }

            context.Decreeoperation.Add(operation);
            context.SaveChanges();
            UpdateDecreeoperationsLocal();
            UpdatePositionsLocal();
            UpdatePositionmrdsLocal();
        }

        /**
         * Cancel adding new position while working with decree
         */
        public void RemovePositiondecree(PositionManagement positionManagement, User user)
        {
            Decreeoperation contextDecreeoperation = DecreeoperationsLocal().Values.FirstOrDefault(d => d.Subject == positionManagement.Id && Decrees.First(decree => decree.Id == d.Decree).Signed == 0);
            if (contextDecreeoperation != null && contextDecreeoperation.Deleted > 0)
            {
                context.Decreeoperation.Remove(contextDecreeoperation);
                context.SaveChanges();
                UpdateDecreeoperationsLocal();
            } else 
            //return;
            if (contextDecreeoperation != null)
            {
                Position contextPosition = Positions.First(p => p.Id == contextDecreeoperation.Subject);
                /**
                 * Remove curation marks.
                 */
                if (contextPosition.Curator > 0 && contextPosition.Curatorlist.Length > 0)
                {
                    int[] curated = contextPosition.Curatorlist.Split(',').Select(int.Parse).ToArray();
                    foreach (int i in curated)
                    {
                        Structure structure = context.Structure.FirstOrDefault(s => s.Id == i);
                        if (structure != null)
                        {
                            if (structure.Curator == contextPosition.Id)
                            {
                                structure.Curator = 0;
                            }
                        }
                        
                    }
                    context.SaveChanges();
                }

                /**
                 * Remove heading mark.
                 */
                if (contextPosition.Head > 0 && contextPosition.Headid > 0)
                {
                    Structure structure = context.Structure.FirstOrDefault(s => s.Id == contextPosition.Headid);
                    if (structure != null)
                    {
                        if (structure.Head == contextPosition.Id)
                        {
                            structure.Head = 0;
                        }
                        context.SaveChanges();
                    }
                    
                }

                context.Position.Remove(contextPosition);
                context.SaveChanges();


                context.Decreeoperation.Remove(contextDecreeoperation);
                context.SaveChanges();
                UpdateDecreeoperationsLocal();
                UpdatePositionsLocal();
                UpdatePositionmrdsLocal();
            }
            
        }

        /**
         * Editing means we remove old positions and adding new positions, also adding history
         */
        public void EditPosition(PositionManagement positionManagement, User user)
        {
            // Если стоит режим неизменения приказа, то изменения будут происходить прямым редактированием
            if (positionManagement.Nodecree > 0) 
            {
                EditPositiondecree(positionManagement, user);
                return;
            }
            Decree decree = GetDecreeByUser(user);
            Dictionary<int, int> decreeoperationsidsremove = RemovePosition(positionManagement, user);
            if (decreeoperationsidsremove.Count > 0)
            {
                int structure = GetStructureByPositionID(decreeoperationsidsremove.First().Key).Id;
                if (positionManagement.Quantity > 1)
                {
                    positionManagement.Quantity = 1;
                }
                if (positionManagement.Quantity < 1)
                {
                    positionManagement.Part = 1;
                } else
                {
                    positionManagement.Part = 0;
                }

                positionManagement.Department = structure;
                //int department = positionManagement.Department.GetValueOrDefault(); - department doesn't exist

                foreach (KeyValuePair<int, int> entry in decreeoperationsidsremove)
                {
                    Dictionary<int, int> decreeoperationsidsadd = AddPosition(positionManagement, user, entry.Key);
                    Positionhistory positionhistory = new Positionhistory();
                    positionhistory.Position = decreeoperationsidsadd.First().Key; 
                    positionhistory.Previous = entry.Key;
                    positionhistory.Decreeoperation = entry.Value;
                    positionhistory.Decree = decree.Id;
                    context.Positionhistory.Add(positionhistory);
                    context.SaveChanges();
                }
            }
            UpdatePositionsLocal();
            UpdatePositionmrdsLocal();
        }

        /**
         * Edit position while working with decree.
         */
        public void EditPositiondecree(PositionManagement positionManagement, User user)
        {
            Decreeoperation contextDecreeoperation = Decreeoperations.FirstOrDefault(d => d.Subject == positionManagement.Id);
            if (contextDecreeoperation != null)
            {
                Position position = Positions.First(p => p.Id == contextDecreeoperation.Subject);

                Dictionary<int, int> decreeoperationsids = new Dictionary<int, int>();
                List<PositionPart> positionParts = new List<PositionPart>();
                string[] parts = positionManagement.PositionsCode.Split(';');
                foreach (string part in parts)
                {
                    PositionPart positionPart = new PositionPart();
                    string[] elements = part.Split('&');
                    foreach (string element in elements)
                    {
                        string[] pair = element.Split("=");
                        switch (pair[0])
                        {
                            case "id":
                                if (pair[1].Length > 0)
                                {
                                    positionPart.Id = Int32.Parse(pair[1]);
                                }
                                break;
                            case "civil":
                                if (pair[1].Length > 0)
                                {
                                    positionPart.Civil = Boolean.Parse(pair[1]);
                                }
                                break;
                            case "civildatelimit":
                                if (pair[1].Length > 0)
                                {
                                    positionPart.Civildatelimit = Boolean.Parse(pair[1]);
                                }
                                break;
                            case "civildate":
                                if (pair[1].Length > 0)
                                {
                                    string[] dates = pair[1].Split('-');
                                    positionPart.Civildate = new DateTime(Int32.Parse(dates[0]), Int32.Parse(dates[1]), Int32.Parse(dates[2]));
                                }
                                break;
                            case "custom":
                                if (pair[1].Length > 0)
                                {
                                    positionPart.Custom = Boolean.Parse(pair[1]);
                                }
                                break;
                            case "customdate":
                                if (pair[1].Length > 0)
                                {
                                    string[] dates = pair[1].Split('-');
                                    positionPart.Customdate = new DateTime(Int32.Parse(dates[0]), Int32.Parse(dates[1]), Int32.Parse(dates[2]));
                                }
                                break;
                            case "decertificate":
                                if (pair[1].Length > 0)
                                {
                                    positionPart.Decertificate = Boolean.Parse(pair[1]);
                                }
                                break;
                            case "decertificatedate":
                                if (pair[1].Length > 0)
                                {
                                    string[] dates = pair[1].Split('-');
                                    positionPart.Decertificatedate = new DateTime(Int32.Parse(dates[0]), Int32.Parse(dates[1]), Int32.Parse(dates[2]));
                                }
                                break;
                            case "decree":
                                if (pair[1].Length > 0)
                                {
                                    positionPart.Decree = Boolean.Parse(pair[1]);
                                }
                                break;
                            case "decreenumber":
                                if (pair[1].Length > 0)
                                {
                                    positionPart.Decreenumber = pair[1];
                                }
                                break;
                            case "decreedate":
                                if (pair[1].Length > 0)
                                {
                                    string[] dates = pair[1].Split('-');
                                    positionPart.Decreedate = new DateTime(Int32.Parse(dates[0]), Int32.Parse(dates[1]), Int32.Parse(dates[2]));
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    positionParts.Add(positionPart);
                }
                positionManagement.PositionParts = positionParts;



                foreach (PositionPart part in positionManagement.PositionParts)
                {
                    position.Positiontype = positionManagement.Positiontype;
                    position.Positioncategory = positionManagement.Positioncategory;
                    position.Cap = positionManagement.RankCap;
                    position.Sourceoffinancing = positionManagement.Sof.GetValueOrDefault(1);
                    position.Replacedbycivil = 0;
                    position.Replacedbycivilpositioncategory = positionManagement.Replacedbycivilpositioncategory;
                    position.Opchs = positionManagement.Opchs;
                    bool fullCivilPosition = false;
                    Positioncategory positionCategory = PositioncategoriesLocal().Values.First(pc => pc.Id == position.Positioncategory);
                    /**
                     * If civil position or not
                     */
                    if (positionCategory.Civil > 0)
                    {
                        fullCivilPosition = true;
                    }

                    position.Replacedbycivilpositiontype = positionManagement.Replacedbycivilpositiontype;
                    if (part.Civil)
                    {
                        position.Replacedbycivil = 1;
                        position.Civilrankhigh = positionManagement.Civilrankhigh;
                        position.Civilranklow = positionManagement.Civilranklow;
                        position.Replacedbycivildatelimit = 0;
                        if (part.Civildatelimit)
                        {
                            position.Replacedbycivildatelimit = 1;
                            position.Replacedbycivildate = part.Civildate;
                        }
                        if (part.Decree)
                        {
                            position.Civildecree = 1;
                            position.Civildecreenumber = part.Decreenumber;
                            position.Civildecreedate = part.Decreedate;
                        }
                    }
                    else
                    {
                        position.Replacedbycivil = 0;
                        position.Civilrankhigh = 0;
                        position.Civilranklow = 0;
                        position.Replacedbycivildatelimit = 0;
                        position.Civildecree = 0;
                    }

                    /**
                     * Contains classes.
                     */
                    if (fullCivilPosition && positionCategory.Classcap > 0)
                    {
                        position.Civilrankhigh = positionManagement.Civilrankhigh;
                        position.Civilranklow = positionManagement.Civilranklow;
                    }

                    position.Notice = positionManagement.Notice;
                    position.Decertificate = 0;

                    if (part.Decertificate)
                    {
                        position.Decertificate = 1;
                        position.Decertificatedate = part.Decertificatedate;
                    } else
                    {
                        position.Decertificate = 0;
                    }
                    if (position.Civildecreenumber == null)
                    {
                        position.Civildecreenumber = "";
                    }

                    /**
                     * If partly
                     */
                    if (positionManagement.Quantity < 1)
                    {
                        position.Part = 1;
                        position.Partval = positionManagement.Quantity;
                    }

                    /**
                     * Для прохождения службы
                     */
                    position.Subject1 = positionManagement.Subject1;
                    position.Subject2 = positionManagement.Subject2;
                    position.Subject3 = positionManagement.Subject3;
                    position.Subject4 = positionManagement.Subject4;
                    position.Subject5 = positionManagement.Subject5;
                    position.Subject6 = positionManagement.Subject6;
                    position.Subject7 = positionManagement.Subject7;
                    position.Subject8 = positionManagement.Subject8;
                    position.Subject9 = positionManagement.Subject9;
                    position.Subject10 = positionManagement.Subject10;
                    position.Subject11 = positionManagement.Subject11;
                    position.Subject12 = positionManagement.Subject12;
                    position.Subject13 = positionManagement.Subject13;
                    position.Subject14 = positionManagement.Subject14;
                    position.Subject15 = positionManagement.Subject15;
                    position.Subject16 = positionManagement.Subject16;
                    position.Subject17 = positionManagement.Subject17;
                    position.Subject18 = positionManagement.Subject18;
                    position.Subject19 = positionManagement.Subject19;
                    position.Subject20 = positionManagement.Subject20;

                    context.SaveChanges();

                    /**
                     * Delete old position mrds.
                     */
                    IEnumerable<Positionmrd> positionmrds = Positionmrds.Where(p => p.Position == position.Id);
                    if (positionmrds.Count() > 0)
                    {
                        foreach (Positionmrd positionmrd in positionmrds)
                        {
                            context.Positionmrd.Remove(positionmrd);
                        }
                        context.SaveChanges();
                    }

                    /**
                     * Mark it has altranks.
                     */
                    if (positionManagement.Altrankconditiongroup > 0)
                    {
                        position.Altrank = 1;
                    }
                    else
                    {
                        position.Altrank = 0;
                    }

                    /**
                     * For curation
                     */
                    position.Curator = 0;
                    int[] structureIds = null;
                    if (positionManagement.Curator > 0)
                    {
                        position.Curator = 1;
                        position.Curatorlist = positionManagement.Curatorlist;
                        structureIds = position.Curatorlist.Split(',').Select(int.Parse).ToArray();
                    }

                    /**
                     * For heading
                     */
                    position.Head = 0;
                    if (positionManagement.Head > 0)
                    {
                        position.Head = 1;
                        position.Headid = positionManagement.Headid;
                    }

                    /**
                     * For curation 2
                     */
                    if (structureIds != null)
                    {
                        foreach (int i in structureIds)
                        {
                            Structure structure = context.Structure.FirstOrDefault(s => s.Id == i);
                            if (structure != null)
                            {
                                structure.Curator = position.Id;
                            }
                            
                        }
                        context.SaveChanges();
                    }

                    /**
                     * For heading 2
                     */
                    if (position.Head > 0 && position.Headid > 0)
                    {
                        Structure structure = context.Structure.First(s => s.Id == position.Headid);
                        structure.Head = position.Id;
                        context.SaveChanges();
                    }


                    /**
                     * Adding MRD array now
                     */
                    if (!String.IsNullOrEmpty(positionManagement.Mrd))
                    {
                        int[] mrds = Array.ConvertAll(positionManagement.Mrd.Split(','), s => int.Parse(s));
                        int positionid = position.Id;
                        foreach (int mrd in mrds)
                        {
                            Positionmrd positionmrd = new Positionmrd();
                            positionmrd.Mrd = mrd;
                            positionmrd.Position = positionid;
                            context.Positionmrd.Add(positionmrd);
                        }
                        context.SaveChanges();
                    }

                    /**
                     * Delete old altranks
                     */
                    IEnumerable<Altrank> altranks = Altranks.Where(a => a.Position == position.Id);
                    if (altranks.Count() > 0)
                    {
                        foreach(Altrank altrank in altranks)
                        {
                            context.Altrank.Remove(altrank);
                        }
                        context.SaveChanges();
                    }

                    /**
                     * If altranks enabled.
                     */
                    if (positionManagement.Altrankconditiongroup > 0)
                    {
                        foreach (string altrankStr in positionManagement.Altranks.Split(";"))
                        {
                            Altrank altrank = new Altrank();
                            string[] pair = altrankStr.Split("=");
                            altrank.Position = position.Id;
                            altrank.Altrankcondition = Int32.Parse(pair[0]);
                            altrank.Rank = Int32.Parse(pair[1]);
                            context.Altrank.Add(altrank);
                        }
                        context.SaveChanges();
                    }

                    if (part.Custom)
                    {

                        contextDecreeoperation.Dateactive = part.Customdate;
                        contextDecreeoperation.Datecustom = 1;
                    } else
                    {
                        contextDecreeoperation.Datecustom = 0;
                    }

                    context.SaveChanges();
                }
                context.SaveChanges();
                UpdateDecreeoperationsLocal();
                UpdatePositionsLocal();
                UpdatePositionmrdsLocal();
            }

        }

        /**
         * Return ids of positions that are equal/duplicates (same name, status)
         */
        public List<int> GetEqualPositions(PositionManagement positionManagement, User user)
        {
            List<int> list = new List<int>();
            Position position = PositionsLocal().GetValueOrDefault(positionManagement.Id.GetValueOrDefault(0));
            DecreeOperationsRequest dor = new DecreeOperationsRequest();
            dor.SubjectID = position.Id;
            dor.RequestedDate = user.Date.GetValueOrDefault();
            dor.Detailed = 0;

            List<DecreeoperationManagement> doms = RequestDecreeOperations(dor);
            // 0 - no purpose, 1 - no purpose not signed, 
            // 2 - will create subject in future, 3 - will create subject in future not signed,
            // 4 - already created subject, 5 - already created subject not signed, 6 - will delete subject in future
            // 7 - will delete subject in future not signed, 8 - already renamed subject, 9 - already renamed subject 
            // not signed, 10 - will rename subject, 11 - will rename subject not signed,
            // 12 - deleted, 13 - deleted not signed,
            int tag = 0;
            foreach (DecreeoperationManagement dom in doms)
            {
                if (tag == 0 && dom.MetaPurposeForSubject == 4)
                {
                    tag = 4;
                } else if (dom.MetaPurposeForSubject != Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE && dom.MetaPurposeForSubject != Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE_NOT_SIGNED
                    && dom.MetaPurposeForSubject != Keys.DECREE_OPERATION_META_PURPOSE_CREATED_SUBJECT && dom.MetaPurposeForSubject != Keys.DECREE_OPERATION_META_PURPOSE_DELETED_SUBJECT)
                {
                    tag = dom.MetaPurposeForSubject;
                }
            }
            if (tag != 0) // TAG1
            {
                List<Position> positions = GetPositions(position.Structure, user.Date.GetValueOrDefault()).ToList();
                foreach(Position pos in positions)
                {
                    DecreeOperationsRequest dorLocal = new DecreeOperationsRequest();
                    dorLocal.SubjectID = pos.Id;
                    dorLocal.RequestedDate = user.Date.GetValueOrDefault();
                    dorLocal.Detailed = 0;
                    List<DecreeoperationManagement> domsLocal = RequestDecreeOperations(dorLocal);
                    domsLocal = RemoveObsoleteDecreeoperations(domsLocal);
                    domsLocal = RemoveObsoleteDecreeoperationsForEqualPositions(domsLocal);
                    //if (domsLocal.FirstOrDefault(dl => dl.MetaPurposeForSubject == tag) != null && pos.Positiontype == position.Positiontype)
                    if (domsLocal.FirstOrDefault(dl => dl.MetaPurposeForSubject == tag) != null && pos.Positiontype == position.Positiontype && pos.Positioncategory == position.Positioncategory)
                    {
                        list.Add(pos.Id);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Для всех должностей с данным типом должности мы меняет составные название должностей для документов. 
        /// То есть для всех главных специалистов ставим составную должность "главный" "специалист"
        /// </summary>
        /// <param name="positionManagement"></param>
        /// <param name="user"></param>
        public void UpdatePositionsByPositiontypes(PositionManagement positionManagement, User user)
        {
            IQueryable<Position> positions = context.Position.Where(p => p.Positiontype == positionManagement.Positiontype);
            foreach (Position position in positions)
            {
                position.Subject1 = positionManagement.Subject1;
                position.Subject2 = positionManagement.Subject2;
                position.Subject3 = positionManagement.Subject3;
                position.Subject4 = positionManagement.Subject4;
                position.Subject5 = positionManagement.Subject5;
                position.Subject6 = positionManagement.Subject6;
                position.Subject7 = positionManagement.Subject7;
                position.Subject8 = positionManagement.Subject8;
                position.Subject9 = positionManagement.Subject9;
                position.Subject10 = positionManagement.Subject10;
                position.Subject11 = positionManagement.Subject11;
                position.Subject12 = positionManagement.Subject12;
                position.Subject13 = positionManagement.Subject13;
                position.Subject14 = positionManagement.Subject14;
                position.Subject15 = positionManagement.Subject15;
                position.Subject16 = positionManagement.Subject16;
                position.Subject17 = positionManagement.Subject17;
                position.Subject18 = positionManagement.Subject18;
                position.Subject19 = positionManagement.Subject19;
                position.Subject20 = positionManagement.Subject20;
            }

            Positiontype contextPositiontype = Positiontypes.FirstOrDefault(p => p.Id == positionManagement.Positiontype);
            if (contextPositiontype != null)
            {
                contextPositiontype.Subject1 = positionManagement.Subject1;
                contextPositiontype.Subject2 = positionManagement.Subject2;
                contextPositiontype.Subject3 = positionManagement.Subject3;
                contextPositiontype.Subject4 = positionManagement.Subject4;
                contextPositiontype.Subject5 = positionManagement.Subject5;
                contextPositiontype.Subject6 = positionManagement.Subject6;
                contextPositiontype.Subject7 = positionManagement.Subject7;
                contextPositiontype.Subject8 = positionManagement.Subject8;
                contextPositiontype.Subject9 = positionManagement.Subject9;
                contextPositiontype.Subject10 = positionManagement.Subject10;
                contextPositiontype.Subject11 = positionManagement.Subject11;
                contextPositiontype.Subject12 = positionManagement.Subject12;
                contextPositiontype.Subject13 = positionManagement.Subject13;
                contextPositiontype.Subject14 = positionManagement.Subject14;
                contextPositiontype.Subject15 = positionManagement.Subject15;
                contextPositiontype.Subject16 = positionManagement.Subject16;
                contextPositiontype.Subject17 = positionManagement.Subject17;
                contextPositiontype.Subject18 = positionManagement.Subject18;
                contextPositiontype.Subject19 = positionManagement.Subject19;
                contextPositiontype.Subject20 = positionManagement.Subject20;
            }

            SaveChanges();
            UpdatePositionsLocal();
            UpdatePositiontypesLocal();
        }

        /// <summary>
        /// Добавляет должности наименования в различных падежах для документов.
        /// </summary>
        /// <param name="position"></param>
        public void AddPositionNames(Position position)
        {
            
        }

        /**
         * Get current head of structure if available
         */
        public Position GetHead(int structureid, DateTime date)
        {
            if (structureid == 0)
            {
                return null;
            }
            if (structureid < 0)
            {
                structureid = -structureid;
            }
            Structure structure = null;
            if (StructuresLocal().ContainsKey(structureid))
            {
                structure = StructuresLocal()[structureid];
            }
            Structure actualStructure = GetActualStructureInfo(structureid, date);
            Structure originalStructure = GetOriginalStructure(structureid);
            //Structure structure = Structures.FirstOrDefault(s => s.Id == structureid);
            if (structure == null && actualStructure == null && originalStructure == null)
            {
                return null;
            }
            int idStructure = 0;
            int idOriginal = 0;
            int idActual = 0;

            int structureId = 0;
            int originalId = 0;
            int actualId = 0;
            if (structure != null)
            {
                idStructure = structure.Head;
                structureId = structure.Id;

            }
            if (originalStructure != null)
            {
                idOriginal = originalStructure.Head;
                originalId = originalStructure.Id;
            }
            if (actualStructure != null)
            {
                idActual = actualStructure.Head;
                actualId = actualStructure.Id;
            }
            
            
            
            if (idStructure > 0)
            {
                Position position = PositionsLocal().GetValueOrDefault(idStructure);
                if (position != null && !IsSignedAndDeleted(position, date))
                {
                    if (position.Head > 0 && (position.Headid == structureid || position.Headid == originalId || position.Headid == actualId) )
                    {
                        return position;
                    }
                }
            }

            if (idOriginal > 0)
            {
                Position position = PositionsLocal().GetValueOrDefault(idOriginal);
                if (position != null && !IsSignedAndDeleted(position, date))
                {
                    if (position.Head > 0 && (position.Headid == structureid || position.Headid == originalId || position.Headid == actualId))
                    {
                        return position;
                    }
                }
            }

            if (idActual > 0)
            {
                Position position = PositionsLocal().GetValueOrDefault(idActual);
                if (position != null && !IsSignedAndDeleted(position, date))
                {
                    if (position.Head > 0 && (position.Headid == structureid || position.Headid == originalId || position.Headid == actualId))
                    {
                        return position;
                    }
                }
            }
            return null;
        }

        public bool IsHeading(int positionid, int structureid, DateTime date)
        {
            Position position = PositionsLocal().GetValueOrDefault(positionid);
            if (position == null || position.Head == 0 || position.Headid == 0 || IsSignedAndDeleted(position, date))
            {
                return false;
            }
            Structure structure = StructuresLocal().GetValue(structureid);
            Structure originalStructure = null;
            if (structure != null)
            {
                originalStructure = GetOriginalStructure(structure);
            }
            Structure positionStructure = StructuresLocal().GetValue(position.Headid);
            Structure positionStructureOriginal = null;
            if (positionStructure != null)
            {
                positionStructureOriginal = GetOriginalStructure(positionStructure);
            }
            if (originalStructure != null && !IsSignedAndDeleted(originalStructure, date) && positionStructureOriginal != null && originalStructure.Id == positionStructureOriginal.Id)
            {
                return true;
            }
            return false;
        }

        /**
         * Get current head of structure if available
         */
        public Position GetCuration(int structureid, DateTime date)
        {
            if (structureid == 0)
            {
                return null;
            }
            if (structureid < 0)
            {
                structureid = -structureid;
            }
            Structure structure = null;
            if (StructuresLocal().ContainsKey(structureid))
            {
                structure = StructuresLocal()[structureid];
            }
            Structure actualStructure = GetActualStructureInfo(structureid, date);
            Structure originalStructure = GetOriginalStructure(structureid);
            //Structure structure = Structures.FirstOrDefault(s => s.Id == structureid);
            if (structure == null && actualStructure == null && originalStructure == null)
            {
                return null;
            }
            int idStructure = 0;
            int idOriginal = 0;
            int idActual = 0;

            int structureId = 0;
            int originalId = 0;
            int actualId = 0;
            if (structure != null)
            {
                idStructure = structure.Curator;
                structureId = structure.Id;

            }
            if (originalStructure != null)
            {
                idOriginal = originalStructure.Curator;
                originalId = originalStructure.Id;
            }
            if (actualStructure != null)
            {
                idActual = actualStructure.Curator;
                actualId = actualStructure.Id;
            }

            if (idStructure > 0)
            {
                Position position = PositionsLocal().GetValueOrDefault(idStructure);
                if (position != null && !IsSignedAndDeleted(position, date))
                {
                    string[] curatorlistStr = position.Curatorlist.Split(',');
                    int[] curatorlist = Array.ConvertAll(curatorlistStr, int.Parse);
                    if (position.Curator > 0 && (curatorlist.Contains(structureid) || curatorlist.Contains(originalId) || curatorlist.Contains(actualId)))
                    {
                        return position;
                    }
                }
            }

            if (idOriginal > 0)
            {
                Position position = PositionsLocal().GetValueOrDefault(idOriginal);
                if (position != null && !IsSignedAndDeleted(position, date))
                {
                    string[] curatorlistStr = position.Curatorlist.Split(',');
                    int[] curatorlist = Array.ConvertAll(curatorlistStr, int.Parse);
                    if (position.Curator > 0 && (curatorlist.Contains(structureid) || curatorlist.Contains(originalId) || curatorlist.Contains(actualId)))
                    {
                        return position;
                    }
                }
            }

            if (idActual > 0)
            {
                Position position = PositionsLocal().GetValueOrDefault(idActual);
                if (position != null && !IsSignedAndDeleted(position, date))
                {
                    string[] curatorlistStr = position.Curatorlist.Split(',');
                    int[] curatorlist = Array.ConvertAll(curatorlistStr, int.Parse);
                    if (position.Curator > 0 && (curatorlist.Contains(structureid) || curatorlist.Contains(originalId) || curatorlist.Contains(actualId)))
                    {
                        return position;
                    }
                }
            }
            return null;
        }

        public bool IsCurating(int positionid, int structureid, DateTime date)
        {
            Position position = PositionsLocal().GetValueOrDefault(positionid);
            if (position == null || position.Curator == 0 || position.Curatorlist.Length == 0 || IsSignedAndDeleted(position, date))
            {
                return false;
            }
            
            Structure structure = StructuresLocal().GetValue(structureid);
            Structure originalStructure = null;
            if (structure != null)
            {
                originalStructure = GetOriginalStructure(structure);
            }

            List<Structure> positionStructures = new List<Structure>();
            List<Structure> positionStructuresOriginal = new List<Structure>();
            List<int> positionStructuresOriginalIds = new List<int>();
            int[] structureids = Array.ConvertAll(position.Curatorlist.Split(','), int.Parse);
            foreach (int curatedid in structureids)
            {
                Structure positionStructure = StructuresLocal().GetValue(curatedid);
                Structure positionStructureOriginal = null;
                if (positionStructure != null)
                {
                    positionStructureOriginal = GetOriginalStructure(positionStructure);
                    positionStructuresOriginalIds.Add(positionStructureOriginal.Id);
                }
            }


            
            if (originalStructure != null && !IsSignedAndDeleted(originalStructure, date) && positionStructuresOriginalIds.Count > 0 && positionStructuresOriginalIds.Contains(originalStructure.Id))
            {
                return true;
            }
            return false;
        }

        /**
         * Remove decree operations which were replaced by newer ones
         */
        public List<DecreeoperationManagement> RemoveObsoleteDecreeoperations(List<DecreeoperationManagement> doms)
        {
            bool alreadyDeleted = false;
            if (doms.FirstOrDefault(dom => dom.MetaPurposeForSubject == Keys.DECREE_OPERATION_META_PURPOSE_DELETED_SUBJECT) != null)
            {
                alreadyDeleted = true;
            }
            if (alreadyDeleted)
            {
                doms = doms.Where(dom => dom.MetaPurposeForSubject == Keys.DECREE_OPERATION_META_PURPOSE_DELETED_SUBJECT).ToList();
            }
            return doms;
        }

        /**
         * Remove obsolete decreeoperations as a part of equal positions function 
         */
        public List<DecreeoperationManagement> RemoveObsoleteDecreeoperationsForEqualPositions(List<DecreeoperationManagement> doms)
        {
            DecreeoperationManagement domLast = null;
            foreach (DecreeoperationManagement dom in doms)
            {
                if (domLast == null)
                {
                    domLast = dom;
                }
                if (dom.MetaDateActive > domLast.MetaDateActive)
                {
                    domLast = dom;
                }
            }
            doms = doms.Where(d => d.Id == domLast.Id).ToList();
            return doms;
        }

        public void UpdateUser(User user)
        {
            User contextUser = Users.First(u => u.Id == user.Id);
            Rights contextRights = contextUser.GetRights(this);
            contextUser.Admin = user.Admin;
            contextUser.Name = user.Name;
            contextUser.Structure = user.Structure;
            contextUser.Firstname = user.Firstname;
            contextUser.Surname = user.Surname;
            contextUser.Patronymic = user.Patronymic;
            contextUser.Positiontype = user.Positiontype;

            if (contextRights != null)
            {
                ChangeRights(user, user.Rights);
                // Раньше права доступа хранились в user напрямую, а теперь в rights, привязанному к этому пользователю
                // Этот код неоходим для методов, которые используют старые привязки к полям в user.
                contextUser.Admin = user.Rights.Admin;
                contextUser.Masterpersonneleditor = user.Rights.Peopleorgreadall;
                contextUser.Structureeditor = user.Rights.Orgedit;
                contextUser.Structureread = user.Rights.Orgread;
                contextUser.Personneleditor = user.Rights.Peopleedit;
                contextUser.Personnelread = user.Rights.Peopleread;
            }

            context.SaveChanges();
            UpdateUsersLocal();
            UpdateRightsLocal();

            //User contextUser = Users.First(u => u.Id == user.Id);
            //contextUser.Admin = user.Admin;
            //contextUser.Name = user.Name;
            //contextUser.Structure = user.Structure;
            //contextUser.Masterpersonneleditor = user.Masterpersonneleditor;
            //contextUser.Structureeditor = user.Structureeditor;
            //contextUser.Structureread = user.Structureread;
            //contextUser.Personneleditor = user.Personneleditor;
            //contextUser.Personnelread = user.Personnelread;
            //contextUser.Firstname = user.Firstname;
            //contextUser.Surname = user.Surname;
            //contextUser.Patronymic = user.Patronymic;
            //contextUser.Positiontype = user.Positiontype;

            ////if (contextUser.Fullmode == 1) // Для режима структурной работы поддерживаем опцию включенной.
            ////{
            ////    contextUser.Sidebardisplay = 1;
            ////}

            //context.SaveChanges();
            //UpdateUsersLocal();
        }

        /**
         * True if success, false otherwise
         */
        public bool SwitchUser(User user)
        {
            User contextUser = Users.First(u => u.Id == user.Id);
            if (contextUser.Mode == 0)
            {
                if (contextUser.Personnelread > 0 || contextUser.Admin.GetValueOrDefault() > 0 || contextUser.Masterpersonneleditor.GetValueOrDefault() > 0)
                {
                    contextUser.Mode = 1;
                    context.SaveChanges();
                    UpdateUsersLocal();
                    return true;
                } else
                {
                    return false;
                }
            } else
            {
                if (contextUser.Structureread > 0 || contextUser.Admin.GetValueOrDefault() > 0 || contextUser.Masterpersonneleditor.GetValueOrDefault() > 0)
                {
                    contextUser.Mode = 0;
                    context.SaveChanges();
                    UpdateUsersLocal();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /**
         * True if success, false otherwise
         */
        public bool OrgUser(User user)
        {
            User contextUser = Users.First(u => u.Id == user.Id);
            if (contextUser.Structureread > 0 || contextUser.Admin.GetValueOrDefault() > 0 || contextUser.Masterpersonneleditor.GetValueOrDefault() > 0)
            {
                contextUser.Mode = 0;
                context.SaveChanges();
                UpdateUsersLocal();
                return true;
            }
            else
            {
                return false;
            }
        }

        /**
         * True if success, false otherwise
         */
        public bool PeopleUser(User user)
        {
            User contextUser = Users.First(u => u.Id == user.Id);
            if (contextUser.Personnelread > 0 || contextUser.Admin.GetValueOrDefault() > 0 || contextUser.Masterpersonneleditor.GetValueOrDefault() > 0)
            {
                contextUser.Mode = 1;
                context.SaveChanges();
                UpdateUsersLocal();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ChangeFullmode(User user, int fullmode)
        {
            if (fullmode == 0)
            {

            } else if (fullmode == 1)
            {

            } else if (fullmode == 2)
            {

            } else if (fullmode == 3)
            {

            } else if (fullmode == 4)
            {

            }
        }

        public void AddRank(Rank rank)
        {
            context.Rank.Add(rank);
            context.SaveChanges();
            if (rank.Order.GetValueOrDefault(0) == 0)
            {
                rank.Order = rank.Id;
                context.SaveChanges();
            }
            UpdateRanksLocal();
        }

        public void UpdateRank(Rank rank)
        {
            Rank contextRank = Ranks.FirstOrDefault(r => r.Id == rank.Id);
            if (contextRank == null)
            {
                return;
            }
            contextRank.Name = rank.Name;
            contextRank.Positioncategory = rank.Positioncategory;
            context.SaveChanges();
            UpdateRanksLocal();
        }

        public void UpdateRankOrder(Rank rank)
        {
            if (rank.Order.GetValueOrDefault() < 1)
            {
                return;
            }
            if (rank.Order.GetValueOrDefault() > Ranks.Count())
            {
                return;
            }
            Rank contextRank = Ranks.First(r => r.Id == rank.Id);
            Rank rankSwap = Ranks.First(r => r.Order == rank.Order);
            int orderSwap = rankSwap.Order.GetValueOrDefault();
            rankSwap.Order = contextRank.Order;
            contextRank.Order = orderSwap;

            context.SaveChanges();
            UpdateRanksLocal();
        }

        public void AddSourceOfFinancing(Sourceoffinancing sourceoffinancing)
        {
            context.Sourceoffinancing.Add(sourceoffinancing);
            context.SaveChanges();
            UpdateSourceoffinancingsLocal();
        }

        /**
         * Returns true if successfully added. False if already occupied
         */
        public bool AddPositiontype(Positiontype positiontype)
        {
            string simplifiedName = positiontype.Name.ToLower().Trim();
            Positiontype occupiedPositiontype = Positiontypes.FirstOrDefault(pt => pt.Name.ToLower().Trim().Equals(simplifiedName));
            if (occupiedPositiontype == null)
            {
                if (positiontype.Name2 == null || positiontype.Name2.Length == 0)
                {
                    positiontype.Name2 = positiontype.Name;
                }
                if (positiontype.Name3 == null || positiontype.Name3.Length == 0)
                {
                    positiontype.Name3 = positiontype.Name;
                }
                context.Positiontype.Add(positiontype);
                context.SaveChanges();
                UpdatePositiontypesLocal();
                return true;
            } else
            {
                return false;
            }
            
        }

        public void UpdatePositiontype(Positiontype positiontype)
        {
            Positiontype contextPositiontype = context.Positiontype.First(p => p.Id == positiontype.Id);
            contextPositiontype.Name = positiontype.Name;
            contextPositiontype.Name2 = positiontype.Name2;
            contextPositiontype.Name3 = positiontype.Name3;
            contextPositiontype.Nameshort = positiontype.Nameshort;
            contextPositiontype.Priority = positiontype.Priority;
            context.SaveChanges();
            UpdatePositiontypesLocal();
        }

        public bool AddSubject(Subject subject)
        {
            string simplifiedName = subject.Name1.ToLower().Trim();
            Subject occupiedSubject = Subjects.FirstOrDefault(pt => pt.Name1.ToLower().Trim().Equals(simplifiedName) && pt.Category == subject.Category);
            if (occupiedSubject == null)
            {
                //if (positiontype.Name2 == null || positiontype.Name2.Length == 0)
                //{
                //    positiontype.Name2 = positiontype.Name;
                //}
                //if (positiontype.Name3 == null || positiontype.Name3.Length == 0)
                //{
                //    positiontype.Name3 = positiontype.Name;
                //}
                context.Subject.Add(subject);
                context.SaveChanges();
                UpdateSubjectsLocal();
                return true;
                
            }
            else
            {
                return false;
            }

        }

        public void UpdateSubject(Subject subject)
        {
            Subject contextSubject = context.Subject.First(p => p.Id == subject.Id);
            contextSubject.Name = subject.Name;
            contextSubject.Name1 = subject.Name1;
            contextSubject.Name2 = subject.Name2;
            contextSubject.Name3 = subject.Name3;
            contextSubject.Name4 = subject.Name4;
            contextSubject.Name5 = subject.Name5;
            contextSubject.Name6 = subject.Name6;
            contextSubject.Gender = subject.Gender;
            contextSubject.Category = subject.Category;
            contextSubject.Dropword = subject.Dropword;
            //contextPositiontype.Name = positiontype.Name;
            //contextPositiontype.Name2 = positiontype.Name2;
            //contextPositiontype.Name3 = positiontype.Name3;
            //contextPositiontype.Nameshort = positiontype.Nameshort;
            //contextPositiontype.Priority = positiontype.Priority;
            context.SaveChanges();
            UpdateSubjectsLocal();
        }

        public void AddPositioncategory(Positioncategory positioncategory)
        {
            context.Positioncategory.Add(positioncategory);
            context.SaveChanges();
            UpdatePositioncategoriesLocal();
        }

        public List<Positioncategory> SortPositioncategory(IEnumerable<Positioncategory> positioncategories)
        {
            List<Positioncategory> positioncategoriesSorted = new List<Positioncategory>();
            positioncategoriesSorted.AddRange(positioncategories.Where(pc => pc.Civil == 0 && pc.Variable == 0).Reverse());
            positioncategoriesSorted.AddRange(positioncategories.Where(pc => pc.Variable == 1));
            positioncategoriesSorted.AddRange(positioncategories.Where(pc => pc.Civil > 0));
            return positioncategoriesSorted;
        }

        public void AddMrd(Mrd mrd)
        {
            context.Mrd.Add(mrd);
            context.SaveChanges();
            UpdateMrdsLocal();
        }

        public void UpdateMrd(Mrd mrd)
        {
            Mrd contextMrd = context.Mrd.First(m => m.Id == mrd.Id);
            contextMrd.Name = mrd.Name;
            contextMrd.Short = mrd.Short;
            context.SaveChanges();
        }

        public void AddPositionmrd(Positionmrd positionmrd)
        {
            context.Positionmrd.Add(positionmrd);
            context.SaveChanges();
            UpdatePositionmrdsLocal();
        }

        public void AddPositionDecertificate(PositionDecertificate positionDecertificate)
        {
            Position position = context.Position.First(p => p.Id == positionDecertificate.Id);
            position.Decertificate = positionDecertificate.Decertificate;
            position.Decertificatedate = positionDecertificate.Decertificatedate;
            context.SaveChanges();
        }

        public void AddAltrankconditiongroup(Altrankconditiongroup altrankconditiongroup)
        {
            context.Altrankconditiongroup.Add(altrankconditiongroup);
            context.SaveChanges();
        }

        public void UpdateAltrankconditiongroup(Altrankconditiongroup altrankconditiongroup)
        {
            //context.Altrankconditiongroup.Add(altrankconditiongroup);
            Altrankconditiongroup contextAltrankconditiongroup = context.Altrankconditiongroup.First(a => a.Id == altrankconditiongroup.Id);
            contextAltrankconditiongroup.Name = altrankconditiongroup.Name;

            context.SaveChanges();
        }

        public void AddAltrankcondition(Altrankcondition altrankcondition)
        {
            context.Altrankcondition.Add(altrankcondition);
            context.SaveChanges();
        }

        public void UpdateAltrankcondition(Altrankcondition altrankcondition)
        {
            Altrankcondition contextAltrankcondition = context.Altrankcondition.First(a => a.Id == altrankcondition.Id);
            contextAltrankcondition.Name = altrankcondition.Name;
            contextAltrankcondition.Group = altrankcondition.Group;
            context.SaveChanges();
        }

        public void AddAltrank(Altrank altrank)
        {
            context.Altrank.Add(altrank);
            context.SaveChanges();
        }

        public void AddStructureregion(Structureregion structureregion)
        {
            context.Structureregion.Add(structureregion);
            context.SaveChanges();
        }

        public void UpdateStructureregion(Structureregion structureregion)
        {
            Structureregion contextStructureregion = context.Structureregion.First(s => s.Id == structureregion.Id);
            contextStructureregion.Name = structureregion.Name;
            context.SaveChanges();
        }

        public void AddStructuretype(Structuretype structuretype)
        {
            context.Structuretype.Add(structuretype);
            context.SaveChanges();
        }

        public void UpdateStructuretype(Structuretype structuretype)
        {
            Structuretype contextStructuretype = context.Structuretype.First(st => st.Id == structuretype.Id);
            contextStructuretype.Name = structuretype.Name;
            context.SaveChanges();
        }

        public void AddSession(Session session)
        {
            context.Session.Add(session);
            context.SaveChanges();
            UpdateSessionsLocal();
        }

        public void RemoveSessions(List<Session> list)
        {
            foreach (Session session in list)
            {
                context.Session.Remove(session);
            }
            context.SaveChanges();
            UpdateSessionsLocal();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        /**
         * Removes all expired sessions from the database. 
         */
        public void RemoveObsoleteSessions()
        {
            DateTime dateTime = DateTime.Now;
            List<Session> list = null;
            foreach (Session session in Sessions)
            {
                if (session.Expires < dateTime)
                {
                    if (list == null)
                    {
                        list = new List<Session>();
                    }
                    list.Add(session);
                }
            }
            if (list != null)
            {
                RemoveSessions(list);
            }
        }

        /**
         * Remove session with a specified id.
         */
        public void RemoveSession(string id)
        {
            Session session = context.Session.First(ses => ses.Id == id);
            context.Session.Remove(session);
            context.SaveChanges();
            UpdateSessionsLocal();
        }

        public int GetStructureType(Structure structure)
        {
            if (structure.Structuretype != 0)
            {
                return structure.Structuretype;
            }
            if (structure.Parentstructure != 0)
            {
                if (StructuresLocal().ContainsKey(structure.Parentstructure))
                {
                    return GetStructureType(StructuresLocal()[structure.Parentstructure]);
                } else
                {
                    return 0;
                }
            }
            return 0;
        }

        /**
         * Is allowed to edit structure
         */
        public bool IsAllowedStructureToEdit(User user)
        {
            if (user.Structureeditor >= 1)
            {
                return true;
            } else
            {
                return false;
            }
        }

        /**
         * Get structures 
         * If no parent, id == 0
         */
        public IEnumerable<StructureExpanded> GetAllowedStructuresToRead(User user)
        {
            List<int> parentsAll = new List<int>();
            return GetAllowedStructuresToRead(user, Structures.Select(x => x.Id).ToArray());
        }

        public IEnumerable<StructureExpanded> GetAllowedStructuresToRead(User user, int[] parents, int featured = 0, bool disabletracking = false)
        {
            //GGGGGGGGGGGG
            if (disabletracking)
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }

            if (user.Structureread == 0 && user.Admin == 0 && user.Masterpersonneleditor == 0)
            {
                return new List<StructureExpanded>(); // Означает, что мы вообще не можем ничего читать
            }
            DateTime date = user.Date.GetValueOrDefault();

            List<int> actualParents = new List<int>();
            foreach (int parent in parents)
            {
                int parentID = parent;
                bool opposite = false;
                if (parentID < 0)
                {
                    parentID = -parentID;
                    opposite = true;
                }
                Structure parentStructure = GetActualStructureInfo(parentID, date);
                if (parentStructure != null)
                {
                    if (opposite)
                    {
                        actualParents.Add(-parentStructure.Id);
                    } else
                    {
                        actualParents.Add(parentStructure.Id);
                    }
                    

                }
                
            }
            //parents = actualParents.ToArray();

            List<int> allowedParents = new List<int>();
            List<StructureExpanded> outputList = new List<StructureExpanded>();
            List<int> parentsList = new List<int>();
            List<int> parentsAbs = new List<int>();
            foreach (int parent in parents)
            {
                parentsAbs.Add(Math.Abs(parent));
            }


            //foreach(int parent in parents)
            foreach(int parent in actualParents.ToArray())
            {
                if (DisplayParent(actualParents.ToArray(), featured, parent, user, date, parents))
                {
                    parentsList.Add(parent);
                }
            }



            if (user.Structureeditor == 1 || user.Masterpersonneleditor == 1)
            {
                List<Structure> structures = GetChildrenOfParents(parentsList);
                

                //List<Structure> debugStructures = structures;
                structures = FilterDeletedStructures(structures, date).ToList();
                List<Structure> filteredStructures = new List<Structure>();
                foreach (Structure structure in structures)
                {
                    Structure actualStructure = GetActualStructureInfo(structure.Id, date, structures);
                    
                    bool isOrigin = false;
                    if (actualStructure != null && featured != 0)
                    {
                        if (featured == actualStructure.Id || featured == actualStructure.Changeorigin)
                        {
                            isOrigin = true;
                        }
                    }

                    //if (actualStructure != null) 
                    if (actualStructure != null && (parentsAbs.Count == 0 || parentsAbs.Contains(actualStructure.Parentstructure) || parentsAbs.Contains(actualStructure.Id) || isOrigin))
                    {
                        bool hasOrigin = true;
                        if (actualStructure.Changeorigin > 0 && !StructuresLocal().ContainsKey(actualStructure.Changeorigin))
                        {
                            hasOrigin = false; // bug
                        }
                        if (hasOrigin)
                        {
                            filteredStructures.Add(actualStructure);
                        }
                    }
                    else
                    {
                        //Structure debug = structure;
                    }

                }
                structures = filteredStructures.Distinct().ToList();
                outputList = StructureExpanded.StructuresToStructuresExpanded(structures, featured, this);
            } else
            {
                foreach (int parent in parentsList)
                {
                    // Берем id подразделения пользователя и пытаемся найти id актуального (измененного) подразделения пользователя
                    // Так как часто нужна именно "современная" версия
                    Structure userActualStructure = null;
                    int userActualStructureId = 0;
                    if (user.Structure.GetValueOrDefault() > 0)
                    {
                        userActualStructure = GetActualStructureInfo(user.Structure.GetValueOrDefault(), user.Date.GetValueOrDefault());
                        userActualStructureId = userActualStructure.Id;
                    }
                    if (parent == user.Structure.GetValueOrDefault() || parent == -user.Structure.GetValueOrDefault()
                        || parent == userActualStructureId || parent == -userActualStructureId)
                    {
                        allowedParents.Add(parent);
                    } else if (IsGrandParent(parent, user.Structure.GetValueOrDefault()))
                    {
                        allowedParents.Add(parent);
                    }
                }

                List<Structure> structures = GetChildrenOfParents(allowedParents);
                structures = FilterDeletedStructures(structures, date).ToList();
                List<Structure> filteredStructures = new List<Structure>();
                foreach (Structure structure in structures)
                {
                    Structure actualStructure = GetActualStructureInfo(structure.Id, date, structures);
                    if (actualStructure != null)
                    {
                        filteredStructures.Add(actualStructure);
                    }

                }
                structures = filteredStructures.Distinct().ToList();
                outputList = StructureExpanded.StructuresToStructuresExpanded(structures, featured, this);
            }
            foreach (StructureExpanded strExp in outputList)
            {
                if (strExp.ChildrenNumber == 0 && HasAnyChild(strExp.Id, date))
                {
                    strExp.ChildrenNumber = 1;
                }
                strExp.Grandparent = GetGrandParentName(GetActualStructureInfo(strExp.Id, user.Date.GetValueOrDefault()).Id, user.Date);
                /*strExp.Grandparent = GetGrandParentName(strExp.Id);*/
            }

            return outputList;

            //return outputList;
        }

        public IEnumerable<StructureExpanded> GetAllowedStructuresToReadStructureBlock(User user, int[] parents, int featured = 0, bool disabletracking = false)
        {
            if (disabletracking)
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            DateTime date = user.Date.GetValueOrDefault();


            List<int> actualParents = new List<int>();
            foreach (int parent in parents)
            {

                int parentID = parent;
                bool opposite = false;
                if (parentID < 0)
                {
                    parentID = -parentID;
                    opposite = true;
                }
                Structure parentStructure = GetActualStructureInfo(parentID, date);
                if (parentStructure != null)
                {
                    if (opposite)
                    {
                        actualParents.Add(-parentStructure.Id);
                    }
                    else
                    {
                        actualParents.Add(parentStructure.Id);
                    }
                }
            }
            //featured = actualParents.First(); // Без этого криво работало
            //parents = actualParents.ToArray();

            //List<int> parentsList = new List<int>();
            //foreach(int parent in parents)
            ////foreach (int parent in actualParents.ToArray())
            //{
            //    if (DisplayParent(actualParents.ToArray(), featured, parent, user, date, parents))
            //    {
            //        parentsList.Add(parent); 
            //    }
            //}


            List<int> allowedParents = new List<int>();
            List<StructureExpanded> outputList = new List<StructureExpanded>();
            List<int> parentsList = new List<int>();
            List<int> parentsAbs = new List<int>();
            foreach (int parent in parents)
            {
                parentsAbs.Add(Math.Abs(parent));
            }


            foreach(int parent in parents)
            //foreach (int parent in actualParents.ToArray())
            {
                //if (DisplayParent(parents.ToArray(), featured, parent, user, date, parents))
                if (DisplayParent(actualParents.ToArray(), featured, parent, user, date, parents))
                {
                    parentsList.Add(parent);
                }
            }

            //foreach (int parent in parents)
            foreach (int parent in actualParents.ToArray())
            {
                //if (DisplayParent(parents.ToArray(), featured, parent, user, date, parents))
                if (DisplayParent(actualParents.ToArray(), featured, parent, user, date, parents))
                {
                    parentsList.Add(parent);
                }
            }
            parentsList = parentsList.Distinct().ToList();


            if (user.Structureeditor == 1 || user.Masterpersonneleditor == 1)
            {
                List<Structure> structures = GetChildrenOfParents(parentsList);


                List<Structure> debugStructures = structures;
                structures = FilterDeletedStructures(structures, date).ToList();
                List<Structure> filteredStructures = new List<Structure>();
                foreach (Structure structure in structures)
                {
                    Structure actualStructure = GetActualStructureInfo(structure.Id, date, structures);
                    if (actualStructure != null)
                    {
                        bool hasOrigin = true;
                        if (actualStructure.Changeorigin > 0 && !StructuresLocal().ContainsKey(actualStructure.Changeorigin))
                        {
                            hasOrigin = false; // bug
                        }
                        if (hasOrigin)
                        {
                            filteredStructures.Add(actualStructure);
                        }
                    }
                    else
                    {
                        Structure debug = structure;
                    }

                }
                structures = filteredStructures.Distinct().ToList();
                outputList = StructureExpanded.StructuresToStructuresExpandedStructureBlock(structures, featured, this);
            }
            else
            {
                foreach (int parent in parentsList)
                {
                    if (parent == user.Structure.GetValueOrDefault() || parent == -user.Structure.GetValueOrDefault())
                    {
                        allowedParents.Add(parent);
                    }
                    else if (IsGrandParent(parent, user.Structure.GetValueOrDefault()))
                    {
                        allowedParents.Add(parent);
                    }
                }

                List<Structure> structures = GetChildrenOfParents(allowedParents);
                structures = FilterDeletedStructures(structures, date).ToList();
                List<Structure> filteredStructures = new List<Structure>();
                foreach (Structure structure in structures)
                {
                    Structure actualStructure = GetActualStructureInfo(structure.Id, date, structures);
                    if (actualStructure != null)
                    {
                        filteredStructures.Add(actualStructure);
                    }

                }
                structures = filteredStructures.Distinct().ToList();
                outputList = StructureExpanded.StructuresToStructuresExpanded(structures, featured, this);
            }
            foreach (StructureExpanded strExp in outputList)
            {
                if (strExp.ChildrenNumber == 0 && HasAnyChild(strExp.Id, date))
                {
                    strExp.ChildrenNumber = 1;
                }

                strExp.Grandparent = GetGrandParentName(strExp.Id);
            }

            return outputList;

            //return outputList;
        }

        public void FilterStructuresByPriority(List<Structure> structures, int parent)
        {
            List<int> rememberedPositions = new List<int>();
            List<Structure> rememberedStructures = new List<Structure>();
            int index = 0;
            foreach(Structure structure in structures)
            {
                if (structure.Parentstructure == parent)
                {
                    rememberedPositions.Add(index);
                    rememberedStructures.Add(structure);
                }
                index++;
            }
            rememberedStructures = rememberedStructures.OrderBy(rs => rs.Priority).ToList();
            index = 0;
            foreach(int pos in rememberedPositions)
            {
                structures[pos] = rememberedStructures[index];
                index++;
            }
            foreach (Structure structure in rememberedStructures)
            {
                int id = structure.Id;
                if (structure.Changeorigin > 0)
                {
                    id = structure.Changeorigin;
                }
                FilterStructuresByPriority(structures, id);
            }
        }

        public IEnumerable<StructureExpanded> FilterDeletedStructures(IEnumerable<StructureExpanded> structures, DateTime date)
        {
            List<StructureExpanded> filtered = new List<StructureExpanded>();
            foreach (StructureExpanded structure in structures)
            {
                if (!IsSignedAndDeleted(structure, date))
                {
                    filtered.Add(structure);
                }
            }
            return filtered;
        }

  

        public IEnumerable<Structure> FilterDeletedStructures(IEnumerable<Structure> structures, DateTime date)
        {
            return structures.Where(r => !IsSignedAndDeleted(r, date));
        }


        public IEnumerable<Structure> FilterDeletedStructures(IEnumerable<int> structures, DateTime date)
        {
            List<Structure> filtered = new List<Structure>();
            foreach (int id in structures)
            {
                Structure structure = StructuresLocal()[id];
                if (!IsSignedAndDeleted(structure, date))
                {
                    filtered.Add(structure);
                }
            }
            return filtered;
        }

        /**
         * True if allowed to read departments and positions
         */
        public bool isAllowedToReadStructure(User user, int structureID)
        {
/*            if (structureID < 0)
                structureID = -structureID;*/
            if (user.Structure.GetValueOrDefault() == structureID ||
                user.Masterpersonneleditor.GetValueOrDefault() == 1 ||
                user.Structureeditor.GetValueOrDefault() == 1)
                return true;

/*            bool grandParent = IsGrandParent(structureID, user.Structure.GetValueOrDefault());*/
            
            if (structureID == user.Structure.GetValueOrDefault() || -structureID == user.Structure.GetValueOrDefault())
            {
                return true;
            }
            return IsGrandParent(structureID, user.Structure.GetValueOrDefault());
        }

        public bool DisplayParent(int[] parents, int featured, int parentID, User user, DateTime date, int[] originalParents)
        {
            //if (structures == null)
            //{
            //    structures = Structures;
            //}
            if (parentID < 0)
            {
                parentID = -parentID;
            }

            while (true)
            {
                Structure parent = StructuresLocal().GetValue(parentID);
                //Structure parent = GetActualStructureInfo(parentID, date);
                //Structure parent = structures.FirstOrDefault(s => s.Id == parentID);
                
                if (parent == null)
                {
                    return false;
                }
                if (parent.Parentstructure == 0 || parent.Id == featured || user.Structure.GetValueOrDefault() ==  parent.Id)
                {
                    return true;
                }
                //Structure actualParent = GetActualStructureInfo(parent.Parentstructure, date);
                //if (actualParent != null && parents.Contains(actualParent.Id))
                if (parents.Contains(parent.Parentstructure) || originalParents.Contains(parent.Parentstructure))
                {
                    parentID = parent.Parentstructure;
                } else
                {
                    return false;
                }
                
                
            }
        }

        public List<StructureTree> GetStructureTrees(int[] ids, DateTime date)
        {
            List<StructureTree> structureTrees = new List<StructureTree>();
            foreach (int id in ids)
            {
                StructureTree structureTree = new StructureTree();
                Structure structure = StructuresLocal().GetValue(id);
                //Structure structure = Structures.FirstOrDefault(s => s.Id == id);
                if (structure != null)
                {
                    structureTree.Tree = FormTree(structure, true, date);
                    structureTree.Id = id;
                    structureTrees.Add(structureTree);
                }
                
            }
            return structureTrees;
        }

        /**
         * Возвращает ID первоначального подразделения.
         * То есть, если подразделение было изменено, то возвращает ID подразделения, которое было до всех изменений.
         * На первоначальный ID ссылаются все подчиненные подразделения и должности
         */
        public int StructureBaseId(Structure structure)
        {
            if (structure.Changeorigin > 0)
            {
                return structure.Changeorigin;
            }
            else
            {
                return structure.Id;
            }
        }

        public int StructureBaseId(StructureExpanded structureExpanded)
        {
            if (structureExpanded.Changeorigin > 0)
            {
                return structureExpanded.Changeorigin;
            }
            else
            {
                return structureExpanded.Id;
            }
        }

        public List<StructureTree> GetCurations(int[] ids, int positionid, DateTime date)
        {
            List<StructureTree> structureTrees = new List<StructureTree>();
            foreach (int id in ids)
            {
                StructureTree structureTree = new StructureTree();
                Structure structure = StructuresLocal().GetValue(id);
                if (structure == null)
                {
                    continue;
                }
                if (IsCurating(positionid, id, date))
                {
                    structureTree.Tree = FormTree(structure, true, date);
                    structureTree.Id = id;
                    structureTrees.Add(structureTree);
                }
                //if (structure.Curator == positionid)
                //{
                //    structureTree.Tree = FormTree(structure, true, date);
                //    structureTree.Id = id;
                //    structureTrees.Add(structureTree);
                //}
                
            }
            return structureTrees;
        }

        public StructureTree GetHeading(int id, int positionid, DateTime date)
        {
            StructureTree structureTree = new StructureTree();
            Structure structure = StructuresLocal().GetValue(id);
            //Structure structure = Structures.FirstOrDefault(s => s.Id == id);
            if (structure.Head == positionid)
            {
                structureTree.Tree = FormTree(structure, true, date);
                structureTree.Id = id;
            }
            return structureTree;
        }

        /// <summary>
        /// Выводит список полных наименований подразделений по всей Республике.
        /// В дальнейшем убрать руководства, караулы и прочее.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<string> GetStructuresAllDocument(User user)
        {
            List<string> list = new List<string>();
            if (StructuresLocal() == null)
            {
                UpdateStructuresLocal();
            }
            IEnumerable<Structure> structures = GetActualStructures(StructuresLocal().Values, user.Date.GetValueOrDefault());
            //IEnumerable<Structure> structures = FilterDeletedStructures(StructuresLocal().Values, user.Date.GetValueOrDefault());
            foreach(Structure structure in structures)
            {
                // Выводить только обособленные (не структурные) подразделения. Пока определяем по городу
                //if (structure.City != null && structure.City.Length > 0)
                //{
                // Проверка на скорую руку. Потом улучшить
                // Пока не включаем ПАСПы, ПАСЧи, ПАСО и прочие
                /*if (structure.Structuretype != 15 && structure.Structuretype != 18 && structure.Structuretype != 45
                    && structure.Structuretype != 13 && structure.Structuretype != 17 && structure.Structuretype != 14
                    && structure.Structuretype != 22 && structure.Structuretype != 30)*/
                if (structure.Featured == 1 || structure.Id == 1)
                {
                        list.Add(FormTreeDocument(structure, user.Date.GetValueOrDefault(), null, 2, null));
                    }
                //}
                
                
            }
            return list;
        }

        

        /**
         * Get all information about structure including head, curations, different quantities
         * childrenWithSameType - если true, то удаляет всех детей, у которых ЕСТЬ тип, но он не совпадает с основным. Если типа нет, то все окей.
         */
        public StructureInfo GetStructureInfo(int structureid, User user, bool disabletracking = false, bool includechildren = true, bool countPositions = false, List<int> acceptedTypes = null)
        {
            DateTime date = user.Date.GetValueOrDefault();
            if (disabletracking)
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            if (structureid < 0)
            {
                structureid = -structureid; // reverse id because ids for structures are negative
            }
            Structure structure = null;
            if (StructuresLocal().ContainsKey(structureid))
            {
                structure = StructuresLocal()[structureid];
            }
            Structure originalStructure = GetOriginalStructure(structureid);
            //Structure structure = Structures.FirstOrDefault(s => s.Id == structureid);
            if (structure == null)
            {
                return null;
            }
            if (structure.Changeorigin > 0)
            {
                if (!StructuresLocal().ContainsKey(structure.Changeorigin))
                {
                    return null; // original not found;
                }
            }

            StructureInfo structureInfo = new StructureInfo();
            // 32 start
            structureInfo.Head = structure.Head;
            if (countPositions)
            {
                structureInfo.Positions = new List<Position>();
            }

            double countSigned = 0; // Overall count of positions signed
            double countUnsigned = 0; // Overall count of positions unsigned
            double varCountSigned = 0;
            double varCountUnsigned = 0;
            double positionAddFuture = 0;
            double positionRemoveFuture = 0;

            int varPositionCategoryId = PositioncategoriesLocal().Values.First(pc => pc.Variable == 1).Id; 
            List<int> externalSofs = SourceoffinancingsLocal().Values.Where(s => s.External == 1).Select(s => s.Id).ToList();
            SortedDictionary<int, KeyValuePair<double, double>> sofvalues = new SortedDictionary<int, KeyValuePair<double, double>>(); // Sof name - count signed - count unsigned

            List<Position> positions = new List<Position>();
            List<int> structureids = new List<int>();
            if (structure.Changeorigin > 0)
            {
                structureid = structure.Changeorigin; // We take information about children and positions from origin id.
            }
            structureids.Add(structureid);
            if (includechildren)
            {
                if (acceptedTypes == null)
                {
                    structureids = GetStructuresSiblings(structureid, null, date); // Добавить дату
                } else
                {
                    structureids = GetStructuresSiblingsWithAcceptedTypes(structureid, acceptedTypes, date); // Добавить дату
                }
                
            }
            structureids = structureids.Distinct().ToList(); // Пробуем удалить дупликаты

            List<Structure> nondeletedStructures = new List<Structure>();
            foreach (int id in structureids)
            {
                Structure structureFinded = StructuresLocal().GetValue(id);
                if (structureFinded != null)
                {
                    nondeletedStructures.Add(structureFinded);
                }
            }
            nondeletedStructures = FilterDeletedStructures(nondeletedStructures, date).ToList();
            structureids = nondeletedStructures.Select(s => s.Id).ToList();

            foreach (int id in structureids)
            {

                Structure actualStructure = GetActualStructureInfo(id, date);
                //&& IsGrandParent(actualStructure.Id, originalStructure.Id)
                if (actualStructure != null )
                {
                    int originalId = GetStructureOriginalId(actualStructure);
                    //IEnumerable<Position> positionsStructure = GetPositions(id, user.Date.GetValueOrDefault(), false, false, true);
                    IEnumerable<Position> positionsStructure = GetPositions(originalId, user.Date.GetValueOrDefault(), false, false, true);
                    positions.AddRange(positionsStructure);
                }

            }
            // Ошибка палится где-то до этого момента

            IEnumerable<DecreeOperationsRequest> requests = GetDecreeOperationsRequests(positions, user.Date.GetValueOrDefault());
            //  32 end, 22 start
            Dictionary<int, Position> positionsDictionary = new Dictionary<int, Position>(); // Dictionary for faster get
            foreach (Position position in positions)
            {
                if (!positionsDictionary.ContainsKey(position.Id))
                {
                    positionsDictionary.Add(position.Id, position);
                }
                
            }

            Dictionary<int, List<DecreeoperationManagement>> decreeoperationManagements = new Dictionary<int, List<DecreeoperationManagement>>();
            foreach (DecreeOperationsRequest request in requests) {
                if (!decreeoperationManagements.ContainsKey(request.SubjectID))
                {
                    decreeoperationManagements.Add(request.SubjectID, RequestDecreeOperations(request));
                }
                
            }
            //  22 end
            List<int> used = new List<int>();
            foreach(KeyValuePair<int, List<DecreeoperationManagement>> domList in decreeoperationManagements)
            {
                if (IsSignedAndDeleted(domList.Value))
                {
                    continue;
                }
                else 
                if (IsSignedAndCreated(domList.Value))
                {
                    //countSigned++;
                    Position position = positionsDictionary[domList.Key];
                    
                    if (countPositions)
                    {
                        structureInfo.Positions.Add(position);
                    }
                    
                    if (position.Positioncategory == varPositionCategoryId)
                    {
                        if (!externalSofs.Contains(position.Sourceoffinancing))
                        {
                            varCountSigned += position.Partval;
                        }
                            
                    } else
                    {
                        if (!externalSofs.Contains(position.Sourceoffinancing))
                        {
                            countSigned += position.Partval;
                        }

                        if (!sofvalues.ContainsKey(position.Sourceoffinancing))
                        {
                            sofvalues.Add(position.Sourceoffinancing, new KeyValuePair<double, double>());
                        }
                        KeyValuePair<double, double> oldKeyValuePair = sofvalues[position.Sourceoffinancing];
                        sofvalues[position.Sourceoffinancing] = new KeyValuePair<double, double>(oldKeyValuePair.Key + position.Partval, oldKeyValuePair.Value); // здесь плюсуется, но видимо лишние должности подгружает
                    }

                    
                } else if (IsNotSignedAndCreated(domList.Value))
                {
                    //countUnsigned++;
                    Position position = positionsDictionary[domList.Key];
                    
                    if (position.Positioncategory == varPositionCategoryId)
                    {
                        varCountUnsigned += position.Partval;
                    } else
                    {
                        if (!externalSofs.Contains(position.Sourceoffinancing))
                        {
                            countUnsigned += position.Partval;
                        }

                        if (!sofvalues.ContainsKey(position.Sourceoffinancing))
                        {
                            sofvalues.Add(position.Sourceoffinancing, new KeyValuePair<double, double>());
                        }
                        KeyValuePair<double, double> oldKeyValuePair = sofvalues[position.Sourceoffinancing];
                        sofvalues[position.Sourceoffinancing] = new KeyValuePair<double, double>(oldKeyValuePair.Key, oldKeyValuePair.Value + position.Partval);
                    }

                    
                }
                if (IsNotSignedAndDeleted(domList.Value))
                {
                    //countUnsigned++;
                    Position position = positionsDictionary[domList.Key];

                    if (position.Positioncategory == varPositionCategoryId)
                    {
                        varCountUnsigned -= position.Partval;
                    }
                    else
                    {
                        if (!externalSofs.Contains(position.Sourceoffinancing))
                        {
                            countUnsigned -= position.Partval;
                        }

                        if (!sofvalues.ContainsKey(position.Sourceoffinancing))
                        {
                            sofvalues.Add(position.Sourceoffinancing, new KeyValuePair<double, double>());
                        }
                        KeyValuePair<double, double> oldKeyValuePair = sofvalues[position.Sourceoffinancing];
                        sofvalues[position.Sourceoffinancing] = new KeyValuePair<double, double>(oldKeyValuePair.Key, oldKeyValuePair.Value - position.Partval);
                    }


                }
                if (IsSignedAndWillCreated(domList.Value))
                {
                    Position position = positionsDictionary[domList.Key];
                    positionAddFuture += position.Partval;
                    string dateFuture = GetDateFuture(domList.Value).ToString("yyyy.MM.dd");

                    if (!structureInfo.FutureAddDetailed.ContainsKey(dateFuture))
                    {
                        structureInfo.FutureAddDetailed.Add(dateFuture, position.Partval);
                    } else
                    {
                        double value = (double)structureInfo.FutureAddDetailed[dateFuture];
                        structureInfo.FutureAddDetailed[dateFuture] = value + position.Partval;
                    }
                    
                }

                if (IsSignedAndWillDeleted(domList.Value))
                {
                    Position position = positionsDictionary[domList.Key];
                    positionRemoveFuture += position.Partval;
                    string dateFuture = GetDateFuture(domList.Value).ToString("yyyy.MM.dd");

                    if (!structureInfo.FutureRemoveDetailed.ContainsKey(dateFuture))
                    {
                        structureInfo.FutureRemoveDetailed.Add(dateFuture, position.Partval);
                    }
                    else
                    {
                        double value = (double)structureInfo.FutureRemoveDetailed[dateFuture];
                        structureInfo.FutureRemoveDetailed[dateFuture] = value + position.Partval;
                    }
                }
            }

            
            structureInfo.PositionCountSigned = countSigned;
            structureInfo.PositionCountUnsigned = countUnsigned;
            structureInfo.VarCountSigned = varCountSigned;
            structureInfo.VarCountUnsigned = varCountUnsigned;
            structureInfo.PositionAddFuture = positionAddFuture;
            structureInfo.PositionRemoveFuture = positionRemoveFuture;
            string sofList = "";
            string sofCountSigned = "";
            string sofCountUnsigned = "";

            structureInfo.SofValues = sofvalues;
            foreach (KeyValuePair<int, KeyValuePair<double, double>> sofvalue in sofvalues)
            {
                string shortified = String.Join(String.Empty, SourceoffinancingsLocal()[sofvalue.Key].Name.Split(new[] { ' ' }).Select(word => word.First())).ToUpper();
                sofList += shortified + ";";
                sofCountSigned += sofvalue.Value.Key.ToString() + ";";
                sofCountUnsigned += sofvalue.Value.Value.ToString() + ";";
            }
            /**
             * Any in list.
             */
            if (sofList.Length > 0)
            {
                sofList = sofList.Remove(sofList.Length - 1);
                sofCountSigned = sofCountSigned.Remove(sofCountSigned.Length - 1);
                sofCountUnsigned = sofCountUnsigned.Remove(sofCountUnsigned.Length - 1);
            }
            structureInfo.SofNameList = sofList;
            structureInfo.PositionCountSofSigned = sofCountSigned;
            structureInfo.PositionCountSofUnsigned = sofCountUnsigned;
            return structureInfo;
        }

        /// <summary>
        /// Принимает первоначальное ID подразделения. Функция возвращает информацию (численность, бюджеты) о всех подразделениях, непосредственно подчиненных ей, а также за саму себя.
        /// Если стоит allsiblings, то возвращает не только непосредственно подчиненных, но и вообще всех подразделений, располагающихся в ней.
        /// </summary>
        /// <param name="structureid"></param>
        /// <param name="user"></param>
        /// <param name="disabletracking"></param>
        /// <param name="includeself"></param>
        /// <param name="includefather"></param>
        /// <returns></returns>
        public List<StructureInfo> GetStructureInfos(int structureid, User user, bool disabletracking = false, bool includeself = true, bool includefather = true, bool allsiblings = false)
        {
            if (disabletracking)
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }

            if (structureid < 0)
            {
                structureid = -structureid; // reverse id because ids for structures are negative
            }

            DateTime date = user.Date.GetValueOrDefault();

            //IEnumerable<StructureExpanded> siblings = GetAllowedStructuresToRead(user, new int[] { structureid }).Skip(1);
            IEnumerable<Structure> siblings = null;
            if (allsiblings)
            {
                List<int> structureids = GetStructuresSiblings(structureid, null, date);
                List<Structure> siblingsList = new List<Structure>();
                foreach (int siblingid in structureids)
                {
                    Structure sibling = GetActualStructureInfo(siblingid, date);
                    if (sibling != null)
                    {
                        siblingsList.Add(sibling);
                    }
                }
                siblings = siblingsList;
            } else
            {
                siblings = GetChildren(structureid);
            }
            
            siblings = FilterDeletedStructures(siblings, date).ToList();
            List<Structure> filteredStructures = new List<Structure>();
            Structure fatherStructure = null;
            Structure currentStructure = GetActualStructureInfo(structureid, date);
            if (currentStructure != null && currentStructure.Parentstructure > 0)
            {
                fatherStructure = GetActualStructureInfo(currentStructure.Parentstructure, date);
            }
            foreach (Structure structure in siblings)
            {
                Structure actualStructure = GetActualStructureInfo(structure.Id, date, siblings);

                if (actualStructure != null)
                {
                    filteredStructures.Add(actualStructure);
                }
                
            }
            
            siblings = filteredStructures.Distinct().ToList();
            if (!allsiblings)
            {
                siblings = siblings.Where(s => s.Parentstructure == structureid); // Отбраковывает те подразделения, которые когда-то были подчинены введенному, но потом перестали (были перемещены).
            }
            

            StructureInfo mainStructureInfo = new StructureInfo();
            mainStructureInfo = GetStructureInfo(structureid, user, true, false);
            mainStructureInfo.Id = structureid;
            SortedDictionary<int, KeyValuePair<double, double>> sofvalues = mainStructureInfo.SofValues;

            List<StructureInfo> structureInfos = new List<StructureInfo>();
            //structureInfos.Add(mainStructureInfo);
            //IEnumerable<Structure> silblingsSub = siblings;
            foreach (Structure structure in siblings)
            {
                StructureInfo structureInfo = new StructureInfo();
                int id = structure.Id;

                structureInfo = GetStructureInfo(structure.Id, user);
                if (structureInfo == null)
                {
                    continue;
                }
                //Structure actualStructure = GetActualStructureInfo(structure.Id, user.Date.GetValueOrDefault());
                structureInfo.Id = structure.Id;
                if (structure.Changeorigin > 0)
                {
                    structureInfo.Id = structure.Changeorigin;
                }
                structureInfo.Name = structure.Name;
                structureInfo.HasChildren = HasAnyChild(structure.Id, date);
                structureInfo.Priority = structure.Priority;
                //structureInfo.Name = actualStructure.Name;
                structureInfos.Add(structureInfo);

                mainStructureInfo.PositionCountSigned += structureInfo.PositionCountSigned;
                mainStructureInfo.PositionCountUnsigned += structureInfo.PositionCountUnsigned;
                mainStructureInfo.PositionAddFuture += structureInfo.PositionAddFuture;
                mainStructureInfo.PositionRemoveFuture += structureInfo.PositionRemoveFuture;
                foreach (KeyValuePair<string, double> de in structureInfo.FutureAddDetailed)
                {
                    if (!mainStructureInfo.FutureAddDetailed.ContainsKey(de.Key))
                    {
                        mainStructureInfo.FutureAddDetailed.Add(de.Key, de.Value);
                    } else
                    {
                        double value = (double)de.Value;
                        mainStructureInfo.FutureAddDetailed[de.Key] = (double)mainStructureInfo.FutureAddDetailed[de.Key] + value;
                    }
                }
                foreach (KeyValuePair<string, double> de in structureInfo.FutureRemoveDetailed)
                {
                    if (!mainStructureInfo.FutureRemoveDetailed.ContainsKey(de.Key))
                    {
                        mainStructureInfo.FutureRemoveDetailed.Add(de.Key, de.Value);
                    }
                    else
                    {
                        double value = (double)de.Value;
                        mainStructureInfo.FutureRemoveDetailed[de.Key] = (double)mainStructureInfo.FutureRemoveDetailed[de.Key] + value;
                    }
                }

                foreach (KeyValuePair<int, KeyValuePair<double, double>> sofvalue in structureInfo.SofValues)
                {
                    if (!sofvalues.ContainsKey(sofvalue.Key))
                    {
                        sofvalues.Add(sofvalue.Key, new KeyValuePair<double, double>());
                    }
                    double prevKey = sofvalues[sofvalue.Key].Key;
                    double prevValue = sofvalues[sofvalue.Key].Value;
                    sofvalues[sofvalue.Key] = new KeyValuePair<double, double>(prevKey + sofvalue.Value.Key, prevValue + sofvalue.Value.Value);
                }
            }

            structureInfos = structureInfos.OrderBy(s => s.Priority).ToList();
            structureInfos.Insert(0, mainStructureInfo);

            string sofList = "";
            string sofCountSigned = "";
            string sofCountUnsigned = "";
            foreach (KeyValuePair<int, KeyValuePair<double, double>> sofvalue in sofvalues)
            {
                string shortified = String.Join(String.Empty, SourceoffinancingsLocal()[sofvalue.Key].Name.Split(new[] { ' ' }).Select(word => word.First())).ToUpper();
                sofList += shortified + ";";
                sofCountSigned += sofvalue.Value.Key.ToString() + ";";
                sofCountUnsigned += sofvalue.Value.Value.ToString() + ";";
            }
            /**
             * Any in list.
             */
            if (sofList.Length > 0)
            {
                sofList = sofList.Remove(sofList.Length - 1);
                sofCountSigned = sofCountSigned.Remove(sofCountSigned.Length - 1);
                sofCountUnsigned = sofCountUnsigned.Remove(sofCountUnsigned.Length - 1);
            }
            mainStructureInfo.SofNameList = sofList;
            mainStructureInfo.PositionCountSofSigned = sofCountSigned;
            mainStructureInfo.PositionCountSofUnsigned = sofCountUnsigned;

            if (includefather && fatherStructure != null)
            {
                StructureInfo fatherStructureInfo = new StructureInfo();
                fatherStructureInfo.Id = fatherStructure.Id;
                fatherStructureInfo.Previous = true;
                if (fatherStructure.Changeorigin > 0)
                {
                    fatherStructureInfo.Id = fatherStructure.Changeorigin;
                }
                fatherStructureInfo.Name = fatherStructure.Name;
                structureInfos.Add(fatherStructureInfo);
            }

            List<string> addremovefuture = new List<string>();

            foreach (KeyValuePair<string, double> de in mainStructureInfo.FutureAddDetailed)
            {
                string[] splittedvalue = de.Key.Split(".");
                addremovefuture.Add("+" + de.Value + " c " + splittedvalue[2] + "." + splittedvalue[1] + "." + splittedvalue[0]);
            }

            foreach (KeyValuePair<string, double> de in mainStructureInfo.FutureRemoveDetailed)
            {
                string[] splittedvalue = de.Key.Split(".");
                addremovefuture.Add("-" + de.Value + " c " + splittedvalue[2] + "." + splittedvalue[1] + "." + splittedvalue[0]);
            }
            mainStructureInfo.PositionFutureDetailed = addremovefuture.ToArray();
            return structureInfos;
        }




        public StructureInfoInner GetStructureInfoInner(int structureid, DateTime date, Pmrequest pmrequest = null)
        {
            if (structureid < 0)
            {
                structureid = -structureid; // reverse id because ids for structures are negative
            }
            Structure structure = StructuresLocal().GetValue(structureid);
            //Structure structure = Structures.FirstOrDefault(s => s.Id == structureid);
            if (structure == null)
            {
                return null;
            }

            StructureInfoInner structureInfo = new StructureInfoInner();
            structureInfo.Head = structure.Head;

            double countSigned = 0; // Overall count of positions signed
            double countUnsigned = 0; // Overall count of positions unsigned
            Dictionary<int, KeyValuePair<double, double>> sofvalues = new Dictionary<int, KeyValuePair<double, double>>(); // Sof name - count signed - count unsigned

            List<Position> positions = new List<Position>();
            List<int> structureids = new List<int>();
            if (pmrequest != null && pmrequest.Structureselfcount > 0)
            {
                structureids.Add(structureid);
            } else
            {
                if (pmrequest != null && pmrequest.Structurecountallinclusive > 0)
                {
                    structureids = GetStructuresSiblings(structureid, null, date); 
                } else
                {
                    structureids = GetStructuresSiblingsWithSameOrNullType(structureid, date);
                }
                
            }

            foreach (int id in structureids)
            {


                Structure actualStructure = GetActualStructureInfo(id, date);

                if (actualStructure != null)
                {
                    int originalId = GetStructureOriginalId(actualStructure);
                    //IEnumerable<Position> positionsStructure = GetPositions(id, user.Date.GetValueOrDefault(), false, false, true);
                    IEnumerable<Position> positionsStructure = GetPositions(originalId, date, false, false, true);
                    positions.AddRange(positionsStructure);
                }

            }

            //foreach (int id in structureids)
            //{
            //    positions.AddRange(GetPositions(id, date, false)); // -structureid because it accepts negative ids for structure 
            //}


            IEnumerable<DecreeOperationsRequest> requests = GetDecreeOperationsRequests(positions, date);
            Dictionary<int, Position> positionsDictionary = new Dictionary<int, Position>(); // Dictionary for faster get
            foreach (Position position in positions)
            {
                if (!positionsDictionary.ContainsKey(position.Id))
                {
                    positionsDictionary.Add(position.Id, position);
                }
                
            }
            Dictionary<int, List<DecreeoperationManagement>> decreeoperationManagements = new Dictionary<int, List<DecreeoperationManagement>>();
            foreach (DecreeOperationsRequest request in requests)
            {
                if (!decreeoperationManagements.ContainsKey(request.SubjectID))
                {
                    decreeoperationManagements.Add(request.SubjectID, RequestDecreeOperations(request));
                }
                
            }
            //  return this.isDeletedUnsigned(posdep) || this.isNotSignedAndWillBeCreated(posdep) || this.isNotSignedAndCreated(posdep) || this.isWillBeDeletedUnsigned(posdep);
            foreach (KeyValuePair<int, List<DecreeoperationManagement>> domList in decreeoperationManagements)
            {
                if (IsSignedAndCreated(domList.Value))
                {

                    //countSigned++;
                    Position position = positionsDictionary[domList.Key];
                    countSigned += position.Partval;
                    if (!sofvalues.ContainsKey(position.Sourceoffinancing))
                    {
                        sofvalues.Add(position.Sourceoffinancing, new KeyValuePair<double, double>());
                    }
                    KeyValuePair<double, double> oldKeyValuePair = sofvalues[position.Sourceoffinancing];
                    sofvalues[position.Sourceoffinancing] = new KeyValuePair<double, double>(oldKeyValuePair.Key + position.Partval, oldKeyValuePair.Value);

                }
                else if (IsNotSignedAndCreated(domList.Value))
                {
                    //countUnsigned++;
                    Position position = positionsDictionary[domList.Key];
                    countUnsigned += position.Partval;
                    if (!sofvalues.ContainsKey(position.Sourceoffinancing))
                    {
                        sofvalues.Add(position.Sourceoffinancing, new KeyValuePair<double, double>());
                    }
                    KeyValuePair<double, double> oldKeyValuePair = sofvalues[position.Sourceoffinancing];
                    sofvalues[position.Sourceoffinancing] = new KeyValuePair<double, double>(oldKeyValuePair.Key, oldKeyValuePair.Value + position.Partval);
                }
            }

            structureInfo.PositionCountSigned = countSigned;
            structureInfo.PositionCountUnsigned = countUnsigned;
            structureInfo.sofValues = sofvalues;
            return structureInfo;
        }

        public List<Structure> GetStructureHighestWithSameType(IEnumerable<Structure> structures, DateTime date)
        {
            List<Structure> structuresFiltered = new List<Structure>();

            foreach (Structure structure in structures)
            {
                bool anyGrandparent = false;
                foreach (Structure subStructure in structures)
                {
                    if (subStructure != structure)
                    {
                        if (IsGrandParent(structure.Id, GetOriginalStructure(subStructure.Id).Id))
                        {
                            Structure child = GetActualStructureInfo(structure.Id, date);
                            Structure parent = GetActualStructureInfo(subStructure.Id, date);
                            if (child.Structuretype != 0 && parent.Structuretype != 0 && child.Structuretype == parent.Structuretype)
                            {
                                anyGrandparent = true;
                                break;
                            }
                            
                        }
                    }
                }
                if (!anyGrandparent)
                {
                    structuresFiltered.Add(structure);
                }
            }

            return structuresFiltered;
        }


        /**
         * Siblings mean children and children of children that follow "same or null type" rule.
         * UPDATED 21.01.2019 Without Get ChildrenAndSelf
         */
        public List<int> GetStructuresSiblingsWithSameOrNullType(int structureid, DateTime? date = null)
        {
            if (structureid < 0)
            {
                structureid = -structureid;
            }
            List<int> siblings = new List<int>(); //FilterDeletedStructures
            List<Structure> structures = null;
            if (date == null)
            {
                //structures = GetChildrenAndSelf(structureid);
                structures = GetChildrenList(structureid);
                structures.Add(GetSelfAsElement(structureid));

            } else
            {
                // structures = FilterDeletedStructures(GetChildrenAndSelf(structureid), date.GetValueOrDefault());
                structures = GetChildrenList(structureid);
                structures.Add(GetSelfAsElement(structureid));
                structures = FilterDeletedStructures(structures, date.GetValueOrDefault()).ToList();

            }
            int structuretype = 0;
            foreach (Structure structure in structures)
            {
                if (structure.Id == structureid)
                {
                    structuretype = structure.Structuretype;
                    siblings.Add(structure.Id);
                    break;
                }
            }
            foreach (Structure structure in structures)
            {
                if (structure.Id != structureid && (structure.Structuretype == structuretype || structure.Structuretype == 0))
                {
                    siblings.AddRange(GetStructuresSiblingsWithSameOrNullType(structure.Id, date));
                }
            }
            return siblings;
        }

        /**
         * Siblings mean children and children of children that follow "same or null type" rule.
         * UPDATED 21.01.2019 Without Get ChildrenAndSelf
         */
        public List<int> GetStructuresSiblingsWithAcceptedTypes(int structureid, List<int> structuretypes, DateTime? date = null, int iterationLimit = 100)
        {
            if (structureid < 0)
            {
                structureid = -structureid;
            }
            List<int> siblings = new List<int>(); //FilterDeletedStructures
            List<Structure> structures = null;
            if (date == null)
            {
                //structures = GetChildrenAndSelf(structureid);
                structures = GetChildrenList(structureid);
                structures.Add(GetSelfAsElement(structureid));

            }
            else
            {
                // structures = FilterDeletedStructures(GetChildrenAndSelf(structureid), date.GetValueOrDefault());
                structures = GetChildrenList(structureid);
                structures.Add(GetSelfAsElement(structureid));
                structures = FilterDeletedStructures(structures, date.GetValueOrDefault()).ToList();

            }
            foreach (Structure structure in structures)
            {
                if (structure.Id == structureid)
                {
                    siblings.Add(structure.Id);
                    break;
                }
            }
            foreach (Structure structure in structures)
            {
                if (structure.Id != structureid && (structuretypes.Contains(structure.Structuretype) || structure.Structuretype == 0) && iterationLimit > 0)
                {
                    siblings.AddRange(GetStructuresSiblingsWithAcceptedTypes(structure.Id, structuretypes, date, iterationLimit - 1));
                }
            }
            //return siblings;

            List<Structure> nondeletedStructures = new List<Structure>();
            List<int> structureids = siblings.Distinct().ToList();

            foreach (int id in structureids)
            {
                Structure structureFinded = StructuresLocal().GetValue(id);
                if (structureFinded != null)
                {
                    nondeletedStructures.Add(structureFinded);
                }
            }
            nondeletedStructures = FilterDeletedStructures(nondeletedStructures, date.GetValueOrDefault()).ToList();
            structureids = nondeletedStructures.Select(s => s.Id).ToList();

            return structureids;
        }



        /**
         * if acceptedStructureTypes is not null, it will accept structure types that present in list
         */
        public List<int> GetStructuresSiblings(int structureid, List<int> acceptedStructuretypes = null, DateTime? date = null, int iterationLimit = 100)
        {
            if (acceptedStructuretypes != null)
            {
                if (!acceptedStructuretypes.Contains(0))
                {
                    acceptedStructuretypes.Add(0);
                }
            }
            if (structureid < 0)
            {
                structureid = -structureid;
            }
            List<int> siblings = new List<int>();
            structureid = GetStructureOriginalId(structureid); // Добавили новое. Теперь смотрим только по изначальной структуре, а не новым.
            IEnumerable<Structure> structures = GetChildrenAndSelf(structureid);
            if (date != null)
            {
                structures = FilterDeletedStructures(structures, date.GetValueOrDefault()).ToList(); // Попробуем удалять неактуал
            }
            int structuretype = 0;
            foreach (Structure structure in structures)
            {
                if (structure.Id == structureid)
                {
                    structuretype = structure.Structuretype;
                    
                    siblings.Add(structure.Id);
                    break;
                }
            }
            foreach (Structure structure in structures)
            {

                // Если дата не нулевая, смотрим на актуальность принадлежности потомка к предку
                if (date != null)
                {
                    Structure actualChild = GetActualStructureInfo(structure, date.GetValueOrDefault());
                    if (actualChild != null)
                    {
                        // У потомка со временем сменился предок с текущего на какого-то другого
                        if (actualChild.Parentstructure != structureid)
                        {
                            continue;
                        }
                    }
                }

                if (acceptedStructuretypes == null)
                {
                    if (structure.Id != structureid && iterationLimit > 0)
                    {
                        siblings.AddRange(GetStructuresSiblings(structure.Id, null, date, iterationLimit - 1));
                    }
                } else
                {
                    if (structure.Id != structureid && acceptedStructuretypes.Contains(structureid) && iterationLimit > 0)
                    {
                        siblings.AddRange(GetStructuresSiblings(structure.Id, acceptedStructuretypes, date, iterationLimit - 1));
                    }
                }
                
            }
            //return siblings;
            List<Structure> nondeletedStructures = new List<Structure>();
            List<int> structureids = siblings.Distinct().ToList();

            foreach (int id in structureids)
            {
                Structure structureFinded = StructuresLocal().GetValue(id);
                if (structureFinded != null)
                {
                    nondeletedStructures.Add(structureFinded);
                }
            }
            nondeletedStructures = FilterDeletedStructures(nondeletedStructures, date.GetValueOrDefault()).ToList();
            structureids = nondeletedStructures.Select(s => s.Id).ToList();

            return structureids;
        }

        public List<Structure> GetChildrenOfParents(List<int> parents, DateTime? date = null)
        {
            List<Structure> structures = new List<Structure>();
            foreach (int i in parents)
            {
                if (i >= 0)
                {
                    structures.AddRange(GetChildrenAndSelfOrigin(i));
                } else
                {
                    /**
                     * If below 0 it means top structure (without parents) without displaying children.  
                     */
                    structures.AddRange(GetSelfOrigin(i));
                }
                
            }
            structures = structures.GroupBy(x => x.Id).Select(x => x.First()).ToList<Structure>();
            return structures;
        }

        public bool HasAnyChild(int structureID, DateTime date)
        {

            Structure structure = StructuresLocal()[structureID];
            if (structure.Changeorigin > 0)
            {
                structureID = structure.Changeorigin;
            }
            
            foreach (Structure str in StructuresLocal().Values)
            {
                
                if (str.Parentstructure == structureID)
                {
                    Structure actualStructure = GetActualStructureInfo(str, date);
                    if (actualStructure != null && actualStructure.Parentstructure == structureID)
                    {
                        return true;
                    }
                    //return true;
                }
                //if (str.Parentstructure == structureID)
                //{
                //    return true;
                //}
            }
            return false;
        }

        public string GetGrandParentName(int structureID, DateTime? dateTime = null)
        {
            bool anyparent = false;
            bool parentsavailable = true;
            string name = "";
            while (parentsavailable)
            {
                Structure structure = dateTime == null ? null : GetActualStructureInfo(structureID, dateTime.GetValueOrDefault());
                if (StructuresLocal().ContainsKey(structureID) && dateTime == null){
                    structure = StructuresLocal()[structureID];
                }
                if (structure == null)
                {
                    anyparent = false;
                    break;
                }
                name = structure.Name;
                if (structure.Parentstructure > 0)
                {
                    anyparent = true;
                    structureID = structure.Parentstructure;
                } else
                {
                    parentsavailable = false;
                }
            }

            if (anyparent)
            {
                return name;
            } else
            {
                return "";
            }
            
        }

        /**
         * True if structureParent is elder of structureChild.
         * False if they are not connected.
         * Если айди совпадают, возвращает false
         */ 
        public bool IsGrandParent(int structureChildID, int structureParentID)
        {
            if (structureChildID < 0)
            {
                structureChildID = -structureChildID;
            }
            if (structureParentID < 0)
            {
                structureParentID = -structureParentID;
            }
            if (structureChildID == structureParentID)
            {
                return false;
            }
            bool hasParent = true;
            Structure child = null;
            if (StructuresLocal() == null)
            {
                UpdateStructuresLocal();
            }
            if (StructuresLocal().ContainsKey(structureChildID))
            {
                child = StructuresLocal()[structureChildID];
            }
            //Structure child = Structures.FirstOrDefault(s => s.Id == structureChildID);
            if (child == null || child.Parentstructure == 0)
            {
                return false;
            }
            Structure grandparent = null;
            Structure parent = null;
            if (StructuresLocal().ContainsKey(structureParentID))
            {
                grandparent = StructuresLocal()[structureParentID];
            }
            if (StructuresLocal().ContainsKey(child.Parentstructure))
            {
                parent = StructuresLocal()[child.Parentstructure];
            }
            //Structure grandparent = Structures.FirstOrDefault(s => s.Id == structureParentID);
            //Structure parent = Structures.FirstOrDefault(s => s.Id == child.Parentstructure);
            if (grandparent == null || parent == null)
            {
                return false;
            }
            while (hasParent)
            {
                if (parent.Id == grandparent.Id)
                {
                    return true;
                }
                if (parent.Parentstructure == 0)
                {
                    return false;
                }

                if (StructuresLocal().ContainsKey(parent.Parentstructure))
                {
                    parent = StructuresLocal()[parent.Parentstructure];
                }
                //parent = Structures.FirstOrDefault(s => s.Id == parent.Parentstructure);
            }
            return false;
        }

        public IEnumerable<Structure> GetChildren(int structureid)
        {
            return StructuresLocal().Values.Where(s => s.Parentstructure == structureid);
        }

        public List<Structure> GetChildrenList(int structureid)
        {
            return StructuresLocal().Values.Where(s => s.Parentstructure == structureid).ToList();
            //return Structures.Where(s => s.Parentstructure == structureid).ToList();
        }

        public IEnumerable<Structure> GetChildrenAndSelf(int structureid)
        {
            return StructuresLocal().Values.Where(s => s.Parentstructure == structureid || s.Id == structureid);
            //return Structures.Where(s => s.Parentstructure == structureid || s.Id == structureid);
        }

        public IEnumerable<Structure> GetChildrenAndSelfOrigin(int structureid)
        {
            Structure structure = StructuresLocal()[structureid];
            if (structure.Changeorigin > 0)
            {
                structureid = structure.Changeorigin;
            }
            return StructuresLocal().Values.Where(s => s.Parentstructure == structureid || s.Id == structureid);
            //return Structures.Where(s => s.Parentstructure == structureid || s.Id == structureid);
        }

        public void GetChildrenAndSelfOrigin(int structureid, List<Structure> all_finds_struct)
        {
            var k = GetStructuresSiblings(structureid);
            foreach (int i in GetStructuresSiblings(structureid)) {
                all_finds_struct.Add(GetOriginalStructure(i));
                /*foreach (Structure j in StructuresLocal().Values.Where(s => s.Parentstructure == GetOriginalStructure(i).Id))
                {
                    all_finds_struct.Add(j);
                }*/
            }
            return;
            /*List<int> new_structfor_find = new List<int>();
            foreach(int i in structureid_for_find)
            {
                all_finds_struct.Add(GetOriginalStructure(i));
                foreach(Structure j in GetChildren(i))
                {
                    new_structfor_find.Add(j.Id);
                }
            }*/
        }

        public IEnumerable<Structure> GetSelf(int structureid)
        {
            if (structureid < 0)
            {
                structureid = -structureid;
            }
            List<Structure> structure = new List<Structure>();
            structure.Add(StructuresLocal()[structureid]);
            return structure;
            //return Structures.Where(s => s.Id == structureid);
        }

        /**
         * Returns structure or origin of structure if it is not origin
         */
        public IEnumerable<Structure> GetSelfOrigin(int structureid)
        {
            if (structureid < 0)
            {
                structureid = -structureid;
            }
            List<Structure> structureList = new List<Structure>();
            Structure structure = StructuresLocal()[structureid];
            if (structure.Changeorigin != 0)
            {
                structure = StructuresLocal()[structure.Changeorigin];
            }
            structureList.Add(structure);
            return structureList;
            //return Structures.Where(s => s.Id == structureid);
        }

        public Structure GetSelfAsElement(int structureid)
        {
            if (structureid < 0)
            {
                structureid = -structureid;
            }

            return StructuresLocal()[structureid];
            //return Structures.Where(s => s.Id == structureid).FirstOrDefault();
        }



        /**
         *  Get decreeoperations with detailed info.
         */
        public IEnumerable<DecreeoperationManagement> GetDecreeoperationManagement(int decreeid, bool disabletracking = false)
        {
            if (disabletracking)
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            Dictionary<int, Structure> structuresLocal = new Dictionary<int, Structure>();

            Decree decree = DecreesLocal()[decreeid];

            List<Decreeoperation> decreeoperations = DecreeoperationsLocal().Values.Where(d => d.Decree == decreeid).ToList();
            List<DecreeoperationManagement> decreeoperationManagements = new List<DecreeoperationManagement>();
            
            foreach (Decreeoperation decreeoperation in decreeoperations)
            {
                try
                {
                    DecreeoperationManagement decreeoperationManagement = new DecreeoperationManagement();
                    decreeoperationManagement.Decree = decreeoperation.Decree;
                    decreeoperationManagement.Id = decreeoperation.Id;
                    decreeoperationManagement.Subject = decreeoperation.Subject;
                    decreeoperationManagement.Changedtype = decreeoperation.Changedtype;
                    decreeoperationManagement.Deleted = decreeoperation.Deleted;
                    decreeoperationManagement.Created = decreeoperation.Created;
                    decreeoperationManagement.Changed = decreeoperation.Changed;
                    decreeoperationManagement.Datecustom = decreeoperation.Datecustom;
                    decreeoperationManagement.Dateactive = decreeoperation.Dateactive;


                    // 1 (or undefined) - position, 2 - department, 3 - structure
                    if (decreeoperationManagement.Subject > 0)
                    {
                            decreeoperationManagement.DecreeConnectionTypeDescr = "Должность";
                        Position position = PositionsLocal().GetValueOrDefault(decreeoperationManagement.Subject);
                        if (position != null)
                        {
                            decreeoperationManagement.DecreeSubjectNameDescr = PositiontypesLocal()[position.Positiontype].Name;
                        }
                    } else { 
                            decreeoperationManagement.DecreeConnectionTypeDescr = "Подразделение";
                            int rightid = decreeoperationManagement.Subject;
                            if (rightid < 0)
                            {
                                rightid = -rightid;
                            }
                            Structure structure = StructuresLocal().GetValue(rightid);
                            //Structure structure = Structures.FirstOrDefault(s => s.Id == decreeoperationManagement.Subject);
                            if (structure != null)
                            {
                                decreeoperationManagement.DecreeSubjectNameDescr = structure.Name;
                            } else
                            {
                                continue;
                            }
                        
                    }
                    if (decreeoperationManagement.Deleted == 1)
                    {
                        decreeoperationManagement.DecreeActionDescr = "Упразднение";
                    } else if (decreeoperationManagement.Created == 1)
                    {
                        decreeoperationManagement.DecreeActionDescr = "Введение";
                    } else if (decreeoperationManagement.Changed == 1)
                    {
                        {
                            decreeoperationManagement.DecreeActionDescr = "Изменение";
                        }
                    
                    }
                    //Get tree
                    decreeoperationManagement.Tree = FormTree(decreeoperationManagement, false); 
                    //decreeoperationManagement.FullTree = FormTree(decreeoperationManagement, true);
                    if (decreeoperationManagement.Tree.Length > 0)
                    {
                        decreeoperationManagement.FullTree = decreeoperationManagement.Tree + Keys.TREE_BEAUTY + decreeoperationManagement.DecreeSubjectNameDescr;
                    } else
                    {
                        decreeoperationManagement.FullTree = decreeoperationManagement.DecreeSubjectNameDescr;
                    }
                
                    decreeoperationManagements.Add(decreeoperationManagement);
                } catch (Exception ex)
                {

                }
            }
            /**
             * Sorting decreeoperation managements.
             */
            decreeoperationManagements = SortDecreeoperationManagementsByTrees(decreeoperationManagements); //// HERE VISUAL TREE
            return decreeoperationManagements;
        }

        public IEnumerable<Decreeoperation> GetDecreeoperations(int decreeid)
        {
            return DecreeoperationsLocal().Values.Where(d => d.Decree == decreeid);
        }

        /**
         * Form hierarchy tree for element. For example, for position: structure -> department -> position.
         * Or structure -> structure -> department -> subdepartment -> position
         * If fulltree == true, name of subject will be included.
         */
        public string FormTree(DecreeoperationManagement decreeoperationManagement, bool fullTree)
        {
            string tree = "";
             
            if (fullTree)
            {
                tree = decreeoperationManagement.DecreeSubjectNameDescr + Keys.TREE_SEPARATOR + tree;
            }
            DecreeoperationManagement elementForTree = GetParent(decreeoperationManagement);
            if (elementForTree == null)
            {
                return "";
            }

            while (!string.IsNullOrEmpty(elementForTree.DecreeSubjectNameDescr))
            {
                tree = elementForTree.DecreeSubjectNameDescr + Keys.TREE_SEPARATOR + tree;
                //elementForTree.DecreeSubjectNameDescr = "";
                // Find new decreeoperationManagement
                elementForTree = GetParent(elementForTree); // Ошибка где-то здесь
            }
            if (tree.Length > 0)
            {
                tree = tree.Remove(tree.Length - 1);
            }
            tree = BeautifyTree(tree);
            return tree;
        }

        public string FormTree(Structure structure, bool fullTree, DateTime date, bool beautifyTree = true)
        {
            string tree = "";
            if (fullTree)
            {
                tree = GetStructureName(structure, date) + Keys.TREE_SEPARATOR + tree;
            }
            structure = GetActualStructureInfo(structure, date); // 18.09.2020 добавили, чтобы в дереве находило предка по актуальному состоянию подразделения
            Structure elementForTree = GetParent(structure);
            while (elementForTree != null)
            {
                elementForTree = GetActualStructureInfo(elementForTree, date); // 18.09.2020 добавили, чтобы в дереве находило предка по актуальному состоянию подразделения
                tree = GetStructureName(elementForTree, date) + Keys.TREE_SEPARATOR + tree;

                // Find new decreeoperationManagement
                elementForTree = GetParent(elementForTree);
            }
            if (tree.Length > 0)
            {
                tree = tree.Remove(tree.Length - 1);
            }
            if (beautifyTree)
            {
                tree = BeautifyTree(tree);
            }
            
            return tree;
        }

        public string FormTree(Position position, DateTime date, bool beautifyTree = true)
        {
            Structure structure = StructuresLocal().GetValue(position.Structure);
            if (structure != null)
            {
                return FormTree(structure, true, date, beautifyTree);
            } else
            {
                return "";
            }
            
        }

        /**
         * Get actual structure name.
         */
        public string GetStructureName(Structure structure, DateTime date)
        {
            if (structure == null)
            {
                return null;
            }
            Structure actualStructure = GetActualStructureInfo(structure, date);
            if (actualStructure == null)
            {
                return null;
            }
            return actualStructure.Name;
        }

        /// <summary>
        /// УСТАРЕЛО
        /// Отобажение иерархии структуры подразделений в родительном падеже для отображения в документах и прочем.
        /// Название первого подразделения также в именительном падеже.
        /// </summary>
        /// <param name="structure"></param>
        /// <param name="fullTree"></param>
        /// <param name="date"></param>
        /// <param name="beautifyTree"></param>
        /// <returns></returns>
        public string FormTreeDocument1(Structure structure, bool fullTree, DateTime date, bool beautifyTree = true)
        {
            string tree = "";
            if (fullTree)
            {
                //tree = GetStructureName1(structure, date) + "," + tree;
                tree = tree + " " + GetStructureName1(structure, date);
            }
            Structure elementForTree = GetParent(structure);
            while (elementForTree != null)
            {
                tree = tree + " " + GetStructureName2(elementForTree, date);

                // Find new decreeoperationManagement
                elementForTree = GetParent(elementForTree);
            }
            //if (tree.Length > 0)
            //{
            //    tree = tree.Remove(tree.Length - 1);
            //}
            //if (beautifyTree) - Не имеет смысла, так как уже в нужном виде
            //{
            //    tree = BeautifyTree(tree);
            //}

            return tree;
        }

        /// <summary>
        /// Отобажение иерархии структуры подразделений в родительном падеже для отображения в документах и прочем.
        /// Название первого подразделения также в родительном падеже.
        /// </summary>
        /// <param name="structure"></param>
        /// <param name="fullTree"></param>
        /// <param name="date"></param>
        /// <param name="beautifyTree"></param>
        /// <returns></returns>
        public string FormTreeDocument2(Structure structure, bool fullTree, DateTime date, bool beautifyTree = true)
        {
            string tree = "";
            if (fullTree)
            {
                //tree = GetStructureName2(structure, date) + "," + tree;
                tree = tree + " " + GetStructureName2(structure, date);
            }
            Structure elementForTree = GetParent(structure);
            while (elementForTree != null)
            {
                tree = tree + " " + GetStructureName2(elementForTree, date);

                // Find new decreeoperationManagement
                elementForTree = GetParent(elementForTree);
            }
            //if (tree.Length > 0)
            //{
            //    tree = tree.Remove(tree.Length - 1);
            //}
            //if (beautifyTree) - Не имеет смысла, так как уже в нужном виде
            //{
            //    tree = BeautifyTree(tree);
            //}

            return tree;
        }

        /// <summary>
        /// Вывод актуального названия подразделения в именительном падеже.
        /// </summary>
        /// <param name="structure"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public string GetStructureName1(Structure structure, DateTime date)
        {
            if (structure == null)
            {
                return null;
            }
            Structure actualStructure = GetActualStructureInfo(structure, date);
            if (actualStructure == null)
            {
                return null;
            }
            return actualStructure.Name1;
        }

        /// <summary>
        /// Вывод актуального названия подразделения в родительном падеже.
        /// </summary>
        /// <param name="structure"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public string GetStructureName2(Structure structure, DateTime date)
        {
            if (structure == null)
            {
                return null;
            }
            Structure actualStructure = GetActualStructureInfo(structure, date);
            if (actualStructure == null)
            {
                return null;
            }
            return actualStructure.Name2;
        }

        /// <summary>
        /// Возвращает наименование должности и/или полное наименование подразделения для документа в необходимом падеже
        /// </summary>
        /// <param name="structure">Подразделение. Если нет, будет просто должность </param>
        /// <param name="date"></param>
        /// <param name="position">Должность. Если нет, будет просто подразделение</param>
        /// <param name="padezh">от 1 до 6. 1 - именительный, 2 - родительный, 3 - дательный, 4 - винительный, 5 - творительный, 6 - предложный</param>
        /// <param name="decreeCreator">Объект пользователя-создателя приказа. Нужен для обрезания иерархической цепочки структур (если областной приказ, то Республику не пишет). Оставить пустым, если выводить всегда все</param>
        /// <returns></returns>
        public string FormTreeDocument(Structure structure, DateTime date, Position position, int padezh, User decreeCreator)
        {
            string tree = "";

            // Если есть должность
            if (position != null)
            {
                // Вносим куски должностей
                List<Subject> subjects = new List<Subject>();
                Subject subject = null;
                if (position.Subject1 > 0)
                {
                    subject = SubjectsLocal().GetValue(position.Subject1);
                    if (subject != null)
                    {
                        subjects.Add(subject);
                        subject = null;
                    }
                }
                if (position.Subject2 > 0)
                {
                    subject = SubjectsLocal().GetValue(position.Subject2);
                    if (subject != null)
                    {
                        subjects.Add(subject);
                        subject = null;
                    }
                }
                if (position.Subject3 > 0)
                {
                    subject = SubjectsLocal().GetValue(position.Subject3);
                    if (subject != null)
                    {
                        subjects.Add(subject);
                        subject = null;
                    }
                }
                if (position.Subject4 > 0)
                {
                    subject = SubjectsLocal().GetValue(position.Subject4);
                    if (subject != null)
                    {
                        subjects.Add(subject);
                        subject = null;
                    }
                }
                if (position.Subject5 > 0)
                {
                    subject = SubjectsLocal().GetValue(position.Subject5);
                    if (subject != null)
                    {
                        subjects.Add(subject);
                        subject = null;
                    }
                }
                if (position.Subject6 > 0)
                {
                    subject = SubjectsLocal().GetValue(position.Subject6);
                    if (subject != null)
                    {
                        subjects.Add(subject);
                        subject = null;
                    }
                }
                if (position.Subject7 > 0)
                {
                    subject = SubjectsLocal().GetValue(position.Subject7);
                    if (subject != null)
                    {
                        subjects.Add(subject);
                        subject = null;
                    }
                }
                if (position.Subject8 > 0)
                {
                    subject = SubjectsLocal().GetValue(position.Subject8);
                    if (subject != null)
                    {
                        subjects.Add(subject);
                        subject = null;
                    }
                }
                if (position.Subject9 > 0)
                {
                    subject = SubjectsLocal().GetValue(position.Subject9);
                    if (subject != null)
                    {
                        subjects.Add(subject);
                        subject = null;
                    }
                }
                if (position.Subject10 > 0)
                {
                    subject = SubjectsLocal().GetValue(position.Subject10);
                    if (subject != null)
                    {
                        subjects.Add(subject);
                        subject = null;
                    }
                }

                foreach (Subject subjectInList in subjects)
                {
                    string subjectname = "";
                    if (padezh == 0 || padezh == 1)
                    {
                        subjectname = subjectInList.Name1;
                    }
                    if (padezh == 2)
                    {
                        subjectname = subjectInList.Name2;
                    }
                    if (padezh == 3)
                    {
                        subjectname = subjectInList.Name3;
                    }
                    if (padezh == 4)
                    {
                        subjectname = subjectInList.Name4;
                    }
                    if (padezh == 5)
                    {
                        subjectname = subjectInList.Name5;
                    }
                    if (padezh == 6)
                    {
                        subjectname = subjectInList.Name6;
                    }
                    // Если полное наименование должности с подразделением, то выполняется проверка, нужно ли слово отбрасывать (как "отдела") 
                    if (subjectInList.Dropword > 0 && structure != null)
                    {

                    } else
                    {
                        tree = tree + " " + subjectname;
                    }
                    
                }
            }

            // Если есть подразделение
            if (structure != null)
            {
                if (position == null)
                {
                    tree = tree + " " + GetStructureNameDocument(structure, date, padezh, decreeCreator); // Если должности нет, то первое подразделение в указанном падеже
                }
                else
                {
                    tree = tree + " " + GetStructureNameDocument(structure, date, 2, decreeCreator); // Если есть должность, то первое подразделение будет в родительном падеже
                }

                Structure elementForTree = GetParent(structure);
                while (elementForTree != null)
                {
                    tree = tree + " " + GetStructureNameDocument(elementForTree, date, 2, decreeCreator); // Все подразделения после первого будут в родительном падеже

                    // Find new decreeoperationManagement
                    elementForTree = GetParent(elementForTree);
                }
            }


            // Если должность составная (есть вторая часть), то мы добавляем после подразделения
            if (position != null)
            { 
                // Вносим куски должностей
                List<Subject> subjects = new List<Subject>();
                Subject subject = null;
                if (position.Subject11 > 0)
                {
                    subject = SubjectsLocal().GetValue(position.Subject11);
                    if (subject != null)
                    {
                        subjects.Add(subject);
                        subject = null;
                    }
                }
                if (position.Subject12 > 0)
                {
                    subject = SubjectsLocal().GetValue(position.Subject12);
                    if (subject != null)
                    {
                        subjects.Add(subject);
                        subject = null;
                    }
                }
                if (position.Subject13 > 0)
                {
                    subject = SubjectsLocal().GetValue(position.Subject13);
                    if (subject != null)
                    {
                        subjects.Add(subject);
                        subject = null;
                    }
                }
                if (position.Subject14 > 0)
                {
                    subject = SubjectsLocal().GetValue(position.Subject14);
                    if (subject != null)
                    {
                        subjects.Add(subject);
                        subject = null;
                    }
                }
                if (position.Subject15 > 0)
                {
                    subject = SubjectsLocal().GetValue(position.Subject15);
                    if (subject != null)
                    {
                        subjects.Add(subject);
                        subject = null;
                    }
                }
                if (position.Subject16 > 0)
                {
                    subject = SubjectsLocal().GetValue(position.Subject16);
                    if (subject != null)
                    {
                        subjects.Add(subject);
                        subject = null;
                    }
                }
                if (position.Subject17 > 0)
                {
                    subject = SubjectsLocal().GetValue(position.Subject17);
                    if (subject != null)
                    {
                        subjects.Add(subject);
                        subject = null;
                    }
                }
                if (position.Subject18 > 0)
                {
                    subject = SubjectsLocal().GetValue(position.Subject18);
                    if (subject != null)
                    {
                        subjects.Add(subject);
                        subject = null;
                    }
                }
                if (position.Subject19 > 0)
                {
                    subject = SubjectsLocal().GetValue(position.Subject19);
                    if (subject != null)
                    {
                        subjects.Add(subject);
                        subject = null;
                    }
                }
                if (position.Subject20 > 0)
                {
                    subject = SubjectsLocal().GetValue(position.Subject20);
                    if (subject != null)
                    {
                        subjects.Add(subject);
                        subject = null;
                    }
                }

                foreach (Subject subjectInList in subjects)
                {
                    string subjectname = "";
                    if (padezh == 0 || padezh == 1)
                    {
                        subjectname = subjectInList.Name1;
                    }
                    if (padezh == 2)
                    {
                        subjectname = subjectInList.Name2;
                    }
                    if (padezh == 3)
                    {
                        subjectname = subjectInList.Name3;
                    }
                    if (padezh == 4)
                    {
                        subjectname = subjectInList.Name4;
                    }
                    if (padezh == 5)
                    {
                        subjectname = subjectInList.Name5;
                    }
                    if (padezh == 6)
                    {
                        subjectname = subjectInList.Name6;
                    }
                    tree = tree + " " + subjectname;
                }

            }

            tree = Regex.Replace(tree, @"\s+", " "); 
            tree = tree.Trim();
            return tree;
        }

        /// <summary>
        /// Возвращает название подразделения для документа в указанном падеже
        /// </summary>
        /// <param name="structure"></param>
        /// <param name="date"></param>
        /// <param name="padezh">от 1 до 6. 1 - именительный, 2 - родительный, 3 - дательный, 4 - винительный, 5 - творительный, 6 - предложный</param>
        /// <param name="decreeCreator">Объект пользователя-создателя приказа. Нужен для обрезания иерархической цепочки структур (если областной приказ, то Республику не пишет). Оставить пустым, если выводить всегда все</param>
        /// <returns></returns>
        public string GetStructureNameDocument(Structure structure, DateTime date, int padezh, User decreeCreator)
        {
            if (structure == null)
            {
                return null;
            }
            Structure actualStructure = GetActualStructureInfo(structure, date);
            if (actualStructure == null)
            {
                return null;
            }

            /**
             * Составляем список из всех частей наименования подразделения
             */
            List<Subject> subjects = new List<Subject>();
            Subject subject = null;
            if (actualStructure.Subject1 > 0)
            {
                if (SubjectsLocal() == null)
                {
                    UpdateSubjectsLocal();
                }
                subject = SubjectsLocal().GetValue(actualStructure.Subject1);
                if (subject != null)
                {
                    subjects.Add(subject);
                    subject = null;
                }
            }
            if (actualStructure.Subject2 > 0)
            {
                subject = SubjectsLocal().GetValue(actualStructure.Subject2);
                if (subject != null)
                {
                    subjects.Add(subject);
                    subject = null;
                }
            }
            if (actualStructure.Subject3 > 0)
            {
                subject = SubjectsLocal().GetValue(actualStructure.Subject3);
                if (subject != null)
                {
                    subjects.Add(subject);
                    subject = null;
                }
            }
            if (actualStructure.Subject4 > 0)
            {
                subject = SubjectsLocal().GetValue(actualStructure.Subject4);
                if (subject != null)
                {
                    subjects.Add(subject);
                    subject = null;
                }
            }
            if (actualStructure.Subject5 > 0)
            {
                subject = SubjectsLocal().GetValue(actualStructure.Subject5);
                if (subject != null)
                {
                    subjects.Add(subject);
                    subject = null;
                }
            }
            if (actualStructure.Subject6 > 0)
            {
                subject = SubjectsLocal().GetValue(actualStructure.Subject6);
                if (subject != null)
                {
                    subjects.Add(subject);
                    subject = null;
                }
            }
            if (actualStructure.Subject7 > 0)
            {
                subject = SubjectsLocal().GetValue(actualStructure.Subject7);
                if (subject != null)
                {
                    subjects.Add(subject);
                    subject = null;
                }
            }
            if (actualStructure.Subject8 > 0)
            {
                subject = SubjectsLocal().GetValue(actualStructure.Subject8);
                if (subject != null)
                {
                    subjects.Add(subject);
                    subject = null;
                }
            }
            if (actualStructure.Subject9 > 0)
            {
                subject = SubjectsLocal().GetValue(actualStructure.Subject9);
                if (subject != null)
                {
                    subjects.Add(subject);
                    subject = null;
                }
            }
            if (actualStructure.Subject10 > 0)
            {
                subject = SubjectsLocal().GetValue(actualStructure.Subject10);
                if (subject != null)
                {
                    subjects.Add(subject);
                    subject = null;
                }
            }
            if (actualStructure.Subject11 > 0)
            {
                subject = SubjectsLocal().GetValue(actualStructure.Subject11);
                if (subject != null)
                {
                    subjects.Add(subject);
                    subject = null;
                }
            }
            if (actualStructure.Subject12 > 0)
            {
                subject = SubjectsLocal().GetValue(actualStructure.Subject12);
                if (subject != null)
                {
                    subjects.Add(subject);
                    subject = null;
                }
            }
            if (actualStructure.Subject13 > 0)
            {
                subject = SubjectsLocal().GetValue(actualStructure.Subject13);
                if (subject != null)
                {
                    subjects.Add(subject);
                    subject = null;
                }
            }
            if (actualStructure.Subject14 > 0)
            {
                subject = SubjectsLocal().GetValue(actualStructure.Subject14);
                if (subject != null)
                {
                    subjects.Add(subject);
                    subject = null;
                }
            }
            if (actualStructure.Subject15 > 0)
            {
                subject = SubjectsLocal().GetValue(actualStructure.Subject15);
                if (subject != null)
                {
                    subjects.Add(subject);
                    subject = null;
                }
            }

            string structurename = "";
            foreach (Subject subjectInList in subjects)
            {
                string subjectname = "";
                if (padezh == 0 || padezh == 1)
                {
                    subjectname = subjectInList.Name1;
                }
                if (padezh == 2)
                {
                    subjectname = subjectInList.Name2;
                }
                if (padezh == 3)
                {
                    subjectname = subjectInList.Name3;
                }
                if (padezh == 4)
                {
                    subjectname = subjectInList.Name4;
                }
                if (padezh == 5)
                {
                    subjectname = subjectInList.Name5;
                }
                if (padezh == 6)
                {
                    subjectname = subjectInList.Name6;
                }
                structurename = structurename + " " + subjectname;
            }
            if (actualStructure.Subjectnumber > 0)
            {
                structurename = structurename + " № " + actualStructure.Subjectnumber.ToString();
            }
            if (actualStructure.Subjectnotice != null && actualStructure.Subjectnotice.Length > 0)
            {
                structurename = structurename + " " + actualStructure.Subjectnotice;
            }

            structurename = structurename.Trim();
            return structurename;
            //return actualStructure.Name1;
        }


        /**
         * Get name of parent element of decree subject if available.
         */
        public DecreeoperationManagement GetParent(DecreeoperationManagement decreeoperationManagement)
        {
            DecreeoperationManagement parentDom = new DecreeoperationManagement();
            /**
             * Contains parent name;
             */
            parentDom.DecreeSubjectNameDescr = "";
            if (decreeoperationManagement.Subject > 0)
            {

                //decreeoperationManagement.DecreeSubjectNameDescr = Departments.FirstOrDefault(d => d.Id == Positions.First(p => p.Id == decreeoperationManagement.Subject).Department).Name;
                Position position = PositionsLocal().GetValueOrDefault(decreeoperationManagement.Subject);
                Structure structure = null;
                if (position != null)
                {
                        Structure cached = null;
                        if (StructuresLocal().ContainsKey(position.Structure))
                        {
                            cached = StructuresLocal()[position.Structure];
                            structure = cached;
                        }
                        else
                        {
                            structure = StructuresLocal().GetValue(position.Structure);
                            //structure = Structures.FirstOrDefault(d => d.Id == position.Structure);
                            if (structure != null)
                            {
                                StructuresLocal().Add(structure.Id, structure);
                            }

                        }
                    if (structure != null)
                    {
                        parentDom.DecreeSubjectNameDescr = structure.Name;
                        parentDom.Subject = -structure.Id;
                    }
                }
                
            } else if (decreeoperationManagement.Subject < 0)
            {
                Structure parentStructure = null;
                int structureid = -decreeoperationManagement.Subject; // because it is negative
                Structure structure = Structures.First(p => p.Id == structureid); // ошибка тут
                Structure cached = null;
                if (StructuresLocal().ContainsKey(structure.Parentstructure))
                {
                    cached = StructuresLocal()[structure.Parentstructure];
                    parentStructure = cached;
                }
                else
                {
                    parentStructure = StructuresLocal().GetValue(structure.Parentstructure);
                    //parentStructure = Structures.FirstOrDefault(d => d.Id == structure.Parentstructure.GetValueOrDefault());
                    if (parentStructure != null)
                    {
                        StructuresLocal().Add(parentStructure.Id, parentStructure);
                    }

                }

                if (parentStructure != null)
                {
                    parentDom.DecreeSubjectNameDescr = parentStructure.Name;
                    parentDom.Subject = -parentStructure.Id;
                }
            }
            return parentDom;
        }

        public void Paste(int structureid, int pasteid, User user)
        {
            Structure structure = StructuresLocal()[structureid];
            if (structure.Changeorigin > 0)
            {
                structureid = structure.Changeorigin;
            }

            structure = StructuresLocal()[pasteid];
            if (structure.Changeorigin > 0)
            {
                pasteid = structure.Changeorigin;
            }

            /**
             * Clone structures.
             */
            if (structureid < 0)
            {
                structureid = -structureid;
            }
            List<Structure> structures = new List<Structure>();
            List<Structure> newStructures = new List<Structure>();
            List<KeyValue<int, int>> IdsAndParentIds = new List<KeyValue<int, int>>();
            Dictionary<int, int> oldIdsAndNewIds = new Dictionary<int, int>();
            //List<int> structuresInts = GetStructuresSiblingsWithSameOrNullType(structureid);
            List<int> structuresInts = GetStructuresSiblings(structureid);
            foreach (int i in structuresInts)
            {
                Structure findedStructure = Structures.First(s => s.Id == i);
                structures.Add(findedStructure);

                IdsAndParentIds.Add(new KeyValue<int, int>(findedStructure.Id, findedStructure.Parentstructure));
                Structure clonedStructure = CloneStructure(findedStructure, user);
                //clonedStructure.Id = 0;
                //clonedStructure.Parentstructure = 0;
                newStructures.Add(clonedStructure);
                //context.Structure.Add(clonedStructure);
            }
            context.SaveChanges();

            int index = 0;
            foreach (Structure cloned in newStructures)
            {
                if (IdsAndParentIds[index].Key == structureid)
                {
                    cloned.Parentstructure = pasteid;
                }
                else
                {
                    Structure parent = newStructures[IdsAndParentIds.FindIndex(ip => ip.Key == IdsAndParentIds[index].Value)];
                    cloned.Parentstructure = parent.Id;
                }
                oldIdsAndNewIds.Add(IdsAndParentIds[index].Key, cloned.Id);

                index++;
            }


            /**
             * Clone positions
             */
            foreach(KeyValue<int,int> oldnew in IdsAndParentIds)
            {
               foreach (Position position in GetPositions(-oldnew.Key, user.Date.GetValueOrDefault(), false, false))
               {
                    //AddPosition
                    Position clonedPosition = ClonePosition(position, user);
                    clonedPosition.Structure = oldIdsAndNewIds[position.Structure];
               }
            } 
            context.SaveChanges();
            
        }

        public Structure GetParent(Structure structure, Dictionary<int, Structure> structuresLocal = null)
        {
            if (structure.Parentstructure == 0)
            {
                return null;
            }
            Structure parentStructure = null;
            if (structuresLocal == null)
            {
                parentStructure = StructuresLocal().GetValue(structure.Parentstructure);
                //parentStructure = Structures.FirstOrDefault(d => d.Id == structure.Parentstructure.GetValueOrDefault());
            }
            else
            {
                Structure cached = null;
                if (structuresLocal.ContainsKey(structure.Parentstructure))
                {
                    cached = structuresLocal[structure.Parentstructure];
                    parentStructure = cached;
                }
                else
                {
                    parentStructure = StructuresLocal().GetValue(structure.Parentstructure);
                    //parentStructure = Structures.FirstOrDefault(d => d.Id == structure.Parentstructure.GetValueOrDefault());
                    if (parentStructure != null)
                    {
                        structuresLocal.Add(parentStructure.Id, parentStructure);
                    }

                }
            }
            return parentStructure;
            //return Structures.FirstOrDefault(s => s.Id == structure.Parentstructure.GetValueOrDefault());
        }

        /**
         * Sorts decree operations in decree according to hierarchy. Sorts by structures, departments, subdepartments etc. 
         * Also provides rank value for each dom. Lower in hierarchy - higher rank, starts from 0
         */
        public List<DecreeoperationManagement> SortDecreeoperationManagementsByTrees(List<DecreeoperationManagement> doms)
        {
            List<DecreeoperationManagement> sortedDoms = new List<DecreeoperationManagement>();
            int currentRank = 0;
            int maxRank = -5;
            int minRank = 100;
            //bool sorting = true;
            //string currentElement = null;
            //HashSet<string> usedNames = new HashSet<string>();
            
            foreach (DecreeoperationManagement domMaxRank in doms)
            {
                int getMaxRank = GetMaxRank(domMaxRank);
                if (maxRank < getMaxRank)
                {
                    maxRank = getMaxRank;
                }
                if (minRank > getMaxRank)
                {
                    minRank = getMaxRank;
                }
                domMaxRank.TreeRank = getMaxRank;
            }
            foreach (DecreeoperationManagement dom in doms)
            {
                dom.VisualRank = GetMaxRank(dom) - minRank;
                dom.SortTree = new string[maxRank + 1];
                dom.SortTreeInt = new int[maxRank + 1];
                dom.SortGroup = new long[maxRank +1];
                if (dom.FullTree == null)
                {
                    dom.FullTree = "";
                }
                string[] splittedTree = dom.FullTree.Split(Keys.TREE_BEAUTY);
                for (int i = 0; i < maxRank + 1; i++)
                {
                    if (splittedTree.Length > i)
                    {
                        dom.SortTree[i] = splittedTree[i];
                    } else
                    {
                        dom.SortTree[i] = "";
                    }
                    dom.SortTreeInt[i] = 0;
                    dom.SortGroup[i] = 0;
                }
            }

            while (currentRank <= maxRank)
            {
                Dictionary<long, List<DecreeoperationManagement>> grouppedDoms = new Dictionary<long, List<DecreeoperationManagement>>();
                foreach (DecreeoperationManagement dom in doms)
                {
                    dom.SortCurrentElement = dom.SortTree[currentRank];
                    if (dom.SortCurrentElement == null)
                    {
                        
                    }
                    if (currentRank > 0)
                    {
                        dom.SortPreviousGroup = dom.SortGroup[currentRank - 1];
                    }
                    else
                    {
                        dom.SortPreviousGroup = 0;
                    }
                    if (grouppedDoms.ContainsKey(dom.SortPreviousGroup))
                    {
                        grouppedDoms[dom.SortPreviousGroup].Add(dom);
                    } else
                    {
                        grouppedDoms.Add(dom.SortPreviousGroup, new List<DecreeoperationManagement>());
                        grouppedDoms[dom.SortPreviousGroup].Add(dom);
                    }
                }

                int count = 1;
                doms.Clear();


                foreach(KeyValuePair<long, List<DecreeoperationManagement>> pair in grouppedDoms)
                {
                    List<DecreeoperationManagement> sortByVisualRank = pair.Value.OrderBy(p => p.VisualRank).ToList();
                    pair.Value.Clear();
                    pair.Value.AddRange(sortByVisualRank);

                    List<DecreeoperationManagement> positionsToSort = new List<DecreeoperationManagement>();
                    //List<DecreeoperationManagement> samelevels = new List<DecreeoperationManagement>();
                    //List<int> samelevelsIndexed = new List<int>();
                    List<int> indexesToSort = new List<int>();
                    Dictionary<string, long> usedNames = new Dictionary<string, long>();
                    int index = 0;
                    foreach (DecreeoperationManagement dom in pair.Value)
                    {
                        if (usedNames.ContainsKey(dom.SortCurrentElement))
                        {
                            dom.SortGroup[currentRank] = usedNames[dom.SortCurrentElement];
                        } else
                        {
                            usedNames.Add(dom.SortCurrentElement, dom.SortPreviousGroup + Convert.ToInt64(Math.Pow(100, currentRank + 1)) * count);
                            dom.SortGroup[currentRank] = usedNames[dom.SortCurrentElement];
                            long val = dom.SortPreviousGroup + Convert.ToInt64(Math.Pow(100, currentRank + 1)) * count;
                            string name = dom.SortCurrentElement;
                            count++;
                        }
                        if (dom.TreeRank - 1 == currentRank)
                        {
                            positionsToSort.Add(dom);
                            indexesToSort.Add(index);
                        }
                        if (dom.TreeRank - 1 == currentRank)
                        {
                            long hash = 0;
                            for (int i = 0; i < currentRank; i++)
                            {
                                hash += dom.SortGroup[i];
                            }
                            dom.ParentHash = hash;
                        }
                        index++;
                    }
                    
                    /**
                     * sorting positions of the same level and block
                     */
                    if (positionsToSort.Count > 0)
                    {
                        positionsToSort = positionsToSort.OrderByDescending(p =>
                        {
                            if (p.Subject > 0)
                            {
                                return PositiontypesLocal()[PositionsLocal()[p.Subject].Positiontype].Priority;
                            } else
                            {
                                return -100;
                            }
                            
                        }
                        ).ToList();
                        //positionsToSort = positionsToSort.OrderByDescending(p => Positiontypes.First(pt => pt.Id == Positions.First(pos => pos.Id == p.Subject).Positiontype).Priority).ToList();
                        index = 0;
                        foreach(int i in indexesToSort)
                        {
                            pair.Value[i] = positionsToSort[index];
                            index++;
                        }
                    }

                    doms.AddRange(pair.Value);
                }
                currentRank++;
            }
            //doms = CompressPositions(doms);
            return doms;
            //return sortedDoms;
        }

        /**
         * Compress positions from the same department in decree list. 
         */
        public List<DecreeoperationManagement> CompressPositions(List<DecreeoperationManagement> decreeoperations)
        {
            List<DecreeoperationManagement> compressed = new List<DecreeoperationManagement>();
            Dictionary<long, List<int>> grouppedByHashes = new Dictionary<long, List<int>>();
            Dictionary<long, Dictionary<string, int>> compressorElements = new Dictionary<long, Dictionary<string, int>>();
            List<int> idsToRemove = new List<int>();


            int index = 0;
            foreach(DecreeoperationManagement dom in decreeoperations)
            {
                if (!grouppedByHashes.ContainsKey(dom.ParentHash))
                {
                    grouppedByHashes.Add(dom.ParentHash, new List<int>());
                }
                grouppedByHashes[dom.ParentHash].Add(index);
                index++;
            }
            foreach(KeyValuePair<long, List<int>> pair in grouppedByHashes)
            {
                foreach (int i in pair.Value)
                {
                    if (decreeoperations[i].Subject > 0)
                    {
                        if (!compressorElements.ContainsKey(pair.Key) || !compressorElements[pair.Key].ContainsKey(decreeoperations[i].DecreeSubjectNameDescr))
                        {
                            if (!compressorElements.ContainsKey(pair.Key)){
                                Dictionary<string, int> dict = new Dictionary<string, int>();
                                compressorElements.Add(pair.Key, dict);
                            }
                            compressorElements[pair.Key].Add(decreeoperations[i].DecreeSubjectNameDescr, i);
                            //dict.Add(decreeoperations[i].DecreeSubjectNameDescr, i);
                            
                            if (decreeoperations[i].Created > 0)
                            {
                                decreeoperations[i].CompressionAdded = 1;
                            }
                            if (decreeoperations[i].Deleted > 0)
                            {
                                decreeoperations[i].CompressionDeleted = 1;
                            }
                        } else
                        {
                            if (decreeoperations[i].Created > 0)
                            {
                                decreeoperations[compressorElements[pair.Key][decreeoperations[i].DecreeSubjectNameDescr]].CompressionAdded += 1;
                                idsToRemove.Add(i);
                                decreeoperations[i].CompressionAdded = -1;
                                decreeoperations[i].CompressionDeleted = -1;
                            }
                            if (decreeoperations[i].Deleted > 0)
                            {
                                decreeoperations[compressorElements[pair.Key][decreeoperations[i].DecreeSubjectNameDescr]].CompressionDeleted += 1;
                                idsToRemove.Add(i);
                                decreeoperations[i].CompressionAdded = -1;
                                decreeoperations[i].CompressionDeleted = -1;
                            }
                        }
                    }
                }
            }

            compressed.AddRange(decreeoperations);
            if (compressed.Count > 0)
            {
                for (int i = compressed.Count - 1; i >= 0; i--){
                    if (idsToRemove.Contains(i))
                    {
                        compressed.RemoveAt(i);
                    }
                }
            }
            return compressed;
        }



        /**
         * Get size of tree (max rank) of decreeoperation.
         */
        public int GetMaxRank(DecreeoperationManagement dom)
        {
            if (string.IsNullOrEmpty(dom.FullTree))
            {
                return -1;
            }
            if (dom.FullTree.Contains(Keys.TREE_BEAUTY))
            {
                return dom.FullTree.Split(Keys.TREE_BEAUTY).Length;
            } else
            {
                return dom.FullTree.Split(Keys.TREE_SEPARATOR).Length - 1;
            }
            
        }

        /**
         * Get name of element in tree which is located at current rank.
         */
        public string GetElementNameInTreeByRank(DecreeoperationManagement dom, int rank)
        {
            if (string.IsNullOrEmpty(dom.Tree))
            {
                return "";
            }
            string[] treeArray = dom.Tree.Split(Keys.TREE_SEPARATOR);
            if (rank >= treeArray.Length)
            {
                return "";
            }
            return treeArray[rank];
        }

        public string BeautifyTree(String tree)
        {
            StringBuilder beautifiedTree = new StringBuilder();
            string[] treeElement = tree.Split(Keys.TREE_SEPARATOR);
            foreach (string element in treeElement)
            {
                beautifiedTree.Append(element);
                beautifiedTree.Append(Keys.TREE_BEAUTY);

            }
            if (beautifiedTree.Length > 0)
            {
                beautifiedTree.Remove(beautifiedTree.Length - 3, 3);
            }
            return beautifiedTree.ToString();
        }



        /**
         * Returns array of mrd ids of position. , - separated.
         */
        public string GetMrds(int positionid)
        {
            string mrds = "";
            //PositionmrdsLocal
            foreach (Positionmrd positionmrd in PositionmrdsLocal().Values.Where(pm => pm.Position == positionid))
            //foreach (Positionmrd positionmrd in Positionmrds.Where(pm => pm.Position == positionid))
            {
                mrds += positionmrd.Mrd + ","; 
            }
            if (!String.IsNullOrEmpty(mrds))
            {
                mrds = mrds.Remove(mrds.Length - 1);
            }
            return mrds;
        }

        /**
         * Returns array of mrd names of position. , - separated.
         */
        public string GetMrdsNames(int positionid)
        {
            string mrds = "";
            foreach (Positionmrd positionmrd in Positionmrds.Where(pm => pm.Position == positionid))
            {
                mrds += MrdsLocal()[positionmrd.Mrd].Name + ",";
            }
            if (!String.IsNullOrEmpty(mrds))
            {
                mrds = mrds.Remove(mrds.Length - 1);
            }
            return mrds;
        }

        public string GetMrdsNamesLocal(int positionid, Dictionary<int, Mrd> mrdsLocal, Dictionary<int, Positionmrd> positionmrdsLocal)
        {
            string mrds = "";
            //foreach (Positionmrd positionmrd in Positionmrds.Where(pm => pm.Position == positionid))
            foreach (Positionmrd positionmrd in positionmrdsLocal.Values.Where(pm => pm.Position == positionid))
            {
                if (mrdsLocal.ContainsKey(positionmrd.Mrd)){ // Почему-то не всегда находило.
                    mrds += mrdsLocal[positionmrd.Mrd].Name + ",";
                }
                
            }
            if (!String.IsNullOrEmpty(mrds))
            {
                mrds = mrds.Remove(mrds.Length - 1);
            }
            return mrds;
        }

        /**
         * Returns array of mrd short name of position
         */
        public string GetMrdsShort(int positionid)
        {
            string mrds = "";
            foreach (Positionmrd positionmrd in PositionmrdsLocal().Values.Where(pm => pm.Position == positionid))
            {
                IEnumerable<Mrd> mrdEn = MrdsLocal().Values.Where(m => m.Id == positionmrd.Mrd);
                if (mrdEn.Count() > 0)
                {
                    mrds += mrdEn.First().Short + ",";
                }
                //mrds += Mrds.Where(m => m.Id == positionmrd.Mrd).First().Short + ",";
            }
            if (!String.IsNullOrEmpty(mrds))
            {
                mrds = mrds.Remove(mrds.Length - 1);
            }
            return mrds;
        }

        /**
         * Возвращает список меток рода деятельности. Вначале списка первым элементом так же будет ID позиции
         */
        public string GetMrdsShortId(int positionid)
        {
            string mrds = positionid + ","; // первый элемент у нас id
            foreach (Positionmrd positionmrd in PositionmrdsLocal().Values.Where(pm => pm.Position == positionid))
            {
                IEnumerable<Mrd> mrdEn = MrdsLocal().Values.Where(m => m.Id == positionmrd.Mrd);
                if (mrdEn.Count() > 0)
                {
                    mrds += mrdEn.First().Short + ",";
                }
                //mrds += Mrds.Where(m => m.Id == positionmrd.Mrd).First().Short + ",";
            }
            if (!String.IsNullOrEmpty(mrds))
            {
                mrds = mrds.Remove(mrds.Length - 1);
            }
            return mrds;
        }

        public IEnumerable<Position> GetAllPositions(int structureID)
        {
            List<Position> positions = new List<Position>();
            Dictionary<int, List<Position>> positionsStructures = PositionsStructureAsKeyLocal();
            if (positionsStructures.ContainsKey(structureID))
            {
                positions = new List<Position>(PositionsStructureAsKeyLocal()[structureID]);
                //positions = PositionsStructureAsKeyLocal()[structureID];

            }
            return positions;
        }

        /**
         * If forInfoOnly, it doesn't sort by priority because it is just gather information about positions.
         */
        public IEnumerable<Position> GetPositions(int structureID, DateTime date, bool withHead = true, bool disabletracking = false, bool forInfoOnly = false)
        {
            if (structureID == 0)
            {
                return new List<Position>();
            }

            if (disabletracking)
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }

            /**
             * For obsolete code.
             */
            if (structureID < 0)
            {
                structureID = -structureID;
            }

            Structure structure = StructuresLocal().GetValue(structureID);
            if (structure != null)
            {
                int originalID = GetStructureOriginalId(structure);
                //structureID = originalID;
            }

            List<Position> positions = new List<Position>();
            Dictionary<int, List<Position>> positionsStructures = PositionsStructureAsKeyLocal();
            if (positionsStructures.ContainsKey(structureID)){
                positions = new List<Position>(PositionsStructureAsKeyLocal()[structureID]);
                //positions = PositionsStructureAsKeyLocal()[structureID];

            }
            //List<Position> positions = Positions.Where(p => p.Structure == originalStructure.Id).ToList();


            if (withHead)
            {
                Position head = GetHead(structureID, date);
                if (head != null && !positions.Contains(head))
                {
                    positions.Add(head);
                }
            }

            if (PositiontypesLocal() == null) // Иногда так случается
            {
                UpdatePositiontypesLocal();
            }
            if (forInfoOnly)
            {
                Dictionary<int, Positiontype> ptLocal = PositiontypesLocal();
                positions = FilterDeletedPositions(positions, date).ToList();
            }
            else
            {
                
                Dictionary<int, Positiontype> ptLocal = PositiontypesLocal();
                positions = FilterDeletedPositions(positions, date).OrderByDescending(p => ptLocal[p.Positiontype].Priority).OrderByDescending(p => p.Headid == structureID).ToList();
                //positions = FilterDeletedPositions(positions, date).OrderByDescending(p => PositiontypesLocal()[p.Positiontype].Priority).OrderByDescending(p => p.Headid == structureID).ToList();
            }



            return positions;
            
        }

        public IEnumerable<Position> FilterDeletedPositionsWithFuture(IEnumerable<Position> positions, DateTime date)
        {
            return positions.
                Where(d =>
                {
                    if (!DecreeoperationsSubjectAsKeyLocal().ContainsKey(d.Id))
                    {
                        return true; // Так как хотим, чтобы null возвращало.
                    }
                    List<Decreeoperation> decreeoperations = DecreeoperationsSubjectAsKeyLocal()[d.Id];
                    if (decreeoperations.FirstOrDefault(deco => deco.Deleted > 0 && GetPositionsDecreeNowOrFuture(deco, date)) == null)
                    {
                        return true;
                    }
                    return false;
                });
            //return positions.
            //    Where(d => DecreeoperationsLocal().Values.FirstOrDefault(deco => deco.Deleted > 0 && deco.Subject == d.Id
            //        && GetPositionsIsActualDecree(deco, date)) == null);

        }

        //GetPositionsDecreeNowOrFuture
        public IEnumerable<Position> FilterDeletedPositions(IEnumerable<Position> positions, DateTime date)
        {
            return positions.
                Where(d =>
                {
                    if (!DecreeoperationsSubjectAsKeyLocal().ContainsKey(d.Id))
                    {
                        return true; // Так как хотим, чтобы null возвращало.
                    }
                    List<Decreeoperation> decreeoperations = DecreeoperationsSubjectAsKeyLocal()[d.Id];
                    if (decreeoperations.FirstOrDefault( deco => deco.Deleted > 0 && GetPositionsIsActualDecree(deco, date)) == null)
                    {
                        return true;
                    }
                    return false;
                });
            //return positions.
            //    Where(d => DecreeoperationsLocal().Values.FirstOrDefault(deco => deco.Deleted > 0 && deco.Subject == d.Id
            //        && GetPositionsIsActualDecree(deco, date)) == null);

        }

        public List<Position> FilterDeletedPositionsQueryable(IQueryable<Position> positions, DateTime date)
        {
            return positions.
                    Where(d => Decreeoperations.FirstOrDefault(deco => deco.Deleted > 0 && deco.Subject == d.Id
                        && GetPositionsIsActualDecree(deco, date)) == null).ToList();
        }


        /**
         * Returns array of mrd ids of each position in list
         */
        public string GetMrdsList(IEnumerable<Position> positions)
        {
            string mrds = "";
            foreach(Position position in positions)
            {
                mrds += GetMrds(position.Id) + ";";
            }
            if (!String.IsNullOrEmpty(mrds))
            {
                mrds = mrds.Remove(mrds.Length - 1);
            }
            return mrds;
        }

        /**
         * Returns array of mrd short names of each position in list
         */
        public string GetMrdsListShort(IEnumerable<Position> positions)
        {
            string mrds = "";
            foreach (Position position in positions)
            {
                mrds += GetMrdsShort(position.Id) + ";";
            }
            if (!String.IsNullOrEmpty(mrds))
            {
                mrds = mrds.Remove(mrds.Length - 1);
            }
            return mrds;
        }

        /**
         * Returns array of mrd short names of each position in list
         */
        public string GetMrdsListShortId(IEnumerable<Position> positions)
        {
            string mrds = "";
            foreach (Position position in positions)
            {
                mrds += GetMrdsShortId(position.Id) + ";";
            }
            if (!String.IsNullOrEmpty(mrds))
            {
                mrds = mrds.Remove(mrds.Length - 1);
            }
            return mrds;
        }

        /**
         * Returns ; separated altranks information: "conditiongroup:condition=rank,condition rank;conditiongroup:condition rank,condition rank,condition rank"; - "если отсутствует...:ученая степень подполковник
         */
        public string GetAltranksList(IEnumerable<Position> positions, bool disabletracking = false)
        {
            if (disabletracking)
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }


            string altranks = "";
            foreach(Position position in positions)
            {
                IEnumerable<Altrank> altrankRequest = AltranksLocal().Values.Where(a => a.Position == position.Id);
                //IEnumerable<Altrank> altrankRequest = Altranks.Where(a => a.Position == position.Id);
                string altrankString = "";
                bool first = true;
                foreach(Altrank altrank in altrankRequest)
                {
                    Altrankcondition altrankcondition = AltrankconditionsLocal().GetValue(altrank.Altrankcondition);
                    //Altrankcondition altrankcondition = Altrankconditions.FirstOrDefault(a => a.Id == altrank.Altrankcondition);
                    string conditionName = altrankcondition.Name;
                    string rankName = Ranks.FirstOrDefault(r => r.Id == altrank.Rank).Name;

                    /**
                     * First time we are typing condition group
                     */
                    if (first)
                    {
                        string conditiongroupname = AltrankconditiongroupsLocal().GetValue(altrankcondition.Group).Name;
                        //string conditiongroupname = Altrankconditiongroups.FirstOrDefault(a => a.Id == altrankcondition.Group).Name;
                        altrankString += conditiongroupname + ":";
                        first = false;
                    }

                    altrankString += conditionName + " " + rankName + ",";
                }
                if (!String.IsNullOrEmpty(altrankString))
                {
                    altrankString = altrankString.Remove(altrankString.Length - 1);
                }
                altranks += altrankString + ";";
            }
            if (!String.IsNullOrEmpty(altranks))
            {
                altranks = altranks.Remove(altranks.Length - 1);
            }
            return altranks;
        }

        /**
         * "conditiongroup:condition=rank,condition=rank" - "1:1=2,2=4"
         */
        public string GetAltrankList(Position position, bool disabletracking = false)
        {
            if (disabletracking)
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            string altranks = "";
            IEnumerable<Altrank> altrankRequest = Altranks.Where(a => a.Position == position.Id);
            string altrankString = "";
            bool first = true;
            foreach (Altrank altrank in altrankRequest)
            {
                Altrankcondition altrankcondition = Altrankconditions.FirstOrDefault(a => a.Id == altrank.Altrankcondition);
                string conditionName = altrankcondition.Id.ToString();
                string rankName = Ranks.FirstOrDefault(r => r.Id == altrank.Rank).Id.ToString();

                /**
                    * First time we are typing condition group
                    */
                if (first)
                {
                    string conditiongroupname = Altrankconditiongroups.FirstOrDefault(a => a.Id == altrankcondition.Group).Id.ToString();
                    altrankString += conditiongroupname + ":";
                    first = false;
                }

                altrankString += conditionName + "=" + rankName + ",";
            }
            if (!String.IsNullOrEmpty(altrankString))
            {
                altrankString = altrankString.Remove(altrankString.Length - 1);
            }
            altranks += altrankString + ";";
            return altranks;
        }

        public bool GetPositionsDecreeNowOrFuture(Decreeoperation deco, DateTime dateTime)
        {
            return DecreesLocal().Values.FirstOrDefault(decree => decree.Id == deco.Decree && decree.Signed > 0) != null;
            //return Decrees.FirstOrDefault(decree => decree.Id == deco.Decree && decree.Signed > 0 && deco.Dateactive.GetValueOrDefault() <= dateTime) != null;
        }

        public bool GetPositionsIsActualDecree(Decreeoperation deco, DateTime dateTime)
        {
            return DecreesLocal().Values.FirstOrDefault(decree => decree.Id == deco.Decree && decree.Signed > 0 && deco.Dateactive.GetValueOrDefault() <= dateTime) != null;
            //return Decrees.FirstOrDefault(decree => decree.Id == deco.Decree && decree.Signed > 0 && deco.Dateactive.GetValueOrDefault() <= dateTime) != null;
        }

        public bool GetPositionsIsActualDecreePadding(Decreeoperation deco, DateTime farawayDate)
        {
            return DecreesLocal().Values.FirstOrDefault(decree => decree.Id == deco.Decree && deco.Dateactive.GetValueOrDefault() <= farawayDate) != null;
            //return Decrees.FirstOrDefault(decree => decree.Id == deco.Decree && deco.Dateactive.GetValueOrDefault() <= farawayDate) != null;
        }


        /**
         * Put ACTUAL date active to decreeoperation whether it connected with decree or custom.
         */
        public List<Decreeoperation> PushDateactivesToDecreeoperation(List<Decreeoperation> decreeoperations)
        {
            decreeoperations.ForEach(d => d.Dateactive = GetDateActive(d));
            context.SaveChanges();
            return decreeoperations;
        }

        /**
         * Получаем дату введения/упразднения должности в будущем если есть.
         * 
         */
        public DateTime GetDateFuture(List<DecreeoperationManagement> decreeoperationManagements)
        {
            DecreeoperationManagement dom = null;
            foreach(DecreeoperationManagement decreeoperation in decreeoperationManagements)
            {
                if (dom == null)
                {
                    dom = decreeoperation;
                }
                if (decreeoperation.MetaPurposeForSubject == Keys.DECREE_OPERATION_META_PURPOSE_DELETE_SUBJECT_IN_FUTURE)
                {
                    dom = decreeoperation;
                    break;
                }
                if (decreeoperation.MetaPurposeForSubject == Keys.DECREE_OPERATION_META_PURPOSE_CREATE_SUBJECT_IN_FUTURE)
                {
                    dom = decreeoperation;
                }
            }
            if (dom == null)
            {
                return DateTime.Now;
            } else
            {
                if (dom.Datecustom > 0)
                {
                    return dom.Dateactive.GetValueOrDefault();
                }
                else
                {
                    return dom.MetaDateActive; // Probably can be broken
                }
            }
        }

        public DateTime GetDateActive(Decreeoperation decreeoperation)
        {
            if (decreeoperation.Datecustom > 0) 
            {
                return decreeoperation.Dateactive.GetValueOrDefault();
            } else
            {
                return DecreesLocal()[decreeoperation.Decree].Dateactive.GetValueOrDefault(); // Probably can be broken
            }
        }

        public void UpdateDatectives()
        {
            context.Decreeoperation.ToList().ForEach(dec => dec.Dateactive = GetDateActive(dec));
            context.SaveChanges();
        }

        public void UpdateStructuretypes(DateTime date)
        {
            for (int i = 0; i < 10; i++)
            {
                context.Structure.ToList().ForEach(s =>
                {
                    if (s.Parentstructure > 0)
                    {
                        Structure structure = GetActualStructureInfo(s.Parentstructure, date);
                        if (structure != null && structure.Structuretype > 0)
                        {
                            s.Structuretype = structure.Structuretype;
                        }
                    }

                });
                context.SaveChanges();
            }
            
            UpdateStructuresLocal();
        }

        public void UpdateDateactives(int decreeid)
        {
            context.Decreeoperation.Where(d => d.Decree == decreeid).ToList().ForEach(dec => dec.Dateactive = GetDateActive(dec));
            context.SaveChanges();
        }

        /**
         * Generate decree operations request for positions
         */
        public List<DecreeOperationsRequest> GetDecreeOperationsRequests(IEnumerable<Position> positions, DateTime date)
        {
            List<DecreeOperationsRequest> decreeOperationsRequests = new List<DecreeOperationsRequest>();
            foreach (Position position in positions)
            {
                decreeOperationsRequests.Add(GetDecreeOperationsRequest(position, date));
            }
            return decreeOperationsRequests;
        }

        public DecreeOperationsRequest GetDecreeOperationsRequest(Position position, DateTime date)
        {
            DecreeOperationsRequest request = new DecreeOperationsRequest();
            request.SubjectID = position.Id;
            request.Detailed = 0;
            request.Padding = 0;
            request.RequestedDate = date;
            return request;
        }

        public DecreeOperationsRequest GetDecreeOperationsRequest(Structure structure, DateTime date)
        {
            DecreeOperationsRequest request = new DecreeOperationsRequest();
            request.SubjectID = -structure.Id;
            request.Detailed = 0;
            request.Padding = 0;
            request.RequestedDate = date;
            return request;
        }

        public DecreeOperationsRequest GetDecreeOperationsRequest(StructureExpanded structure, DateTime date)
        {
            DecreeOperationsRequest request = new DecreeOperationsRequest();
            request.SubjectID = -structure.Id;
            request.Detailed = 0;
            request.Padding = 0;
            request.RequestedDate = date;
            return request;
        }

        public bool IsNotSignedAndDeleted(Position position, DateTime date)
        {
            DecreeOperationsRequest request = GetDecreeOperationsRequest(position, date);
            List<DecreeoperationManagement> doms = RequestDecreeOperations(request);
            return IsNotSignedAndCreated(doms);
        }

        public bool IsNotSignedAndDeleted(List<DecreeoperationManagement> decreeoperationManagements)
        {
            foreach (DecreeoperationManagement dom in decreeoperationManagements)
            {
                if (dom.MetaPurposeForSubject == Keys.DECREE_OPERATION_META_PURPOSE_DELETED_SUBJECT_NOT_SIGNED)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsNotSignedAndCreated(Position position, DateTime date)
        {
            DecreeOperationsRequest request = GetDecreeOperationsRequest(position, date);
            List<DecreeoperationManagement> doms = RequestDecreeOperations(request);
            return IsNotSignedAndCreated(doms);
        }

        public bool IsNotSignedAndCreated(List<DecreeoperationManagement> decreeoperationManagements)
        {
            foreach (DecreeoperationManagement dom in decreeoperationManagements)
            {
                if (dom.MetaPurposeForSubject == Keys.DECREE_OPERATION_META_PURPOSE_CREATED_SUBJECT_NOT_SIGNED)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsSignedAndCreated(Structure structure, DateTime date)
        {
            DecreeOperationsRequest request = GetDecreeOperationsRequest(structure, date);
            List<DecreeoperationManagement> doms = RequestDecreeOperations(request);
            return IsSignedAndCreated(doms);
        }

        public bool IsSignedAndCreated(StructureExpanded structure, DateTime date)
        {
            DecreeOperationsRequest request = GetDecreeOperationsRequest(structure, date);
            List<DecreeoperationManagement> doms = RequestDecreeOperations(request);
            return IsSignedAndCreated(doms);
        }

        public bool IsSignedAndCreated(Position position, DateTime date)
        {
            DecreeOperationsRequest request = GetDecreeOperationsRequest(position, date);
            List<DecreeoperationManagement> doms = RequestDecreeOperations(request);
            return IsSignedAndCreated(doms); 
        }

        public bool IsSignedAndCreated(List<DecreeoperationManagement> decreeoperationManagements)
        {
            foreach (DecreeoperationManagement dom in decreeoperationManagements)
            {
                if (dom.MetaPurposeForSubject == Keys.DECREE_OPERATION_META_PURPOSE_CREATED_SUBJECT)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsSignedAndDeleted(Structure structure, DateTime date)
        {
            DecreeOperationsRequest request = GetDecreeOperationsRequest(structure, date);
            List<DecreeoperationManagement> doms = RequestDecreeOperations(request);
            return IsSignedAndDeleted(doms);
        }

        public bool IsSignedAndDeleted(StructureExpanded structure, DateTime date)
        {
            DecreeOperationsRequest request = GetDecreeOperationsRequest(structure, date);
            List<DecreeoperationManagement> doms = RequestDecreeOperations(request);
            return IsSignedAndDeleted(doms);
        }

        public bool IsSignedAndDeleted(Position position, DateTime date)
        {
            DecreeOperationsRequest request = GetDecreeOperationsRequest(position, date);
            List<DecreeoperationManagement> doms = RequestDecreeOperations(request);
            return IsSignedAndDeleted(doms);
        }

        public bool IsSignedAndDeleted(List<DecreeoperationManagement> decreeoperationManagements)
        {
            foreach (DecreeoperationManagement dom in decreeoperationManagements)
            {
                if (dom.MetaPurposeForSubject == Keys.DECREE_OPERATION_META_PURPOSE_DELETED_SUBJECT)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsSignedAndWillCreated(Structure structure, DateTime date)
        {
            DecreeOperationsRequest request = GetDecreeOperationsRequest(structure, date);
            List<DecreeoperationManagement> doms = RequestDecreeOperations(request);
            return IsSignedAndWillCreated(doms);
        }

        public bool IsSignedAndWillCreated(StructureExpanded structure, DateTime date)
        {
            DecreeOperationsRequest request = GetDecreeOperationsRequest(structure, date);
            List<DecreeoperationManagement> doms = RequestDecreeOperations(request);
            return IsSignedAndWillCreated(doms);
        }

        public bool IsSignedAndWillCreated(Position position, DateTime date)
        {
            DecreeOperationsRequest request = GetDecreeOperationsRequest(position, date);
            List<DecreeoperationManagement> doms = RequestDecreeOperations(request);
            bool signedAndWillCreated =  IsSignedAndWillCreated(doms);
            return signedAndWillCreated;
        }


        public bool IsSignedAndWillCreated(List<DecreeoperationManagement> decreeoperationManagements)
        {
            foreach (DecreeoperationManagement dom in decreeoperationManagements)
            {
                if (dom.MetaPurposeForSubject == Keys.DECREE_OPERATION_META_PURPOSE_CREATE_SUBJECT_IN_FUTURE)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsSignedAndWillDeleted(Position position, DateTime date)
        {
            DecreeOperationsRequest request = GetDecreeOperationsRequest(position, date);
            List<DecreeoperationManagement> doms = RequestDecreeOperations(request);
            return IsSignedAndWillDeleted(doms);
        }

        public bool IsSignedAndWillDeleted(List<DecreeoperationManagement> decreeoperationManagements)
        {
            foreach (DecreeoperationManagement dom in decreeoperationManagements)
            {
                if (dom.MetaPurposeForSubject == Keys.DECREE_OPERATION_META_PURPOSE_DELETE_SUBJECT_IN_FUTURE)
                {
                    return true;
                }
            }
            return false;
        }

        public DateTime TimeWillCreated(Structure structure, DateTime date)
        {
            DecreeOperationsRequest request = GetDecreeOperationsRequest(structure, date);
            List<DecreeoperationManagement> doms = RequestDecreeOperations(request);
            return TimeWillCreated(doms);
        }

        public DateTime TimeWillCreated(StructureExpanded structure, DateTime date)
        {
            DecreeOperationsRequest request = GetDecreeOperationsRequest(structure, date);
            List<DecreeoperationManagement> doms = RequestDecreeOperations(request);
            return TimeWillCreated(doms);
        }

        public DateTime TimeWillCreated(Position position, DateTime date)
        {
            DecreeOperationsRequest request = GetDecreeOperationsRequest(position, date);
            List<DecreeoperationManagement> doms = RequestDecreeOperations(request);
            return TimeWillCreated(doms);
        }

        public DateTime TimeWillCreated(List<DecreeoperationManagement> decreeoperationManagements)
        {
            foreach (DecreeoperationManagement dom in decreeoperationManagements)
            {
                if (dom.MetaPurposeForSubject == Keys.DECREE_OPERATION_META_PURPOSE_CREATE_SUBJECT_IN_FUTURE)
                {
                    return dom.MetaDateActive;
                }
            }
            return DateTime.Now;
        }

        public bool IsSigned(Structure structure, DateTime date)
        {
            DecreeOperationsRequest request = GetDecreeOperationsRequest(structure, date);
            List<DecreeoperationManagement> doms = RequestDecreeOperations(request);
            return IsSigned(doms);
        }

        public bool IsSigned(StructureExpanded structure, DateTime date)
        {
            DecreeOperationsRequest request = GetDecreeOperationsRequest(structure, date);
            List<DecreeoperationManagement> doms = RequestDecreeOperations(request);
            return IsSigned(doms);
        }

        public bool IsSigned(Position position, DateTime date)
        {
            DecreeOperationsRequest request = GetDecreeOperationsRequest(position, date);
            List<DecreeoperationManagement> doms = RequestDecreeOperations(request);
            return IsSigned(doms);
        }

        public bool IsSigned(List<DecreeoperationManagement> decreeoperationManagements)
        {
            foreach (DecreeoperationManagement dom in decreeoperationManagements)
            {
                if (dom.MetaPurposeForSubject == Keys.DECREE_OPERATION_META_PURPOSE_CREATED_SUBJECT
                    || dom.MetaPurposeForSubject == Keys.DECREE_OPERATION_META_PURPOSE_CREATE_SUBJECT_IN_FUTURE
                    || dom.MetaPurposeForSubject == Keys.DECREE_OPERATION_META_PURPOSE_DELETE_SUBJECT_IN_FUTURE)
                {
                    return true;
                }
            }
            return false;
        }

        public List<PmrequestStructure> GetPmrequestStructures(Pmrequest pmrequest, User user)
        {
            Dictionary<int, Structuretype> structuretypesLocal = LocalizeObject<Structuretype>(Structuretypes.Select(p => p.Id).ToList(), Structuretypes.ToList());
            Dictionary<int, Structureregion> structureregionsLocal = LocalizeObject<Structureregion>(Structureregions.Select(p => p.Id).ToList(), Structureregions.ToList());
            Dictionary<int, Sourceoffinancing> sofsLocal = LocalizeObject<Sourceoffinancing>(Sourcesoffinancings.Select(p => p.Id).ToList(), Sourcesoffinancings.ToList());
            DateTime date = user.Date.GetValueOrDefault();

            List<PmrequestStructure> list = new List<PmrequestStructure>();
            IEnumerable<Structure> structures = null;
            List<Structure> actualStructures = new List<Structure>();

            /**
             * Include only specified structures
             */
            if (pmrequest.Structures != null && pmrequest.Structures.Length > 0)
            {
                int[] structuresArray = pmrequest.Structures.Split(',').Select(int.Parse).ToArray();
                HashSet<int> structuresIds = new HashSet<int>();
                foreach (int i in structuresArray)
                {
                    structuresIds.UnionWith(GetStructuresSiblings(i, null, date));
                }
                List<Structure> structuresTemp = new List<Structure>();
                foreach (int i in structuresIds)
                {
                    // structuresTemp.AddRange(Structures.Where(p => p.Parentstructure == i));
                    structuresTemp.AddRange(StructuresLocal().Values.Where(p => p.Parentstructure == i));
                }
                structures = FilterDeletedStructures(structuresTemp, user.Date.GetValueOrDefault());
                /**
                 * Include all structures
                 */
            }
            else
            {
                //structures = FilterDeletedStructures(Structures.AsEnumerable(), user.Date.GetValueOrDefault());
                List<int> structureids = GetStructuresSiblings(0, null, date).ToList();
                List<Structure> allStructures = new List<Structure>();
                foreach (int structureid in structureids)
                {
                    Structure allStructure = StructuresLocal().GetValue(structureid);
                    if (allStructure != null)
                    {
                        allStructures.Add(allStructure);
                    }
                }
                structures = allStructures;
                //structures = FilterDeletedStructures(StructuresLocal().Values, user.Date.GetValueOrDefault());
            }

            /**
             * Фильтрация после выборки
             */
            
            List<Structure> structuresDistinct = new List<Structure>();
            foreach (Structure structure in structures)
            {
               /* if (structure.Separatestructure != 1 && (structure.Nameshortened == "Аппарат отдела" || structure.Nameshortened == "Аппарат упарвления"))
                    continue;*/
                Structure originalStructure = GetOriginalStructure(structure);
                if (originalStructure != null)
                {
                    structuresDistinct.Add(originalStructure);
                }
            }
            structures = structuresDistinct.Distinct();
            structures = structures.Where(s => GetActualStructureInfo(s, date) != null);

            if (pmrequest.Structuretypes != null && pmrequest.Structuretypes.Length > 0)
            {
                int[] structuretypes = pmrequest.Structuretypes.Split(',').Select(int.Parse).ToArray();
                //structures = structures.Where(s => structuretypes.Contains(s.Structuretype));
                structures = structures.Where(s => structuretypes.Contains(GetActualStructureInfo(s, date).Structuretype));
            }

            if (pmrequest.Structurerank != null && pmrequest.Structurerank.Length > 0)
            {
                int[] structureranks = pmrequest.Structurerank.Split(',').Select(int.Parse).ToArray();
                // structures = structures.Where(s => structureranks.Contains(s.Rank));
                structures = structures.Where(s => structureranks.Contains(GetActualStructureInfo(s, date).Rank));
            }

            if (pmrequest.Structurecity != null && pmrequest.Structurecity.Length > 0)
            {
                structures = structures.Where(s => GetActualStructureInfo(s, date).City.Contains(pmrequest.Structurecity));
            }

            if (pmrequest.Structurestreet != null && pmrequest.Structurestreet.Length > 0)
            {
                structures = structures.Where(s => GetActualStructureInfo(s, date).Street.Contains(pmrequest.Structurestreet));
            }

            if (pmrequest.Structureregion != null && pmrequest.Structureregion.Length > 0)
            {
                int[] structureregions = pmrequest.Structureregion.Split(',').Select(int.Parse).ToArray();
                structures = structures.Where(s => structureregions.Contains(GetActualStructureInfo(s, date).Structureregion));
            }

            // Включать подчиненные подразделения тех, кто прошел фильтрацию
            if (pmrequest.Structuresub > 0)
            {
                List<Structure> expandedStructures = new List<Structure>();
                foreach(Structure structure in structures)
                {
                    int sublevel = 100;
                    if (pmrequest.Structuresublevel > 0)
                    {
                        sublevel = pmrequest.Structuresublevel;
                    }
                    List<int> childrenIds = GetStructuresSiblings(structure.Id, null, date, sublevel);
                    List<Structure> structuresChildren = new List<Structure>();



                    foreach(int id in childrenIds)
                    {
                        Structure childrenStructure = GetActualStructureInfo(id, date);
                        if (childrenStructure != null)
                        {
                            structuresChildren.Add(childrenStructure);
                        }
                    }
                    //expandedStructures.Add(structure);
                    expandedStructures.AddRange(structuresChildren);
                }
                structures = expandedStructures.Distinct();
            }

            /**
             * Forming all SOF options
             */
            bool checkparrentstruktyretype(Structure structure)
            {
                Structure actual_structure = GetActualStructureInfo(structure.Parentstructure, date);
                if (pmrequest.Structuretypes == "" || actual_structure == null)
                    return false;
                foreach (string i in pmrequest.Structuretypes.Split(','))
                {
                    if (i == actual_structure.Structuretype.ToString() && actual_structure.Structuretype == structure.Structuretype)
                        return true;
                }
                return checkparrentstruktyretype(actual_structure);
            }
            /**
             * Forming data
             */
            foreach (Structure structure in structures)
            {
                PmrequestStructure pmrequestStructure = new PmrequestStructure();
                Structure actualStructure = GetActualStructureInfo(structure, date);
                if (checkparrentstruktyretype(actualStructure))
                    continue;
                pmrequestStructure.Name = actualStructure.Name;
                
                //GetOriginalStructure(structure)
                //pmrequestStructure.Tree = FormTree(structure, false);
                pmrequestStructure.Tree = FormTree(actualStructure, false, date);
                //if (structuretypesLocal.ContainsKey(structure.Structuretype))
                if (structuretypesLocal.ContainsKey(actualStructure.Structuretype))
                {
                    //pmrequestStructure.Type = structuretypesLocal[structure.Structuretype].Name;
                    pmrequestStructure.Type = structuretypesLocal[actualStructure.Structuretype].Name;
                } else
                {
                    pmrequestStructure.Type = "";
                }
                //if (structureregionsLocal.ContainsKey(structure.Structureregion))
                if (structureregionsLocal.ContainsKey(actualStructure.Structureregion))
                {
                    //pmrequestStructure.Region = structureregionsLocal[structure.Structureregion].Name;
                    pmrequestStructure.Region = structureregionsLocal[actualStructure.Structureregion].Name;
                }
                else
                {
                    pmrequestStructure.Region = "";
                }
                //pmrequestStructure.City = structure.City;
                //pmrequestStructure.Street = structure.Street;
                pmrequestStructure.City = actualStructure.City;
                pmrequestStructure.Street = actualStructure.Street;
                //if (structure.Rank > 0)
                if (actualStructure.Rank > 0)
                {
                    //pmrequestStructure.Rank = structure.Rank + " разряд";
                    pmrequestStructure.Rank = actualStructure.Rank + " разряд";
                } else
                {
                    pmrequestStructure.Rank = "Отсутствует разряд";
                }
                pmrequestStructure.StructureInfoInner = new StructureInfoInner();
                //pmrequestStructure.StructureInfoInner = GetStructureInfoInner(structure.Id, date);
                pmrequestStructure.StructureInfoInner = GetStructureInfoInner(GetOriginalStructure(structure).Id, date, pmrequest);

                
                pmrequestStructure.SofSigned = new List<KeyValuePair<Sourceoffinancing, double>>();
                pmrequestStructure.SofUnsigned = new List<KeyValuePair<Sourceoffinancing, double>>();
                foreach (Sourceoffinancing sof in sofsLocal.Values)
                {
                    if (pmrequestStructure.StructureInfoInner.sofValues.ContainsKey(sof.Id)){
                        pmrequestStructure.SofSigned.Add(new KeyValuePair<Sourceoffinancing, double>(sof, pmrequestStructure.StructureInfoInner.sofValues[sof.Id].Key));
                        pmrequestStructure.SofUnsigned.Add(new KeyValuePair<Sourceoffinancing, double>(sof, pmrequestStructure.StructureInfoInner.sofValues[sof.Id].Value));
                    } else
                    {
                        pmrequestStructure.SofSigned.Add(new KeyValuePair<Sourceoffinancing, double>(sof, 0));
                        pmrequestStructure.SofUnsigned.Add(new KeyValuePair<Sourceoffinancing, double>(sof, 0));
                    }
                }

                pmrequestStructure.Curator = "";
                if (structure.Curator > 0 && IsCurating(structure.Curator, structure.Id, date))
                {
                    Position position = PositionsLocal().GetValueOrDefault(structure.Curator);
                    if (position != null)
                    {
                        pmrequestStructure.Curator = PositiontypesLocal()[position.Positiontype].Name;
                    }
                    //Position position = PositionsLocal().GetValueOrDefault(structure.Curator);
                    //if (position != null)
                    //{
                    //    int[] curationids = position.Curatorlist.Split(',').Select(int.Parse).ToArray();
                    //    if (curationids.Contains(structure.Id))
                    //    {
                    //        pmrequestStructure.Curator = PositiontypesLocal()[position.Positiontype].Name;
                    //    }
                    //}
                }

                pmrequestStructure.Head = "";
                if (structure.Head > 0 && IsHeading(structure.Head, structure.Id, date))
                {
                    Position position = PositionsLocal().GetValueOrDefault(structure.Head);
                    if (position != null)
                    {
                        pmrequestStructure.Head = PositiontypesLocal()[position.Positiontype].Name;
                    }
                    //if (position != null && position.Headid == structure.Id)
                    //{
                    //    pmrequestStructure.Head = PositiontypesLocal()[position.Positiontype].Name;
                    //}
                }


                DecreeOperationsRequest request = GetDecreeOperationsRequest(structure, user.Date.GetValueOrDefault());
                List<DecreeoperationManagement> doms = RequestDecreeOperations(request);

                if (IsSignedAndCreated(doms))
                {
                    pmrequestStructure.Signed = "Подразделение создано";
                }
                else
                {
                    pmrequestStructure.Signed = "Подразделение создано, но приказ не подписан";
                }


                DecreeoperationManagement dom = doms.FirstOrDefault(d => d.MetaPurposeForSubject == Keys.DECREE_OPERATION_META_PURPOSE_CREATED_SUBJECT ||
                    d.MetaPurposeForSubject == Keys.DECREE_OPERATION_META_PURPOSE_CREATED_SUBJECT_NOT_SIGNED || d.MetaPurposeForSubject == Keys.DECREE_OPERATION_META_PURPOSE_CREATE_SUBJECT_IN_FUTURE ||
                    d.MetaPurposeForSubject == Keys.DECREE_OPERATION_META_PURPOSE_CREATE_SUBJECT_IN_FUTURE_NOT_SIGNED);

                if (dom != null) //
                {
                    pmrequestStructure.DateCreated = dom.MetaDateActive.ToString("yyyy.MM.dd");
                    pmrequestStructure.DecreeCreationName = dom.MetaDecreeName;
                    pmrequestStructure.DecreeCreationUnofficialName = dom.MetaDecreeNameUnofficial;
                    pmrequestStructure.DecreeCreationNumber = dom.MetaDecreeNumber;
                }
                


                list.Add(pmrequestStructure);
            }

                return list;
        }

        public List<PmrequestStructure> GetPmrequestStructuresCount(Pmrequest pmrequest, User user)
        {
            Dictionary<int, Structureregion> structureregionsLocal = LocalizeObject<Structureregion>(Structureregions.Select(p => p.Id).ToList(), Structureregions.ToList());
            DateTime date = user.Date.GetValueOrDefault();

            List<PmrequestStructure> list = new List<PmrequestStructure>();
            IEnumerable<Structure> structures = null;
            List<int> typelessStructureIds = new List<int>();
            int typelessStructureID = 0;
            bool typeless = true;
            if (pmrequest.Structuretypes != null && pmrequest.Structuretypes.Length > 0)
            {
                typeless = false;
            } else if (pmrequest.Structures == null || pmrequest.Structures.Length == 0)
            {
                List<int> structuresArray = new List<int>();
                foreach (Structure structure in FilterDeletedStructures(StructuresLocal().Values.Where(s => s.Parentstructure == 0), date))
                {
                    structuresArray.Add(StructureBaseId(structure));
                }
                typelessStructureID = structuresArray.First(); // Когда нам нужно было только единственное подразделение, мы брали первое в списке
                typelessStructureIds.Add(typelessStructureID); // Теперь может обрабатываться целый массив
                //typelessStructureID = pmrequest.Structures.Split(',').Select(int.Parse).First();
            } else
            {
                typelessStructureID = pmrequest.Structures.Split(',').Select(int.Parse).First(); // Когда нам нужно было только единственное подразделение, мы брали первое в списке
                foreach (int id in pmrequest.Structures.Split(',').Select(int.Parse))
                {
                    typelessStructureIds.Add(id);
                }
            }
            int structureMainID = 0;
            if (pmrequest.Structures != null && pmrequest.Structures.Length > 0)
            {
                structureMainID = pmrequest.Structures.Split(',').Select(int.Parse).First();
            }



            /**
             * Include only specified structures
             */
            if (!typeless && pmrequest.Structures != null && pmrequest.Structures.Length > 0)
            {
                int[] structuresArray = pmrequest.Structures.Split(',').Select(int.Parse).ToArray();
                HashSet<int> structuresIds = new HashSet<int>();
                foreach (int i in structuresArray)
                {
                    structuresIds.UnionWith(GetStructuresSiblings(i, null, date));
                }
                List<Structure> structuresTemp = new List<Structure>();
                foreach (int i in structuresIds)
                {
                    structuresTemp.AddRange(StructuresLocal().Values.Where(p => p.Parentstructure == i));
                }
                structures = FilterDeletedStructures(structuresTemp, user.Date.GetValueOrDefault());
                structures = GetActualStructures(structures, date);
                /**
                 * Include all structures
                 */
            }
            else if (!typeless)
            {
                //structures = FilterDeletedStructures(StructuresLocal().Values, user.Date.GetValueOrDefault());

                List<int> structuresArray = new List<int>();
                foreach (Structure structure in FilterDeletedStructures(StructuresLocal().Values.Where(s => s.Parentstructure == 0), user.Date.GetValueOrDefault()))
                {
                    structuresArray.Add(StructureBaseId(structure));
                }
                HashSet<int> structuresIds = new HashSet<int>();
                foreach (int i in structuresArray)
                {
                    structuresIds.UnionWith(GetStructuresSiblings(i, null, date));
                }
                List<Structure> structuresTemp = new List<Structure>();
                foreach (int i in structuresIds)
                {
                    structuresTemp.AddRange(StructuresLocal().Values.Where(p => p.Parentstructure == i || p.Id == i));
                }
                structures = FilterDeletedStructures(structuresTemp, user.Date.GetValueOrDefault());
                //structures = GetOriginalStructures(structures);
                structures = GetActualStructures(structures, date);

            } else if (typeless)
            {
                List<Structure> structuresList = new List<Structure>();
                
                if (pmrequest.Structures == null || pmrequest.Structures.Length == 0)
                {
                    foreach (Structure structure in FilterDeletedStructures(StructuresLocal().Values.Where(s => s.Parentstructure == 0), user.Date.GetValueOrDefault()))
                    {
                        structuresList.Add(GetOriginalStructure(structure));
                    }
                } else
                {
                    //structuresList.Add(GetOriginalStructure(typelessStructureID));
                    //typelessStructureIds
                    foreach (int structureid in typelessStructureIds)
                    {
                        structuresList.Add(GetOriginalStructure(structureid));
                    }
                }
                structures = structuresList.Distinct().ToList();
            }
            if (structureMainID == 0)
            {
                //Structure structureMain = structures.Where(s => s.Main > 0).FirstOrDefault();
                //if (structureMain != null)
                //{
                //    structureMainID = structureMain.Id;
                //} else
                //{
                //    structureMainID = structures.First().Id;
                //}
                if (structures.Count() == 1) // У нас всего лишь одна структура
                {
                    structureMainID = structures.First().Id;
                    
                }
            }
            if (structureMainID != 0)
            {
                pmrequest.StructureMain = GetOriginalStructure(structureMainID);
            }
            //pmrequest.StructureMain = GetOriginalStructure(structureMainID);
            List<int> acceptedTypes = new List<int>();

            if (!typeless)
            {
                int[] structuretypes = pmrequest.Structuretypes.Split(',').Select(int.Parse).ToArray();
                acceptedTypes = structuretypes.ToList();
                structures = structures.Where(s => structuretypes.Contains(s.Structuretype)).ToList();
                
                //structures = structures.Where(s => s.Parentstructure.GetValueOrDefault() == 0 || !structuretypes.Contains(StructuresLocal()[s.Parentstructure.GetValueOrDefault()].Structuretype)).ToList();

                /**
                 * Create summary PM Structure
                 */
                PmrequestStructure summaryPmrequestStructure = new PmrequestStructure();
                summaryPmrequestStructure.PmrequestStructureCounts = new List<PmrequestStructureCount>();
                PmrequestStructureCount summaryPSC = new PmrequestStructureCount();
                

                summaryPSC.CountPositionCategory = new Dictionary<int, double>();
                List<Positioncategory> positioncategoriesSorted = SortPositioncategory(PositioncategoriesLocal().Values);
                foreach (Positioncategory positionCategory in positioncategoriesSorted)
                {
                    summaryPSC.CountPositionCategory.Add(positionCategory.Id, 0);
                }
                summaryPmrequestStructure.PmrequestStructureCounts.Add(summaryPSC);

                foreach (Sourceoffinancing sof in SourceoffinancingsLocal().Values)
                {
                    PmrequestStructureCount pmrequestStructureCount = new PmrequestStructureCount();
                    pmrequestStructureCount.Sourceoffinancing = sof;
                    pmrequestStructureCount.CountPositionCategory = new Dictionary<int, double>();

                    //pmrequestStructureCount.CountPositionCategory.Add(0, 0);

                    foreach (Positioncategory positionCategory in positioncategoriesSorted)
                    {
                        pmrequestStructureCount.CountPositionCategory.Add(positionCategory.Id, 0);
                    }

                    summaryPmrequestStructure.PmrequestStructureCounts.Add(pmrequestStructureCount);
                }

                summaryPmrequestStructure.Name = "Всего";

                list.Add(summaryPmrequestStructure);
            } else if (structures.Count() > 0)
            {
                /**
                 * Создаем суммарное подразделение, если число подразделений больше 1
                 */
                PmrequestStructure summaryPmrequestStructure = new PmrequestStructure();
                summaryPmrequestStructure.PmrequestStructureCounts = new List<PmrequestStructureCount>();
                PmrequestStructureCount summaryPSC = new PmrequestStructureCount();


                summaryPSC.CountPositionCategory = new Dictionary<int, double>();
                List<Positioncategory> positioncategoriesSorted = SortPositioncategory(PositioncategoriesLocal().Values);
                foreach (Positioncategory positionCategory in positioncategoriesSorted)
                {
                    summaryPSC.CountPositionCategory.Add(positionCategory.Id, 0);
                }
                summaryPmrequestStructure.PmrequestStructureCounts.Add(summaryPSC);

                foreach (Sourceoffinancing sof in SourceoffinancingsLocal().Values)
                {
                    PmrequestStructureCount pmrequestStructureCount = new PmrequestStructureCount();
                    pmrequestStructureCount.Sourceoffinancing = sof;
                    pmrequestStructureCount.CountPositionCategory = new Dictionary<int, double>();

                    //pmrequestStructureCount.CountPositionCategory.Add(0, 0);

                    foreach (Positioncategory positionCategory in positioncategoriesSorted)
                    {
                        pmrequestStructureCount.CountPositionCategory.Add(positionCategory.Id, 0);
                    }

                    summaryPmrequestStructure.PmrequestStructureCounts.Add(pmrequestStructureCount);
                }

                summaryPmrequestStructure.Name = "Всего";

                list.Add(summaryPmrequestStructure);
            }

            structures = GetStructureHighestWithSameType(structures, date);
            structures = structures.Distinct().ToList();

            //if (pmrequest.Structurerank != null && pmrequest.Structurerank.Length > 0)
            //{
            //    int[] structureranks = pmrequest.Structurerank.Split(',').Select(int.Parse).ToArray();
            //    structures = structures.Where(s => structureranks.Contains(s.Rank));
            //}

            //if (pmrequest.Structuretypes != null && pmrequest.Structuretypes.Length > 0)
            //{
            //    int[] structuretypes = pmrequest.Structuretypes.Split(',').Select(int.Parse).ToArray();
            //    //structures = structures.Where(s => structuretypes.Contains(s.Structuretype));
            //    structures = structures.Where(s => structuretypes.Contains(GetActualStructureInfo(s, date).Structuretype));
            //}

            if (pmrequest.Structurerank != null && pmrequest.Structurerank.Length > 0)
            {
                int[] structureranks = pmrequest.Structurerank.Split(',').Select(int.Parse).ToArray();
                // structures = structures.Where(s => structureranks.Contains(s.Rank));
                structures = structures.Where(s => structureranks.Contains(GetActualStructureInfo(s, date).Rank));
            }

            if (pmrequest.Structurecity != null && pmrequest.Structurecity.Length > 0)
            {
                structures = structures.Where(s => GetActualStructureInfo(s, date).City.Contains(pmrequest.Structurecity));
            }

            if (pmrequest.Structurestreet != null && pmrequest.Structurestreet.Length > 0)
            {
                structures = structures.Where(s => GetActualStructureInfo(s, date).Street.Contains(pmrequest.Structurestreet));
            }

            if (pmrequest.Structureregion != null && pmrequest.Structureregion.Length > 0)
            {
                int[] structureregions = pmrequest.Structureregion.Split(',').Select(int.Parse).ToArray();
                structures = structures.Where(s => structureregions.Contains(GetActualStructureInfo(s, date).Structureregion));
            }

            bool allinclusive = false;
            if (pmrequest.Structurecountallinclusive > 0)
            {
                allinclusive = true;
            }

            /**
             * Сounting (подсчет)
             */
            foreach (Structure structure in structures)
            {
                PmrequestStructure pmrequestStructure = new PmrequestStructure();


                pmrequestStructure.PmrequestStructureCounts = new List<PmrequestStructureCount>();
                PmrequestStructureCount summaryPSC = new PmrequestStructureCount();
                

                summaryPSC.CountPositionCategory = new Dictionary<int, double>();
                List<Positioncategory> positioncategoriesSorted = SortPositioncategory(PositioncategoriesLocal().Values);
                foreach (Positioncategory positionCategory in positioncategoriesSorted)
                {
                    summaryPSC.CountPositionCategory.Add(positionCategory.Id, 0);
                }
                pmrequestStructure.PmrequestStructureCounts.Add(summaryPSC);

                foreach (Sourceoffinancing sof in SourceoffinancingsLocal().Values)
                {
                    PmrequestStructureCount pmrequestStructureCount = new PmrequestStructureCount();
                    pmrequestStructureCount.Sourceoffinancing = sof;
                    pmrequestStructureCount.CountPositionCategory = new Dictionary<int, double>();

                    //pmrequestStructureCount.CountPositionCategory.Add(0, 0);
                    foreach (Positioncategory positionCategory in positioncategoriesSorted)
                    {
                        pmrequestStructureCount.CountPositionCategory.Add(positionCategory.Id, 0);
                    }

                    pmrequestStructure.PmrequestStructureCounts.Add(pmrequestStructureCount);
                }

                Structure actualStructure = GetActualStructureInfo(structure.Id, user.Date.GetValueOrDefault());
                pmrequestStructure.Name = actualStructure.Name;
                pmrequestStructure.Tree = FormTree(actualStructure, false, date, true);
                pmrequestStructure.Rank = actualStructure.Rank.ToString();
                StructureInfo structureInfo = null;

                if (!typeless && !allinclusive)
                {
                    structureInfo = GetStructureInfo(GetStructureOriginalId(structure.Id), user, false, true, true, acceptedTypes);
                } else
                {
                    structureInfo = GetStructureInfo(GetStructureOriginalId(structure.Id), user, false, true, true);
                }
                // 2148.5
                foreach (Position position in structureInfo.Positions)
                {
                    PmrequestStructureCount selectedPSC = pmrequestStructure.PmrequestStructureCounts.FirstOrDefault(psc => psc.Sourceoffinancing != null && psc.Sourceoffinancing.Id == position.Sourceoffinancing);
                    if (selectedPSC != null)
                    {
                        selectedPSC.CountSummary += position.Partval;
                        if (!selectedPSC.CountPositionCategory.ContainsKey(position.Positioncategory))
                        {
                            selectedPSC.CountPositionCategory[position.Positioncategory] = 0;
                        }
                        selectedPSC.CountPositionCategory[position.Positioncategory] = selectedPSC.CountPositionCategory[position.Positioncategory] + position.Partval;

                        if (!typeless) // Запись в "подразделение", отображающее суммарное число.
                        {
                            PmrequestStructureCount selectedPSCSummary = list.First().PmrequestStructureCounts.FirstOrDefault(psc => psc.Sourceoffinancing != null && psc.Sourceoffinancing.Id == position.Sourceoffinancing);
                            selectedPSCSummary.CountSummary += position.Partval;
                            if (!selectedPSCSummary.CountPositionCategory.ContainsKey(position.Positioncategory))
                            {
                                selectedPSCSummary.CountPositionCategory[position.Positioncategory] = 0;
                            }
                            selectedPSCSummary.CountPositionCategory[position.Positioncategory] = selectedPSCSummary.CountPositionCategory[position.Positioncategory] + position.Partval;
                        }
                    }

                    summaryPSC.CountSummary += position.Partval;
                    if (!summaryPSC.CountPositionCategory.ContainsKey(position.Positioncategory))
                    {
                        summaryPSC.CountPositionCategory[position.Positioncategory] = 0;
                    }
                    summaryPSC.CountPositionCategory[position.Positioncategory] = summaryPSC.CountPositionCategory[position.Positioncategory] + position.Partval;

                    if (!typeless) // Запись в "подразделение", отображающее суммарное число.
                    {
                        PmrequestStructureCount summaryPSCSummary = list.First().PmrequestStructureCounts.First();
                        summaryPSCSummary.CountSummary += position.Partval;
                        if (!summaryPSCSummary.CountPositionCategory.ContainsKey(position.Positioncategory))
                        {
                            summaryPSCSummary.CountPositionCategory[position.Positioncategory] = 0;
                        }
                        summaryPSCSummary.CountPositionCategory[position.Positioncategory] = summaryPSCSummary.CountPositionCategory[position.Positioncategory] + position.Partval;
                    }
                    else
                    //else if (structures.Count() > 1) // Запись в "подразделение", отображающее суммарное число.
                    {
                         PmrequestStructureCount summaryPSCSummary = list.First().PmrequestStructureCounts.First();
                        //PmrequestStructureCount summaryPSCSummary = list.First().PmrequestStructureCounts.FirstOrDefault(psc => psc.Sourceoffinancing != null && psc.Sourceoffinancing.Id == position.Sourceoffinancing);
                        summaryPSCSummary.CountSummary += position.Partval;
                        if (!summaryPSCSummary.CountPositionCategory.ContainsKey(position.Positioncategory))
                        {
                            summaryPSCSummary.CountPositionCategory[position.Positioncategory] = 0;
                        }
                        summaryPSCSummary.CountPositionCategory[position.Positioncategory] = summaryPSCSummary.CountPositionCategory[position.Positioncategory] + position.Partval;

                        summaryPSCSummary = list.First().PmrequestStructureCounts.FirstOrDefault(psc => psc.Sourceoffinancing != null && psc.Sourceoffinancing.Id == position.Sourceoffinancing);
                        summaryPSCSummary.CountSummary += position.Partval;
                        if (!summaryPSCSummary.CountPositionCategory.ContainsKey(position.Positioncategory))
                        {
                            summaryPSCSummary.CountPositionCategory[position.Positioncategory] = 0;
                        }
                        summaryPSCSummary.CountPositionCategory[position.Positioncategory] = summaryPSCSummary.CountPositionCategory[position.Positioncategory] + position.Partval;
                    }
                }

                list.Add(pmrequestStructure);
            }


            return list;
        }

        public List<int> FilterStructuresByReadability(User user, List<int> structures)
        {
            List<int> structuresFiltered = new List<int>();
            foreach(int structureid in structures)
            {
                bool hasAccess = IdentityService.CanReadStructure(user, this, structureid);
                if (hasAccess)
                {
                    structuresFiltered.Add(structureid);
                }
            }
            return structuresFiltered;
        }

        /**
         * Выдает информацию о должностях, основанную на запросе.
         */
        public List<PmrequestPosition> GetPmrequestPositionsAddRemove(Pmrequest pmrequest, User user)
        {
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            DateTime date = user.Date.GetValueOrDefault();

            Dictionary<int, Positiontype> positiontypesLocal = PositiontypesLocal();
            Dictionary<int, Sourceoffinancing> sofsLocal = SourceoffinancingsLocal();
            Dictionary<int, Mrd> mrdsLocal = MrdsLocal();
            Dictionary<int, Positionmrd> positionmrdsLocal = LocalizeObject<Positionmrd>(Positionmrds.Select(p => p.Id).ToList(), Positionmrds.ToList());
            Dictionary<int, Rank> ranksLocal = LocalizeObject<Rank>(Ranks.Select(p => p.Id).ToList(), Ranks.ToList());

            List<PmrequestPosition> list = new List<PmrequestPosition>();
            IEnumerable<Position> positions = null;
            List<int> structuretypes = null;

            int[] structuresArray = new int[] { pmrequest.Id };
            //int[] structuresArray = pmrequest.Structures.Split(',').Select(int.Parse).ToArray();
            structuresArray = FilterStructuresByReadability(user, structuresArray.ToList()).ToArray();

            HashSet<int> structures = new HashSet<int>();
            foreach (int i in structuresArray)
            {
                structures.UnionWith(GetStructuresSiblings(i, null, date));
            }
            List<Position> positionsTemp = new List<Position>();
            foreach (int i in structures)
            {
                List<Position> valuesToAdd = new List<Position>();
                if (PositionsStructureAsKeyLocal().ContainsKey(i))
                {
                    valuesToAdd = PositionsStructureAsKeyLocal()[i];
                }
                //List<Position> positionsToAdd = new List<Position>(PositionsLocal().Values.Where(p => p.Structure == i));
                positionsTemp.AddRange(valuesToAdd);
            }

            positions = FilterDeletedPositions(positionsTemp, user.Date.GetValueOrDefault()).ToList();

            positions = positions.Where(p => IsSignedAndWillCreated(p, user.Date.GetValueOrDefault()) || IsSignedAndWillDeleted(p, user.Date.GetValueOrDefault())).ToList();




            /**
             * W R I T I N G    I N F O R M A T I O N
             */
            List<AltrankPrintable> printables = new List<AltrankPrintable>();
            pmrequest.AltrankPrintables = printables;
            //positions = positions.ToList();
            int index = 0;
            foreach (Position position in positions)
            {
                try
                {

                    PmrequestPosition pmrequestPosition = new PmrequestPosition();
                    pmrequestPosition.position = position;
                    pmrequestPosition.Id = position.Id;
                    if (positiontypesLocal.ContainsKey(position.Positiontype))
                    {
                        pmrequestPosition.Positiontype = positiontypesLocal[position.Positiontype].Name;
                    }
                    else
                    {
                        pmrequestPosition.Positiontype = "Должность не определена";
                    }
                    //pmrequestPosition.Positiontype += position.Id; - Нужно было для отладки
                    pmrequestPosition.Tree = FormTree(position, date, true);

                    pmrequestPosition.Part = position.Part;
                    pmrequestPosition.Partval = position.Partval;

                    string rank = "Гражданская должность";
                    if (position.Cap.GetValueOrDefault() != 0)
                    {
                        rank = ranksLocal[position.Cap.GetValueOrDefault()].Name;
                        if (rank == null)
                        {
                            rank = "Гражданская должность";
                        }
                    }
                    pmrequestPosition.Rank = rank;
                    pmrequestPosition.Positioncategory = PositioncategoriesLocal()[position.Positioncategory].Name;
                    pmrequestPosition.Sof = sofsLocal[position.Sourceoffinancing].Name;
                    pmrequestPosition.Mrds = GetMrdsNamesLocal(position.Id, mrdsLocal, positionmrdsLocal);

                    if (position.Replacedbycivil > 0)
                    {
                        pmrequestPosition.ReplacedByCivil = "Может";
                        pmrequestPosition.ReplacedByCivilPositiontype = positiontypesLocal[position.Replacedbycivilpositiontype].Name;
                        if (PositioncategoriesLocal().ContainsKey(position.Replacedbycivilpositioncategory))
                        {
                            pmrequestPosition.ReplacedByCivilPositioncategory = PositioncategoriesLocal()[position.Replacedbycivilpositioncategory].Name;
                        }
                        if (position.Replacedbycivildatelimit > 0)
                        {
                            pmrequestPosition.ReplacedByCivilDate = position.Replacedbycivildate.GetValueOrDefault().ToString("yyyy.MM.dd");
                        }

                        if (position.Civildecree > 0)
                        {
                            pmrequestPosition.ReplacedByCivilDecree = position.Civildecreenumber;
                            pmrequestPosition.ReplacedByCivilDecreeDate = position.Civildecreedate.GetValueOrDefault().ToString("yyyy.MM.dd");
                        }
                    }
                    else
                    {
                        pmrequestPosition.ReplacedByCivil = "Не может";
                        pmrequestPosition.ReplacedByCivilPositiontype = "";
                        pmrequestPosition.ReplacedByCivilPositioncategory = "";
                        pmrequestPosition.ReplacedByCivilDecreeDate = "";
                        pmrequestPosition.ReplacedByCivilDate = "";
                        pmrequestPosition.ReplacedByCivilDecree = "";
                    }


                    DecreeOperationsRequest request = GetDecreeOperationsRequest(position, user.Date.GetValueOrDefault());
                    List<DecreeoperationManagement> doms = RequestDecreeOperations(request);

                    if (IsSignedAndWillCreated(doms))
                    {
                        pmrequestPosition.Signed = "Будет введена";
                    }
                    else
                    {
                        pmrequestPosition.Signed = "Будет сокращена";
                    }

                    if (position.Decertificate > 0)
                    {
                        pmrequestPosition.DecertificateDate = position.Decertificatedate.GetValueOrDefault().ToString("yyyy.MM.dd");
                    }
                    else
                    {
                        pmrequestPosition.DecertificateDate = "Не подлежит";
                    }

                    if (position.Civilrankhigh > 0)
                    {
                        pmrequestPosition.CivilClassHigh = position.Civilrankhigh.ToString();
                        pmrequestPosition.CivilClassLow = position.Civilranklow.ToString();
                    }
                    else
                    {
                        pmrequestPosition.CivilClassHigh = "";
                        pmrequestPosition.CivilClassLow = "";
                    }

                    DecreeoperationManagement dom = doms.FirstOrDefault(d => d.MetaPurposeForSubject == Keys.DECREE_OPERATION_META_PURPOSE_CREATE_SUBJECT_IN_FUTURE ||
                        d.MetaPurposeForSubject == Keys.DECREE_OPERATION_META_PURPOSE_DELETE_SUBJECT_IN_FUTURE );

                    
                    pmrequestPosition.DateCreated = dom.MetaDateActive.ToString("dd.MM.yyyy");
                    pmrequestPosition.DecreeCreationName = dom.MetaDecreeName;
                    pmrequestPosition.DecreeCreationUnofficialName = dom.MetaDecreeNameUnofficial;
                    pmrequestPosition.DecreeCreationNumber = dom.MetaDecreeNumber;

                    /**
                     * Has altranks.
                     */
                    if (position.Altrank > 0)
                    {
                        pmrequest.AnyAltranks = true;
                        AltrankInner altrankInner = new AltrankInner(position, this);
                        AltrankPrintable altrankPrintable = printables.FirstOrDefault(p => p.Altrankconditiongroup.Id == altrankInner.Altrankconditiongroup.Id);
                        if (altrankPrintable == null)
                        {
                            altrankPrintable = new AltrankPrintable();
                            altrankPrintable.Altrankconditiongroup = altrankInner.Altrankconditiongroup;
                            altrankPrintable.Altrankconditionnames = altrankInner.Conditions.Select(c => c.Name).ToList();
                            altrankPrintable.Altranknames = new Dictionary<int, List<string>>();
                            printables.Add(altrankPrintable);
                        }
                        altrankPrintable.Altranknames.Add(position.Id, altrankInner.Ranks.Select(c => c.Name).ToList());
                    }

                    pmrequestPosition.Heading = "";
                    if (position.Head > 0)
                    {

                        Structure headingStructure = StructuresLocal().GetValue(position.Headid);
                        //Structure headingStructure = Structures.FirstOrDefault(s => s.Id == position.Headid);
                        if (headingStructure != null)
                        {
                            pmrequestPosition.Heading = headingStructure.Name;
                        }

                    }

                    pmrequestPosition.Curation = "";
                    if (position.Curator > 0)
                    {
                        int[] curationids = position.Curatorlist.Split(',').Select(int.Parse).ToArray();
                        foreach (int id in curationids)
                        {
                            Structure structure = StructuresLocal().GetValue(id);
                            //Structure structure = Structures.FirstOrDefault(s => s.Id == id);
                            if (structure != null)
                            {
                                pmrequestPosition.Curation += structure.Name + ", ";
                            }

                        }
                        if (pmrequestPosition.Curation.Length > 0)
                        {
                            pmrequestPosition.Curation = pmrequestPosition.Curation.Substring(0, pmrequestPosition.Curation.Length - 2);
                        }
                    }

                    if (position.Notice != null)
                    {
                        pmrequestPosition.Notice = position.Notice;
                    }
                    else
                    {
                        pmrequestPosition.Notice = "";
                    }


                    //pmrequestPosition.Signed = IsNotSignedAndCreated();
                    list.Add(pmrequestPosition);
                    index++;
                }
                catch (Exception ex)
                {

                }

            }
            pmrequest.AltrankPrintables = printables;

            return list;
        }

        /**
         * Выдает информацию о должностях, основанную на запросе.
         */
        public List<PmrequestPosition> GetPmrequestPositions(Pmrequest pmrequest, User user)
        {
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            DateTime date = user.Date.GetValueOrDefault();

            Dictionary<int, Positiontype> positiontypesLocal = PositiontypesLocal();
            Dictionary<int, Sourceoffinancing> sofsLocal = SourceoffinancingsLocal();
            Dictionary<int, Mrd> mrdsLocal = MrdsLocal();
            Dictionary<int, Positionmrd> positionmrdsLocal = LocalizeObject<Positionmrd>(Positionmrds.Select(p => p.Id).ToList(), Positionmrds.ToList());
            Dictionary<int, Rank> ranksLocal = LocalizeObject<Rank>(Ranks.Select(p => p.Id).ToList(), Ranks.ToList());
            //Positiontypes.Select(i => i.)
            //bool hasAccess = IdentityService.CanReadStructure(sessionid, repository, staffManagement.Id);

            List<PmrequestPosition> list = new List<PmrequestPosition>();
            IEnumerable<Position> positions = null;
            List<int> structuretypes = null;

            if (pmrequest.Structuretypes != null && pmrequest.Structuretypes.Length > 0)
            {
                structuretypes = pmrequest.Structuretypes.Split(',').Select(int.Parse).ToList();
                structuretypes = FilterStructuresByReadability(user, structuretypes);

            }

            /**
             * Include only specified structures
             */
            if (pmrequest.Structures != null && pmrequest.Structures.Length > 0)
            {
                int[] structuresArray = pmrequest.Structures.Split(',').Select(int.Parse).ToArray();
                structuresArray = FilterStructuresByReadability(user, structuresArray.ToList()).ToArray();

                HashSet<int> structures = new HashSet<int>();
                foreach (int i in structuresArray)
                {
                    structures.UnionWith(GetStructuresSiblings(i, null, date));
                }
                List<Position> positionsTemp = new List<Position>();
                foreach (int i in structures)
                {
                    List<Position> valuesToAdd = new List<Position>();
                    if (PositionsStructureAsKeyLocal().ContainsKey(i))
                    {
                        valuesToAdd = PositionsStructureAsKeyLocal()[i];
                    }
                    //List<Position> positionsToAdd = new List<Position>(PositionsLocal().Values.Where(p => p.Structure == i));
                    positionsTemp.AddRange(valuesToAdd);
                }

                positions = FilterDeletedPositions(positionsTemp, user.Date.GetValueOrDefault()).ToList(); 
            /**
             * Include all structures
             */
            } else
            {
                List<int> structuresArray = new List<int>();
                foreach(Structure structure in FilterDeletedStructures(StructuresLocal().Values.Where(s => s.Parentstructure == 0), user.Date.GetValueOrDefault()))
                {
                    structuresArray.Add(StructureBaseId(structure));
                }
                HashSet<int> structures = new HashSet<int>();
                foreach (int i in structuresArray)
                {
                    structures.UnionWith(GetStructuresSiblings(i, null, date));
                }
                List<Position> positionsTemp = new List<Position>();
                foreach (int i in structures)
                {
                    List<Position> valuesToAdd = new List<Position>();
                    if (PositionsStructureAsKeyLocal().ContainsKey(i))
                    {
                        valuesToAdd = PositionsStructureAsKeyLocal()[i];
                    }
                    //List<Position> positionsToAdd = new List<Position>(PositionsLocal().Values.Where(p => p.Structure == i));
                    positionsTemp.AddRange(valuesToAdd);
                }
                positions = FilterDeletedPositions(positionsTemp, user.Date.GetValueOrDefault()).ToList();
                //positions = FilterDeletedPositionsQueryable(Positions, user.Date.GetValueOrDefault());
            }

            if (structuretypes != null)
            {
                positions = positions.Where(p => {
                //positions = positions.AsParallel().Where(p => {
                    if (p.Structure == 0)
                    {
                        return false;
                    }
                    //Structure structure = Structures.FirstOrDefault(s => s.Id == p.Structure);
                    Structure structure = null;
                    if (StructuresLocal().ContainsKey(p.Structure))
                    {
                        structure = StructuresLocal()[p.Structure];
                    } else
                    {
                        return false;
                    }

                    return structuretypes.Contains(GetStructureType(structure)); });
                    
            }


            /**
             * Include only replaced by civils
             */
            if (pmrequest.Replacedbycivil > 0)
            {
                positions = positions.Where(p => p.Replacedbycivil > 0);
            }

            if (pmrequest.Notopchs > 0)
            {
                positions = positions.Where(p => p.Opchs == 0);
            }


            /**
             * Exclude replaced by civils
             */
            if (pmrequest.Replacedbycivilnot > 0)
            {
                positions = positions.Where(p => p.Replacedbycivil == 0);
            }

            if (pmrequest.Civil > 0)
            {
                positions = positions.Where(p => p.Cap.GetValueOrDefault() == 0);
            }

            if (pmrequest.Decertificate > 0)
            {
                positions = positions.Where(p => p.Decertificate > 0);
            }

            if (pmrequest.Decertificateexpired > 0)
            {
                positions = positions.Where(p => p.Decertificate > 0 && user.Date.GetValueOrDefault() > p.Decertificatedate);
            }

            if (pmrequest.Replacedbycivildateavailable > 0)
            {
                positions = positions.Where(p => p.Replacedbycivildatelimit > 0);
            }

            if (pmrequest.Replacedbycivildateexpired > 0)
            {
                positions = positions.Where(p => p.Replacedbycivildatelimit > 0 && user.Date.GetValueOrDefault() > p.Replacedbycivildate);
            }

            /**
             * 
             */
            if (pmrequest.Signed > 0 || pmrequest.Notsigned > 0 || pmrequest.Willbenotsigned > 0 || pmrequest.Willbesigned > 0)
            {
                if (pmrequest.Signed == 0)
                {
                    positions = positions.Except(positions.Where(p => IsSignedAndCreated(p, user.Date.GetValueOrDefault())));
                }
                if ( pmrequest.Notsigned == 0)
                {
                    positions = positions.Except(positions.Where(p => IsNotSignedAndCreated(p, user.Date.GetValueOrDefault())));
                } 
            }

            if (pmrequest.Ranks != null && pmrequest.Ranks.Length > 0)
            {
                int[] ranks = pmrequest.Ranks.Split(',').Select(int.Parse).ToArray();
                positions = positions.Where(p => ranks.Contains(p.Cap.GetValueOrDefault()));
            }

            if (pmrequest.Positiontypes != null && pmrequest.Positiontypes.Length > 0)
            {
                int[] positiontypes = pmrequest.Positiontypes.Split(',').Select(int.Parse).ToArray();
                positions = positions.Where(p => positiontypes.Contains(p.Positiontype));
            }
            if (pmrequest.Positioncategories != null && pmrequest.Positioncategories.Length > 0)
            {
                int[] positioncategories = pmrequest.Positioncategories.Split(',').Select(int.Parse).ToArray();
                positions = positions.Where(p => positioncategories.Contains(p.Positioncategory));
            }

            if (pmrequest.Civilclasshigh > 0 && pmrequest.Civilclasslow > 0)
            {
                foreach (Position pos in positions)
                {
                    int civilrankhigh = pos.Civilrankhigh;
                    int pmcivilrankhigh = pmrequest.Civilclasshigh;
                }
                positions = positions.Where(p => p.Civilrankhigh > 0 && pmrequest.Civilclasshigh <= p.Civilrankhigh && pmrequest.Civilclasslow >= p.Civilranklow);

            }

            if (pmrequest.Sofs != null && pmrequest.Sofs.Length > 0)
            {
                int[] sofs = pmrequest.Sofs.Split(',').Select(int.Parse).ToArray();
                positions = positions.Where(p => sofs.Contains(p.Sourceoffinancing)).ToList();
            }
            if (pmrequest.Mrds != null && pmrequest.Mrds.Length > 0)
            {
                int[] mrds = pmrequest.Mrds.Split(',').Select(int.Parse).ToArray();
                //IEnumerable<int> pmids = Positionmrds.Where(pm => pm.Position == p.Id).Select(pm => pm.Mrd);
                positions = positions.Where(p => {
                    IEnumerable<int> pmids = positionmrdsLocal.Values.Where(pm => pm.Position == p.Id).Select(pm => pm.Mrd);
                    foreach (int pmid in pmids)
                    {

                        if (mrds.FirstOrDefault(m => m == pmid) != 0)
                        {
                            return true;
                        }
                    }
                    return false;
                }).ToList();
            }


            /**
             * W R I T I N G    I N F O R M A T I O N
             */
            List<AltrankPrintable> printables = new List<AltrankPrintable>();
            pmrequest.AltrankPrintables = printables;
            //positions = positions.ToList();
            int index = 0;
            foreach (Position position in positions)
            {
                try
                {

                    if (IsSignedAndWillCreated(position, user.Date.GetValueOrDefault()))
                    {
                        continue;
                    }
                
                PmrequestPosition pmrequestPosition = new PmrequestPosition();
                pmrequestPosition.position = position;
                pmrequestPosition.Id = position.Id;
                if (positiontypesLocal.ContainsKey(position.Positiontype))
                    {
                        pmrequestPosition.Positiontype = positiontypesLocal[position.Positiontype].Name;
                    } else
                    {
                        pmrequestPosition.Positiontype = "Должность не определена";
                    }
                    //pmrequestPosition.Positiontype += position.Id; - Нужно было для отладки
                pmrequestPosition.Tree = FormTree(position, date, true);

                pmrequestPosition.Part = position.Part;
                pmrequestPosition.Partval = position.Partval;

                string rank = "Гражданская должность";
                if (position.Cap.GetValueOrDefault() != 0)
                {
                    rank = ranksLocal[position.Cap.GetValueOrDefault()].Name;
                    if (rank == null)
                    {
                        rank = "Гражданская должность";
                    }
                }
                pmrequestPosition.Rank = rank;
                pmrequestPosition.Positioncategory = PositioncategoriesLocal()[position.Positioncategory].Name;
                pmrequestPosition.Sof = sofsLocal[position.Sourceoffinancing].Name;
                pmrequestPosition.Mrds = GetMrdsNamesLocal(position.Id, mrdsLocal, positionmrdsLocal);

                Structure structureForStructuretype = StructuresLocal().GetValue(position.Structure);
                pmrequestPosition.Structuremrd = "";
                if (structureForStructuretype != null)
                {
                    Structure actualStructureForStructuretype = GetActualStructureInfo(structureForStructuretype, date);
                    int structuretypeid = GetStructureType(actualStructureForStructuretype);
                    if (structuretypeid > 0) 
                    {
                        Structuretype structuretype = StructuretypesLocal().GetValue(structuretypeid);
                        pmrequestPosition.Structuremrd = structuretype.Name;
                    }
                    
                }
                

                if (position.Replacedbycivil > 0)
                {
                    pmrequestPosition.ReplacedByCivil = "Может";
                    pmrequestPosition.ReplacedByCivilPositiontype = positiontypesLocal[position.Replacedbycivilpositiontype].Name;
                    if (PositioncategoriesLocal().ContainsKey(position.Replacedbycivilpositioncategory))
                    {
                        pmrequestPosition.ReplacedByCivilPositioncategory = PositioncategoriesLocal()[position.Replacedbycivilpositioncategory].Name;
                    }
                    if (position.Replacedbycivildatelimit > 0)
                    {
                        pmrequestPosition.ReplacedByCivilDate = position.Replacedbycivildate.GetValueOrDefault().ToString("yyyy.MM.dd");
                    }

                    if (position.Civildecree > 0)
                    {
                        pmrequestPosition.ReplacedByCivilDecree = position.Civildecreenumber;
                        pmrequestPosition.ReplacedByCivilDecreeDate = position.Civildecreedate.GetValueOrDefault().ToString("yyyy.MM.dd");
                    }
                } else
                {
                    pmrequestPosition.ReplacedByCivil = "Не может";
                    pmrequestPosition.ReplacedByCivilPositiontype = "";
                    pmrequestPosition.ReplacedByCivilPositioncategory = "";
                    pmrequestPosition.ReplacedByCivilDecreeDate = "";
                    pmrequestPosition.ReplacedByCivilDate = "";
                    pmrequestPosition.ReplacedByCivilDecree = "";
                }

                
                DecreeOperationsRequest request = GetDecreeOperationsRequest(position, user.Date.GetValueOrDefault()); 
                List<DecreeoperationManagement> doms = RequestDecreeOperations(request);
                    
                if (IsSignedAndCreated(doms))
                {
                    pmrequestPosition.Signed = "Должность введена";
                }
                else
                {
                    pmrequestPosition.Signed = "Должность введена, но приказ не подписан";
                }

                if (position.Decertificate > 0)
                {
                    pmrequestPosition.DecertificateDate = position.Decertificatedate.GetValueOrDefault().ToString("yyyy.MM.dd");
                } else
                {
                    pmrequestPosition.DecertificateDate = "Не подлежит";
                }

                if (position.Civilrankhigh > 0)
                {
                    pmrequestPosition.CivilClassHigh = position.Civilrankhigh.ToString();
                    pmrequestPosition.CivilClassLow = position.Civilranklow.ToString();
                } else
                {
                    pmrequestPosition.CivilClassHigh = "";
                    pmrequestPosition.CivilClassLow = "";
                }

                DecreeoperationManagement dom = doms.FirstOrDefault(d => d.MetaPurposeForSubject == Keys.DECREE_OPERATION_META_PURPOSE_CREATED_SUBJECT ||
                    d.MetaPurposeForSubject == Keys.DECREE_OPERATION_META_PURPOSE_CREATED_SUBJECT_NOT_SIGNED || d.MetaPurposeForSubject == Keys.DECREE_OPERATION_META_PURPOSE_CREATE_SUBJECT_IN_FUTURE ||
                    d.MetaPurposeForSubject == Keys.DECREE_OPERATION_META_PURPOSE_CREATE_SUBJECT_IN_FUTURE_NOT_SIGNED);

                pmrequestPosition.DateCreated = dom.MetaDateActive.ToString("yyyy.MM.dd");
                pmrequestPosition.DecreeCreationName = dom.MetaDecreeName;
                pmrequestPosition.DecreeCreationUnofficialName = dom.MetaDecreeNameUnofficial;
                pmrequestPosition.DecreeCreationNumber = dom.MetaDecreeNumber;

                /**
                 * Has altranks.
                 */
                if (position.Altrank > 0)
                {
                    pmrequest.AnyAltranks = true;
                    AltrankInner altrankInner = new AltrankInner(position, this);
                    AltrankPrintable altrankPrintable = printables.FirstOrDefault(p => p.Altrankconditiongroup.Id == altrankInner.Altrankconditiongroup.Id);
                    if (altrankPrintable == null)
                    {
                        altrankPrintable = new AltrankPrintable();
                        altrankPrintable.Altrankconditiongroup = altrankInner.Altrankconditiongroup;
                        altrankPrintable.Altrankconditionnames = altrankInner.Conditions.Select(c => c.Name).ToList();
                        altrankPrintable.Altranknames = new Dictionary<int, List<string>>();
                        printables.Add(altrankPrintable);
                    }
                    altrankPrintable.Altranknames.Add(position.Id, altrankInner.Ranks.Select(c => c.Name).ToList());
                }

                pmrequestPosition.Heading = "";
                if (position.Head > 0)
                {
                    
                    Structure headingStructure = StructuresLocal().GetValue(position.Headid);
                    //Structure headingStructure = Structures.FirstOrDefault(s => s.Id == position.Headid);
                    if (headingStructure != null)
                    {
                        pmrequestPosition.Heading = headingStructure.Name;
                    }
                    
                }

                pmrequestPosition.Curation = "";
                if (position.Curator > 0)
                {
                    int[] curationids = position.Curatorlist.Split(',').Select(int.Parse).ToArray();
                    foreach (int id in curationids)
                    {
                        Structure structure = StructuresLocal().GetValue(id);
                        //Structure structure = Structures.FirstOrDefault(s => s.Id == id);
                        if (structure != null)
                        {
                            pmrequestPosition.Curation += structure.Name + ", ";
                        }
                        
                    }
                    if (pmrequestPosition.Curation.Length > 0)
                    {
                        pmrequestPosition.Curation = pmrequestPosition.Curation.Substring(0, pmrequestPosition.Curation.Length - 2);
                    }
                }

                if (position.Notice != null)
                {
                    pmrequestPosition.Notice = position.Notice;
                } else
                {
                    pmrequestPosition.Notice = "";
                }


                //pmrequestPosition.Signed = IsNotSignedAndCreated();
                list.Add(pmrequestPosition);
                index++;
                } catch (Exception ex)
                {

                } 

            }
            pmrequest.AltrankPrintables = printables;
            
            return list;
        }

        public List<Structure> GetAllStructureFromOneOriginal(Structure structure)
        {
            Structure originalStructure = structure;
            List<Structure> output_list = new List<Structure>();
            if (originalStructure == null)
            {
                return output_list;
            }
            if (structure.Changeorigin != 0)
            {
                originalStructure = GetOriginalStructure(structure);
            }
            foreach (KeyValuePair<int, Structure> key in StructuresLocal())
            {
                if(originalStructure.Id == key.Value.Changeorigin)
                {
                    output_list.Add(key.Value);
                }
            }

            return output_list;
        }

        public Structure GetOriginalStructure(Structure structure)
        {
            Structure originalStructure = structure;
            if (structure.Changeorigin > 0)
            {
                originalStructure = StructuresLocal().GetValue(structure.Changeorigin);
            }
            return originalStructure;
        }

        public Structure GetOriginalStructure(int id)
        {
            Structure originalStructure = StructuresLocal().GetValue(id);
            if (originalStructure == null)
            {
                return null;
            }
            if (originalStructure.Changeorigin > 0)
            {
                //originalStructure = StructuresLocal()[originalStructure.Changeorigin];
                originalStructure = StructuresLocal().GetValue(originalStructure.Changeorigin);
            }
            return originalStructure;
        }

        /**
         * Берет любой список с подразделениям и возвращает список начальных подразделений (к которым прикреплены подчиненные подразделения и должности), исключая дубликаты
         */
        public IEnumerable<Structure> GetOriginalStructures(IEnumerable<Structure> structures)
        {
            SortedDictionary<int, Structure> originalStructuresMap = new SortedDictionary<int, Structure>();
            foreach (Structure structure in structures)
            {
                Structure originalStructure = GetOriginalStructure(structure);
                if (!originalStructuresMap.ContainsKey(originalStructure.Id))
                {
                    originalStructuresMap.Add(originalStructure.Id, originalStructure);
                }
                originalStructuresMap[originalStructure.Id] = originalStructure;
            }

            return originalStructuresMap.Values;
        }

        public int GetStructureOriginalId(Structure structure)
        {
            if (structure == null)
            {
                return 0;
            }

            if (structure.Changeorigin > 0)
            {
                return structure.Changeorigin;
            } else
            {
                return structure.Id;
            }
        }

        public int GetStructureOriginalId(int structureid)
        {
            Structure structure = StructuresLocal().GetValue(structureid);
            if (structure == null)
            {
                return 0;
            }
            if (structure.Changeorigin > 0)
            {
                return structure.Changeorigin;
            }
            else
            {
                return structure.Id;
            }
        }

        /**
         * Берет любой список с подразделениям и возвращает список актуальных подразделений (к которым НЕ прикреплены подчиненные подразделения и должности), исключая дубликаты
         */
        public IEnumerable<Structure> GetActualStructures(IEnumerable<Structure> structures, DateTime date)
        {
            SortedDictionary<int, Structure> actualStructuresMap = new SortedDictionary<int, Structure>();
            foreach (Structure structure in structures)
            {
                Structure actualStructure = GetActualStructureInfo(structure.Id, date);
                if (actualStructure == null)
                {
                    continue;
                }
                int originalid = GetStructureOriginalId(actualStructure);

                if (!actualStructuresMap.ContainsKey(originalid))
                {
                    actualStructuresMap.Add(originalid, actualStructure);
                }
                actualStructuresMap[originalid] = actualStructure;
            }

            return actualStructuresMap.Values;
        }

        public Pmresult GetPmresult(List<PmrequestPosition> pmrequestPositions, Pmrequest pmrequest)
        {
            Pmresult pmresult = new Pmresult();
            List<Position> positions = null;
            List<Position> positionsvar = null;
            if (pmrequest.Ranksexpanded > 0)
            {
                positions = pmrequestPositions.Select(p => p.position).Where(pp => PositioncategoriesLocal().Values.First(pc => pc.Id == pp.Positioncategory).Variable == 0).ToList();
                positionsvar = pmrequestPositions.Select(p => p.position).Where(pp => PositioncategoriesLocal().Values.First(pc => pc.Id == pp.Positioncategory).Variable > 0).ToList();
            } else
            {
                positions = pmrequestPositions.Select(p => p.position).ToList();
                positionsvar = new List<Position>();
            }

            foreach (Position position in positions)
            {
                pmresult.Count += position.Partval;
            }

            foreach (Position position in positionsvar)
            {
                pmresult.Count += position.Partval;
                pmresult.Countvar += position.Partval;
            }

            pmresult.Ranksexpanded = pmrequest.Ranksexpanded;
            List<Rank> requestedRanks = new List<Rank>();
            requestedRanks.AddRange(Ranks); // because we need fetch all ranks


            requestedRanks = requestedRanks.OrderBy(r => r.Order).ToList();
            Dictionary<int, PmresultSinglerank> rankInfos = new Dictionary<int, PmresultSinglerank>(); // int - order
            foreach (Rank rank in Ranks.OrderBy(r => r.Order))
            {
                rankInfos.Add(rank.Order.GetValueOrDefault(), new PmresultSinglerank());
            }
            Dictionary<int, double> civil = new Dictionary<int, double>();
            foreach (Position position in positions)
            {
                if (position.Cap.GetValueOrDefault() == 0) // CIVIL
                {

                    if (!civil.ContainsKey(position.Positioncategory))
                    {
                        civil.Add(position.Positioncategory, position.Partval);
                    }
                    else
                    {
                        civil[position.Positioncategory] += position.Partval;
                    }
                }
                else if (position.Altrank > 0) // HAS ALTRANKS
                {
                    AltrankInner altrankInner = new AltrankInner(position, this);
                    rankInfos[position.Cap.GetValueOrDefault()].Defaultcount++;
                    rankInfos[position.Cap.GetValueOrDefault()].Absolutecount++;
                    rankInfos[position.Cap.GetValueOrDefault()].Reduce++;
                    rankInfos[altrankInner.RanksMinMax.Value.Order.GetValueOrDefault()].Maxcount++;
                    int dif = altrankInner.RanksMinMax.Value.Order.GetValueOrDefault() - position.Cap.GetValueOrDefault();
                    // Upcount
                    if (!rankInfos[position.Cap.GetValueOrDefault()].Upcount.ContainsKey(altrankInner.Altrankconditiongroup.Id))
                    {
                        rankInfos[position.Cap.GetValueOrDefault()].Upcount.Add(altrankInner.Altrankconditiongroup.Id, new Dictionary<int, int>());
                    }
                    if (!rankInfos[position.Cap.GetValueOrDefault()].Upcount[altrankInner.Altrankconditiongroup.Id].ContainsKey(dif))
                    {
                        rankInfos[position.Cap.GetValueOrDefault()].Upcount[altrankInner.Altrankconditiongroup.Id].Add(dif, 1);
                    } else
                    {
                        rankInfos[position.Cap.GetValueOrDefault()].Upcount[altrankInner.Altrankconditiongroup.Id][dif] += 1;
                    }

                    if (!rankInfos[position.Cap.GetValueOrDefault()].UpcountUnited.ContainsKey(dif))
                    {
                        rankInfos[position.Cap.GetValueOrDefault()].UpcountUnited.Add(dif, 1);
                    }
                    else
                    {
                        rankInfos[position.Cap.GetValueOrDefault()].UpcountUnited[dif] += 1;
                    }

                    // Came from other ranks
                    if (!rankInfos[position.Cap.GetValueOrDefault() + dif].CameFromUnited.ContainsKey(dif))
                    {
                        rankInfos[position.Cap.GetValueOrDefault() + dif].CameFromUnited.Add(dif, 1);
                    }
                    else
                    {
                        rankInfos[position.Cap.GetValueOrDefault() + dif].CameFromUnited[dif] += 1;
                    }

                    bool sumunitedcalculated = false;
                    while (dif > 0)
                    {
                        rankInfos[position.Cap.GetValueOrDefault() + dif].Absolutecount++;

                        // Came from other ranks
                        if (!rankInfos[position.Cap.GetValueOrDefault() + dif].CameFrom.ContainsKey(altrankInner.Altrankconditiongroup.Id))
                        {
                            rankInfos[position.Cap.GetValueOrDefault() + dif].CameFrom.Add(altrankInner.Altrankconditiongroup.Id, new Dictionary<int, int>());
                        }
                        if (!rankInfos[position.Cap.GetValueOrDefault() + dif].CameFrom[altrankInner.Altrankconditiongroup.Id].ContainsKey(dif))
                        {
                            rankInfos[position.Cap.GetValueOrDefault() + dif].CameFrom[altrankInner.Altrankconditiongroup.Id].Add(dif, 1);
                        }
                        else
                        {
                            rankInfos[position.Cap.GetValueOrDefault() + dif].CameFrom[altrankInner.Altrankconditiongroup.Id][dif] += 1;
                        }

                        // Сколько переменных должностей прошлой мимо этой должности.  1) этап от начала 2) количество таких прошедших 
                        if (!rankInfos[position.Cap.GetValueOrDefault() + dif].ComethroughUnited.ContainsKey(dif))
                        {
                            rankInfos[position.Cap.GetValueOrDefault() + dif].ComethroughUnited.Add(dif, 1);
                        }
                        else
                        {
                            rankInfos[position.Cap.GetValueOrDefault() + dif].ComethroughUnited[dif] += 1;
                        }

                        dif--;
                    }
                        
                } else // HASN'T
                {
                    rankInfos[position.Cap.GetValueOrDefault()].Defaultcount++;
                    rankInfos[position.Cap.GetValueOrDefault()].Absolutecount++;
                    rankInfos[position.Cap.GetValueOrDefault()].Maxcount++;
                }
                    
            }

            /**
             *  Переменный состав.
             */
            foreach (Position position in positionsvar)
            {
                rankInfos[position.Cap.GetValueOrDefault()].Defaultcountvar++;
            }

            /**
                * We generated separate information block for each rank
                */
            List<PmresultSinglerank> pmresultSingleranks = new List<PmresultSinglerank>();
            foreach (Rank requestedRank in requestedRanks)
            {
                //PmresultSinglerank pmresultSinglerank = new PmresultSinglerank();
                PmresultSinglerank pmresultSinglerank = rankInfos[requestedRank.Order.GetValueOrDefault()];
                pmresultSinglerank.Rank = requestedRank;
                pmresultSingleranks.Add(pmresultSinglerank);
            }

            pmresult.Civil = "";
            // Государственные служащие=1,Cлужащие=5
            foreach(KeyValuePair<int, double> civilPair in civil)
            {
                pmresult.Civil += PositioncategoriesLocal().Values.First(p => p.Id == civilPair.Key).Name + "=" + civilPair.Value.ToString() + "@";
            }
            if (pmresult.Civil.Length > 0)
            {
                pmresult.Civil = pmresult.Civil.Substring(0, pmresult.Civil.Length - 1);
            }


            if (!(pmrequest.Ranks == null || pmrequest.Ranks.Length == 0))
            {
                IEnumerable<int> ids = pmrequest.Ranks.Split(',').Select(Int32.Parse);
                List<PmresultSinglerank> remove = new List<PmresultSinglerank>();
                foreach (PmresultSinglerank pmresultSinglerank in pmresultSingleranks)
                {
                    if (!ids.Contains(pmresultSinglerank.Rank.Id))
                    {
                        remove.Add(pmresultSinglerank);
                    }
                }
                pmresultSingleranks.RemoveAll(r => remove.Contains(r));
            }




            // Paste info
            if (pmresultSingleranks.Count > 0)
            {
                pmresult.Ranks = "";
                pmresult.Defaultcount = "";
                pmresult.Defaultcountvar = "";
                pmresult.Absolutecount = "";
                pmresult.Maxcount = "";
                pmresult.Mincount = "";
                pmresult.Uprankready = "";
                pmresult.Uprankunited = "";
                pmresult.Unitedlengthmax = "";
                pmresult.Sumunited = "";

                /**
                 * Calculate max difference
                 */
                int absoluteUnitedlengthmax = 0;
                foreach (PmresultSinglerank pmresultSinglerank in pmresultSingleranks)
                {
                    
                    if (pmresultSinglerank.UpcountUnited.Count > absoluteUnitedlengthmax)
                    {
                        absoluteUnitedlengthmax = pmresultSinglerank.UpcountUnited.Count;
                    }
                    if (pmresultSinglerank.CameFromUnited.Count > absoluteUnitedlengthmax)
                    {
                        absoluteUnitedlengthmax = pmresultSinglerank.CameFromUnited.Count;
                    }
                }

                foreach (PmresultSinglerank pmresultSinglerank in pmresultSingleranks)
                {
                    pmresult.Ranks += pmresultSinglerank.Rank.Name + "@";
                    pmresult.Defaultcount += pmresultSinglerank.Defaultcount + "@";
                    pmresult.Defaultcountvar += pmresultSinglerank.Defaultcountvar + "@";
                    pmresult.Absolutecount += pmresultSinglerank.Absolutecount + "@";
                    int mincount = pmresultSinglerank.Defaultcount - pmresultSinglerank.Reduce;
                    pmresult.Mincount += mincount + "@";
                    //int maxcount = pmresultSinglerank.Absolutecount - pmresultSinglerank.Reduce;
                    pmresult.Maxcount += pmresultSinglerank.Maxcount + "@";

                    
                    // Из них при наличии ученой степени степени получить звание майор внутренней службы 10 ед., подполковник внутренней службы 20 ед. &
                    if (pmresultSinglerank.Upcount != null && pmresultSinglerank.Upcount.Count > 0)
                    {
                        pmresult.Uprankready += "Из них ";
                        foreach (KeyValuePair<int, Dictionary<int, int>> up in pmresultSinglerank.Upcount)
                        {
                            pmresult.Uprankready += Altrankconditiongroups.First(ar => ar.Id == up.Key).Name.ToLower() + " могут получить звание ";
                            foreach (KeyValuePair<int, int> rankcount in up.Value)
                            {
                                Rank rank = Ranks.First(r => r.Order == pmresultSinglerank.Rank.Order + rankcount.Key);
                                pmresult.Uprankready += rank.Name.ToLower() + " " + rankcount.Value + " ед., ";
                            }
                        }
                        pmresult.Uprankready = pmresult.Uprankready.Substring(0, pmresult.Uprankready.Length - 2);
                    }
                    pmresult.Uprankready += "&";

                    //public string Uprankunited { get; set; } // Капитан внутренней службы:1:5;Майор внутренней службы:2:5;

                    int unitedlengthmax = 0;
                    if (pmresultSinglerank.UpcountUnited.Count > pmresultSinglerank.CameFromUnited.Count)
                    {
                        unitedlengthmax = pmresultSinglerank.UpcountUnited.Count;
                    } else
                    {
                        unitedlengthmax = pmresultSinglerank.CameFromUnited.Count;
                    }
                    pmresult.Unitedlengthmax += unitedlengthmax + "@";

                    bool anyupcountunited = false;
                    // Капитан внутренней службы:1:5;Майор внутренней службы:2:5;
                    foreach (KeyValuePair<int, int> up in pmresultSinglerank.UpcountUnited)
                    {
                        pmresult.Uprankunited += Ranks.First(r => r.Order == pmresultSinglerank.Rank.Order + up.Key).Name + ':';
                        pmresult.Uprankunited += up.Key.ToString() + ':';
                        pmresult.Uprankunited += up.Value.ToString() + ';';
                        anyupcountunited = true;
                    }
                    if (anyupcountunited)
                    {
                        pmresult.Uprankunited = pmresult.Uprankunited.Substring(0, pmresult.Uprankunited.Length - 1);
                    }
                    pmresult.Uprankunited += "@";

                    bool anycomefromunited = false;
                    //    public string Comefromunited { get; set; } // Старший лейтенант внутренней службы:1:5;Капитан внутренней службы:2:5;
                    foreach (KeyValuePair<int, int> comefrom in pmresultSinglerank.CameFromUnited)
                    {
                        pmresult.Comefromunited += Ranks.First(r => r.Order == pmresultSinglerank.Rank.Order - comefrom.Key).Name + ':';
                        pmresult.Comefromunited += comefrom.Key.ToString() + ':';
                        pmresult.Comefromunited += comefrom.Value.ToString() + ';';
                        anycomefromunited = true;
                    }
                    if (anycomefromunited)
                    {
                        pmresult.Comefromunited = pmresult.Comefromunited.Substring(0, pmresult.Comefromunited.Length - 1);
                    }
                    pmresult.Comefromunited += "@";


                    bool anysumunited = false;
                    /**
                     * Calculate sum united. 100:120:100,100:100:100,
                     */
                    for (int i = 1; i <= absoluteUnitedlengthmax; i++)
                    {
                        int newdefaultcount = pmresultSinglerank.Defaultcount - pmresultSinglerank.Reduce;
                        if (pmresultSinglerank.ComethroughUnited.ContainsKey(i))
                        {
                            newdefaultcount += pmresultSinglerank.ComethroughUnited[i];
                        }
                        pmresult.Sumunited += newdefaultcount.ToString() + ":";
                        anysumunited = true;
                    }
                    if (anysumunited)
                    {
                        pmresult.Sumunited = pmresult.Sumunited.Substring(0, pmresult.Sumunited.Length - 1);
                    }
                    pmresult.Sumunited += "@";
                }
                pmresult.Ranks = pmresult.Ranks.Substring(0, pmresult.Ranks.Length - 1);
                pmresult.Defaultcount = pmresult.Defaultcount.Substring(0, pmresult.Defaultcount.Length - 1);
                pmresult.Defaultcountvar = pmresult.Defaultcountvar.Substring(0, pmresult.Defaultcountvar.Length - 1);
                pmresult.Absolutecount = pmresult.Absolutecount.Substring(0, pmresult.Absolutecount.Length - 1);
                pmresult.Maxcount = pmresult.Maxcount.Substring(0, pmresult.Maxcount.Length - 1);
                pmresult.Mincount = pmresult.Mincount.Substring(0, pmresult.Mincount.Length - 1);
                pmresult.Uprankready = pmresult.Uprankready.Substring(0, pmresult.Uprankready.Length - 1);
                pmresult.Uprankunited = pmresult.Uprankunited.Substring(0, pmresult.Uprankunited.Length - 1);
                pmresult.Comefromunited = pmresult.Comefromunited.Substring(0, pmresult.Comefromunited.Length - 1);
                pmresult.Sumunited = pmresult.Sumunited.Substring(0, pmresult.Sumunited.Length - 1);
                pmresult.Unitedlengthmax = pmresult.Unitedlengthmax.Substring(0, pmresult.Unitedlengthmax.Length - 1);

                /**
                 * Additional calculations for Ranks expanded.
                 */
                if (pmresult.Ranksexpanded > -1) // -1 Because it is calculated for all cases for now.
                {
                    pmresult.Uppedcount = "";
                    pmresult.Uppedmap = "";

                    // Fullfil uppedmap
                    // Карта: Название=Айди;На сколько ранков максимум доступен подъем.  При наличии классности=1;2
                    Dictionary<int, int> uppedmap = new Dictionary<int, int>();
                    foreach (PmresultSinglerank pmresultSinglerank in pmresultSingleranks)
                    {
                        if (pmresultSinglerank.Upcount != null && pmresultSinglerank.Upcount.Count > 0)
                        {
                            foreach (KeyValuePair<int, Dictionary<int, int>> up in pmresultSinglerank.Upcount)
                            {
                                if (!uppedmap.ContainsKey(up.Key))
                                {
                                    uppedmap.Add(up.Key, 1);
                                }
                                foreach (KeyValuePair<int, int> rankcount in up.Value)
                                {
                                    if (rankcount.Key > uppedmap[up.Key])
                                    {
                                        uppedmap[up.Key] = rankcount.Key;
                                    }
                                }
                            }
                        }
                    }
                    if (pmresult.Uppedmap.Length > 0)
                    {
                        pmresult.Uppedmap = pmresult.Uppedmap.Substring(0, pmresult.Uppedmap.Length - 1);
                    }

                    bool anyGroup = false;
                    // Fullfil 
                    //При наличии классности=1:100;2:120&При наличии ученой степени=1:5, 
                    foreach (PmresultSinglerank pmresultSinglerank in pmresultSingleranks)
                    {
                        //bool anyFrom = pmresultSinglerank.CameFrom.Count > 0; // Доходят ли какие должности до этого звания
                        //bool anyUp = pmresultSinglerank.Upcount.Count > 0; // Есть ли должности, которые стартуют с этого звания и уходят вверх. 

                        
                        // group id - max up.
                        foreach (KeyValuePair<int,int> uppedPart in uppedmap)
                        {
                            pmresult.Uppedcount += Altrankconditiongroups.First(ar => ar.Id == uppedPart.Key).Name.ToLower() + "=";
                            int up = 1;
                            bool anyUpped = false;
                            while (up <= uppedPart.Value)
                            {
                                pmresult.Uppedcount += up + ":";
                                int count = pmresultSinglerank.Defaultcount - pmresultSinglerank.Reduce ;
                                if (pmresultSinglerank.CameFrom.ContainsKey(uppedPart.Key))
                                {
                                    bool anyPlus = false;
                                    foreach (KeyValuePair<int,int> cameFromPair in pmresultSinglerank.CameFrom[uppedPart.Key])
                                    {
                                        if (!anyPlus && cameFromPair.Key == up)
                                        {
                                            count += cameFromPair.Value;
                                        }
                                        anyPlus = true;
                                    }
                                    //count += pmresultSinglerank.CameFrom[uppedPart.Key][up];
                                }
                                pmresult.Uppedcount += count;
                                pmresult.Uppedcount += ";";
                                anyUpped = true;
                                up += 1;
                            }
                            if (anyUpped)
                            {
                                pmresult.Uppedcount = pmresult.Uppedcount.Substring(0, pmresult.Uppedcount.Length - 1);
                            }

                            pmresult.Uppedcount += "&";
                            anyGroup = true;
                        }
                        if (anyGroup)
                        {
                            pmresult.Uppedcount = pmresult.Uppedcount.Substring(0, pmresult.Uppedcount.Length - 1);
                        }

                        pmresult.Uppedcount += "@";
                    }
                    pmresult.Uppedcount = pmresult.Uppedcount.Substring(0, pmresult.Uppedcount.Length - 1);
                    
                }
            }
            

            return pmresult;
        }

        public static T Clone<T>(T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }

        public Dictionary<int, T> LocalizeObject<T>(List<int> keys, List<T> values)
        {
            Dictionary<int, T> dictionary = new Dictionary<int, T>();
            int index = 0;
            foreach(int key in keys)
            {
                dictionary.Add(key, values[index]);
                index++;
            }
            return dictionary;
        }

       
        /**
         * Returns expanded (detailed) Decreeoperations based on a request.
         */
        public List<DecreeoperationManagement> RequestDecreeOperations(DecreeOperationsRequest request, bool full_output_flag = false)
        {
            
            // 0 - no purpose, 1 - no purpose not signed, 
            // 2 - will create subject in future, 3 - will create subject in future not signed,
            // 4 - already created subject, 5 - already created subject not signed, 6 - will delete subject in future
            // 7 - will delete subject in future not signed,
            // 12 - deleted, 13 - deleted not signed, 14 - renamed not signed, 15 - will be renamed,
            // 16 - will be renamed not signed
            List<DecreeoperationManagement> decreeoperationManagements = new List<DecreeoperationManagement>();

            /**
             * Initializing variables
             */
            bool detailed = false;
            List<Decreeoperation> operationsAll = new List<Decreeoperation>();
            List<DecreeoperationManagement> decreeoperationManagementsForRequest = null;
            DecreeoperationManagement decreeoperationManagementCreated = null;
            DecreeoperationManagement decreeoperationManagementCreatedNotSigned = null;
            DecreeoperationManagement decreeoperationManagementCreatedFuture = null;
            DecreeoperationManagement decreeoperationManagementCreatedFutureNotSigned = null;
            DecreeoperationManagement decreeoperationManagementDeleted = null;
            DecreeoperationManagement decreeoperationManagementDeletedNotSigned = null;
            DecreeoperationManagement decreeoperationManagementDeletedFuture = null;
            DecreeoperationManagement decreeoperationManagementDeletedFutureNotSigned = null;
            DecreeoperationManagement decreeoperationManagementRenamed = null;
            DecreeoperationManagement decreeoperationManagementRenamedNotSigned = null;
            DecreeoperationManagement decreeoperationManagementRenamedFuture = null;
            DecreeoperationManagement decreeoperationManagementRenamedFutureNotSigned = null;

            /**
             * Request date has time priority.
             */
            request.RequestedDate = request.RequestedDate.AddHours(23);
            if (request.SubjectID < 0)
            {
                /**
                 * STRUCTURES.
                 */
                    detailed = false;
                if (request.Detailed > 0)
                {
                    detailed = true;
                }
                if (request.Subjectidstructureupdate != 0)
                {
                    int minusid = request.Subjectidstructureupdate;

                    //operationsAll = DecreeoperationsLocal().Values.Where(d => d.Subject == request.Subjectidstructureupdate).ToList();
                    if (DecreeoperationsSubjectAsKeyLocal().ContainsKey(request.Subjectidstructureupdate))
                    {
                        operationsAll = DecreeoperationsSubjectAsKeyLocal()[request.Subjectidstructureupdate];
                    } else
                    {
                        //operationsAll = new List<Decreeoperation>();
                    }
                    

                } else
                {
                    int minusid = request.SubjectID;
                    //operationsAll = DecreeoperationsLocal().Values.Where(d => d.Subject == minusid).ToList();
                    if (DecreeoperationsSubjectAsKeyLocal() == null)
                    {
                        UpdateDecreeoperationsLocal();
                    }
                    if (DecreeoperationsSubjectAsKeyLocal().ContainsKey(minusid))
                    {
                        operationsAll = DecreeoperationsSubjectAsKeyLocal()[minusid];
                    }
                    else
                    {
                        //operationsAll = new List<Decreeoperation>();
                    }
                        
                }
                decreeoperationManagementsForRequest = new List<DecreeoperationManagement>();
                decreeoperationManagementCreated = null;
                decreeoperationManagementCreatedNotSigned = null;
                decreeoperationManagementCreatedFuture = null;
                decreeoperationManagementCreatedFutureNotSigned = null;
                decreeoperationManagementDeleted = null;
                decreeoperationManagementDeletedNotSigned = null;
                decreeoperationManagementDeletedFuture = null;
                decreeoperationManagementDeletedFutureNotSigned = null;
                decreeoperationManagementRenamed = null;
                decreeoperationManagementRenamedNotSigned = null;
                decreeoperationManagementRenamedFuture = null;
                decreeoperationManagementRenamedFutureNotSigned = null;

                foreach (Decreeoperation operation in operationsAll)
                {
                    DecreeoperationManagement decreeoperationManagement = new DecreeoperationManagement();
                    decreeoperationManagement.MetaPurposeForSubject = 0;
                    decreeoperationManagement.Fulfill(operation);
                    Decree decree = null;
                    if (DecreesLocal() == null)
                    {
                        UpdateDecreesLocal();
                    }
                    if (DecreesLocal().ContainsKey(operation.Decree))
                    {
                        decree = DecreesLocal()[operation.Decree];
                    }

                    decreeoperationManagement.MetaDateActive = operation.Dateactive.GetValueOrDefault();
                    if (operation.Datecustom == 0)
                    {
                        decreeoperationManagement.MetaDateActive = decree.Dateactive.GetValueOrDefault();
                    }


                    bool signed = false;
                    if (decree.Signed > 0 && request.RequestedDate >= decree.Datesigned.GetValueOrDefault())
                    {
                        signed = true;
                    }
                    if (request.Padding > 0)
                    {
                        signed = true;
                    }
                    decreeoperationManagement.MetaDecreeName = decree.Name;
                    decreeoperationManagement.MetaDecreeNameUnofficial = decree.Nickname;
                    decreeoperationManagement.MetaDecreeNumber = decree.Number;

                    /**
                     * Created part.
                     */
                    if (decreeoperationManagement.Created > 0)
                    {
                        /**
                         * All signed.
                         */
                        if (signed)
                        {
                            /**
                             * Future
                             */
                            if (decreeoperationManagement.MetaDateActive > request.RequestedDate)
                            {
                                /**
                                 * Already have.
                                 */
                                if (decreeoperationManagementCreatedFuture != null && decreeoperationManagementCreatedFuture.MetaDateActive < decreeoperationManagement.MetaDateActive)
                                {
                                    decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE;
                                    decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                } else
                                {
                                    if (decreeoperationManagementCreatedFuture != null)
                                    {
                                        decreeoperationManagementCreatedFuture.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE;
                                    }

                                    decreeoperationManagementCreatedFuture = decreeoperationManagement;
                                    decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_CREATE_SUBJECT_IN_FUTURE;
                                    decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                }
                                /**
                                 * Current
                                 */
                            } else
                            {
                                /**
                                 * Already have.
                                 */
                                if (decreeoperationManagementCreated != null && decreeoperationManagementCreated.MetaDateActive > decreeoperationManagement.MetaDateActive)
                                {
                                    decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE;
                                    decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                }
                                else
                                {
                                    if (decreeoperationManagementCreated != null)
                                    {
                                        decreeoperationManagementCreated.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE;
                                    }


                                    decreeoperationManagementCreated = decreeoperationManagement;
                                    decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_CREATED_SUBJECT;
                                    decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                }
                            }
                            /**
                             * Not signed.
                             */
                        } else
                        {
                            /**
                             * Future
                             */
                            if (decreeoperationManagement.MetaDateActive > request.RequestedDate)
                            {
                                /**
                                 * Already have.
                                 */
                                if (decreeoperationManagementCreatedFutureNotSigned != null && decreeoperationManagementCreatedFutureNotSigned.MetaDateActive < decreeoperationManagement.MetaDateActive)
                                {
                                    decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE_NOT_SIGNED;
                                    decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                }
                                else
                                {
                                    if (decreeoperationManagementCreatedFutureNotSigned != null)
                                    {
                                        decreeoperationManagementCreatedFutureNotSigned.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE_NOT_SIGNED;
                                    }

                                    decreeoperationManagementCreatedFutureNotSigned = decreeoperationManagement;
                                    decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_CREATE_SUBJECT_IN_FUTURE_NOT_SIGNED;
                                    decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                }
                                /**
                                 * Current
                                 */
                            }
                            else
                            {
                                /**
                                 * Already have.
                                 */
                                if (decreeoperationManagementCreatedNotSigned != null && decreeoperationManagementCreatedNotSigned.MetaDateActive > decreeoperationManagement.MetaDateActive)
                                {
                                    decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE_NOT_SIGNED;
                                    decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                }
                                else
                                {
                                    if (decreeoperationManagementCreatedNotSigned != null)
                                    {
                                        decreeoperationManagementCreatedNotSigned.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE_NOT_SIGNED;
                                    }
                                    decreeoperationManagementCreatedNotSigned = decreeoperationManagement;
                                    decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_CREATED_SUBJECT_NOT_SIGNED;
                                    decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                }
                            }
                        }
                    }
                    /**
                     * Deleted part.
                     */
                    if (decreeoperationManagement.Deleted > 0)
                    {
                        /**
                         * All signed.
                         */
                        if (signed)
                        {
                            /**
                             * Future
                             */
                            if (decreeoperationManagement.MetaDateActive > request.RequestedDate)
                            {
                                /**
                                 * Already have.
                                 */
                                if (decreeoperationManagementDeletedFuture != null && decreeoperationManagementDeletedFuture.MetaDateActive < decreeoperationManagement.MetaDateActive)
                                {
                                    decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE;
                                    decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                }
                                else
                                {
                                    if (decreeoperationManagementDeletedFuture != null)
                                    {
                                        decreeoperationManagementDeletedFuture.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE;
                                    }
                                    decreeoperationManagementDeletedFuture = decreeoperationManagement;
                                    decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_DELETE_SUBJECT_IN_FUTURE;
                                    decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                }
                                /**
                                 * Current
                                 */
                            }
                            else
                            {
                                /**
                                 * Already have.
                                 */
                                if (decreeoperationManagementDeleted != null && decreeoperationManagementDeleted.MetaDateActive > decreeoperationManagement.MetaDateActive)
                                {
                                    decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE;
                                    decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                }
                                else
                                {
                                    if (decreeoperationManagementDeleted != null)
                                    {
                                        decreeoperationManagementDeleted.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE;
                                    }
                                    decreeoperationManagementDeleted = decreeoperationManagement;
                                    decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_DELETED_SUBJECT;
                                    decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                }
                            }
                            /**
                             * Not signed.
                             */
                        }
                        else
                        {
                            /**
                             * Future
                             */
                            if (decreeoperationManagement.MetaDateActive > request.RequestedDate)
                            {
                                /**
                                 * Already have.
                                 */
                                if (decreeoperationManagementDeletedFutureNotSigned != null && decreeoperationManagementDeletedFutureNotSigned.MetaDateActive < decreeoperationManagement.MetaDateActive)
                                {
                                    decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE_NOT_SIGNED;
                                    decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                }
                                else
                                {
                                    if (decreeoperationManagementDeletedFutureNotSigned != null)
                                    {
                                        decreeoperationManagementDeletedFutureNotSigned.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE_NOT_SIGNED;
                                    }
                                    decreeoperationManagementDeletedFutureNotSigned = decreeoperationManagement;
                                    decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_DELETE_SUBJECT_IN_FUTURE_NOT_SIGNED;
                                    decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                }
                                /**
                                 * Current
                                 */
                            }
                            else
                            {
                                /**
                                 * Already have.
                                 */
                                if (decreeoperationManagementDeletedNotSigned != null && decreeoperationManagementDeletedNotSigned.MetaDateActive > decreeoperationManagement.MetaDateActive)
                                {
                                    decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE_NOT_SIGNED;
                                    decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                }
                                else
                                {
                                    if (decreeoperationManagementDeletedNotSigned != null)
                                    {
                                        decreeoperationManagementDeletedNotSigned.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE_NOT_SIGNED;
                                    }
                                    decreeoperationManagementDeletedNotSigned = decreeoperationManagement;
                                    decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_DELETED_SUBJECT_NOT_SIGNED;
                                    decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                }
                            }
                        }
                    }
                    /**
                     * Renamed part. TODO: ПРОДУМАТЬ МЕХАНИЗМ ИЗМЕНЕНИЯ НИКНЕЙМА У ПОДРАЗДЕЛЕНИЙ. СЕЙЧАС ОН ПРИВЯЗАН К СТРУКТУРАМ.
                     */
                    if (decreeoperationManagement.Changed > 0)
                    {
                        /**
                         * All signed.
                         */
                        if (signed)
                        {
                            /**
                             * Future
                             */
                            if (decreeoperationManagement.MetaDateActive > request.RequestedDate)
                            {
                                /**
                                 * Already have.
                                 */
                                if (decreeoperationManagementRenamedFuture != null && decreeoperationManagementRenamedFuture.MetaDateActive < decreeoperationManagement.MetaDateActive)
                                {
                                    decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE;
                                    decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                }
                                else
                                {
                                    if (decreeoperationManagementRenamedFuture != null)
                                    {
                                        decreeoperationManagementRenamedFuture.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE;
                                    }

                                    decreeoperationManagementRenamedFuture = decreeoperationManagement;
                                    decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_RENAMED_SUBJECT_IN_FUTURE;
                                    decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                }
                                /**
                                 * Current
                                 */
                            }
                            else
                            {
                                if (request.full_output_flag)
                                {
                                    decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                }
                                // We doesn't send information if department is ALREADY renamed and it is signed.
                            }
                            /**
                             * Not signed.
                             */
                        }
                        else
                        {
                            /**
                             * Future
                             */
                            if (decreeoperationManagement.MetaDateActive > request.RequestedDate)
                            {
                                /**
                                 * Already have.
                                 */
                                if (decreeoperationManagementRenamedFutureNotSigned != null && decreeoperationManagementRenamedFutureNotSigned.MetaDateActive < decreeoperationManagement.MetaDateActive)
                                {
                                    decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE_NOT_SIGNED;
                                    decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                }
                                else
                                {
                                    if (decreeoperationManagementRenamedFutureNotSigned != null)
                                    {
                                        decreeoperationManagementRenamedFutureNotSigned.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE_NOT_SIGNED;
                                    }

                                    decreeoperationManagementRenamedFutureNotSigned = decreeoperationManagement;
                                    decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_RENAMED_SUBJECT_IN_FUTURE_NOT_SIGNED;
                                    decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                }
                            }
                            else
                            /**
                             * Current
                             */
                            {
                                /**
                                 * Already have.
                                 */
                                if (decreeoperationManagementRenamedNotSigned != null && decreeoperationManagementRenamedNotSigned.MetaDateActive > decreeoperationManagement.MetaDateActive)
                                {
                                    decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE_NOT_SIGNED;
                                    decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                }
                                else
                                {
                                    if (decreeoperationManagementRenamedNotSigned != null)
                                    {
                                        decreeoperationManagementRenamedNotSigned.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE_NOT_SIGNED;
                                    }
                                    decreeoperationManagementRenamedNotSigned = decreeoperationManagement;
                                    decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_RENAMED_SUBJECT_NOT_SIGNED;
                                    decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                }
                            }
                        }
                    }
                }

                /**
                 * If not detailed, we remove unused parts.
                 */
                if (!detailed)
                {
                    decreeoperationManagementsForRequest.RemoveAll(d => d.MetaPurposeForSubject == Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE ||
                                                                        d.MetaPurposeForSubject == Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE_NOT_SIGNED);
                    if (decreeoperationManagementCreated != null &&
                            decreeoperationManagementCreatedNotSigned != null)
                    {
                        decreeoperationManagementsForRequest.Remove(decreeoperationManagementCreatedNotSigned);
                    }

                    if (decreeoperationManagementCreatedFuture != null &&
                            decreeoperationManagementCreatedFutureNotSigned != null)
                    {
                        decreeoperationManagementsForRequest.Remove(decreeoperationManagementCreatedFutureNotSigned);
                    }

                    if (decreeoperationManagementDeleted != null &&
                            decreeoperationManagementDeletedNotSigned != null)
                    {
                        decreeoperationManagementsForRequest.Remove(decreeoperationManagementDeletedNotSigned);
                    }

                    if (decreeoperationManagementDeletedFuture != null &&
                            decreeoperationManagementDeletedFutureNotSigned != null)
                    {
                        decreeoperationManagementsForRequest.Remove(decreeoperationManagementDeletedFutureNotSigned);
                    }

                    if (decreeoperationManagementRenamed != null &&
                            decreeoperationManagementRenamedNotSigned != null)
                    {
                        decreeoperationManagementsForRequest.Remove(decreeoperationManagementRenamedNotSigned);
                    }

                    if (decreeoperationManagementRenamedFuture != null &&
                            decreeoperationManagementRenamedFutureNotSigned != null)
                    {
                        decreeoperationManagementsForRequest.Remove(decreeoperationManagementRenamedFutureNotSigned);
                    }


                }
                decreeoperationManagements.AddRange(decreeoperationManagementsForRequest);
            } else { 

                /**
                 * POSITIONS.
                 */
                    detailed = false;
                    if (request.Detailed > 0)
                    {
                        detailed = true;
                    }
                //operationsAll = DecreeoperationsLocal().Values.Where(d => d.Subject == request.SubjectID && request.SubjectID > 0).ToList();
                if (request.SubjectID > 0)
                {
                    if (DecreeoperationsSubjectAsKeyLocal().ContainsKey(request.SubjectID))
                    {
                        operationsAll = DecreeoperationsSubjectAsKeyLocal()[request.SubjectID];
                    }

                }


                decreeoperationManagementsForRequest = new List<DecreeoperationManagement>();
                    decreeoperationManagementCreated = null;
                    decreeoperationManagementCreatedNotSigned = null;
                    decreeoperationManagementCreatedFuture = null;
                    decreeoperationManagementCreatedFutureNotSigned = null;
                    decreeoperationManagementDeleted = null;
                    decreeoperationManagementDeletedNotSigned = null;
                    decreeoperationManagementDeletedFuture = null;
                    decreeoperationManagementDeletedFutureNotSigned = null;

                    foreach (Decreeoperation operation in operationsAll)
                    {

                        DecreeoperationManagement decreeoperationManagement = new DecreeoperationManagement();
                        decreeoperationManagement.MetaPurposeForSubject = 0;
                        decreeoperationManagement.Fulfill(operation);
                        Decree decree = DecreesLocal()[operation.Decree];
                        decreeoperationManagement.MetaDateActive = operation.Dateactive.GetValueOrDefault();
                        if (operation.Datecustom == 0)
                        {
                            decreeoperationManagement.MetaDateActive = decree.Dateactive.GetValueOrDefault();
                        }
                        bool signed = false;
                        if (decree.Signed > 0 && request.RequestedDate >= decree.Datesigned.GetValueOrDefault())
                        {
                            signed = true;
                        }
                        if (request.Padding > 0)
                        {
                            request.RequestedDate = request.RequestedDate;
                            signed = true;
                        }
                        decreeoperationManagement.MetaDecreeName = decree.Name;
                        decreeoperationManagement.MetaDecreeNameUnofficial = decree.Nickname;
                        decreeoperationManagement.MetaDecreeNumber = decree.Number;

                        /**
                         * Created part.
                         */
                        if (decreeoperationManagement.Created > 0)
                        {
                            /**
                             * All signed.
                             */
                            if (signed)
                            {
                                /**
                                 * Future
                                 */
                                if (decreeoperationManagement.MetaDateActive > request.RequestedDate)
                                {
                                    /**
                                     * Already have.
                                     */
                                    if (decreeoperationManagementCreatedFuture != null && decreeoperationManagementCreatedFuture.MetaDateActive < decreeoperationManagement.MetaDateActive)
                                    {
                                        decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE;
                                        decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                    }
                                    else
                                    {
                                        if (decreeoperationManagementCreatedFuture != null)
                                        {
                                            decreeoperationManagementCreatedFuture.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE;
                                        }

                                        decreeoperationManagementCreatedFuture = decreeoperationManagement;
                                        decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_CREATE_SUBJECT_IN_FUTURE;
                                        decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                    }

                                }
                                else
                                /**
                                 * Current
                                 */
                                {
                                    /**
                                     * Already have.
                                     */
                                    if (decreeoperationManagementCreated != null && decreeoperationManagementCreated.MetaDateActive > decreeoperationManagement.MetaDateActive)
                                    {
                                        decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE;
                                        decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                    }
                                    else
                                    {
                                        if (decreeoperationManagementCreated != null)
                                        {
                                            decreeoperationManagementCreated.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE;
                                        }


                                        decreeoperationManagementCreated = decreeoperationManagement;
                                        decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_CREATED_SUBJECT;
                                        decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                    }
                                }
                                /**
                                 * Not signed.
                                 */
                            }
                            else
                            {
                                /**
                                 * Future
                                 */
                                if (decreeoperationManagement.MetaDateActive > request.RequestedDate)
                                {
                                    /**
                                     * Already have.
                                     */
                                    if (decreeoperationManagementCreatedFutureNotSigned != null && decreeoperationManagementCreatedFutureNotSigned.MetaDateActive < decreeoperationManagement.MetaDateActive)
                                    {
                                        decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE_NOT_SIGNED;
                                        decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                    }
                                    else
                                    {
                                        if (decreeoperationManagementCreatedFutureNotSigned != null)
                                        {
                                            decreeoperationManagementCreatedFutureNotSigned.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE_NOT_SIGNED;
                                        }

                                        decreeoperationManagementCreatedFutureNotSigned = decreeoperationManagement;
                                        decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_CREATE_SUBJECT_IN_FUTURE_NOT_SIGNED;
                                        decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                    }
                                /**
                                 * Current
                                 */
                                }
                                else
                                {
                                    /**
                                     * Already have.
                                     */
                                    if (decreeoperationManagementCreatedNotSigned != null && decreeoperationManagementCreatedNotSigned.MetaDateActive > decreeoperationManagement.MetaDateActive)
                                    {
                                        decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE_NOT_SIGNED;
                                        decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                    }
                                    else
                                    {
                                        if (decreeoperationManagementCreatedNotSigned != null)
                                        {
                                            decreeoperationManagementCreatedNotSigned.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE_NOT_SIGNED;
                                        }
                                        decreeoperationManagementCreatedNotSigned = decreeoperationManagement;
                                        decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_CREATED_SUBJECT_NOT_SIGNED;
                                        decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                    }
                                }
                            }
                        }
                        /**
                         * Deleted part.
                         */
                        if (decreeoperationManagement.Deleted > 0)
                        {
                            /**
                             * All signed.
                             */
                            if (signed)
                            {
                                /**
                                 * Future
                                 */
                                if (decreeoperationManagement.MetaDateActive > request.RequestedDate)
                                {
                                    /**
                                     * Already have.
                                     */
                                    if (decreeoperationManagementDeletedFuture != null && decreeoperationManagementDeletedFuture.MetaDateActive < decreeoperationManagement.MetaDateActive)
                                    {
                                        decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE;
                                        decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                    }
                                    else
                                    {
                                        if (decreeoperationManagementDeletedFuture != null)
                                        {
                                            decreeoperationManagementDeletedFuture.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE;
                                        }

                                        decreeoperationManagementDeletedFuture = decreeoperationManagement;
                                        decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_DELETE_SUBJECT_IN_FUTURE;
                                        decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                    }
                                    /**
                                     * Current
                                     */
                                }
                                else
                                {
                                    /**
                                     * Already have.
                                     */
                                    if (decreeoperationManagementDeleted != null && decreeoperationManagementDeleted.MetaDateActive > decreeoperationManagement.MetaDateActive)
                                    {
                                        decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE;
                                        decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                    }
                                    else
                                    {
                                        if (decreeoperationManagementDeleted != null)
                                        {
                                            decreeoperationManagementDeleted.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE;
                                        }


                                        decreeoperationManagementDeleted = decreeoperationManagement;
                                        decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_DELETED_SUBJECT;
                                        decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                    }
                                }
                                /**
                                 * Not signed.
                                 */
                            }
                            else
                            {
                                /**
                                 * Future
                                 */
                                if (decreeoperationManagement.MetaDateActive > request.RequestedDate)
                                {
                                    /**
                                     * Already have.
                                     */
                                    if (decreeoperationManagementDeletedFutureNotSigned != null && decreeoperationManagementDeletedFutureNotSigned.MetaDateActive < decreeoperationManagement.MetaDateActive)
                                    {
                                        decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE_NOT_SIGNED;
                                        decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                    }
                                    else
                                    {
                                        if (decreeoperationManagementDeletedFutureNotSigned != null)
                                        {
                                            decreeoperationManagementDeletedFutureNotSigned.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE_NOT_SIGNED;
                                        }

                                        decreeoperationManagementDeletedFutureNotSigned = decreeoperationManagement;
                                        decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_DELETE_SUBJECT_IN_FUTURE_NOT_SIGNED;
                                        decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                    }
                                }
                                else
                                /**
                                 * Current
                                 */
                                {
                                    /**
                                     * Already have.
                                     */
                                    if (decreeoperationManagementDeletedNotSigned != null && decreeoperationManagementDeletedNotSigned.MetaDateActive > decreeoperationManagement.MetaDateActive)
                                    {
                                        decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE_NOT_SIGNED;
                                        decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                    }
                                    else
                                    {
                                        if (decreeoperationManagementDeletedNotSigned != null)
                                        {
                                            decreeoperationManagementDeletedNotSigned.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE_NOT_SIGNED;
                                        }
                                        decreeoperationManagementDeletedNotSigned = decreeoperationManagement;
                                        decreeoperationManagement.MetaPurposeForSubject = Keys.DECREE_OPERATION_META_PURPOSE_DELETED_SUBJECT_NOT_SIGNED;
                                        decreeoperationManagementsForRequest.Add(decreeoperationManagement);
                                    }
                                }
                            }
                        }
                    }

                    /**
                     * If not detailed, we remove unused parts.
                     */
                    if (!detailed)
                    {
                        decreeoperationManagementsForRequest.RemoveAll(d => d.MetaPurposeForSubject == Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE ||
                                                                            d.MetaPurposeForSubject == Keys.DECREE_OPERATION_META_PURPOSE_NO_PURPOSE_NOT_SIGNED);
                        if (decreeoperationManagementCreated != null &&
                                decreeoperationManagementCreatedNotSigned != null)
                        {
                            decreeoperationManagementsForRequest.Remove(decreeoperationManagementCreatedNotSigned);
                        }

                        if (decreeoperationManagementCreatedFuture != null &&
                                decreeoperationManagementCreatedFutureNotSigned != null)
                        {
                            decreeoperationManagementsForRequest.Remove(decreeoperationManagementCreatedFutureNotSigned);
                        }

                        if (decreeoperationManagementDeleted != null &&
                                decreeoperationManagementDeletedNotSigned != null)
                        {
                            decreeoperationManagementsForRequest.Remove(decreeoperationManagementDeletedNotSigned);
                        }

                        if (decreeoperationManagementDeletedFuture != null &&
                                decreeoperationManagementDeletedFutureNotSigned != null)
                        {
                            decreeoperationManagementsForRequest.Remove(decreeoperationManagementDeletedFutureNotSigned);
                        }



                    }
                    decreeoperationManagements.AddRange(decreeoperationManagementsForRequest);
            }
            if (request.Subjectidstructureupdate != 0)
            {
                foreach (DecreeoperationManagement dom in decreeoperationManagements)
                {
                    dom.Subject = request.SubjectID;
                }
            }
            return decreeoperationManagements;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="personID"></param>
        /// <returns></returns>
        public bool isAllowedToReadPerson(User user, int personID)
        {
            if (user.Admin.GetValueOrDefault() > 0 || user.Masterpersonneleditor.GetValueOrDefault() > 0)
            {
                return true;
            }
            if (user.Personnelread == 0)
            {
                return false;
            }
            Person person = PersonsLocal().GetValue(personID);
            if (person.Structure == 0)
            {
                return true;
            }
            return isAllowedToReadStructure(user, person.Structure);
        }

        public bool isAllowedToEditPerson(User user, int personID)
        {
            if (user.Admin.GetValueOrDefault() > 0 || user.Masterpersonneleditor.GetValueOrDefault() > 0)
            {
                return true;
            }
            if (user.Personneleditor == 0)
            {
                return false;
            }
            Person person = PersonsLocal().GetValue(personID);
            if (person.Structure == 0)
            {
                return true;
            }
            return isAllowedToReadStructure(user, person.Structure);
        }

        /// <summary>
        /// Получает список личных дел по совпадении в Фамилии, имени, отчеству. 
        /// 
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="fio"></param>
        /// <param name="excludeRemoved">выносит из поиска удаленные (заархивированные) должности.</param>
        /// <param name="fastSearch">Ускоренный поиск. Не подгружает полную информацию, а только головную</param>
        /// <returns></returns>
        public List<PersonManager> GetPersons(User user, string fio, bool excludeRemoved = true, bool fastSearch = false)
        {
            if (fio == null)
            {
                return new List<PersonManager>();
            }

            
            List<PersonManager> persons = new List<PersonManager>();
            string fioPrepared = fio.ToLower().Trim();
            if (PersonsLocal() == null)
            {
                UpdatePersonsLocal();
            }
            // Меняем на StartsWith, чтобы в первую очередь искало по фамилиям, потом имени, а уже потом отчеству.
            persons.AddRange(PersonManager.PersonsToPersonManagers(this, user, PersonsLocal().Values.Where(p => (p.Surname + " " + p.Name + " " + p.Fathername).ToLower().StartsWith(fioPrepared)), fastSearch));
            //persons.AddRange(PersonManager.PersonsToPersonManagers(this, user, PersonsLocal().Values.Where(p => (p.Surname + " " + p.Name + " " + p.Fathername).ToLower().Contains(fioPrepared)), fastSearch));

            if (excludeRemoved)
            {
                persons = persons.Where(p => p.Removed == 0).ToList();
            }

            return persons;
        }

        /// <summary>
        /// Создаем новое ЭЛД. По умолчанию в конце к ФИО добавляется пол
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int CreatePerson(User user, string fio, bool withGender = true)
        {
            Person person = new Person();
            bool man = true; // Пол мужской.
            if (withGender)
            {
                string genderString = fio.GetLast(7);
                if (genderString.Equals("Мужской"))
                {
                    man = true;
                }
                else
                {
                    man = false;
                }
                fio = fio.Remove(fio.Length - 7); 
            }
            
            string[] fioSplitted = fio.Split(" ");
            string personalnumber = "";
            if (fioSplitted.Length == 1) // значит, ввели сюда личный номер.
            {
                string numpersonal = fioSplitted[0];

                IEnumerable<Certificate> certificatesRaw = Certificates.Where(c => c.NumPersonal.ToLower().Equals(numpersonal));
                List<CertificateManager> certificates = new List<CertificateManager>();
                foreach (Certificate certificateRaw in certificatesRaw)
                {
                    certificates.Add(GetCertificate(certificateRaw));
                }
                certificates.Reverse();
                CertificateManager certificate = certificates.FirstOrDefault();
                if (certificate != null)
                {
                    personalnumber = numpersonal;
                    fioSplitted = certificate.FullName.Split(" ");
                }
            }


            string surname = fioSplitted.First();


            /**
             * Если строка ФИО разделяется хотя бы на два слова (как минимум Фамилия и Имя)
             */
            if (fioSplitted.Length > 1)
            {
                if (man)
                {
                    if (SubjectsLocal() == null)
                    {
                        UpdateSubjectsLocal();
                    }
                    Subject subject = SubjectsLocal().Values.FirstOrDefault(s => s.Category == 3 && s.Name1.ToLower().Equals(fioSplitted[1].ToLower()));
                    if (subject != null)
                    {
                        person.Name = subject.Name1;
                        person.Name2 = subject.Name2;
                        person.Name3 = subject.Name3;
                        person.Name4 = subject.Name4;
                        person.Name5 = subject.Name5;
                        person.Name6 = subject.Name6;
                        person.Namesubject = subject.Id;
                    } else
                    {
                        person.Name = fioSplitted[1];
                        person.Name2 = fioSplitted[1] + "а";
                        person.Name3 = fioSplitted[1] + "у";
                        person.Name4 = fioSplitted[1] + "а";
                        person.Name5 = fioSplitted[1] + "ом";
                        person.Name6 = fioSplitted[1] + "е";
                    }
                    
                } else
                {
                    if (SubjectsLocal() == null)
                    {
                        UpdateSubjectsLocal();
                    }
                    Subject subject = SubjectsLocal().Values.FirstOrDefault(s => s.Category == 5 && s.Name1.ToLower().Equals(fioSplitted[1].ToLower()));
                    if (subject != null)
                    {
                        person.Name = subject.Name1;
                        person.Name2 = subject.Name2;
                        person.Name3 = subject.Name3;
                        person.Name4 = subject.Name4;
                        person.Name5 = subject.Name5;
                        person.Name6 = subject.Name6;
                        person.Namesubject = subject.Id;
                    } else
                    {
                        person.Name = fioSplitted[1];
                        person.Name2 = fioSplitted[1].Remove(fioSplitted[1].Length - 1) + "ы";
                        person.Name3 = fioSplitted[1].Remove(fioSplitted[1].Length - 1) + "е";
                        person.Name4 = fioSplitted[1].Remove(fioSplitted[1].Length - 1) + "у";
                        person.Name5 = fioSplitted[1].Remove(fioSplitted[1].Length - 1) + "ой";
                        person.Name6 = fioSplitted[1].Remove(fioSplitted[1].Length - 1) + "е";
                    }
                    

                }
                
            } else
            {
                person.Name = "Имя"; 
                person.Name2 = "Имени";
                person.Name3 = "Имени";
                person.Name4 = "Имя";
                person.Name5 = "Именем";
                person.Name6 = "Имени";

            }

            /**
             * Если строка ФИО разделяется хотя бы на три слова (Фамилия, Имя и Отчество)
             */
            if (fioSplitted.Length > 2)
            {
                if (man)
                {
                    if (SubjectsLocal() == null)
                    {
                        UpdateSubjectsLocal();
                    }
                    Subject subject = SubjectsLocal().Values.FirstOrDefault(s => s.Category == 4 && s.Name1.ToLower().Equals(fioSplitted[2].ToLower()));
                    if (subject != null)
                    {
                        person.Fathername = subject.Name1;
                        person.Fathername2 = subject.Name2;
                        person.Fathername3 = subject.Name3;
                        person.Fathername4 = subject.Name4;
                        person.Fathername5 = subject.Name5;
                        person.Fathername6 = subject.Name6;
                        person.Fathernamesubject = subject.Id;
                    } else
                    {
                        person.Fathername = fioSplitted[2];
                        person.Fathername2 = fioSplitted[2] + "а";
                        person.Fathername3 = fioSplitted[2] + "у";
                        person.Fathername4 = fioSplitted[2] + "а";
                        person.Fathername5 = fioSplitted[2] + "ем";
                        person.Fathername6 = fioSplitted[2] + "е";
                    }
                    
                } else
                {
                    if (SubjectsLocal() == null)
                    {
                        UpdateSubjectsLocal();
                    }
                    Subject subject = SubjectsLocal().Values.FirstOrDefault(s => s.Category == 6 && s.Name1.ToLower().Equals(fioSplitted[2].ToLower()));
                    if (subject != null)
                    {
                        person.Fathername = subject.Name1;
                        person.Fathername2 = subject.Name2;
                        person.Fathername3 = subject.Name3;
                        person.Fathername4 = subject.Name4;
                        person.Fathername5 = subject.Name5;
                        person.Fathername6 = subject.Name6;
                        person.Fathernamesubject = subject.Id;
                    } else
                    {
                        person.Fathername = fioSplitted[2];
                        person.Fathername2 = fioSplitted[2].Remove(fioSplitted[2].Length - 1) + "ы";
                        person.Fathername3 = fioSplitted[2].Remove(fioSplitted[2].Length - 1) + "е";
                        person.Fathername4 = fioSplitted[2].Remove(fioSplitted[2].Length - 1) + "у";
                        person.Fathername5 = fioSplitted[2].Remove(fioSplitted[2].Length - 1) + "ой";
                        person.Fathername6 = fioSplitted[2].Remove(fioSplitted[2].Length - 1) + "е";
                    }

                    
                }
            } else
            {
                person.Fathername = "Отчество";
                person.Fathername2 = "Отчества";
                person.Fathername3 = "Отчеству";
                person.Fathername4 = "Отчество";
                person.Fathername5 = "Отчеством";
                person.Fathername6 = "Отчестве";
            }
            
            if (surname.Length > 0)
            {
                if (man)
                {
                    person.Surname = surname;
                    person.Surname2 = surname + "а";
                    person.Surname3 = surname + "у";
                    person.Surname4 = surname + "а";
                    person.Surname5 = surname + "ым";
                    person.Surname6 = surname + "е";
                } else
                {
                    person.Surname = surname;
                    person.Surname2 = surname.Remove(surname.Length - 1) + "ы";
                    person.Surname3 = surname.Remove(surname.Length - 1) + "е";
                    person.Surname4 = surname.Remove(surname.Length - 1) + "у";
                    person.Surname5 = surname.Remove(surname.Length - 1) + "ой";
                    person.Surname6 = surname.Remove(surname.Length - 1) + "е";
                }
                
            } else
            {
                person.Surname = "Фамилия";
                person.Surname2 = "Фамилии";
                person.Surname3 = "Фамилии";
                person.Surname4 = "Фамилию";
                person.Surname5 = "Фамилией";
                person.Surname6 = "Фамилии";
            }

            if (man)
            {
                person.Gender = "Мужской";
                person.Nationality = "Белорус";
                person.Gendersubject = 0;
            }
            else
            {
                person.Gender = "Женский";
                person.Nationality = "Белоруска";
                person.Gendersubject = 1;
            }
            if (personalnumber.Length > 0)
            {
                person.Numpersonal = personalnumber;
            }
            person.Livecountry = "Республика Беларусь";
            person.Registercountry = "Республика Беларусь";

            context.Person.Add(person);
            SaveChanges();
            UpdatePersonsLocal();

            return person.Id;

            //return 0;
        }

        public void UpdatePerson(User user, Person updatedPerson)
        {
            if (updatedPerson == null)
            {
                return;
            }
            Person personContext = Persons.FirstOrDefault(p => p.Id == updatedPerson.Id);
            if (personContext == null)
            {
                return;
            }
            personContext.Name = updatedPerson.Name;
            personContext.Surname = updatedPerson.Surname;
            personContext.Fathername = updatedPerson.Fathername;
            personContext.Birthdate = updatedPerson.Birthdate;
            personContext.Photo = updatedPerson.Photo;
            personContext.Gender = updatedPerson.Gender;
            if (personContext.Gender.ToLower().StartsWith("ж"))
            {
                personContext.Gendersubject = 1; // Женский;
            }
            else if (personContext.Gender.ToLower().StartsWith("м"))
            {
                personContext.Gendersubject = 0; // Мужской;
            }
            personContext.Passportid = updatedPerson.Passportid;
            personContext.Passportnum = updatedPerson.Passportnum;
            personContext.Nationality = updatedPerson.Nationality;
            personContext.Passportdatestart = updatedPerson.Passportdatestart;
            personContext.Passportdateend = updatedPerson.Passportdateend;
            personContext.Birthlocation = updatedPerson.Birthlocation;
            personContext.Registercountry = updatedPerson.Registercountry;
            personContext.Registerstate = updatedPerson.Registerstate;
            personContext.Registercitysubstate = updatedPerson.Registercitysubstate;
            personContext.Registersubstate = updatedPerson.Registersubstate;
            personContext.Registercitytype = updatedPerson.Registercitytype;
            personContext.Registercity = updatedPerson.Registercity;
            personContext.Registerstreettype = updatedPerson.Registerstreettype;
            personContext.Registerstreet = updatedPerson.Registerstreet;
            personContext.Registerhouse = updatedPerson.Registerhouse;
            personContext.Registerhousing = updatedPerson.Registerhousing;
            personContext.Registerflat = updatedPerson.Registerflat;
            personContext.Livecountry = updatedPerson.Livecountry;
            personContext.Livestate = updatedPerson.Livestate;
            personContext.Livecitysubstate = updatedPerson.Livecitysubstate;
            personContext.Livesubstate = updatedPerson.Livesubstate;
            personContext.Livecitytype = updatedPerson.Livecitytype;
            personContext.Livecity = updatedPerson.Livecity;
            personContext.Livestreettype = updatedPerson.Livestreettype;
            personContext.Livestreet = updatedPerson.Livestreet;
            personContext.Livehouse = updatedPerson.Livehouse;
            personContext.Livehousing = updatedPerson.Livehousing;
            personContext.Liveflat = updatedPerson.Liveflat;
            personContext.Science = updatedPerson.Science;
            personContext.Numpersonal = updatedPerson.Numpersonal;
            personContext.Wound = updatedPerson.Wound;
            personContext.Sciencerank = updatedPerson.Sciencerank;
            personContext.Surnameother = updatedPerson.Surnameother;
            personContext.Name2 = updatedPerson.Name2;
            personContext.Surname2 = updatedPerson.Surname2;
            personContext.Fathername2 = updatedPerson.Fathername2;
            personContext.Name3 = updatedPerson.Name3;
            personContext.Surname3 = updatedPerson.Surname3;
            personContext.Fathername3 = updatedPerson.Fathername3;
            personContext.Name4 = updatedPerson.Name4;
            personContext.Surname4 = updatedPerson.Surname4;
            personContext.Fathername4 = updatedPerson.Fathername4;
            personContext.Name5 = updatedPerson.Name5;
            personContext.Surname5 = updatedPerson.Surname5;
            personContext.Fathername5 = updatedPerson.Fathername5;
            personContext.Name6 = updatedPerson.Name6;
            personContext.Surname6 = updatedPerson.Surname6;
            personContext.Fathername6 = updatedPerson.Fathername6;
            if (updatedPerson.Namesubject > 0)
            {
                Subject name = SubjectsLocal().GetValue(updatedPerson.Namesubject);
                if (name != null)
                {
                    personContext.Namesubject = updatedPerson.Namesubject;
                    personContext.Name = name.Name1;
                    personContext.Name2 = name.Name2;
                    personContext.Name3 = name.Name3;
                    personContext.Name4 = name.Name4;
                    personContext.Name5 = name.Name5;
                    personContext.Name6 = name.Name6;
                }
            }

            if (updatedPerson.Fathernamesubject > 0)
            {
                Subject fathername = SubjectsLocal().GetValue(updatedPerson.Fathernamesubject);
                if (fathername != null)
                {
                    personContext.Fathernamesubject = updatedPerson.Fathernamesubject;
                    personContext.Fathername = fathername.Name1;
                    personContext.Fathername2 = fathername.Name2;
                    personContext.Fathername3 = fathername.Name3;
                    personContext.Fathername4 = fathername.Name4;
                    personContext.Fathername5 = fathername.Name5;
                    personContext.Fathername6 = fathername.Name6;
                }
            }

            personContext.Livestatenum = updatedPerson.Livestatenum;
            personContext.Livesubstatenum = updatedPerson.Livesubstatenum;
            personContext.Registerstatenum = updatedPerson.Registerstatenum;
            personContext.Registersubstatenum = updatedPerson.Registersubstatenum;
            personContext.Birthcountry = updatedPerson.Birthcountry;
            personContext.Birthstate = updatedPerson.Birthstate;
            personContext.Birthsubstate = updatedPerson.Birthsubstate;
            personContext.Birthcitysubstate = updatedPerson.Birthcitysubstate;
            personContext.Birthcitytype = updatedPerson.Birthcitytype;
            personContext.Birthcity = updatedPerson.Birthcity;
            personContext.Birthadditional = updatedPerson.Birthadditional.Trim();
            SaveChanges();
            UpdatePersonsLocal();
        }

        public void RemovePerson(User user, int personid)
        {
            Person personContext = Persons.FirstOrDefault(p => p.Id == personid);
            if (personContext == null)
            {
                return;
            }
            personContext.Removed = 1; // Помечаем как удаленное.

            SaveChanges();
            UpdatePersonsLocal();
        }

        public void UploadPhoto(User user, Personphoto personphoto)
        {
            //personphoto.Person64header = "";
            context.Add(personphoto);
            SaveChanges();
        }

        public List<Personphoto> GetPersonMedia(User user, int personid)
        {
            return Personphotos.Where(p => p.Person == personid).ToList();
        }

        public Personphoto GetPersonMediaMain(User user, int personid)
        {
            Person person = PersonsLocal().GetValue(personid);
            if (person != null)
            {
                Personphoto personphoto = Personphotos.FirstOrDefault(p => p.Id == person.Photo);
                if (personphoto != null)
                {
                    return personphoto;
                }
            }
            return null;
        }

        public void DeletePhoto(User user, int photoid)
        {
            Personphoto personphoto = Personphotos.FirstOrDefault(p => p.Id == photoid);
            if (personphoto != null)
            {
                context.Personphoto.Remove(personphoto);
                context.SaveChanges();
            }
        }

        public void MainPhoto(User user, int photoid)
        {
            Personphoto personphoto = Personphotos.FirstOrDefault(p => p.Id == photoid);
            
            if (personphoto != null)
            {
                Person personContext = context.Person.FirstOrDefault(p => p.Id == personphoto.Person);
                if (personContext != null)
                {
                    personContext.Photo = personphoto.Id;
                    SaveChanges();
                    UpdatePersonsLocal();
                }
            }
        }

        /// <summary>
        /// Назначить ЭЛД (сотрудника) на конкретную должность
        /// </summary>
        /// <param name="user"></param>
        /// <param name="personid"></param>
        /// <param name="appointid"></param>
        public void AppointPerson(User user, int personid, int appointid)
        {
            Person personContext = Persons.FirstOrDefault(p => p.Id == personid);
            if (personContext == null)
            {
                return;
            }
            bool positionBool = true;
            if (appointid < 0)
            {
                positionBool = false;
            }
            if (positionBool)
            {
                Position position = PositionsLocal().GetValue(appointid);
                if (position != null)
                {
                    personContext.Position = position.Id;
                    personContext.Structure = position.Structure;
                    SaveChanges();
                    UpdatePersonsLocal();
                    return;
                }
            }
        }

        /// <summary>
        /// Снять сотрудника (ЭЛД) с должности.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="personid"></param>
        public void TakeoffPerson(User user, int personid)
        {
            Person personContext = Persons.FirstOrDefault(p => p.Id == personid);
            if (personContext == null)
            {
                return;
            }
            personContext.Position = 0;
            personContext.Structure = 0;
            SaveChanges();
            UpdatePersonsLocal();
            //Position position = PositionsLocal().GetValue(positionid);
            //if (position != null)
            //{
            //    personContext.Position = position.Id;
            //    personContext.Structure = position.Structure;
            //    SaveChanges();
            //    return;
            //}
        }

        public PersonManager GetPersonManager(User user, int personid)
        {
            return GetPersonManager(user, PersonsLocal().GetValue(personid));
        }

        /// <summary>
        /// Получает информацию об Электронном Личном Деле. Если fastSearch отключен, то возвращает со всеми записями (работа, звания и т.п.) единым объектом.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="person"></param>
        /// <param name="fastSearch"></param>
        /// <returns></returns>
        public PersonManager GetPersonManager(User user, Person person, bool fastSearch = false)
        {
            if (person == null)
            {
                return null;
            }
            DateTime date = user.Date.GetValueOrDefault();
            PersonManager personManager = new PersonManager(person);

            Structure actualStructure = null;
            Position position = null;
            // Если сотрудник привязан к подразделению, записать информацию о сокращенном и полном наименовании
            if (person.Structure > 0)
            {
                actualStructure = GetActualStructureInfo(person.Structure, user.Date.GetValueOrDefault());
                personManager.Structurename = GetStructureNameDocument(actualStructure, date, 1, null);
                personManager.Structurename1 = personManager.Structurename;
                // Для оптимизации в быстром поиске не будем вычислять все падежи
                if (!fastSearch)
                {
                    personManager.Structurename2 = GetStructureNameDocument(actualStructure, date, 2, null);
                    personManager.Structurename3 = GetStructureNameDocument(actualStructure, date, 3, null);
                    personManager.Structurename4 = GetStructureNameDocument(actualStructure, date, 4, null);
                    personManager.Structurename5 = GetStructureNameDocument(actualStructure, date, 5, null);
                    personManager.Structurename6 = GetStructureNameDocument(actualStructure, date, 6, null);
                }
                

                personManager.Structuretree = FormTreeDocument(actualStructure, date, null, 1, null);
                personManager.Structuretree1 = personManager.Structuretree; // Не пересчитываем заново
                // Для оптимизации в быстром поиске не будем вычислять все падежи
                if (!fastSearch)
                {
                    personManager.Structuretree2 = FormTreeDocument(actualStructure, date, null, 2, null);
                    personManager.Structuretree3 = FormTreeDocument(actualStructure, date, null, 3, null);
                    personManager.Structuretree4 = FormTreeDocument(actualStructure, date, null, 4, null);
                    personManager.Structuretree5 = FormTreeDocument(actualStructure, date, null, 5, null);
                    personManager.Structuretree6 = FormTreeDocument(actualStructure, date, null, 6, null);
                }
                
            } else
            {
                personManager.Structurename = "";
                personManager.Structurename1 = "";
                personManager.Structurename2 = "";
                personManager.Structurename3 = "";
                personManager.Structurename4 = "";
                personManager.Structurename5 = "";
                personManager.Structurename6 = "";
                personManager.Structuretree = "";
                personManager.Structuretree1 = "";
                personManager.Structuretree2 = "";
                personManager.Structuretree3 = "";
                personManager.Structuretree4 = "";
                personManager.Structuretree5 = "";
                personManager.Structuretree6 = "";
            }

            // Если сотрудник привязан к должности, записать информацию о сокращенном наименовании должности
            if (person.Position > 0)
            {
                position = PositionsLocal().GetValue(person.Position);
                if (position != null)
                {
                    if (PositiontypesLocal() == null)
                    {
                        UpdatePositiontypesLocal();
                    }
                    //personManager.Positiontypestring = PositiontypesLocal().GetValue(position.Positiontype).Name;
                    //personManager.Positiontype2string = PositiontypesLocal().GetValue(position.Positiontype).Name2;
                    //personManager.Positiontype3string = PositiontypesLocal().GetValue(position.Positiontype).Name3;

                    personManager.Positiontypestring = FormTreeDocument(null, date, position, 1, null);
                    personManager.Positiontype1string = personManager.Positiontypestring;
                    // Для оптимизации в быстром поиске не будем вычислять все падежи
                    if (!fastSearch)
                    {
                        personManager.Positiontype2string = FormTreeDocument(null, date, position, 2, null);
                        personManager.Positiontype3string = FormTreeDocument(null, date, position, 3, null);
                        personManager.Positiontype4string = FormTreeDocument(null, date, position, 4, null);
                        personManager.Positiontype5string = FormTreeDocument(null, date, position, 5, null);
                        personManager.Positiontype6string = FormTreeDocument(null, date, position, 6, null);
                    }
                    
                } else
                {
                    personManager.Positiontypestring = "";
                    personManager.Positiontype2string = "";
                    personManager.Positiontype3string = "";
                    personManager.Positiontype4string = "";
                    personManager.Positiontype5string = "";
                    personManager.Positiontype6string = "";
                }
            } else
            {
                personManager.Positiontypestring = "";
                personManager.Positiontype2string = "";
                personManager.Positiontype3string = "";
                personManager.Positiontype4string = "";
                personManager.Positiontype5string = "";
                personManager.Positiontype6string = "";
            }

            // Если сотрудник привязан и к должности и к подразделению
            if (person.Structure > 0 && person.Position > 0)
            {
                if (actualStructure != null && position != null)
                {
                    personManager.Positiontree = FormTreeDocument(actualStructure, date, position, 1, null);
                    personManager.Positiontree1 = personManager.Positiontree;
                    // Для оптимизации в быстром поиске не будем вычислять все падежи
                    if (!fastSearch)
                    {
                        personManager.Positiontree2 = FormTreeDocument(actualStructure, date, position, 2, null);
                        personManager.Positiontree3 = FormTreeDocument(actualStructure, date, position, 3, null);
                        personManager.Positiontree4 = FormTreeDocument(actualStructure, date, position, 4, null);
                        personManager.Positiontree5 = FormTreeDocument(actualStructure, date, position, 5, null);
                        personManager.Positiontree6 = FormTreeDocument(actualStructure, date, position, 6, null);
                    }
                    
                } else
                {
                    personManager.Positiontree = "";
                    personManager.Positiontree1 = "";
                    personManager.Positiontree2 = "";
                    personManager.Positiontree3 = "";
                    personManager.Positiontree4 = "";
                    personManager.Positiontree5 = "";
                    personManager.Positiontree6 = "";
                }
            }

            // При быстром поиске мы загружаем только основные данные, отбрасывая подробности, что повышает быстродействие.
            if (fastSearch)
            {
                return personManager;
            }

            IEnumerable<Personrank> ranks = PersonranksLocal().Values.Where(p => p.Person == personManager.Id);
            personManager.Personranks = ranks.ToArray();
            //IEnumerable<Personrank> ranks = Personranks.Where(p => p.Person == personManager.Id);
            //personManager.Personranks = ranks.ToArray();

            Personrank actualPersonrank = null;
            DateTime? lastdecreePersonrank = null;

            // Узнаем актуальное звание на текущий момент
            foreach (Personrank personrank in ranks)
            {
                DateTime rankDate = personrank.Decreedate;
                if (personrank.Datestart != null)
                {
                    rankDate = personrank.Datestart.GetValueOrDefault();
                }
                if (lastdecreePersonrank == null || lastdecreePersonrank < rankDate) 
                {
                    lastdecreePersonrank = personrank.Decreedate;
                    actualPersonrank = personrank;
                }
            }
            if (actualPersonrank != null)
            {
                Rank actualRank = RanksLocal().GetValue(actualPersonrank.Rank);
                if (actualRank != null)
                {
                    personManager.ActualRank = actualRank;
                }
            }

            IEnumerable<Personcontract> contracts = PersoncontractsLocal().Values.Where(p => p.Person == personManager.Id);
            personManager.Personcontracts = contracts.ToArray();
            foreach (Personcontract personcontract in personManager.Personcontracts)
            {
                
                //IEnumerable<Personadditionalagreement> personadditionalagreements = Personadditionalagreements.ToList();
                IEnumerable<Personadditionalagreement> personadditionalagreements = Personadditionalagreements.Where(p => p.Contract == personcontract.Id);
                personcontract.Personadditionalagreements = personadditionalagreements.ToList();
            }
            //IEnumerable<Personcontract> contracts = Personcontracts.Where(p => p.Person == personManager.Id);
            //personManager.Personcontracts = contracts.ToArray();

            IEnumerable<Personrelative> relatives = PersonrelativesLocal().Values.Where(p => p.Person == personManager.Id);
            personManager.Personrelatives = relatives.ToArray();
            //IEnumerable<Personrelative> relatives = Personrelatives.Where(p => p.Person == personManager.Id);
            //personManager.Personrelatives = relatives.ToArray();

            IEnumerable<Personattestation> attestations = Personattestations.Where(p => p.Person == personManager.Id);
            personManager.Personattestations = attestations.ToArray();

            IEnumerable<Personvacation> vacations = PersonvacationsLocal().Values.Where(p => p.Person == personManager.Id);
            personManager.Personvacations = vacations.ToArray();
            //IEnumerable<Personvacation> vacations = Personvacations.Where(p => p.Person == personManager.Id);
            //personManager.Personvacations = vacations.ToArray();

            IEnumerable<Personlanguage> languages = Personlanguages.Where(p => p.Person == personManager.Id);
            personManager.Personlanguages = languages.ToArray();

            IEnumerable<Personill> ills = Personills.Where(p => p.Person == personManager.Id);
            personManager.Personills = ills.ToArray();

            IEnumerable<Personworktrip> worktrips = Personworktrips.Where(p => p.Person == personManager.Id);
            personManager.Personworktrips = worktrips.ToArray();

            //IEnumerable<Personjobprivelege> jobpriveleges = Personjobpriveleges.Where(p => p.Person == personManager.Id);
            //personManager.Personjobpriveleges = jobpriveleges.ToArray();
            List<Personjobprivelege> jobpriveleges = new List<Personjobprivelege>();

            IEnumerable <Personjob> jobs = PersonjobsLocal().Values.Where(p => p.Person == personManager.Id);
            //IEnumerable<Personjob> jobs = Personjobs.Where(p => p.Person == personManager.Id);
            personManager.Personjobs = jobs.ToArray();
            foreach (var job in personManager.Personjobs)
            {
                List<Personjobprivelege> personjobpriveleges = Personjobpriveleges.Where(e => e.Personjob == job.Id).ToList();
                job.Personjobpriveleges = personjobpriveleges;
                foreach (var personjobprivelege in job.Personjobpriveleges)
                {
                    List<Personjobprivelegeperiod> personjobprivelegeperiods = Personjobprivelegeperiods.Where(e => e.Personjobprivelege == personjobprivelege.Id).ToList();
                    personjobprivelege.Personjobprivelegeperiods = personjobprivelegeperiods;
                }

                List<Personjobprivelege> newPersonjobpriveleges = job.GeneratePersonjobsByPeriodsPrivelege(date, personManager.Personvacations, personManager.Personills, personManager.Personworktrips);
                jobpriveleges.AddRange(newPersonjobpriveleges);
            }
            personManager.Personjobpriveleges = jobpriveleges.ToArray();

            IEnumerable<Personpenalty> penalties = Personpenalties.Where(p => p.Person == personManager.Id);
            personManager.Personpenalties = penalties.ToArray();
            

            IEnumerable<Personelection> elections = Personelections.Where(p => p.Person == personManager.Id);
            personManager.Personelections = elections.ToArray();

            IEnumerable<Personscience> sciences = Personsciences.Where(p => p.Person == personManager.Id);
            personManager.Personsciences = sciences.ToArray();

            IEnumerable<Personreward> rewards = PersonrewardsLocal().Values.Where(p => p.Person == personManager.Id);
            //IEnumerable<Personreward> rewards = Personrewards.Where(p => p.Person == personManager.Id);
            List<Personreward> rewardsfiltered = new List<Personreward>();
            foreach (Personreward personreward in rewards)
            {
                Persondecreeoperation persondecreeoperation = PersondecreeoperationsLocal().Values.FirstOrDefault(p => p.Subjecttype == 1 && p.Subjectid == personreward.Id);
                //Persondecreeoperation persondecreeoperation = Persondecreeoperations.FirstOrDefault(p => p.Subjecttype == 1 && p.Subjectid == personreward.Id);
                bool signed = false;
                if (persondecreeoperation != null)
                {
                    Persondecree persondecree = PersondecreesLocal().GetValue(persondecreeoperation.Persondecree);
                    //Persondecree persondecree = Persondecrees.FirstOrDefault(p => p.Id == persondecreeoperation.Persondecree);
                    if (persondecree != null && persondecree.Signed > 0)
                    {
                        signed = true;
                    }
                }

                if (persondecreeoperation == null || signed)
                {
                    rewardsfiltered.Add(personreward);
                }
            }
            personManager.Personrewards = rewardsfiltered.ToArray();

            

            IEnumerable<Personeducation> educations = Personeducations.Where(p => p.Person == personManager.Id);
            personManager.Personeducations = educations.ToArray();
            foreach (var education in personManager.Personeducations)
            {
                List<Educationtypeblock> educationtypeblocks = Educationtypeblocks.Where(e => e.Personeducation == education.Id).ToList();
                education.Educationtypeblocks = educationtypeblocks;
                foreach(var educationtypeblock in education.Educationtypeblocks)
                {
                    List<Educationperiod> educationperiods = Educationperiods.Where(e => e.Educationtypeblock == educationtypeblock.Id).ToList();
                    educationtypeblock.Educationperiods = educationperiods;
                }

                List<Academicvacation> academicvacations = Academicvacations.Where(e => e.Personeducation == education.Id).ToList();
                education.Academicvacations = academicvacations;

                List<Educationmaternity> educationmaternities = Educationmaternities.Where(e => e.Personeducation == education.Id).ToList();
                education.Educationmaternities = educationmaternities;

                education.GeneratePersoneducationparts();
            }

            // Подсчет выслуги лет
            personManager.Pensions = Pension.GetPensions(date, this, personManager, personManager.Personjobs, personManager.Personeducations,
                personManager.Personvacations, personManager.Personills, personManager.Personworktrips).ToArray();

            int pensioncivilyears = 0;
            int pensioncivilmonths = 0;
            int pensioncivildays = 0;
            int pensionmilitaryyears = 0;
            int pensionmilitarymonths = 0;
            int pensionmilitarydays = 0;
            // Высчитываем суммарную зачтенную выслугу как для военной, так и для смешанной
            foreach (Pension pension in personManager.Pensions)
            {
                if (pension.Job)
                {
                    if (pension.JobMilitary)
                    {
                        pensioncivilyears += Period.YearDiff(pension.Start, pension.End);
                        pensioncivilmonths += Period.MonthDiff(pension.Start, pension.End);
                        pensioncivildays += Period.DayDiff(pension.Start, pension.End);
                    }
                }
                if (pension.Education)
                {
                    if (pension.EducationMilitary)
                    {

                    }
                }
                
            }
            //mark_version
            CustomDate JOBS = new CustomDate(0, 0, 0);
            int appending_days = 1;
            if (personManager.Pensions == null  || personManager.Pensions.Length == 0)
            {
                personManager.pension_A = "0 0 0 ";
                personManager.pension_B = "0 0 0 ";
            }
            else
            {
                double y_militery = 0, m_militery = 0, d_militery = 0;
                double y_job = 0, m_job = 0, d_job = 0;
                double y_education_mil = 0, m_education_mil = 0, d_education_mil = 0;
                double y_education = 0, m_education = 0, d_education = 0;
                List<Pension> ptr = personManager.Pensions.ToList();
                ptr.Sort((a, b) => b.Start.Ticks.CompareTo(a.Start.Ticks));
                personManager.Pensions = ptr.ToArray();
                DateTime prrevios_end_date = ptr.First().End.AddDays(1);
                int plused = 0;
                List<double> time_v = new List<double>() { 0.0, 0.0, 0.0 };

                foreach (Pension pension in personManager.Pensions)
                {
                    if (pension.Start > pension.End)
                        continue;
                    int total = prrevios_end_date.Subtract(pension.End).Days;
                    if (total == 0)
                        plused = -1;
                    CustomDate time_date = CustomDate.difference(pension.End.AddDays(1), pension.Start);
                    time_date.rebase();
                    pension.Daysbeforecoef = plused != 0 ? time_date.Day + plused : time_date.Day - 1;
                    pension.Monthsbeforecoef = time_date.Month;
                    pension.Yearsbeforecoef = time_date.Year;
                    time_v[0] = pension.Coef * time_date.Year;
                    time_v[1] = pension.Coef * time_date.Month;
                    time_v[2] = pension.Coef * (time_date.Day + plused);
                    CustomDate after_date = new CustomDate(time_v[0], time_v[1], pension.Coef * pension.Daysbeforecoef);
                    after_date.rebase();
                    pension.Daysaftercoef = after_date.Day;
                    pension.Monthsaftercoef = after_date.Month;
                    pension.Yearsaftercoef = after_date.Year;

                    if (pension.Job)
                    {
                        if (pension.JobMilitary)
                        {
                            y_militery += time_v[0];
                            m_militery += time_v[1];
                            d_militery += time_v[2];
                        }
                        else
                        {
                            y_job += time_v[0];
                            m_job += time_v[1];
                            d_job += time_v[2];
                        }
                    }
                    if (pension.Education)
                    {
                        if (pension.EducationMilitary)
                        {
                            y_education_mil += time_v[0];
                            m_education_mil += time_v[1];
                            d_education_mil += time_v[2];
                        }
                        if (pension.EducationConsider && !pension.EducationMilitary)
                        {
                            y_education += time_v[0];
                            m_education += time_v[1];
                            d_education += time_v[2];
                        }
                    }
                    appending_days += (Math.Abs(total) > 1) && (pension.JobMilitary) ? 1 : 0;
                    plused = 0;
                    prrevios_end_date = pension.Start;
                }

                //A category
                CustomDate militeryy = new CustomDate(Year: y_militery, Month: m_militery, Day: d_militery),
                    jobb = new CustomDate(Year: y_job, Month: m_job, Day: d_job),
                    education_m = new CustomDate(Year: y_education_mil, Month: m_education_mil, Day: d_education_mil),
                    educationn = new CustomDate(Year: y_education, Month: m_education, Day: d_education);
                militeryy.rebase();
                jobb.rebase();
                educationn.rebase();
                education_m.rebase();
                CustomDate A = CustomDate.add(militeryy, education_m),
                    B = CustomDate.add(CustomDate.add(militeryy, jobb),
                    (educationn.Year >= 5 && educationn.Month >= 0 && educationn.Day >= 0) ? CustomDate.add(education_m, new CustomDate(Year: 5, Month: 0, Day: 0)) : CustomDate.add(educationn, education_m));
                JOBS = CustomDate.add(CustomDate.add(militeryy, jobb), education_m);
                JOBS.rebase();
                A.rebase();
                if (A.Year > 20)
                    A = (educationn.Year >= 4 && educationn.Month >= 0 && educationn.Day >= 0) ? CustomDate.add(A, new CustomDate(Year: 4, Month: 0, Day: 0)) : CustomDate.add(A, educationn);
                A.rebase();
                B.rebase();
                personManager.pension_A = A.Year.ToString() + " " + A.Month.ToString() + " " + A.Day.ToString() + " ";
                personManager.pension_B = B.Year.ToString() + " " + B.Month.ToString() + " " + B.Day.ToString() + " ";

                ptr = personManager.Pensions.ToList();
                ptr.Sort((a, b) => a.Start.Ticks.CompareTo(b.Start.Ticks));
                personManager.Pensions = ptr.ToArray();
                personManager.appending_days = appending_days;
                A.add(new CustomDate(Day: -appending_days));
                personManager.pension_A_with = A.Year.ToString() + " " + A.Month.ToString() + " " + A.Day.ToString() + " ";
                B.add(new CustomDate(Day: -appending_days));
                personManager.pension_B_with = B.Year.ToString() + " " + B.Month.ToString() + " " + B.Day.ToString() + " ";
            }

            if (personManager.Numpersonal != null && personManager.Numpersonal.Length > 0 && !fastSearch)
            {
                IEnumerable<Certificate> certificatesRaw = CertificatesLocal().Values.Where(c => c.NumPersonal.ToLower().Equals(personManager.Numpersonal.ToLower()));
                //IEnumerable<Certificate> certificatesRaw = Certificates.Where(c => c.NumPersonal.ToLower().Equals(personManager.Numpersonal.ToLower()));
                List<CertificateManager> certificates = new List<CertificateManager>();
                foreach (Certificate certificateRaw in certificatesRaw)
                {
                    certificates.Add(GetCertificate(certificateRaw));
                }
                certificates.Reverse();
                personManager.Certificates = certificates.ToArray();
            }
            
            IEnumerable<Personphysical> personphysicals = Personphysicals.Where(p => p.Person == personManager.Id);
            List<PersonphysicalManager> physicals = new List<PersonphysicalManager>();
            foreach (Personphysical personphysical in personphysicals)
            {
                PersonphysicalManager personphysicalManager = new PersonphysicalManager(personphysical);
                IEnumerable<Physicalfield> physicalfields = Physicalfields.Where(p => p.Personphysical == personphysicalManager.Id);
                personphysicalManager.Physicalfields = physicalfields.ToArray();

                physicals.Add(personphysicalManager);
            }
            personManager.Personphysicals = physicals.ToArray();
            //personManager.Personeducations = educations.ToArray();

            IEnumerable<Persondriver> drivers = Persondrivers.Where(p => p.Person == personManager.Id);
            personManager.Persondrivers = drivers.ToArray();

            IEnumerable<Personpermission> permissions = Personpermissions.Where(p => p.Person == personManager.Id);
            personManager.Personpermissions = permissions.ToArray();

            IEnumerable<Personprivelege> priveleges = Personpriveleges.Where(p => p.Person == personManager.Id);
            personManager.Personpriveleges = priveleges.ToArray();

            IEnumerable<Persondispanserization> dispanserizations = Persondispanserizations.Where(p => p.Person == personManager.Id);
            personManager.Persondispanserizations = dispanserizations.ToArray();

            IEnumerable<Personvvk> vvks = Personvvks.Where(p => p.Person == personManager.Id);
            personManager.Personvvks = vvks.ToArray();

            

            IEnumerable<Persontransfer> transfers = Persontransfers.Where(p => p.Person == personManager.Id);
            personManager.Persontransfers = transfers.ToArray();

            /**
             * После загрузки всей информации в объект из таблиц баз данных, ниже добавляются вычислительные операции на основе данных. Например, подсчет стажа.
            */
            if (!fastSearch) { 

                /**
                 * Узнаем, является ли в данный момент человек военным.
                 */
                if (personManager.Personranks.Length > 0)
                {
                    personManager.Military = true; // Если есть хоть одно звание, то военный.
                }

                personManager.Military = personManager.IsMilitary(user);
                

                // Подсчет выслуги лет. Заодно вылавливается текущая работа.
                DateTime currentDate = user.Date.GetValueOrDefault();
                int previousyear = currentDate.Year - 1; // Предыдущий год от текущего. 
            

                foreach (Personjob job in personManager.Personjobs)
                {


                    if (job.Start != null && job.Actual == 1)
                    {
                        personManager.Currentjob = job; // Находится и записывается актуальная должность/позиция/работа.
                    }
                }

                if (!personManager.Military && personManager.Currentjob != null)
                {
                    personManager.Jobstart = personManager.Currentjob.Start;
                } else if (personManager.Military) // Высчитываем начало службы для подсчета отпуска для военных
                {
                    DateTime earliestRank = currentDate; 
                    foreach (Personrank rank in personManager.Personranks)
                    {
                        DateTime rankDate = rank.Decreedate;
                        if (rank.Datestart != null)
                        {
                            rankDate = rank.Datestart.GetValueOrDefault();
                        }
                        if (rankDate < earliestRank)
                        {

                            earliestRank = rankDate; // Находим первейшее звание. Именно по нему и будем определять откуда считать начало службы.
                        }
                    }
                    personManager.Jobstart = earliestRank;
                } else
                {
                    personManager.Jobstart = currentDate; // Если нет никаких данных, то считаем началой службы/работы сегодняшнее число.
                }

                // В Jobperiods мы за каждый период (обычно продолжительностью год) записываем данные, какой стаж работы накопился, сколько дней отпуска, сколько дней отпуска перенесено и так далее.
                List<Jobperiod> jobperiods = GetJobperiods(user, personManager);
                personManager.Jobperiods = jobperiods.ToArray(); // записываем все периоды службы.
                if (jobperiods.Count > 0)
                {
                    personManager.Jobperiodcurrent = personManager.Jobperiods[jobperiods.Count - 1];
                }
                if (jobperiods.Count > 1)
                {
                    personManager.Jobperiodprevious = personManager.Jobperiods[jobperiods.Count - 2];
                }

                personManager.Experience = 0; // Суммарно дней службы
                if (jobperiods.Count > 0)
                {
                    personManager.Experience = personManager.Jobperiodcurrent.Experience;
                    personManager.ExperienceDays = personManager.Jobperiodcurrent.ExperienceDays;
                    personManager.ExperienceMonths = personManager.Jobperiodcurrent.ExperienceMonths;
                    personManager.ExperienceYears = personManager.Jobperiodcurrent.ExperienceYears;
                    personManager.ExperienceDays = JOBS.Day;
                    personManager.ExperienceMonths = JOBS.Month;
                    personManager.ExperienceYears = JOBS.Year;

                    // Записываем тот же опыт, только по государственной службе.
                    personManager.Stateservicedays = personManager.Jobperiodcurrent.Stateservicedays;
                    personManager.Stateservicemonths = personManager.Jobperiodcurrent.Stateservicemonths;
                    personManager.Stateserviceyears = personManager.Jobperiodcurrent.Stateserviceyears;
                }

                int daysprivelege = 0; // Дни льготной службы
                double year = 0, month = 0, days = 0;
                foreach (Personjobprivelege personjobprivelege in personManager.Personjobpriveleges)
                {
                        if (personjobprivelege.Start == null || personjobprivelege.End == null)
                        {
                            continue; // Пропускаем, если у нас нет начала и/или конца.
                        }

                    CustomDate time_date = CustomDate.difference(personjobprivelege.End.GetValueOrDefault().AddDays(1), personjobprivelege.Start.GetValueOrDefault());
                    year += personjobprivelege.Coef * time_date.Year;
                    month += personjobprivelege.Coef * time_date.Month;
                    days += personjobprivelege.Coef * time_date.Day;
                    time_date.rebase();
                    personjobprivelege.Daysbeforecoef = time_date.Day;
                    personjobprivelege.Monthsbeforecoef = time_date.Month;
                    personjobprivelege.Yearsbeforecoef = time_date.Year;
                    CustomDate after_date = new CustomDate(personjobprivelege.Coef * time_date.Year, personjobprivelege.Coef * time_date.Month, personjobprivelege.Coef * time_date.Day);
                    after_date.rebase();
                    personjobprivelege.Daysaftercoef = after_date.Day;
                    personjobprivelege.Monthsaftercoef = after_date.Month;
                    personjobprivelege.Yearsaftercoef = after_date.Year;

                    daysprivelege += (int)(((personjobprivelege.End.GetValueOrDefault() - personjobprivelege.Start.GetValueOrDefault()).Days) * personjobprivelege.Coef);
                }
                CustomDate exemption = new CustomDate(Year: year, Month: month, Day: days);
                exemption.rebase();
                personManager.Experienceprivelege = daysprivelege;
                personManager.Experienceprivelege = exemption.Day + exemption.Month * 30 + exemption.Year * 30 * 12;

                // Производим подсчет, сколько дней человек занимает на последнем звании
                int actualRankExperience = 0;
                if (actualPersonrank != null && lastdecreePersonrank != null)
                {
                    actualRankExperience = (currentDate - lastdecreePersonrank.GetValueOrDefault()).Days;
                    
                }
                personManager.ActualRankExperience = actualRankExperience;

                if (personManager.Jobperiodcurrent != null)
                {
                    personManager.Vacationdayscurrentyear = personManager.Jobperiodcurrent.Vacationdaysgivenclear;
                    personManager.Vacationshiftdate = personManager.Jobperiodcurrent.Vacationshiftdate;
                    personManager.Vacationshiftbefore = personManager.Jobperiodcurrent.Vacationshiftbefore;
                    personManager.Vacationshiftafter = personManager.Jobperiodcurrent.Vacationshiftafter;
                }

                /**
                 * Здесь от последнего начала непрерывной службы мы будем вести  подсчет, сколько дней отпуска было истрачено, а сколько переносилось
                 */
                DateTime currentWorkYearStart = new DateTime(currentDate.Year, 1, 1);
                DateTime currentWorkYearEnd = new DateTime(currentDate.Year, 12, 31);
                // Для военных начало года является календарным или началом службы. А конец является концом года
                if (personManager.Military) 
                {
                    if (personManager.Jobstart != null && personManager.Jobstart.GetValueOrDefault() > currentWorkYearStart)
                    {
                        currentWorkYearStart = personManager.Jobstart.GetValueOrDefault();
                    }
                // Рассматриваем дату началы и конца службы для гражданских
                } else
                {
                    if (personManager.Jobstart != null)
                    {
                        DateTime jobstartMonthAndDay = new DateTime(currentDate.Year, personManager.Jobstart.GetValueOrDefault().Month, personManager.Jobstart.GetValueOrDefault().Day);
                        if (currentDate < jobstartMonthAndDay) // Означает, что начало рабочего года приходится на предыдущий, а конец рабочего года приходится на этот же год
                        {
                            currentWorkYearStart = new DateTime(currentDate.Year - 1, personManager.Jobstart.GetValueOrDefault().Month, personManager.Jobstart.GetValueOrDefault().Day);
                            currentWorkYearEnd = new DateTime(currentDate.Year, personManager.Jobstart.GetValueOrDefault().Month, personManager.Jobstart.GetValueOrDefault().Day);
                            currentWorkYearEnd = currentWorkYearEnd.AddDays(-1);
                        }
                        else // Означает, что начало рабочего года приходится на этот, а конец рабочего года приходится на следующий год
                        {
                            currentWorkYearStart = new DateTime(currentDate.Year, personManager.Jobstart.GetValueOrDefault().Month, personManager.Jobstart.GetValueOrDefault().Day);
                            currentWorkYearEnd = new DateTime(currentDate.Year + 1, personManager.Jobstart.GetValueOrDefault().Month, personManager.Jobstart.GetValueOrDefault().Day);
                            currentWorkYearEnd = currentWorkYearEnd.AddDays(-1);
                        }
                    }
                }

                personManager.Vacationdaysused = 0;
                //foreach (Personvacation vacation in personManager.Personvacations)
                //{
                //    // 
                //    if (currentWorkYearStart <= vacation.Date && currentWorkYearEnd >= vacation.Date) // Входит в текущий рабочий период
                //    {
                //        personManager.Vacationdaysused += vacation.Duration;
                //    }
                //}

                if (personManager.Jobperiodcurrent != null)
                {
                    personManager.Vacationdaysused = personManager.Jobperiodcurrent.Vacationdaysconsumed;
                }


                if (personManager.Jobperiodcurrent != null)
                {
                    personManager.Vacationdaysleft = personManager.Jobperiodcurrent.Vacationdaysgiven - personManager.Jobperiodcurrent.Vacationdaysgivenclear;
                }

                /**
                 * На основании всех больничных и отпусков считаем сколько человек упустил льготных дней
                 */
                int privelegesmissed = 0;
            
                foreach (Personvacation vacation in personManager.Personvacations)
                {
                    if (vacation.DisplayPrivelege)
                    {
                        privelegesmissed += vacation.Duration;
                        privelegesmissed += vacation.Trip;
                    }
                    
                }

                foreach (Personill ill in personManager.Personills)
                {
                    if (ill.Privelege == 0 && ill.DisplayPrivelege)
                    {
                        int duration = (ill.Dateend - ill.Datestart).Days + 1;
                        privelegesmissed += duration;
                    }
                    
                }

                foreach (Personworktrip personworktrip in personManager.Personworktrips)
                {
                    if (personworktrip.Privelege == 0 && personworktrip.DisplayPrivelege)
                    {
                        privelegesmissed += personworktrip.Days;
                    }

                }
                personManager.Privelegesmissed = privelegesmissed;
                
                // Является ли человек капитаном, который при увольнении может стать майором
                if (personManager.ActualRank != null && personManager.ActualRank.Id == 11 && personManager.Experience > 7300 
                    && actualRankExperience >= 1095 && position != null && position.Cap.GetValueOrDefault() == 11) // Есть 20 лет выслуги и 3 года на последней должности и является капитаном
                {
                    personManager.Major = 1;
                } else
                {
                    personManager.Major = 0;
                }

                // Если курсант, то мы создаем фэйковую трудовую деятельность, 
                
                foreach (Personeducation personeducation in personManager.Personeducations)
                {
                    // Старый метод
                    //if (personeducation.Cadet > 0)
                    //{
                    //    Personjob cadetPersonjob = new Personjob();
                    //    cadetPersonjob.Id = -1; // Помечаем таким образом, что запись ненастоящая и что ее нельзя редактировать.
                    //    cadetPersonjob.Jobplace = "курсант";
                    //    cadetPersonjob.Jobposition = "курсант";
                    //    cadetPersonjob.Start = personeducation.Start;
                    //    cadetPersonjob.End = personeducation.End;
                    //    cadetPersonjob.Servicetype = 2;
                    //    cadetPersonjob.Jobtype = 2; // служба
                    //    //cadetPersonjob.Serviceplace = "курсант " + personeducation.Name; // пока что так, но надо более адекватно
                    //    cadetPersonjob.Serviceplace = "курсант " + personeducation.Orderwho; // пока что так, но надо более адекватно
                    //    cadetPersonjob.Ordernumber = personeducation.Ordernumber;
                    //    cadetPersonjob.Ordernumbertype = personeducation.Ordernumbertype;
                    //    cadetPersonjob.Orderdate = personeducation.Orderdate;
                    //    cadetPersonjob.Orderwho = personeducation.Orderwho;
                    //    cadetPersonjob.Orderwhoid = personeducation.Orderwhoid;
                    //    cadetPersonjob.Orderid = personeducation.Orderid;

                    //    List<Personjob> newPersonjobs = new List<Personjob>(personManager.Personjobs);
                    //    newPersonjobs.Add(cadetPersonjob);
                    //    personManager.Personjobs = newPersonjobs.ToArray();
                    //}

                    // Игнорируем незавершенное образование
                    if (personeducation.Interrupted > 0)
                    {
                        continue;
                    }

                    // Идем по периодам в учебе и смотрим, какие из них являются военными.
                    //foreach (Personeducationpart personeducationpart in personeducation.Personeducationparts)
                    //{
                    //    // Засчитывается только очное
                    //    if (personeducationpart.Educationperiod != null && personeducationpart.Educationtypeblock != null && personeducationpart.Educationtypeblock.Educationtype == 1)
                    //    {
                    //        // Если не из УГЗ вкладки, то смотрим, что стоит галочка, что являлось службой.
                    //        // Если из УГЗ вкладки, то смотрим, чтобы было прописано звание (больше 2ух символов не-пустых).
                    //        if (personeducationpart.Educationperiod.Service > 0 || personeducationpart.Educationperiod.Rank.Trim().Length > 2)
                    //        {
                    //            Personjob cadetPersonjob = new Personjob();
                    //            cadetPersonjob.Id = -1; // Помечаем таким образом, что запись ненастоящая и что ее нельзя редактировать.
                    //            cadetPersonjob.Jobplace = personeducation.Name2;
                    //            Educationpositiontype educationpositiontype = Educationpositiontypes.FirstOrDefault(e => e.Id == personeducationpart.Educationperiod.Educationpositiontype);
                    //            if (educationpositiontype != null)
                    //            {
                    //                cadetPersonjob.Jobposition = educationpositiontype.Name;
                    //            } else
                    //            {
                    //                cadetPersonjob.Jobposition = "";
                    //            }
                                
                    //            cadetPersonjob.Jobpositionplace = cadetPersonjob.Jobposition + " " + cadetPersonjob.Jobplace;
                    //            cadetPersonjob.Start = personeducationpart.Educationperiod.Start;
                    //            cadetPersonjob.End = personeducationpart.Educationperiod.End;
                    //            cadetPersonjob.Servicetype = 2;
                    //            cadetPersonjob.Jobtype = 2; // служба
                    //                                        //cadetPersonjob.Serviceplace = "курсант " + personeducation.Name; // пока что так, но надо более адекватно
                    //            //cadetPersonjob.Serviceplace = "курсант " + personeducation.Orderwho; // пока что так, но надо более адекватно
                    //            cadetPersonjob.Ordernumber = personeducation.Ordernumber;
                    //            cadetPersonjob.Ordernumbertype = personeducation.Ordernumbertype;
                    //            cadetPersonjob.Orderdate = personeducation.Orderdate;
                    //            cadetPersonjob.Orderwho = personeducation.Orderwho;
                    //            cadetPersonjob.Orderwhoid = personeducation.Orderwhoid;
                    //            cadetPersonjob.Orderid = personeducation.Orderid;

                    //            List<Personjob> newPersonjobs = new List<Personjob>(personManager.Personjobs);
                    //            newPersonjobs.Add(cadetPersonjob);
                    //            personManager.Personjobs = newPersonjobs.ToArray();
                    //        }
                    //    }

                    //    // Обучение до академ отпуска или декрета было очным
                    //    if ((personeducationpart.Educationmaternity != null || personeducationpart.Academicvacation != null) && personeducationpart.PreviousEducationpartWithEducationperiod != null
                    //        && personeducationpart.PreviousEducationpartWithEducationperiod.Educationtypeblock != null
                    //        && personeducationpart.PreviousEducationpartWithEducationperiod.Educationtypeblock.Educationtype == 1)
                    //    {
                    //        Personvacation educationPersonvacation = new Personvacation();
                    //        educationPersonvacation.Id = -1;
                    //        educationPersonvacation.Date = personeducationpart.Start;
                    //        educationPersonvacation.Duration = (personeducationpart.End.GetValueOrDefault() - personeducationpart.Start.GetValueOrDefault()).Days + 1;
                    //        educationPersonvacation.Allowstart = personeducationpart.Start.GetValueOrDefault();
                    //        educationPersonvacation.Allowend = personeducationpart.End.GetValueOrDefault();
                    //        if (personeducationpart.Educationmaternity != null)
                    //        {
                    //            educationPersonvacation.Vacationtype = 6;
                    //        }
                    //        if (personeducationpart.Academicvacation != null)
                    //        {
                    //            educationPersonvacation.Vacationtype = 4;
                    //        }
                    //        //foreach (Jobperiod jobperiod in personManager.Jobperiods)
                    //        //{
                    //        //    Period period = new Period(jobperiod.Start.GetValueOrDefault(), jobperiod.End.GetValueOrDefault()); 
                    //        //    if (period.Surround())
                    //        //}

                    //        List<Personvacation> newPersonvacations = new List<Personvacation>(personManager.Personvacations);
                    //        newPersonvacations.Add(educationPersonvacation);
                    //        personManager.Personvacations = newPersonvacations.ToArray();
                    //    }
                    //}
                }
            } // Заканчиваем подсчет стажа
            

            //personManager.Personattestations = personManager.Personattestations.Reverse().ToArray();
            personManager.Personattestations = personManager.Personattestations.Reverse().OrderBy(p => p.Date).Reverse().ToArray();
            //personManager.Personcontracts = personManager.Personcontracts.Reverse().ToArray();
            personManager.Personcontracts = personManager.Personcontracts.Reverse().OrderBy(p => p.Datestart).Reverse().ToArray();
            //personManager.Persondispanserizations = personManager.Persondispanserizations.Reverse().ToArray();
            personManager.Persondispanserizations = personManager.Persondispanserizations.Reverse().OrderBy(p => p.Date).Reverse().ToArray();
            personManager.Persondrivers = personManager.Persondrivers.Reverse().ToArray();
            //personManager.Personeducations = personManager.Personeducations.Reverse().ToArray();
            personManager.Personeducations = personManager.Personeducations.Reverse().OrderBy(p => p.Datestart).Reverse().OrderBy(p => p.Educationlevel).Reverse().ToArray();
            personManager.Personelections = personManager.Personelections.Reverse().ToArray();
            //personManager.Personills = personManager.Personills.Reverse().ToArray();
            personManager.Personills = personManager.Personills.Reverse().OrderBy(p => p.Datestart).Reverse().ToArray();
            //personManager.Personjobpriveleges = personManager.Personjobpriveleges.Reverse().ToArray();
            personManager.Personjobpriveleges = personManager.Personjobpriveleges.OrderBy(p => p.Start.GetValueOrDefault()).Reverse().ToArray();
            //personManager.Personjobs = personManager.Personjobs.Reverse().ToArray();
            personManager.Personjobs = personManager.Personjobs.Reverse().OrderBy(p => p.Start).Reverse().ToArray();
            personManager.Personlanguages = personManager.Personlanguages.Reverse().ToArray();
            //personManager.Personpenalties = personManager.Personpenalties.Reverse().ToArray();
            personManager.Personpenalties = personManager.Personpenalties.Reverse().OrderBy(p => p.Orderdate).Reverse().ToArray();
            personManager.Personpermissions = personManager.Personpermissions.Reverse().ToArray();
            //personManager.Personphysicals = personManager.Personphysicals.Reverse().ToArray();
            personManager.Personphysicals = personManager.Personphysicals.Reverse().OrderBy(p => p.Physicaldate).Reverse().ToArray();
            personManager.Personpriveleges = personManager.Personpriveleges.Reverse().ToArray();
            //personManager.Personranks = personManager.Personranks.Reverse().ToArray();
            personManager.Personranks = personManager.Personranks.Reverse().OrderBy(p => p.Decreedate).Reverse().ToArray();
            //personManager.Personranks = personManager.Personranks.Reverse().OrderBy(p => p.Datestart).Reverse().ToArray();
            //personManager.Personrelatives = personManager.Personrelatives.Reverse().ToArray();
            personManager.Personrelatives = personManager.Personrelatives.Reverse().OrderBy(p => p.Relativetype).ToArray();
            //personManager.Personrewards = personManager.Personrewards.Reverse().ToArray();
            personManager.Personrewards = personManager.Personrewards.Reverse().OrderBy(p => p.Rewarddate).Reverse().ToArray();
            personManager.Personsciences = personManager.Personsciences.Reverse().ToArray();
            //personManager.Personvacations = personManager.Personvacations.Reverse().ToArray();
            personManager.Personvacations = personManager.Personvacations.Reverse().OrderBy(p => p.Date).Reverse().ToArray();
            //personManager.Personvvks = personManager.Personvvks.Reverse().ToArray();
            personManager.Personvvks = personManager.Personvvks.Reverse().OrderBy(p => p.Date).Reverse().ToArray();
            //personManager.Personworktrips = personManager.Personworktrips.Reverse().ToArray();
            personManager.Personworktrips = personManager.Personworktrips.Reverse().OrderBy(p => p.Tripdate).Reverse().ToArray();
            personManager.Persontransfers = personManager.Persontransfers.Reverse().ToArray();
            return personManager;
        }

        /// <summary>
        /// Делит службу на рабочие периоды. 
        /// К началу запуска функции должна иметься дата начала службы/работы, иначе будет считаться, что служба/работа начинается с сегодняшнего дня.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="person"></param>
        /// <returns></returns>
        public List<Jobperiod> GetJobperiods(User user, PersonManager person)
        {
            var k = new DataAnalysis(this, person);
            k.Worker();

            if (person.Jobstart == null)
            {
                person.Jobstart = user.Date.GetValueOrDefault(); // Если неизвестно когда началась служба, то считаем, что с сегодняшнего дня.
            }
            List<Jobperiod> jobperiods = new List<Jobperiod>();
            DateTime current = user.Date.GetValueOrDefault();
            DateTime jobstart = person.Jobstart.GetValueOrDefault();
            DateTime periodpoint = jobstart; // будем сдвигать текущую точку, чтобы вычислять периоды.
            Jobperiod jobperiodPrevious = null;



            while (periodpoint <= current)
            {
                
                int year = periodpoint.Year; // год периода.
                int day = periodpoint.Day; // день периода.
                int month = periodpoint.Month; // месяц периода.
                if (jobperiodPrevious == null) // Предыдущего периода нет, то есть это начало.
                {

                } else
                {
                    if (person.Military)
                    {
                        day = 1; // Если это не первый год, то у военных начало периода с начала года
                        month = 1;
                    }
                }
                DateTime start = new DateTime(year, month, day); // Дата начала периода.

                DateTime end = start.AddYears(1).AddDays(-1); // Дата окончания периода. 
                // Если военный и это начало службы, то поправляем, что окончание будет 31.12
                if (jobperiodPrevious == null && person.Military) 
                {
                    day = 31; 
                    month = 12;
                    end = new DateTime(start.Year, month, day);
                }
                
                bool vacationcount = false;  // Учитывать ли данный период при подсчете переноса отпусков
                                             // Не записанные периоды в разделе отпусков мы учитывать не будем
                bool actual = start.Date <= current.Date && end.Date >= current.Date; // актуальный ли период. Период актуальный, если текущее число попадает в рамки этого периода
                if (actual)
                {
                    vacationcount = true;
                }

                foreach (Personvacation personvacation in person.Personvacations)
                {
                    // Если период работы совпадает с периодом отпуска (то есть в отпусках есть пометка о данном периоде работы
                    // Мы будем учитывать перенос отпуска за этот год
                    // Также мы всегда учитываем текущий год как активный
                    if ((personvacation.Allowstart.Date <= start.Date && end.Date <= personvacation.Allowend.Date) || actual)
                    {
                        vacationcount = true;
                        break;
                    }
                }

                if (!person.Military)
                {
                    foreach (Personvacation vacation in person.Personvacations)
                    {
                        if (start <= vacation.Date && end >= vacation.Date && vacation.Duration > 30) // Здесь мы будем смотреть за всеми отпусками для гражданских которые более 30 дней
                        {
                            Vacationtype vacationtype = Vacationtypes.FirstOrDefault(v => v.Id == vacation.Vacationtype);
                            if (vacationtype != null && vacationtype.Transferworkyear == 1)
                            {
                                //start = start.AddDays(vacation.Duration);
                                end = end.AddDays(vacation.Duration); // Если отпуск по личным причинам был взят более чем на 30 дней, то рабочий период сдвигается на его продолжительность.
                            }
                        }
                        
                    }
                }

                //int vacationdaystransferprev = 0; // Сколько дней было перенесено с прошлого года.
                int vacationdaysgiven = 0;
                int vacationdaysgivenclear = 0;
                int vacationdaysconsumed = 0; // сколько в этом периоде вообще потратили дней отпуска
                int vacationdaysconsumedprev = 0; // сколько в этом периоде потратили перенесенных дней отпуска
                int vacationdaystransfer = 0; // сколько в этом периоде отправляем дней отпуска на следующий
                int vacationdaystransferleft = 0; // Сколько дней из перенесенных осталось. Обычно сгорают в 0, если не было использовано.
                DateTime? vacationdaysincreasedate = null; // Если в этом году с определенной даты идет больше дней отпуска, то с какой
                                                           // Если нет, то null
                DateTime? vacationshiftdate = null; // То же самое, что и vacationdaysincreasedate, только мы в подсчетах не задействуем
                                                    // а сохраняем для списка jobperiods
                
                int vacationshiftbefore = 0;
                int vacationshiftafter = 0;

                // Все то же самое что и выше, только для гражданских служащих
                DateTime? vacationdaysincreasedateStatecivil = null; // Если в этом году с определенной даты идет больше дней отпуска, то с какой
                                                           // Если нет, то null
                DateTime? vacationshiftdateStatecivil = null; // То же самое, что и vacationdaysincreasedate, только мы в подсчетах не задействуем
                                                    // а сохраняем для списка jobperiods

                int vacationshiftbeforeStatecivil = 0;
                int vacationshiftafterStatecivil = 0;

                int transferprevious = 0; // Сколько дней перенесено с прошлого периода. 
                if (jobperiodPrevious != null)
                {
                    transferprevious = jobperiodPrevious.Vacationdaystransfer;
                }

                int experience = 0; // выслуга лет на текущий период
                int experienceYears = 0; // выслуга в годах на текущий период
                int experienceMonths = 0; // выслуга в месяцах за вычетом лет на текущий период
                int experienceDays = 0; // выслуга в днях за вычетом лет и месяцев на текущий период
                List<DateDiff> experienceDateDiffs = new List<DateDiff>();

                int experienceStatecivil = 0; // выслуга лет на текущий период как гражданского служащего
                int experienceYearsStatecivil = 0; // выслуга в годах на текущий период как гражданского служащего
                int experienceMonthsStatecivil = 0; // выслуга в месяцах за вычетом лет на текущий период как гражданского служащего
                int experienceDaysStatecivil = 0; // выслуга в днях за вычетом лет и месяцев на текущий период как гражданского служащего
                List<DateDiff> experienceDateDiffsStatecivil = new List<DateDiff>();

                //DateDiff dateDiff1 = new DateDiff(new DateTime(2005, 2, 3), new DateTime(2020, 3, 5));
                //int elapsed = dateDiff1.ElapsedYears;

                // Подсчет выслуги лет. Заодно вылавливается текущая работа.
                DateTime currentDate = end; // За окончание берем окончание периода.
                if (currentDate > user.Date.GetValueOrDefault())
                {
                    currentDate = user.Date.GetValueOrDefault(); // Если доходит до актуальной даты, то отпуск для военных высчитывается по выслуге лет на эту дату.
                }
                int previousyear = currentDate.Year - 1; // Предыдущий год от текущего. 

                /**
                 *  Считаем выслугу лет для военных и гос. служащих
                 */
                // Для тех, у кого есть звания (аттестованные)
                if (person.Military)
                {
                    // Здесь смотрим, учился ли 
                    foreach (Personeducation education in person.Personeducations)
                    {
                        //if (education.Cadet == 1) // Курсантам обучение идет в стаж
                        //{
                        //    if (education.Start != null && education.End != null)
                        //    {
                        //        experience += (education.End.GetValueOrDefault() - education.Start.GetValueOrDefault()).Days;
                        //        DateDiff dateDiff = new DateDiff(education.Start.GetValueOrDefault(), education.End.GetValueOrDefault());
                        //        experienceDateDiffs.Add(dateDiff);
                        //        experienceYears = DateDiffsToYears(experienceDateDiffs);
                        //        experienceMonths = DateDiffsToMonths(experienceDateDiffs);
                        //        experienceDays = DateDiffsToDays(experienceDateDiffs);
                        //    }
                        //    else if (education.Start != null)
                        //    {
                        //        experience += (user.Date.GetValueOrDefault() - education.Start.GetValueOrDefault()).Days;
                        //        DateDiff dateDiff = new DateDiff(education.Start.GetValueOrDefault(), user.Date.GetValueOrDefault());
                        //        experienceDateDiffs.Add(dateDiff);
                        //        experienceYears = DateDiffsToYears(experienceDateDiffs);
                        //        experienceMonths = DateDiffsToMonths(experienceDateDiffs);
                        //        experienceDays = DateDiffsToDays(experienceDateDiffs);
                        //    }
                        //    break; // Раз нашли образование, дальше не смотрим.
                        //}

                        // Игнорируем незавершенное образование
                        if (education.Interrupted > 0)
                        {
                            continue;
                        }

                        // Идем по периодам в учебе и смотрим, какие из них являются военными.
                        foreach (Personeducationpart personeducationpart in education.Personeducationparts)
                        {
                            // Засчитывается только очное
                            if (personeducationpart.Educationperiod != null && personeducationpart.Educationtypeblock != null && personeducationpart.Educationtypeblock.Educationtype == 1)
                            {
                                // Если не из УГЗ вкладки, то смотрим, что стоит галочка, что являлось службой.
                                // Если из УГЗ вкладки, то смотрим, чтобы было прописано звание (больше 2ух символов не-пустых).
                                if (personeducationpart.Educationperiod.Service > 0 || personeducationpart.Educationperiod.Rank.Trim().Length > 2)
                                {
                                    if (personeducationpart.Start != null && personeducationpart.End != null)
                                    {
                                        experience += (personeducationpart.End.GetValueOrDefault() - personeducationpart.Start.GetValueOrDefault()).Days;
                                        DateDiff dateDiff = new DateDiff(personeducationpart.Start.GetValueOrDefault(), personeducationpart.End.GetValueOrDefault());
                                        experienceDateDiffs.Add(dateDiff);
                                        experienceYears = DateDiffsToYears(experienceDateDiffs);
                                        experienceMonths = DateDiffsToMonths(experienceDateDiffs);
                                        experienceDays = DateDiffsToDays(experienceDateDiffs);
                                    }
                                    else if (personeducationpart.Start != null)
                                    {
                                        experience += (user.Date.GetValueOrDefault() - personeducationpart.Start.GetValueOrDefault()).Days;
                                        DateDiff dateDiff = new DateDiff(personeducationpart.Start.GetValueOrDefault(), user.Date.GetValueOrDefault());
                                        experienceDateDiffs.Add(dateDiff);
                                        experienceYears = DateDiffsToYears(experienceDateDiffs);
                                        experienceMonths = DateDiffsToMonths(experienceDateDiffs);
                                        experienceDays = DateDiffsToDays(experienceDateDiffs);
                                    }
                                }
                            }
                        }
                    }

                    // Считаем выслугу лет 
                    foreach (Personjob job in person.Personjobs.OrderBy(j => j.Start.GetValueOrDefault()))
                    {
                        if (job.Jobtype == 2) // Jobtype 2 - служба
                        {
                            DateTime? startDate = job.Start;
                            DateTime? endDate = job.End;
                            if (startDate > currentDate)
                            {
                                continue; // Пропускаем, если работа позже начинается.
                            }

                            //int experienceYearsBeforeAddition = experienceYears;
                            //int experienceMonthsBeforeAddition = experienceMonths;
                            //int experienceDaysBeforeAddition = experienceDays;
                            //int experienceBeforeAddition = experience; // Записываем перед прибавкой стажа за конкретный период службы
                            //                                           // Это нужно для того, чтобы выявлять моменты, когда длительность отпуска увеличивается
                            //                                           // Чтобы таким образом находить день увеличения длительности отпуска
                            //                                           // И чтобы считать, если отпуск за текущий год брался до даты, то "надбавка" в этом году терялась
                            
                            DateTime countDate = endDate.GetValueOrDefault();


                            // Экспериментально. С изменением подсчета выслуги в днях на выслугу в дни, года и месяцы
                            DateDiff dateDiffPeriod = new DateDiff(startDate.GetValueOrDefault(), end);
                            if (experienceYearsStatecivil < 10 && dateDiffPeriod.Years <= 10)
                            {
                                vacationdaysincreasedate = startDate.GetValueOrDefault();
                                int yearsDiff = 9 - experienceYears;
                                int monthsDiff = 11 - experienceMonths;
                                int daysDiff = 30 - experienceDays;
                                vacationshiftbefore = 30;
                                vacationshiftafter = 35;
                                vacationdaysincreasedate = vacationdaysincreasedate.GetValueOrDefault().AddYears(yearsDiff).AddMonths(monthsDiff).AddDays(daysDiff);
                            } else if (experienceYears < 15 && dateDiffPeriod.Years <= 15)
                            {
                                vacationdaysincreasedate = startDate.GetValueOrDefault();
                                int yearsDiff = 14 - experienceYears;
                                int monthsDiff = 11 - experienceMonths;
                                int daysDiff = 30 - experienceDays;
                                vacationshiftbefore = 35;
                                vacationshiftafter = 40;
                                vacationdaysincreasedate = vacationdaysincreasedate.GetValueOrDefault().AddYears(yearsDiff).AddMonths(monthsDiff).AddDays(daysDiff);
                            } else if (experienceYears < 20 && dateDiffPeriod.Years <= 20)
                            {
                                vacationdaysincreasedate = startDate.GetValueOrDefault();
                                int yearsDiff = 19 - experienceYears;
                                int monthsDiff = 11 - experienceMonths;
                                int daysDiff = 30 - experienceDays;
                                vacationshiftbefore = 40;
                                vacationshiftafter = 45;
                                vacationdaysincreasedate = vacationdaysincreasedate.GetValueOrDefault().AddYears(yearsDiff).AddMonths(monthsDiff).AddDays(daysDiff);
                            }

                            if (job.Start != null && job.Actual == 1)
                            {
                                experience += (currentDate - startDate.GetValueOrDefault()).Days;
                                DateDiff dateDiff = new DateDiff(startDate.GetValueOrDefault(), currentDate);
                                experienceDateDiffs.Add(dateDiff);
                                experienceYears = DateDiffsToYears(experienceDateDiffs);
                                experienceMonths = DateDiffsToMonths(experienceDateDiffs);
                                experienceDays = DateDiffsToDays(experienceDateDiffs);
                            } else
                            if (startDate != null && endDate != null)
                            {
                                experience += (endDate.GetValueOrDefault() - startDate.GetValueOrDefault()).Days;
                                DateDiff dateDiff = new DateDiff(startDate.GetValueOrDefault(), endDate.GetValueOrDefault());
                                experienceDateDiffs.Add(dateDiff);
                                experienceYears = DateDiffsToYears(experienceDateDiffs);
                                experienceMonths = DateDiffsToMonths(experienceDateDiffs);
                                experienceDays = DateDiffsToDays(experienceDateDiffs);
                            } 
                            

                            // Если дата перехода с одной длительности отпуска на другую приходится не на этот год, а на другой
                            // то обнуляем
                            if (vacationdaysincreasedate.GetValueOrDefault().Year != currentDate.Year)
                            {
                                vacationdaysincreasedate = null;
                            }
                            vacationshiftdate = vacationdaysincreasedate;
                        } 

                    }
                    
                }

                /**
                 * Считаем выслугу лет для подсчета госслужбы
                 */
                foreach (Personjob job in person.Personjobs.OrderBy(j => j.Start.GetValueOrDefault()))
                {
                    if (job.Statecivil == 1) // Значит, что относится к государственной службе
                    {
                        DateTime? startDate = job.Start;
                        if (job.Statecivilstart != null)
                        {
                            startDate = job.Statecivilstart;
                        }
                        DateTime? endDate = job.End;
                        if (job.Statecivilend != null)
                        {
                            endDate = job.Statecivilend;
                        }
                        if (startDate > currentDate)
                        {
                            continue; // Пропускаем, если работа позже начинается.
                        }

                        DateTime countDate = endDate.GetValueOrDefault();


                        // Экспериментально. С изменением подсчета выслуги в днях на выслугу в дни, года и месяцы
                        DateDiff dateDiffPeriod = new DateDiff(startDate.GetValueOrDefault(), end);
                        if (experienceYearsStatecivil < 5 && dateDiffPeriod.Years <= 5)
                        {
                            vacationdaysincreasedateStatecivil = startDate.GetValueOrDefault();
                            int yearsDiff = 4 - experienceYearsStatecivil;
                            int monthsDiff = 11 - experienceMonthsStatecivil;
                            int daysDiff = 30 - experienceDaysStatecivil;
                            vacationshiftbeforeStatecivil = 28;
                            vacationshiftafterStatecivil = 30;
                            vacationdaysincreasedateStatecivil = vacationdaysincreasedateStatecivil.GetValueOrDefault().AddYears(yearsDiff).AddMonths(monthsDiff).AddDays(daysDiff);
                        }
                        else if (experienceYearsStatecivil < 10 && dateDiffPeriod.Years <= 10)
                        {
                            vacationdaysincreasedateStatecivil = startDate.GetValueOrDefault();
                            int yearsDiff = 9 - experienceYearsStatecivil;
                            int monthsDiff = 11 - experienceMonthsStatecivil;
                            int daysDiff = 30 - experienceDaysStatecivil;
                            vacationshiftbeforeStatecivil = 30;
                            vacationshiftafterStatecivil = 32;
                            vacationdaysincreasedateStatecivil = vacationdaysincreasedateStatecivil.GetValueOrDefault().AddYears(yearsDiff).AddMonths(monthsDiff).AddDays(daysDiff);
                        }

                        if (startDate != null && (job.Actual == 1 && job.Statecivilend == null))
                        {
                            experienceStatecivil += (currentDate - startDate.GetValueOrDefault()).Days;
                            DateDiff dateDiff = new DateDiff(startDate.GetValueOrDefault(), currentDate);
                            experienceDateDiffsStatecivil.Add(dateDiff);
                            experienceYearsStatecivil = DateDiffsToYears(experienceDateDiffsStatecivil);
                            experienceMonthsStatecivil = DateDiffsToMonths(experienceDateDiffsStatecivil);
                            experienceDaysStatecivil = DateDiffsToDays(experienceDateDiffsStatecivil);
                        }
                        else
                        if (startDate != null && endDate != null)
                        {
                            experienceStatecivil += (endDate.GetValueOrDefault() - startDate.GetValueOrDefault()).Days;
                            DateDiff dateDiff = new DateDiff(startDate.GetValueOrDefault(), endDate.GetValueOrDefault());
                            experienceDateDiffsStatecivil.Add(dateDiff);
                            experienceYearsStatecivil = DateDiffsToYears(experienceDateDiffsStatecivil);
                            experienceMonthsStatecivil = DateDiffsToMonths(experienceDateDiffsStatecivil);
                            experienceDaysStatecivil = DateDiffsToDays(experienceDateDiffsStatecivil);
                        }


                        // Если дата перехода с одной длительности отпуска на другую приходится не на этот год, а на другой
                        // то обнуляем
                        if (vacationdaysincreasedateStatecivil.GetValueOrDefault().Year != currentDate.Year)
                        {
                            vacationdaysincreasedateStatecivil = null;
                        }
                        vacationshiftdateStatecivil = vacationdaysincreasedateStatecivil;
                    }
                }


                /**
                 * Подсчитываем сколько было положено отпуска
                 */
                if (person.Military)
                {
                    vacationdaysgiven = 30;
                    if (experienceYears >= 10) // Есть 10 лет
                    //if (experience >= 3650) // Есть 10 лет
                    {
                        vacationdaysgiven = 35;
                    }
                    if (experienceYears >= 15) // Есть 15 лет
                    //if (experience >= 5475) // Есть 15 лет
                    {
                        vacationdaysgiven = 40;
                    }
                    if (experienceYears >= 20) // Есть 20 лет
                    //if (experience >= 7300) // Есть 20 лет
                    {
                        vacationdaysgiven = 45;
                    }
                    // Служба по льготным условиям
                    bool wasPrivelege = false;
                    if (person.Personjobpriveleges.FirstOrDefault(pm => pm.Start.GetValueOrDefault() != null && pm.Start.GetValueOrDefault().Year == previousyear) != null)
                    {
                        wasPrivelege = true;
                    }

                    if (wasPrivelege)
                    {
                        vacationdaysgiven = 45; // На льготных основаниях всегда будет предоставлено 45 дней отпуска в следующем году.
                    }

                    if (!wasPrivelege && jobperiodPrevious != null)
                    {
                        // Если в этом году отпуск вышел больше чем в предыдущем из-за стажа, мы должны включить режим, в котором будем смотреть, 
                        // что если отпуск в текущем периоде начался ДО точной даты увеличения отпуска, то добавившиеся дни теряются
                        // То есть человек, чтобы воспользоваться появившимися днями, должен уйти в отпуск после того как эти дни точно появились
                        if (vacationdaysgiven > jobperiodPrevious.Vacationdaysgivenclear)
                        {

                        }
                    }
                }
                // Определяем размер отпуска для гражданских
                else
                {
                    // Старый код, где величина отпуска прописывалась в трудовой деятельности
                    //vacationdaysgiven = 28;
                    //if (person.Currentjob != null && person.Currentjob.Vacationdays > 0)
                    //{
                    //    vacationdaysgiven = person.Currentjob.Vacationdays;
                    //}

                    Personcontract actualPersoncontract = null;
                    vacationdaysgiven = 28;
                    foreach (Personcontract personcontract in person.Personcontracts)
                    {
                        if (personcontract.Datestart.Date <= start && personcontract.Dateend.Date >= end)
                        {
                            actualPersoncontract = personcontract;
                            break;
                        }
                    }
                    // Если какой-нибудь контракт затрагивает текущий рабочий период.
                    if (actualPersoncontract != null)
                    {
                        // Обычный служащий
                        if (actualPersoncontract.Vacationdays > 0)
                        {
                            vacationdaysgiven = actualPersoncontract.Vacationdays;
                            DateTime lastChange = actualPersoncontract.Datestart.Date;
                            foreach (Personadditionalagreement personadditionalagreement in actualPersoncontract.Personadditionalagreements)
                            {
                                if (personadditionalagreement.Datestart != null && personadditionalagreement.Datestart.GetValueOrDefault().Date >= lastChange
                                    && personadditionalagreement.Datestart.GetValueOrDefault().Date <= currentDate)
                                {
                                    vacationdaysgiven = personadditionalagreement.Duration;
                                    lastChange = personadditionalagreement.Datestart.GetValueOrDefault();
                                }
                            }
                            
                        // Считаем государственным служащим, так как отпуск не прописан
                        } else
                        {
                            //int stateserviceyears = actualPersoncontract.Stateserviceyears;
                            //int stateservicemonths = actualPersoncontract.Stateservicemonths;
                            //int stateservicedays = actualPersoncontract.Stateservicedays;
                            int stateserviceyears = experienceYearsStatecivil;
                            int stateservicemonths = experienceMonthsStatecivil;
                            int stateservicedays = experienceDaysStatecivil;

                            //DateDiff elapsedStateTime = new DateDiff(actualPersoncontract.Datestart, current);
                            //stateserviceyears += elapsedStateTime.ElapsedYears;
                            //stateservicemonths += elapsedStateTime.ElapsedMonths;
                            //stateservicedays += elapsedStateTime.ElapsedDays;

                            if (stateservicedays > 29)
                            {
                                stateservicedays -= 29;
                                stateservicemonths += 1;
                            }
                            if (stateservicemonths > 11)
                            {
                                stateservicemonths -= 11 ;
                                stateserviceyears += 1;
                            }
                            if (stateserviceyears >= 10)
                            {
                                vacationdaysgiven = 32;
                            } else if (stateserviceyears >= 5)
                            {
                                vacationdaysgiven = 30;
                            } else
                            {
                                vacationdaysgiven = 28;
                            }
                        }
                    }
                    
                }

                vacationdaysgivenclear = vacationdaysgiven;
                // Подсчет переноса с прошлых периодов. Если прошлый период не внесен в отпуска, то мы не учитываем перенос.
                if (jobperiodPrevious != null && jobperiodPrevious.Vacationcount)
                {
                    //int previousleft = jobperiodPrevious.Vacationdaysgiven - jobperiodPrevious.Vacationdaysconsumed;
                    int previousleft = jobperiodPrevious.Vacationdaystransfer;
                    if (previousleft > 0)
                    {
                        int maxprev = jobperiodPrevious.Vacationdaysgivenclear - 14; // человек должен минимум отгулять 14 дней отпуска
                        if (maxprev < 0)
                        {
                            maxprev = 0;
                        }
                        if (previousleft > maxprev)
                        {
                            previousleft = maxprev;
                        }
                        vacationdaysgiven += previousleft; // остаток переносится
                    }
                }


                foreach (Personvacation vacation in person.Personvacations)
                {

                    // Смотрим, попадает ли конкретный отпуск по дате в наш период
                    if (start <= vacation.Date && end >= vacation.Date ) // Отпуск попадает в период.
                    {
                        bool previousperiod = false; // Если стоит метка true, то отпуск за предыдущий период, значит был перенесен на этот период
                        // Окончания периода переносимого отпуска должна быть меньше начала текущего периода 
                        if (vacation.Allowend.Date <= start.Date)
                        {
                            previousperiod = true;
                        }

                        Vacationtype vacationtype = Vacationtypes.FirstOrDefault(v => v.Id == vacation.Vacationtype);
                        bool social = false;
                        bool cadet = false;
                        if (vacationtype != null)
                        {
                            if (vacationtype.Social == 1)
                            {
                                social = true;
                            }

                            if (vacationtype.Cadet == 1)
                            {
                                cadet = true;
                            }
                        }
                        if (!social && !cadet)
                        {
                            vacationdaysconsumed += vacation.Duration;
                            // Отдельно помечаем сколько дней отпуска потратили за предыдущий период, потому что все что осталось с прошлого - не переносится
                            if (previousperiod)
                            {
                                vacationdaysconsumedprev += vacation.Duration;
                                // Если отпуск относится к прошлому периоду, то с прошлого вычитаем, сколько перенесенных дней было использовано.
                                if (vacation.Allowstart.Date >= jobperiodPrevious.Start.GetValueOrDefault().Date 
                                    && vacation.Allowend.Date <= jobperiodPrevious.End.GetValueOrDefault().Date)
                                {
                                    jobperiodPrevious.Vacationdaystransferleft -= vacation.Duration;
                                }
                                
                            }

                            // Если на этот год приходится период увеличения отпуска (то есть у нас есть точная дата удлинения отпуска на 5 дней)
                            // Мы делаем проверку, есть ли хоть один период отпуска в этом году до даты увеличения
                            // Если есть - то мы срезаем 5 дней (увеличение длительности) для всего отпуска в этом году
                            if (vacationdaysincreasedate != null && vacation.Date < vacationdaysincreasedate.GetValueOrDefault()
                                && vacation.Allowstart.Date <= vacationdaysincreasedate.GetValueOrDefault() && vacation.Allowend.Date >= vacationdaysincreasedate.GetValueOrDefault()
                                && vacationshiftbefore > 0)
                            {
                                // Как правило, разница будет или 0 или 5. 0 - если текущая дата не дошла до даты увеличения отпуска
                                // 5 - если текущая дата перешла дату увеличения длительности отпуска
                                // От этого зависит величина vacationdaysgivenclear
                                int dif = vacationdaysgivenclear - vacationshiftbefore; 
                                vacationdaysgivenclear -= dif;
                                vacationdaysgiven -= dif;
                                vacationdaysincreasedate = null; // Обнуляем, чтобы во второй раз на 5 дней не срезало.
                            }
                        }
                    }
                }

                // Все неиспользованные перенесенные дни отпуска сгорают.
                if (transferprevious > vacationdaysconsumedprev)
                {
                    int unused = transferprevious - vacationdaysconsumedprev;
                    
                    // Если мы рассматриваем актуальный период (то есть действует сейчас), то считается, что прошлый период еще не должен сгореть
                    if (!(actual))
                    {
                        vacationdaysconsumed += unused;
                        jobperiodPrevious.Vacationdaystransferleft -= unused; // На предыдущем периоде срезаем неизрасходованное.
                    }
                    //jobperiodPrevious.Vacationdaystransferleft -= unused; // На предыдущем периоде срезаем неизрасходованное.
                }

                if (vacationcount)
                {
                    vacationdaystransfer = vacationdaysgiven - vacationdaysconsumed;
                    vacationdaystransferleft = vacationdaystransfer;
                }
                else
                {
                    vacationdaystransfer = 0;
                    vacationdaystransferleft = 0;
                }
                


                Jobperiod jobperiod = new Jobperiod();
                jobperiod.Start = start;
                jobperiod.End = end;
                jobperiod.Actual = actual;
                jobperiod.Vacationdaysgiven = vacationdaysgiven;
                jobperiod.Vacationdaysgivenclear = vacationdaysgivenclear;
                jobperiod.Vacationdaysconsumed = vacationdaysconsumed;
                jobperiod.Vacationdaysconsumedprevious = vacationdaysconsumedprev;
                jobperiod.Vacationdaystransfer = vacationdaystransfer;
                jobperiod.Vacationdaystransferleft = vacationdaystransferleft;
                jobperiod.Vacationcount = vacationcount;
                jobperiod.Vacationshiftdate = vacationshiftdate;
                jobperiod.Vacationshiftbefore = vacationshiftbefore;
                jobperiod.Vacationshiftafter = vacationshiftafter;
                jobperiod.Experience = experience;
                jobperiod.ExperienceDays = experienceDays;
                jobperiod.ExperienceMonths = experienceMonths;
                jobperiod.ExperienceYears = experienceYears;
                jobperiod.Stateserviceyears = experienceYearsStatecivil;
                jobperiod.Stateservicemonths = experienceMonthsStatecivil;
                jobperiod.Stateservicedays = experienceDaysStatecivil;
                jobperiod.VacationshiftdateStateservice = vacationshiftdateStatecivil;
                jobperiod.VacationshiftbeforeStateservice = vacationshiftbeforeStatecivil;
                jobperiod.VacationshiftafterStateservice = vacationshiftafterStatecivil;
                jobperiods.Add(jobperiod);

                periodpoint = end.AddDays(1);
                jobperiodPrevious = jobperiod;
            }

            return jobperiods;
        }


        public int DateDiffsToYears(IEnumerable<DateDiff> dateDiffs)
        {
            int days = 0;
            int months = 0;
            int years = 0;
            foreach (DateDiff dateDiff in dateDiffs)
            {
                years += dateDiff.ElapsedYears;
                months += dateDiff.ElapsedMonths;
                days += dateDiff.ElapsedDays;
            }
            while (days > 29)
            {
                days -= 30;
                months += 1;
            }
            while (months > 11)
            {
                months -= 12;
                years += 1;
            }
            return years;
        }

        public int DateDiffsToMonths(IEnumerable<DateDiff> dateDiffs)
        {
            int days = 0;
            int months = 0;
            int years = 0;
            foreach (DateDiff dateDiff in dateDiffs)
            {
                years += dateDiff.ElapsedYears;
                months += dateDiff.ElapsedMonths;
                days += dateDiff.ElapsedDays;
            }
            while (days > 29)
            {
                days -= 30;
                months += 1;
            }
            while (months > 11)
            {
                months -= 12;
                years += 1;
            }
            return months;
        }

        public int DateDiffsToDays(IEnumerable<DateDiff> dateDiffs)
        {
            int days = 0;
            int months = 0;
            int years = 0;
            foreach (DateDiff dateDiff in dateDiffs)
            {
                years += dateDiff.ElapsedYears;
                months += dateDiff.ElapsedMonths;
                days += dateDiff.ElapsedDays;
            }
            while (days > 29)
            {
                days -= 30;
                months += 1;
            }
            while (months > 11)
            {
                months -= 12;
                years += 1;
            }
            return days;
        }


        public List<PersonManager> GetPersons(User user, IEnumerable<Position> positions, int structureid, bool fastSearch = false)
        {
            Structure structure = null;
            if (structureid != 0)
            {
                structure = GetOriginalStructure(structureid);
            }
            return GetPersons(user, positions, structure, fastSearch);
        }

        /**
         * Получает список личностей, прикрепленных к должностям и список личностей, прикрепленный к подразделению.
         */
        public List<PersonManager> GetPersons(User user, IEnumerable<Position> positions, Structure structure = null, bool fastSearch = false)
        {
            List<PersonManager> persons = new List<PersonManager>();
            foreach (Position position in positions)
            {
                if (PersonsPositionAsKeyLocal().ContainsKey(position.Id))
                {
                    foreach (Person personLocal in PersonsPositionAsKeyLocal()[position.Id])
                    {
                        persons.Add(GetPersonManager(user, personLocal, fastSearch));
                    }
                    // persons.AddRange(PersonsPositionAsKeyLocal()[position.Id]);
                }

            }
            if (structure != null)
            {
                int structureid = StructureBaseId(structure);
                int minusid = -structureid;
                if (PersonsStructureAsKeyLocal().ContainsKey(minusid))
                {
                    foreach (Person personLocal in PersonsStructureAsKeyLocal()[minusid])
                    {
                        persons.Add(GetPersonManager(user, personLocal, fastSearch));
                    }
                    //persons.AddRange(PersonsStructureAsKeyLocal()[minusid]);
                }
            }
            
            return persons;
        }

        public void AddPersonRank(User user, Personrank personrank)
        {
            context.Personrank.Add(personrank);
            SaveChanges();
            UpdatePersonranksLocal();
        }

        public void ChangePersonRank(User user, Personrank personrank)
        {
            Personrank personrankContext = context.Personrank.FirstOrDefault(p => p.Id == personrank.Id);
            if (personrankContext == null)
            {
                return;
            }
            personrankContext.Decreedate = personrank.Decreedate;
            personrankContext.Decreenumber = personrank.Decreenumber;
            personrankContext.Decreenumbertype = personrank.Decreenumbertype;
            personrankContext.Decreeid = personrank.Decreeid;
            personrankContext.Datestart = personrank.Datestart;
            personrankContext.Rank = personrank.Rank;
            personrankContext.Rankstring = personrank.Rankstring;
            personrankContext.Structure = personrank.Structure;
            personrankContext.Structureid = personrank.Structureid;

            SaveChanges();
            UpdatePersonranksLocal();
        }

        public void DeletePersonRank(User user, Personrank personrank)
        {
            int minusid = -personrank.Id;
            Personrank personrankContext = context.Personrank.FirstOrDefault(p => p.Id == minusid);
            if (personrankContext == null)
            {
                return;
            }
            context.Personrank.Remove(personrankContext);

            SaveChanges();
            UpdatePersonranksLocal();
        }

        public void AddPersonAdditionalagreement(User user, Personadditionalagreement personadditionalagreement)
        {
            context.Personadditionalagreement.Add(personadditionalagreement);
            SaveChanges();
        }

        public void ChangePersonAdditionalagreement(User user, Personadditionalagreement personadditionalagreement)
        {
            Personadditionalagreement personadditionalagreementContext = context.Personadditionalagreement.FirstOrDefault(p => p.Id == personadditionalagreement.Id);
            if (personadditionalagreement == null)
            {
                return;
            }
            personadditionalagreementContext.Contract = personadditionalagreement.Contract;
            personadditionalagreementContext.Datestart = personadditionalagreement.Datestart;
            personadditionalagreementContext.Duration = personadditionalagreement.Duration;

            SaveChanges();
        }

        public void DeletePersonAdditionalagreement(User user, Personadditionalagreement personadditionalagreement)
        {
            int minusid = -personadditionalagreement.Id;
            Personadditionalagreement personadditionalagreementContext = context.Personadditionalagreement.FirstOrDefault(p => p.Id == minusid);
            if (personadditionalagreementContext == null)
            {
                return;
            }
            context.Personadditionalagreement.Remove(personadditionalagreementContext);

            SaveChanges();
        }

        public void AddHoliday(User user, Holiday holiday)
        {
            context.Holiday.Add(holiday);
            SaveChanges();
        }

        public void ChangeHoliday(User user, Holiday holiday)
        {
            Holiday holidayContext = context.Holiday.FirstOrDefault(p => p.Id == holiday.Id);
            if (holidayContext == null)
            {
                return;
            }
            holidayContext.Date = holiday.Date;
            holidayContext.Permanent = holiday.Permanent;


            SaveChanges();
        }

        public void DeleteHoliday(User user, Holiday holiday)
        {
            int minusid = -holiday.Id;
            Holiday holidayContext = context.Holiday.FirstOrDefault(p => p.Id == minusid);
            if (holidayContext == null)
            {
                return;
            }
            context.Holiday.Remove(holidayContext);

            SaveChanges();
        }

        public void AddPersonContract(User user, Personcontract personcontract)
        {
            context.Personcontract.Add(personcontract);
            SaveChanges();
            UpdatePersoncontractsLocal();
        }

        public void ChangePersonContract(User user, Personcontract personcontract)
        {
            Personcontract personcontractContext = context.Personcontract.FirstOrDefault(p => p.Id == personcontract.Id);
            if (personcontractContext == null)
            {
                return;
            }
            personcontractContext.Datestart = personcontract.Datestart;
            personcontractContext.Dateend = personcontract.Dateend;
            personcontractContext.Pay = personcontract.Pay;
            personcontractContext.Ordernumber = personcontract.Ordernumber;
            personcontractContext.Ordernumbertype = personcontract.Ordernumbertype;
            personcontractContext.Orderdate = personcontract.Orderdate;
            personcontractContext.Orderwho = personcontract.Orderwho;
            personcontractContext.Orderwhoid = personcontract.Orderwhoid;
            personcontractContext.Orderid = personcontract.Orderid;
            personcontractContext.Sourceoffinancing = personcontract.Sourceoffinancing;
            personcontractContext.Payvalue = personcontract.Payvalue;
            personcontractContext.Stateserviceyears = personcontract.Stateserviceyears;
            personcontractContext.Stateservicemonths = personcontract.Stateservicemonths;
            personcontractContext.Stateservicedays = personcontract.Stateservicedays;
            personcontractContext.Vacationdays = personcontract.Vacationdays;

            SaveChanges();
            UpdatePersoncontractsLocal();
        }

        public void DeletePersonContract(User user, Personcontract personcontract)
        {
            int minusid = -personcontract.Id;
            Personcontract personcontractContext = context.Personcontract.FirstOrDefault(p => p.Id == minusid);
            if (personcontractContext == null)
            {
                return;
            }
            context.Personcontract.Remove(personcontractContext);

            SaveChanges();
            UpdatePersoncontractsLocal();
        }

        public void AddPersonRelative(User user, Personrelative personrelative)
        {
            context.Personrelative.Add(personrelative);
            SaveChanges();
            UpdatePersonrelativesLocal();
        }

        public void ChangePersonRelative(User user, Personrelative personrelative)
        {
            Personrelative personrelativeContext = context.Personrelative.FirstOrDefault(p => p.Id == personrelative.Id);
            if (personrelativeContext == null)
            {
                return;
            }
            personrelativeContext.Relativetype = personrelative.Relativetype;
            personrelativeContext.Fio = personrelative.Fio;
            personrelativeContext.Fioother = personrelative.Fioother;
            personrelativeContext.Jobplace = personrelative.Jobplace;
            personrelativeContext.Jobposition = personrelative.Jobposition;
            personrelativeContext.Birthday = personrelative.Birthday;
            personrelativeContext.Birthplace = personrelative.Birthplace;
            personrelativeContext.Livecountry = personrelative.Livecountry;
            personrelativeContext.Livestate = personrelative.Livestate;
            personrelativeContext.Livesubstate = personrelative.Livesubstate;
            personrelativeContext.Livecitysubstate = personrelative.Livecitysubstate;
            personrelativeContext.Livecitytype = personrelative.Livecitytype;
            personrelativeContext.Livecity = personrelative.Livecity;
            personrelativeContext.Livestreettype = personrelative.Livestreettype;
            personrelativeContext.Livestreet = personrelative.Livestreet;
            personrelativeContext.Livehouse = personrelative.Livehouse;
            personrelativeContext.Livehousing = personrelative.Livehousing;
            personrelativeContext.Liveflat = personrelative.Liveflat;
            personrelativeContext.Birthcountry = personrelative.Birthcountry;
            personrelativeContext.Birthstate = personrelative.Birthstate;
            personrelativeContext.Birthsubstate = personrelative.Birthsubstate;
            personrelativeContext.Birthcitysubstate = personrelative.Birthcitysubstate;
            personrelativeContext.Birthcitytype = personrelative.Birthcitytype;
            personrelativeContext.Birthcity = personrelative.Birthcity;
            personrelativeContext.Birthadditional = personrelative.Birthadditional;
            personrelativeContext.Nodata = personrelative.Nodata;
            personrelativeContext.Death = personrelative.Death;
            personrelativeContext.Deathnodata = personrelative.Deathnodata;
            personrelativeContext.Deathcountry = personrelative.Deathcountry;
            personrelativeContext.Deathstate = personrelative.Deathstate;
            personrelativeContext.Deathsubstate = personrelative.Deathsubstate;
            personrelativeContext.Deathcitysubstate = personrelative.Deathcitysubstate;
            personrelativeContext.Deathcitytype = personrelative.Deathcitytype;
            personrelativeContext.Deathcity = personrelative.Deathcity;
            personrelativeContext.Deathadditional = personrelative.Deathadditional;

            //personcontractContext.Datestart = personcontract.Datestart;
            //personcontractContext.Dateend = personcontract.Dateend;
            //personcontractContext.Pay = personcontract.Pay;


            SaveChanges();
            UpdatePersonrelativesLocal();
        }

        public void DeletePersonRelative(User user, Personrelative personrelative)
        {
            int minusid = -personrelative.Id;
            Personrelative personrelativeContext = context.Personrelative.FirstOrDefault(p => p.Id == minusid);
            if (personrelativeContext == null)
            {
                return;
            }
            context.Personrelative.Remove(personrelativeContext);

            SaveChanges();
            UpdatePersonrelativesLocal();
        }

        public void AddPersonVacation(User user, Personvacation personvacation)
        {
            // Максимальное время на проезд - 15.
            if (personvacation.Trip > 15)
            {
                personvacation.Trip = 15;
            }
            context.Personvacation.Add(personvacation);
            SaveChanges();
            UpdatePersonvacationsLocal();
        }

        public void ChangePersonVacation(User user, Personvacation personvacation)
        {
            Personvacation personvacationContext = context.Personvacation.FirstOrDefault(p => p.Id == personvacation.Id);
            if (personvacationContext == null)
            {
                return;
            }
            personvacationContext.Vacationmilitary = personvacation.Vacationmilitary;
            personvacationContext.Vacationtype = personvacation.Vacationtype;
            personvacationContext.Date = personvacation.Date;
            personvacationContext.Duration = personvacation.Duration;
            personvacationContext.Trip = personvacation.Trip;
            personvacationContext.Compensation = personvacation.Compensation;
            personvacationContext.Compensationdate = personvacation.Compensationdate;
            personvacationContext.Compensationnumber = personvacation.Compensationnumber;
            personvacationContext.Compensationdays = personvacation.Compensationdays;
            personvacationContext.Cancel = personvacation.Cancel;
            personvacationContext.Canceldate = personvacation.Canceldate;
            personvacationContext.Allowstart = personvacation.Allowstart;
            personvacationContext.Allowend = personvacation.Allowend;
            personvacationContext.Holidays = personvacation.Holidays;
            personvacationContext.Canceldateend = personvacation.Canceldateend;
            personvacationContext.Cancelcontinue = personvacation.Cancelcontinue;

            //personcontractContext.Datestart = personcontract.Datestart;
            //personcontractContext.Dateend = personcontract.Dateend;
            //personcontractContext.Pay = personcontract.Pay;


            SaveChanges();
            UpdatePersonvacationsLocal();
        }

        public void DeletePersonVacation(User user, Personvacation personvacation)
        {
            int minusid = -personvacation.Id;
            Personvacation personvacationContext = context.Personvacation.FirstOrDefault(p => p.Id == minusid);
            if (personvacationContext == null)
            {
                return;
            }
            context.Personvacation.Remove(personvacationContext);

            SaveChanges();
            UpdatePersonvacationsLocal();
        }

        public void AddPersonAttestation(User user, Personattestation personattestation)
        {
            context.Personattestation.Add(personattestation);
            SaveChanges();
        }

        public void ChangePersonAttestation(User user, Personattestation personattestation)
        {
            Personattestation personattestationContext = context.Personattestation.FirstOrDefault(p => p.Id == personattestation.Id);
            if (personattestationContext == null)
            {
                return;
            }
            personattestationContext.Attestationtype = personattestation.Attestationtype;
            personattestationContext.Date = personattestation.Date;
            personattestationContext.Result = personattestation.Result;
            personattestationContext.Recomendation = personattestation.Recomendation;
            //personcontractContext.Datestart = personcontract.Datestart;
            //personcontractContext.Dateend = personcontract.Dateend;
            //personcontractContext.Pay = personcontract.Pay;


            SaveChanges();
        }

        public void DeletePersonAttestation(User user, Personattestation personattestation)
        {
            int minusid = -personattestation.Id;
            Personattestation personattestationContext = context.Personattestation.FirstOrDefault(p => p.Id == minusid);
            if (personattestationContext == null)
            {
                return;
            }
            context.Personattestation.Remove(personattestationContext);

            SaveChanges();
        }

        public void AddRelativetype(Relativetype relativetype)
        {
            context.Relativetype.Add(relativetype);
            context.SaveChanges();
        }

        public void UpdateRelativetype(Relativetype relativetype)
        {
            Relativetype contextRelativetype = context.Relativetype.First(st => st.Id == relativetype.Id);
            contextRelativetype.Name = relativetype.Name;
            context.SaveChanges();
        }

        public void AddAttestationtype(Attestationtype attestationtype)
        {
            context.Attestationtype.Add(attestationtype);
            context.SaveChanges();
        }

        public void UpdateAttestationtype(Attestationtype attestationtype)
        {
            Attestationtype contextAttestationtype = context.Attestationtype.First(st => st.Id == attestationtype.Id);
            contextAttestationtype.Name = attestationtype.Name;
            context.SaveChanges();
        }

        public void AddVacationmilitary(Vacationmilitary vacationmilitary)
        {
            context.Vacationmilitary.Add(vacationmilitary);
            context.SaveChanges();
        }

        public void UpdateVacationmilitary(Vacationmilitary vacationmilitary)
        {
            Vacationmilitary contextVacationmilitary = context.Vacationmilitary.First(st => st.Id == vacationmilitary.Id);
            contextVacationmilitary.Name = vacationmilitary.Name;
            context.SaveChanges();
        }

        public void AddVacationtype(Vacationtype vacationtype)
        {
            context.Vacationtype.Add(vacationtype);
            context.SaveChanges();
        }

        public void UpdateVacationtype(Vacationtype vacationtype)
        {
            Vacationtype contextVacationtype = context.Vacationtype.First(st => st.Id == vacationtype.Id);
            contextVacationtype.Name = vacationtype.Name;
            contextVacationtype.Cadet = vacationtype.Cadet;
            contextVacationtype.Civil = vacationtype.Civil;
            contextVacationtype.Military = vacationtype.Military;
            contextVacationtype.Social = vacationtype.Social;
            contextVacationtype.Transferworkyear = vacationtype.Transferworkyear;
            context.SaveChanges();
        }

        public void AddLanguagetype(Languagetype languagetype)
        {
            context.Languagetype.Add(languagetype);
            context.SaveChanges();
        }

        public void UpdateLanguagetype(Languagetype languagetype)
        {
            Languagetype contextLanguagetype = context.Languagetype.First(st => st.Id == languagetype.Id);
            contextLanguagetype.Name = languagetype.Name;
            context.SaveChanges();
        }

        public void AddLanguageskill(Languageskill languageskill)
        {
            context.Languageskill.Add(languageskill);
            context.SaveChanges();
        }

        public void UpdateLanguageskill(Languageskill languageskill)
        {
            Languageskill contextLanguageskill = context.Languageskill.First(st => st.Id == languageskill.Id);
            contextLanguageskill.Name = languageskill.Name;
            context.SaveChanges();
        }

        public void AddJobtype(Jobtype jobtype)
        {
            context.Jobtype.Add(jobtype);
            context.SaveChanges();
        }

        public void UpdateJobtype(Jobtype jobtype)
        {
            Jobtype contextJobtype = context.Jobtype.First(st => st.Id == jobtype.Id);
            contextJobtype.Name = jobtype.Name;
            context.SaveChanges();
        }

        public void AddServicetype(Servicetype servicetype)
        {
            context.Servicetype.Add(servicetype);
            context.SaveChanges();
        }

        public void UpdateServicetype(Servicetype servicetype)
        {
            Servicetype contextServicetype = context.Servicetype.First(st => st.Id == servicetype.Id);
            contextServicetype.Name = servicetype.Name;
            context.SaveChanges();
        }

        public void AddServicefeature(Servicefeature servicefeature)
        {
            context.Servicefeature.Add(servicefeature);
            context.SaveChanges();
        }

        public void UpdateServicefeature(Servicefeature servicefeature)
        {
            Servicefeature contextServicefeature = context.Servicefeature.First(st => st.Id == servicefeature.Id);
            contextServicefeature.Name = servicefeature.Name;
            context.SaveChanges();
        }

        public void AddServicecoef(Servicecoef servicecoef)
        {
            context.Servicecoef.Add(servicecoef);
            context.SaveChanges();
        }

        public void UpdateServicecoef(Servicecoef servicecoef)
        {
            Servicecoef contextServicecoef = context.Servicecoef.First(st => st.Id == servicecoef.Id);
            contextServicecoef.Name = servicecoef.Name;
            context.SaveChanges();
        }

        public void AddPenalty(Penalty penalty)
        {
            context.Penalty.Add(penalty);
            context.SaveChanges();
        }

        public void UpdatePenalty(Penalty penalty)
        {
            Penalty contextPenalty = context.Penalty.First(el => el.Id == penalty.Id);
            contextPenalty.Name = penalty.Name;
            context.SaveChanges();
        }

        public void AddCountry(Country country)
        {
            context.Country.Add(country);
            context.SaveChanges();
        }

        public void UpdateCountry(Country country)
        {
            Country contextCountry = context.Country.First(el => el.Id == country.Id);
            contextCountry.Name = country.Name;
            context.SaveChanges();
        }

        public void AddRewardtype(Rewardtype rewardtype)
        {
            context.Rewardtype.Add(rewardtype);
            context.SaveChanges();
        }

        public void UpdateRewardtype(Rewardtype rewardtype)
        {
            Rewardtype contextRewardtype = context.Rewardtype.First(el => el.Id == rewardtype.Id);
            contextRewardtype.Name = rewardtype.Name;
            context.SaveChanges();
        }

        public void AddReward(Reward reward)
        {
            context.Reward.Add(reward);
            context.SaveChanges();
        }

        public void UpdateReward(Reward reward)
        {
            Reward contextReward = context.Reward.First(el => el.Id == reward.Id);
            contextReward.Name = reward.Name;
            contextReward.Rewardtype = reward.Rewardtype;
            context.SaveChanges();
        }

        public void AddIllcode(Illcode illcode)
        {
            context.Illcode.Add(illcode);
            context.SaveChanges();
        }

        public void UpdateIllcode(Illcode illcode)
        {
            Illcode contextIllcode = context.Illcode.First(el => el.Id == illcode.Id);
            contextIllcode.Name = illcode.Name;
            context.SaveChanges();
        }

        public void AddIllregime(Illregime illregime)
        {
            context.Illregime.Add(illregime);
            context.SaveChanges();
        }

        public void UpdateIllregime(Illregime illregime)
        {
            Illregime contextIllregime = context.Illregime.First(el => el.Id == illregime.Id);
            contextIllregime.Name = illregime.Name;
            context.SaveChanges();
        }

        public void AddEducationlevel(Educationlevel educationlevel)
        {
            context.Educationlevel.Add(educationlevel);
            context.SaveChanges();
        }

        public void UpdateEducationlevel(Educationlevel educationlevel)
        {
            Educationlevel contextEducationlevel = context.Educationlevel.First(el => el.Id == educationlevel.Id);
            contextEducationlevel.Levelname = educationlevel.Levelname;
            contextEducationlevel.Levelcomment = educationlevel.Levelcomment;
            context.SaveChanges();
        }

        public void AddEducationtype(Educationtype educationtype)
        {
            context.Educationtype.Add(educationtype);
            context.SaveChanges();
        }

        public void UpdateEducationtype(Educationtype educationtype)
        {
            Educationtype contextEducationtype = context.Educationtype.First(el => el.Id == educationtype.Id);
            contextEducationtype.Name = educationtype.Name;
            context.SaveChanges();
        }

        public void AddNormativ(Normativ normativ)
        {
            context.Normativ.Add(normativ);
            context.SaveChanges();
        }

        public void UpdateNormativ(Normativ normativ)
        {
            Normativ contextNormativ = context.Normativ.First(el => el.Id == normativ.Id);
            contextNormativ.Name = normativ.Name;
            context.SaveChanges();
        }

        public void AddDrivertype(Drivertype drivertype)
        {
            context.Drivertype.Add(drivertype);
            context.SaveChanges();
        }

        public void UpdateDrivertype(Drivertype drivertype)
        {
            Drivertype contextDrivertype = context.Drivertype.First(el => el.Id == drivertype.Id);
            contextDrivertype.Name = drivertype.Name;
            context.SaveChanges();
        }

        public void AddDrivercategory(Drivercategory drivercategory)
        {
            context.Drivercategory.Add(drivercategory);
            context.SaveChanges();
        }

        public void UpdateDrivercategory(Drivercategory drivercategory)
        {
            Drivercategory contextDrivercategory = context.Drivercategory.First(el => el.Id == drivercategory.Id);
            contextDrivercategory.Name = drivercategory.Name;
            context.SaveChanges();
        }

        public void AddEducationdocument(Educationdocument educationdocument)
        {
            context.Educationdocument.Add(educationdocument);
            context.SaveChanges();
        }

        public void UpdateEducationdocument(Educationdocument educationdocument)
        {
            Educationdocument contextEducationdocument = context.Educationdocument.First(el => el.Id == educationdocument.Id);
            contextEducationdocument.Name = educationdocument.Name;
            context.SaveChanges();
        }

        public void AddPermissiontype(Permissiontype permissiontype)
        {
            context.Permissiontype.Add(permissiontype);
            context.SaveChanges();
        }

        public void UpdatePermissiontype(Permissiontype permissiontype)
        {
            Permissiontype contextPermissiontype = context.Permissiontype.First(el => el.Id == permissiontype.Id);
            contextPermissiontype.Name = permissiontype.Name;
            context.SaveChanges();
        }

        public void AddProoftype(Prooftype prooftype)
        {
            context.Prooftype.Add(prooftype);
            context.SaveChanges();
        }

        public void UpdateProoftype(Prooftype prooftype)
        {
            Prooftype contextProoftype = context.Prooftype.First(el => el.Id == prooftype.Id);
            contextProoftype.Name = prooftype.Name;
            context.SaveChanges();
        }

        public void AddPersonLanguage(User user, Personlanguage personlanguage)
        {
            context.Personlanguage.Add(personlanguage);
            SaveChanges();
        }

        public void ChangePersonLanguage(User user, Personlanguage personlanguage)
        {
            Personlanguage personlanguageContext = context.Personlanguage.FirstOrDefault(p => p.Id == personlanguage.Id);
            if (personlanguageContext == null)
            {
                return;
            }
            personlanguageContext.Languageskill = personlanguage.Languageskill;
            personlanguageContext.Languagetype = personlanguage.Languagetype;

            SaveChanges();
        }

        public void DeletePersonLanguage(User user, Personlanguage personlanguage)
        {
            int minusid = -personlanguage.Id;
            Personlanguage personlanguageContext = context.Personlanguage.FirstOrDefault(p => p.Id == minusid);
            if (personlanguageContext == null)
            {
                return;
            }
            context.Personlanguage.Remove(personlanguageContext);

            SaveChanges();
        }

        public void AddPersonJob(User user, Personjob personjob)
        {
            // Если работа актуальна по текущий момент, мы стираем дату окончания для избегания багов на всякий случай
            if (personjob.Actual > 0)
            {
                personjob.End = null;

                foreach (Personjob personjobExist in context.Personjob.Where(p => p.Person == personjob.Person))
                {
                    if (personjobExist.Actual > 0)
                    {
                        personjobExist.Actual = 0;
                        personjobExist.End = DateTime.Now;
                    }
                }
            } 
            context.Personjob.Add(personjob);
            
            if (personjob.Position > 0 && personjob.Actual > 0 ) // Если есть привязка к какой-то определенной должности.
            {
                AppointPerson(user, personjob.Person, personjob.Position);
                SaveChanges();
            }

            SaveChanges();
            UpdatePersonjobsLocal();

            List<Personjobprivelege> personjobpriveleges = new List<Personjobprivelege>(personjob.Personjobpriveleges);

            // Если стоит галочка зачесть льготные периоды в выслугу лет, то создаем запись о них. Если галочки нет, то пропускаем льготную выслугу.
            if (personjob.Privelege > 0)
            {
                foreach (Personjobprivelege personjobprivelege in personjobpriveleges)
                {
                    Personjobprivelege newPersonjobprivelege = new Personjobprivelege();
                    newPersonjobprivelege.Personjob = personjob.Id;
                    newPersonjobprivelege.Start = personjobprivelege.Start;
                    newPersonjobprivelege.End = personjobprivelege.End;
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
                    List<Personjobprivelegeperiod> personjobprivelegeperiods = new List<Personjobprivelegeperiod>(personjobprivelege.Personjobprivelegeperiods);
                    context.Personjobprivelege.Add(newPersonjobprivelege);
                    SaveChanges();
                    foreach (var personjobprivelegeperiod in personjobprivelegeperiods)
                    {
                        Personjobprivelegeperiod newPersonjobprivelegeperiod = new Personjobprivelegeperiod();
                        newPersonjobprivelegeperiod.Personjobprivelege = newPersonjobprivelege.Id;
                        newPersonjobprivelegeperiod.Start = personjobprivelegeperiod.Start;
                        newPersonjobprivelegeperiod.End = personjobprivelegeperiod.End;

                        context.Personjobprivelegeperiod.Add(newPersonjobprivelegeperiod);
                    }
                    SaveChanges();
                }
            }
            
        }

        public void ChangePersonJob(User user, Personjob personjob)
        {
            Personjob personjobContext = context.Personjob.FirstOrDefault(p => p.Id == personjob.Id);
            bool wasActual = false;
            // Записываем, стояла ли метка актуально. Если да, то мы просто снимем потом с должности.
            if (personjobContext.Actual == 1)
            {
                wasActual = true;
            }
            if (personjobContext == null)
            {
                return;
            }
            personjobContext.Jobtype = personjob.Jobtype;
            personjobContext.Start = personjob.Start;
            personjobContext.End = personjob.End;
            personjobContext.Jobplace = personjob.Jobplace;
            personjobContext.Jobposition = personjob.Jobposition;
            personjobContext.Jobpositionplace = personjob.Jobpositionplace;
            personjobContext.Servicetype = personjob.Servicetype;
            personjobContext.Servicetypestr = personjob.Servicetypestr;
            personjobContext.Servicefeature = personjob.Servicefeature;
            personjobContext.Serviceorder = personjob.Serviceorder;
            personjobContext.Servicecoef = personjob.Servicecoef;
            personjobContext.Serviceplace = personjob.Serviceplace;
            personjobContext.Ordernumber = personjob.Ordernumber;
            personjobContext.Ordernumbertype = personjob.Ordernumbertype;
            personjobContext.Orderdate = personjob.Orderdate;
            personjobContext.Orderwho = personjob.Orderwho;
            personjobContext.Actual = personjob.Actual;
            personjobContext.Manual = personjob.Manual;
            //personjobContext.Statecivil = personjob.Statecivil;
            personjobContext.Statecivilstart = personjob.Statecivilstart;
            personjobContext.Statecivilend = personjob.Statecivilend;
            personjobContext.Startcustom = personjob.Startcustom;
            personjobContext.Privelege = personjob.Privelege;
            // Если работа актуальна по текущий момент, мы стираем дату окончания для избегания багов на всякий случай
            if (personjobContext.Actual > 0 && personjob.Actual == 0)
            {
                personjobContext.End = null;
            }
            personjobContext.Mchs = personjob.Mchs;
            personjobContext.Vacationdays = personjob.Vacationdays;
            personjobContext.Positiontoselect = personjob.Positiontoselect;
            personjobContext.Position = personjob.Position;

            bool statecivilchange = false;
            if (personjobContext.Statecivil != personjob.Statecivil)
            {
                statecivilchange = true;
            }
            personjobContext.Statecivil = personjob.Statecivil;
            personjobContext.Statecivilstart = personjob.Statecivilstart;
            personjobContext.Statecivilend = personjob.Statecivilend;
            /**
             * Если ставим, что стаж идет в гос. службу
             */
            if (statecivilchange && personjob.Statecivil == 1)
            {
                personjobContext.Statecivilstart = personjobContext.Start;
                personjobContext.Statecivilend = personjobContext.End;
                if (personjobContext.Actual == 1)
                {
                    personjobContext.Statecivilend = null;
                }
            }
            /**
             * Если убираем, что стаж идет в гос. службу
             */
            if (statecivilchange && personjob.Statecivil == 0)
            {
                personjobContext.Statecivilstart = null;
                personjobContext.Statecivilend = null;
                SaveChanges();
                UpdatePersonjobsLocal();
            }

            if (personjobContext.Position > 0 && personjobContext.Actual > 0)
            {
                AppointPerson(user, personjobContext.Person, personjobContext.Position);
                SaveChanges();
            }
            else if (wasActual)
            {
                TakeoffPerson(user, personjobContext.Person);
                SaveChanges();
            }

            IEnumerable<Personjobprivelege> personjobpriveleges = context.Personjobprivelege.Where(e => e.Personjob == personjobContext.Id);
            if(personjobpriveleges.ToList().Count > 0)
            {
                foreach (var personjobprivelege in personjobpriveleges)
                {
                    IEnumerable<Personjobprivelegeperiod> personjobprivelegeperiods = context.Personjobprivelegeperiod.Where(e => e.Personjobprivelege == personjobprivelege.Id);
                    context.Personjobprivelege.Remove(personjobprivelege);
                    context.Personjobprivelegeperiod.RemoveRange(personjobprivelegeperiods);
                }
                SaveChanges();
            }
            


            personjobpriveleges = new List<Personjobprivelege>(personjob.Personjobpriveleges);

            // Если стоит галочка зачесть льготные периоды в выслугу лет, то создаем запись о них. Если галочки нет, то пропускаем льготную выслугу.
            if (personjob.Privelege > 0)
            {
                foreach (Personjobprivelege personjobprivelege in personjobpriveleges)
                {
                    Personjobprivelege newPersonjobprivelege = new Personjobprivelege();
                    newPersonjobprivelege.Personjob = personjob.Id;
                    newPersonjobprivelege.Start = personjobprivelege.Start;
                    newPersonjobprivelege.End = personjobprivelege.End;
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
                    List<Personjobprivelegeperiod> personjobprivelegeperiods = new List<Personjobprivelegeperiod>(personjobprivelege.Personjobprivelegeperiods);
                    context.Personjobprivelege.Add(newPersonjobprivelege);
                    SaveChanges();
                    foreach (var personjobprivelegeperiod in personjobprivelegeperiods)
                    {
                        Personjobprivelegeperiod newPersonjobprivelegeperiod = new Personjobprivelegeperiod();
                        newPersonjobprivelegeperiod.Personjobprivelege = newPersonjobprivelege.Id;
                        newPersonjobprivelegeperiod.Start = personjobprivelegeperiod.Start;
                        newPersonjobprivelegeperiod.End = personjobprivelegeperiod.End;

                        context.Personjobprivelegeperiod.Add(newPersonjobprivelegeperiod);
                    }
                    SaveChanges();
                }
            }
            
        }

        public void DeletePersonJob(User user, Personjob personjob)
        {
            int minusid = -personjob.Id;
            Personjob personjobContext = context.Personjob.FirstOrDefault(p => p.Id == minusid);
            if (personjobContext == null)
            {
                return;
            }
            context.Personjob.Remove(personjobContext);
            

            if (personjobContext.Position > 0 && personjobContext.Actual > 0)
            {
                TakeoffPerson(user, personjobContext.Person);
            }

            SaveChanges();
            UpdatePersonjobsLocal();
        }

        public void AddPersonTransfer(User user, Persontransfer persontransfer)
        {
            context.Persontransfer.Add(persontransfer);
            SaveChanges();

            //if (personjob.Position > 0) // Если есть привязка к какой-то определенной должности.
            //{
            //    AppointPerson(user, personjob.Person, personjob.Position);
            //    SaveChanges();
            //}
        }

        public void ChangePersonTransfer(User user, Persontransfer persontransfer)
        {
            Persontransfer persontransferContext = context.Persontransfer.FirstOrDefault(p => p.Id == persontransfer.Id);
            if (persontransferContext == null)
            {
                return;
            }
            persontransferContext.Transfertype = persontransfer.Transfertype;
            persontransferContext.Reason = persontransfer.Reason;
            persontransferContext.Start = persontransfer.Start;
            persontransferContext.End = persontransfer.End;
            persontransferContext.Structure = persontransfer.Structure;
            persontransferContext.Place = persontransfer.Place;
            persontransferContext.Ordernumber = persontransfer.Ordernumber;
            persontransferContext.Orderdate = persontransfer.Orderdate;
            persontransferContext.Orderwho = persontransfer.Orderwho;
            persontransferContext.Structuretoselect = persontransfer.Structuretoselect;
            

            SaveChanges();

            //if (personjobContext.Position > 0 && personjobContext.Actual > 0)
            //{
            //    AppointPerson(user, personjobContext.Person, personjobContext.Position);
            //}
            //else if (personjobContext.Actual > 0)
            //{
            //    TakeoffPerson(user, personjobContext.Person);
            //}

            //SaveChanges();
        }

        public void DeletePersonTransfer(User user, Persontransfer persontransfer)
        {
            int minusid = -persontransfer.Id;
            Persontransfer persontransferContext = context.Persontransfer.FirstOrDefault(p => p.Id == minusid);
            if (persontransferContext == null)
            {
                return;
            }
            context.Persontransfer.Remove(persontransferContext);

            //if (personjobContext.Position > 0 && personjobContext.Actual > 0)
            //{
            //    TakeoffPerson(user, personjobContext.Person);
            //}

            SaveChanges();
        }

        /// <summary>
        /// При выборе должности, на которую будут возвращать сотрудника, возвращает информацию о ней в формате ЭЛД
        /// </summary>
        /// <param name="user"></param>
        /// <param name="personjob"></param>
        /// <returns></returns>
        public Personjob AppointPersonJob(User user, Personjob personjob)
        {
            DateTime date = user.Date.GetValueOrDefault();
            Personjob outputPersonjob = personjob;

            Position position = PositionsLocal().GetValue(personjob.Positiontoselect);
            if (position == null)
            {
                return null;
            }
            Positiontype positiontype = PositiontypesLocal().GetValue(position.Positiontype);
            string positionName = "";
            if (positiontype != null)
            {
                //positionName = positiontype.Name;
                positionName = FormTreeDocument(null, date, position, 1, null);
            }
            //outputPersonjob.Jobposition = positionName; // Больше не пишем, потому что должность записываем сразу в должность + место службы.

            Structure actualStructure = GetActualStructureInfo(position.Structure, date); 
            if (actualStructure != null)
            {
                //string structureName = actualStructure.Name;
                string structureName = FormTreeDocument(actualStructure, date, null, 1, null);
                //string structureTree = FormTree(actualStructure, true, date);
                //string structureTree = FormTreeDocument1(actualStructure, true, date); // Временное решение
                //string structureTree = FormTreeDocument(actualStructure, date, null, 1, null); // Временное решение
                string structureTree = FormTreeDocument(actualStructure, date, position, 1, null); // Временное решение
                //outputPersonjob.Jobplace = structureTree;
                //outputPersonjob.Serviceplace = structureTree;
                outputPersonjob.Jobposition = positionName;
                outputPersonjob.Jobplace = structureName;
                outputPersonjob.Jobpositionplace = structureTree;
                outputPersonjob.Serviceplace = outputPersonjob.Jobpositionplace;
            }
            return outputPersonjob;
        }

        public Persontransfer AppointTransfer(User user, Persontransfer persontransfer)
        {
            DateTime date = user.Date.GetValueOrDefault();
            Persontransfer outputPersontransfer = new Persontransfer();

            Structure structure = StructuresLocal().GetValue(persontransfer.Structuretoselect);
            if (structure == null)
            {
                return null;
            }
            Structure actualStructure = GetActualStructureInfo(structure, date);
            Structure originalStructure = GetOriginalStructure(structure);

            outputPersontransfer.Structure = originalStructure.Id;

            outputPersontransfer.Place = FormTreeDocument(originalStructure, date, null, 1, null);



            return outputPersontransfer;
        }

        public void AddPersonPenalty(User user, Personpenalty personpenalty)
        {
            context.Personpenalty.Add(personpenalty);
            SaveChanges();
        }

        public void ChangePersonPenalty(User user, Personpenalty personpenalty)
        {
            Personpenalty personpenaltyContext = context.Personpenalty.FirstOrDefault(p => p.Id == personpenalty.Id);
            if (personpenaltyContext == null)
            {
                return;
            }
            personpenaltyContext.Penalty = personpenalty.Penalty;
            personpenaltyContext.Violation = personpenalty.Violation;
            personpenaltyContext.Orderwho = personpenalty.Orderwho;
            personpenaltyContext.Ordernumber = personpenalty.Ordernumber;
            personpenaltyContext.Orderdate = personpenalty.Orderdate;

            SaveChanges();
        }

        public void DeletePersonPenalty(User user, Personpenalty personpenalty)
        {
            int minusid = -personpenalty.Id;
            Personpenalty personpenaltyContext = context.Personpenalty.FirstOrDefault(p => p.Id == minusid);
            if (personpenaltyContext == null)
            {
                return;
            }
            context.Personpenalty.Remove(personpenaltyContext);

            SaveChanges();
        }

        public void AddPersonWorktrip(User user, Personworktrip personworktrip)
        {
            context.Personworktrip.Add(personworktrip);
            SaveChanges();
        }

        public void ChangePersonWorktrip(User user, Personworktrip personworktrip)
        {
            Personworktrip personworktripContext = context.Personworktrip.FirstOrDefault(p => p.Id == personworktrip.Id);
            if (personworktripContext == null)
            {
                return;
            }
            personworktripContext.Country = personworktrip.Country;
            personworktripContext.Tripdate = personworktrip.Tripdate;
            personworktripContext.Days = personworktrip.Days;
            personworktripContext.Reason = personworktrip.Reason;
            personworktripContext.Privelege = personworktrip.Privelege;

            SaveChanges();
        }

        public void DeletePersonWorktrip(User user, Personworktrip personworktrip)
        {
            int minusid = -personworktrip.Id;
            Personworktrip personworktripContext = context.Personworktrip.FirstOrDefault(p => p.Id == minusid);
            if (personworktripContext == null)
            {
                return;
            }
            context.Personworktrip.Remove(personworktripContext);

            SaveChanges();
        }

        public void AddPersonElection(User user, Personelection personelection)
        {
            context.Personelection.Add(personelection);
            SaveChanges();
        }

        public void ChangePersonElection(User user, Personelection personelection)
        {
            Personelection personelectionContext = context.Personelection.FirstOrDefault(p => p.Id == personelection.Id);
            if (personelectionContext == null)
            {
                return;
            }
            personelectionContext.Location = personelection.Location;
            personelectionContext.Electionwho = personelection.Electionwho;
            personelectionContext.Electiondate = personelection.Electiondate;
            personelectionContext.Electionwhat = personelection.Electionwhat;
            personelectionContext.Electiondateend = personelection.Electiondateend;

            SaveChanges();
        }

        public void DeletePersonElection(User user, Personelection personelection)
        {
            int minusid = -personelection.Id;
            Personelection personelectionContext = context.Personelection.FirstOrDefault(p => p.Id == minusid);
            if (personelectionContext == null)
            {
                return;
            }
            context.Personelection.Remove(personelectionContext);

            SaveChanges();
        }

        public void AddPersonScience(User user, Personscience personscience)
        {
            context.Personscience.Add(personscience);
            SaveChanges();
        }

        public void ChangePersonScience(User user, Personscience personscience)
        {
            Personscience personscienceContext = context.Personscience.FirstOrDefault(p => p.Id == personscience.Id);
            if (personscienceContext == null)
            {
                return;
            }
            personscienceContext.Sciencetype = personscience.Sciencetype;
            personscienceContext.Sciencedescription = personscience.Sciencedescription;
            personscienceContext.Sciencediplom = personscience.Sciencediplom;
            personscienceContext.Sciencedate = personscience.Sciencedate;

            SaveChanges();
        }

        public void DeletePersonScience(User user, Personscience personscience)
        {
            int minusid = -personscience.Id;
            Personscience personscienceContext = context.Personscience.FirstOrDefault(p => p.Id == minusid);
            if (personscienceContext == null)
            {
                return;
            }
            context.Personscience.Remove(personscienceContext);

            SaveChanges();
        }

        public Personreward AddPersonReward(User user, Personreward personreward)
        {
            context.Personreward.Add(personreward);
            SaveChanges();
            UpdatePersonrewardsLocal();

            return personreward;
            
        }

        public void ChangePersonReward(User user, Personreward personreward)
        {
            Personreward personrewardContext = context.Personreward.FirstOrDefault(p => p.Id == personreward.Id);
            if (personrewardContext == null)
            {
                return;
            }
            personrewardContext.Rewardtype = personreward.Rewardtype;
            personrewardContext.Reward = personreward.Reward;
            personrewardContext.Reason = personreward.Reason;
            personrewardContext.Order = personreward.Order;
            personrewardContext.Ordernumbertype = personreward.Ordernumbertype;
            personrewardContext.Orderwho = personreward.Orderwho;
            personrewardContext.Orderwhoid = personreward.Orderwhoid;
            personrewardContext.Orderid = personreward.Orderid;
            personrewardContext.Rewarddate = personreward.Rewarddate;
            personrewardContext.Optionnumber1 = personreward.Optionnumber1;
            personrewardContext.Optionnumber2 = personreward.Optionnumber2;
            personrewardContext.Optionstring1 = personreward.Optionstring1;
            personrewardContext.Optionstring2 = personreward.Optionstring2;
            personrewardContext.Area = personreward.Area;
            personrewardContext.Areaother = personreward.Areaother;
            personrewardContext.Externalordertype = personreward.Externalordertype;
            personrewardContext.Externalorderwhotype = personreward.Externalorderwhotype;

            SaveChanges();
            UpdatePersonrewardsLocal();
        }

        public void DeletePersonReward(User user, Personreward personreward)
        {
            int minusid = -personreward.Id;
            Personreward personrewardContext = context.Personreward.FirstOrDefault(p => p.Id == minusid);
            if (personrewardContext == null)
            {
                return;
            }
            context.Personreward.Remove(personrewardContext);

            SaveChanges();
            UpdatePersonrewardsLocal();
        }

        public void AddPersonIll(User user, Personill personill)
        {
            context.Personill.Add(personill);
            SaveChanges();
        }

        public void ChangePersonIll(User user, Personill personill)
        {
            Personill personillContext = context.Personill.FirstOrDefault(p => p.Id == personill.Id);
            if (personillContext == null)
            {
                return;
            }
            personillContext.Illtype = personill.Illtype;
            personillContext.Illcode = personill.Illcode;
            personillContext.Datestart = personill.Datestart;
            personillContext.Dateend = personill.Dateend;
            personillContext.Illregime = personill.Illregime;
            personillContext.Illwho = personill.Illwho;
            personillContext.Illtype = personill.Illtype;
            personillContext.Privelege = personill.Privelege;
            SaveChanges();
        }

        public void DeletePersonIll(User user, Personill personill)
        {
            int minusid = -personill.Id;
            Personill personillContext = context.Personill.FirstOrDefault(p => p.Id == minusid);
            if (personillContext == null)
            {
                return;
            }
            context.Personill.Remove(personillContext);

            SaveChanges();
        }

        public void AddPersonEducation(User user, Personeducation personeducation)
        {
            List<Educationtypeblock> educationtypeblocks = new List<Educationtypeblock>(personeducation.Educationtypeblocks);
            List<Academicvacation> academicvacations = new List<Academicvacation>(personeducation.Academicvacation);
            List<Educationmaternity> educationmaternities = new List<Educationmaternity>(personeducation.Educationmaternities);
            context.Personeducation.Add(personeducation);


            SaveChanges();
            foreach (Educationtypeblock educationtypeblock in educationtypeblocks)
            {
                //educationtypeblock.Personeducation = personeducation.Id;
                //List<Educationperiod> educationperiods = new List<Educationperiod>(educationtypeblock.Educationperiods);
                //context.Educationtypeblock.Add(educationtypeblock);

                Educationtypeblock newEducationtypeblock = new Educationtypeblock();
                newEducationtypeblock.Personeducation = personeducation.Id;
                newEducationtypeblock.Educationtype = educationtypeblock.Educationtype;
                List<Educationperiod> educationperiods = new List<Educationperiod>(educationtypeblock.Educationperiods);
                context.Educationtypeblock.Add(newEducationtypeblock);
                SaveChanges();
                foreach(var educationperiod in educationperiods)
                {
                    //educationperiod.Educationtypeblock = educationtypeblock.Id;
                    //context.Educationperiod.Add(educationperiod);

                    Educationperiod newEducationperiod = new Educationperiod();
                    newEducationperiod.Educationtypeblock = newEducationtypeblock.Id;
                    newEducationperiod.Service = educationperiod.Service;
                    newEducationperiod.Start = educationperiod.Start;
                    newEducationperiod.End = educationperiod.End;
                    newEducationperiod.Educationpositiontype = educationperiod.Educationpositiontype;
                    newEducationperiod.Rank = educationperiod.Rank;
                    newEducationperiod.Ranktype = educationperiod.Ranktype;
                    newEducationperiod.Platoon = educationperiod.Platoon;
                    newEducationperiod.Course = educationperiod.Course;
                    newEducationperiod.Orderdate = educationperiod.Orderdate;
                    newEducationperiod.Ordernum = educationperiod.Ordernum;
                    newEducationperiod.Ordernumbertype = educationperiod.Ordernumbertype;
                    context.Educationperiod.Add(newEducationperiod);
                }
                SaveChanges();
            }

            // Записываем информацию об академ отпусках только если стоит соответствующая галочка
            if (personeducation.Academicvacation > 0)
            {
                foreach (var academicvacation in academicvacations)
                {
                    Academicvacation newAcademicvacation = new Academicvacation();
                    newAcademicvacation.Personeducation = personeducation.Id;
                    newAcademicvacation.Start = academicvacation.Start;
                    newAcademicvacation.End = academicvacation.End;
                    newAcademicvacation.Orderdate = academicvacation.Orderdate;
                    newAcademicvacation.Ordernumber = academicvacation.Ordernumber;
                    newAcademicvacation.Ordernumbertype = academicvacation.Ordernumbertype;
                    newAcademicvacation.Orderwho = academicvacation.Orderwho;
                    context.Academicvacation.Add(newAcademicvacation);
                }
                SaveChanges();
            }

            // Записываем информацию о декрете только если стоит соответствующая галочка
            if (personeducation.Maternityvacation > 0)
            {
                foreach (var educationmaternity in educationmaternities)
                {
                    Educationmaternity newEducationmaternity = new Educationmaternity();
                    newEducationmaternity.Personeducation = personeducation.Id;
                    newEducationmaternity.Start = educationmaternity.Start;
                    newEducationmaternity.End = educationmaternity.End;
                    newEducationmaternity.Orderdate = educationmaternity.Orderdate;
                    newEducationmaternity.Ordernumber = educationmaternity.Ordernumber;
                    newEducationmaternity.Ordernumbertype = educationmaternity.Ordernumbertype;
                    newEducationmaternity.Orderwho = educationmaternity.Orderwho;
                    context.Educationmaternity.Add(newEducationmaternity);
                }
                SaveChanges();
            }
            

        }

        public void ChangePersonEducation(User user, Personeducation personeducation)
        {
            Personeducation personeducationContext = context.Personeducation.FirstOrDefault(p => p.Id == personeducation.Id);
            if (personeducationContext == null)
            {
                return;
            }
            personeducationContext.Main = personeducation.Main;
            personeducationContext.Educationlevel = personeducation.Educationlevel;
            personeducationContext.Educationstage = personeducation.Educationstage;
            personeducationContext.Name = personeducation.Name;
            personeducationContext.Name2 = personeducation.Name2;
            personeducationContext.Location = personeducation.Location;
            personeducationContext.City = personeducation.City;
            personeducationContext.Faculty = personeducation.Faculty;
            personeducationContext.Educationtype = personeducation.Educationtype;
            personeducationContext.Datestart = personeducation.Datestart;
            personeducationContext.Dateend = personeducation.Dateend;
            personeducationContext.Speciality = personeducation.Speciality;
            personeducationContext.Documentseries = personeducation.Documentseries;
            personeducationContext.Documentnumber = personeducation.Documentnumber;
            personeducationContext.Cadet = personeducation.Cadet;
            personeducationContext.Qualification = personeducation.Qualification;
            personeducationContext.Start = personeducation.Start;
            personeducationContext.End = personeducation.End;
            personeducationContext.Interrupted = personeducation.Interrupted;
            personeducationContext.Interruptorderdate = personeducation.Interruptorderdate;
            personeducationContext.Interruptorderwho = personeducation.Interruptorderwho;
            personeducationContext.Interruptordernumber = personeducation.Interruptordernumber;
            personeducationContext.Interruptordernumbertype = personeducation.Interruptordernumbertype;
            personeducationContext.Interruptorderreason = personeducation.Interruptorderreason;
            personeducationContext.Educationdocument = personeducation.Educationdocument;
            personeducationContext.Ordernumber = personeducation.Ordernumber;
            personeducationContext.Ordernumbertype = personeducation.Ordernumbertype;
            personeducationContext.Orderdate = personeducation.Orderdate;
            personeducationContext.Orderwho = personeducation.Orderwho;
            personeducationContext.Orderwhoid = personeducation.Orderwhoid;
            personeducationContext.Orderid = personeducation.Orderid;
            personeducationContext.Nameasjobfull = personeducation.Nameasjobfull;
            personeducationContext.Nameasjobplace = personeducation.Nameasjobplace;
            personeducationContext.Nameasjobposition = personeducation.Nameasjobposition;
            personeducationContext.Educationadditionaltype = personeducation.Educationadditionaltype;
            personeducationContext.Ucp = personeducation.Ucp;
            personeducationContext.Academicvacation = personeducation.Academicvacation;
            personeducationContext.Maternityvacation = personeducation.Maternityvacation;
            personeducationContext.Rating = personeducation.Rating;
            personeducationContext.State = personeducation.State;
            personeducationContext.Citytype = personeducation.Citytype;

            SaveChanges();

            IEnumerable<Educationtypeblock> educationtypeblocks = context.Educationtypeblock.Where(e => e.Personeducation == personeducationContext.Id);
            foreach (var educationtypeblock in educationtypeblocks)
            {
                IEnumerable<Educationperiod> educationperiods = context.Educationperiod.Where(e => e.Educationtypeblock == educationtypeblock.Id);
                context.Educationtypeblock.Remove(educationtypeblock);
                context.Educationperiod.RemoveRange(educationperiods);
            }
            SaveChanges();

            
            IEnumerable<Academicvacation> academicvacations = context.Academicvacation.Where(e => e.Personeducation == personeducationContext.Id);
            foreach (var academicvacation in academicvacations)
            {
                context.Academicvacation.Remove(academicvacation);
            }
            SaveChanges();
            
            

            IEnumerable<Educationmaternity> educationmaternities = context.Educationmaternity.Where(e => e.Personeducation == personeducationContext.Id);
            foreach (var educationmaternity in educationmaternities)
            {
                context.Educationmaternity.Remove(educationmaternity);
            }
            SaveChanges();

            

            educationtypeblocks = new List<Educationtypeblock>(personeducation.Educationtypeblocks);
            academicvacations = new List<Academicvacation>(personeducation.Academicvacations);
            educationmaternities = new List<Educationmaternity>(personeducation.Educationmaternities);

            foreach (Educationtypeblock educationtypeblock in educationtypeblocks)
            {
                Educationtypeblock newEducationtypeblock = new Educationtypeblock();
                newEducationtypeblock.Personeducation = personeducation.Id;
                newEducationtypeblock.Educationtype = educationtypeblock.Educationtype;
                List<Educationperiod> educationperiods = new List<Educationperiod>(educationtypeblock.Educationperiods);
                context.Educationtypeblock.Add(newEducationtypeblock);
                SaveChanges();
                foreach (var educationperiod in educationperiods)
                {
                    Educationperiod newEducationperiod = new Educationperiod();
                    newEducationperiod.Educationtypeblock = newEducationtypeblock.Id;
                    newEducationperiod.Service = educationperiod.Service;
                    newEducationperiod.Start = educationperiod.Start;
                    newEducationperiod.End = educationperiod.End;
                    newEducationperiod.Educationpositiontype = educationperiod.Educationpositiontype;
                    newEducationperiod.Rank = educationperiod.Rank;
                    newEducationperiod.Ranktype = educationperiod.Ranktype;
                    newEducationperiod.Platoon = educationperiod.Platoon;
                    newEducationperiod.Course = educationperiod.Course;
                    newEducationperiod.Orderdate = educationperiod.Orderdate;
                    newEducationperiod.Ordernum = educationperiod.Ordernum;
                    newEducationperiod.Ordernumbertype = educationperiod.Ordernumbertype;
                    context.Educationperiod.Add(newEducationperiod);
                }
                SaveChanges();
            }

            // Записываем информацию об академ отпусках только если стоит соответствующая галочка
            if (personeducation.Academicvacation > 0)
            {
                foreach (var academicvacation in academicvacations)
                {
                    Academicvacation newAcademicvacation = new Academicvacation();
                    newAcademicvacation.Personeducation = personeducation.Id;
                    newAcademicvacation.Start = academicvacation.Start;
                    newAcademicvacation.End = academicvacation.End;
                    newAcademicvacation.Orderdate = academicvacation.Orderdate;
                    newAcademicvacation.Ordernumber = academicvacation.Ordernumber;
                    newAcademicvacation.Ordernumbertype = academicvacation.Ordernumbertype;
                    newAcademicvacation.Orderwho = academicvacation.Orderwho;
                    context.Academicvacation.Add(newAcademicvacation);
                }
                SaveChanges();
            }
            
            // Записываем информацию о декрете только если стоит соответствующая галочка
            if (personeducation.Maternityvacation > 0)
            {
                foreach (var educationmaternity in educationmaternities)
                {
                    Educationmaternity newEducationmaternity = new Educationmaternity();
                    newEducationmaternity.Personeducation = personeducation.Id;
                    newEducationmaternity.Start = educationmaternity.Start;
                    newEducationmaternity.End = educationmaternity.End;
                    newEducationmaternity.Orderdate = educationmaternity.Orderdate;
                    newEducationmaternity.Ordernumber = educationmaternity.Ordernumber;
                    newEducationmaternity.Ordernumbertype = educationmaternity.Ordernumbertype;
                    newEducationmaternity.Orderwho = educationmaternity.Orderwho;
                    context.Educationmaternity.Add(newEducationmaternity);
                }
                SaveChanges();
            }
            
        }

        public void DeletePersonEducation(User user, Personeducation personeducation)
        {
            int minusid = -personeducation.Id;
            Personeducation personeducationContext = context.Personeducation.FirstOrDefault(p => p.Id == minusid);
            if (personeducationContext == null)
            {
                return;
            }
            context.Personeducation.Remove(personeducationContext);

            SaveChanges();
        }

        public void AddPersonPhysical(User user, PersonphysicalManager personphysical)
        {
            Personphysical personphysicalOriginal = new Personphysical();
            personphysicalOriginal.Person = personphysical.Person;
            personphysicalOriginal.Physicaldate = personphysical.Physicaldate;

            context.Personphysical.Add(personphysicalOriginal);
            
            SaveChanges();

            IEnumerable<Physicalfield> physicalfields = personphysical.Physicalfields;
            foreach (Physicalfield physicalfield in physicalfields)
            {
                physicalfield.Personphysical = personphysicalOriginal.Id;
            }
            context.Physicalfield.AddRange(physicalfields);
            SaveChanges();
        }

        public void ChangePersonPhysical(User user, PersonphysicalManager personphysical)
        {
            Personphysical personphysicalContext = context.Personphysical.FirstOrDefault(p => p.Id == personphysical.Id);
            if (personphysicalContext == null)
            {
                return;
            }
            personphysicalContext.Physicaldate = personphysical.Physicaldate;

            IEnumerable<Physicalfield> oldPhysicalfields = context.Physicalfield.Where(r => r.Personphysical == personphysical.Id);
            context.Physicalfield.RemoveRange(oldPhysicalfields);
            SaveChanges();

            foreach (Physicalfield physicalfield in personphysical.Physicalfields)
            {
                physicalfield.Personphysical = personphysical.Id;
            }

            context.Physicalfield.AddRange(personphysical.Physicalfields);

            SaveChanges();
        }

        public void DeletePersonPhysical(User user, PersonphysicalManager personphysical)
        {
            int minusid = -personphysical.Id;
            Personphysical personphysicalContext = context.Personphysical.FirstOrDefault(p => p.Id == minusid);
            if (personphysicalContext == null)
            {
                return;
            }
            context.Personphysical.Remove(personphysicalContext);

            SaveChanges();
        }

        public void AddPersonDriver(User user, Persondriver persondriver)
        {
            context.Persondriver.Add(persondriver);
            SaveChanges();
        }

        public void ChangePersonDriver(User user, Persondriver persondriver)
        {
            Persondriver persondriverContext = context.Persondriver.FirstOrDefault(p => p.Id == persondriver.Id);
            if (persondriverContext == null)
            {
                return;
            }
            persondriverContext.Drivertype = persondriver.Drivertype;
            persondriverContext.Series = persondriver.Series;
            persondriverContext.Number = persondriver.Number;
            persondriverContext.Datestart = persondriver.Datestart;
            persondriverContext.Dateend = persondriver.Dateend;
            persondriverContext.Drivercategory = persondriver.Drivercategory;
            SaveChanges();
        }

        public void DeletePersonDriver(User user, Persondriver persondriver)
        {
            int minusid = -persondriver.Id;
            Persondriver persondriverContext = context.Persondriver.FirstOrDefault(p => p.Id == minusid);
            if (persondriverContext == null)
            {
                return;
            }
            context.Persondriver.Remove(persondriverContext);

            SaveChanges();
        }

        public void AddPersonPermission(User user, Personpermission personpermission)
        {
            context.Personpermission.Add(personpermission);
            SaveChanges();
        }

        public void ChangePersonPermission(User user, Personpermission personpermission)
        {
            Personpermission personpermissionContext = context.Personpermission.FirstOrDefault(p => p.Id == personpermission.Id);
            if (personpermissionContext == null)
            {
                return;
            }
            personpermissionContext.Permissiontype = personpermission.Permissiontype;
            personpermissionContext.Number = personpermission.Number;
            personpermissionContext.Datestart = personpermission.Datestart;
            personpermissionContext.Dateend = personpermission.Dateend;
            SaveChanges();
        }

        public void DeletePersonPermission(User user, Personpermission personpermission)
        {
            int minusid = -personpermission.Id;
            Personpermission personpermissionContext = context.Personpermission.FirstOrDefault(p => p.Id == minusid);
            if (personpermissionContext == null)
            {
                return;
            }
            context.Personpermission.Remove(personpermissionContext);

            SaveChanges();
        }

        public void AddPersonPrivelege(User user, Personprivelege personprivelege)
        {
            context.Personprivelege.Add(personprivelege);
            SaveChanges();
        }

        public void ChangePersonPrivelege(User user, Personprivelege personprivelege)
        {
            Personprivelege personprivelegeContext = context.Personprivelege.FirstOrDefault(p => p.Id == personprivelege.Id);
            if (personprivelegeContext == null)
            {
                return;
            }
            personprivelegeContext.Name = personprivelege.Name;
            SaveChanges();
        }

        public void DeletePersonPrivelege(User user, Personprivelege personprivelege)
        {
            int minusid = -personprivelege.Id;
            Personprivelege personprivelegeContext = context.Personprivelege.FirstOrDefault(p => p.Id == minusid);
            if (personprivelegeContext == null)
            {
                return;
            }
            context.Personprivelege.Remove(personprivelegeContext);

            SaveChanges();
        }

        public void AddPersonDispanserization(User user, Persondispanserization persondispanserization)
        {
            context.Persondispanserization.Add(persondispanserization);
            SaveChanges();
        }

        public void ChangePersonDispanserization(User user, Persondispanserization persondispanserization)
        {
            Persondispanserization persondispanserizationContext = context.Persondispanserization.FirstOrDefault(p => p.Id == persondispanserization.Id);
            if (persondispanserizationContext == null)
            {
                return;
            }
            persondispanserizationContext.Group = persondispanserization.Group;
            persondispanserizationContext.Result = persondispanserization.Result;
            persondispanserizationContext.Date = persondispanserization.Date;
            SaveChanges();
        }

        public void DeletePersonDispanserization(User user, Persondispanserization persondispanserization)
        {
            int minusid = -persondispanserization.Id;
            Persondispanserization persondispanserizationContext = context.Persondispanserization.FirstOrDefault(p => p.Id == minusid);
            if (persondispanserizationContext == null)
            {
                return;
            }
            context.Persondispanserization.Remove(persondispanserizationContext);

            SaveChanges();
        }

        public void AddPersonVvk(User user, Personvvk personvvk)
        {
            context.Personvvk.Add(personvvk);
            SaveChanges();
        }

        public void ChangePersonVvk(User user, Personvvk personvvk)
        {
            Personvvk personvvkContext = context.Personvvk.FirstOrDefault(p => p.Id == personvvk.Id);
            if (personvvkContext == null)
            {
                return;
            }
            personvvkContext.Number = personvvk.Number;
            personvvkContext.Result = personvvk.Result;
            personvvkContext.Date = personvvk.Date;
            SaveChanges();
        }

        public void DeletePersonVvk(User user, Personvvk personvvk)
        {
            int minusid = -personvvk.Id;
            Personvvk personvvkContext = context.Personvvk.FirstOrDefault(p => p.Id == minusid);
            if (personvvkContext == null)
            {
                return;
            }
            context.Personvvk.Remove(personvvkContext);

            SaveChanges();
        }

        public void AddPersonJobprivelege(User user, Personjobprivelege personjobprivelege)
        {
            if (personjobprivelege.Start == null || personjobprivelege.End == null)
            {
                return; // Нам нужна дата начала и конца
            }
            //context.Personjobprivelege.Add(personjobprivelege); // вместо стандартной операции добавления, мы из всего периода вычеркиваем отпуска

            int year = personjobprivelege.Start.GetValueOrDefault().Year; // получаем год
            List<Personvacation> vacations = new List<Personvacation>(); // Сюда будут входить все отпуска в указанном году
            foreach (Personvacation vacation in Personvacations.Where(p => p.Person == personjobprivelege.Person))
            {
                if (vacation.Allowstart.Year == year && vacation.Compensationdays < vacation.Duration)
                {
                    vacations.Add(vacation); // добавляем подходящий отпуск в список.
                }
            }

            List<Personill> ills = new List<Personill>(); // Из льготной службы также исключаются стационарные больничные
            foreach (Personill ill in Personills.Where(p => p.Person == personjobprivelege.Person))
            {
                if (ill.Datestart.Year == year || ill.Dateend.Year == year)
                {
                    if (ill.Illtype == 0 || (ill.Illtype == 1 && ill.Illregime == 1)) // Только если работник вышел за близким или если работник лег на стационар
                    {
                        ills.Add(ill);
                    }
                }
            }

            List<Personjobprivelege> personjobpriveleges = SplitPersonjobprivelege(personjobprivelege, vacations, ills); // здесь мы будем вносить все раздробленные льготные периоды.
            foreach (Personjobprivelege splittedPersonjobprivelege in personjobpriveleges)
            {
                context.Personjobprivelege.Add(splittedPersonjobprivelege);
            }

            SaveChanges();
        }

        /// <summary>
        /// Дробит льготный коэффициент с учетом всех вставок из отпусков и в дальнейшем больничных в нем.
        /// </summary>
        /// <param name="personjobprivelege"></param>
        /// <param name="personvacation"></param>
        /// <returns></returns>
        public List<Personjobprivelege> SplitPersonjobprivelege(Personjobprivelege personjobprivelege, List<Personvacation> vacations, List<Personill> ills)
        {
            List<Personjobprivelege> personjobpriveleges = new List<Personjobprivelege>(); // - Сюда будут входить все раздробленые периоды.
            DateTime start = personjobprivelege.Start.GetValueOrDefault(); // - C какого периода
            DateTime end = personjobprivelege.End.GetValueOrDefault(); //- По какой период

            List<KeyValue<DateTime, DateTime>> datesPriveleges = new List<KeyValue<DateTime, DateTime>>(); // Даты, в которых останутся 
            List<KeyValue<DateTime, DateTime>> dates = new List<KeyValue<DateTime, DateTime>>(); // Даты отпусков, больничных и т.п.
            //vacations = vacations.OrderBy(v => v.Date).ToList(); // Сортируем даты, чтобы двиг
            foreach (Personill ill in ills)
            {
                dates.Add(new KeyValue<DateTime, DateTime>(ill.Datestart, ill.Dateend));
            }

            foreach (Personvacation vacation in vacations)
            {
                if (vacation.Date != null)
                {
                    DateTime dateEnd = vacation.Date.GetValueOrDefault().AddDays(vacation.Duration + vacation.Trip);

                    dates.Add(new KeyValue<DateTime, DateTime>(vacation.Date.GetValueOrDefault(), dateEnd)); // Добавляем даты начала и окончания отпуска
                }
                
            }

            

            dates = dates.OrderBy(d => d.Key).ToList(); // Сортируем даты, чтобы двигались слево направо.

            foreach (KeyValue<DateTime, DateTime> date in dates)
            {
                if (date.Key < start && date.Value < start)
                {
                    continue; // отпуска и больничные до начала льготной выслуги пропускаем.
                }
                if (date.Key < start)
                {
                    start = date.Value; // мы сдвигаем текущую точку до окончания отпуска и больничного.
                    continue;
                }
                if (start > end) // Если мы зашли за окончания периода для льготной выслуги, мы приостанавливаемся.
                {
                    break;
                }
                datesPriveleges.Add(new KeyValue<DateTime, DateTime>(start, date.Key.AddDays(-1))); // Мы вносим период между текущей точкой и ближайшим началом отпуска/больничного.
                start = date.Value.AddDays(1); // Смещаем текущую точку на конец отпуска/больничного.
            }

            if (start <= end)
            {
                datesPriveleges.Add(new KeyValue<DateTime, DateTime>(start, end)); // Если текущая точка еще не обошла конечную, устанавливает между ними отрезок.
            }

            foreach(KeyValue<DateTime, DateTime> date in datesPriveleges)
            {
                Personjobprivelege splittedJobprivelege = new Personjobprivelege();
                splittedJobprivelege.Start = date.Key;
                splittedJobprivelege.End = date.Value;
                splittedJobprivelege.Coef = personjobprivelege.Coef;
                splittedJobprivelege.Prooftype = personjobprivelege.Prooftype;
                splittedJobprivelege.Proofdate = personjobprivelege.Proofdate;
                splittedJobprivelege.Proofnumber = personjobprivelege.Proofnumber;
                splittedJobprivelege.Prooftext = personjobprivelege.Prooftext;
                splittedJobprivelege.Documentdate = personjobprivelege.Documentdate;
                splittedJobprivelege.Documentnumber = personjobprivelege.Documentnumber;
                splittedJobprivelege.Documentorder = personjobprivelege.Documentorder;
                splittedJobprivelege.Person = personjobprivelege.Person;

                personjobpriveleges.Add(splittedJobprivelege);
            }

            return personjobpriveleges;
        }

        public void ChangePersonJobprivelege(User user, Personjobprivelege personjobprivelege)
        {
            Personjobprivelege personjobprivelegeContext = context.Personjobprivelege.FirstOrDefault(p => p.Id == personjobprivelege.Id);
            if (personjobprivelegeContext == null)
            {
                return;
            }
            personjobprivelegeContext.Start = personjobprivelege.Start;
            personjobprivelegeContext.End = personjobprivelege.End;
            personjobprivelegeContext.Coef = personjobprivelege.Coef;
            personjobprivelegeContext.Prooftype = personjobprivelege.Prooftype;
            personjobprivelegeContext.Proofdate = personjobprivelege.Proofdate;
            personjobprivelegeContext.Proofnumber = personjobprivelege.Proofnumber;
            personjobprivelegeContext.Prooftext = personjobprivelege.Prooftext;
            personjobprivelegeContext.Documentorder = personjobprivelege.Documentorder;
            personjobprivelegeContext.Documentdate = personjobprivelege.Documentdate;
            personjobprivelegeContext.Documentnumber = personjobprivelege.Documentnumber;

            SaveChanges();
        }

        public void DeletePersonJobprivelege(User user, Personjobprivelege personjobprivelege)
        {
            int minusid = -personjobprivelege.Id;
            Personjobprivelege personjobprivelegeContext = context.Personjobprivelege.FirstOrDefault(p => p.Id == minusid);
            if (personjobprivelegeContext == null)
            {
                return;
            }
            context.Personjobprivelege.Remove(personjobprivelegeContext);

            SaveChanges();
        }

        /////////////////////////////////////////
        ////////////////////////////////////////   Кандидаты
        ////////////////////////////////////////
        // Возвращает статус: 0 - успешно, 1 - такой юзер уже существует, 2 - юзер уже существует, но забракован, 3 - не вся информация введена
        public int AddCabinet(User user, CabinetdataManager cabinetdataManager)
        {
            Cabinetdata exists = Cabinetdatas.FirstOrDefault(c => c.Userind.Equals(cabinetdataManager.Userind));
            if (exists != null)
            {
                if (exists.Status == 2)
                {
                    return 2;
                } else
                {
                    return 1;
                }
                
            }

            Cabinetdata cabinetdata = new Cabinetdata();
            cabinetdata.Usersurname = cabinetdataManager.Usersurname;
            cabinetdata.Username = cabinetdataManager.Username;
            cabinetdata.Userpatronymic = cabinetdataManager.Userpatronymic;
            cabinetdata.Userind = cabinetdataManager.Userind;
            cabinetdata.Employeesid = 0; // Если 0, то не сотрудник
            cabinetdata.Reasonid = cabinetdataManager.Reasonid;

            cabinetdata.Creatorid = user.Id;
            cabinetdata.Accesscode = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + CreatePassword(10);
            context.Cabinetdata.Add(cabinetdata);

            Profiledata profiledata = new Profiledata();
            profiledata.Profilesurname = cabinetdataManager.Usersurname;
            profiledata.Profilename = cabinetdataManager.Username;
            profiledata.Profilepatronymic = cabinetdataManager.Userpatronymic;
            profiledata.Profilepassportind = cabinetdataManager.Userind;
            context.Profiledata.Add(profiledata);

            Sheetdata sheetdata = new Sheetdata();
            sheetdata.Sheetsurname = cabinetdataManager.Usersurname;
            sheetdata.Sheetname = cabinetdataManager.Username;
            sheetdata.Sheetpatronymic = cabinetdataManager.Userpatronymic;
            sheetdata.Sheetpassportind = cabinetdataManager.Userind;
            context.Sheetdata.Add(sheetdata);

            Declarationdata declarationdata = new Declarationdata();
            declarationdata.Declarationsurname = cabinetdataManager.Usersurname;
            declarationdata.Declarationname = cabinetdataManager.Username;
            declarationdata.Declarationpatronymic = cabinetdataManager.Userpatronymic;
            declarationdata.Declarationind = cabinetdataManager.Userind;
            declarationdata.Declarationtypeform = cabinetdataManager.Declarationid;
            context.Declarationdata.Add(declarationdata);


            SaveChanges();
            //cabinetdata.Status 0 - активен, 1 - прошел, 2 - забракован
            // cabinetdata.Employeesid = cabinetdata.Id;
            profiledata.Cabinetid = cabinetdata.Id;
            sheetdata.Cabinetid = cabinetdata.Id;
            declarationdata.Cabinetid = cabinetdata.Id;

            Autobiographydata autobiographydata = new Autobiographydata();
            autobiographydata.Cabinetid = cabinetdata.Id;
            context.Autobiographydata.Add(autobiographydata);

            SaveChanges();
            //UpdatePersonsLocal();
            return 0;
        }

        public void DenyCabinet(User user, CabinetdataManager cabinetdataManager)
        {
            Cabinetdata cabinetdata = context.Cabinetdata.FirstOrDefault(c => c.Id == cabinetdataManager.Id);
            if (cabinetdata == null)
            {
                return;
            }

            cabinetdata.Status = 2; // После этого считается забракованным.
            cabinetdata.Denyreason = cabinetdataManager.Denyreason;
            SaveChanges();
        }

        public string CreatePassword(int length)
        {
            //const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            const string valid = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public CabinetdataManager GetCabinetdataManager(User user, int cabinetdataid, bool includedeny = false)
        {
            return GetCabinetdataManager(user, Cabinetdatas.FirstOrDefault(c => c.Id == cabinetdataid), includedeny);
        }

        public CabinetdataManager GetCabinetdataManagerByIdent(User user, string ident, bool includedeny = false)
        {
            return GetCabinetdataManager(user, Cabinetdatas.FirstOrDefault(c => c.Userind == ident), includedeny);
        }

        public CabinetdataManager GetCabinetdataManager(User user, Cabinetdata cabinetdata, bool includedeny = false)
        {
            DateTime date = user.Date.GetValueOrDefault();
            CabinetdataManager cabinetdataManager = new CabinetdataManager(cabinetdata);
           if (cabinetdata.Status == 2 && !includedeny)
           {
                return null; // Статус 2 означает, что кандидат забракован, поэтому в общем списке не отображается.
           }

            //IEnumerable<Personcontract> contracts = Personcontracts.Where(p => p.Person == personManager.Id);
            //personManager.Personcontracts = contracts.ToArray();

            IEnumerable<Autobiographydata> autobiographydataList = Autobiographydatas.Where(e => e.Cabinetid == cabinetdataManager.Id);
            cabinetdataManager.Autobiographydatalist = autobiographydataList.ToArray();

            IEnumerable<Declarationdata> declarationdatalist = Declarationdatas.Where(e => e.Cabinetid == cabinetdataManager.Id);
            cabinetdataManager.Declarationdatalist = declarationdatalist.ToArray();

            IEnumerable<Declarationrelative> declarationrelativelist = Declarationrelatives.Where(e => e.Cabinetid == cabinetdataManager.Id);
            cabinetdataManager.Declarationrelativelist = declarationrelativelist.ToArray();

            //IEnumerable<Declarationtabledata> declarationtabledatalist = Declarationtabledatas.Where(e => e.Cabinetid == cabinetdataManager.Id);
            //cabinetdataManager.Declarationtabledatalist = declarationtabledatalist.ToArray();
            List<Declarationtabledata> declarationtabledatalist = new List<Declarationtabledata>();

            foreach (Declarationdata declarationdata in cabinetdataManager.Declarationdatalist)
            {
                IEnumerable<Declarationtabledata> declarationtables = Declarationtabledatas.Where(e => e.Cabinetid == declarationdata.Id);
                declarationtabledatalist.AddRange(declarationtables);
            }
            cabinetdataManager.Declarationtabledatalist = declarationtabledatalist.ToArray();

            IEnumerable<Profiledata> profiledatalist = Profiledatas.Where(e => e.Cabinetid == cabinetdataManager.Id);
            cabinetdataManager.Profiledatalist = profiledatalist.ToArray();

            IEnumerable<Profilerelatives> profilerelativeslist = Profilerelatives.Where(e => e.Cabinetid == cabinetdataManager.Id);
            cabinetdataManager.Profilerelativeslist = profilerelativeslist.ToArray();

            IEnumerable<Pseducation> pseducationlist = Pseducations.Where(e => e.Cabinetid == cabinetdataManager.Id);
            cabinetdataManager.Pseducationlist = pseducationlist.ToArray();

            IEnumerable<Pswork> psworklist = Psworks.Where(e => e.Cabinetid == cabinetdataManager.Id);
            cabinetdataManager.Psworklist = psworklist.ToArray();

            IEnumerable<Sheetdata> sheetdatalist = Sheetdatas.Where(e => e.Cabinetid == cabinetdataManager.Id);
            cabinetdataManager.Sheetdatalist = sheetdatalist.ToArray();

            IEnumerable<Sheetpolitics> sheetpoliticslist = Sheetpolitics.Where(e => e.Cabinetid == cabinetdataManager.Id);
            cabinetdataManager.Sheetpoliticslist = sheetpoliticslist.ToArray();

            

            cabinetdataManager.UserCompact = new UserCompact();
            User creator = Users.FirstOrDefault(u => u.Id == cabinetdataManager.Creatorid);
            if (user.Structure != null && creator != null && creator.Structure > 0)
            {
                Structure structure = GetActualStructureInfo(creator.Structure.GetValueOrDefault(), date);
                if (structure != null)
                {
                    cabinetdataManager.UserCompact.Structure = structure.Name;
                    cabinetdataManager.UserCompact.StructureTree = FormTree(structure, true, date);
                }
            }
            
            //cabinetdataManager.UserCompact.Structure =  user.Structure.GetValueOrDefault();

            return cabinetdataManager;
        }

        public List<CabinetdataManager> GetCabinetdatas(User user, string fio)
        {
            if (fio == null)
            {
                return new List<CabinetdataManager>();
            }
            string[] fioSplitted = fio.Split(" ");
            string surname = fioSplitted.First();
            string name = "";
            if (fioSplitted.Length > 1)
            {
                name = fioSplitted[1];
            }
            string fathername = "";
            if (fioSplitted.Length > 2)
            {
                fathername = fioSplitted[2];
            }

            List<CabinetdataManager> cabinets = new List<CabinetdataManager>();

            if (name.Length > 0 && fathername.Length > 0)
            { // Заменить потом на локальную версию как кабинетов станет больше.
                cabinets.AddRange(CabinetdataManager.CabinetsToCabinetManagers(this, user, Cabinetdatas.Where(p => p.Userpatronymic.Contains(fathername) && p.Username.Contains(name) && p.Usersurname.Contains(surname))));
            }
            else if (name.Length > 0)
            {
                cabinets.AddRange(CabinetdataManager.CabinetsToCabinetManagers(this, user, Cabinetdatas.Where(p => p.Username.Contains(name) && p.Usersurname.Contains(surname))));
            }
            else
            {
                cabinets.AddRange(CabinetdataManager.CabinetsToCabinetManagers(this, user, Cabinetdatas.Where(p => p.Usersurname.Contains(surname))));
            }
            cabinets = cabinets.Distinct().ToList();
       
            

            return cabinets;
        }

        public bool isAllowedToReadCandidate(User user, int candidateID)
        {
            if (user.Admin.GetValueOrDefault() > 0 || user.Masterpersonneleditor.GetValueOrDefault() > 0)
            {
                return true;
            }
            if (user.Personnelread == 0)
            {
                return false;
            }
            Cabinetdata candidate = Cabinetdatas.FirstOrDefault(c => c.Id == candidateID);
            if (candidate == null)
            {
                return false;
            }

            //Person person = PersonsLocal().GetValue(personID);
            //if (person.Structure == 0)
            //{
            //    return true;
            //}

            User creator = Users.FirstOrDefault(u => u.Id == candidate.Creatorid);
            if (creator == null)
            {
                return false;
            }
            if (creator.Structure.GetValueOrDefault() == 0)
            {
                return false;
            }
            return isAllowedToReadStructure(user, creator.Structure.GetValueOrDefault());
        }

        public bool isAllowedToEditCandidate(User user, int candidateID)
        {
            if (user.Admin.GetValueOrDefault() > 0 || user.Masterpersonneleditor.GetValueOrDefault() > 0)
            {
                return true;
            }
            if (user.Personneleditor == 0)
            {
                return false;
            }
            Cabinetdata candidate = Cabinetdatas.FirstOrDefault(c => c.Id == candidateID);
            if (candidate == null)
            {
                return false;
            }

            //Person person = PersonsLocal().GetValue(personID);
            //if (person.Structure == 0)
            //{
            //    return true;
            //}
            User creator = Users.FirstOrDefault(u => u.Id == candidate.Creatorid);
            if (creator == null)
            {
                return false;
            }
            if (creator.Structure.GetValueOrDefault() == 0)
            {
                return false;
            }
            return isAllowedToReadStructure(user, creator.Structure.GetValueOrDefault());
        }

        public void ToggleBiography(User user, CabinetdataManager cabinetdataManager)
        {
            Cabinetdata cabinetdata = context.Cabinetdata.FirstOrDefault(c => c.Id == cabinetdataManager.Id);
            if (cabinetdata == null)
            {
                return;
            }
            Autobiographydata autobiographydata = context.Autobiographydata.FirstOrDefault(a => a.Cabinetid == cabinetdata.Id);
            if (autobiographydata == null)
            {
                return;
            }

            if (autobiographydata.Autobiographylockunlock == 0)
            {
                autobiographydata.Autobiographylockunlock = 1;
                autobiographydata.Autobiographysignature = DateTime.Now;
            } else
            {
                autobiographydata.Autobiographylockunlock = 0;
            }
            SaveChanges();
        }

        public void ToggleProfile(User user, CabinetdataManager cabinetdataManager)
        {
            Cabinetdata cabinetdata = context.Cabinetdata.FirstOrDefault(c => c.Id == cabinetdataManager.Id);
            if (cabinetdata == null)
            {
                return;
            }
            Profiledata profiledata = context.Profiledata.FirstOrDefault(a => a.Cabinetid == cabinetdata.Id);
            if (profiledata == null)
            {
                return;
            }

            if (profiledata.Profilelockunlock == 0)
            {
                profiledata.Profilelockunlock = 1;
                profiledata.Profilesignature = DateTime.Now;
            }
            else
            {
                profiledata.Profilelockunlock = 0;
            }
            SaveChanges();
        }

        public void ToggleSheet(User user, CabinetdataManager cabinetdataManager)
        {
            Cabinetdata cabinetdata = context.Cabinetdata.FirstOrDefault(c => c.Id == cabinetdataManager.Id);
            if (cabinetdata == null)
            {
                return;
            }
            Sheetdata sheetdata = context.Sheetdata.FirstOrDefault(a => a.Cabinetid == cabinetdata.Id);
            if (sheetdata == null)
            {
                return;
            }

            if (sheetdata.Sheetlockunlock == 0)
            {
                sheetdata.Sheetlockunlock = 1;
                sheetdata.Sheetsignature = DateTime.Now;
            }
            else
            {
                sheetdata.Sheetlockunlock = 0;
            }
            SaveChanges();
        }

        public void ToggleDeclaration(User user, CabinetdataManager cabinetdataManager)
        {
            Cabinetdata cabinetdata = context.Cabinetdata.FirstOrDefault(c => c.Id == cabinetdataManager.Id);
            if (cabinetdata == null)
            {
                return;
            }
            Declarationdata declarationdata = context.Declarationdata.FirstOrDefault(a => a.Cabinetid == cabinetdata.Id);
            if (declarationdata == null)
            {
                return;
            }

            if (declarationdata.Declarationlockunlock == 0)
            {
                declarationdata.Declarationlockunlock = 1;
                declarationdata.Declarationsignature = DateTime.Now;
            }
            else
            {
                declarationdata.Declarationlockunlock = 0;
            }
            SaveChanges();
        }

        public CertificateManager GetCertificate(Certificate certificate)
        {
            CertificateManager certificateManager = new CertificateManager();
            certificateManager.Numud = certificate.NumUd.ToString();
            certificateManager.Status = certificate.Status.GetValueOrDefault();
            certificateManager.Certificatecommiteddate = certificate.CertificateCommitedDate.GetValueOrDefault();
            certificateManager.Expirationdate = certificate.ExpirationDate.GetValueOrDefault();
            certificateManager.FullName = certificate.FullName;
            Blankform blankform = Blankforms.FirstOrDefault(b => b.Id == certificate.Blank.GetValueOrDefault());
            if (blankform != null)
            {
                certificateManager.Blank = blankform.Name;
            }

            Agency issuingauthority = Agencies.FirstOrDefault(a => a.Id == certificate.Issuingauthority.GetValueOrDefault());
            if (issuingauthority != null)
            {
                certificateManager.Issuingauthority = issuingauthority.Name;
            }

            Agency agency = Agencies.FirstOrDefault(a => a.Id == certificate.Agency.GetValueOrDefault());
            if (agency != null)
            {
                certificateManager.Agency = agency.Name;
            }

            Post post = Posts.FirstOrDefault(p => p.Id == certificate.Post.GetValueOrDefault());
            if (post != null)
            {
                certificateManager.Post = post.Name;
            }
            

            return certificateManager;
        }

        public IEnumerable<Position> GetAllFreePositionsFromStructure(int id, List<Person> persons, Persondecreeoperation persondecreeoperation)
        {
            IEnumerable<int> positions = new List<int> { };
            persons.ForEach(el =>
            {
                positions = positions.Append(el.Position);
            });
            IEnumerable<int> enumerableId = new List<int> { };
            List<Position> output = GetPositions(id, DateTime.Now).ToList();
            output.ForEach(el => enumerableId = enumerableId.Append(el.Id));
            IEnumerable<int> output_list_id = enumerableId.Except(positions);

            return output.Where(r => GetFreePositionsFromStructure(id, persondecreeoperation, positions, output_list_id, output, enumerableId, persondecreeoperation.Optionstring5).Contains(r.Id));
        }

        public IEnumerable<int> GetFreePositionsFromStructure(int id, Persondecreeoperation persondecreeoperation, IEnumerable<int> positions, IEnumerable<int> output_list_id, List<Position> output, IEnumerable<int> enumerableId, string positionRank)
        {
            IEnumerable<Persondecreeoperation> persondecreeoperations = PersondecreeoperationsLocal().Values.Where(el => el.Persondecreeblock == persondecreeoperation.Persondecreeblock);
            persondecreeoperations.ToList().ForEach(el =>
            {
                positions = positions.Append(el.Optionnumber3);
            });

            enumerableId = new List<int> { };
            output = GetPositions(id, DateTime.Now).ToList();


            List<Position> positions1 = new List<Position> { };

            output.ForEach(el => {
                if (GetPositionRank(el.Positiontype) == positionRank)
                {
                    positions1.Add(el);
                }
            });

            output = positions1;
            output.ForEach(el => enumerableId = enumerableId.Append(el.Id));
            output_list_id = enumerableId.Except(positions);

            return output_list_id;
        }

        public string GetPositionRank(int Positiontype)
        {
            return PositiontypesLocal().Values.FirstOrDefault(el => el.Id == Positiontype).Name;
        }

        public List<Person> GetPersonsForStructure(int id, bool excludeRemoved = true)
        {
            List<Person> persons = new List<Person>();
            if (PersonsLocal() == null)
            {
                UpdatePersonsLocal();
            }
            // Меняем на StartsWith, чтобы в первую очередь искало по фамилиям, потом имени, а уже потом отчеству.
            persons = PersonsLocal().Values.Where(p => p.Structure == id).ToList();
            //persons.AddRange(PersonManager.PersonsToPersonManagers(this, user, PersonsLocal().Values.Where(p => (p.Surname + " " + p.Name + " " + p.Fathername).ToLower().Contains(fioPrepared)), fastSearch));

            if (excludeRemoved)
            {
                persons = persons.Where(p => p.Removed == 0).ToList();
            }
            return persons;
        }

        public IEnumerable<Persondecree> GetPersondecreesActiveOld(User user)
        {
            //return Decrees.Where(d => d.User.GetValueOrDefault() == user.Id).Where(d => d.Declined == 0).Where(d => d.Signed == 0);
            //return DecreesLocal().Values.Where(d => d.Declined == 0).Where(d => d.Signed == 0);

            //return Persondecrees.Where(d => d.Signed == 0);
            return PersondecreesLocal().Values.Where(d => d.Signed == 0 && d.Owner == user.Id); // Проект приказа может быть виден только у одного кадровика
        }

        public IEnumerable<PersondecreeManagement> GetPersondecreesActive(User user)
        {
            List<PersondecreeManagement> persondecreeManagements = new List<PersondecreeManagement>();

            foreach (Persondecree persondecree in PersondecreesLocal().Values.Where(d => d.Signed == 0 && d.Owner == user.Id)) // Проект приказа может быть виден только у одного кадровика
            {
                PersondecreeManagement persondecreeManagement = GetPersondecreeManagement(user, persondecree.Id);
                persondecreeManagements.Add(persondecreeManagement);
            }
            return persondecreeManagements;
        }

        public IEnumerable<PersondecreeManagement> GetPersondecreesFull(User user)
        {
            List<PersondecreeManagement> persondecreeManagements = new List<PersondecreeManagement>();

            foreach (Persondecree persondecree in PersondecreesLocal().Values)//.Values.Where(d => d.Owner == user.Id || d.Creator == user.Id ))
            {
                PersondecreeManagement persondecreeManagement = GetPersondecreeManagement(user, persondecree.Id);
                persondecreeManagements.Add(persondecreeManagement);
            }
            return persondecreeManagements;
        }

        public PersondecreeManagement GetPersondecreeManagement(User user, int id)
        {
            Persondecree persondecree = PersondecreesLocal().GetValue(id);
            if (persondecree == null)
            {
                return null;
            }
            PersondecreeManagement persondecreeManagement = new PersondecreeManagement(persondecree);

            User creator = Users.FirstOrDefault(u => u.Id == persondecreeManagement.Creator);
            if (creator != null)
            {
                UserManager userObject = GetUserManager(user, creator);
                persondecreeManagement.CreatorObject = userObject; // Добавляем в приказ информацию о создателе.
            }
            

            return persondecreeManagement;
        }

        public void AddNewPersondecree(User user, PersondecreeManagement decreeManagement)
        {
            User contextUser = Users.First(u => u.Id == user.Id);
            Persondecree decree = new Persondecree();
            decree.Creator = user.Id;
            decree.Owner = user.Id; 
            decree.Nickname = decreeManagement.Nickname;
            decree.Datesigned = null; // Если нуль, значит, не подписано
            decree.Datecreated = DateTime.Now.Date;
            decree.Mailexplorerid = decreeManagement.Mailexplorerid;
            context.Persondecree.Add(decree);
            context.SaveChanges();

            UpdatePersondecreesLocal();
            //UpdateDateactives(decree.Id);
            //UpdateDecreesLocal();
        }

        public void RemovePersondecree(User user, PersondecreeManagement decreeManagement)
        {
            User contextUser = Users.First(u => u.Id == user.Id);
            Persondecree decree = Persondecrees.First(d => d.Id == decreeManagement.Id);
            IEnumerable<Persondecreeblock> persondecreeblocks = Persondecreeblocks.Where(p => p.Persondecree == decreeManagement.Id);
            foreach(Persondecreeblock contextPersondecreeblock in persondecreeblocks)
            {
                PersondecreeblockManagement persondecreeblockManagement = new PersondecreeblockManagement(contextPersondecreeblock);
                RemovePersonDecreeblock(user, persondecreeblockManagement);
            }

            List<Persondecreeoperation> decreeoperationsToRemove = Persondecreeoperations.Where(d => d.Persondecree == decreeManagement.Id).ToList();
            foreach (Persondecreeoperation decreeoperation in decreeoperationsToRemove)
            {
                Persondecreeoperation decreeoperationManagement = new Persondecreeoperation();
                decreeoperationManagement.Id = decreeoperation.Id;
                RemovePersondecreeOperationWithItsSubject(decreeoperationManagement);
            }

            context.Persondecree.Remove(decree);
            context.SaveChanges();

            UpdatePersondecreesLocal();
            //UpdateDecreesLocal();
        }

        public void RemovePersondecreeOperationWithItsSubject(Persondecreeoperation decreeoperation)
        {
            Persondecreeoperation contextDecreeoperation = Persondecreeoperations.First(d => d.Id == decreeoperation.Id);
            if (contextDecreeoperation.Subjecttype == 1) // 1 - это награды
            {
                /**
                 * Удаляем созданную награду
                 */

                Personreward contextPersonreward = Personrewards.First(p => p.Id == contextDecreeoperation.Subjectid);
                context.Personreward.Remove(contextPersonreward);
            }

            context.Persondecreeoperation.Remove(contextDecreeoperation);
            context.SaveChanges();
            UpdatePersondecreeoperationsLocal();

        }

        public void AcceptPersondecree(PersondecreeManagement decreeManagement, User user)
        {
            Persondecree decree = Persondecrees.FirstOrDefault(d => d.Id == decreeManagement.Id);
            if (decree == null)
            {
                return; // Не нашли проект приказа.
            }

            DateTime date = user.Date.GetValueOrDefault();
            User owner = Users.FirstOrDefault(u => u.Id == decree.Owner);
            Structure structureOwner = GetActualStructureInfo(owner.Structure.GetValueOrDefault(), date); // Получаем название подразделения, подписавшего указ.
            PersonManager person = null;
            //if (creator != null)
            //{
            //    UserManager userObject = GetUserManager(user, creator);
            //    persondecreeManagement.CreatorObject = userObject; // Добавляем в приказ информацию о создателе.
            //}

            decree.Name = decreeManagement.Name;
            decree.Number = decreeManagement.Number;
            decree.Datecreated = decreeManagement.Datecreated;
            decree.Datesigned = decreeManagement.Datesigned; 
            decree.Signed = 1; // считается, что подписано.

            context.SaveChanges();

            // После первого сохранения в базе данных, мы будем вести список, чтобы потом записывать в новые объекты id приказа
            //List<int> personjobsIds = new List<int>();
            //List<int> personjobsFireIds = new List<int>();

            IEnumerable<Persondecreeoperation> decreeoperationsToAccept = PersondecreeoperationsLocal().Values.Where(d => d.Persondecree == decreeManagement.Id && d.Persondecreeblocktype != 21).ToList();
            IEnumerable<Persondecreeoperation> decreeoperationsToAcceptMove = PersondecreeoperationsLocal().Values.Where(d => d.Persondecree == decreeManagement.Id && d.Persondecreeblocktype == 21).ToList();

            if(decreeoperationsToAcceptMove.ToList().Count > 0)
            {
                int n = 0;
                Dictionary<int, NewVzvod> new_vzvods = new Dictionary<int, NewVzvod>();
                foreach (Persondecreeoperation decreeoperation in decreeoperationsToAcceptMove)
                {
                    if(decreeoperation.Optionnumber1 == 18)
                    {
                        bool isExist = true;
                        if (new_vzvods.Count == 0)
                        {
                            new_vzvods.Add(n, new NewVzvod(decreeoperation.Optionnumber7, decreeoperation.Optionnumber6, decreeoperation.Optionstring5) { });
                            n++;
                        }

                        foreach (KeyValuePair<int, NewVzvod> vzvod in new_vzvods)
                        {
                            if (vzvod.Value.newIdStructure == decreeoperation.Optionnumber7 && vzvod.Value.name == decreeoperation.Optionstring5)
                            {
                                isExist = false;
                                break;
                            }
                            else
                                continue;
                        }
                        if (isExist)
                        {
                            n++;
                            new_vzvods.Add(n, new NewVzvod(decreeoperation.Optionnumber7, decreeoperation.Optionnumber6, decreeoperation.Optionstring5) { });
                        }
                    }
                }

                MovePersonBetweenCourses movePersonBetweenCourses = new MovePersonBetweenCourses(this, user, new_vzvods);
                movePersonBetweenCourses.Worker(decreeoperationsToAcceptMove);
            }


            foreach (Persondecreeoperation decreeoperation in decreeoperationsToAccept)
            {
                // Поощрить (наградить)
                if (decreeoperation.Persondecreeblocktype == 1)
                {
                    // В ЭЛД вносим только те записи, которые относятся к сотруднику, у которого есть личное дело
                    if (decreeoperation.Person > 0)
                    {
                        /**
                     * В созданной награде мы выставляем дату и номер приказа, аналогичные дате и номеру этого конкретного приказа.
                     */
                        Personreward personreward = new Personreward();
                        personreward.Rewarddate = decree.Datesigned.GetValueOrDefault();
                        string structureOwnerName = "";
                        if (structureOwner != null)
                        {
                            structureOwnerName = GetStructureNameDocument(structureOwner, date, 1, null);
                            structureOwnerName += " ";
                        }
                        //personreward.Order = structureOwnerName + decree.Name + " " + decree.Number;
                        //personreward.Order = structureOwnerName + " " + decree.Number;
                        personreward.Order = decree.Number;
                        personreward.Ordernumbertype = decree.Numbertype;
                        personreward.Orderwho = ""; // Поменять на подписывателя приказа.
                        if (structureOwner != null)
                        {
                            personreward.Orderwho = GetStructureNameDocument(structureOwner, date, 1, null);
                        }

                        personreward.Optionnumber1 = decreeoperation.Subvaluenumber1;
                        personreward.Optionnumber2 = decreeoperation.Subvaluenumber2;
                        personreward.Optionstring1 = decreeoperation.Subvaluestring1;
                        personreward.Optionstring2 = decreeoperation.Subvaluestring2;
                        //personreward.Optionnumber1 = decreeoperation.Optionnumber1;
                        //personreward.Optionnumber2 = decreeoperation.Optionnumber2;
                        //personreward.Optionstring1 = decreeoperation.Optionstring1;
                        //personreward.Optionstring2 = decreeoperation.Optionstring2;
                        personreward.Person = decreeoperation.Person;
                        personreward.Reason = decreeoperation.Intro;
                        switch (decreeoperation.Persondecreeblocksubtype)
                        {
                            // Нагрудный знак
                            case 4:
                                personreward.Rewardtype = 1;
                                break;
                            // Медаль
                            case 3:
                                personreward.Rewardtype = 1;
                                break;
                            // На одну ступень выше
                            case 1:
                                personreward.Rewardtype = 4;
                                break;
                            // Досрочно
                            case 2:
                                personreward.Rewardtype = 5;
                                break;
                            // Грамота
                            case 5:
                                personreward.Rewardtype = 6;
                                break;
                            // Почетная грамота
                            case 6:
                                personreward.Rewardtype = 7;
                                break;
                            // Денежное вознаграждение
                            case 7:
                                personreward.Rewardtype = 8;
                                break;
                            // Снятие взыскания
                            case 8:
                                personreward.Rewardtype = 9;
                                break;
                            // Благодарность
                            case 9:
                                personreward.Rewardtype = 10;
                                break;
                        }
                        //personreward.Rewardtype

                        //Personreward contextPersonreward = Personrewards.First(p => p.Id == decreeoperation.Subjectid);
                        //contextPersonreward.Rewarddate = decree.Datesigned.GetValueOrDefault();
                        //contextPersonreward.Order = decree.Name + " " + decree.Number;
                        context.Personreward.Add(personreward);
                    }

                }
                // Наложение дисциплинарного взыскания
                else if (decreeoperation.Persondecreeblocktype == 2)
                {
                    Personpenalty personpenalty = new Personpenalty();
                    personpenalty.Orderdate = decree.Datesigned.GetValueOrDefault(); ;
                    //personpenalty.Ordernumber = decree.Name + " " + decree.Number;
                    personpenalty.Ordernumber = decree.Number;
                    personpenalty.Ordernumbertype = decree.Numbertype;
                    personpenalty.Orderwho = ""; // Поменять на подписывателя приказа.
                    personpenalty.Orderwhoid = 0;
                    personpenalty.Orderid = decree.Id;
                    personpenalty.Penalty = decreeoperation.Optionnumber1;
                    personpenalty.Violation = decreeoperation.Intro;
                    personpenalty.Person = decreeoperation.Person;
                    if (structureOwner != null)
                    {
                        personpenalty.Orderwho = GetStructureNameDocument(structureOwner, date, 1, null);
                        personpenalty.Orderwhoid = structureOwner.Id;
                    }

                    context.Personpenalty.Add(personpenalty);
                }                
                // Назначить
                else if (decreeoperation.Persondecreeblocktype == 3)
                {
                    Personjob personjob = new Personjob();
                    personjob.Person = decreeoperation.Person;
                    personjob.Servicetype = 2;
                    personjob.Servicecoef = 1;
                    personjob.Servicefeature = 1;
                    personjob.Position = decreeoperation.Optionnumber1;
                    personjob.Jobposition = decreeoperation.Optionstring1;
                    personjob.Jobplace = decreeoperation.Optionstring2;
                    personjob.Serviceplace = decreeoperation.Optionstring2;
                    personjob.Mchs = 1;
                    personjob.Jobtype = 2;
                    personjob.Orderdate = decree.Datesigned.GetValueOrDefault();
                    if (decreeoperation.Optiondate1 == null)
                    {
                        personjob.Start = decree.Datesigned.GetValueOrDefault();
                    }
                    else
                    {
                        personjob.Start = decreeoperation.Optiondate1;
                    }
                    //personjob.Ordernumber = decree.Name + " " + decree.Number;
                    personjob.Ordernumber = decree.Number;
                    personjob.Ordernumbertype = decree.Numbertype;
                    personjob.Orderwho = "";
                    personjob.Orderwhoid = 0;
                    personjob.Orderid = decree.Id;
                    if (structureOwner != null)
                    {
                        personjob.Orderwho = GetStructureNameDocument(structureOwner, date, 1, null);
                        personjob.Orderwhoid = structureOwner.Id;
                    }
                    personjob.Actual = 1;

                    IQueryable<Personjob> personjobs = Personjobs.Where(p => p.Person == decreeoperation.Person);
                    foreach (Personjob personjobExisted in personjobs)
                    {
                        if (personjobExisted.Actual == 1)
                        {
                            personjobExisted.Actual = 0;
                            //personjobExisted.End = personjob.Start.GetValueOrDefault().AddDays(-1);
                            personjobExisted.End = personjob.Start.GetValueOrDefault();
                        }

                    }

                    AppointPerson(user, personjob.Person, personjob.Position); // Обновляем в ЭЛД актуальную должность
                    context.Personjob.Add(personjob);
                    //SaveChanges();
                    //personjobsIds.Add(personjob.Id);
                }
                // Уволить
                else if (decreeoperation.Persondecreeblocktype == 4)
                {
                    Personjob lastPersonjob = context.Personjob.FirstOrDefault(p => p.Person == decreeoperation.Person && p.Actual > 0); // Актуальная работа должна быть одна. Или ищем первую.
                    if (lastPersonjob != null)
                    {
                        lastPersonjob.Actual = 0;
                        //lastPersonjob.Fireordernumber = decree.Name + " " + decree.Number;
                        lastPersonjob.Fireordernumber = decree.Number;
                        lastPersonjob.Fireordernumbertype = decree.Numbertype;
                        lastPersonjob.Fireorderid = decree.Id;
                        lastPersonjob.Fireorderwho = "";
                        lastPersonjob.Fireorderwhoid = 0;
                        lastPersonjob.Fireorderdate = decree.Datesigned.GetValueOrDefault();
                        if (structureOwner != null)
                        {
                            lastPersonjob.Fireorderwho = GetStructureNameDocument(structureOwner, date, 1, null);
                            lastPersonjob.Fireorderwhoid = structureOwner.Id;
                        }
                        if (decreeoperation.Optiondate1 == null)
                        {
                            lastPersonjob.End = decree.Datesigned.GetValueOrDefault();
                        }
                        else
                        {
                            lastPersonjob.End = decreeoperation.Optiondate1;
                        }
                        TakeoffPerson(user, decreeoperation.Person);
                        //SaveChanges();
                        //personjobsFireIds.Add(lastPersonjob.Id);

                        // optionnumber4 - право ношение установленной формы одежды и знаков отличия
                        // optionnumber5 - повышение до майора
                        // optionnumber6 - Тип поощрения. Если 0, нет поощрения при увольнении
                        // optionnumber7 - для поощрения val1
                        // optionnumber8 - для поощрения val2
                        // optionstring3 - для поощрения val1
                        // optionstring4 - для поощрения val2
                        if (decreeoperation.Optionnumber6 > 0 && decreeoperation.Optionnumber9 > 0)
                        {
                            /**
                             * В созданной награде мы выставляем дату и номер приказа, аналогичные дате и номеру этого конкретного приказа.
                             */
                            Personreward personreward = new Personreward();
                            personreward.Rewarddate = decree.Datesigned.GetValueOrDefault();
                            string structureOwnerName = "";
                            if (structureOwner != null)
                            {
                                structureOwnerName = GetStructureNameDocument(structureOwner, date, 1, null);
                                structureOwnerName += " ";
                            }
                            //personreward.Order = structureOwnerName + decree.Name + " " + decree.Number;
                            personreward.Order = structureOwnerName + " " + decree.Number;
                            personreward.Optionnumber1 = decreeoperation.Optionnumber7;
                            personreward.Optionnumber2 = decreeoperation.Optionnumber8;
                            personreward.Optionstring1 = decreeoperation.Optionstring3;
                            personreward.Optionstring2 = decreeoperation.Optionstring4;
                            //personreward.Optionnumber1 = decreeoperation.Optionnumber1;
                            //personreward.Optionnumber2 = decreeoperation.Optionnumber2;
                            //personreward.Optionstring1 = decreeoperation.Optionstring1;
                            //personreward.Optionstring2 = decreeoperation.Optionstring2;
                            personreward.Person = decreeoperation.Person;
                            personreward.Reason = "За многолетнюю безупречную службу в органах и подразделениях по чрезвычайным ситуациям";
                            switch (decreeoperation.Optionnumber6)
                            {
                                // Нагрудный знак
                                case 4:
                                    personreward.Rewardtype = 1;
                                    break;
                                // Медаль
                                case 3:
                                    personreward.Rewardtype = 1;
                                    break;
                                // На одну ступень выше
                                case 1:
                                    personreward.Rewardtype = 4;
                                    break;
                                // Досрочно
                                case 2:
                                    personreward.Rewardtype = 5;
                                    break;
                                // Грамота
                                case 5:
                                    personreward.Rewardtype = 6;
                                    break;
                                // Почетная грамота
                                case 6:
                                    personreward.Rewardtype = 7;
                                    break;
                                // Денежное вознаграждение
                                case 7:
                                    personreward.Rewardtype = 8;
                                    break;
                                // Снятие взыскания
                                case 8:
                                    personreward.Rewardtype = 9;
                                    break;
                                // Благодарность
                                case 9:
                                    personreward.Rewardtype = 10;
                                    break;
                            }
                            context.Personreward.Add(personreward);
                        }
                    }
                }
                // Освободить
                else if (decreeoperation.Persondecreeblocktype == 5)
                {

                }
                // Перевести
                else if (decreeoperation.Persondecreeblocktype == 6)
                {

                }
                // Прекратить службу
                else if (decreeoperation.Persondecreeblocktype == 7)
                {

                }
                // Отстранить
                else if (decreeoperation.Persondecreeblocktype == 8)
                {

                }
                // Внести изменения в учетные документы
                else if (decreeoperation.Persondecreeblocktype == 9)
                {

                }
                // Установить
                else if (decreeoperation.Persondecreeblocktype == 10)
                {

                }
                // Заключить контракты с
                else if (decreeoperation.Persondecreeblocktype == 11)
                {
                    Personcontract personcontract = new Personcontract();
                    personcontract.Person = decreeoperation.Person;
                    personcontract.Datestart = decreeoperation.Optiondate1.GetValueOrDefault();
                    personcontract.Dateend = decreeoperation.Optiondate2.GetValueOrDefault();
                    if (decreeoperation.Optionnumber2 > 0)
                    {
                        personcontract.Pay = 1;
                    }
                    personcontract.Ordernumber = decree.Number;
                    personcontract.Ordernumbertype = decree.Numbertype;
                    personcontract.Orderwho = "";
                    personcontract.Orderwhoid = 0;
                    personcontract.Orderid = decree.Id;
                    if (structureOwner != null)
                    {
                        personcontract.Orderwho = GetStructureNameDocument(structureOwner, date, 1, null);
                        personcontract.Orderwhoid = structureOwner.Id;
                    }
                    context.Personcontract.Add(personcontract);
                }
                // Продлить контракт с
                else if (decreeoperation.Persondecreeblocktype == 12)
                {
                    Personcontract personcontract = new Personcontract();
                    personcontract.Person = decreeoperation.Person;
                    personcontract.Datestart = decreeoperation.Optiondate1.GetValueOrDefault();
                    personcontract.Dateend = decreeoperation.Optiondate2.GetValueOrDefault();
                    if (decreeoperation.Optionnumber2 > 0)
                    {
                        personcontract.Pay = 1;
                    }
                    personcontract.Ordernumber = decree.Number;
                    personcontract.Ordernumbertype = decree.Numbertype;
                    personcontract.Orderwho = "";
                    personcontract.Orderwhoid = 0;
                    personcontract.Orderid = decree.Id;
                    if (structureOwner != null)
                    {
                        personcontract.Orderwho = GetStructureNameDocument(structureOwner, date, 1, null);
                        personcontract.Orderwhoid = structureOwner.Id;
                    }
                    context.Personcontract.Add(personcontract);
                }
                // Выплатить денежную компенсацию
                else if (decreeoperation.Persondecreeblocktype == 13)
                {

                }
                // Присвоить
                else if (decreeoperation.Persondecreeblocktype == 14)
                {
                    Personrank personrank = new Personrank();
                    personrank.Person = decreeoperation.Person;
                    personrank.Rank = decreeoperation.Persondecreeblocksubtype;
                    Rank rank = RanksLocal().GetValue(decreeoperation.Persondecreeblocksubtype);
                    if (rank != null)
                    {
                        personrank.Rankstring = rank.Name;
                    }
                    personrank.Structureid = 0;
                    personrank.Decreeid = decree.Id;
                    string structureOwnerName = "";
                    if (structureOwner != null)
                    {
                        structureOwnerName = GetStructureNameDocument(structureOwner, date, 1, null);
                        structureOwnerName += " ";
                        personrank.Structureid = structureOwner.Id;
                        //personrank.Structureid = GetOriginalStructure(structureOwner).Id;
                    }
                    personrank.Structure = structureOwnerName;

                    //personrank.Decreenumber = decree.Name + " " + decree.Number;
                    personrank.Decreenumber = decree.Number;
                    personrank.Decreenumbertype = decree.Numbertype;
                    if (decreeoperation.Optiondate1 == null)
                    {
                        personrank.Datestart = decree.Datesigned.GetValueOrDefault();
                    }
                    else
                    {
                        personrank.Datestart = decreeoperation.Optiondate1.GetValueOrDefault();
                    }
                    personrank.Decreedate = decree.Datesigned.GetValueOrDefault();
                    context.Personrank.Add(personrank);
                }
                // Предоставить
                else if (decreeoperation.Persondecreeblocktype == 15)
                {
                    // отпуск
                    if (decreeoperation.Persondecreeblocksubtype == 11)
                    {
                        if (person == null)
                        {
                            person = GetPersonManager(user, decreeoperation.Person);
                        }

                        int countryid = decreeoperation.Optionnumber3;
                        string city = decreeoperation.Optionstring2;
                        int traveldays = decreeoperation.Optionnumber2;
                        int totalduration = decreeoperation.Optionnumber1;
                        int type = decreeoperation.Subvaluenumber1;
                        int holidays = decreeoperation.Optionnumber8;
                        int military = 0;
                        if (person.ActualRank != null)
                        {
                            military = 1;
                        }
                        DateTime startdate = decreeoperation.Optiondate1.GetValueOrDefault();

                        Vacationtype vacationtype = Vacationtypes.FirstOrDefault(v => v.Id == decreeoperation.Subvaluenumber1);

                        // Отпуск основной
                        if (decreeoperation.Optionstring5.Length > 0 && vacationtype.Main == 1)
                        {
                            string[] periods = decreeoperation.Optionstring5.Split(';');
                            Personvacation lastvacation = null; // Записываем последний отпуск, чтобы к нему прибавить дни на переезд в случае чего. 
                            // За каждый период мы создаем свой personvacation, которые идут последовательно.
                            foreach (string period in periods)
                            {
                                string[] parts = period.Split('%');
                                // Новый режим работы, когда мы при помощи + разделяем начало и конец периода.
                                if (period.Contains('+'))
                                {
                                    DateTime dateStart = new DateTime(Int32.Parse(parts[0].Split('+')[0].Split('.')[2]),
                                                                      Int32.Parse(parts[0].Split('+')[0].Split('.')[1]),
                                                                      Int32.Parse(parts[0].Split('+')[0].Split('.')[0]));
                                    DateTime dateEnd = new DateTime(Int32.Parse(parts[0].Split('+')[1].Split('.')[2]),
                                                                      Int32.Parse(parts[0].Split('+')[1].Split('.')[1]),
                                                                      Int32.Parse(parts[0].Split('+')[1].Split('.')[0]));
                                    int days = Int32.Parse(parts[1]);

                                    Personvacation personvacation = new Personvacation();
                                    Jobperiod jobperiod = person.Jobperiods.FirstOrDefault(j => j.Start.GetValueOrDefault().Date == dateStart.Date);
                                    if (jobperiod != null)
                                    {
                                        personvacation.Allowstart = jobperiod.Start.GetValueOrDefault();
                                        personvacation.Allowend = jobperiod.End.GetValueOrDefault();
                                    }
                                    personvacation.Date = startdate;
                                    personvacation.Duration = days;
                                    personvacation.Person = decreeoperation.Person;
                                    personvacation.Vacationtype = type;
                                    personvacation.Vacationmilitary = military;
                                    //personvacation.Holidays = holidays;
                                    startdate = startdate.AddDays(days);
                                    context.Personvacation.Add(personvacation);
                                    lastvacation = personvacation;
                                // Старый режим работы, когда мы записывали только года, а не начало и конец периода
                                } else
                                {
                                    int year = Int32.Parse(parts[0]);
                                    int days = Int32.Parse(parts[1]);

                                    Personvacation personvacation = new Personvacation();
                                    Jobperiod jobperiod = person.Jobperiods.FirstOrDefault(j => j.Start.GetValueOrDefault().Year == year);
                                    if (jobperiod != null)
                                    {
                                        personvacation.Allowstart = jobperiod.Start.GetValueOrDefault();
                                        personvacation.Allowend = jobperiod.End.GetValueOrDefault();
                                    }
                                    personvacation.Date = startdate;
                                    personvacation.Duration = days;
                                    personvacation.Person = decreeoperation.Person;
                                    personvacation.Vacationtype = type;
                                    personvacation.Vacationmilitary = military;
                                    //personvacation.Holidays = holidays;
                                    startdate = startdate.AddDays(days);
                                    context.Personvacation.Add(personvacation);
                                    lastvacation = personvacation;
                                }


                                
                            }

                            if (lastvacation != null)
                            {
                                lastvacation.Trip = traveldays;
                                lastvacation.Holidays = holidays;
                            }
                            
                        }
                        // Отпуск социальный
                        else
                        {
                            Personvacation personvacation = new Personvacation();
                            Jobperiod jobperiod = person.Jobperiods.FirstOrDefault(j => j.Start.GetValueOrDefault().Year == startdate.Year);
                            if (jobperiod != null)
                            {
                                personvacation.Allowstart = jobperiod.Start.GetValueOrDefault();
                                personvacation.Allowend = jobperiod.End.GetValueOrDefault();
                            }
                            personvacation.Date = startdate;
                            personvacation.Duration = totalduration;
                            personvacation.Person = decreeoperation.Person;
                            personvacation.Vacationtype = type;
                            personvacation.Vacationmilitary = military;
                            
                            context.Personvacation.Add(personvacation);
                        }

                        SaveChanges();
                        UpdatePersonvacationsLocal();
                    }
                    // предоставление дополнительного дня отдыха
                    else if (decreeoperation.Persondecreeblocksubtype == 12)
                    {
                        
                    }
                }
                // Командировать
                else if (decreeoperation.Persondecreeblocktype == 16)
                {
                    // Записываем если за границу
                    if (decreeoperation.Persondecreeblocksubtype == 13 && decreeoperation.Optionnumber1 > 0)
                    {
                        //Personworktrip personworktrip = new Personworktrip();
                        //personworktrip.Country = decreeoperation.Optionnumber1;
                        //personworktrip.Tripdate =
                    }
                    
                }
                // Зачислить
                else if (decreeoperation.Persondecreeblocktype == 17)
                {
                    Personjob personjob = new Personjob();
                    personjob.Person = decreeoperation.Person;
                    personjob.Servicetype = 2;
                    personjob.Servicecoef = 1;
                    personjob.Servicefeature = 1;
                    personjob.Position = decreeoperation.Optionnumber3;
                    personjob.Serviceplace = decreeoperation.Optionstring2;
                    personjob.Mchs = 1;
                    if (decreeoperation.Optionnumber9 == 0)
                    {
                        personjob.Jobtype = 4;
                    }
                    else
                    {
                        personjob.Jobtype = 2;
                    }
                    personjob.Orderdate = decree.Datesigned.GetValueOrDefault();
                    if (decreeoperation.Optiondate1 == null)
                    {
                        personjob.Start = decree.Datesigned.GetValueOrDefault();
                    }
                    else
                    {
                        personjob.Start = decreeoperation.Optiondate1;
                    }
                    //personjob.Ordernumber = decree.Name + " " + decree.Number;
                    personjob.Ordernumber = decree.Number;
                    personjob.Ordernumbertype = decree.Numbertype;
                    personjob.Orderwho = "";
                    personjob.Orderwhoid = 0;
                    personjob.Orderid = decree.Id;
                    if (structureOwner != null)
                    {
                        personjob.Orderwho = GetStructureNameDocument(structureOwner, date, 1, null);
                        personjob.Orderwhoid = structureOwner.Id;
                    }
                    personjob.Actual = 1;

                    if(decreeoperation.Optionnumber8 != 2)
                    {
                        List<Personjob> personjobs = PersonjobsLocal().Values.Where(p => p.Person == decreeoperation.Person).ToList();
                        foreach (Personjob personjobExisted in personjobs)
                        {
                            if (personjobExisted.Actual == 1)
                            {
                                personjobExisted.Actual = 0;
                                //personjobExisted.End = personjob.Start.GetValueOrDefault().AddDays(-1);
                                personjobExisted.End = personjob.Start.GetValueOrDefault();
                            }
                        }
                    }

                    AppointPersonJob(user, personjob);
                    AppointPerson(user, personjob.Person, personjob.Position); // Обновляем в ЭЛД актуальную должность
                    context.Personjob.Add(personjob);
                    //SaveChanges();
                    //personjobsIds.Add(personjob.Id);

                    if (decreeoperation.Optionnumber9 != 0)
                    {
                        Personrank personrank = new Personrank();
                        personrank.Person = decreeoperation.Person;
                        personrank.Rank = decreeoperation.Optionnumber9;
                        Rank ranks = RanksLocal().GetValue(decreeoperation.Persondecreeblocksubtype);
                        if (ranks != null)
                        {
                            personrank.Rankstring = ranks.Name;
                        }
                        personrank.Structureid = 0;
                        personrank.Decreeid = decree.Id;
                        string structureOwnerName = "";
                        if (structureOwner != null)
                        {
                            structureOwnerName = GetStructureNameDocument(structureOwner, date, 1, null);
                            structureOwnerName += " ";
                            personrank.Structureid = structureOwner.Id;
                            //personrank.Structureid = GetOriginalStructure(structureOwner).Id;
                        }
                        personrank.Structure = structureOwnerName;

                        //personrank.Decreenumber = decree.Name + " " + decree.Number;
                        personrank.Decreenumber = decree.Number;
                        personrank.Decreenumbertype = decree.Numbertype;
                        if (decreeoperation.Optiondate1 == null)
                        {
                            personrank.Datestart = decree.Datesigned.GetValueOrDefault();
                        }
                        else
                        {
                            personrank.Datestart = decreeoperation.Optiondate1.GetValueOrDefault();
                        }
                        personrank.Decreedate = decree.Datesigned.GetValueOrDefault();
                        context.Personrank.Add(personrank);
                    }

                    Personeducation personeducation = new Personeducation();

                    personeducation.Ucp = 1;
                    personeducation.Main = 1;
                    personeducation.Person = decreeoperation.Person;
                    personeducation.City = decreeoperation.Optionstring7;
                    personeducation.Location = decreeoperation.Optionstring6;
                    personeducation.Name = decreeoperation.Optionstring8;
                    personeducation.Name2 = decreeoperation.Optionstring8;
                    personeducation.Faculty = decreeoperation.Optionstring4;
                    personeducation.Speciality = decreeoperation.Optionstring3;
                    personeducation.Start = decreeoperation.Optiondate1;
                    personeducation.Educationstage = decreeoperation.Optionnumber5;
                    personeducation.Educationlevel = decreeoperation.Optionnumber6;
                    personeducation.Orderdate = decree.Datesigned;
                    personeducation.Orderid = decree.Id;
                    personeducation.Ordernumber = decree.Number;
                    personeducation.Ordernumbertype = decree.Numbertype;

                    Educationtypeblock educationtypeblock = new Educationtypeblock();
                    educationtypeblock.Educationperiods = new List<Educationperiod>();
                    educationtypeblock.Educationtype = 1;
                    Educationperiod educationperiod = new Educationperiod();
                    educationperiod.Start = decreeoperation.Optiondate1;
                    

                    personeducation.Educationtypeblocks = new List<Educationtypeblock> { };

                    personeducation.Educationtypeblocks.Add(educationtypeblock);

                    List<Educationtypeblock> educationtypeblocks = new List<Educationtypeblock>(personeducation.Educationtypeblocks);
                    List<Academicvacation> academicvacations = new List<Academicvacation>(personeducation.Academicvacation);
                    List<Educationmaternity> educationmaternities = new List<Educationmaternity>(personeducation.Educationmaternities);
                    context.Personeducation.Add(personeducation);


                    SaveChanges();

                    Educationtypeblock newEducationtypeblock = new Educationtypeblock();
                    newEducationtypeblock.Personeducation = personeducation.Id;
                    newEducationtypeblock.Educationtype = decreeoperation.Optionnumber8;

                    List<Educationperiod> educationperiods = new List<Educationperiod>(educationtypeblock.Educationperiods);
                    context.Educationtypeblock.Add(newEducationtypeblock);
                    SaveChanges();

                    int platoon = int.Parse(decreeoperation.Optionstring1.Split(' ')[0]);
                    int course = int.Parse(decreeoperation.Optionstring2.Split(' ')[0]);
                    Rank rank = new Rank();
                    if (decreeoperation.Optionnumber9 == 0)
                    {
                        rank.Name = "";
                    }
                    else
                    {
                        rank = RanksLocal().GetValue(decreeoperation.Optionnumber9);
                    }

                    Educationperiod newEducationperiod = new Educationperiod();
                    newEducationperiod.Educationtypeblock = newEducationtypeblock.Id;
                    newEducationperiod.Service = 0;
                    newEducationperiod.Start = decreeoperation.Optiondate1;
                    newEducationperiod.Educationpositiontype = decreeoperation.Optionnumber2;
                    newEducationperiod.Rank = rank.Name;
                    newEducationperiod.Platoon = platoon;
                    newEducationperiod.Course = course;
                    context.Educationperiod.Add(newEducationperiod);
                    SaveChanges();
                }
                // Отчислить
                else if (decreeoperation.Persondecreeblocktype == 18)
                {
                    Personjob lastPersonjob = context.Personjob.FirstOrDefault(p => p.Person == decreeoperation.Person && p.Actual > 0); // Актуальная работа должна быть одна. Или ищем первую.
                    if (lastPersonjob != null)
                    {
                        lastPersonjob.Actual = 0;
                        //lastPersonjob.Fireordernumber = decree.Name + " " + decree.Number;
                        lastPersonjob.Fireordernumber = decree.Number;
                        lastPersonjob.Fireordernumbertype = decree.Numbertype;
                        lastPersonjob.Fireorderid = decree.Id;
                        lastPersonjob.Fireorderwho = "";
                        lastPersonjob.Fireorderwhoid = 0;
                        lastPersonjob.Fireorderdate = decree.Datesigned.GetValueOrDefault();
                        if (structureOwner != null)
                        {
                            lastPersonjob.Fireorderwho = GetStructureNameDocument(structureOwner, date, 1, null);
                            lastPersonjob.Fireorderwhoid = structureOwner.Id;
                        }
                        if (decreeoperation.Optiondate1 == null)
                        {
                            lastPersonjob.End = decree.Datesigned.GetValueOrDefault();
                        }
                        else
                        {
                            lastPersonjob.End = decreeoperation.Optiondate1;
                        }
                    }
                    TakeoffPerson(user, decreeoperation.Person);

                    SaveChanges();

                    Personeducation lastPersoneducation = context.Personeducation.FirstOrDefault(e => e.Person == decreeoperation.Person && e.End == null);

                    if (decreeoperation.Optionnumber1 == 0)
                    {
                        if(lastPersoneducation != null)
                        {
                            lastPersoneducation.End = decreeoperation.Optiondate1;

                            Educationtypeblock educationtypeblock = context.Educationtypeblock.FirstOrDefault(e => e.Personeducation == lastPersoneducation.Id);
                            if (educationtypeblock != null)
                            {
                                educationtypeblock.IsEnded = 1;
                                Educationperiod educationperiod = context.Educationperiod.FirstOrDefault(e => e.Educationtypeblock == educationtypeblock.Id && e.End == null);
                                educationperiod.End = decree.Datesigned;
                            }
                            SaveChanges();
                        }
                    }
                    else
                    {
                        if (lastPersoneducation != null)
                        {
                            lastPersoneducation.Interrupted = 1;
                            lastPersoneducation.Interruptorderdate = decree.Datesigned;
                            lastPersoneducation.Interruptordernumber = decree.Number;
                            lastPersoneducation.Interruptordernumbertype = decree.Numbertype;
                            lastPersoneducation.Interruptorderreason = decreeoperation.Optionstring7;
                            lastPersoneducation.Interruptorderwho = decree.Nickname;

                            Educationtypeblock educationtypeblock = context.Educationtypeblock.FirstOrDefault(e => e.Personeducation == lastPersoneducation.Id);
                            if (educationtypeblock != null)
                            {
                                educationtypeblock.IsEnded = 1;
                                Educationperiod educationperiod = context.Educationperiod.FirstOrDefault(e => e.Educationtypeblock == educationtypeblock.Id && e.End == null);
                                educationperiod.End = decree.Datesigned;
                            }
                            SaveChanges();
                        }
                    }
                }
                // Увеличить
                else if (decreeoperation.Persondecreeblocktype == 19)
                {

                }
                // Восстановить
                else if (decreeoperation.Persondecreeblocktype == 20)
                {
                    Personrank personrank = context.Personrank.FirstOrDefault(p => p.Person == decreeoperation.Person);

                    Personjob personjob = new Personjob();
                    personjob.Person = decreeoperation.Person;
                    personjob.Servicetype = 2;
                    personjob.Servicecoef = 1;
                    personjob.Servicefeature = 1;
                    personjob.Position = decreeoperation.Optionnumber4;
                    personjob.Serviceplace = decreeoperation.Optionstring2;
                    personjob.Mchs = 1;
                    if (personrank != null)
                    {
                        personjob.Jobtype = 2;
                    }
                    else
                    {
                        personjob.Jobtype = 4;
                    }
                    personjob.Orderdate = decree.Datesigned.GetValueOrDefault();
                    if (decreeoperation.Optiondate1 == null)
                    {
                        personjob.Start = decree.Datesigned.GetValueOrDefault();
                    }
                    else
                    {
                        personjob.Start = decreeoperation.Optiondate1;
                    }
                    //personjob.Ordernumber = decree.Name + " " + decree.Number;
                    personjob.Ordernumber = decree.Number;
                    personjob.Ordernumbertype = decree.Numbertype;
                    personjob.Orderwho = "";
                    personjob.Orderwhoid = 0;
                    personjob.Orderid = decree.Id;
                    if (structureOwner != null)
                    {
                        personjob.Orderwho = GetStructureNameDocument(structureOwner, date, 1, null);
                        personjob.Orderwhoid = structureOwner.Id;
                    }
                    personjob.Actual = 1;
                    if(decreeoperation.Optionnumber8 != 2)
                    {
                        List<Personjob> personjobs = PersonjobsLocal().Values.Where(p => p.Person == decreeoperation.Person).ToList();
                        foreach (Personjob personjobExisted in personjobs)
                        {
                            if (personjobExisted.Actual == 1)
                            {
                                personjobExisted.Actual = 0;
                                //personjobExisted.End = personjob.Start.GetValueOrDefault().AddDays(-1);
                                personjobExisted.End = personjob.Start.GetValueOrDefault();
                            }
                        }
                    }

                    AppointPersonJob(user, personjob);
                    AppointPerson(user, personjob.Person, personjob.Position); // Обновляем в ЭЛД актуальную должность
                    context.Personjob.Add(personjob);
                    SaveChanges();
                    //personjobsIds.Add(personjob.Id);

                    
                    Personeducation personeducation = context.Personeducation.FirstOrDefault(p => p.Person == decreeoperation.Person && p.End == null);

                    if(personeducation != null)
                    {
                        personeducation.Interrupted = 0;
                        personeducation.Interruptorderdate = null;
                        personeducation.Interruptordernumber = "";
                        personeducation.Interruptordernumbertype = "";
                        personeducation.Interruptorderreason = "";
                        personeducation.Interruptorderwho = "";
                    }

                    Educationtypeblock educationtypeblock = new Educationtypeblock();
                    educationtypeblock.Educationperiods = new List<Educationperiod>();
                    educationtypeblock.Educationtype = 1;
                    Educationperiod educationperiod = new Educationperiod();
                    educationperiod.Start = decreeoperation.Optiondate1;


                    personeducation.Educationtypeblocks = new List<Educationtypeblock> { };

                    personeducation.Educationtypeblocks.Add(educationtypeblock);

                    List<Educationtypeblock> educationtypeblocks = new List<Educationtypeblock>(personeducation.Educationtypeblocks);
                    List<Academicvacation> academicvacations = new List<Academicvacation>(personeducation.Academicvacation);
                    List<Educationmaternity> educationmaternities = new List<Educationmaternity>(personeducation.Educationmaternities);
                    


                    SaveChanges();

                    Educationtypeblock newEducationtypeblock = new Educationtypeblock();
                    newEducationtypeblock.Personeducation = personeducation.Id;
                    newEducationtypeblock.Educationtype = decreeoperation.Optionnumber8;

                    List<Educationperiod> educationperiods = new List<Educationperiod>(educationtypeblock.Educationperiods);

                    context.Educationtypeblock.Add(newEducationtypeblock);

                    SaveChanges();

                    int platoon = int.Parse(decreeoperation.Optionstring1.Split(' ')[0]);
                    int course = int.Parse(decreeoperation.Optionnumber2.ToString().Split(' ')[0]);
                    
                    Educationperiod newEducationperiod = new Educationperiod();
                    newEducationperiod.Educationtypeblock = newEducationtypeblock.Id;
                    newEducationperiod.Service = 0;
                    newEducationperiod.Start = decreeoperation.Optiondate1;
                    newEducationperiod.Educationpositiontype = decreeoperation.Optionnumber3;
                    newEducationperiod.Rank = context.Rank.FirstOrDefault(r => r.Id == personrank.Rank).Name;
                    newEducationperiod.Platoon = platoon;
                    newEducationperiod.Course = course;
                    context.Educationperiod.Add(newEducationperiod);
                    SaveChanges();
                }
            }

            context.SaveChanges();
            UpdatePersondecreesLocal();
            UpdatePersonrewardsLocal();

            //UpdateDateactives(decree.Id);

        }

        public void UpdatePersondecree(PersondecreeManagement decreeManagement, User user)
        {
            Persondecree decree = Persondecrees.First(d => d.Id == decreeManagement.Id);
            //contextUser.Decree = 0;
            decree.Name = decreeManagement.Name;
            decree.Number = decreeManagement.Number;
            decree.Numbertype = decreeManagement.Numbertype;
            decree.Nickname = decreeManagement.Nickname;
            decree.Datecreated = decreeManagement.Datecreated;
            decree.Datesigned = decreeManagement.Datesigned; // - Дата подписи появляется только после оформления проекта приказа. 
            context.SaveChanges();

            UpdatePersondecreesLocal();
            //UpdateDateactives(decree.Id);
        }

        /// <summary>
        /// Меняет владельца проекта приказа. 
        /// </summary>
        /// <param name="decreeManagement"></param>
        /// <param name="user"></param>
        public void ChangeownerPersondecree(PersondecreeManagement decreeManagement, User user)
        {
            Persondecree decree = Persondecrees.First(d => d.Id == decreeManagement.Id);
            decree.Owner = decreeManagement.Owner;

            context.SaveChanges();
            UpdatePersondecreesLocal();
        }

        /// <summary>
        /// Возвращает все элементы проекта приказа, связанного с электронными личными делами
        /// </summary>
        /// <param name="decreeid"></param>
        /// <param name="disabletracking"></param>
        /// <returns></returns>
        public IEnumerable<PersondecreeoperationManagement> GetPersondecreeoperation(User user, int decreeid, bool disabletracking = false)
        {
            DateTime date = user.Date.GetValueOrDefault();
            if (disabletracking)
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }

            //Decree decree = DecreesLocal()[decreeid];

            Persondecree decree = PersondecreesLocal().GetValue(decreeid);

            List<Persondecreeoperation> persondecreeoperationsBase = PersondecreeoperationsLocal().Values.Where(p => p.Persondecree == decreeid).ToList();
            List<PersondecreeoperationManagement> persondecreeoperations = new List<PersondecreeoperationManagement>();

            Dictionary<int, Structure> all_structures = StructuresLocal();
            IEnumerable<Persondecreeexcerpt> persondecreeexcerpts = context.Persondecreeexcerpt;
            foreach (Persondecreeoperation decreeoperationBase in persondecreeoperationsBase)
            {
                PersondecreeoperationManagement decreeoperation = new PersondecreeoperationManagement(decreeoperationBase);
                //Person person = PersonsLocal().GetValue(decreeoperationBase.Person);
                //if (person != null)
                //{
                //    decreeoperation.Personobject = person;
                //}

                PersonManager person = GetPersonManager(user, decreeoperationBase.Person);
                if (person != null)
                {
                    decreeoperation.Personobject = person;
                }

                List<PersonManager> personManagers = new List<PersonManager>();
                if (decreeoperation.Optionarrayperson != null && decreeoperation.Optionarrayperson.Length > 0)
                {
                    string[] personidsStrings = decreeoperation.Optionarrayperson.Split(',');
                    foreach (string personidsString in personidsStrings)
                    {
                        int personid = Int32.Parse(personidsString);
                        PersonManager personInList = GetPersonManager(user, personid);
                        if (personInList != null)
                        {
                            personManagers.Add(personInList);
                        }
                    }
                }
                decreeoperation.OptionarraypersonObjects = personManagers;

                // Поощрить
                if (decreeoperation.Persondecreeblocktype == 1) 
                {
                    //снять ранее наложенное взыскание
                    if (decreeoperation.Persondecreeblocksubtype == 8)
                    {
                        // Загружаем сюда 
                        if (decreeoperation.Subvaluenumber2 > 0) // В Optionnumber2 мы храним информацию об id personpenalty
                        {
                            Personpenalty personpenalty = Personpenalties.FirstOrDefault(p => p.Id == decreeoperation.Subvaluenumber2);
                            if (personpenalty != null)
                            {
                                decreeoperation.Personpenalty = personpenalty;
                            }
                        }
                    }
                    
                }
                // Наложить дисциплинарное взыскание
                if (decreeoperation.Persondecreeblocktype == 2)
                {

                }
                // Назначить
                if (decreeoperation.Persondecreeblocktype == 3)
                {
                    if (decreeoperation.Optionnumber1 > 0) // Здеси хранится ID должности
                    {
                        Position position = PositionsLocal().GetValue(decreeoperation.Optionnumber1);
                        if (position != null)
                        {
                            decreeoperation.Positionobject = position;

                            //if (position == null)
                            //{
                            //    return null;
                            //}
                            Positiontype positiontype = PositiontypesLocal().GetValue(position.Positiontype);
                            decreeoperation.Positiontypeobject = positiontype;
                            if (positiontype != null)
                            {
                                // Наименование должности
                                decreeoperation.Optionstring1 = positiontype.Name2; // По умолчанию дательный падеж
                            }

                            Structure actualStructure = GetActualStructureInfo(position.Structure, date); 
                            if (actualStructure != null)
                            {
                                decreeoperation.Structureobject = actualStructure;
                                string structureTree = FormTreeDocument(actualStructure, date, null, 2, null); // Временное решение
                                //string structureTree = FormTreeDocument2(actualStructure, true, date); // Временное решение
                                //optionstring2
                                decreeoperation.Optionstring2 = structureTree;
                            }

                            if (actualStructure != null)
                            {
                                // Полное наименование должности и подразделения в родительном падеже.
                                decreeoperation.Optionstring4 = FormTreeDocument(actualStructure, date, position, 2, null);
                            }
                            
                        }
                    }
                }
                // Уволить
                if (decreeoperation.Persondecreeblocktype == 4)
                {
                    if (decreeoperation.Persondecreeblocksubtype > 0) // Здесь хранится по какой причине увольняем человека
                    {
                        Fire fire = Fires.FirstOrDefault(f => f.Id == decreeoperation.Persondecreeblocksubtype);
                        if (fire != null)
                        {
                            decreeoperation.Fireobject = fire;
                        }
                    }
                }
                // Освободить
                if (decreeoperation.Persondecreeblocktype == 5)
                {
                    // optionnumber1 - в соответствии с каким пунктом
                    // optionnumber2 - здесь хранится id подразделения
                    // optionstring1 - по какой причине
                    // optionstring2 - Полное наименование подразделение, в распоряжение начальника которого отправляют сотрудника
                    // optionstring3 - Основание

                    Structure actualStructure = GetActualStructureInfo(decreeoperation.Optionnumber2, date);
                    if (actualStructure != null)
                    {
                        decreeoperation.Structureobject = actualStructure;
                        string structureTree = FormTreeDocument(actualStructure, date, null, 2, null); // Временное решение
                        decreeoperation.Optionstring2 = structureTree;
                    }
                }
                // Перевести
                if (decreeoperation.Persondecreeblocktype == 6)
                {
                    
                }
                // Прекратить службу
                if (decreeoperation.Persondecreeblocktype == 7)
                {

                }
                // Отстранить
                if (decreeoperation.Persondecreeblocktype == 8)
                {

                }
                // Внести изменения в учетные документы
                if (decreeoperation.Persondecreeblocktype == 9)
                {

                }
                // Установить
                if (decreeoperation.Persondecreeblocktype == 10)
                {

                }
                // Заключить контракты с
                if (decreeoperation.Persondecreeblocktype == 11)
                {

                }
                // Продлить контракт с
                if (decreeoperation.Persondecreeblocktype == 12)
                {

                }
                // Выплатить денежную компенсацию
                if (decreeoperation.Persondecreeblocktype == 13)
                {

                }

                //if (decreeoperation.Subjecttype == 1 && decreeoperation.Subjectid > 0) // Награда
                //{
                //    Personreward personreward = PersonrewardsLocal().GetValue(decreeoperation.Subjectid);
                //    if (personreward != null)
                //    {
                //        decreeoperation.Personreward = personreward;
                //    }
                //}
                decreeoperation.Excerptstructures = generateexcerptstructurename(decreeoperation.Decreeexcerpt,
                    all_structures,
                    persondecreeexcerpts);
                persondecreeoperations.Add(decreeoperation);
            }


            return persondecreeoperations;
        }

        private List<string> generateexcerptstructurename(string id_list,
            Dictionary<int, Structure> structures,
            IEnumerable<Persondecreeexcerpt> persondecreeexcerpts,
            string devizer = "_")
        {
            List<string> output = new List<string>() { "" };
            if (id_list.Length == 0)
                return output;

            List<string> working_ides_structures = id_list.Split(devizer).ToList();
            Structure time_structure = null;
            Persondecreeexcerpt time_persondecreeexcerpt = null;
            int parse_value;
            foreach(string id in working_ides_structures)
            {
                parse_value = Int32.Parse(id);
                time_persondecreeexcerpt = persondecreeexcerpts.FirstOrDefault(r => r.Id == parse_value);
                if (time_persondecreeexcerpt == null)
                    continue;
                time_structure = structures[time_persondecreeexcerpt.Structure];
                output.Add(time_structure.Nameshortened);
            }
            return output;
        }

        // public void AddPersonDecreeoperation(User user, PersondecreeoperationManagement persondecreeoperation)
        public Persondecreeoperation AddPersonDecreeoperation(User user, Persondecreeoperation persondecreeoperation)
        {
            if (persondecreeoperation == null || persondecreeoperation.Persondecreeblock == 0)
            {
                return null;
            }
            if (persondecreeoperation.Person == 0 && (persondecreeoperation.Nonperson == null || persondecreeoperation.Nonperson.Length == 0)
                && (persondecreeoperation.Optionarrayperson == null || persondecreeoperation.Optionarrayperson.Length == 0)){
                return null;
            }
                 
            PersonManager personManager = null;

            Persondecreeoperation decreeoperation = new Persondecreeoperation();
            if (persondecreeoperation.Person != 0)
            {
                decreeoperation.Person = persondecreeoperation.Person;
            }
            // Случай, когда вместо работника из ЭЛД прописывается человек, не состоящий в МЧС.
            else if (persondecreeoperation.Nonperson != null && persondecreeoperation.Nonperson.Length > 0)
            {
                decreeoperation.Nonperson = persondecreeoperation.Nonperson;
            // Случай, когда несколько сотрудников
            } else
            {
                decreeoperation.Optionarrayperson = persondecreeoperation.Optionarrayperson;
                if (decreeoperation.Optionarrayperson.Split(',').Length == 1)
                {
                    decreeoperation.Person = Int32.Parse(decreeoperation.Optionarrayperson.Split(',')[0]); // Если только 1, то работа в режиме 1-го сотрудника
                    decreeoperation.Optionarrayperson = "";
                }
            }

            if (persondecreeoperation.Persondecreeblocktype == 17)
            {
                persondecreeoperation.Optionstring5 = ToUpperFirstLetter(context.Educationpositiontype.FirstOrDefault(e => e.Id == persondecreeoperation.Optionnumber2).Name);
                
                IEnumerable<Position> positions1 = GetAllFreePositionsFromStructure(persondecreeoperation.Optionnumber1, GetPersonsForStructure(persondecreeoperation.Optionnumber1), persondecreeoperation);
                persondecreeoperation.Optionnumber3 = positions1.ToList()[0].Id;

                if(persondecreeoperation.Person < 0)
                {
                    string vowels = "аеёиоуыэюя";
                    int numCandidate = -persondecreeoperation.Person;
                    Cabinetdata cabinetdata = context.Cabinetdata.FirstOrDefault(c => c.Id == numCandidate);
                    string fio = cabinetdata.Usersurname + ' ' + cabinetdata.Username + ' ' + cabinetdata.Userpatronymic;
                    if (vowels.Contains(fio.GetLast(1)))
                    {
                        fio += " Женский";
                    }
                    else
                    {
                        fio += " Мужской";
                    }
                    decreeoperation.Person = CreatePerson(user, fio);
                }
            }

            if (persondecreeoperation.Persondecreeblocktype == 18)
            {
                if(persondecreeoperation.Persondecreeblocksubtype == 1)
                {
                    Person person = context.Person.FirstOrDefault(p => p.Id == persondecreeoperation.Person);
                    Personjob personjob = context.Personjob.FirstOrDefault(p => p.Person == person.Id && p.End == null);
                    Position position = context.Position.FirstOrDefault(p => p.Id == personjob.Position);
                    Structure structure1 = context.Structure.FirstOrDefault(s => s.Id == position.Structure);
                    Structure structure2 = context.Structure.FirstOrDefault(s => s.Id == structure1.Parentstructure);
                    Structure structure3 = context.Structure.FirstOrDefault(s => s.Id == structure2.Parentstructure);
                    Structure structure4 = context.Structure.FirstOrDefault(s => s.Id == structure3.Parentstructure);
                    persondecreeoperation.Optionstring1 = structure3.Name;
                    persondecreeoperation.Optionstring2 = structure4.Name;
                }
                if (persondecreeoperation.Persondecreeblocksub == 2)
                {

                }
                Personeducation personeducation = Personeducations.ToList().FirstOrDefault(p => p.End == null && p.Person == decreeoperation.Person);
                Educationtypeblock educationtypeblock = Educationtypeblocks.ToList().FirstOrDefault(e => e.Personeducation == personeducation.Id && e.IsEnded == 0);
                Educationperiod educationperiod = Educationperiods.FirstOrDefault(e => e.Educationtypeblock == educationtypeblock.Id && e.End == null);

                persondecreeoperation.Optionnumber3 = GetEducationperiod(educationperiod.Id).Course;
            }

            if (persondecreeoperation.Persondecreeblocktype == 20)
            {
                persondecreeoperation.Optionstring5 = ToUpperFirstLetter(context.Educationpositiontype.FirstOrDefault(e => e.Id == persondecreeoperation.Optionnumber3).Name);

                IEnumerable<Position> positions1 = GetAllFreePositionsFromStructure(persondecreeoperation.Optionnumber1, GetPersonsForStructure(persondecreeoperation.Optionnumber1), persondecreeoperation);
                persondecreeoperation.Optionnumber4 = positions1.ToList()[0].Id;
            }

            if (persondecreeoperation.Persondecreeblocktype == 21 && persondecreeoperation.Optionnumber1 == 27)
            {
                Personeducation personeducation = Personeducations.ToList().FirstOrDefault(p => p.End == null && p.Person == decreeoperation.Person);
                Educationtypeblock educationtypeblock = Educationtypeblocks.ToList().FirstOrDefault(e => e.Personeducation == personeducation.Id && e.IsEnded == 0);
                Educationperiod educationperiod = Educationperiods.FirstOrDefault(e => e.Educationtypeblock == educationtypeblock.Id && e.End == null);

                persondecreeoperation.Optionnumber3 = GetEducationperiod(educationperiod.Id).Course;
            }
            decreeoperation.Intro = persondecreeoperation.Intro;
            decreeoperation.Persondecreeblocksubtype = persondecreeoperation.Persondecreeblocksubtype; // Тип подблока, например 6 - "наградить грамотой"
            decreeoperation.Persondecreeblocktype = persondecreeoperation.Persondecreeblocktype;
            decreeoperation.Persondecreeblock = persondecreeoperation.Persondecreeblock;
            decreeoperation.Creator = user.Id;
            decreeoperation.Persondecree = persondecreeoperation.Persondecree;
            decreeoperation.Optionnumber1 = persondecreeoperation.Optionnumber1;
            decreeoperation.Optionnumber2 = persondecreeoperation.Optionnumber2;
            decreeoperation.Optionnumber3 = persondecreeoperation.Optionnumber3;
            decreeoperation.Optionnumber4 = persondecreeoperation.Optionnumber4;
            decreeoperation.Optionnumber5 = persondecreeoperation.Optionnumber5;
            decreeoperation.Optionnumber6 = persondecreeoperation.Optionnumber6;
            decreeoperation.Optionnumber7 = persondecreeoperation.Optionnumber7;
            decreeoperation.Optionnumber8 = persondecreeoperation.Optionnumber8;
            decreeoperation.Optionnumber9 = persondecreeoperation.Optionnumber9;
            decreeoperation.Optionnumber10 = persondecreeoperation.Optionnumber10;
            decreeoperation.Optionnumber11 = persondecreeoperation.Optionnumber11;
            decreeoperation.Optionstring1 = persondecreeoperation.Optionstring1;
            if (persondecreeoperation.Optionstring2 == null)
            {
                persondecreeoperation.Optionstring2 = "";
            }
            decreeoperation.Optionstring2 = persondecreeoperation.Optionstring2;
            decreeoperation.Optionstring3 = persondecreeoperation.Optionstring3;
            decreeoperation.Optionstring4 = persondecreeoperation.Optionstring4;
            decreeoperation.Optionstring5 = persondecreeoperation.Optionstring5;
            decreeoperation.Optionstring6 = persondecreeoperation.Optionstring6;
            decreeoperation.Optionstring7 = persondecreeoperation.Optionstring7;
            decreeoperation.Optionstring8 = persondecreeoperation.Optionstring8;
            decreeoperation.Optionstring9 = persondecreeoperation.Optionstring9;
            decreeoperation.Optiondate1 = persondecreeoperation.Optiondate1;
            decreeoperation.Optiondate2 = persondecreeoperation.Optiondate2;
            decreeoperation.Optiondate3 = persondecreeoperation.Optiondate3;
            decreeoperation.Optiondate4 = persondecreeoperation.Optiondate4;
            decreeoperation.Optiondate5 = persondecreeoperation.Optiondate5;
            decreeoperation.Optiondate6 = persondecreeoperation.Optiondate6;
            decreeoperation.Optiondate7 = persondecreeoperation.Optiondate7;
            decreeoperation.Optiondate8 = persondecreeoperation.Optiondate8;
            decreeoperation.Subvaluenumber1 = persondecreeoperation.Subvaluenumber1;
            decreeoperation.Subvaluenumber2 = persondecreeoperation.Subvaluenumber2;
            decreeoperation.Subvaluestring1 = persondecreeoperation.Subvaluestring1;
            decreeoperation.Subvaluestring2 = persondecreeoperation.Subvaluestring2;
            decreeoperation.Optionarray1 = persondecreeoperation.Optionarray1;

            List<Persondecreeblockintro> persondecreeblockintros = Persondecreeblockintros.Where(p => p.Persondecreeblock == persondecreeoperation.Persondecreeblock).ToList();
            //List<Persondecreeblocksub> persondecreeblocksubs = Persondecreeblocksubs.Where(p => p.Persondecreeblock == persondecreeoperation.Persondecreeblock).ToList();
            bool hasIntro = false;
            // Проверяем, есть ли фабула у пункта
            if (persondecreeoperation.Intro != null && persondecreeoperation.Intro.Length > 0)
            {
                hasIntro = true;
            }

            // Проверяем, есть ли подпункт
            bool hasSub = false;
            if (persondecreeoperation.Persondecreeblocksubtype > 0)
            {
                hasSub = true;
            }

            IQueryable<Persondecreeblocksub> persondecreeblocksubs = Enumerable.Empty<Persondecreeblocksub>().AsQueryable();
            // Если уже существует фабула, как у операции, то мы прогружаем все сабы, которые входят в нее
            if (hasIntro)
            {
                Persondecreeblockintro existingPersondecreeblockintro = persondecreeblockintros.FirstOrDefault(p => p.Name.Equals(persondecreeoperation.Intro));
                if (existingPersondecreeblockintro != null)
                {
                    persondecreeblocksubs = Persondecreeblocksubs.Where(p => p.Persondecreeblockintro == existingPersondecreeblockintro.Id);
                }
            // Если фабулы не существует, то мы прогружаем все подпункты, которые не входят ни в одну фабулу.
            } else
            {
                persondecreeblocksubs = Persondecreeblocksubs.Where(p => p.Persondecreeblockintro == 0 && p.Persondecreeblock == persondecreeoperation.Persondecreeblock);
            }

            // Все подпункты блока.
            IQueryable<Persondecreeblocksub> persondecreeblocksubsAll = Persondecreeblocksubs.Where(p => p.Persondecreeblock == persondecreeoperation.Persondecreeblock);


            // 1 Поощрить
            if (decreeoperation.Persondecreeblocktype == 1)
            {
                
                if (hasIntro)
                {
                    Persondecreeblockintro existingPersondecreeblockintro = persondecreeblockintros.FirstOrDefault(p => p.Name.Equals(persondecreeoperation.Intro));
                    // Уже есть пункт, совпадающий по фабуле - не создаем новый объект
                    if (existingPersondecreeblockintro != null)
                    {
                        decreeoperation.Persondecreeblockintro = existingPersondecreeblockintro.Id;
                    }
                    else
                    {
                        Persondecreeblockintro persondecreeblockintro = new Persondecreeblockintro();
                        persondecreeblockintro.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                        persondecreeblockintro.Name = persondecreeoperation.Intro;
                        // Фабул до этого не было, это первая фабула
                        if (persondecreeblockintros.Count == 0)
                        {
                            persondecreeblockintro.Priority = 1;
                            persondecreeblockintro.Index = 1;
                        }
                        // Есть другие фабулы
                        else
                        {
                            int maxPriority = persondecreeblockintros.Max(p => p.Priority) + 1;
                            int maxIndex = persondecreeblockintros.Max(p => p.Index) + 1;

                            persondecreeblockintro.Priority = maxPriority;
                            persondecreeblockintro.Index = maxIndex;
                        }

                        if (persondecreeblocksubsAll.FirstOrDefault(p => p.Index == persondecreeblockintro.Index) != null)
                        {
                            foreach (Persondecreeblocksub p in persondecreeblocksubsAll){
                                if (p.Index >= persondecreeblockintro.Index)
                                {
                                    p.Index += 1;
                                }
                            }

                        }
                        
                        context.Persondecreeblockintro.Add(persondecreeblockintro);
                        SaveChanges();
                        decreeoperation.Persondecreeblockintro = persondecreeblockintro.Id;
                        existingPersondecreeblockintro = persondecreeblockintro; // Вписываем после создания
                    }

                    // Есть фабула и есть подпункт.
                    if (hasSub)
                    {
                        // Пытаемся найти, есть ли уже такой подпункт
                        Persondecreeblocksub existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Persondecreeblocksubtype == persondecreeoperation.Persondecreeblocksubtype
                         && p.Subvaluenumber1 == persondecreeoperation.Subvaluenumber1 && p.Subvaluenumber2 == persondecreeoperation.Subvaluenumber2 
                         && p.Subvaluestring1.Equals(persondecreeoperation.Subvaluestring1) && p.Subvaluestring2.Equals(persondecreeoperation.Subvaluestring2));
                        // Подпункт существует
                        if (existingPersondecreeblocksub != null)
                        {
                            decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;
                        } else
                        {
                            Persondecreeblocksub persondecreeblocksub = new Persondecreeblocksub();
                            persondecreeblocksub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                            persondecreeblocksub.Persondecreeblockintro = existingPersondecreeblockintro.Id;
                            persondecreeblocksub.Persondecreeblocksubtype = persondecreeoperation.Persondecreeblocksubtype;
                            persondecreeblocksub.Subvaluenumber1 = persondecreeoperation.Subvaluenumber1;
                            persondecreeblocksub.Subvaluenumber2 = persondecreeoperation.Subvaluenumber2;
                            persondecreeblocksub.Subvaluestring1 = persondecreeoperation.Subvaluestring1;
                            persondecreeblocksub.Subvaluestring2 = persondecreeoperation.Subvaluestring2;
                            persondecreeblocksub.Intro = existingPersondecreeblockintro.Id;
                            // Подпунктов в фабуле до этого не было, это первая фабула
                            if (persondecreeblocksubs.Count() == 0)
                            {
                                persondecreeblocksub.Priority = 1;
                                //persondecreeblocksub.Index = 1;
                            }
                            // Есть другие подпункты в фабуле
                            else
                            {
                                //int maxPriority = persondecreeblocksubs.Max(p => p.Priority) + 1;
                                ////int maxIndex = persondecreeblockintros.Max(p => p.Index) + 1;

                                //persondecreeblocksub.Priority = maxPriority;
                                ////persondecreeblocksub.Index = maxIndex;

                                // Ниже производится операция сортировки типа поощрения (подуправляющего слова).
                                // Например, если есть "наградить почетной грамотой" и "объявить благодарность",
                                // То "наградить грамотой" должно быть между ними.
                                int orderBase = Persondecreeblocksubtypes.FirstOrDefault(p => p.Id == persondecreeblocksub.Persondecreeblocksubtype).Order;
                                int orderClosest = 0;
                                int indexClosest = 0;
                                Persondecreeblocksub persondecreeblocksubNext = null;

                                foreach (Persondecreeblocksub persondecreeblocksubExisting in persondecreeblocksubs)
                                {
                                    int orderExisting = Persondecreeblocksubtypes.FirstOrDefault(p => p.Id == persondecreeblocksubExisting.Persondecreeblocksubtype).Order;
                                    int indexExisting = persondecreeblocksubExisting.Priority; // Так как саб внутри фабулы, то вместо Index используется Priority
                                    if (orderExisting > orderBase && (orderExisting < orderClosest || orderClosest == 0)){
                                        if (orderExisting < orderClosest)
                                        {
                                            indexClosest = 0; // Если мы находим тип поощрения, порядок сортировке которого ближе к добавляемому,
                                                              // обнуляем ближайший индекс
                                        }

                                        if (indexExisting > 0 && (indexExisting < indexClosest || indexClosest == 0))
                                        {
                                            orderClosest = orderExisting;
                                            indexClosest = indexExisting;
                                            persondecreeblocksubNext = persondecreeblocksubExisting; // Если есть хоть один тип поощрения, который идет после добавляемого саба
                                        }
                                    }
                                }

                                // Если мы не нашли саб, перед которым надо вклинить наш саб, кидаем в конец
                                if (persondecreeblocksubNext == null)
                                {
                                    int maxPriority = persondecreeblocksubs.Max(p => p.Priority) + 1;
                                    persondecreeblocksub.Priority = maxPriority;
                                // Если мы нашли саб, на место которого вставим наш, а все остальные сдвинем вперед
                                } else
                                {
                                    persondecreeblocksub.Priority = indexClosest; // Берем наиболее подходящий приоритет
                                    foreach (Persondecreeblocksub persondecreeblocksubExisting in persondecreeblocksubs)
                                    {
                                        int indexExisting = persondecreeblocksubExisting.Priority; // Так как саб внутри фабулы, то вместо Index используется Priority
                                        if (indexClosest >= indexExisting)
                                        {
                                            persondecreeblocksubExisting.Priority += 1; // Все что равно или выше нашего приоритета смещаем на 1 выше.
                                        }
                                    }
                                }

                            }
                            context.Persondecreeblocksub.Add(persondecreeblocksub);
                            SaveChanges();
                            decreeoperation.Persondecreeblocksub = persondecreeblocksub.Id;
                            existingPersondecreeblocksub = persondecreeblocksub; // Вписываем после создания
                        }
                    }
                // Если нет фабулы, но есть подпункт
                } else if (hasSub)
                {
                    // Пытаемся найти, есть ли уже такой подпункт
                    Persondecreeblocksub existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Persondecreeblocksubtype == persondecreeoperation.Persondecreeblocksubtype
                         && p.Subvaluenumber1 == persondecreeoperation.Subvaluenumber1 && p.Subvaluenumber2 == persondecreeoperation.Subvaluenumber2
                         && p.Subvaluestring1.Equals(persondecreeoperation.Subvaluestring1) && p.Subvaluestring2.Equals(persondecreeoperation.Subvaluestring2));
                    // Подпункт существует
                    if (existingPersondecreeblocksub != null)
                    {
                        decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;
                    }
                    else
                    {
                        Persondecreeblocksub persondecreeblocksub = new Persondecreeblocksub();
                        persondecreeblocksub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                        persondecreeblocksub.Persondecreeblocksubtype = persondecreeoperation.Persondecreeblocksubtype;
                        persondecreeblocksub.Subvaluenumber1 = persondecreeoperation.Subvaluenumber1;
                        persondecreeblocksub.Subvaluenumber2 = persondecreeoperation.Subvaluenumber2;
                        persondecreeblocksub.Subvaluestring1 = persondecreeoperation.Subvaluestring1;
                        persondecreeblocksub.Subvaluestring2 = persondecreeoperation.Subvaluestring2;
                        //persondecreeblocksub.Persondecreeblockintro = existingPersondecreeblockintro.Id; - фабулы нет

                        // Фабул не было
                        if (persondecreeblockintros.Count == 0)
                        {
                            // Подпунктов не было
                            if (persondecreeblocksubs.Count() == 0)
                            {
                                persondecreeblocksub.Priority = 1;
                                persondecreeblocksub.Index = 1;
                            } else
                            {
                                int maxPriority = persondecreeblocksubs.Max(p => p.Priority) + 1;
                                int maxIndex = persondecreeblocksubs.Max(p => p.Index) + 1;

                                persondecreeblocksub.Priority = maxPriority;
                                persondecreeblocksub.Index = maxIndex;
                            }

                        }
                        // Были фабулы
                        else
                        {

                            if (persondecreeblocksubs.Count() == 0)
                            {
                                int maxPriority = persondecreeblockintros.Max(p => p.Priority) + 1;
                                int maxIndex = persondecreeblockintros.Max(p => p.Index) + 1;

                                persondecreeblocksub.Priority = maxPriority;
                                persondecreeblocksub.Index = maxIndex;
                            } else
                            {
                                int maxPriority = persondecreeblocksubs.Max(p => p.Priority) + 1;
                                int maxIndex = persondecreeblocksubs.Max(p => p.Index) + 1;

                                persondecreeblocksub.Priority = maxPriority;
                                persondecreeblocksub.Index = maxIndex;
                            }
                        }
                        context.Persondecreeblocksub.Add(persondecreeblocksub);
                        SaveChanges();
                        decreeoperation.Persondecreeblocksub = persondecreeblocksub.Id;
                        existingPersondecreeblocksub = persondecreeblocksub; // Вписываем после создания
                    }
                }

                // 8 снять ранее наложенное взыскание.
                if (decreeoperation.Persondecreeblocksubtype == 8)
                {
                    // decreeoperation.Optionnumber1 - тип взыскания, которое будем пытаться снимать
                    if (personManager == null)
                    {
                        personManager = GetPersonManager(user, decreeoperation.Person); // Получаем полноценный ЭЛД
                    }
                    
                    Personpenalty personpenalty = personManager.Personpenalties.FirstOrDefault(p => p.Penalty == decreeoperation.Subvaluenumber1);
                    if (personpenalty != null && decreeoperation.Optionstring3.Length == 0 && decreeoperation.Optionstring4.Length == 0)
                    {
                        decreeoperation.Subvaluenumber2 = personpenalty.Id;
                    // Если взыскания нет в ЭЛД, то мы создаем собственное вне ЭЛД.
                    } else
                    {
                        Personpenalty fakePenalty = new Personpenalty();
                        //decreeoperation.Optionstring3 - Объявленное приказом
                        //decreeoperation.Optionstring4 - Номер приказа
                        //decreeoperation.Optiondate1 - От какого числа
                        fakePenalty.Orderwho = decreeoperation.Optionstring3;
                        fakePenalty.Ordernumber = decreeoperation.Optionstring4;
                        fakePenalty.Orderdate = decreeoperation.Optiondate1.GetValueOrDefault();
                        fakePenalty.Penalty = decreeoperation.Subvaluenumber1;
                        context.Personpenalty.Add(fakePenalty);
                        SaveChanges();
                        decreeoperation.Subvaluenumber2 = fakePenalty.Id;
                    }
                }
            }

            // Уволить
            if (persondecreeoperation.Persondecreeblocktype == 4)
            {
                // Пытаемся найти, есть ли уже такой подпункт
                //Persondecreeblocksub existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Persondecreeblocksubtype == persondecreeoperation.Persondecreeblocksubtype);
                Persondecreeblocksub existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Persondecreeblocksubtype == persondecreeoperation.Persondecreeblocksubtype
                         && p.Subvaluenumber1 == persondecreeoperation.Subvaluenumber1 && p.Subvaluenumber2 == persondecreeoperation.Subvaluenumber2
                         && p.Subvaluestring1.Equals(persondecreeoperation.Subvaluestring1) && p.Subvaluestring2.Equals(persondecreeoperation.Subvaluestring2));
                // Подпункт существует
                if (existingPersondecreeblocksub != null)
                {
                    decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;
                }
                else
                {
                    Persondecreeblocksub persondecreeblocksub = new Persondecreeblocksub();
                    persondecreeblocksub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                    persondecreeblocksub.Persondecreeblocksubtype = persondecreeoperation.Persondecreeblocksubtype;
                    persondecreeblocksub.Subvaluenumber1 = persondecreeoperation.Subvaluenumber1;
                    persondecreeblocksub.Subvaluenumber2 = persondecreeoperation.Subvaluenumber2;
                    persondecreeblocksub.Subvaluestring1 = persondecreeoperation.Subvaluestring1;
                    persondecreeblocksub.Subvaluestring2 = persondecreeoperation.Subvaluestring2;
                    //persondecreeblocksub.Persondecreeblockintro = existingPersondecreeblockintro.Id; - фабулы нет

                    // Фабул не было
                    if (persondecreeblockintros.Count == 0)
                    {
                        // Подпунктов не было
                        if (persondecreeblocksubs.Count() == 0)
                        {
                            persondecreeblocksub.Priority = 1;
                            persondecreeblocksub.Index = 1;
                        }
                        else
                        {
                            int maxPriority = persondecreeblocksubs.Max(p => p.Priority) + 1;
                            int maxIndex = persondecreeblocksubs.Max(p => p.Index) + 1;

                            persondecreeblocksub.Priority = maxPriority;
                            persondecreeblocksub.Index = maxIndex;
                        }

                    }
                    // Были фабулы
                    else
                    {

                        if (persondecreeblocksubs.Count() == 0)
                        {
                            int maxPriority = persondecreeblockintros.Max(p => p.Priority) + 1;
                            int maxIndex = persondecreeblockintros.Max(p => p.Index) + 1;

                            persondecreeblocksub.Priority = maxPriority;
                            persondecreeblocksub.Index = maxIndex;
                        }
                        else
                        {
                            int maxPriority = persondecreeblocksubs.Max(p => p.Priority) + 1;
                            int maxIndex = persondecreeblocksubs.Max(p => p.Index) + 1;

                            persondecreeblocksub.Priority = maxPriority;
                            persondecreeblocksub.Index = maxIndex;
                        }
                    }
                    context.Persondecreeblocksub.Add(persondecreeblocksub);
                    SaveChanges();
                    decreeoperation.Persondecreeblocksub = persondecreeblocksub.Id;
                    existingPersondecreeblocksub = persondecreeblocksub; // Вписываем после создания

                    
                }

                if (personManager == null)
                {
                    personManager = GetPersonManager(user, decreeoperation.Person); // Получаем полноценный ЭЛД
                }
                // Смотрим, проставляли ли сотруднику, что он может быть повышен до майора. Если да, еще раз проверяем, подходят ли для этого условия
                bool fireForMajor = false;
                if (decreeoperation.Persondecreeblocksubtype == 1 || decreeoperation.Persondecreeblocksubtype == 3 || decreeoperation.Persondecreeblocksubtype == 9 ||
                    decreeoperation.Persondecreeblocksubtype == 13 || decreeoperation.Persondecreeblocksubtype == 17 || decreeoperation.Persondecreeblocksubtype == 18)
                {
                    fireForMajor = true;
                }

                if (persondecreeoperation.Optionnumber5 > 0 && (personManager.Major == 0 || !fireForMajor))
                {
                    decreeoperation.Optionnumber5 = 0;
                    persondecreeoperation.Optionnumber5 = 0;
                }
            }

            // Назначить
            if (persondecreeoperation.Persondecreeblocktype == 3)
            {
                // Автоматически высчитываем цифры при заключении контракта при назначении
                if (decreeoperation.Optiondate3 != null)
                {
                    DateDiff dateDiff = new DateDiff(decreeoperation.Optiondate3.GetValueOrDefault(), decreeoperation.Optiondate4.GetValueOrDefault().AddDays(1));
                    if (dateDiff.ElapsedYears > 5 || (dateDiff.ElapsedYears == 5 && dateDiff.ElapsedMonths > 0) ||
                        (dateDiff.ElapsedYears == 5 && dateDiff.ElapsedMonths == 0 && dateDiff.ElapsedDays > 0))
                    {
                        decreeoperation.Optiondate4 = decreeoperation.Optiondate3.GetValueOrDefault().AddYears(5).AddDays(-1);
                        dateDiff = new DateDiff(decreeoperation.Optiondate3.GetValueOrDefault(), decreeoperation.Optiondate4.GetValueOrDefault().AddDays(1));
                    }

                    decreeoperation.Optionnumber8 = dateDiff.ElapsedYears;
                    decreeoperation.Optionnumber9 = dateDiff.ElapsedMonths;
                    decreeoperation.Optionnumber10 = dateDiff.ElapsedDays;
                }
                
            }

            // Освободить
            if (persondecreeoperation.Persondecreeblocktype == 5)
            {
                // Пытаемся найти, есть ли уже такой подпункт
                //Persondecreeblocksub existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Persondecreeblocksubtype == persondecreeoperation.Persondecreeblocksubtype);
                Persondecreeblocksub existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Persondecreeblocksubtype == persondecreeoperation.Persondecreeblocksubtype
                         && p.Subvaluenumber1 == persondecreeoperation.Subvaluenumber1 && p.Subvaluenumber2 == persondecreeoperation.Subvaluenumber2
                         && p.Subvaluestring1.Equals(persondecreeoperation.Subvaluestring1) && p.Subvaluestring2.Equals(persondecreeoperation.Subvaluestring2));
                // Подпункт существует
                if (existingPersondecreeblocksub != null)
                {
                    decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;
                }
                else
                {
                    Persondecreeblocksub persondecreeblocksub = new Persondecreeblocksub();
                    persondecreeblocksub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                    persondecreeblocksub.Persondecreeblocksubtype = persondecreeoperation.Persondecreeblocksubtype;
                    persondecreeblocksub.Subvaluenumber1 = persondecreeoperation.Subvaluenumber1;
                    persondecreeblocksub.Subvaluenumber2 = persondecreeoperation.Subvaluenumber2;
                    persondecreeblocksub.Subvaluestring1 = persondecreeoperation.Subvaluestring1;
                    persondecreeblocksub.Subvaluestring2 = persondecreeoperation.Subvaluestring2;
                    //persondecreeblocksub.Persondecreeblockintro = existingPersondecreeblockintro.Id; - фабулы нет

                    // Фабул не было
                    if (persondecreeblockintros.Count == 0)
                    {
                        // Подпунктов не было
                        if (persondecreeblocksubs.Count() == 0)
                        {
                            persondecreeblocksub.Priority = 1;
                            persondecreeblocksub.Index = 1;
                        }
                        else
                        {
                            int maxPriority = persondecreeblocksubs.Max(p => p.Priority) + 1;
                            int maxIndex = persondecreeblocksubs.Max(p => p.Index) + 1;

                            persondecreeblocksub.Priority = maxPriority;
                            persondecreeblocksub.Index = maxIndex;
                        }

                    }
                    // Были фабулы
                    else
                    {

                        if (persondecreeblocksubs.Count() == 0)
                        {
                            int maxPriority = persondecreeblockintros.Max(p => p.Priority) + 1;
                            int maxIndex = persondecreeblockintros.Max(p => p.Index) + 1;

                            persondecreeblocksub.Priority = maxPriority;
                            persondecreeblocksub.Index = maxIndex;
                        }
                        else
                        {
                            int maxPriority = persondecreeblocksubs.Max(p => p.Priority) + 1;
                            int maxIndex = persondecreeblocksubs.Max(p => p.Index) + 1;

                            persondecreeblocksub.Priority = maxPriority;
                            persondecreeblocksub.Index = maxIndex;
                        }
                    }
                    context.Persondecreeblocksub.Add(persondecreeblocksub);
                    SaveChanges();
                    decreeoperation.Persondecreeblocksub = persondecreeblocksub.Id;
                    existingPersondecreeblocksub = persondecreeblocksub; // Вписываем после создания
                }
            }

            // Установить
            if (persondecreeoperation.Persondecreeblocktype == 10)
            {
                if (persondecreeoperation.Optionnumber11 == 0)
                {
                    // Пытаемся найти, есть ли уже такой подпункт
                    //Persondecreeblocksub existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Persondecreeblocksubtype == persondecreeoperation.Persondecreeblocksubtype);
                    Persondecreeblocksub existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Subvaluestring1 == persondecreeoperation.Optionstring3);
                    if (existingPersondecreeblocksub != null)
                    {
                        decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;

                        existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Subvaluedate1 == persondecreeoperation.Optiondate1
                             && p.Subvaluedate2 == persondecreeoperation.Optiondate2);
                        if (existingPersondecreeblocksub != null)
                        {
                            decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;

                            existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Subvaluenumber1 == persondecreeoperation.Optionnumber2 && p.Parentpersondecreeblocksub == decreeoperation.Persondecreeblocksub);
                            if (existingPersondecreeblocksub != null)
                            {
                                decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;
                            }
                            else
                            {
                                Persondecreeblocksub persondecreeblocksubsubsub = new Persondecreeblocksub();
                                persondecreeblocksubsubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                                persondecreeblocksubsubsub.Persondecreeblocksubtype = persondecreeoperation.Persondecreeblocksubtype;
                                persondecreeblocksubsubsub.Subvaluenumber1 = persondecreeoperation.Optionnumber2;
                                persondecreeblocksubsubsub.Subvaluenumber2 = persondecreeoperation.Subvaluenumber2;
                                persondecreeblocksubsubsub.Subvaluestring1 = persondecreeoperation.Optionstring4;
                                persondecreeblocksubsubsub.Subvaluestring2 = persondecreeoperation.Subvaluestring2;
                                persondecreeblocksubsubsub.Parentpersondecreeblocksub = decreeoperation.Persondecreeblocksub;


                                context.Persondecreeblocksub.Add(persondecreeblocksubsubsub);
                                SaveChanges();
                                decreeoperation.Persondecreeblocksub = persondecreeblocksubsubsub.Id;

                            }
                        }
                        else
                        {
                            Persondecreeblocksub persondecreeblocksubsub = new Persondecreeblocksub();
                            persondecreeblocksubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                            persondecreeblocksubsub.Persondecreeblocksubtype = persondecreeoperation.Persondecreeblocksubtype;
                            persondecreeblocksubsub.Subvaluenumber1 = persondecreeoperation.Subvaluenumber1;
                            persondecreeblocksubsub.Subvaluenumber2 = persondecreeoperation.Subvaluenumber2;
                            persondecreeblocksubsub.Subvaluedate1 = persondecreeoperation.Optiondate1;
                            persondecreeblocksubsub.Subvaluedate2 = persondecreeoperation.Optiondate2;
                            persondecreeblocksubsub.Parentpersondecreeblocksub = decreeoperation.Persondecreeblocksub;

                            context.Persondecreeblocksub.Add(persondecreeblocksubsub);
                            SaveChanges();
                            decreeoperation.Persondecreeblocksub = persondecreeblocksubsub.Id;

                            Persondecreeblocksub persondecreeblocksubsubsub = new Persondecreeblocksub();
                            persondecreeblocksubsubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                            persondecreeblocksubsubsub.Persondecreeblocksubtype = persondecreeoperation.Persondecreeblocksubtype;
                            persondecreeblocksubsubsub.Subvaluenumber1 = persondecreeoperation.Optionnumber2;
                            persondecreeblocksubsubsub.Subvaluenumber2 = persondecreeoperation.Subvaluenumber2;
                            persondecreeblocksubsubsub.Subvaluestring1 = persondecreeoperation.Optionstring4;
                            persondecreeblocksubsubsub.Subvaluestring2 = persondecreeoperation.Subvaluestring2;
                            persondecreeblocksubsubsub.Parentpersondecreeblocksub = persondecreeblocksubsub.Id;

                            context.Persondecreeblocksub.Add(persondecreeblocksubsubsub);
                            SaveChanges();
                            decreeoperation.Persondecreeblocksub = persondecreeblocksubsubsub.Id;

                        }
                    }
                    else
                    {
                        Persondecreeblocksub persondecreeblocksub = new Persondecreeblocksub();
                        persondecreeblocksub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                        persondecreeblocksub.Persondecreeblocksubtype = persondecreeoperation.Persondecreeblocksubtype;
                        persondecreeblocksub.Subvaluenumber1 = persondecreeoperation.Subvaluenumber1;
                        persondecreeblocksub.Subvaluenumber2 = persondecreeoperation.Subvaluenumber2;
                        persondecreeblocksub.Subvaluestring1 = persondecreeoperation.Optionstring3;
                        persondecreeblocksub.Subvaluestring2 = persondecreeoperation.Subvaluestring2;

                        context.Persondecreeblocksub.Add(persondecreeblocksub);
                        SaveChanges();
                        decreeoperation.Persondecreeblocksub = persondecreeblocksub.Id;

                        Persondecreeblocksub persondecreeblocksubsub = new Persondecreeblocksub();
                        persondecreeblocksubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                        persondecreeblocksubsub.Persondecreeblocksubtype = persondecreeoperation.Persondecreeblocksubtype;
                        persondecreeblocksubsub.Subvaluenumber1 = persondecreeoperation.Subvaluenumber1;
                        persondecreeblocksubsub.Subvaluenumber2 = persondecreeoperation.Subvaluenumber2;
                        persondecreeblocksubsub.Subvaluedate1 = persondecreeoperation.Optiondate1;
                        persondecreeblocksubsub.Subvaluedate2 = persondecreeoperation.Optiondate2;
                        persondecreeblocksubsub.Parentpersondecreeblocksub = persondecreeblocksub.Id;

                        context.Persondecreeblocksub.Add(persondecreeblocksubsub);
                        SaveChanges();
                        decreeoperation.Persondecreeblocksub = persondecreeblocksubsub.Id;

                        Persondecreeblocksub persondecreeblocksubsubsub = new Persondecreeblocksub();
                        persondecreeblocksubsubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                        persondecreeblocksubsubsub.Persondecreeblocksubtype = persondecreeoperation.Persondecreeblocksubtype;
                        persondecreeblocksubsubsub.Subvaluenumber1 = persondecreeoperation.Optionnumber2;
                        persondecreeblocksubsubsub.Subvaluenumber2 = persondecreeoperation.Subvaluenumber2;
                        persondecreeblocksubsubsub.Subvaluestring1 = persondecreeoperation.Optionstring4;
                        persondecreeblocksubsubsub.Subvaluestring2 = persondecreeoperation.Subvaluestring2;
                        persondecreeblocksubsubsub.Parentpersondecreeblocksub = persondecreeblocksubsub.Id;

                        context.Persondecreeblocksub.Add(persondecreeblocksubsubsub);
                        SaveChanges();
                        decreeoperation.Persondecreeblocksub = persondecreeblocksubsubsub.Id;

                        // Фабул не было
                        if (persondecreeblockintros.Count == 0)
                        {
                            // Подпунктов не было
                            if (persondecreeblocksubs.Count() == 0)
                            {
                                persondecreeblocksub.Priority = 1;
                                persondecreeblocksub.Index = 1;
                            }
                            else
                            {
                                int maxPriority = persondecreeblocksubs.Max(p => p.Priority) + 1;
                                int maxIndex = persondecreeblocksubs.Max(p => p.Index) + 1;

                                persondecreeblocksub.Priority = maxPriority;
                                persondecreeblocksub.Index = maxIndex;
                            }
                        }
                    }
                }
                else
                {
                    // Пытаемся найти, есть ли уже такой подпункт
                    //Persondecreeblocksub existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Persondecreeblocksubtype == persondecreeoperation.Persondecreeblocksubtype);
                    Persondecreeblocksub existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Persondecreeblocksubtype == persondecreeoperation.Persondecreeblocksubtype
                             && p.Subvaluenumber1 == persondecreeoperation.Subvaluenumber1 && p.Subvaluenumber2 == persondecreeoperation.Subvaluenumber2
                             && p.Subvaluestring1.Equals(persondecreeoperation.Subvaluestring1) && p.Subvaluestring2.Equals(persondecreeoperation.Subvaluestring2));
                    // Подпункт существует
                    if (existingPersondecreeblocksub != null)
                    {
                        decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;
                    }
                    else
                    {
                        Persondecreeblocksub persondecreeblocksub = new Persondecreeblocksub();
                        persondecreeblocksub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                        persondecreeblocksub.Persondecreeblocksubtype = persondecreeoperation.Persondecreeblocksubtype;
                        persondecreeblocksub.Subvaluenumber1 = persondecreeoperation.Subvaluenumber1;
                        persondecreeblocksub.Subvaluenumber2 = persondecreeoperation.Subvaluenumber2;
                        persondecreeblocksub.Subvaluestring1 = persondecreeoperation.Subvaluestring1;
                        persondecreeblocksub.Subvaluestring2 = persondecreeoperation.Subvaluestring2;
                        //persondecreeblocksub.Persondecreeblockintro = existingPersondecreeblockintro.Id; - фабулы нет

                        // Фабул не было
                        if (persondecreeblockintros.Count == 0)
                        {
                            // Подпунктов не было
                            if (persondecreeblocksubs.Count() == 0)
                            {
                                persondecreeblocksub.Priority = 1;
                                persondecreeblocksub.Index = 1;
                            }
                            else
                            {
                                int maxPriority = persondecreeblocksubs.Max(p => p.Priority) + 1;
                                int maxIndex = persondecreeblocksubs.Max(p => p.Index) + 1;

                                persondecreeblocksub.Priority = maxPriority;
                                persondecreeblocksub.Index = maxIndex;
                            }

                        }
                        // Были фабулы
                        else
                        {

                            if (persondecreeblocksubs.Count() == 0)
                            {
                                int maxPriority = persondecreeblockintros.Max(p => p.Priority) + 1;
                                int maxIndex = persondecreeblockintros.Max(p => p.Index) + 1;

                                persondecreeblocksub.Priority = maxPriority;
                                persondecreeblocksub.Index = maxIndex;
                            }
                            else
                            {
                                int maxPriority = persondecreeblocksubs.Max(p => p.Priority) + 1;
                                int maxIndex = persondecreeblocksubs.Max(p => p.Index) + 1;

                                persondecreeblocksub.Priority = maxPriority;
                                persondecreeblocksub.Index = maxIndex;
                            }
                        }
                        context.Persondecreeblocksub.Add(persondecreeblocksub);
                        SaveChanges();
                        decreeoperation.Persondecreeblocksub = persondecreeblocksub.Id;
                        existingPersondecreeblocksub = persondecreeblocksub; // Вписываем после создания
                    }

                }

            }

            // Заключить контракты с
            if (persondecreeoperation.Persondecreeblocktype == 11)
            {
                //decreeoperation.Optiondate1 = persondecreeoperation.Optiondate1;
                //decreeoperation.Optiondate2 = persondecreeoperation.Optiondate2;
                DateDiff dateDiff = new DateDiff(decreeoperation.Optiondate1.GetValueOrDefault(), decreeoperation.Optiondate2.GetValueOrDefault().AddDays(1));
                if (dateDiff.ElapsedYears > 5 || (dateDiff.ElapsedYears == 5 && dateDiff.ElapsedMonths > 0) || 
                    (dateDiff.ElapsedYears == 5 && dateDiff.ElapsedMonths == 0 && dateDiff.ElapsedDays > 0))
                {
                    decreeoperation.Optiondate2 = decreeoperation.Optiondate1.GetValueOrDefault().AddYears(5).AddDays(-1);
                    dateDiff = new DateDiff(decreeoperation.Optiondate1.GetValueOrDefault(), decreeoperation.Optiondate2.GetValueOrDefault().AddDays(1));
                }

                decreeoperation.Optionnumber3 = dateDiff.ElapsedYears;
                decreeoperation.Optionnumber4 = dateDiff.ElapsedMonths;
                decreeoperation.Optionnumber5 = dateDiff.ElapsedDays;

                if (personManager == null)
                {
                    personManager = GetPersonManager(user, decreeoperation.Person); // Получаем полноценный ЭЛД
                }
                int yearsContract = GetPersonContractYears(personManager);
                
                // Если контракт заключается на срок от 3 до 5 лет, при этом есть выслуга по контрактам от 5 лет.
                if (dateDiff.ElapsedYears >= 3 && dateDiff.ElapsedYears < 5 && yearsContract >= 5)
                {
                    decreeoperation.Optionnumber2 = 10; // 10 базовых величин
                // Если контракт заключается на 5 лет, при этом выслуга по контрактам от 5 лет
                } else if (dateDiff.ElapsedYears >= 5 && yearsContract >= 5)
                {
                    decreeoperation.Optionnumber2 = 35; // 35 базовых величин
                } else
                {
                    decreeoperation.Optionnumber2 = 0; // без выплат
                }
            }

            // Продлить контракты с
            if (persondecreeoperation.Persondecreeblocktype == 12)
            {
                DateDiff dateDiff = new DateDiff(decreeoperation.Optiondate1.GetValueOrDefault(), decreeoperation.Optiondate2.GetValueOrDefault().AddDays(1));
                if (dateDiff.ElapsedYears > 5 || (dateDiff.ElapsedYears == 5 && dateDiff.ElapsedMonths > 0) ||
                    (dateDiff.ElapsedYears == 5 && dateDiff.ElapsedMonths == 0 && dateDiff.ElapsedDays > 0))
                {
                    decreeoperation.Optiondate2 = decreeoperation.Optiondate1.GetValueOrDefault().AddYears(5).AddDays(-1);
                    dateDiff = new DateDiff(decreeoperation.Optiondate1.GetValueOrDefault(), decreeoperation.Optiondate2.GetValueOrDefault().AddDays(1));
                }

                decreeoperation.Optionnumber3 = dateDiff.ElapsedYears;
                decreeoperation.Optionnumber4 = dateDiff.ElapsedMonths;
                decreeoperation.Optionnumber5 = dateDiff.ElapsedDays;

                if (personManager == null)
                {
                    personManager = GetPersonManager(user, decreeoperation.Person); // Получаем полноценный ЭЛД
                }
                int yearsContract = GetPersonContractYears(personManager);

                // Если контракт заключается на срок от 3 до 5 лет, при этом есть выслуга по контрактам от 5 лет.
                if (dateDiff.ElapsedYears >= 3 && dateDiff.ElapsedYears < 5 && yearsContract >= 5)
                {
                    decreeoperation.Optionnumber2 = 10; // 10 базовых величин
                                                        // Если контракт заключается на 5 лет, при этом выслуга по контрактам от 5 лет
                }
                else if (dateDiff.ElapsedYears >= 5 && yearsContract >= 5)
                {
                    decreeoperation.Optionnumber2 = 35; // 35 базовых величин
                }
                else
                {
                    decreeoperation.Optionnumber2 = 0; // без выплат
                }
            }

            // Присвоить.
            if (persondecreeoperation.Persondecreeblocktype == 14)
            {
                // Пытаемся найти, есть ли уже такой подпункт
                //Persondecreeblocksub existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Persondecreeblocksubtype == persondecreeoperation.Persondecreeblocksubtype);
                Persondecreeblocksub existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Persondecreeblocksubtype == persondecreeoperation.Persondecreeblocksubtype
                         && p.Subvaluenumber1 == persondecreeoperation.Subvaluenumber1 && p.Subvaluenumber2 == persondecreeoperation.Subvaluenumber2
                         && p.Subvaluestring1.Equals(persondecreeoperation.Subvaluestring1) && p.Subvaluestring2.Equals(persondecreeoperation.Subvaluestring2));
                // Подпункт существует
                if (existingPersondecreeblocksub != null)
                {
                    decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;
                }
                else
                {
                    Persondecreeblocksub persondecreeblocksub = new Persondecreeblocksub();
                    persondecreeblocksub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                    persondecreeblocksub.Persondecreeblocksubtype = persondecreeoperation.Persondecreeblocksubtype;
                    persondecreeblocksub.Subvaluenumber1 = persondecreeoperation.Subvaluenumber1;
                    persondecreeblocksub.Subvaluenumber2 = persondecreeoperation.Subvaluenumber2;
                    persondecreeblocksub.Subvaluestring1 = persondecreeoperation.Subvaluestring1;
                    persondecreeblocksub.Subvaluestring2 = persondecreeoperation.Subvaluestring2;
                    //persondecreeblocksub.Persondecreeblockintro = existingPersondecreeblockintro.Id; - фабулы нет

                    // Фабул не было
                    if (persondecreeblockintros.Count == 0)
                    {
                        // Подпунктов не было
                        if (persondecreeblocksubs.Count() == 0)
                        {
                            persondecreeblocksub.Priority = 1;
                            persondecreeblocksub.Index = 1;
                        }
                        else
                        {
                            int maxPriority = persondecreeblocksubs.Max(p => p.Priority) + 1;
                            int maxIndex = persondecreeblocksubs.Max(p => p.Index) + 1;

                            persondecreeblocksub.Priority = maxPriority;
                            persondecreeblocksub.Index = maxIndex;
                        }

                    }
                    // Были фабулы
                    else
                    {

                        if (persondecreeblocksubs.Count() == 0)
                        {
                            int maxPriority = persondecreeblockintros.Max(p => p.Priority) + 1;
                            int maxIndex = persondecreeblockintros.Max(p => p.Index) + 1;

                            persondecreeblocksub.Priority = maxPriority;
                            persondecreeblocksub.Index = maxIndex;
                        }
                        else
                        {
                            int maxPriority = persondecreeblocksubs.Max(p => p.Priority) + 1;
                            int maxIndex = persondecreeblocksubs.Max(p => p.Index) + 1;

                            persondecreeblocksub.Priority = maxPriority;
                            persondecreeblocksub.Index = maxIndex;
                        }
                    }
                    context.Persondecreeblocksub.Add(persondecreeblocksub);
                    SaveChanges();
                    decreeoperation.Persondecreeblocksub = persondecreeblocksub.Id;
                    existingPersondecreeblocksub = persondecreeblocksub; // Вписываем после создания
                }
            }

            // Предоставить
            if (persondecreeoperation.Persondecreeblocktype == 15)
            {
                if(persondecreeoperation.Subvaluenumber1 != 4)
                {
                    if (decreeoperation.Optionnumber2 > 15)
                    {
                        decreeoperation.Optionnumber2 = 15;
                    }

                    string[] jobperiods = decreeoperation.Optionstring5.Split(';');
                    if (jobperiods.Length > 0 && decreeoperation.Optionstring5.Length > 0)
                    {
                        string[] jobperiodelements = jobperiods[0].Split('%');
                        decreeoperation.Subvaluestring1 = jobperiodelements[0];
                        persondecreeoperation.Subvaluestring1 = jobperiodelements[0];
                        // Храним длительность отпуска за 1ый год
                        decreeoperation.Optionnumber4 = Int32.Parse(jobperiodelements[1]);
                        persondecreeoperation.Optionnumber4 = Int32.Parse(jobperiodelements[1]);
                    }
                    if (jobperiods.Length > 1 && decreeoperation.Optionstring5.Length > 0)
                    {
                        string[] jobperiodelements = jobperiods[1].Split('%');
                        decreeoperation.Subvaluestring2 = jobperiodelements[0];
                        persondecreeoperation.Subvaluestring2 = jobperiodelements[0];
                        // Храним длительность отпуска за 2ой год
                        decreeoperation.Optionnumber5 = Int32.Parse(jobperiodelements[1]);
                        persondecreeoperation.Optionnumber5 = Int32.Parse(jobperiodelements[1]);
                    }
                    //decreeoperation.Subvaluestring1 = persondecreeoperation.Subvaluestring1;
                    //decreeoperation.Subvaluestring2 = persondecreeoperation.Subvaluestring2;
                    List<Persondecreeblocksub> t = persondecreeblocksubs.ToList();
                    // Пытаемся найти, есть ли уже такой подпункт
                    //Persondecreeblocksub existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Persondecreeblocksubtype == persondecreeoperation.Persondecreeblocksubtype);
                    Persondecreeblocksub existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Persondecreeblocksubtype == persondecreeoperation.Persondecreeblocksubtype
                             && p.Subvaluenumber1 == persondecreeoperation.Subvaluenumber1 && p.Subvaluenumber2 == persondecreeoperation.Subvaluenumber2
                             && p.Subvaluestring1.Equals(persondecreeoperation.Subvaluestring1) && p.Subvaluestring2.Equals(persondecreeoperation.Subvaluestring2));
                    // Подпункт существует
                    if (existingPersondecreeblocksub != null)
                    {
                        decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;
                    }
                    else
                    {
                        Persondecreeblocksub persondecreeblocksub = new Persondecreeblocksub();
                        persondecreeblocksub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                        persondecreeblocksub.Persondecreeblocksubtype = persondecreeoperation.Persondecreeblocksubtype;
                        persondecreeblocksub.Subvaluenumber1 = persondecreeoperation.Subvaluenumber1;
                        persondecreeblocksub.Subvaluenumber2 = persondecreeoperation.Subvaluenumber2;
                        persondecreeblocksub.Subvaluestring1 = persondecreeoperation.Subvaluestring1;
                        persondecreeblocksub.Subvaluestring2 = persondecreeoperation.Subvaluestring2;
                        //persondecreeblocksub.Persondecreeblockintro = existingPersondecreeblockintro.Id; - фабулы нет

                        // Фабул не было
                        if (persondecreeblockintros.Count == 0)
                        {
                            // Подпунктов не было
                            if (persondecreeblocksubs.Count() == 0)
                            {
                                persondecreeblocksub.Priority = 1;
                                persondecreeblocksub.Index = 1;
                            }
                            else
                            {
                                int maxPriority = persondecreeblocksubs.Max(p => p.Priority) + 1;
                                int maxIndex = persondecreeblocksubs.Max(p => p.Index) + 1;

                                persondecreeblocksub.Priority = maxPriority;
                                persondecreeblocksub.Index = maxIndex;
                            }

                        }
                        // Были фабулы
                        else
                        {

                            if (persondecreeblocksubs.Count() == 0)
                            {
                                int maxPriority = persondecreeblockintros.Max(p => p.Priority) + 1;
                                int maxIndex = persondecreeblockintros.Max(p => p.Index) + 1;

                                persondecreeblocksub.Priority = maxPriority;
                                persondecreeblocksub.Index = maxIndex;
                            }
                            else
                            {
                                int maxPriority = persondecreeblocksubs.Max(p => p.Priority) + 1;
                                int maxIndex = persondecreeblocksubs.Max(p => p.Index) + 1;

                                persondecreeblocksub.Priority = maxPriority;
                                persondecreeblocksub.Index = maxIndex;
                            }
                        }
                        context.Persondecreeblocksub.Add(persondecreeblocksub);
                        SaveChanges();
                        decreeoperation.Persondecreeblocksub = persondecreeblocksub.Id;
                        existingPersondecreeblocksub = persondecreeblocksub; // Вписываем после создания
                    }
                }
                else
                {
                    // Пытаемся найти, есть ли уже такой подпункт
                    Persondecreeblocksub existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Persondecreeblocksubtype == persondecreeoperation.Persondecreeblocksubtype
                             && p.Subvaluenumber1 == persondecreeoperation.Subvaluenumber1);
                    if (existingPersondecreeblocksub != null)
                    {
                        decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;

                        existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Subvaluedate1 == persondecreeoperation.Optiondate1 && p.Subvaluedate2 == persondecreeoperation.Optiondate3);
                        if (existingPersondecreeblocksub != null)
                        {
                            decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;
                        }
                        else
                        {
                            Persondecreeblocksub persondecreeblocksubsub = new Persondecreeblocksub();
                            persondecreeblocksubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                            persondecreeblocksubsub.Subvaluedate1 = persondecreeoperation.Optiondate1;
                            persondecreeblocksubsub.Subvaluedate2 = persondecreeoperation.Optiondate3;
                            persondecreeblocksubsub.Parentpersondecreeblocksub = decreeoperation.Persondecreeblocksub;

                            context.Persondecreeblocksub.Add(persondecreeblocksubsub);
                            SaveChanges();
                            decreeoperation.Persondecreeblocksub = persondecreeblocksubsub.Id;
                        }
                    }
                    else
                    {
                        Persondecreeblocksub persondecreeblocksub = new Persondecreeblocksub();
                        persondecreeblocksub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                        persondecreeblocksub.Persondecreeblocksubtype = persondecreeoperation.Persondecreeblocksubtype;
                        persondecreeblocksub.Subvaluenumber1 = persondecreeoperation.Subvaluenumber1;
                        persondecreeblocksub.Parentpersondecreeblocksub = decreeoperation.Persondecreeblocksub;

                        context.Persondecreeblocksub.Add(persondecreeblocksub);
                        SaveChanges();
                        decreeoperation.Persondecreeblocksub = persondecreeblocksub.Id;

                        Persondecreeblocksub persondecreeblocksubsub = new Persondecreeblocksub();
                        persondecreeblocksubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock; 
                        persondecreeblocksubsub.Subvaluedate1 = persondecreeoperation.Optiondate1;
                        persondecreeblocksubsub.Subvaluedate2 = persondecreeoperation.Optiondate3;
                        persondecreeblocksubsub.Parentpersondecreeblocksub = persondecreeblocksub.Id;

                        context.Persondecreeblocksub.Add(persondecreeblocksubsub);
                        SaveChanges();
                        decreeoperation.Persondecreeblocksub = persondecreeblocksubsub.Id;

                        // Фабул не было
                        if (persondecreeblockintros.Count == 0)
                        {
                            // Подпунктов не было
                            if (persondecreeblocksubs.Count() == 0)
                            {
                                persondecreeblocksub.Priority = 1;
                                persondecreeblocksub.Index = 1;
                            }
                            else
                            {
                                int maxPriority = persondecreeblocksubs.Max(p => p.Priority) + 1;
                                int maxIndex = persondecreeblocksubs.Max(p => p.Index) + 1;

                                persondecreeblocksub.Priority = maxPriority;
                                persondecreeblocksub.Index = maxIndex;
                            }
                        }
                    }
                }
            }

            // Командировать
            if (persondecreeoperation.Persondecreeblocktype == 16)
            {
                // Пытаемся найти, есть ли уже такой подпункт
                //Persondecreeblocksub existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Persondecreeblocksubtype == persondecreeoperation.Persondecreeblocksubtype);
                Persondecreeblocksub existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Persondecreeblocksubtype == persondecreeoperation.Persondecreeblocksubtype
                         && p.Subvaluenumber1 == persondecreeoperation.Subvaluenumber1 && p.Subvaluenumber2 == persondecreeoperation.Subvaluenumber2
                         && p.Subvaluestring1.Equals(persondecreeoperation.Subvaluestring1) && p.Subvaluestring2.Equals(persondecreeoperation.Subvaluestring2));
                // Подпункт существует
                if (existingPersondecreeblocksub != null)
                {
                    decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;
                }
                else
                {
                    Persondecreeblocksub persondecreeblocksub = new Persondecreeblocksub();
                    persondecreeblocksub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                    persondecreeblocksub.Persondecreeblocksubtype = persondecreeoperation.Persondecreeblocksubtype;
                    persondecreeblocksub.Subvaluenumber1 = persondecreeoperation.Subvaluenumber1;
                    persondecreeblocksub.Subvaluenumber2 = persondecreeoperation.Subvaluenumber2;
                    persondecreeblocksub.Subvaluestring1 = persondecreeoperation.Subvaluestring1;
                    persondecreeblocksub.Subvaluestring2 = persondecreeoperation.Subvaluestring2;
                    //persondecreeblocksub.Persondecreeblockintro = existingPersondecreeblockintro.Id; - фабулы нет

                    // Фабул не было
                    if (persondecreeblockintros.Count == 0)
                    {
                        // Подпунктов не было
                        if (persondecreeblocksubs.Count() == 0)
                        {
                            persondecreeblocksub.Priority = 1;
                            persondecreeblocksub.Index = 1;
                        }
                        else
                        {
                            int maxPriority = persondecreeblocksubs.Max(p => p.Priority) + 1;
                            int maxIndex = persondecreeblocksubs.Max(p => p.Index) + 1;

                            persondecreeblocksub.Priority = maxPriority;
                            persondecreeblocksub.Index = maxIndex;
                        }

                    }
                    // Были фабулы
                    else
                    {
                        if (persondecreeblocksubs.Count() == 0)
                        {
                            int maxPriority = persondecreeblockintros.Max(p => p.Priority) + 1;
                            int maxIndex = persondecreeblockintros.Max(p => p.Index) + 1;

                            persondecreeblocksub.Priority = maxPriority;
                            persondecreeblocksub.Index = maxIndex;
                        }
                        else
                        {
                            int maxPriority = persondecreeblocksubs.Max(p => p.Priority) + 1;
                            int maxIndex = persondecreeblocksubs.Max(p => p.Index) + 1;

                            persondecreeblocksub.Priority = maxPriority;
                            persondecreeblocksub.Index = maxIndex;
                        }
                    }
                    context.Persondecreeblocksub.Add(persondecreeblocksub);
                    SaveChanges();
                    decreeoperation.Persondecreeblocksub = persondecreeblocksub.Id;
                    existingPersondecreeblocksub = persondecreeblocksub; // Вписываем после создания
                }
            }

            // Зачислить
            if (persondecreeoperation.Persondecreeblocktype == 17)
            {
                // Пытаемся найти, есть ли уже такой подпункт
                //Persondecreeblocksub existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Persondecreeblocksubtype == persondecreeoperation.Persondecreeblocksubtype);
                Persondecreeblocksub existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Subvaluedate1 == persondecreeoperation.Optiondate1 &&
                    p.Subvaluenumber1 == persondecreeoperation.Optionnumber6 &&
                    p.Subvaluenumber2 == persondecreeoperation.Optionnumber5 &&
                    p.Subvaluenumber3 == persondecreeoperation.Optionnumber7);
                if (existingPersondecreeblocksub != null)
                {
                    decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;


                    existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Subvaluestring1 == persondecreeoperation.Optionstring5 &&
                    p.Subvaluenumber1 == persondecreeoperation.Optionnumber6 &&
                    p.Subvaluedate2 == persondecreeoperation.Optiondate2 &&
                    p.Subvaluenumber4 == persondecreeoperation.Optionnumber4 &&
                    p.Subvaluenumber2 == persondecreeoperation.Optionnumber8 &&
                    p.Subvaluenumber3 == persondecreeoperation.Optionnumber9 &&
                    p.Parentpersondecreeblocksub == decreeoperation.Persondecreeblocksub);
                    if (existingPersondecreeblocksub != null)
                    {
                        decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;

                        existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Subvaluestring1 == persondecreeoperation.Optionstring4 &&
                        p.Parentpersondecreeblocksub == decreeoperation.Persondecreeblocksub);
                        if (existingPersondecreeblocksub != null)
                        {
                            decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;

                            existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Subvaluestring1 == persondecreeoperation.Optionstring1 &&
                            p.Parentpersondecreeblocksub == decreeoperation.Persondecreeblocksub);
                        if (existingPersondecreeblocksub != null)
                        {
                            decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;
                        }
                        else
                        {
                            Persondecreeblocksub persondecreeblocksubsubsubsub = new Persondecreeblocksub();
                            persondecreeblocksubsubsubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                            persondecreeblocksubsubsubsub.Subvaluestring1 = persondecreeoperation.Optionstring1;
                            persondecreeblocksubsubsubsub.Parentpersondecreeblocksub = decreeoperation.Persondecreeblocksub;

                            context.Persondecreeblocksub.Add(persondecreeblocksubsubsubsub);
                            SaveChanges();
                            decreeoperation.Persondecreeblocksub = persondecreeblocksubsubsubsub.Id;
                        }
                        }
                        else
                        {
                            Persondecreeblocksub persondecreeblocksubsubsub = new Persondecreeblocksub();
                            persondecreeblocksubsubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                            persondecreeblocksubsubsub.Subvaluestring1 = persondecreeoperation.Optionstring4;
                            persondecreeblocksubsubsub.Parentpersondecreeblocksub = decreeoperation.Persondecreeblocksub;

                            context.Persondecreeblocksub.Add(persondecreeblocksubsubsub);
                            SaveChanges();
                            decreeoperation.Persondecreeblocksub = persondecreeblocksubsubsub.Id;

                            Persondecreeblocksub persondecreeblocksubsubsubsub = new Persondecreeblocksub();
                            persondecreeblocksubsubsubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                            persondecreeblocksubsubsubsub.Subvaluestring1 = persondecreeoperation.Optionstring1;
                            persondecreeblocksubsubsubsub.Parentpersondecreeblocksub = persondecreeblocksubsubsub.Id;

                            context.Persondecreeblocksub.Add(persondecreeblocksubsubsubsub);
                            SaveChanges();
                            decreeoperation.Persondecreeblocksub = persondecreeblocksubsubsubsub.Id;
                        }
                    }
                    else
                    {
                        Persondecreeblocksub persondecreeblocksubsub = new Persondecreeblocksub();
                        persondecreeblocksubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                        persondecreeblocksubsub.Subvaluestring1 = persondecreeoperation.Optionstring5;
                        persondecreeblocksubsub.Subvaluestring2 = persondecreeoperation.Optionstring2;
                        persondecreeblocksubsub.Subvaluedate2 = persondecreeoperation.Optiondate2;
                        persondecreeblocksubsub.Subvaluenumber1 = persondecreeoperation.Optionnumber6;
                        persondecreeblocksubsub.Subvaluenumber4 = persondecreeoperation.Optionnumber4;
                        persondecreeblocksubsub.Subvaluenumber2 = persondecreeoperation.Optionnumber8;
                        persondecreeblocksubsub.Subvaluenumber3 = persondecreeoperation.Optionnumber9;
                        persondecreeblocksubsub.Parentpersondecreeblocksub = decreeoperation.Persondecreeblocksub;

                        context.Persondecreeblocksub.Add(persondecreeblocksubsub);
                        SaveChanges();
                        decreeoperation.Persondecreeblocksub = persondecreeblocksubsub.Id;

                        Persondecreeblocksub persondecreeblocksubsubsub = new Persondecreeblocksub();
                        persondecreeblocksubsubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                        persondecreeblocksubsubsub.Subvaluestring1 = persondecreeoperation.Optionstring4;
                        persondecreeblocksubsubsub.Parentpersondecreeblocksub = persondecreeblocksubsub.Id;

                        context.Persondecreeblocksub.Add(persondecreeblocksubsubsub);
                        SaveChanges();
                        decreeoperation.Persondecreeblocksub = persondecreeblocksubsubsub.Id;

                        Persondecreeblocksub persondecreeblocksubsubsubsub = new Persondecreeblocksub();
                        persondecreeblocksubsubsubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                        persondecreeblocksubsubsubsub.Subvaluestring1 = persondecreeoperation.Optionstring1;
                        persondecreeblocksubsubsubsub.Parentpersondecreeblocksub = persondecreeblocksubsubsub.Id;

                        context.Persondecreeblocksub.Add(persondecreeblocksubsubsubsub);
                        SaveChanges();
                        decreeoperation.Persondecreeblocksub = persondecreeblocksubsubsubsub.Id;

                    }
                }
                else
                {
                    Persondecreeblocksub persondecreeblocksub = new Persondecreeblocksub();
                    persondecreeblocksub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                    persondecreeblocksub.Subvaluedate1 = persondecreeoperation.Optiondate1;
                    persondecreeblocksub.Subvaluenumber1 = persondecreeoperation.Optionnumber6;
                    persondecreeblocksub.Subvaluenumber2 = persondecreeoperation.Optionnumber5;
                    persondecreeblocksub.Subvaluenumber3 = persondecreeoperation.Optionnumber7;

                    context.Persondecreeblocksub.Add(persondecreeblocksub);
                    SaveChanges();
                    decreeoperation.Persondecreeblocksub = persondecreeblocksub.Id;

                    Persondecreeblocksub persondecreeblocksubsub = new Persondecreeblocksub();
                    persondecreeblocksubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                    persondecreeblocksubsub.Subvaluestring1 = persondecreeoperation.Optionstring5;
                    persondecreeblocksubsub.Subvaluestring2 = persondecreeoperation.Optionstring2;
                    persondecreeblocksubsub.Subvaluedate2 = persondecreeoperation.Optiondate2;
                    persondecreeblocksubsub.Subvaluenumber1 = persondecreeoperation.Optionnumber6;
                    persondecreeblocksubsub.Subvaluenumber4 = persondecreeoperation.Optionnumber4;
                    persondecreeblocksubsub.Subvaluenumber2 = persondecreeoperation.Optionnumber8;
                    persondecreeblocksubsub.Subvaluenumber3 = persondecreeoperation.Optionnumber9;
                    persondecreeblocksubsub.Parentpersondecreeblocksub = persondecreeblocksub.Id;

                    context.Persondecreeblocksub.Add(persondecreeblocksubsub);
                    SaveChanges();
                    decreeoperation.Persondecreeblocksub = persondecreeblocksubsub.Id;

                    Persondecreeblocksub persondecreeblocksubsubsub = new Persondecreeblocksub();
                    persondecreeblocksubsubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                    persondecreeblocksubsubsub.Subvaluestring1 = persondecreeoperation.Optionstring4;
                    persondecreeblocksubsubsub.Parentpersondecreeblocksub = persondecreeblocksubsub.Id;

                    context.Persondecreeblocksub.Add(persondecreeblocksubsubsub);
                    SaveChanges();
                    decreeoperation.Persondecreeblocksub = persondecreeblocksubsubsub.Id;

                    Persondecreeblocksub persondecreeblocksubsubsubsub = new Persondecreeblocksub();
                    persondecreeblocksubsubsubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                    persondecreeblocksubsubsubsub.Subvaluestring1 = persondecreeoperation.Optionstring1;
                    persondecreeblocksubsubsubsub.Parentpersondecreeblocksub = persondecreeblocksubsubsub.Id;

                    context.Persondecreeblocksub.Add(persondecreeblocksubsubsubsub);
                    SaveChanges();
                    decreeoperation.Persondecreeblocksub = persondecreeblocksubsubsubsub.Id;

                    // Фабул не было
                    if (persondecreeblockintros.Count == 0)
                    {
                        // Подпунктов не было
                        if (persondecreeblocksubs.Count() == 0)
                        {
                            persondecreeblocksub.Priority = 1;
                            persondecreeblocksub.Index = 1;
                        }
                        else
                        {
                            int maxPriority = persondecreeblocksubs.Max(p => p.Priority) + 1;
                            int maxIndex = persondecreeblocksubs.Max(p => p.Index) + 1;

                            persondecreeblocksub.Priority = maxPriority;
                            persondecreeblocksub.Index = maxIndex;
                        }
                    }
                }
            }

            // Отчислить
            if (persondecreeoperation.Persondecreeblocktype == 18)
            {
                switch(persondecreeoperation.Optionnumber11){
                    case 1:
                        {
                            // Пытаемся найти, есть ли уже такой подпункт
                            //Persondecreeblocksub existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Persondecreeblocksubtype == persondecreeoperation.Persondecreeblocksubtype);
                            Persondecreeblocksub existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => 
                            p.Persondecreeblocksubtype == persondecreeoperation.Persondecreeblocksubtype &&
                            (p.Subvaluenumber1 == persondecreeoperation.Optionnumber7 || p.Subvaluedate1 == persondecreeoperation.Optiondate2));
                            // Подпункт существует
                            if (existingPersondecreeblocksub != null)
                            {
                                decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;

                                existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => 
                                p.Subvaluedate1 == persondecreeoperation.Optiondate1 &&
                                p.Subvaluenumber3 == persondecreeoperation.Optionnumber3);
                                if (existingPersondecreeblocksub != null)
                                {
                                    decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;
                                } 
                                else
                                {
                                    Persondecreeblocksub persondecreeblocksubsub = new Persondecreeblocksub();
                                    persondecreeblocksubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                                    persondecreeblocksubsub.Persondecreeblocksubtype = persondecreeoperation.Optionnumber1;
                                    persondecreeblocksubsub.Subvaluenumber1 = persondecreeoperation.Optionnumber8;
                                    persondecreeblocksubsub.Subvaluenumber2 = persondecreeoperation.Subvaluenumber2;
                                    persondecreeblocksubsub.Subvaluenumber3 = persondecreeoperation.Optionnumber3;
                                    persondecreeblocksubsub.Subvaluedate1 = persondecreeoperation.Optiondate1;
                                    persondecreeblocksubsub.Parentpersondecreeblocksub = decreeoperation.Persondecreeblocksub;
                                    persondecreeblocksubsub.Subvaluestring1 = decreeoperation.Optionstring7;

                                    context.Persondecreeblocksub.Add(persondecreeblocksubsub);
                                    SaveChanges();
                                    decreeoperation.Persondecreeblocksub = persondecreeblocksubsub.Id;
                                }
                            }
                            else
                            {
                                Persondecreeblocksub persondecreeblocksub = new Persondecreeblocksub();
                                persondecreeblocksub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                                persondecreeblocksub.Persondecreeblocksubtype = persondecreeoperation.Optionnumber1;
                                persondecreeblocksub.Subvaluenumber1 = persondecreeoperation.Optionnumber7;
                                persondecreeblocksub.Subvaluenumber2 = persondecreeoperation.Optionnumber2;
                                persondecreeblocksub.Subvaluenumber3 = persondecreeoperation.Optionnumber11;
                                persondecreeblocksub.Subvaluestring1 = persondecreeoperation.Subvaluestring1;
                                persondecreeblocksub.Subvaluestring2 = persondecreeoperation.Subvaluestring2;
                                persondecreeblocksub.Parentpersondecreeblocksub = persondecreeoperation.Persondecreeblock;

                                context.Persondecreeblocksub.Add(persondecreeblocksub);
                                SaveChanges();
                                decreeoperation.Persondecreeblocksub = persondecreeblocksub.Id;

                                Persondecreeblocksub persondecreeblocksubsub = new Persondecreeblocksub();
                                persondecreeblocksubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                                persondecreeblocksubsub.Persondecreeblocksubtype = persondecreeoperation.Persondecreeblocksubtype;
                                persondecreeblocksubsub.Subvaluenumber1 = persondecreeoperation.Optionnumber8;
                                persondecreeblocksubsub.Subvaluenumber2 = persondecreeoperation.Subvaluenumber2;
                                persondecreeblocksubsub.Subvaluenumber3 = persondecreeoperation.Optionnumber3;
                                persondecreeblocksubsub.Subvaluedate1 = persondecreeoperation.Optiondate1;
                                persondecreeblocksubsub.Subvaluestring1 = decreeoperation.Optionstring7;
                                persondecreeblocksubsub.Parentpersondecreeblocksub = persondecreeblocksub.Id;

                                context.Persondecreeblocksub.Add(persondecreeblocksubsub);
                                SaveChanges();
                                decreeoperation.Persondecreeblocksub = persondecreeblocksubsub.Id;

                                // Фабул не было
                                if (persondecreeblockintros.Count == 0)
                                {
                                    // Подпунктов не было
                                    if (persondecreeblocksubs.Count() == 0)
                                    {
                                        persondecreeblocksub.Priority = 1;
                                        persondecreeblocksub.Index = 1;   
                                    } 
                                    else
                                    {
                                        int maxPriority = persondecreeblocksubs.Max(p => p.Priority) + 1;
                                        int maxIndex = persondecreeblocksubs.Max(p => p.Index) + 1;      

                                        persondecreeblocksub.Priority = maxPriority;
                                        persondecreeblocksub.Index = maxIndex;      
                                    } 
                                }
                            }
                            break;
                        }
                    case 2:
                        {
                            // Пытаемся найти, есть ли уже такой подпункт
                            //Persondecreeblocksub existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Persondecreeblocksubtype == persondecreeoperation.Persondecreeblocksubtype);
                            Persondecreeblocksub existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => 
                            p.Subvaluedate1 == persondecreeoperation.Optiondate2);
                            // Подпункт существует
                            if (existingPersondecreeblocksub != null)
                            {
                                decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;

                                existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p =>
                                p.Subvaluestring1 == persondecreeoperation.Optionstring1 &&
                                p.Subvaluestring2 == persondecreeoperation.Optionstring2 &&
                                p.Subvaluedate1 == persondecreeoperation.Optiondate3 &&
                                p.Subvaluenumber2 == persondecreeoperation.Optionnumber5);
                                if (existingPersondecreeblocksub != null)
                                {
                                    decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;

                                    existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => 
                                    p.Subvaluenumber1 == persondecreeoperation.Optionnumber6 &&
                                    p.Subvaluenumber2 == persondecreeoperation.Optionnumber7 &&
                                    p.Subvaluenumber3 == persondecreeoperation.Optionnumber8 &&
                                    p.Subvaluenumber4 == persondecreeoperation.Optionnumber9);
                                    if (existingPersondecreeblocksub != null)
                                    {
                                        decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;
                                    }
                                    else
                                    {
                                        Persondecreeblocksub persondecreeblocksubsubsub = new Persondecreeblocksub();
                                        persondecreeblocksubsubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                                        persondecreeblocksubsubsub.Subvaluenumber1 = persondecreeoperation.Optionnumber6;
                                        persondecreeblocksubsubsub.Subvaluenumber2 = persondecreeoperation.Optionnumber7;
                                        persondecreeblocksubsubsub.Subvaluenumber3 = persondecreeoperation.Optionnumber8;
                                        persondecreeblocksubsubsub.Subvaluenumber4 = persondecreeoperation.Optionnumber9;
                                        persondecreeblocksubsubsub.Parentpersondecreeblocksub = decreeoperation.Persondecreeblocksub;

                                        context.Persondecreeblocksub.Add(persondecreeblocksubsubsub);
                                        SaveChanges();
                                        decreeoperation.Persondecreeblocksub = persondecreeblocksubsubsub.Id;
                                    }
                                }
                                else
                                {
                                    Persondecreeblocksub persondecreeblocksubsub = new Persondecreeblocksub();
                                    persondecreeblocksubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                                    persondecreeblocksubsub.Subvaluestring1 = persondecreeoperation.Optionstring1;
                                    persondecreeblocksubsub.Subvaluestring2 = persondecreeoperation.Optionstring2;
                                    persondecreeblocksubsub.Subvaluedate1 = persondecreeoperation.Optiondate3;
                                    persondecreeblocksubsub.Subvaluenumber2 = decreeoperation.Optionnumber5;
                                    persondecreeblocksubsub.Parentpersondecreeblocksub = decreeoperation.Persondecreeblocksub;

                                    context.Persondecreeblocksub.Add(persondecreeblocksubsub);
                                    SaveChanges();
                                    decreeoperation.Persondecreeblocksub = persondecreeblocksubsub.Id;

                                    Persondecreeblocksub persondecreeblocksubsubsub = new Persondecreeblocksub();
                                    persondecreeblocksubsubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                                    persondecreeblocksubsubsub.Subvaluenumber1 = persondecreeoperation.Optionnumber6;
                                    persondecreeblocksubsubsub.Subvaluenumber2 = persondecreeoperation.Optionnumber7;
                                    persondecreeblocksubsubsub.Subvaluenumber3 = persondecreeoperation.Optionnumber8;
                                    persondecreeblocksubsubsub.Subvaluenumber4 = persondecreeoperation.Optionnumber9;
                                    persondecreeblocksubsubsub.Parentpersondecreeblocksub = decreeoperation.Persondecreeblocksub;

                                    context.Persondecreeblocksub.Add(persondecreeblocksubsubsub);
                                    SaveChanges();
                                    decreeoperation.Persondecreeblocksub = persondecreeblocksubsubsub.Id;
                                }
                            }
                            else
                            {
                                Persondecreeblocksub persondecreeblocksub = new Persondecreeblocksub();
                                persondecreeblocksub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                                persondecreeblocksub.Subvaluedate1 = persondecreeoperation.Optiondate2;
                                persondecreeblocksub.Subvaluenumber3 = persondecreeoperation.Optionnumber11;
                                persondecreeblocksub.Parentpersondecreeblocksub = decreeoperation.Persondecreeblocksub;

                                context.Persondecreeblocksub.Add(persondecreeblocksub);
                                SaveChanges();
                                decreeoperation.Persondecreeblocksub = persondecreeblocksub.Id;

                                Persondecreeblocksub persondecreeblocksubsub = new Persondecreeblocksub();
                                persondecreeblocksubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                                persondecreeblocksubsub.Subvaluestring1 = persondecreeoperation.Optionstring1;
                                persondecreeblocksubsub.Subvaluestring2 = persondecreeoperation.Optionstring2;
                                persondecreeblocksubsub.Subvaluedate1 = persondecreeoperation.Optiondate3;
                                persondecreeblocksubsub.Subvaluenumber2 = decreeoperation.Optionnumber5;
                                persondecreeblocksubsub.Parentpersondecreeblocksub = decreeoperation.Persondecreeblocksub;

                                context.Persondecreeblocksub.Add(persondecreeblocksubsub);
                                SaveChanges();
                                decreeoperation.Persondecreeblocksub = persondecreeblocksubsub.Id;

                                Persondecreeblocksub persondecreeblocksubsubsub = new Persondecreeblocksub();
                                persondecreeblocksubsubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                                persondecreeblocksubsubsub.Subvaluenumber1 = persondecreeoperation.Optionnumber6;
                                persondecreeblocksubsubsub.Subvaluenumber2 = persondecreeoperation.Optionnumber7;
                                persondecreeblocksubsubsub.Subvaluenumber3 = persondecreeoperation.Optionnumber8;
                                persondecreeblocksubsubsub.Subvaluenumber4 = persondecreeoperation.Optionnumber9;
                                persondecreeblocksubsubsub.Parentpersondecreeblocksub = decreeoperation.Persondecreeblocksub;

                                context.Persondecreeblocksub.Add(persondecreeblocksubsubsub);
                                SaveChanges();
                                decreeoperation.Persondecreeblocksub = persondecreeblocksubsubsub.Id;

                                // Фабул не было
                                if (persondecreeblockintros.Count == 0)
                                {
                                    // Подпунктов не было
                                    if (persondecreeblocksubs.Count() == 0)
                                    {
                                        persondecreeblocksub.Priority = 1;
                                        persondecreeblocksub.Index = 1;
                                    }
                                    else
                                    {
                                        int maxPriority = persondecreeblocksubs.Max(p => p.Priority) + 1;
                                        int maxIndex = persondecreeblocksubs.Max(p => p.Index) + 1;

                                        persondecreeblocksub.Priority = maxPriority;
                                        persondecreeblocksub.Index = maxIndex;
                                    }
                                }
                            }
                            break;
                        }
                    default: 
                        break;
                }

            }

            // Увеличить
            if (persondecreeoperation.Persondecreeblocktype == 19)
            {
                // Пытаемся найти, есть ли уже такой подпункт
                //Persondecreeblocksub existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Persondecreeblocksubtype == persondecreeoperation.Persondecreeblocksubtype);
                Persondecreeblocksub existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Subvaluestring1 == persondecreeoperation.Optionstring1);
                if (existingPersondecreeblocksub != null)
                {
                    decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;

                    existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Subvaluedate1 == persondecreeoperation.Optiondate1);
                    if (existingPersondecreeblocksub != null)
                    {
                        decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;
                    }
                    else
                    {
                        Persondecreeblocksub persondecreeblocksubsub = new Persondecreeblocksub();
                        persondecreeblocksubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                        persondecreeblocksubsub.Subvaluedate1 = persondecreeoperation.Optiondate1;
                        persondecreeblocksubsub.Parentpersondecreeblocksub = decreeoperation.Persondecreeblocksub;
                        persondecreeblocksubsub.Subvaluestring1 = persondecreeoperation.Optionstring2;
                        persondecreeblocksubsub.Parentpersondecreeblocksub = decreeoperation.Persondecreeblocksub;

                        context.Persondecreeblocksub.Add(persondecreeblocksubsub);
                        SaveChanges();
                        decreeoperation.Persondecreeblocksub = persondecreeblocksubsub.Id;
                    }
                }
                else
                {
                    Persondecreeblocksub persondecreeblocksub = new Persondecreeblocksub();
                    persondecreeblocksub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                    persondecreeblocksub.Persondecreeblocksubtype = persondecreeoperation.Persondecreeblocksubtype;
                    persondecreeblocksub.Subvaluestring1 = persondecreeoperation.Optionstring1;
                    persondecreeblocksub.Parentpersondecreeblocksub = decreeoperation.Persondecreeblocksub;

                    context.Persondecreeblocksub.Add(persondecreeblocksub);
                    SaveChanges();
                    decreeoperation.Persondecreeblocksub = persondecreeblocksub.Id;

                    Persondecreeblocksub persondecreeblocksubsub = new Persondecreeblocksub();
                    persondecreeblocksubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                    persondecreeblocksubsub.Persondecreeblocksubtype = persondecreeoperation.Persondecreeblocksubtype;
                    persondecreeblocksubsub.Subvaluedate1 = persondecreeoperation.Optiondate1;
                    persondecreeblocksubsub.Subvaluestring1 = persondecreeoperation.Optionstring2;
                    persondecreeblocksubsub.Parentpersondecreeblocksub = persondecreeblocksub.Id;

                    context.Persondecreeblocksub.Add(persondecreeblocksubsub);
                    SaveChanges();
                    decreeoperation.Persondecreeblocksub = persondecreeblocksubsub.Id;

                    // Фабул не было
                    if (persondecreeblockintros.Count == 0)
                    {
                        // Подпунктов не было
                        if (persondecreeblocksubs.Count() == 0)
                        {
                            persondecreeblocksub.Priority = 1;
                            persondecreeblocksub.Index = 1;
                        }
                        else
                        {
                            int maxPriority = persondecreeblocksubs.Max(p => p.Priority) + 1;
                            int maxIndex = persondecreeblocksubs.Max(p => p.Index) + 1;

                            persondecreeblocksub.Priority = maxPriority;
                            persondecreeblocksub.Index = maxIndex;
                        }
                    }
                }
            }

            //Восстановить
            if (persondecreeoperation.Persondecreeblocktype == 20)
            {
                // Пытаемся найти, есть ли уже такой подпункт
                //Persondecreeblocksub existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Persondecreeblocksubtype == persondecreeoperation.Persondecreeblocksubtype);
                Persondecreeblocksub existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Subvaluestring1 == persondecreeoperation.Optionstring3 &&
                p.Subvaluestring2 == persondecreeoperation.Optionstring4);
                if (existingPersondecreeblocksub != null)
                {
                    decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;

                    existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Subvaluestring1 == persondecreeoperation.Optionstring2 && p.Subvaluestring2 == persondecreeoperation.Optionnumber2.ToString());
                    if (existingPersondecreeblocksub != null)
                    {
                        decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;
                        
                        existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Subvaluedate1 == persondecreeoperation.Optiondate1);
                        if (existingPersondecreeblocksub != null)
                        {
                            decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;
                        }
                        else
                        {
                            Persondecreeblocksub persondecreeblocksubsubsub = new Persondecreeblocksub();
                            persondecreeblocksubsubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                            persondecreeblocksubsubsub.Persondecreeblocksubtype = persondecreeoperation.Persondecreeblocksubtype;
                            persondecreeblocksubsubsub.Subvaluedate1 = persondecreeoperation.Optiondate1;
                            persondecreeblocksubsubsub.Parentpersondecreeblocksub = decreeoperation.Persondecreeblocksub;

                            context.Persondecreeblocksub.Add(persondecreeblocksubsubsub);
                            SaveChanges();
                            decreeoperation.Persondecreeblocksub = persondecreeblocksubsubsub.Id;
                        }
                    }
                    else
                    {
                        Persondecreeblocksub persondecreeblocksubsub = new Persondecreeblocksub();
                        persondecreeblocksubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                        persondecreeblocksubsub.Subvaluestring1 = persondecreeoperation.Optionstring2;
                        persondecreeblocksubsub.Subvaluestring2 = persondecreeoperation.Optionnumber2.ToString();
                        persondecreeblocksubsub.Parentpersondecreeblocksub = decreeoperation.Persondecreeblocksub;

                        context.Persondecreeblocksub.Add(persondecreeblocksubsub);
                        SaveChanges();
                        decreeoperation.Persondecreeblocksub = persondecreeblocksubsub.Id;

                        Persondecreeblocksub persondecreeblocksubsubsub = new Persondecreeblocksub();
                        persondecreeblocksubsubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                        persondecreeblocksubsubsub.Persondecreeblocksubtype = persondecreeoperation.Persondecreeblocksubtype;
                        persondecreeblocksubsubsub.Subvaluedate1 = persondecreeoperation.Optiondate1;
                        persondecreeblocksubsubsub.Parentpersondecreeblocksub = persondecreeblocksubsub.Id;

                        context.Persondecreeblocksub.Add(persondecreeblocksubsubsub);
                        SaveChanges();
                        decreeoperation.Persondecreeblocksub = persondecreeblocksubsubsub.Id;
                    }
                }
                else
                {
                    Persondecreeblocksub persondecreeblocksub = new Persondecreeblocksub();
                    persondecreeblocksub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                    persondecreeblocksub.Persondecreeblocksubtype = persondecreeoperation.Persondecreeblocksubtype;
                    persondecreeblocksub.Subvaluestring1 = persondecreeoperation.Optionstring3;
                    persondecreeblocksub.Subvaluestring2 = persondecreeoperation.Optionstring4;

                    context.Persondecreeblocksub.Add(persondecreeblocksub);
                    SaveChanges();
                    decreeoperation.Persondecreeblocksub = persondecreeblocksub.Id;

                    Persondecreeblocksub persondecreeblocksubsub = new Persondecreeblocksub();
                    persondecreeblocksubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                    persondecreeblocksubsub.Persondecreeblocksubtype = persondecreeoperation.Persondecreeblocksubtype;
                    persondecreeblocksubsub.Subvaluestring1 = persondecreeoperation.Optionstring2;
                    persondecreeblocksubsub.Subvaluestring2 = persondecreeoperation.Optionnumber2.ToString();
                    persondecreeblocksubsub.Parentpersondecreeblocksub = persondecreeblocksub.Id;

                    context.Persondecreeblocksub.Add(persondecreeblocksubsub);
                    SaveChanges();
                    decreeoperation.Persondecreeblocksub = persondecreeblocksubsub.Id;

                    Persondecreeblocksub persondecreeblocksubsubsub = new Persondecreeblocksub();
                    persondecreeblocksubsubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                    persondecreeblocksubsubsub.Persondecreeblocksubtype = persondecreeoperation.Persondecreeblocksubtype;
                    persondecreeblocksubsubsub.Subvaluedate1 = persondecreeoperation.Optiondate1;
                    persondecreeblocksubsubsub.Parentpersondecreeblocksub = persondecreeblocksubsub.Id;

                    context.Persondecreeblocksub.Add(persondecreeblocksubsubsub);
                    SaveChanges();
                    decreeoperation.Persondecreeblocksub = persondecreeblocksubsubsub.Id;

                    // Фабул не было
                    if (persondecreeblockintros.Count == 0)
                    {
                        // Подпунктов не было
                        if (persondecreeblocksubs.Count() == 0)
                        {
                            persondecreeblocksub.Priority = 1;
                            persondecreeblocksub.Index = 1;
                        }
                        else
                        {
                            int maxPriority = persondecreeblocksubs.Max(p => p.Priority) + 1;
                            int maxIndex = persondecreeblocksubs.Max(p => p.Index) + 1;

                            persondecreeblocksub.Priority = maxPriority;
                            persondecreeblocksub.Index = maxIndex;
                        }
                    }
                }
            }

            //Перевести(УГЗ)
            if (persondecreeoperation.Persondecreeblocktype == 21)
            {
                // Пытаемся найти, есть ли уже такой подпункт
                //Persondecreeblocksub existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Persondecreeblocksubtype == persondecreeoperation.Persondecreeblocksubtype);
                Persondecreeblocksub existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Subvaluestring1 == persondecreeoperation.Optionstring1);
                if (existingPersondecreeblocksub != null)
                {
                    decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;

                    existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => p.Subvaluestring1 == persondecreeoperation.Optionstring6 );
                    if (existingPersondecreeblocksub != null)
                    {
                        decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;

                        existingPersondecreeblocksub = persondecreeblocksubs.FirstOrDefault(p => 
                           p.Subvaluestring1 == persondecreeoperation.Optionstring4
                        && p.Subvaluedate1 == persondecreeoperation.Optiondate1 
                        && p.Subvaluenumber1 == persondecreeoperation.Optionnumber2 
                        && p.Subvaluedate2 == persondecreeoperation.Optiondate2);
                        if (existingPersondecreeblocksub != null)
                        {
                            decreeoperation.Persondecreeblocksub = existingPersondecreeblocksub.Id;
                        }
                        else
                        {
                            Persondecreeblocksub persondecreeblocksubsubsub = new Persondecreeblocksub();
                            persondecreeblocksubsubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                            persondecreeblocksubsubsub.Subvaluestring1 = persondecreeoperation.Optionstring4;
                            persondecreeblocksubsubsub.Subvaluenumber1 = persondecreeoperation.Optionnumber2;
                            persondecreeblocksubsubsub.Subvaluedate1 = persondecreeoperation.Optiondate1;
                            persondecreeblocksubsubsub.Subvaluedate2 = persondecreeoperation.Optiondate2;
                            persondecreeblocksubsubsub.Parentpersondecreeblocksub = decreeoperation.Persondecreeblocksub;

                            context.Persondecreeblocksub.Add(persondecreeblocksubsubsub);
                            SaveChanges();
                            decreeoperation.Persondecreeblocksub = persondecreeblocksubsubsub.Id;

                            if (persondecreeblocksubs.Count() == 0)
                            {
                                persondecreeblocksubsubsub.Priority = 1;
                                persondecreeblocksubsubsub.Index = 1;
                            }
                            else
                            {
                                int maxPriority = persondecreeblocksubs.Max(p => p.Priority) + 1;
                                int maxIndex = persondecreeblocksubs.Max(p => p.Index) + 1;

                                persondecreeblocksubsubsub.Priority = maxPriority;
                                persondecreeblocksubsubsub.Index = maxIndex;
                            }

                        }
                    }
                    else
                    {
                        Persondecreeblocksub persondecreeblocksubsub = new Persondecreeblocksub();
                        persondecreeblocksubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                        persondecreeblocksubsub.Subvaluestring1 = persondecreeoperation.Optionstring6;
                        persondecreeblocksubsub.Parentpersondecreeblocksub = decreeoperation.Persondecreeblocksub;

                        context.Persondecreeblocksub.Add(persondecreeblocksubsub);
                        SaveChanges();
                        decreeoperation.Persondecreeblocksub = persondecreeblocksubsub.Id;

                        Persondecreeblocksub persondecreeblocksubsubsub = new Persondecreeblocksub();
                        persondecreeblocksubsubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                        persondecreeblocksubsubsub.Subvaluestring1 = persondecreeoperation.Optionstring4;
                        persondecreeblocksubsubsub.Subvaluenumber1 = persondecreeoperation.Optionnumber2;
                        persondecreeblocksubsubsub.Subvaluedate1 = persondecreeoperation.Optiondate1;
                        persondecreeblocksubsubsub.Subvaluedate2 = persondecreeoperation.Optiondate2;
                        persondecreeblocksubsubsub.Parentpersondecreeblocksub = persondecreeblocksubsub.Id;

                        context.Persondecreeblocksub.Add(persondecreeblocksubsubsub);
                        SaveChanges();
                        decreeoperation.Persondecreeblocksub = persondecreeblocksubsubsub.Id;

                        if (persondecreeblocksubs.Count() == 0)
                        {
                            persondecreeblocksubsubsub.Priority = 1;
                            persondecreeblocksubsubsub.Index = 1;
                        }
                        else
                        {
                            int maxPriority = persondecreeblocksubs.Max(p => p.Priority) + 1;
                            int maxIndex = persondecreeblocksubs.Max(p => p.Index) + 1;

                            persondecreeblocksubsubsub.Priority = maxPriority;
                            persondecreeblocksubsubsub.Index = maxIndex;
                        }
                    }
                }
                else
                { 
                    Persondecreeblocksub persondecreeblocksub = new Persondecreeblocksub();
                    persondecreeblocksub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                    persondecreeblocksub.Persondecreeblocksubtype = persondecreeoperation.Persondecreeblocksubtype;
                    persondecreeblocksub.Subvaluestring1 = persondecreeoperation.Optionstring1;

                    context.Persondecreeblocksub.Add(persondecreeblocksub);
                    SaveChanges();
                    decreeoperation.Persondecreeblocksub = persondecreeblocksub.Id;

                    Persondecreeblocksub persondecreeblocksubsub = new Persondecreeblocksub();
                    persondecreeblocksubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                    persondecreeblocksubsub.Subvaluestring1 = persondecreeoperation.Optionstring6;
                    persondecreeblocksubsub.Parentpersondecreeblocksub = persondecreeblocksub.Id;

                    context.Persondecreeblocksub.Add(persondecreeblocksubsub);
                    SaveChanges();
                    decreeoperation.Persondecreeblocksub = persondecreeblocksubsub.Id;

                    Persondecreeblocksub persondecreeblocksubsubsub = new Persondecreeblocksub();
                    persondecreeblocksubsubsub.Persondecreeblock = persondecreeoperation.Persondecreeblock;
                    persondecreeblocksubsubsub.Subvaluestring1 = persondecreeoperation.Optionstring4;
                    persondecreeblocksubsubsub.Subvaluenumber1 = persondecreeoperation.Optionnumber2;
                    persondecreeblocksubsubsub.Subvaluedate1 = persondecreeoperation.Optiondate1;
                    persondecreeblocksubsubsub.Subvaluedate2 = persondecreeoperation.Optiondate2;
                    persondecreeblocksubsubsub.Parentpersondecreeblocksub = persondecreeblocksubsub.Id;

                    context.Persondecreeblocksub.Add(persondecreeblocksubsubsub);
                    SaveChanges();
                    decreeoperation.Persondecreeblocksub = persondecreeblocksubsubsub.Id;

                    // Подпунктов не было
                    if (persondecreeblocksubs.Count() == 0)
                    {
                        persondecreeblocksubsubsub.Priority = 1;
                        persondecreeblocksubsubsub.Index = 1;
                    }
                    else
                    {
                        int maxPriority = persondecreeblocksubs.Max(p => p.Priority) + 1;
                        int maxIndex = persondecreeblocksubs.Max(p => p.Index) + 1;
                        persondecreeblocksubsubsub.Priority = maxPriority;
                        persondecreeblocksubsubsub.Index = maxIndex;
                    }
                }
            }


            context.Persondecreeoperation.Add(decreeoperation);
            SaveChanges();

            UpdatePersondecreeoperationsLocal();

            // Обновляем ту информацию, которая по умолчанию будет в блоке.
            Persondecreeblock persondecreeblock = Persondecreeblocks.FirstOrDefault(p => p.Id == persondecreeoperation.Persondecreeblock);
            if (persondecreeblock != null)
            {
                // Не для всех мы сохраняем в блоке информацию для дальнейшего заполнения. Например, для отпусков обнуляем.
                if (persondecreeoperation.Persondecreeblocktype == 15)
                {
                    persondecreeblock.Intro = "";
                    //persondecreeblock.Persondecreeblocksub = 0;
                    persondecreeblock.Persondecreeblocksub = persondecreeoperation.Persondecreeblocksubtype;
                    persondecreeblock.Optionnumber1 = 0;
                    persondecreeblock.Optionnumber2 = 0;
                    persondecreeblock.Optionnumber3 = 0;
                    persondecreeblock.Optionnumber4 = 0;
                    persondecreeblock.Optionnumber5 = 0;
                    persondecreeblock.Optionnumber6 = 0;
                    persondecreeblock.Optionnumber7 = 0;
                    persondecreeblock.Optionnumber8 = 0;
                    persondecreeblock.Optionnumber9 = 0;
                    persondecreeblock.Optionnumber10 = 0;
                    persondecreeblock.Optionnumber11 = 0;
                    persondecreeblock.Optionstring1 = "";
                    persondecreeblock.Optionstring2 = "";
                    persondecreeblock.Optionstring3 = "";
                    persondecreeblock.Optionstring4 = "";
                    persondecreeblock.Optionstring5 = "";
                    persondecreeblock.Optionstring6 = "";
                    persondecreeblock.Optionstring7 = "";
                    persondecreeblock.Optionstring8 = "";
                    persondecreeblock.Optiondate1 = null;
                    persondecreeblock.Optiondate2 = null;
                    persondecreeblock.Optiondate3 = null;
                    persondecreeblock.Optiondate4 = null;
                    persondecreeblock.Optiondate5 = null;
                    persondecreeblock.Optiondate6 = null;
                    persondecreeblock.Optiondate7 = null;
                    persondecreeblock.Optiondate8 = null;
                    //persondecreeblock.Subvaluenumber1 = 0;
                    persondecreeblock.Subvaluenumber1 = persondecreeoperation.Subvaluenumber1;
                    persondecreeblock.Subvaluenumber2 = 0;
                    persondecreeblock.Subvaluestring1 = "";
                    persondecreeblock.Subvaluestring2 = "";
                } else
                {
                    persondecreeblock.Intro = persondecreeoperation.Intro;
                    persondecreeblock.Persondecreeblocksub = persondecreeoperation.Persondecreeblocksubtype;
                    persondecreeblock.Optionnumber1 = persondecreeoperation.Optionnumber1;
                    persondecreeblock.Optionnumber2 = persondecreeoperation.Optionnumber2;
                    persondecreeblock.Optionnumber3 = persondecreeoperation.Optionnumber3;
                    persondecreeblock.Optionnumber4 = persondecreeoperation.Optionnumber4;
                    persondecreeblock.Optionnumber5 = persondecreeoperation.Optionnumber5;
                    persondecreeblock.Optionnumber6 = persondecreeoperation.Optionnumber6;
                    persondecreeblock.Optionnumber7 = persondecreeoperation.Optionnumber7;
                    persondecreeblock.Optionnumber8 = persondecreeoperation.Optionnumber8;
                    persondecreeblock.Optionnumber9 = persondecreeoperation.Optionnumber9;
                    persondecreeblock.Optionnumber10 = persondecreeoperation.Optionnumber10;
                    persondecreeblock.Optionnumber11 = persondecreeoperation.Optionnumber11;
                    persondecreeblock.Optionstring1 = persondecreeoperation.Optionstring1;
                    persondecreeblock.Optionstring2 = persondecreeoperation.Optionstring2;
                    persondecreeblock.Optionstring3 = persondecreeoperation.Optionstring3;
                    persondecreeblock.Optionstring4 = persondecreeoperation.Optionstring4;
                    persondecreeblock.Optionstring5 = persondecreeoperation.Optionstring5;
                    persondecreeblock.Optionstring6 = persondecreeoperation.Optionstring6;
                    persondecreeblock.Optionstring7 = persondecreeoperation.Optionstring7;
                    persondecreeblock.Optionstring8 = persondecreeoperation.Optionstring8;
                    persondecreeblock.Optiondate1 = persondecreeoperation.Optiondate1;
                    persondecreeblock.Optiondate2 = persondecreeoperation.Optiondate2;
                    persondecreeblock.Optiondate3 = persondecreeoperation.Optiondate3;
                    persondecreeblock.Optiondate4 = persondecreeoperation.Optiondate4;
                    persondecreeblock.Optiondate5 = persondecreeoperation.Optiondate5;
                    persondecreeblock.Optiondate6 = persondecreeoperation.Optiondate6;
                    persondecreeblock.Optiondate7 = persondecreeoperation.Optiondate7;
                    persondecreeblock.Optiondate8 = persondecreeoperation.Optiondate8;
                    persondecreeblock.Subvaluenumber1 = persondecreeoperation.Subvaluenumber1;
                    persondecreeblock.Subvaluenumber2 = persondecreeoperation.Subvaluenumber2;
                    persondecreeblock.Subvaluestring1 = persondecreeoperation.Subvaluestring1;
                    persondecreeblock.Subvaluestring2 = persondecreeoperation.Subvaluestring2;
                }
                

                // Присвоить. Мы помечаем информацию в intro "младший, средний и старший"
                if (persondecreeoperation.Persondecreeblocktype == 14)
                {
                    // positioncategory 2 - младший нач состав.
                    // 3 - средний нач состав.
                    // 4 - старший нач состав
                    bool junior = false;
                    bool middle = false;
                    bool senior = false;
                    IQueryable<Persondecreeblocksub> persondecreeblocksubsBlock = context.Persondecreeblocksub.Where(p => p.Persondecreeblock == persondecreeblock.Id);
                    foreach (Persondecreeblocksub persondecreeblocksub in persondecreeblocksubsBlock)
                    {
                        Rank rank = RanksLocal().GetValue(persondecreeblocksub.Persondecreeblocksubtype);
                        if (rank != null)
                        {
                            if (rank.Positioncategory == 2)
                            {
                                junior = true;
                            }
                            if (rank.Positioncategory == 3)
                            {
                                middle = true;
                            }
                            if (rank.Positioncategory == 4)
                            {
                                senior = true;
                            }
                        }
                    }
                    List<string> words = new List<string>();
                    string finalIntro = "";
                    if (junior)
                    {
                        words.Add("младшего");
                    } 
                    if (middle)
                    {
                        words.Add("среднего");
                    }
                    if (senior)
                    {
                        words.Add("старшего");
                    }
                    if (words.Count == 1)
                    {
                        finalIntro = words[0];
                    }
                    if (words.Count == 2)
                    {
                        finalIntro = words[0] + " и " + words[1];
                    }
                    if (words.Count == 3)
                    {
                        finalIntro = words[0] + ", " + words[1] + " и " + words[2];
                    }

                    persondecreeblock.Intro = finalIntro;
                }
                



                SaveChanges();
                //persondecreeblock.Persondecreeblocktype
            }

            return decreeoperation;

        }

        public void RemovePersonDecreeoperation(User user, PersondecreeoperationManagement persondecreeoperation)
        {
            Persondecreeoperation contextPersondecreeoperation = Persondecreeoperations.FirstOrDefault(p => p.Id == persondecreeoperation.Id);
            if (contextPersondecreeoperation == null)
            {
                return; // Не нашли
            }
            Persondecreeblockintro persondecreeblockintro = null;
            Persondecreeblocksub persondecreeblocksub = null;

            // Пытаемся найти фабулу, связанный с операцией, если есть.
            if (contextPersondecreeoperation.Persondecreeblockintro != 0)
            {
                persondecreeblockintro = Persondecreeblockintros.FirstOrDefault(p => p.Id == contextPersondecreeoperation.Persondecreeblockintro);
            }
            // Пытаемся найти подблок, связанный с операцией, если есть
            if (contextPersondecreeoperation.Persondecreeblocksub != 0)
            {
                persondecreeblocksub = Persondecreeblocksubs.FirstOrDefault(p => p.Id == contextPersondecreeoperation.Persondecreeblocksub);
            }

            if (persondecreeblockintro != null)
            {
                List<Persondecreeoperation> persondecreeoperationsIntro = Persondecreeoperations.Where(p => p.Persondecreeblockintro == persondecreeblockintro.Id).ToList();
                // Означает, что этот элемент последний в Фабуле, поэтому саму фабулу тоже надо удалить и сместить индексы.
                if (persondecreeoperationsIntro.Count == 1)
                {
                    context.Persondecreeblockintro.Remove(persondecreeblockintro); // Удаляем

                    int indexIntro = persondecreeblockintro.Index;
                    IQueryable<Persondecreeblockintro> persondecreeblockintros = Persondecreeblockintros.Where(p => p.Persondecreeblock == persondecreeblocksub.Persondecreeblock && p.Index != 0);
                    IQueryable<Persondecreeblocksub> persondecreeblocksubs = Persondecreeblocksubs.Where(p => p.Persondecreeblock == persondecreeblocksub.Persondecreeblock && p.Index != 0);
                    foreach (Persondecreeblockintro p in persondecreeblockintros)
                    {
                        if (p.Index > indexIntro)
                        {
                            p.Index -= 1;
                        }
                    }

                    foreach (Persondecreeblocksub p in persondecreeblocksubs)
                    {
                        if (p.Index > indexIntro)
                        {
                            p.Index -= 1;
                        }
                    }

                    
                }
            }

            if (persondecreeblocksub != null)
            {
                //List<Persondecreeoperation> persondecreeoperationsSub = Persondecreeoperations.Where(p => p.Persondecreeblocksub == persondecreeblocksub.Id).ToList();
                List<Persondecreeoperation> persondecreeoperationsSub = Persondecreeoperations.Where(p => p.Persondecreeblocksub == persondecreeblocksub.Id 
                         && p.Subvaluenumber1 == persondecreeblocksub.Subvaluenumber1 && p.Subvaluenumber2 == persondecreeblocksub.Subvaluenumber2
                         && p.Subvaluestring1.Equals(persondecreeblocksub.Subvaluestring1) && p.Subvaluestring2.Equals(persondecreeblocksub.Subvaluestring2)).ToList();
                // Означает, что этот элемент последний в Фабуле, поэтому саму фабулу тоже надо удалить и сместить индексы.
                if (persondecreeoperationsSub.Count == 1)
                {
                    int indexSub = persondecreeblocksub.Index;
                    context.Persondecreeblocksub.Remove(persondecreeblocksub); // Удаляем

                    // Означает, что подблок расположен вне фабулы, поэтому вся нумерация подблоков выше этого станет меньше на 1
                    // Если внутри фабулы, то мы ничего не делаем.
                    if (persondecreeblockintro == null)
                    {
                        IQueryable<Persondecreeblocksub> persondecreeblocksubs = Persondecreeblocksubs.Where(p => p.Persondecreeblock == persondecreeblocksub.Persondecreeblock && p.Index != 0);

                        foreach (Persondecreeblocksub p in persondecreeblocksubs)
                        {
                            if (p.Index > indexSub)
                            {
                                p.Index -= 1;
                            }
                        }
                    }
                }
            }
            

            if (contextPersondecreeoperation.Subjecttype == 1) // Награда. Прикреплено к награде.
            {
                Personreward contextPersonreward = Personrewards.FirstOrDefault(p => p.Id == contextPersondecreeoperation.Subjectid);
                if (contextPersonreward != null)
                {
                    context.Personreward.Remove(contextPersonreward);
                }
            }

            context.Persondecreeoperation.Remove(contextPersondecreeoperation);
            SaveChanges(); // Удаляем и саму операцию и объект.
            UpdatePersondecreeoperationsLocal();

            // Присвоить. Мы помечаем информацию в intro "младший, средний и старший"
            if (contextPersondecreeoperation.Persondecreeblocktype == 14)
            {
                Persondecreeblock persondecreeblock = context.Persondecreeblock.FirstOrDefault(p => p.Id == contextPersondecreeoperation.Persondecreeblock);

                // positioncategory 2 - младший нач состав.
                // 3 - средний нач состав.
                // 4 - старший нач состав
                bool junior = false;
                bool middle = false;
                bool senior = false;
                IQueryable<Persondecreeblocksub> persondecreeblocksubsBlock = context.Persondecreeblocksub.Where(p => p.Persondecreeblock == persondecreeblock.Id);
                foreach (Persondecreeblocksub persondecreeblocksubBlock in persondecreeblocksubsBlock)
                {
                    Rank rank = RanksLocal().GetValue(persondecreeblocksubBlock.Persondecreeblocksubtype);
                    if (rank != null)
                    {
                        if (rank.Positioncategory == 2)
                        {
                            junior = true;
                        }
                        if (rank.Positioncategory == 3)
                        {
                            middle = true;
                        }
                        if (rank.Positioncategory == 4)
                        {
                            senior = true;
                        }
                    }
                }
                List<string> words = new List<string>();
                string finalIntro = "";
                if (junior)
                {
                    words.Add("младшего");
                }
                if (middle)
                {
                    words.Add("среднего");
                }
                if (senior)
                {
                    words.Add("старшего");
                }
                if (words.Count == 1)
                {
                    finalIntro = words[0];
                }
                if (words.Count == 2)
                {
                    finalIntro = words[0] + " и " + words[1];
                }
                if (words.Count == 3)
                {
                    finalIntro = words[0] + ", " + words[1] + " и " + words[2];
                }

                persondecreeblock.Intro = finalIntro;
                SaveChanges();
            }

            
        }

        public void UpdatePersonDecreeoperation(User user, PersondecreeoperationManagement persondecreeoperation)
        {
            if (persondecreeoperation.Subjecttype == 1 && persondecreeoperation.Personreward != null) // обновляем награду
            {
                Personreward contextPersonreward = Personrewards.FirstOrDefault(p => p.Id == persondecreeoperation.Subjectid);
                if (contextPersonreward == null)
                {
                    return; // Почему-то нет.
                }

                if (persondecreeoperation.Personreward.Order == null)
                {
                    persondecreeoperation.Personreward.Order = ""; // Не может быть нулем
                }
                contextPersonreward.Order = persondecreeoperation.Personreward.Order;
                contextPersonreward.Reason = persondecreeoperation.Personreward.Reason;
                contextPersonreward.Reward = persondecreeoperation.Personreward.Reward;
                contextPersonreward.Rewardtype = persondecreeoperation.Personreward.Rewardtype;
                contextPersonreward.Rewarddate = persondecreeoperation.Personreward.Rewarddate;

                Persondecreeoperation contextDecreeoperation = Persondecreeoperations.FirstOrDefault(p => p.Id == persondecreeoperation.Id);
                if (contextDecreeoperation == null)
                {
                    return; // Если почему-то 
                }

                contextDecreeoperation.Persondecreeblock = persondecreeoperation.Persondecreeblock;

                SaveChanges();
                UpdatePersondecreeoperationsLocal();
            }

        }

        /// <summary>
        /// Берет все блоки проекта приказа прохождения службы
        /// </summary>
        /// <param name="decreeid"></param>
        /// <param name="disabletracking"></param>
        /// <returns></returns>
        public IEnumerable<PersondecreeblockManagement> GetPersondecreeblock(User user, int decreeid, bool disabletracking = false)
        {
            DateTime date = user.Date.GetValueOrDefault();
            if (disabletracking)
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }

            Persondecree decree = Persondecrees.FirstOrDefault(p => p.Id == decreeid);

            List<Persondecreeblock> persondecreeblocksBase = Persondecreeblocks.Where(p => p.Persondecree == decreeid).ToList();
            List<PersondecreeblockManagement> persondecreeblocks = new List<PersondecreeblockManagement>();

            foreach (Persondecreeblock decreeblockBase in persondecreeblocksBase)
            {
                PersondecreeblockManagement decreeblock = new PersondecreeblockManagement(decreeblockBase);
                List<Persondecreeblockintro> persondecreeblockintros = Persondecreeblockintros.Where(p => p.Persondecreeblock == decreeblock.Id).ToList();
                decreeblock.Persondecreeblockintros = persondecreeblockintros;

                List<Persondecreeblocksub> persondecreeblocksubs = Persondecreeblocksubs.Where(p => p.Persondecreeblock == decreeblock.Id).ToList();
                decreeblock.Persondecreeblocksubs = persondecreeblocksubs;

                // Назначить
                if (decreeblock.Persondecreeblocktype == 3)
                {
                    if (decreeblock.Optionnumber1 > 0) // Здесь хранится ID должности
                    {
                        Position position = PositionsLocal().GetValue(decreeblock.Optionnumber1);
                        if (position != null)
                        {
                            decreeblock.SamplePosition = position;
                            

                            //if (position == null)
                            //{
                            //    return null;
                            //}
                            Positiontype positiontype = PositiontypesLocal().GetValue(position.Positiontype);
                            decreeblock.SamplePositiontype = positiontype;
                            //if (positiontype != null)
                            //{
                            //    decreeoperation.Optionstring1 = positiontype.Name2; // По умолчанию дательный падеж
                            //}

                            Structure actualStructure = GetActualStructureInfo(position.Structure, date);
                            if (actualStructure != null)
                            {
                                decreeblock.SampleStructure = actualStructure;
                                //string structureTree = FormTreeDocument2(actualStructure, true, date); // Временное решение
                                string structureTree = FormTreeDocument(actualStructure, date, null, 1, null); // Временное решение
                                //optionstring2
                                decreeblock.Optionstring2 = structureTree;
                            }
                        }
                    }
                }
                    persondecreeblocks.Add(decreeblock);
            }


            return persondecreeblocks;
        }

        /// <summary>
        /// Добавляет новый блок в проект приказа прохождения службы
        /// </summary>
        /// <param name="user"></param>
        /// <param name="persondecreeblock"></param>
        // public void AddPersonDecreeblock(User user, PersondecreeblockManagement persondecreeblock)
        public Persondecreeblock AddPersonDecreeblock(User user, Persondecreeblock persondecreeblock)
        {
            Persondecreeblock newPersondecreeblock = new Persondecreeblock();
            newPersondecreeblock.Persondecree = persondecreeblock.Persondecree;
            newPersondecreeblock.Persondecreeblocktype = persondecreeblock.Persondecreeblocktype;

            Persondecree persondecree = PersondecreesLocal().GetValue(persondecreeblock.Persondecree);
            if (persondecree == null)
            {
                return null;
            }
            List<Persondecreeblock> existingPersondecreeblocks = Persondecreeblocks.Where(p => p.Persondecree == persondecree.Id).ToList();
            if (existingPersondecreeblocks.Count == 0)
            {
                newPersondecreeblock.Index = 1;
            } else
            {
                newPersondecreeblock.Index = existingPersondecreeblocks.Max(p => p.Index) + 1;
            }

            context.Persondecreeblock.Add(newPersondecreeblock);
            SaveChanges();
            return newPersondecreeblock;
        }

        /// <summary>
        /// Удаляем пункт приказа из проекта с удалением всех записей в нем.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="persondecreeblock"></param>
        public void RemovePersonDecreeblock(User user, PersondecreeblockManagement persondecreeblock)
        {
            Persondecreeblock contextDecreeblock = Persondecreeblocks.FirstOrDefault(p => p.Id == persondecreeblock.Id);
            if (contextDecreeblock == null)
            {
                return;
            }

            IEnumerable<Persondecreeoperation> contextPersondecreeoperations = Persondecreeoperations.Where(p => p.Persondecreeblock == persondecreeblock.Id);
            foreach (Persondecreeoperation contextPersondecreeoperation in contextPersondecreeoperations)
            {
                context.Persondecreeoperation.Remove(contextPersondecreeoperation);
            }

            Persondecree persondecree = PersondecreesLocal().GetValue(contextDecreeblock.Persondecree);
            if (persondecree == null)
            {
                return;
            }
            IQueryable<Persondecreeblock> existingPersondecreeblocks = context.Persondecreeblock.Where(p => p.Persondecree == persondecree.Id).Where(p => p.Index > contextDecreeblock.Index);
            foreach(Persondecreeblock existingPersondecreeblock in existingPersondecreeblocks)
            {
                existingPersondecreeblock.Index = existingPersondecreeblock.Index - 1;
            }


            context.Persondecreeblock.Remove(contextDecreeblock);
            SaveChanges();
        }

        /// <summary>
        /// UserBase - пользователь, который пользуется программой
        /// User - преобразуемый объект
        /// </summary>
        /// <param name="userBase"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserManager GetUserManager(User userBase, User user)
        {
            DateTime date = userBase.Date.GetValueOrDefault();
            UserManager userManager = new UserManager(user);
            if (userManager.Structure.GetValueOrDefault() > 0)
            {
                Structure actualStructure = GetActualStructureInfo(userManager.Structure.GetValueOrDefault(), date);
                if (actualStructure != null)
                {
                    userManager.StructureString = actualStructure.Name;
                    userManager.StructureTreeString = FormTree(actualStructure, true, date);
                }
                
            }
            else
            {
                userManager.StructureString = "";
                userManager.StructureTreeString = "";
            }

            if (userManager.Positiontype > 0)
            {
                Position position = PositionsLocal().GetValue(userManager.Positiontype);
                if (position != null)
                {
                    userManager.PositionString = PositiontypesLocal().GetValue(position.Positiontype).Name;
                }
                else
                {
                    userManager.PositionString = "";
                }
            }
            else
            {
                userManager.PositionString = "";
            }

            return userManager;
        }

        /// <summary>
        /// Поиск пользователей по логину, фамилии и/или имени
        /// </summary>
        /// <param name="user"></param>
        /// <param name="fio"></param>
        /// <returns></returns>
        public List<UserManager> GetUsersSearch(User user, string fio)
        {
            if (fio == null)
            {
                return new List<UserManager>();
            }

            List<UserManager> users = new List<UserManager>();
            List<int> structureIds = new List<int>(); // Все айди структур, которые подошли бы под поиск.
            IEnumerable<Structure> possibleStructures = StructuresLocal().Values.Where(n => n.Name.ToLower().Contains(fio.ToLower()));
            possibleStructures = FilterDeletedStructures(possibleStructures, user.Date.GetValueOrDefault());
            foreach (Structure structure in possibleStructures)
            {
                Structure baseStructure = GetOriginalStructure(structure);
                if (baseStructure != null)
                {
                    structureIds.Add(baseStructure.Id);
                }
            }

            users.AddRange(UserManager.UsersToUserManagers(this, user, Users.Where(p => (p.Surname + " " + p.Firstname + " " + p.Patronymic).ToLower().Contains(fio.ToLower()))));
            users.AddRange(UserManager.UsersToUserManagers(this, user, Users.Where(p => structureIds.Contains(p.Structure.GetValueOrDefault()))));
            users = users.DistinctBy(p => p.Id).ToList();

            return users;
        }

        /// <summary>
        /// Перемещает элемент проекта приказа выше. Но он все равно не может быть выше, чем ему положено по сортировке.
        /// </summary>
        /// <param name="persondecreeoperationid"></param>
        /// <param name="user"></param>
        public void UpPersondecreeoperationpriority(int persondecreeoperationid, User user)
        {
            Persondecreeoperation persondecreeoperation = Persondecreeoperations.First(s => s.Id == persondecreeoperationid);
            List<Persondecreeoperation> persondecreeoperations = new List<Persondecreeoperation>();
            persondecreeoperations = Persondecreeoperations.Where(p => p.Persondecreeblock == persondecreeoperation.Persondecreeblock).ToList(); // Относится к одному блоку

            persondecreeoperations = persondecreeoperations.OrderBy(s => s.Priority).ToList();
            if (persondecreeoperations.First().Id == persondecreeoperationid)
            {
                return; // If first, we do nothing.
            }

            List<Persondecreeoperation> persondecreeoperationsSamePriority = persondecreeoperations.Where(s => s.Priority == persondecreeoperation.Priority).ToList();
            int posInSame = persondecreeoperationsSamePriority.FindIndex(s => s.Id == persondecreeoperation.Id);
            List<Persondecreeoperation> persondecreeoperationsLowerPriority = persondecreeoperations.Where(s => s.Priority < persondecreeoperation.Priority).ToList();
            Persondecreeoperation persondecreeoperationBitLowerPriority = persondecreeoperations.LastOrDefault(s => s.Priority < persondecreeoperation.Priority);
            if (persondecreeoperationBitLowerPriority == null || persondecreeoperationsSamePriority.First().Id != persondecreeoperation.Id) // means there are two or more elements with same level but it is the highest at the same time.
            {
                int ind = persondecreeoperationsSamePriority.Count;
                foreach (Persondecreeoperation pdo in persondecreeoperationsSamePriority)
                {
                    pdo.Priority += ind;
                    ind -= 1;
                }
                //structure.Priority += ind + 1;
                persondecreeoperationsSamePriority[posInSame].Priority -= 1;
                persondecreeoperationsSamePriority[posInSame - 1].Priority += 1;
                context.SaveChanges();
                return;
            }

            // major cases
            List<Persondecreeoperation> persondecreeoperationsBitLowerPriority = persondecreeoperations.Where(s => s.Priority == persondecreeoperationBitLowerPriority.Priority).ToList();
            List<Persondecreeoperation> persondecreeoperationsMuchLowerPriority = persondecreeoperations.Where(s => s.Priority < persondecreeoperationBitLowerPriority.Priority).ToList();
            foreach (Persondecreeoperation pdo in persondecreeoperationsLowerPriority)
            {
                pdo.Priority += -persondecreeoperationsSamePriority.Count - 1;
            }
            int index = persondecreeoperationsSamePriority.Count;
            foreach (Persondecreeoperation pdo in persondecreeoperationsSamePriority)
            {
                if (pdo.Id != persondecreeoperation.Id)
                {
                    pdo.Priority += index;
                    index -= 1;
                }
            }
            int additionalPriority = persondecreeoperationBitLowerPriority.Priority - 1 - persondecreeoperation.Priority;
            foreach (Persondecreeoperation pdo in persondecreeoperationsMuchLowerPriority)
            {
                pdo.Priority -= 1;
            }
            persondecreeoperation.Priority += additionalPriority;
            int newpriority = persondecreeoperation.Priority;

            context.SaveChanges();
            UpdatePersondecreeoperationsLocal();
        }

        public void DownPersondecreeoperationpriority(int persondecreeoperationid, User user)
        {

            Persondecreeoperation persondecreeoperation = Persondecreeoperations.First(s => s.Id == persondecreeoperationid);
            List<Persondecreeoperation> persondecreeoperations = new List<Persondecreeoperation>();
            persondecreeoperations = Persondecreeoperations.Where(p => p.Persondecreeblock == persondecreeoperation.Persondecreeblock).ToList(); // Относится к одному блоку

            persondecreeoperations = persondecreeoperations.OrderBy(s => s.Priority).ToList();
            if (persondecreeoperations.First().Id == persondecreeoperationid)
            {
                return; // If first, we do nothing.
            }

            List<Persondecreeoperation> persondecreeoperationsSamePriority = persondecreeoperations.Where(s => s.Priority == persondecreeoperation.Priority).ToList();
            int posInSame = persondecreeoperationsSamePriority.FindIndex(s => s.Id == persondecreeoperation.Id);
            List<Persondecreeoperation> persondecreeoperationsHigherPriority = persondecreeoperations.Where(s => s.Priority > persondecreeoperation.Priority).ToList();
            Persondecreeoperation persondecreeoperationBitHigherPriority = persondecreeoperations.FirstOrDefault(s => s.Priority > persondecreeoperation.Priority);
            if (persondecreeoperationBitHigherPriority == null || persondecreeoperationsSamePriority.Last().Id != persondecreeoperation.Id) // means there are two or more elements with same level but it is the highest at the same time.
            {
                int ind = 1;
                foreach (Persondecreeoperation pdo in persondecreeoperationsSamePriority)
                {
                    pdo.Priority += ind;
                    ind += 1;
                }
                //structure.Priority += ind + 1;
                persondecreeoperationsSamePriority[posInSame].Priority += 1;
                persondecreeoperationsSamePriority[posInSame + 1].Priority -= 1;
                context.SaveChanges();
                return;
            }
            
            // major cases
            List<Persondecreeoperation> persondecreeoperationsBitHigherPriority = persondecreeoperations.Where(s => s.Priority == persondecreeoperationBitHigherPriority.Priority).ToList();
            List<Persondecreeoperation> persondecreeoperationsMuchHigherPriority = persondecreeoperations.Where(s => s.Priority > persondecreeoperationBitHigherPriority.Priority).ToList();
            foreach (Persondecreeoperation pdo in persondecreeoperationsHigherPriority)
            {
                pdo.Priority += persondecreeoperationsSamePriority.Count - 1;
            }
            int index = 1;
            foreach (Persondecreeoperation pdo in persondecreeoperationsSamePriority)
            {
                if (pdo.Id != persondecreeoperation.Id)
                {
                    pdo.Priority += index;
                    index += 1;
                }
            }
            int additionalPriority = persondecreeoperationBitHigherPriority.Priority + 1 - persondecreeoperation.Priority;
            foreach (Persondecreeoperation pdo in persondecreeoperationsMuchHigherPriority)
            {
                pdo.Priority += 1;
            }
            persondecreeoperation.Priority += additionalPriority;


            context.SaveChanges();
            UpdatePersondecreeoperationsLocal();
        }

        /// <summary>
        /// Возвращает число лет, сколько по контракту прослужил сотрудник
        /// </summary>
        /// <param name="personManager"></param>
        /// <returns></returns>
        public int GetPersonContractYears(PersonManager personManager)
        {
            int years = 0;
            int yearsAccamulated = 0;
            int monthsAccamulated = 0;
            int daysAccamulated = 0;
            foreach (Personcontract personcontract in personManager.Personcontracts)
            {
                //DateDiff dateDiff = new DateDiff(decreeoperation.Optiondate1.GetValueOrDefault(), decreeoperation.Optiondate2.GetValueOrDefault().AddDays(1));
                DateDiff dateDiff = new DateDiff(personcontract.Datestart, personcontract.Dateend.AddDays(1));
                yearsAccamulated += dateDiff.ElapsedYears;
                monthsAccamulated += dateDiff.ElapsedMonths;
                daysAccamulated += dateDiff.ElapsedDays;
                
                if (daysAccamulated >= 30)
                {
                    daysAccamulated -= 30;
                    monthsAccamulated += 1;
                }
                if (monthsAccamulated >= 12)
                {
                    monthsAccamulated -= 12;
                    yearsAccamulated += 1;
                }
            }
            years = yearsAccamulated;
            return yearsAccamulated;
        }

        /// <summary>
        /// Объединяем несколько проектов приказов по прохождению службы в один, в котором содержатся все блоки и операции этих проектов приказов
        /// </summary>
        /// <param name="user"></param>
        /// <param name="persondecreeIds"></param>
        public void PersondecreesUnite(User user, IEnumerable<int> persondecreeIds, Persondecree folder = null)
        {
            Mailexplorer for_new_decree = new Mailexplorer() { FolderCreator = 6, AccessForReading = user.Id.ToString(), FolderOwner = 0 };
            context.Mailexplorer.Add(for_new_decree);
            context.SaveChanges();

            DateTime date = user.Date.GetValueOrDefault();
            Persondecree unitedPersondecree = new Persondecree();
            unitedPersondecree.Creator = user.Id;
            unitedPersondecree.Owner = user.Id;
            unitedPersondecree.Datecreated = date;
            unitedPersondecree.Datesigned = date;
            unitedPersondecree.Mailexplorerid = for_new_decree.Id;
            unitedPersondecree.Nickname = (folder == null || folder.Nickname == "" || folder.Nickname == null) ? "Объединенный проект приказа " + date.ToString("dd MMMM yyyy") : folder.Nickname;
            unitedPersondecree.Name = (folder == null || folder.Name == "" || folder.Nickname == null) ? "" : folder.Name;


            context.Persondecree.Add(unitedPersondecree);
            SaveChanges();
            UpdatePersondecreesLocal();
            folder = unitedPersondecree;

            Dictionary<int, Persondecreeblock> persondecreeblocks = new Dictionary<int, Persondecreeblock>(); // Блоки, которые входят в объединенный проект приказа.
                                                                                                              // Ключ - тип блока

            // Вначале ищем persondecree по id и добавляем в список
            List<Persondecree> persondecrees = new List<Persondecree>();
            foreach(int persondecreeId in persondecreeIds)
            {
                Persondecree persondecree = PersondecreesLocal().GetValue(persondecreeId);
                if (persondecree != null)
                {
                    persondecrees.Add(persondecree);
                }
            }

            
            foreach(Persondecree persondecree in persondecrees)
            {
                List<Persondecreeoperation> persondecreeoperations = PersondecreeoperationsLocal().Values.Where(p => p.Persondecree == persondecree.Id).ToList();
                foreach (Persondecreeoperation persondecreeoperation in persondecreeoperations)
                {
                    if (persondecreeoperation.Persondecreeblocktype == 0)
                    {
                        continue;
                    }
                    if (!persondecreeblocks.ContainsKey(persondecreeoperation.Persondecreeblocktype))
                    {
                        Persondecreeblock oldPersondecreeblock = Persondecreeblocks.FirstOrDefault(p => p.Id == persondecreeoperation.Persondecreeblock);
                        Persondecreeblock newPersondecreeblock = new Persondecreeblock();
                        if (oldPersondecreeblock != null)
                        {
                            // Потенциально может перенимать еще какую-то информацию с блока.
                            newPersondecreeblock.Persondecreeblocktype = persondecreeoperation.Persondecreeblocktype;
                        } else
                        {
                            newPersondecreeblock.Persondecreeblocktype = persondecreeoperation.Persondecreeblocktype;
                        }
                        newPersondecreeblock.Persondecree = unitedPersondecree.Id;
                        newPersondecreeblock = AddPersonDecreeblock(user, newPersondecreeblock); // Тут null почему-то
                        //context.Persondecreeblock.Add(newPersondecreeblock);
                        //SaveChanges();
                        persondecreeblocks.Add(newPersondecreeblock.Persondecreeblocktype, newPersondecreeblock);
                    }
                    Persondecreeblock persondecreeblock = persondecreeblocks[persondecreeoperation.Persondecreeblocktype];

                    // Создаем новый проект приказа
                    Persondecreeoperation newPersondecreeoperation = ClonePersondecreeoperation(persondecreeoperation, user, unitedPersondecree, persondecreeblock);
                    AddPersonDecreeoperation(user, newPersondecreeoperation);
                }
            }
        }

        /// <summary>
        /// Клонирует операцию проекта приказа с возможностью переместить к другому проекту приказа или блоку, а также указать нового "создателя" операции
        /// Важно: при клонировании не добавляет в базу данных. Это необходимо производить отдельно
        /// </summary>
        /// <param name="persondecreeoperation"></param>
        /// <param name="persondecree"></param>
        /// <param name="persondecreeblock"></param>
        /// <returns></returns>
        public Persondecreeoperation ClonePersondecreeoperation(Persondecreeoperation persondecreeoperation, User user = null, Persondecree persondecree = null, Persondecreeblock persondecreeblock = null)
        {
            Persondecreeoperation newPersondecreeoperation = new Persondecreeoperation();

            if (persondecreeoperation.Person != 0)
            {
                newPersondecreeoperation.Person = persondecreeoperation.Person;
            }
            // Случай, когда вместо работника из ЭЛД прописывается человек, не состоящий в МЧС.
            else
            {
                newPersondecreeoperation.Nonperson = persondecreeoperation.Nonperson;
            }
            newPersondecreeoperation.Intro = persondecreeoperation.Intro;
            newPersondecreeoperation.Persondecreeblocksubtype = persondecreeoperation.Persondecreeblocksubtype; // Тип подблока, например 6 - "наградить грамотой"
            newPersondecreeoperation.Persondecreeblocktype = persondecreeoperation.Persondecreeblocktype;
            newPersondecreeoperation.Persondecreeblock = persondecreeoperation.Persondecreeblock;
            //newPersondecreeoperation.Creator = user.Id;
            newPersondecreeoperation.Creator = persondecreeoperation.Creator;
            newPersondecreeoperation.Persondecree = persondecreeoperation.Persondecree;
            newPersondecreeoperation.Optionnumber1 = persondecreeoperation.Optionnumber1;
            newPersondecreeoperation.Optionnumber2 = persondecreeoperation.Optionnumber2;
            newPersondecreeoperation.Optionnumber3 = persondecreeoperation.Optionnumber3;
            newPersondecreeoperation.Optionnumber4 = persondecreeoperation.Optionnumber4;
            newPersondecreeoperation.Optionnumber5 = persondecreeoperation.Optionnumber5;
            newPersondecreeoperation.Optionnumber6 = persondecreeoperation.Optionnumber6;
            newPersondecreeoperation.Optionnumber7 = persondecreeoperation.Optionnumber7;
            newPersondecreeoperation.Optionnumber8 = persondecreeoperation.Optionnumber8;
            newPersondecreeoperation.Optionnumber9 = persondecreeoperation.Optionnumber9;
            newPersondecreeoperation.Optionnumber10 = persondecreeoperation.Optionnumber10;
            newPersondecreeoperation.Optionnumber11 = persondecreeoperation.Optionnumber11;
            newPersondecreeoperation.Optionstring1 = persondecreeoperation.Optionstring1;
            newPersondecreeoperation.Optionstring2 = persondecreeoperation.Optionstring2;
            newPersondecreeoperation.Optionstring3 = persondecreeoperation.Optionstring3;
            newPersondecreeoperation.Optionstring4 = persondecreeoperation.Optionstring4;
            newPersondecreeoperation.Optionstring5 = persondecreeoperation.Optionstring5;
            newPersondecreeoperation.Optionstring6 = persondecreeoperation.Optionstring6;
            newPersondecreeoperation.Optionstring7 = persondecreeoperation.Optionstring7;
            newPersondecreeoperation.Optionstring8 = persondecreeoperation.Optionstring8;
            newPersondecreeoperation.Optiondate1 = persondecreeoperation.Optiondate1;
            newPersondecreeoperation.Optiondate2 = persondecreeoperation.Optiondate2;
            newPersondecreeoperation.Optiondate3 = persondecreeoperation.Optiondate3;
            newPersondecreeoperation.Optiondate4 = persondecreeoperation.Optiondate4;
            newPersondecreeoperation.Optiondate5 = persondecreeoperation.Optiondate5;
            newPersondecreeoperation.Optiondate6 = persondecreeoperation.Optiondate6;
            newPersondecreeoperation.Optiondate7 = persondecreeoperation.Optiondate7;
            newPersondecreeoperation.Optiondate8 = persondecreeoperation.Optiondate8;
            newPersondecreeoperation.Subvaluenumber1 = persondecreeoperation.Subvaluenumber1;
            newPersondecreeoperation.Subvaluenumber2 = persondecreeoperation.Subvaluenumber2;
            newPersondecreeoperation.Subvaluestring1 = persondecreeoperation.Subvaluestring1;
            newPersondecreeoperation.Subvaluestring2 = persondecreeoperation.Subvaluestring2;
            if (user != null)
            {
                newPersondecreeoperation.Creator = user.Id;
            }
            if (persondecree != null)
            {
                newPersondecreeoperation.Persondecree = persondecree.Id;
            }
            if (persondecreeblock != null)
            {
                //newPersondecreeoperation.Persondecreeblocksubtype = persondecreeblock.Persondecreeblocksub; // Тип подблока, например 6 - "наградить грамотой"
                newPersondecreeoperation.Persondecreeblocktype = persondecreeblock.Persondecreeblocktype;
                newPersondecreeoperation.Persondecreeblock = persondecreeblock.Id;
            }

            return newPersondecreeoperation;
        }

       public Educationperiod GetEducationperiod(int idEducationPeriod)
        {
            return context.Educationperiod.ToList().FirstOrDefault(e => e.Id == idEducationPeriod);
        }
        public string ToUpperFirstLetter(string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            // convert to char array of the string
            char[] letters = source.ToCharArray();
            // upper case the first char
            letters[0] = char.ToUpper(letters[0]);
            // return the array made of the new char array
            return new string(letters);
        }

    }
}
