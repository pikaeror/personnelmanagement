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
    [Route("api/Person")]
    public class PersonController : Controller
    {
        private Repository repository;

        public PersonController(Repository repository)
        {
            this.repository = repository;
        }

        // GET: api/Person/5
        [HttpGet("{id}")]
        public IEnumerable<PersonManager> GetPerson([FromRoute] int id)
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
                bool isAllowedToReadStructure = repository.isAllowedToReadStructure(user, id);
                if (isAllowedToReadStructure)
                {
                    IEnumerable<Position> positions = repository.GetPositions(id, user.Date.GetValueOrDefault(), true, true);
                    List<PersonManager> persons = repository.GetPersons(user, positions, id);
                    return persons;
                }

                return new List<PersonManager>();
            }
            List<PersonManager> empty = new List<PersonManager>();
            //return empty;
            return empty;
        }

        // GET: api/Person/Single5
        [HttpGet("Single{id}")]
        public PersonManager GetPersonSingle([FromRoute] int id)
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
                bool isAllowedToReadPerson = repository.isAllowedToReadPerson(user, id);
                if (isAllowedToReadPerson)
                {
                    PersonManager personManager = repository.GetPersonManager(user, id);
                    return personManager;
                }

                return null;
            }
            //return empty;
            return null;
        }

        // GET: api/Person/Deletephoto5
        [HttpGet("Deletephoto{id}")]
        public int DeletePersonphoto([FromRoute] int id)
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
                bool isAllowedToReadPerson = repository.isAllowedToReadPerson(user, id);
                if (isAllowedToReadPerson)
                {
                    repository.DeletePhoto(user, id);
                    //return repository.PersonsLocal().GetValue(id);
                    return 1;
                }

                return 0;
            }
            //return empty;
            return 0;
        }

        // GET: api/Person/Mainphoto5
        [HttpGet("Mainphoto{id}")]
        public int MainPersonphoto([FromRoute] int id)
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
                bool isAllowedToReadPerson = repository.isAllowedToReadPerson(user, id);
                if (isAllowedToReadPerson)
                {
                    repository.MainPhoto(user, id);
                    //return repository.PersonsLocal().GetValue(id);
                    return 1;
                }

                return 0;
            }
            //return empty;
            return 0;
        }

        // GET: api/Person/Media5
        [HttpGet("Media{id}")]
        public IEnumerable<PersonphotoManager> GetPersonMedia([FromRoute] int id)
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
                bool isAllowedToReadPerson = repository.isAllowedToReadPerson(user, id);
                if (isAllowedToReadPerson)
                {
                    List<Personphoto> personphotos = repository.GetPersonMedia(user, id);
                    List<PersonphotoManager> personphotoManagers = new List<PersonphotoManager>();
                    foreach(Personphoto personphoto in personphotos)
                    {
                        personphotoManagers.Add(new PersonphotoManager(personphoto));
                    }
                    return personphotoManagers;
                }

                return new List<PersonphotoManager>();
            }
            //return empty;
            return new List<PersonphotoManager>();
        }

        // GET: api/Person/Appoint5&10
        [HttpGet("Appoint{ids}")]
        public int AppointPerson([FromRoute] string ids)
        {
            // ids - первая это id person, вторая id должности/подразделение
            // appointid > 0 - прикрепляет к должности
            // appointid < 0 - прикрепляет к подразделению.
            string[] idsSplit = ids.Split('&');
            int id = Int32.Parse(idsSplit[0]);
            int appointid = Int32.Parse(idsSplit[1]);
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool isAllowedToEditPerson = repository.isAllowedToEditPerson(user, id);
                if (isAllowedToEditPerson)
                {
                    repository.AppointPerson(user, id, appointid);
                    return 1;
                }

                return 0;
            }
            //return empty;
            return 0;
        }

        // GET: api/Person/Takeoff5
        [HttpGet("Takeoff{ids}")]
        public int TakeoffPerson([FromRoute] string ids)
        {
            // ids - первая это id person, вторая id должности/подразделение
            int id = Int32.Parse(ids);
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool isAllowedToEditPerson = repository.isAllowedToEditPerson(user, id);
                if (isAllowedToEditPerson)
                {
                    repository.TakeoffPerson(user, id);
                    return 1;
                }

                return 0;
            }
            //return empty;
            return 0;
        }

        // GET: api/Person/Specialphotospreview5
        [HttpGet("Specialphotospreview{id}")]
        public IEnumerable<PersonphotoManager> GetSpecialphotospreview([FromRoute] int id)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                Person person = repository.PersonsLocal().GetValue(id);
                if (person == null)
                {
                    return new List<PersonphotoManager>();
                }
                List<PersonphotoManager> photos = new List<PersonphotoManager>();
                    bool isAllowedToReadPerson = repository.isAllowedToReadPerson(user, person.Id);
                    if (isAllowedToReadPerson)
                    {
                        Personphoto personphoto = repository.GetPersonMediaMain(user, person.Id);
                        if (personphoto != null)
                        {
                            photos.Add(new PersonphotoManager(personphoto));
                        }
                    }
                return photos;
            }
            //return empty;
            return new List<PersonphotoManager>();
        }

        // GET: api/Person/PhotospreviewФамилия
        [HttpGet("Photospreview{fio}")]
        public IEnumerable<PersonphotoManager> GetPhotospreview([FromRoute] string fio)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                List<PersonManager> personsUnfiltered = repository.GetPersons(user, fio, true, true);
                List<Person> persons = new List<Person>();
                List<PersonphotoManager> photos = new List<PersonphotoManager>();
                foreach (Person person in personsUnfiltered)
                {
                    bool isAllowedToReadPerson = repository.isAllowedToReadPerson(user, person.Id);
                    if (isAllowedToReadPerson && persons.Count < 10)
                    {
                        persons.Add(person);
                        Personphoto personphoto = repository.GetPersonMediaMain(user, person.Id);
                        if (personphoto != null)
                        {
                            photos.Add(new PersonphotoManager(personphoto));
                        }
                    }
                }
                return photos;
            }
            //return empty;
            return new List<PersonphotoManager>();
        }

        // GET: api/Person/Positionsphotospreview5
        [HttpGet("Positionsphotospreview{id}")]
        public IEnumerable<PersonphotoManager> GetPositionsphotospreview([FromRoute] int id)
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
                bool isAllowedToReadStructure = repository.isAllowedToReadStructure(user, id);
                if (isAllowedToReadStructure)
                {
                    IEnumerable<Position> positions = repository.GetPositions(id, user.Date.GetValueOrDefault(), true, true);
                    List<Person> persons = new List<Person>();
                    List<PersonManager> personsUnfiltered = repository.GetPersons(user, positions, id, true);
                    List<PersonphotoManager> photos = new List<PersonphotoManager>();
                    foreach (Person person in personsUnfiltered)
                    {
                        bool isAllowedToReadPerson = repository.isAllowedToReadPerson(user, person.Id);
                        if (isAllowedToReadPerson && persons.Count < 10)
                        {
                            persons.Add(person);
                            Personphoto personphoto = repository.GetPersonMediaMain(user, person.Id);
                            if (personphoto != null)
                            {
                                photos.Add(new PersonphotoManager(personphoto));
                            }
                        }
                    }
                    return photos;
                }
                return new List<PersonphotoManager>();
            }
            //return empty;
            return new List<PersonphotoManager>();
        }

        // GET: api/Person/SearchФамилия
        /// <summary>
        /// Поиск личных дел по фамилии/имени/отчеству
        /// </summary>
        /// <param name="fio"></param>
        /// <returns></returns>
        [HttpGet("Search{fio}")]
        public IEnumerable<PersonManager> SearchPerson([FromRoute] string fio)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository) && fio.Length > 1) // fio.Length - минимальное число символов для поиска - 2.
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                List<PersonManager> personsUnfiltered = repository.GetPersons(user, fio, true, true);
                List<PersonManager> persons = new List<PersonManager>();
                foreach (PersonManager person in personsUnfiltered)
                {
                    bool isAllowedToReadPerson = repository.isAllowedToReadPerson(user, person.Id);
                    if (isAllowedToReadPerson && persons.Count < 50)
                    {
                        persons.Add(person);
                    }
                }
                return persons;
            }
            List<PersonManager> empty = new List<PersonManager>();
            //return empty;
            return empty;
        }

        /**
         * Создает 
         */
        // GET: api/Person/Create
        [HttpGet("Create{fio}")]
        public int CreatePerson([FromRoute] string fio)
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
                    return 0;
                }

                int id = repository.CreatePerson(user, fio);
                return id;
            }
            else
            {
                return 0;
            }


            return 0;
        }

        /**
         * Удаляет 
         */
        // GET: api/Person/Remove
        [HttpGet("Remove{id}")]
        public int RemovePerson([FromRoute] int id)
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
                    return 0;
                }

                repository.RemovePerson(user, id);
                return 1;
            }
            else
            {
                return 0;
            }


            return 0;
        }

        [HttpPost()]
        public IActionResult ManagePerson([FromBody]Person person)
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

            repository.UpdatePerson(user, person);
            return new ObjectResult(Keys.SUCCESS_SHORT + ":Электронное личное дело обновлено");
            //return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

    }
}