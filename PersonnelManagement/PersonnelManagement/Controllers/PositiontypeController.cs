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
    [Route("api/Positiontype")]
    public class PositiontypeController : Controller
    {
        private Repository repository;


        public PositiontypeController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Positiontype> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    if (repository.PositiontypesLocal() == null)
                    {
                        repository.UpdatePositiontypesLocal();
                    }
                    return repository.PositiontypesLocal().Values;
                }
            }
            List<Positiontype> empty = new List<Positiontype>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdatePositiontype([FromBody]Positiontype newPositiontype)
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
            }
            else
            {
                return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");
            }



            /**
             * Means, we add new positiontype.
             */
            if (newPositiontype.Id == 0)
            {
                bool success = repository.AddPositiontype(newPositiontype);
                if (success)
                {
                    return new ObjectResult(Keys.SUCCESS_SHORT + ":Наименование должности успешно добавлено");
                } else
                {
                    return new ObjectResult(Keys.ERROR_SHORT + ":Наименование должности уже занято");
                }
                
            }
            else
            {
                repository.UpdatePositiontype(newPositiontype);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Наименование должности успешно изменено");
            }
            //return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

        [HttpGet("Current/{id}")]
        public Positiontype GetDataByID([FromRoute] int id)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    if (repository.PositiontypesLocal() == null)
                    {
                        repository.UpdatePositiontypesLocal();
                    }
                    return repository.PositiontypesLocal().Values.Where(r => r.Id == id).ToList()[0];
                }
            }
            return new Positiontype();
        }
    }
}