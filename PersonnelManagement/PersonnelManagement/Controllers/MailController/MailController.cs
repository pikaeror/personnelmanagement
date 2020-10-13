using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Models;
using PersonnelManagement.Services;
using System.Collections.Generic;
using System.Linq;

namespace PersonnelManagement.Controllers
{
    [Produces("application/json")]
    [Route("api/MailController")]
    public class MailController : Controller
    {
        private Repository repository;

        public MailController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet("Full")]
        public IEnumerable<Mailexplorer> Full()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                // return repository.GetDecree 
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                repository.UpdateMailexplorersLocal();
                return repository.MailexplorersLocal().Values;
            }
            return null;
        }

        [HttpGet("AddNew")]
        public Mailexplorer AddNew()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                // return repository.GetDecree 
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                Mailexplorer time = new Mailexplorer();
                time.FolderCreator = 1;
                time.FolderOwner = 0;
                time.AccessForReading = user.Id.ToString();
                repository.GetContext().Mailexplorer.Add(time);
                repository.GetContext().SaveChanges();
                return time;
            }
            return null;
        }

        [HttpPost("ChangeFolder/{id}")]
        public void ChangeFolder([FromRoute] uint id, [FromBody] IEnumerable<Mailexplorer> body)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                // return repository.GetDecree 
                user = IdentityService.GetUserBySessionID(sessionid, repository);

                body.ToList().ForEach(i => {
                    Persondecree decree = repository.PersondecreesLocal().Values.Where(r => r.Mailexplorerid == i.Id).FirstOrDefault();
                    var time = repository.GetContext().Mailexplorer.First(r => r.Id == i.Id);
                    if (decree.Creator == user.Id)
                        time.FolderCreator = id;
                    else if (decree.Owner == user.Id)
                        time.FolderOwner = id;
                    repository.GetContext().SaveChanges();
                });
            }
        }

        [HttpPost("DefaultWorker")]
        public void DefaultWorker()
        {
            var folders = repository.MailfoldersLocal().Values;
            var t = repository.PersondecreesLocal().Values;
            foreach(Persondecree i in t)
            {
                repository.UpdateMailexplorersLocal();
                var mailexplorers = repository.MailexplorersLocal().Values;
                Mailexplorer time = new Mailexplorer();
                if(i.Creator == i.Owner && i.Signed == 0)
                {
                    time.FolderCreator = 6;
                    time.FolderOwner = 0;
                    time.AccessForReading = i.Creator.ToString();
                } 
                else if(i.Creator == i.Owner && i.Signed == 1)
                {
                    time.FolderCreator = 7;
                    time.FolderOwner = 0;
                    time.AccessForReading = i.Creator.ToString();
                }
                else if(i.Creator != i.Owner && i.Signed == 0)
                {
                    time.FolderCreator = 3;
                    time.FolderOwner = 2;
                    time.AccessForReading = i.Creator.ToString() + "_" + i.Owner.ToString();
                }
                else if (i.Creator != i.Owner && i.Signed == 1)
                {
                    time.FolderCreator = 3;
                    time.FolderOwner = 2;
                    time.AccessForReading = i.Creator.ToString() + "_" + i.Owner.ToString();
                }

                repository.GetContext().Mailexplorer.Add(time);
                repository.GetContext().SaveChanges();
                var toupdate = repository.GetContext().Persondecree.Find(i.Id);
                toupdate.Mailexplorerid = time.Id;
                repository.GetContext().Persondecree.Update(toupdate);
                repository.GetContext().SaveChanges();
                //repository.GetContext().Mailexplorer.Update(time);
                //repository.GetContext().Persondecree.Find(i).Mailexplorerid = mailexplorers;

                //repository.GetContext().Persondecree.
            }
        }
    }
}
