using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Models;
using PersonnelManagement.Services;
using System.Collections.Generic;

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
        public IEnumerable<Mailexplorer> PrintOrderhistoryByPositions()
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
