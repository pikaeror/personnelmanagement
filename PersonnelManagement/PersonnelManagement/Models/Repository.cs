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
using System.Threading;

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

        public Repository(pmContext ctx)
        {
            context = ctx;
            certContext = null;
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
        public IQueryable<Mrd> Mrds => context.Mrd;
        public IQueryable<Positionmrd> Positionmrds => context.Positionmrd;
        public IQueryable<Altrankcondition> Altrankconditions  => context.Altrankcondition;
        public IQueryable<Altrankconditiongroup> Altrankconditiongroups => context.Altrankconditiongroup;
        public IQueryable<Altrank> Altranks => context.Altrank;
        public IQueryable<Positionhistory> Positionhistories => context.Positionhistory;
        public IQueryable<Departmentrename> Departmentrenames => context.Departmentrename;
        public IQueryable<Structureregion> Structureregions => context.Structureregion;
        public IQueryable<Structuretype> Structuretypes => context.Structuretype;
        public IQueryable<Dismissalclauses> Dismissalclauses => context.Dismissalclauses;
        public IQueryable<Country> Countries => context.Country;
        public IQueryable<Agency> Agencies => certContext.Agency;
        public IQueryable<Base> Bases => certContext.Base;
        public IQueryable<Blankform> Blankforms => certContext.Blankform;
        public IQueryable<Issuingauthority> Issuingauthorities => certContext.Issuingauthority;
        public IQueryable<Post> Posts => certContext.Post;
        /*public IQueryable<Udostoverenia.Rank> cRanks => certContext.Rank;*/
        public IQueryable<Certificate> Certificates => certContext.Certificate;
        public IQueryable<Rejectreason> Rejectreasons => certContext.Rejectreason;
        public IQueryable<Drivertype> Drivertypes => context.Drivertype;
        public IQueryable<Drivercategory> Drivercategories => context.Drivercategory;
        public IQueryable<Prooftype> Prooftypes => context.Prooftype;
        public IQueryable<Region> Regions => context.Region;
        public IQueryable<Area> Areas => context.Area;
        public IQueryable<Transfertype> Transfertypes => context.Transfertype;
        public IQueryable<Subject> Subjects => context.Subject;
        public IQueryable<Subjectgender> Subjectgenders => context.Subjectgender;
        public IQueryable<Subjectcategory> Subjectcategories => context.Subjectcategory;
        public IQueryable<Setpersondatatype> Setpersondatatypes => context.Setpersondatatype;
        public IQueryable<Ordernumbertype> Ordernumbertypes => context.Ordernumbertype;
        public IQueryable<Streettype> Streettypes => context.Streettype;
        public IQueryable<Citytype> Citytypes => context.Citytype;
        public IQueryable<Areaother> Areaothers => context.Areaother;
        public IQueryable<Externalorderwhotype> Externalorderwhotypes => context.Externalorderwhotype;
        public IQueryable<Citysubstate> Citysubstates => context.Citysubstate;
        public IQueryable<Role> Roles => context.Role;
        public IQueryable<Rights> Rights => context.Rights;
        public IQueryable<Rightsstructure> Rightsstructures => context.Rightsstructure;

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
            structureToClone = GetActualStructureInfo(structureToClone, user.Date.GetValueOrDefault());
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
            //UpdateStructuresLocal();

            Decree decree = GetDecreeByUser(user);
            Decreeoperation operation = new Decreeoperation();
            operation.Decree = decree.Id;
            operation.Created = 1;
            operation.Subject = -structure.Id; // У подразделений subject имеет знак минуса

            context.Decreeoperation.Add(operation);
            context.SaveChanges();
            //UpdateDecreeoperationsLocal();
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
            Structure newStructure = new Structure(structure);
            newStructure.Id = 0;
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

            newStructure.Subject1 = structureManagement.Subject1;
            newStructure.Subject2 = structureManagement.Subject2;
            newStructure.Subject3 = structureManagement.Subject3;
            newStructure.Subject4 = structureManagement.Subject4;
            newStructure.Subject5 = structureManagement.Subject5;
            newStructure.Subject6 = structureManagement.Subject6;
            newStructure.Subject7 = structureManagement.Subject7;
            newStructure.Subject8 = structureManagement.Subject8;
            newStructure.Subject9 = structureManagement.Subject9;
            newStructure.Subject10 = structureManagement.Subject10;
            newStructure.Subject11 = structureManagement.Subject11;
            newStructure.Subject12 = structureManagement.Subject12;
            newStructure.Subject13 = structureManagement.Subject13;
            newStructure.Subject14 = structureManagement.Subject14;
            newStructure.Subject15 = structureManagement.Subject15;
            newStructure.Subjectgender = structureManagement.Subjectgender;
            newStructure.Subjectnumber = structureManagement.Subjectnumber;
            newStructure.Subjectnotice = structureManagement.Subjectnotice;

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

/*            structure.Subject1 = structureManagement.Subject1;
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
            structure.Subjectnotice = structureManagement.Subjectnotice;*/

            newStructure.Changeorigin = origin.Id;
            context.Structure.Add(newStructure);
            context.SaveChanges();

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

            UpdateStructuresLocal();
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
            IEnumerable<Decreeoperation> all_operations_by_decree = DecreeoperationsLocal().Values.Where(r => r.Decree == decree.Id);
            Decreeoperation operation = new Decreeoperation();
            operation.Decree = decree.Id;
            operation.Deleted = 1;
            operation.Subject = -structureManagement.Parent; // У подразделений subject имеет знак минуса
            if (structureManagement.Datecustom > 0)
            {
                operation.Dateactive = structureManagement.Dateactive;
                operation.Datecustom = 1;
            }
            if(!operation.isEqual(all_operations_by_decree)) { context.Decreeoperation.Add(operation); all_operations_by_decree = all_operations_by_decree.Append(operation); }

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
                    if (!operationStruct.isEqual(all_operations_by_decree)) { context.Decreeoperation.Add(operationStruct); all_operations_by_decree = all_operations_by_decree.Append(operationStruct); }
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
                if (!operationPosition.isEqual(all_operations_by_decree)) { context.Decreeoperation.Add(operationPosition); all_operations_by_decree = all_operations_by_decree.Append(operationPosition); }
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
            Position position = new Position(positionToClone);
            position.Id = 0;
/*            position.Cap = positionToClone.Cap;
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
            position.Altrank = positionToClone.Altrank;*/
            //position.Altrank = positionToClone.Altrank;
            position.Origin = 0;
/*            position.Decertificate = positionToClone.Decertificate;
            position.Decertificatedate = positionToClone.Decertificatedate;
            position.Civilranklow = positionToClone.Civilranklow;
            position.Civilrankhigh = positionToClone.Civilrankhigh;
            position.Replacedbycivildatelimit = positionToClone.Replacedbycivildatelimit;
            position.Replacedbycivildate = positionToClone.Replacedbycivildate;*/
            position.Structure = 0; // Put parent later
/*            position.Positiontype = positionToClone.Positiontype;
            position.Civildecree = positionToClone.Civildecree;
            position.Civildecreenumber = positionToClone.Civildecreenumber;
            position.Civildecreedate = positionToClone.Civildecreedate;*/
            position.Curator = 0;
            position.Head = 0;
            position.Curatorlist = "";
            position.Headid = 0;

            /**
             * Для прохождения службы
             */
/*            position.Subject1 = positionToClone.Subject1;
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
            position.Subject20 = positionToClone.Subject20;*/

            context.Position.Add(position);
            context.SaveChanges();
            position.Origin = position.Id;
            //context.SaveChanges();

            List<Positionmrd> positionmrdsToClone = Positionmrds.Where(pm => pm.Position == positionToClone.Id).ToList();
            foreach(Positionmrd positionmrdToClone in positionmrdsToClone)
            {
                Positionmrd positionmrd = new Positionmrd();
                positionmrd.Mrd = positionmrdToClone.Mrd;
                positionmrd.Position = position.Id;
                context.Positionmrd.Add(positionmrd);
            }
            //context.SaveChanges();



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
            /**/context.SaveChanges();

            

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
            /*UpdateDecreeoperationsLocal();
            UpdatePositionsLocal();
            UpdatePositionmrdsLocal();*/
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
            IEnumerable<Decreeoperation> all_operations_by_decree = DecreeoperationsLocal().Values.Where(r => r.Decree == decree.Id);
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
            if (operation.isEqual(all_operations_by_decree)) { return; }

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
            contextUser.Onlyreadflagtoeditor = user.Onlyreadflagtoeditor;
            if (user.Onlyreadflagtoeditor == 0)
                contextUser.Decree = null;

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
            //context.Rank.FirstOrDefault(r => r.Id == rank.Id);
            Rank contextRank = context.Rank.FirstOrDefault(r => r.Id == rank.Id);
            if (contextRank == null)
            {
                return;
            }
            contextRank.Name = rank.Name;
            contextRank.Positioncategory = rank.Positioncategory;
            contextRank.MaxPeriod = rank.MaxPeriod;
            context.Rank.Update(contextRank);
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


            context.Rank.Update(contextRank);
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
            User user = context.User.First(r => r.Id == session.Userid);
            user.Onlyreadflagtoeditor = 0;
            context.Session.Remove(session);
            context.User.Update(user);
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

        public StructureTree GetStructureTree(int id, DateTime date)
        {
            StructureTree structureTree = new StructureTree();
            Structure structure = StructuresLocal().GetValue(id);
            //Structure structure = Structures.FirstOrDefault(s => s.Id == id);
            if (structure != null)
            {
                structureTree.Tree = FormTree(structure, true, date);
                structureTree.Id = id;
            }
            return structureTree;
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
            List<int> structuresInts = GetStructuresSiblings(structureid, date: user.Date);
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
            UpdateStructuresLocal();
            UpdateDecreeoperationsLocal();

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
            UpdateDecreeoperationsLocal();
            UpdatePositionsLocal();
            UpdatePositionmrdsLocal();
            //context.SaveChanges();
            
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

        public List<Position> GetAllPositions(int structureID)
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
                if (head != null && !positions.ToDictionary(r => r.Id).ContainsKey(head.Id))
                {
                    //!positions.ToDictionary(r => r.Id).ContainsKey(head.Id) ? positions.Add(head) : null;
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

            Dictionary<int, Structure> struct_local = context.Structure.ToDictionary(r => r.Id);

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
                foreach(Structure structure in FilterDeletedStructures(struct_local.Values.Where(s => s.Parentstructure == 0), user.Date.GetValueOrDefault()))
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

            /*Dictionary<int, Structure> struct_local = context.Structure.ToDictionary(r => r.Id);*/

            if (structuretypes != null)
            {
                List<Position> input = positions.ToList();
                positions = positions.Where(p => {
                //positions = positions.AsParallel().Where(p => {
                    if (p.Structure == 0)
                    {
                        return false;
                    }
                    //Structure structure = Structures.FirstOrDefault(s => s.Id == p.Structure);
                    Structure structure = null;
                    if (struct_local.ContainsKey(p.Structure))
                    {
                        structure = struct_local[p.Structure];
                    } else
                    {
                        return false;
                    }

                    return structuretypes.Contains(GetStructureType(structure));
                });

                List<Position> output = new List<Position>();
                foreach(Position time in input)
                {
                    Structure str = struct_local.GetValue(time.Structure),
                    curent = GetActualStructureInfo(str, user.Date.GetValueOrDefault());
                    if (structuretypes.Contains(curent.Structuretype))
                        output.Add(time);
                }
                positions = output;
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

                Structure structureForStructuretype = struct_local.GetValue(position.Structure);
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
                    
                    Structure headingStructure = struct_local.GetValue(position.Headid);
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
                        Structure structure = struct_local.GetValue(id);
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

        public string GetPositionRank(int Positiontype)
        {
            return PositiontypesLocal().Values.FirstOrDefault(el => el.Id == Positiontype).Name;
        }

        public Structure getParentForUserExcert(User user, Dictionary<int, Structure> str = null)
        {
            str = str != null ? str : StructuresLocal();
            return getStructuresForUserExcert(user: user, str: str, parentstructureonly: true).Last();
        }

        public IEnumerable<Structure> getStructuresForUserExcert(User user, Dictionary<int, Structure> str = null, bool parentstructureonly = false)
        {
            str = str != null ? str : StructuresLocal();
            Dictionary<int, Structure> structures_output_dic = new Dictionary<int, Structure>();
            Structure struct_user = str[user.Structure.GetValueOrDefault()], time_struct;
            struct_user = struct_user.Parentstructure == 0 ? struct_user : str[struct_user.Parentstructure];
            DateTime actualdate = user.Date.GetValueOrDefault();
            struct_user = GetActualStructureInfo(struct_user.Id, actualdate, str.Values);
            IEnumerable<Structure> iteration_list = struct_user.Nameshortened == "ЦА МЧС" ? str.Values.Where(r => r.Featured == 1) : str.Values.Where(r => r.Parentstructure == struct_user.Id);
            foreach (Structure time in iteration_list)
            {
                time_struct = GetActualStructureInfo(time.Id, actualdate, str.Values);
                if (time_struct != null && !structures_output_dic.ContainsKey(time_struct.Id))
                {
                    structures_output_dic.Add(time_struct.Id, time_struct);
                    if(parentstructureonly)
                    {
                        return new List<Structure>() { GetActualStructureInfo(structures_output_dic.Values.Last().Id, actualdate, str.Values) };
                    }
                }
            }
            return (structures_output_dic != null ? structures_output_dic.Values.ToList() : new List<Structure>());
        }

        public IEnumerable<FeaturedStructure> getStructuresForUserExcert(User user)
        {
            IEnumerable<FeaturedStructure> output = new List<FeaturedStructure>();
            Dictionary<int, Structure> str = StructuresLocal();
            foreach (Structure time in getStructuresForUserExcert(user: user, str: str))
            {
                output = output.Append(new FeaturedStructure() { Id = time.Id.ToString(), Name = time.Nameshortened });
            }
            return output != null ? output : new List<FeaturedStructure>();
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

        public static Dictionary<int, CancellationTokenSource> ThreadArray = new Dictionary<int, CancellationTokenSource>();
    }
}
