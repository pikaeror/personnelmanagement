using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonnelManagement.Models;
using PersonnelManagement.USERS;
using PersonnelManagement.Services;

using Microsoft.EntityFrameworkCore;

namespace PersonnelManagement.Controllers
{
    [Route("api/Structure_Tree_Controller")]
    [Produces("application/json")]
    public class Structure_Tree_Controller : Controller
    {
        private orgContext orgContext { get; set; }
        private userContext userContext { get; set; }
        private Repository repository { get; set; }

        public Structure_Tree_Controller(Repository repository)
        {
            this.repository = repository; 
            orgContext = repository.GetContext();
            userContext = repository.GetContextUser();
        }
        
        [HttpPost("CurrentTree")]
        public Models.NewStructure.StructureTree currenttree()
        {
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!start!!!!!!!!!!!!!!!!!!!!!!!!!!!!!" + DateTime.Now.ToString());
            USERS.User user = IdentityService.GetUserBySessionID(Request.Cookies[Keys.COOKIES_SESSION], repository);
            bool hasAccess = IdentityService.CanReadStructure(Request.Cookies[Keys.COOKIES_SESSION], repository, user.Structure.GetValueOrDefault());
            Models.NewStructure.StructureTree structureTree = new Models.NewStructure.StructureTree();
            structureTree.GenerateCurrentTree(user, orgContext, userContext);
            /*List<Models.NewStructure.StructureTree> tree = new List<Models.NewStructure.StructureTree>();
            List<Structure> all_structures = orgContext.Structure.ToList();
            List<Decree> all_decrees = orgContext.Decree.ToList();
            List<Structuredecreeoperation> all_operations = orgContext.Structuredecreeoperation.ToList();
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!DATA Base Done!!!!!!!!!!!!!!!!!!!!!!!!!!!!!" + DateTime.Now.ToString());
            foreach (Structure str in all_structures.Where(r => r.Featured == 1))
            {
                Models.NewStructure.StructureTree time = new Models.NewStructure.StructureTree(str);
                time.GenerateTreeById(str.Id, user, all_structures, all_decrees, all_operations);
                tree.Add(time);
            }*/
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!DONE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!" + DateTime.Now.ToString());
            return structureTree;
        }
    }
}
