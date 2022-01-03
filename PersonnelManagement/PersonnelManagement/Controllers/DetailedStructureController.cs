using System;
using System.IO;
using System.Text;
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
    [Route("api/DetailedStructure")]
    public class DetailedStructureController : Controller
    {
        private Repository repository;

        public DetailedStructureController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet()]
        public IEnumerable<StructureExpanded> GetData()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                var allowedStructuresToRead = repository.GetAllowedStructuresToRead(user);
                var length = allowedStructuresToRead.Count();
                return allowedStructuresToRead;
            }
            List<StructureExpanded> empty = new List<StructureExpanded>();
            return empty;
        }

        /**
         * Returns structures without parents (top structures
         */
        // GET: api/DetailedStructure/Top
        [HttpGet("Top")]
        public IEnumerable<int> GetStructureTop()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                //bool isAllowedToReadStructure = repository.isAllowedToReadStructure(user, id);
                //if (isAllowedToReadStructure)
                //{
                int[] topStructuresIDs = new int[1] { 0 };
                if (user.Admin > 0 || user.Masterpersonneleditor > 0 || user.Structure.GetValueOrDefault() == 0)
                {
                    topStructuresIDs = repository.Structures.Where(s => s.Parentstructure == 0).Select(s => s.Id).ToArray();
                } else
                {
                    topStructuresIDs = new int[1] { user.Structure.GetValueOrDefault() };
                    
                }

                
                //var allowedStructuresToRead = repository.GetAllowedStructuresToRead(user, topStructuresIDs);
                return topStructuresIDs;
                //}

                //return new List<int>();
            }
            return new List<int>();
        }

        [HttpPost("newList")]
        public IEnumerable<StructureExpanded> GetData_new([FromBody] string get)
        {
            if (get.Contains("NaN"))
            {
                get = "0f0";
            }
            // Если перед id стоит -, то не показывает подчиненные подразделения. Если id положителен, то показывает
            string[] splittedGet = get.Split('f');

            int getFeatured = Int32.Parse(splittedGet[1]);
            if (getFeatured != 0 && !splittedGet[0].Contains(getFeatured.ToString()))
            {
                if (splittedGet[0].Length == 0)
                {
                    splittedGet[0] = getFeatured.ToString();
                }
                else
                {
                    splittedGet[0] = splittedGet[0] + "_" + getFeatured.ToString();
                }

            }
            if (splittedGet[0].Length == 0)
            {
                //if (getFeatured != 0)
                //{
                //    splittedGet[0] = getFeatured.ToString();
                //}
                //else
                //{
                //    return new List<StructureExpanded>();
                //}
                return new List<StructureExpanded>();
            }////ffffff

            List<int> idsList = splittedGet[0].Split('_').Select(n => Convert.ToInt32(n)).ToList();
            List<int> filteredIds = new List<int>();
            foreach (int id in idsList)
            {
                //int abs = Math.Abs(id);
                if (idsList.Contains(id) && idsList.Contains(-id) && !filteredIds.Contains(Math.Abs(id)))
                {
                    filteredIds.Add(Math.Abs(id));
                }
                else
                {
                    filteredIds.Add(id);
                }
            }
            int[] ids = filteredIds.Distinct().ToArray();

            // filters
            List<int> new_ids = new List<int>(),
                to_user = new List<int>();
            Dictionary<int, Structure> structures = repository.GetContext().Structure.ToDictionary(r => r.Id);
            foreach (int i in ids)
            {
                if (structures.ContainsKey(Math.Abs(i)))
                {
                    new_ids.Add(i);
                }
            }
            ids = new_ids.ToArray();
            //--------------
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                repository.RememberUserStructureTree(get, user);
                var allowedStructuresToRead = repository.GetAllowedStructuresToRead(user, ids, getFeatured, true);

                //var length = allowedStructuresToRead.Count(); // Debug info
                return allowedStructuresToRead;
            }
            List<StructureExpanded> empty = new List<StructureExpanded>();
            return empty;
        }

        [HttpGet("{get}")]
        public IEnumerable<StructureExpanded> GetData([FromRoute] string get)
        {
            if (get.Contains("NaN"))
            {
                get = "0f0";
            }
            // Если перед id стоит -, то не показывает подчиненные подразделения. Если id положителен, то показывает
            string[] splittedGet = get.Split('f');

            int getFeatured = Int32.Parse(splittedGet[1]);

            if (getFeatured != 0 && !splittedGet[0].Contains(getFeatured.ToString()))
            {
                if (splittedGet[0].Length == 0)
                {
                    splittedGet[0] = getFeatured.ToString();
                } else
                {
                    splittedGet[0] = splittedGet[0] + "_" + getFeatured.ToString();
                }
                    
            }
            if (splittedGet[0].Length == 0)
            {
                //if (getFeatured != 0)
                //{
                //    splittedGet[0] = getFeatured.ToString();
                //}
                //else
                //{
                //    return new List<StructureExpanded>();
                //}
                return new List<StructureExpanded>();
            }////ffffff

            List<int> idsList = splittedGet[0].Split('_').Select(n => Convert.ToInt32(n)).ToList();
            List<int> filteredIds = new List<int>();
            foreach (int id in idsList)
            {
                //int abs = Math.Abs(id);
                if (idsList.Contains(id) && idsList.Contains(-id) && !filteredIds.Contains(Math.Abs(id)))
                {
                    filteredIds.Add(Math.Abs(id));
                } else
                {
                    filteredIds.Add(id);
                }
            }
            int[] ids = filteredIds.Distinct().ToArray();

            // filters
            List<int> new_ids = new List<int>(),
                to_user = new List<int>();
            Dictionary<int, Structure> structures = repository.GetContext().Structure.ToDictionary(r => r.Id);
            foreach(int i in ids)
            {
                if (structures.ContainsKey(Math.Abs(i)))
                {
                    new_ids.Add(i);
                }
            }
            ids = new_ids.ToArray();
            //--------------
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, repository);
                repository.RememberUserStructureTree(get, user);
                var allowedStructuresToRead = repository.GetAllowedStructuresToRead(user, ids, getFeatured, true);
                
                //var length = allowedStructuresToRead.Count(); // Debug info
                return allowedStructuresToRead;
            }
            List<StructureExpanded> empty = new List<StructureExpanded>();
            return empty;
        }


        // GET: api/DetailedStructure/Solo5
        [HttpGet("Solo{id}")]
        public Structure GetStructureSolo([FromRoute] int id)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool isAllowedToReadStructure = repository.isAllowedToReadStructure(user, id);
                if (isAllowedToReadStructure)
                {
                    //Structure structure = repository.Structures.FirstOrDefault(s => s.Id == id);
                    Structure structure =  repository.GetActualStructureInfo(id, user.Date.GetValueOrDefault());
                    return structure;
                }

                return new Structure();
            }
            return new Structure();
        }

        /// <summary>
        /// Список подразделений, который могут выдавать награды.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/DetailedStructure/Rewards
        [HttpGet("Rewards")]
        public List<Structure> GetStructureReward()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool isAllowedToReadStructure = true;
                //bool isAllowedToReadStructure = repository.isAllowedToReadStructure(user, id);
                if (isAllowedToReadStructure)
                {
                    //Structure structure = repository.Structures.FirstOrDefault(s => s.Id == id);
                    List<Structure> structureRewards = new List<Structure>();
                    if (repository.StructuresLocal() == null)
                    {
                        repository.UpdateStructuresLocal();
                    }
                    foreach(Structure structure in repository.StructuresLocal().Values.Where(s => s.Printreward > 0))
                    {
                        Structure actualStructure = repository.GetActualStructureInfo(structure.Id, user.Date.GetValueOrDefault());
                        if (actualStructure != null)
                        {
                            structureRewards.Add(actualStructure);
                        }
                    }
                    foreach (Structure structure in structureRewards)
                    {
                        // Меняем динамично
                        structure.Name1 = repository.GetStructureNameDocument(structure, user.Date.GetValueOrDefault(), 1, null);
                        structure.Name2 = repository.GetStructureNameDocument(structure, user.Date.GetValueOrDefault(), 2, null);
                    }
                    //Structure structure = repository.GetActualStructureInfo(id, user.Date.GetValueOrDefault());
                    return structureRewards;
                }

                return new List<Structure>();
            }
            return new List<Structure>();
        }

        /// <summary>
        /// Список подразделений, который могут выдавать награды и которые можно выбирать.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/DetailedStructure/Rewardsallowed
        [HttpGet("Rewardsallowed")]
        public List<Structure> GetStructureRewardsallowed()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool isAllowedToReadStructure = true;
                //bool isAllowedToReadStructure = repository.isAllowedToReadStructure(user, id);
                if (isAllowedToReadStructure)
                {
                    //Structure structure = repository.Structures.FirstOrDefault(s => s.Id == id);
                    List<Structure> structureRewards = new List<Structure>();
                    if (repository.StructuresLocal() == null)
                    {
                        repository.UpdateStructuresLocal();
                    }
                    foreach (Structure structure in repository.StructuresLocal().Values.Where(s => s.Printreward > 0))
                    {
                        if (structure.Id != user.Structure.GetValueOrDefault() && structure.Main < 1)
                        {
                            continue;
                        }
                        Structure actualStructure = repository.GetActualStructureInfo(structure.Id, user.Date.GetValueOrDefault());
                        if (actualStructure != null)
                        {
                            structureRewards.Add(actualStructure);
                        }
                    }
                    foreach (Structure structure in structureRewards)
                    {
                        // Меняем динамично
                        structure.Name1 = repository.GetStructureNameDocument(structure, user.Date.GetValueOrDefault(), 1, null);
                        structure.Name2 = repository.GetStructureNameDocument(structure, user.Date.GetValueOrDefault(), 2, null);
                    }
                    //Structure structure = repository.GetActualStructureInfo(id, user.Date.GetValueOrDefault());
                    return structureRewards;
                }

                return new List<Structure>();
            }
            return new List<Structure>();
        }

        /// <summary>
        /// Возвращает список, состоящий из подразделения пользователя и главенствующих подразделений, включая ЦА
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/DetailedStructure/Elders
        [HttpGet("Elders")]
        public List<Structure> GetStructureElders()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool isAllowedToReadStructure = true;
                
                //bool isAllowedToReadStructure = repository.isAllowedToReadStructure(user, id);
                if (isAllowedToReadStructure)
                {
                    if (repository.StructuresLocal() == null)
                    {
                        repository.UpdateStructuresLocal();
                    }
                    Structure structure = repository.StructuresLocal().GetValue(user.Structure.GetValueOrDefault()); //  подразделение пользователя
                    Structure mainStructure = repository.StructuresLocal().Values.FirstOrDefault(s => s.Main > 0); // основное подразделение
                    if (structure != null && mainStructure != null)
                    {
                        if (structure == mainStructure || structure.Id == mainStructure.Id)
                        {
                            mainStructure = null;
                        }
                    }
                    Structure parentStructure = null;
                    if (structure != null)
                    {
                        parentStructure = repository.GetParent(structure);
                    }
                    //repository.GetParent(st)
                    List<Structure> structureElders = new List<Structure>();
                    if (structure != null)
                    {
                        Structure actualStructure = repository.GetActualStructureInfo(structure.Id, user.Date.GetValueOrDefault());
                        structureElders.Add(actualStructure);
                    }
                    if (mainStructure != null)
                    {
                        Structure actualMainStructure = repository.GetActualStructureInfo(mainStructure.Id, user.Date.GetValueOrDefault());
                        structureElders.Add(actualMainStructure);
                    }
                    if (parentStructure != null)
                    {
                        Structure actualParentStructure = repository.GetActualStructureInfo(parentStructure.Id, user.Date.GetValueOrDefault());
                        structureElders.Add(actualParentStructure);
                    }

                    return structureElders;
                }

                return new List<Structure>();
            }
            return new List<Structure>();
        }

        // GET: api/DetailedStructure/Up5
        [HttpGet("Up{id}")]
        public IActionResult UpStructure([FromRoute] int id)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool isAllowedToReadStructure = repository.isAllowedToReadStructure(user, id);
                if (isAllowedToReadStructure)
                {
                    writeChangesPriorety(user, id, "Up");
                    repository.UpStructure(id, user);
                    return new ObjectResult(Keys.SUCCESS_SHORT + ":Выполнено");
                }

                return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

        // GET: api/DetailedStructure/Down5
        [HttpGet("Down{id}")]
        public IActionResult DownStructure([FromRoute] int id)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool isAllowedToReadStructure = repository.isAllowedToReadStructure(user, id);
                if (isAllowedToReadStructure)
                {
                    writeChangesPriorety(user, id, "Down");
                    repository.DownStructure(id, user);
                    return new ObjectResult(Keys.SUCCESS_SHORT + ":Выполнено");
                }

                return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

        // GET: api/DetailedStructure/Prioritychange5;5
        [HttpGet("Prioritychange{code}")]
        public IActionResult PrioritychangeStructure([FromRoute] string code)
        {
            string[] values = code.Split(';');
            int id = Int32.Parse(values[0]);
            int value = Int32.Parse(values[1]);
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool isAllowedToReadStructure = repository.isAllowedToReadStructure(user, id);
                if (isAllowedToReadStructure)
                {
                    writeChangesPriorety(user, id, value.ToString());
                    repository.PrioritychangeStructure(id, value, user);
                    return new ObjectResult(Keys.SUCCESS_SHORT + ":Выполнено");
                }

                return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

        // GET: api/DetailedStructure/Pastes=2&5
        [HttpGet("Paste{str}")]
        public String GetPasteRequest([FromRoute] string str)
        {
            // First is id of COPY structure. Second is id of PASTE structure
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                string[] substring = str.Split('&');
                repository.Paste(Int32.Parse(substring[0].Split('=')[1]), Int32.Parse(substring[1]), user);

                /*StructureCustomEditor custom = new StructureCustomEditor(repository, user);
                custom.PasteStructure(Int32.Parse(substring[0].Split('=')[1]), Int32.Parse(substring[1]));*/

                return "";
            }
            return "";
        }



        // GET: api/DetailedStructure/Trees5&6&12&8
        [HttpGet("Trees{get}")]
        public List<StructureTree> GetStructureTrees([FromRoute] string get)
        {
            int[] ids = get.Split('&').Select(int.Parse).ToArray();
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                return repository.GetStructureTrees(ids, user.Date.GetValueOrDefault());
            }
            return new List<StructureTree>();
        }

        // GET: api/DetailedStructure/Info5
        [HttpGet("Info{id}")]
        public List<StructureInfo> GetStructureInfo([FromRoute] int id)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool isAllowedToReadStructure = repository.isAllowedToReadStructure(user, id);
                if (isAllowedToReadStructure)
                {
                    //return repository.GetStructureInfo(id, user, true);
                    return repository.GetStructureInfos(id, user, true);
                }

                return new List<StructureInfo>();
            }
            return new List<StructureInfo>();
        }

        // GET: api/DetailedStructure/Alldocument
        [HttpGet("Alldocument")]
        public List<string> GetStructuresAlldocument([FromRoute] int id)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                bool hasAccess = IdentityService.CanReadCommonData(user);
                if (hasAccess)
                {
                    //Structure structure = repository.Structures.FirstOrDefault(s => s.Id == id);
                    List<string> structures = repository.GetStructuresAllDocument(user);
                    return structures;
                }
                return new List<string>();
            }
            return new List<string>();
        }

        [HttpPost()]
        public IActionResult ManageStructure([FromBody]StructureManagement structureManagement)
        {
            /**
             * Check access.
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
            /**
             * Means, we add new structure.
             */
            if (structureManagement.Type.Equals(Keys.STRUCTURE_MANAGEMENT_ADDNEWSTRUCTURE))
            {
                repository.AddStructure(structureManagement, user);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Подразделение создано");
            }
            else if (structureManagement.Type.Equals(Keys.STRUCTURE_MANAGEMENT_REMOVESTRUCTURE))
            {
                repository.RemoveStructure(structureManagement, user);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Подразделение удалено");
            }
            else if (structureManagement.Type.Equals(Keys.STRUCTURE_MANAGEMENT_REMOVESTRUCTUREDECREE))
            {
                repository.RemoveStructureDecree(structureManagement, user);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Подразделение убрано из проекта приказа");
            }
            else if (structureManagement.Type.Equals(Keys.STRUCTURE_MANAGEMENT_RENAMESTRUCTURE) || structureManagement.Type.Equals(Keys.STRUCTURE_MANAGEMENT_RENAMESTRUCTURENODECREE))
            {
                repository.RenameStructure(structureManagement, user);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Подразделение переименовано");
            }
            else if (structureManagement.Type.Equals(Keys.STRUCTURE_MANAGEMENT_RENAMESTRUCTUREDECREE))
            {
                repository.RenameStructureDecree(structureManagement, user);
                return new ObjectResult(Keys.SUCCESS_SHORT + ":Подразделение отредактировано");
            }
            return new ObjectResult(Keys.ERROR_SHORT + ":Произошла какая-то оказия");
        }

        private void writeChangesPriorety(User user, int structure_id, string new_priorety)
        {
            string path = @"c:\organizational_staff\tree\priorety\";
            string file = @"changed_";
            DateTime localDate = DateTime.Now;
            file += localDate.ToString("MM_yyyy") + ".txt";
            Structure current = repository.GetOriginalStructure(structure_id);
            if (!System.IO.Directory.Exists(path))
            {
                DirectoryInfo di = System.IO.Directory.CreateDirectory(path);
            }
            if (!System.IO.File.Exists(path + file))
            {
                // Create a file to write to.
                using (StreamWriter sw = System.IO.File.CreateText(path + file))
                {
                    sw.WriteLine("Date; Name; SurName; structure id; parent structure; new priorety");
                }
            }
            using (StreamWriter writer = System.IO.File.AppendText(path + file))
            {
                writer.WriteLine("{0};{1};{2};{3};{4};{5}",
                                localDate.ToString(),
                                user.Firstname,
                                user.Surname,
                                structure_id,
                                current.Id,
                                new_priorety);
            }
        }

    }
}