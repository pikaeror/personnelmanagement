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
    [Route("api/Drivercategory")]
    public class DrivercategoryController : Controller
    {

        private Repository repository;


        public DrivercategoryController(Repository repository)
        {
            this.repository = repository;

        }

        [HttpGet()]
        public IEnumerable<Drivercategory> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    return repository.Drivercategories;
                }
            }
            List<Drivercategory> empty = new List<Drivercategory>();
            return empty;
        }



        [HttpPost()]
        public IActionResult UpdateDrivercategory([FromBody]Drivercategory newDrivercategory)
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
             * Means, we add new structure type.
             */
            if (newDrivercategory.Id == 0)
            {
                repository.AddDrivercategory(newDrivercategory);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Категория водительского удостоверения успешно добавлена");
            }
            else if (newDrivercategory.Id != 0)
            {
                repository.UpdateDrivercategory(newDrivercategory);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Категория водительского удостоверения успешно обновлена");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}