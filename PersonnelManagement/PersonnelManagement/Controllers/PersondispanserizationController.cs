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
    [Produces("application/json")]
    [Route("api/Persondispanserization")]
    public class PersondispanserizationController : Controller
    {

        private Repository repository;

        public PersondispanserizationController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost()]
        public IActionResult PostDispanserization([FromBody]Persondispanserization persondispanserization)
        {

            /**
             * Check access.
             */
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanEditPerson(user, repository);
                if (!hasAccess)
                {
                    return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");

                }
            }
            else
            {
                return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");
            }
            //repository.AddPersonContract(user, personcontract);
            //return new ObjectResult(Keys.SUCCESS_SHORT + ":Контракт добавлен");

            if (persondispanserization.Id == 0)
            {
                repository.AddPersonDispanserization(user, persondispanserization);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Диспансеризация добавлена");
            }
            else if (persondispanserization.Id > 0)
            {
                repository.ChangePersonDispanserization(user, persondispanserization);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Диспансеризация изменена");
            }
            else if (persondispanserization.Id < 0)
            {
                repository.DeletePersonDispanserization(user, persondispanserization);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Диспансеризация удалена");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}