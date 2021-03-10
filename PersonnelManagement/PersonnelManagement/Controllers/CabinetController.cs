using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Models;
using PersonnelManagement.Services;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;

namespace PersonnelManagement.Controllers
{
    [Produces("application/json")]
    [Route("api/Cabinet")]
    public class CabinetController : Controller
    {

        private Repository repository;

        public CabinetController(Repository repository)
        {
            this.repository = repository;
        }

        // GET: api/Cabinet/SearchФамилия
        [HttpGet("Search{fio}")]
        public IEnumerable<CabinetdataManager> GetCabinet([FromRoute] string fio)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (fio.Contains("nullsearch"))
            {
                fio = "";
            }
            if (IdentityService.IsLogined(sessionid, repository)/* && fio.Length > 1*/ /*Altksei searching condition*/)
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                List<CabinetdataManager> cabinetdatasUnfiltered = repository.GetCabinetdatas(user, fio);
                List<CabinetdataManager> cabinetdatas = new List<CabinetdataManager>();
                foreach (CabinetdataManager cabinetdataUnfiltered in cabinetdatasUnfiltered)
                {
                    if (cabinetdataUnfiltered != null)
                    {
                        bool isAllowedToReadCandidate = repository.isAllowedToReadCandidate(user, cabinetdataUnfiltered.Id);
                        if (isAllowedToReadCandidate && cabinetdatas.Count < 300)
                        {
                            cabinetdatas.Add(cabinetdataUnfiltered);
                        }
                    }
                    
                }
                return cabinetdatas;
            }
            List<CabinetdataManager> empty = new List<CabinetdataManager>();
            //return empty;
            return empty;
        }

        // GET: api/Cabinet/Single5
        [HttpGet("Single{id}")]
        public CabinetdataManager GetCabinetSingle([FromRoute] int id)
        {
            if (id < 0)
            {
                id = -id; // Зачастую (если не всегда), фронтэнд кидает 
            }
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool isAllowedToReadCandidate = repository.isAllowedToReadCandidate(user, id);
                if (isAllowedToReadCandidate)
                {
                    CabinetdataManager cabinetdataManager = repository.GetCabinetdataManager(user, id);
                    return cabinetdataManager;
                }

                return null;
            }
            //return empty;
            return null;
        }

        // GET: api/Cabinet/Ident5
        [HttpGet("Ident{id}")]
        public CabinetdataManager GetCabinetIdent([FromRoute] string id)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                //bool isAllowedToReadCandidate = repository.isAllowedToReadCandidate(user, id);
                //if (isAllowedToReadCandidate)
                //{
                    CabinetdataManager cabinetdataManager = repository.GetCabinetdataManagerByIdent(user, id);
                    return cabinetdataManager;
                //}
            }
            //return empty;
            return null;
        }

        [HttpPost()]
        public IActionResult ManageCabinet([FromBody]CabinetdataManager cabinet)
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

            if (cabinet.Action == 0) // Создание нового кабинета
            {
                int status = repository.AddCabinet(user, cabinet);
                if (status == 0) // Создан
                {
                    return new ObjectResult(Keys.SUCCESS_SHORT + ":Новый личный кабинет создан");
                } else if (status == 1) // 
                {
                    return new ObjectResult(Keys.ERROR_SHORT + ":Личный кабинет уже существует");
                } else if (status == 2) // 
                {
                    return new ObjectResult(Keys.ERROR_SHORT + ":Личный кабинет уже существует, но кандидат забракован");
                }
                
            } else
            if (cabinet.Action == 1) // Бракование кандидата
            {
                repository.DenyCabinet(user, cabinet);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Кандидат забракован");
            }
            else
            if (cabinet.Action == 2) // лок/анлок биографии
            {
                repository.ToggleBiography(user, cabinet);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Блокировка биографии изменена");
            }
            if (cabinet.Action == 3) // лок/анлок анкеты
            {
                repository.ToggleProfile(user, cabinet);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Блокировка анкеты изменена");
            }
            if (cabinet.Action == 4) // лок/анлок личного листка по учету кадров
            {
                repository.ToggleSheet(user, cabinet);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Блокировка личного листка по учету кадров изменена");
            }
            if (cabinet.Action == 5) // лок/анлок декларации
            {
                repository.ToggleDeclaration(user, cabinet);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Блокировка декларации изменена");
            }

            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

        [HttpGet("LoginDate/{id}")]
        public IActionResult LoginData([FromRoute] int id)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                //bool isAllowedToReadCandidate = repository.isAllowedToReadCandidate(user, id);
                //if (isAllowedToReadCandidate)
                //{
                CabinetdataManager cabinetdataManager = repository.GetCabinetdataManager(user, id);
                MemoryStream mem = new MemoryStream();
                Models.CandidatLoginData.CreateDocument(mem, repository, cabinetdataManager);
                mem.Position = 0;

                return File(mem, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", cabinetdataManager.Usersurname + "_candidate.docx");
                //}
            }
            //return empty;
            return null;
        }
    }
}