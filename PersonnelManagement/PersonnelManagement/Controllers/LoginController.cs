using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Models;
using PersonnelManagement.Services;

namespace PersonnelManagement.Controllers
{
    //[Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : Controller
    {

        private IdentityService identityService;
        private Repository repository;
        //private static string salt = IdentityService.GenerateSalt();

        public LoginController(IdentityService identityService, Repository repository)
        {
            this.identityService = identityService;
            this.repository = repository;
        }


        [HttpPost()]
        public IActionResult Login([FromBody]LoginPassword loginPassword )
        {
            string login = loginPassword.Login;
            string password = loginPassword.Password;
            if (login == null || login.Length == 0)
            {
                return new ObjectResult(Keys.ERROR_SHORT + ":Введите логин");
            }
            if (password == null || password.Length == 0)
            {
                return new ObjectResult(Keys.ERROR_SHORT + ":Введите пароль");
            }

            User user = repository.Users.FirstOrDefault(r => r.Name == login);
            /**
             * Login exists
             */
            if (user != null)
            {
                if (user.Password == null)
                {
                    //return new ObjectResult("Пароль пуст");
                    string salt = IdentityService.GenerateSalt();
                    string hash = IdentityService.CalculateHash(password, salt);
                    user.Password = hash;
                    user.Salt = salt;
                    repository.SaveChanges();
                    repository.UpdateUsersLocal();
                    return new ObjectResult(Keys.SUCCESS_SHORT + ":Пароль изменен. Выполните вход.");
                } else if (user.Password != null)
                {
                    string salt = user.Salt;
                    string hash = IdentityService.CalculateHash(password, salt);
                    /**
                     * Finally login
                     */
                    if (hash == user.Password)
                    {
                        string sessionId = identityService.GenerateSession(user.Id, repository);
                        GenerateSessionCookie(sessionId, IdentityService.SESSION_DURATION);
                        if ((user.Personnelread > 0 || user.Personneleditor > 0) && user.Admin == 0 && user.Masterpersonneleditor == 0)
                        {
                            user.Mode = 1;
                        } else
                        {
                            user.Mode = 0;
                        }
                        user.Fullmode = 0; // Режим по умолчанию не выбран
                        repository.SaveChanges();
                        repository.UpdateUsersLocal();
                        return new ObjectResult(Keys.IDENTITY_SESSIONID_PREFIX + ":" + sessionId + ":" + Keys.SUCCESS_SHORT + ":Вход выполнен");
                    } else
                    {
                        return new ObjectResult(Keys.ERROR_SHORT + ":Пароль введен неверно");
                    }
                }
            } else
            {
                return new ObjectResult(Keys.ERROR_SHORT + ":Введенный логин некорректен");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Введенные данные некорректны");
        }

        public void GenerateSessionCookie(string sessionid, int expirationTime)
        {
            Response.Cookies.Append(
                Keys.COOKIES_SESSION,
                sessionid,
                new Microsoft.AspNetCore.Http.CookieOptions()
                {
                    Path = "/",
                    Expires = DateTimeOffset.Now.AddSeconds(expirationTime)

                });
        }

    }
}