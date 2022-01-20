using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using PersonnelManagement.Models;
using PersonnelManagement.USERS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Services
{
    /**
     * Manages authorization and authentication: login, logout, access rights.
     */
    public class IdentityService
    {

        private const string ALLOWED_CHARACTERS = "abcdefghijklmnopqrstuvwxyz0123456789";
        public const int SALT_LENGTH = 512 / 8;
        public const int SESSION_LENGTH = 128;
        public const int SESSION_DURATION = 36864; // In seconds.

        public IdentityService()
        {
        }

        public void test()
        {
            Console.WriteLine("Test!");
        }

        public static bool IsLogined(string session, Repository repository)
        {
            if (session == null)
            {
                return false;
            }
            if (repository.GetContextUser().SessionsLocal() == null)
            {
                repository.GetContextUser().UpdateSessionsLocal();
            }

            if (repository.GetContextUser().SessionsLocal().GetValue(session) != null) // Не может коннектнуться
            //if (repository.Sessions.Any(s => s.Id == session)) // Не может коннектнуться
            {
                //Session sessionDB = repository.SessionsLocal().GetValue(session);
                //Session sessionDB = repository.Sessions.FirstOrDefault(s => s.Id == session);
                /**
                 * TODO Add expiration. 
                 */
                return true;
            } else
            {
                return false;
            }
        }

        public static int getStructureOfUser(string session, Repository repository)
        {
            User user = GetUserBySessionID(session, repository);
            if (user == null)
            {
                return 0;
            }
            return user.Structure.GetValueOrDefault(0);
        }

        public static Dictionary<string, string> getRightsOfUser(string session, Repository repository)
        {
            User user = GetUserBySessionID(session, repository);
            if (user == null)
            {
                return null;
            }
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(Keys.IDENTITY_LOGIN_KEY, user.Name);
            dictionary.Add(Keys.IDENTITY_MASTERPERSONNELEDITOR_KEY, user.Masterpersonneleditor.GetValueOrDefault(0).ToString());
            dictionary.Add(Keys.IDENTITY_ADMIN_KEY, user.Admin.GetValueOrDefault(0).ToString());
            dictionary.Add(Keys.IDENTITY_PERSONNELEDITOR_KEY, user.Personneleditor.GetValueOrDefault(0).ToString());
            dictionary.Add(Keys.IDENTITY_PERSONNELREAD_KEY, user.Personnelread.ToString());
            dictionary.Add(Keys.IDENTITY_STRUCTUREEDITOR_KEY, user.Structureeditor.GetValueOrDefault(0).ToString());
            dictionary.Add(Keys.IDENTITY_STRUCTUREREAD_KEY, user.Structureread.ToString());
            dictionary.Add(Keys.IDENTITY_MODE_KEY, user.Mode.ToString());
            dictionary.Add(Keys.IDENTITY_DECREE_KEY, user.Decree.GetValueOrDefault(0).ToString());
            if (user.Decree.GetValueOrDefault(0) > 0)
            {
                dictionary.Add(Keys.IDENTITY_DECREE_NAME_KEY, repository.DecreesLocal()[user.Decree.GetValueOrDefault(0)].Nickname);
                //dictionary.Add(Keys.IDENTITY_DECREE_NAME_KEY, repository.Decrees.First(d => d.Id == user.Decree.GetValueOrDefault(0)).Nickname);


            } else
            {
                dictionary.Add(Keys.IDENTITY_DECREE_NAME_KEY, "");
            }
            
            dictionary.Add(Keys.IDENTITY_POSITIONCOMPACT_KEY, user.Positioncompact.GetValueOrDefault(0).ToString()); 
            dictionary.Add(Keys.IDENTITY_DATE_KEY, user.Date.GetValueOrDefault().ToString("yyyy-MM-dd")); //.Now.ToString("yyyy-MM-dd")
            dictionary.Add(Keys.IDENTITY_SIDEBAR_DISPLAY_KEY, user.Sidebardisplay.GetValueOrDefault(0).ToString());
            dictionary.Add(Keys.IDENTITY_CURRENTSTRUCTURETREE_KEY, user.Currentstructuretree);
            dictionary.Add(Keys.IDENTITY_FULLMODE_KEY, user.Fullmode.ToString());

            return dictionary;
        }

        /**
         * Removes all expired sessions from the database. 
         */
        public static void RemoveObsoleteSessions(Repository repository)
        {
            repository.RemoveObsoleteSessions();
        }

        public string GenerateSession(int userID, Repository repository)
        {
            Session session = new Session();

            string sessionid = GenerateSessionID();
            session.Id = sessionid;
            
            DateTime expireDate = DateTime.Now;
            expireDate = expireDate.AddSeconds(SESSION_DURATION);
            session.Expires = expireDate;

            session.Userid = userID;

            repository.GetContextUser().AddSession(session);
            return sessionid;
        }

        public static string GenerateSessionID()
        {
            var bytes = new byte[SESSION_LENGTH];

            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(bytes);
            }

            return new string(bytes.Select(x => ALLOWED_CHARACTERS[x % ALLOWED_CHARACTERS.Length]).ToArray());
        }

        public static string GenerateSalt()
        {
            var bytes = new byte[SALT_LENGTH];

            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(bytes);
            }

            return new string(bytes.Select(x => ALLOWED_CHARACTERS[x % ALLOWED_CHARACTERS.Length]).ToArray());
        }

        public static string CalculateHash(string password, string salt)
        {
            var valueBytes = KeyDerivation.Pbkdf2(
                             password: password,
                             salt: Encoding.UTF8.GetBytes(salt),
                             prf: KeyDerivationPrf.HMACSHA512,
                             iterationCount: 2971,
                             numBytesRequested: 512 / 8);
            return Convert.ToBase64String(valueBytes);
        }

        /**
         * Every logined user can read common data.
         */
        public static bool CanReadCommonData(User user)
        {
            if (user != null)
            {
                return true;
            } else
            {
                return false;
            }
            
        }

        public static bool CanWriteCommonData(User user)
        {
            return CanReadCommonData(user);
        }

        /**
         * Only admins can read that data.
         */
        public static bool CanReadAdminData(User user)
        {
            if (user != null && user.Admin == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /**
         * Only admins can write that data.
         */
        public static bool CanWriteAdminData(User user)
        {
            return CanReadAdminData(user);
        }


        public static User GetUserBySessionID(string sessionid, Repository repository)
        {
            Session session = repository.GetContextUser().SessionsLocal().GetValue(sessionid);
            //Session session = repository.Sessions.FirstOrDefault(s => s.Id == sessionid);
            if (session != null)
            {
                if (repository.GetContextUser().UsersLocal() == null)
                {
                    repository.GetContextUser().UpdateUsersLocal();
                }
                User user = repository.GetContextUser().UsersLocal()[session.Userid];
                return user;
                //return repository.Users.First(u => u.Id == (session.Userid));
            } else
            {
                return null; 
            }
            
        }

        public static bool canEditStructures(string sessionid, Repository repository)
        {
            User user = repository.Users.First(u => u.Id == (repository.GetContextUser().SessionsLocal()[sessionid].Userid));
            //User user = repository.Users.First(u => u.Id == (repository.Sessions.First(s => s.Id == sessionid).Userid));
            return (user.Structureeditor.GetValueOrDefault(0) == 1);
        }

        public static bool CanReadStructure(string sessionid, Repository repository, int structureid)
        {
            User user = repository.Users.First(u => u.Id == (repository.GetContextUser().SessionsLocal()[sessionid].Userid));
            //User user = repository.Users.First(u => u.Id == (repository.Sessions.First(s => s.Id == sessionid).Userid));
            return CanReadStructure(user, repository, structureid);
        }

        public static bool CanReadStructure(User user, Repository repository, int structureid)
        {
            if (user.Admin.GetValueOrDefault() > 0 || user.Masterpersonneleditor.GetValueOrDefault() > 0)
            {
                return true;
            }

            if (!repository.StructuresLocal().ContainsKey(structureid))
            {
                return false;
            }
            int structureUser = user.Structure.GetValueOrDefault();
            if (structureUser == 0)
            {
                return false;
            }
            //Structure structure = repository.StructuresLocal()[structureid];
            if (structureid == structureUser)
            {
                return true;
            }
            return repository.IsGrandParent(structureid, structureUser);


            //return false;
        }

        public static bool CanEditPerson(User user, Repository repository)
        {
            if (user.Admin.GetValueOrDefault() > 0 || user.Masterpersonneleditor.GetValueOrDefault() > 0 || user.Personneleditor.GetValueOrDefault() > 0)
            {
                return true;
            }
            return false;
        }

        //public static bool CanEditPerson(User user, Repository repository, int structureid)
        //{
        //    if (user.Admin.GetValueOrDefault() > 0 || user.Masterpersonneleditor.GetValueOrDefault() > 0)
        //    {
        //        return true;
        //    }

        //    if (!repository.StructuresLocal().ContainsKey(structureid))
        //    {
        //        return false;
        //    }
        //    int structureUser = user.Structure.GetValueOrDefault();
        //    if (structureUser == 0)
        //    {
        //        return false;
        //    }
        //    //Structure structure = repository.StructuresLocal()[structureid];
        //    if (structureid == structureUser)
        //    {
        //        return true;
        //    }
        //    return repository.IsGrandParent(structureid, structureUser);


        //    //return false;
        //}
    }
}
