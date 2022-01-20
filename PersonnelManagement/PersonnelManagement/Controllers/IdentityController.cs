using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using PersonnelManagement.Models;
using PersonnelManagement.Services;
using PersonnelManagement.USERS;

namespace PersonnelManagement.Controllers
{
    [Route("api/Identity")]
    public class IdentityController : Controller
    {
        private IdentityService identityService;
        private Repository repository;

        public IdentityController(IdentityService identityService, Repository repository)
        {
            this.identityService = identityService;
            this.repository = repository;
        }

        [HttpGet()]
        public Dictionary<string, string> GetIdentityData()
        {
            Dictionary<string, string> identityData = new Dictionary<string, string>();
            


            if (IdentityService.IsLogined(Request.Cookies[Keys.COOKIES_SESSION], repository))
            {

                identityData[Keys.IDENTITY_LOGINED_KEY] = Keys.IDENTITY_LOGINED_TRUE;
                identityData[Keys.IDENTITY_STRUCTURE_KEY] = IdentityService.getStructureOfUser(Request.Cookies[Keys.COOKIES_SESSION], repository).ToString();
                Dictionary<string, string> dictionary = IdentityService.getRightsOfUser(Request.Cookies[Keys.COOKIES_SESSION], repository);
                identityData[Keys.IDENTITY_LOGIN_KEY] = dictionary[Keys.IDENTITY_LOGIN_KEY];
                identityData[Keys.IDENTITY_MASTERPERSONNELEDITOR_KEY] = dictionary[Keys.IDENTITY_MASTERPERSONNELEDITOR_KEY];
                identityData[Keys.IDENTITY_PERSONNELEDITOR_KEY] = dictionary[Keys.IDENTITY_PERSONNELEDITOR_KEY];
                identityData[Keys.IDENTITY_PERSONNELREAD_KEY] = dictionary[Keys.IDENTITY_PERSONNELREAD_KEY];
                identityData[Keys.IDENTITY_STRUCTUREEDITOR_KEY] = dictionary[Keys.IDENTITY_STRUCTUREEDITOR_KEY];
                identityData[Keys.IDENTITY_STRUCTUREREAD_KEY] = dictionary[Keys.IDENTITY_STRUCTUREREAD_KEY];
                identityData[Keys.IDENTITY_MODE_KEY] = dictionary[Keys.IDENTITY_MODE_KEY];
                identityData[Keys.IDENTITY_ADMIN_KEY] = dictionary[Keys.IDENTITY_ADMIN_KEY];
                identityData[Keys.IDENTITY_DECREE_KEY] = dictionary[Keys.IDENTITY_DECREE_KEY];
                identityData[Keys.IDENTITY_DECREE_NAME_KEY] = dictionary[Keys.IDENTITY_DECREE_NAME_KEY];// MODE
                identityData[Keys.IDENTITY_POSITIONCOMPACT_KEY] = dictionary[Keys.IDENTITY_POSITIONCOMPACT_KEY];
                identityData[Keys.IDENTITY_DATE_KEY] = dictionary[Keys.IDENTITY_DATE_KEY];
                identityData[Keys.IDENTITY_SIDEBAR_DISPLAY_KEY] = dictionary[Keys.IDENTITY_SIDEBAR_DISPLAY_KEY];
                identityData[Keys.IDENTITY_CURRENTSTRUCTURETREE_KEY] = dictionary[Keys.IDENTITY_CURRENTSTRUCTURETREE_KEY];
                identityData[Keys.IDENTITY_FULLMODE_KEY] = dictionary[Keys.IDENTITY_FULLMODE_KEY];
            }
            else
            {
                identityData[Keys.IDENTITY_LOGINED_KEY] = Keys.IDENTITY_LOGINED_FALSE;
                identityData[Keys.IDENTITY_LOGIN_KEY] = "";
                identityData[Keys.IDENTITY_STRUCTURE_KEY] = "0";
                identityData[Keys.IDENTITY_MASTERPERSONNELEDITOR_KEY] = "0";
                identityData[Keys.IDENTITY_PERSONNELEDITOR_KEY] = "0";
                identityData[Keys.IDENTITY_PERSONNELREAD_KEY] = "0";
                identityData[Keys.IDENTITY_STRUCTUREEDITOR_KEY] = "0";
                identityData[Keys.IDENTITY_STRUCTUREREAD_KEY] = "0";
                identityData[Keys.IDENTITY_MODE_KEY] = "0";
                identityData[Keys.IDENTITY_ADMIN_KEY] = "0";
                identityData[Keys.IDENTITY_DECREE_KEY] = "0";
                identityData[Keys.IDENTITY_DECREE_NAME_KEY] = "";
                identityData[Keys.IDENTITY_POSITIONCOMPACT_KEY] = "0";
                identityData[Keys.IDENTITY_DATE_KEY] = DateTime.Now.ToString("yyyy-MM-dd");
                identityData[Keys.IDENTITY_SIDEBAR_DISPLAY_KEY] = "0";
                identityData[Keys.IDENTITY_CURRENTSTRUCTURETREE_KEY] = "";
                identityData[Keys.IDENTITY_FULLMODE_KEY] = "0";
            }
            return identityData;
        }

        // Возвращаем объект user, в котором описано, какие доступы присутствуют и отсутствуют у пользователя.
        // GET: api/Identity/User
        [HttpGet("User")]
        public User GetIdentityUserData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = new User();
            if (IdentityService.IsLogined(Request.Cookies[Keys.COOKIES_SESSION], repository))
            {
                User dbUser = IdentityService.GetUserBySessionID(sessionid, repository);
                user = dbUser.GetUserModel(repository.GetContextUser());
            }
            else
            {
                user = new User();
            }
            return user;
        }

        // GET: api/Identity/Switch
        [HttpGet("Switch")]
        public string Switch()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(Request.Cookies[Keys.COOKIES_SESSION], repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                repository.SwitchUser(user);
            }
            return "switch";
        }

        // GET: api/Identity/Org
        [HttpGet("Org")]
        public string Org()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(Request.Cookies[Keys.COOKIES_SESSION], repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                repository.OrgUser(user);
            }
            return "org";
        }

        // GET: api/Identity/People
        [HttpGet("People")]
        public string People()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(Request.Cookies[Keys.COOKIES_SESSION], repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                repository.PeopleUser(user);
            }
            return "people";
        }

        // GET: api/Identity/People
        [HttpGet("Decree")]
        public string Decree()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(Request.Cookies[Keys.COOKIES_SESSION], repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                repository.PeopleUser(user);
            }
            return "decree";
        }

        // GET: api/Identity/Fullmode
        [HttpGet("Fullmode{id}")]
        public IActionResult Fullmode([FromRoute] int id)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(Request.Cookies[Keys.COOKIES_SESSION], repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                User contextUser = repository.Users.First(u => u.Id == user.Id);
                contextUser.Fullmode = id; // пока что ставим мод напрямую.
                repository.SaveChanges();
                repository.GetContextUser().UpdateUsersLocal();
                //repository.ChangeFullmode(user, id);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Режим изменен");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Нет доступа");
        }

        [HttpPost()]
        public IActionResult UpdateUserSettings([FromBody]UserSettings userSettings)
        {
            /**
             * Check access.
             */
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
            }
            else
            {
                return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");
            }


            repository.GetContextUser().UpdateUserSettings(user, userSettings);
            return new ObjectResult(Keys.SUCCESS_SHORT + ":Пользовательские настройки сохранены");
        }


    }
}