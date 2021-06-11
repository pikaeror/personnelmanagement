using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Models;
using PersonnelManagement.Services;
using System.Collections.Generic;
using System.Linq;
using System;

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

        [HttpGet("rand")]
        public IEnumerable<Structure> rand()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                // return repository.GetDecree 
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                Random rand = new Random();
                int count = rand.Next(2, 6);
                int full_structures = repository.StructuresLocal().Count;
                List<Structure> output = new List<Structure>(){ };
                for(int k = 0; k < count; k++)
                {
                    int y = rand.Next(full_structures);
                    while( output.Find(r => r.Id == y) != null )
                    {
                        y = rand.Next(full_structures);
                    }
                    output.Add(repository.StructuresLocal().Values.First(r => r.Id == y));
                }
                return output;
            }
            return null;
        }

        [HttpPost("Update")]
        public Mailexplorer Update([FromBody] Mailexplorer body)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                // return repository.GetDecree 
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                var time = repository.GetContext().Mailexplorer.SingleOrDefault(r => r.Id == body.Id);
                time.FolderCreator = body.FolderCreator;
                time.FolderOwner = body.FolderOwner;
                //repository.GetContext().Mailexplorer.U
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

        [HttpPost("Send/{id}")]
        public void Send([FromRoute] int id, [FromBody] IEnumerable<Persondecree> body)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                // return repository.GetDecree 
                user = IdentityService.GetUserBySessionID(sessionid, repository);

                body.ToList().ForEach(i => {
                    var decree = repository.GetContext().Persondecree.First(r => r.Id == i.Id);
                    var time = repository.GetContext().Mailexplorer.First(r => r.Id == i.Mailexplorerid);
                    if (decree.Creator == user.Id)
                        time.FolderCreator = 3;
                    time.FolderOwner = 2;
                    if (time.AccessForReading.Split("_").ToList().IndexOf(id.ToString()) == -1)
                        time.AccessForReading += "_" + id.ToString();
                    List<string> users = time.LastCountOwner != null ? time.LastCountOwner.Split("|").ToList() : new List<string>() { "" };
                    int index = users.IndexOf(id.ToString());
                    if (index != -1)
                    {
                        users = time.LastDateOpen.Split("|").ToList();
                        users[index] = " ";
                        time.LastDateOpen = String.Join("|", users);
                        users = time.DetaSend.Split("|").ToList();
                        users[index] = user.Date.GetValueOrDefault().ToString("dd:MM:yyyy");
                        time.DetaSend = String.Join("|", users);

                    }
                    else
                    {
                        time.LastCountOwner += id.ToString() + "|";
                        time.LastDateOpen += " |";
                        time.DetaSend += user.Date.GetValueOrDefault().ToString("dd:MM:yyyy") + "|";
                    }
                    decree.Owner = id;
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

        [HttpGet("unopen")]
        public IEnumerable<PersondecreeManagement> get_unopen_counts()
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                // return repository.GetDecree 
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                MailWorker worker = new MailWorker(user, repository);
                return worker.getUnopenDecree();
            }
            return null;
        }

        [HttpPost("opendecree/{id}")]
        public void set_open_decree([FromRoute] int id)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                MailWorker worker = new MailWorker(user, repository);
                worker.open_unreed_decree(id);
            }
        }

        [HttpGet("archive/{id}")]
        public IEnumerable<PersondecreeManagement> get_archive_by_decree([FromRoute] int id)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            List<PersondecreeManagement> output = new List<PersondecreeManagement>();
            User user = null;
            if (IdentityService.IsLogined(sessionid, repository))
            {
                user = IdentityService.GetUserBySessionID(sessionid, repository);
                MailWorker worker = new MailWorker(user, repository);
                return worker.get_archive_by_decree(id);
            }
            return output;
        }
    }
}
