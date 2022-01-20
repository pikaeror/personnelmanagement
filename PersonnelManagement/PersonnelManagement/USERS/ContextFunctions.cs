using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace PersonnelManagement.USERS
{
    public partial class userContext : DbContext
    {
        public userContext(DbContextOptions<userContext> options) : base(options) { }

        private static Stopwatch stopWatch = Stopwatch.StartNew();

        private static long SessionsGetLastTime = -SESSIONS_GET_DELAY - 1;
        private const long SESSIONS_GET_DELAY = 80000; // in ms
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
            SessionsLocalObject = Session.ToDictionary(session => session.Id);
        }



        private static long UsersGetLastTime = -USERS_GET_DELAY - 1;
        private const long USERS_GET_DELAY = 150000; // in ms
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
            UsersLocalObject = User.ToDictionary(user => user.Id);
        }

        private static long RightsGetLastTime = -RIGHTS_GET_DELAY - 1;
        private const long RIGHTS_GET_DELAY = 100000; // in ms
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

        /// <summary>
        /// Добавляет нового пользователя
        /// </summary>
        /// <param name="user"></param>
        public void SaveUser(User user)
        {
            User.Add(user);
            SaveChanges();
            UpdateUsersLocal();
            Rights rights = user.Rights;
            if (rights != null)
            {
                rights.User = user.Id;
                Rights.Add(rights);
                SaveChanges();
                UpdateRightsLocal();

                // Раньше права доступа хранились в user напрямую, а теперь в rights, привязанному к этому пользователю
                // Этот код неоходим для методов, которые используют старые привязки к полям в user.
                user.Admin = user.Rights.Admin;
                user.Masterpersonneleditor = user.Rights.Peopleorgreadall;
                user.Structureeditor = user.Rights.Orgedit;
                user.Structureread = user.Rights.Orgread;
                user.Personneleditor = user.Rights.Peopleedit;
                user.Personnelread = user.Rights.Peopleread;
                SaveChanges();
                UpdateUsersLocal();
            }
        }

        /// <summary>
        /// Удаляет пользователя
        /// </summary>
        /// <param name="user"></param>
        public void DeleteUser(User user)
        {
            User contextUser = User.First(u => u.Id == user.Id);
            int userId = contextUser.Id;
            User.Remove(contextUser);
            SaveChanges();
            UpdateUsersLocal();

            Rights contextRights = Rights.FirstOrDefault(r => r.User == userId);
            if (contextRights != null)
            {
                Rights.Remove(contextRights);
                SaveChanges();

            }
        }

        /// <summary>
        /// Обнуляет пароль пользователя. Новый пароль сохраняется после первого ввода
        /// </summary>
        /// <param name="user"></param>
        public void NullifyPassUser(User user)
        {
            User contextUser = User.First(u => u.Id == user.Id);
            contextUser.Salt = null;
            contextUser.Password = null;
            SaveChanges();
            UpdateUsersLocal();
        }

        public void AddRights(User user, Rights rights)
        {
            Rights.Add(rights);
            SaveChanges();
            UpdateRightsLocal();
        }

        public void ChangeRights(User user, Rights rights)
        {
            Rights rightsContext = Rights.FirstOrDefault(p => p.Id == rights.Id);
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
            rightsContext.CopyFields(rightsContext);

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
            }
            else
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

            Rights.Add(rights);
            SaveChanges();
            UpdateRightsLocal();

            return rights;
        }

        public void DeleteRights(User user, Rights rights)
        {
            int minusid = -rights.Id;
            Rights rightsContext = Rights.FirstOrDefault(p => p.Id == minusid);
            if (rightsContext == null)
            {
                return;
            }
            Rights.Remove(rightsContext);

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
            User.First(u => u.Id == user.Id).Currentstructuretree = getString;
            SaveChanges();
            UpdateUsersLocal();
        }


        /// <summary>
        /// Обновляет настройки пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userSettings"></param>
        public void UpdateUserSettings(User user, UserSettings userSettings)
        {
            User contextUser = User.First(u => u.Id == user.Id);
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


            SaveChanges();
            UpdateUsersLocal();
        }

        /// <summary>
        /// Устанавливает орг-штатный проект приказа, над которым работает пользователь
        /// </summary>
        /// <param name="user"></param>
        /// <param name="id">ID приказа</param>
        public void DecreesSelectActive(User user, int id)
        {
            User contextUser = User.First(u => u.Id == user.Id);
            contextUser.Decree = id;
            SaveChanges();
            UpdateUsersLocal();
        }

        /**
         * Remove session with a specified id.
         */
        public void RemoveSession(string id)
        {
            Session session = Session.First(ses => ses.Id == id);
            Session.Remove(session);
            SaveChanges();
            UpdateSessionsLocal();
        }

        public void AddSession(Session session)
        {
            Session.Add(session);
            SaveChanges();
            UpdateSessionsLocal();
        }

        public void RemoveSessions(List<Session> list)
        {
            foreach (Session session in list)
            {
                Session.Remove(session);
            }
            SaveChanges();
            UpdateSessionsLocal();
        }

        public void UpdateUser(User user)
        {
            User contextUser = User.First(u => u.Id == user.Id);
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

            SaveChanges();
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

    }
}
