using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Models;
using PersonnelManagement.Services;
using PersonnelManagement.USERS;

namespace PersonnelManagement.Controllers
{
    [Produces("application/json")]
    [Route("api/Positioncategory")]
    public class PositioncategoryController : Controller
    {

        private Repository repository;


        public PositioncategoryController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Positioncategory> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    if (repository.PositioncategoriesLocal() == null)
                    {
                        repository.UpdatePositioncategoriesLocal();
                    }
                    return repository.PositioncategoriesLocal().Values;


                    //return repository.Positioncategories;
                }
            }
            List<Positioncategory> empty = new List<Positioncategory>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdatePositioncategory([FromBody]Positioncategory newPositioncategory)
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
            if (newPositioncategory.Id == 0)
            {
                repository.AddPositioncategory(newPositioncategory);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Категория успешно добавлена");
            }
            else
            {
                return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
            }
        }

    }
}