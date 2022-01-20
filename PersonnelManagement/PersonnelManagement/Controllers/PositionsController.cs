using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Models;
using PersonnelManagement.Services;
using PersonnelManagement.USERS;

namespace PersonnelManagement.Controllers
{
    [Produces("application/json")]
    [Route("api/Positions")]
    public class PositionsController : Controller
    {
        private Repository repository;

        public PositionsController(Repository repository)
        {
            this.repository = repository;
        }

        // Is allowed to edit structures.
        // GET: api/Positions
        [HttpGet]
        public bool IsAllowedToEditStructures()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                return repository.IsAllowedStructureToEdit(user);
            }
            return false;
        }


        // GET: api/Positions/Stringaltranks5
        [HttpGet("Stringaltranks{id}")]
        public string GetStringaltranks([FromRoute] int id)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool isAllowedToReadStructure = repository.isAllowedToReadStructure(user, id);
                if (isAllowedToReadStructure)
                {
                    return repository.GetAltranksList(repository.GetPositions(id, user.Date.GetValueOrDefault()), true);
                }

                return "";
            }
            return "";
        }

        // GET: api/Positions/Stringaltrankload5
        [HttpGet("Stringaltrankload{id}")]
        public string GetStringaltrankload([FromRoute] int id)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool isAllowedToReadStructure = repository.isAllowedToReadStructure(user, id);
                if (isAllowedToReadStructure)
                {
                    string altrankslist = repository.GetAltrankList(repository.Positions.First(p => p.Id == id));
                    return altrankslist;
                }

                return "";
            }
            return "";
        }

        // GET: api/Positions/Stringmrds5
        [HttpGet("Stringmrds{id}")]
        public string GetStringmrds([FromRoute] int id)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool isAllowedToReadStructure = repository.isAllowedToReadStructure(user, id);
                if (isAllowedToReadStructure)
                {
                    string mrdsshort = repository.GetMrdsListShort(repository.GetPositions(id, user.Date.GetValueOrDefault(), true, true, true));
                    return mrdsshort;
                }
                
                return "";
            }
            return "";
        }

        // GET: api/Positions/Stringmrdids5
        // В отличие от старого метода еще включает в себя id должности в самом начале
        [HttpGet("Stringmrdids{id}")]
        public string GetStringmrdids([FromRoute] int id)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool isAllowedToReadStructure = repository.isAllowedToReadStructure(user, id);
                if (isAllowedToReadStructure)
                {
                    string mrdsshort = repository.GetMrdsListShortId(repository.GetPositions(id, user.Date.GetValueOrDefault(), true, true, true));
                    return mrdsshort;
                }

                return "";
            }
            return "";
        }

        // GET: api/Positions/Listmrd5
        [HttpGet("Listmrd{id}")]
        public string GetListmrd([FromRoute] int id)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool isAllowedToReadStructure = repository.isAllowedToReadStructure(user, id);
                if (isAllowedToReadStructure)
                {
                    return repository.GetMrdsList(repository.GetPositions(id, user.Date.GetValueOrDefault(), true, true));
                }

                return "";
            }
            return "";
        }

        /**
         * Returns array of mrd ids of position. , - separated.
         */
        // GET: api/Positions/Mrd5
        [HttpGet("Mrd{id}")]
        public string GetMrds([FromRoute] int id)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool isAllowedToReadStructure = repository.isAllowedToReadStructure(user, id);
                if (isAllowedToReadStructure)
                {
                    return repository.GetMrds(id);
                }

                return "" ;
            }
            return "";
        }


        // GET: api/Positions/Solo5
        [HttpGet("Solo{id}")]
        public Position GetPositionSolo([FromRoute] int id)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool isAllowedToReadStructure = repository.isAllowedToReadStructure(user, id);
                if (isAllowedToReadStructure)
                {
                    return repository.Positions.FirstOrDefault(p => p.Id == id);
                }

                return new Position();
            }
            return new Position();
        }

        // GET: api/Positions/5
        [HttpGet("{id}")]
        public IEnumerable<Position> GetPosition([FromRoute] int id)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool isAllowedToReadStructure = repository.isAllowedToReadStructure(user, id);
                if (isAllowedToReadStructure)
                {
                    return repository.GetPositions(id, user.Date.GetValueOrDefault(), true, true);
                }

                return new List<Position>();
            }
            List<Position> empty = new List<Position>();
            return empty;
        }


        // GET: api/Positions/Curations5,6,12,8&20  curatiorlist&positionid
        [HttpGet("Curations{get}")]
        public List<StructureTree> GetCurations([FromRoute] string get)
        {
            string[] splittedget = get.Split('&');
            int positionid = Int32.Parse(splittedget[1]);
            int[] ids = splittedget[0].Split(',').Select(int.Parse).ToArray();
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                return repository.GetCurations(ids, positionid, user.Date.GetValueOrDefault());
            }
            return new List<StructureTree>();
        }

        // GET: api/Positions/Heading8&20  structureid(heading)&positionid
        [HttpGet("Heading{get}")]
        public StructureTree GetHeading([FromRoute] string get)
        {
            string[] splittedget = get.Split('&');
            int positionid = Int32.Parse(splittedget[1]);
            int id = Int32.Parse(splittedget[0]); 
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                return repository.GetHeading(id, positionid, user.Date.GetValueOrDefault());
            }
            return new StructureTree();
        }


        // POST: api/Positions
        [HttpPost]
        public IActionResult PostPosition([FromBody] PositionManagement positionManagement)
        {

            /**
             * Проверить доступ
             */
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.canEditStructures(sessionid, repository);
                if (!hasAccess)
                {
                    return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");

                }
            }
            else
            {
                return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");
            }

            if (positionManagement == null)
            {
                return new ObjectResult(Keys.ERROR_SHORT + ":Произошла ошибка в работе с должностью.");
            }

            /**
             * Means, we add new position.
             */
            if (positionManagement.Type.Equals(Keys.POSITION_MANAGEMENT_ADDNEWPOSITION))
            {
                
                repository.AddPosition(positionManagement, user, 0);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Должность создана");
            }
            /**
             * Remove position.
             */
            else if (positionManagement.Type.Equals(Keys.POSITION_MANAGEMENT_REMOVEPOSITION))
            {
                repository.RemovePosition(positionManagement, user);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Должность удалена");
            }
            /**
             * Remove position while working with decree.
             */
            else if (positionManagement.Type.Equals(Keys.POSITION_MANAGEMENT_REMOVEPOSITIONDECREE))
            {
                repository.RemovePositiondecree(positionManagement, user);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Должность убрана из проекта приказа");
            }
            /**
             * Edit position.
             * Редактируем должность (в проекте приказа).
             */
            else if (positionManagement.Type.Equals(Keys.POSITION_MANAGEMENT_RENAMEPOSITION))
            {
                repository.EditPosition(positionManagement, user);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Должность изменена");
            }
            /**
             * Edit position while working with decree.
             */
            else if (positionManagement.Type.Equals(Keys.POSITION_MANAGEMENT_RENAMEPOSITIONDECREE))
            {
                repository.EditPositiondecree(positionManagement, user);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Должность отредактирована");
            }
            else if (positionManagement.Type.Equals(Keys.POSITION_MANAGEMENT_UPDATEPOSITIONSBYPOSITIONTYPE))
            {
                repository.UpdatePositionsByPositiontypes(positionManagement, user);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Должность обновлена");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }
    }
}