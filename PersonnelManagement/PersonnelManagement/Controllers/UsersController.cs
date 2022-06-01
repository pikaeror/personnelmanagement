using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PersonnelManagement.Models;
using PersonnelManagement.Services;

namespace PersonnelManagement.Controllers
{
    //[Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private Repository repository;


        public UsersController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<User> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadAdminData(user);
                if (hasAccess)
                {
                    // Поменять на user models?
                    // return repository.Users;
                    List<User> users = new List<User>();
                    foreach (User userToModel in repository.UsersLocal().Values)
                    {
                        User model = userToModel.GetUserModel(repository);
                        users.Add(model);
                    }
                    return users;
                }
            }
            List<User> empty = new List<User>();
            return empty;
        }

        // GET: api/Users/SearchФамилия
        [HttpGet("Search{fio}")]
        public IEnumerable<UserManager> GetUser([FromRoute] string fio)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User userBase = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                userBase = IdentityService.GetUserBySessionID(sessionid, repository);
                List<UserManager> usersUnfiltered = repository.GetUsersSearch(userBase, fio);
                List<UserManager> users = new List<UserManager>();
                foreach (UserManager user in usersUnfiltered)
                {
                    if (users.Count < 50)
                    {
                        users.Add(user);
                    }
                }
                return users;
            }
            List<UserManager> empty = new List<UserManager>();
            //return empty;
            return empty;
        }

        [HttpPost()]
        public IActionResult UpdateUser([FromBody]User newUser)
        {
            /**
             * Check access.
             */
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanWriteAdminData(user);
                if (!hasAccess)
                {
                    return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");
                    
                }
            } else
            {
                return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");
            }
            

            /**
             * Means, we add new user.
             */
            if (newUser.Id == 0) {
                repository.SaveUser(newUser);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Пользователь успешно создан");
            } else if (newUser.Salt != null && newUser.Salt.Equals(Keys.STATUS_NULLIFYPASS))
            {
                repository.NullifyPassUser(newUser);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Пароль обнулен.");
            } else if (newUser.Salt != null && newUser.Salt.Equals(Keys.STATUS_DELETE))
            {
                repository.DeleteUser(newUser);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Пользователь удален.");
                /**
                 * Simply update user by new values.
                 */
            } else
            {
                repository.UpdateUser(newUser);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Данные о пользователе обновлены.");
            }
        }

        [HttpPost("update")]
        public User GetUser([FromBody] User user)
        {
            return repository.GetContext().User.Where(r => r.Id == user.Id).FirstOrDefault();
        }
    }

}