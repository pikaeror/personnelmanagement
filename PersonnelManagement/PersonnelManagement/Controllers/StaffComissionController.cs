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
    [Route("api/StaffComission")]
    public class StaffComissionController : Controller
    {
        private Repository m_repository;
        public StaffComissionController(Repository repository)
        {
            m_repository = repository;

        }

        private bool ChechAccseseble(HttpRequest Request)
        {
            string sessionid = Request.Cookies[Keys.COOKIES_SESSION];
            if (IdentityService.IsLogined(sessionid, m_repository))
            {
                User user = IdentityService.GetUserBySessionID(sessionid, m_repository);
                bool hasAccess = IdentityService.CanWriteAdminData(user);
                if (hasAccess)
                    return true;
            }
            return false;
        }

        [HttpGet()]
        public IEnumerable<Staffcomission> GetFullComission()
        {
            List<Staffcomission> output = new List<Staffcomission>() { };
            if (!ChechAccseseble(Request))
                return output;

            output = m_repository.GetContext().Staffcomission.ToList();
            output.Sort((a, b) => a.Rank.CompareTo(b.Rank));
            return output;
        }

        [HttpPost()]
        public IActionResult UpdateStaffComission([FromBody] List<Staffcomission> staffcomissions)
        {
            if(!ChechAccseseble(Request))
                return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");

            //somesing doing
            return new ObjectResult(Keys.SUCCESS_SHORT + ":Штатная комиссия успешно изменена");
        }

        [HttpPost("Update")]
        public IActionResult UpdateStaffComission([FromBody] Staffcomission person)
        {
            if (!ChechAccseseble(Request))
                return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");
            pmContext m_Context = m_repository.GetContext();
            List<Staffcomission> all_staff = m_Context.Staffcomission.ToList();
            all_staff.Sort((a, b) => a.Rank.CompareTo(b.Rank));
            Staffcomission new_person = all_staff.First<Staffcomission>(r => r.Id == person.Id);
            new_person.Fio = person.Fio;
            m_Context.Staffcomission.Update(new_person);
            m_Context.SaveChanges();
            //somesing doing
            return new ObjectResult(Keys.SUCCESS_SHORT + ":Штатная комиссия успешно изменена");
        }

        [HttpPost("Add")]
        public IActionResult AddPersonToStaffComission([FromBody] Staffcomission person)
        {
            if (!ChechAccseseble(Request))
                return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");
            pmContext m_Context = m_repository.GetContext();
            List<Staffcomission> all_staff = m_Context.Staffcomission.ToList();
            all_staff.Sort((a, b) => a.Rank.CompareTo(b.Rank));
            person.Rank = all_staff.Last().Rank + 1;
            m_Context.Staffcomission.Add(person);
            m_Context.SaveChanges();
            //somesing doing
            return new ObjectResult(Keys.SUCCESS_SHORT + ":Успешно добавлен член штатной комиссии");
        }

        [HttpPost("UP")]
        public IActionResult UpPersonToStaffComission([FromBody] Staffcomission person)
        {
            if (!ChechAccseseble(Request))
                return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");
            pmContext m_Context = m_repository.GetContext();
            List<Staffcomission> all_staff = m_Context.Staffcomission.ToList();
            all_staff.Sort((a, b) => a.Rank.CompareTo(b.Rank));
            int actual_index = all_staff.FindIndex(r => r.Id == person.Id),
                actual_rank = person.Rank;
            if(actual_index == 0)
                return new ObjectResult(Keys.ERROR_SHORT + ":Первый в списке");
            Staffcomission upPerson = all_staff.ElementAt<Staffcomission>(actual_index - 1);
            person = all_staff.ElementAt<Staffcomission>(actual_index);
            person.Rank = upPerson.Rank;
            upPerson.Rank = actual_rank;
            m_Context.Staffcomission.Update(person);
            m_Context.Staffcomission.Update(upPerson);
            m_Context.SaveChanges();
            //somesing doing
            return new ObjectResult(Keys.SUCCESS_SHORT + ":Успешно изменен порядок штатной комиссии");
        }

        [HttpPost("DOWN")]
        public IActionResult DownPersonToStaffComission([FromBody] Staffcomission person)
        {
            if (!ChechAccseseble(Request))
                return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");
            pmContext m_Context = m_repository.GetContext();
            List<Staffcomission> all_staff = m_Context.Staffcomission.ToList();
            all_staff.Sort((a, b) => a.Rank.CompareTo(b.Rank));
            int actual_index = all_staff.FindIndex(r => r.Id == person.Id),
                actual_rank = person.Rank;
            if (actual_index == all_staff.Count - 1)
                return new ObjectResult(Keys.ERROR_SHORT + "Последний в списке");
            Staffcomission downPerson = all_staff.ElementAt<Staffcomission>(actual_index + 1);
            person = all_staff.ElementAt<Staffcomission>(actual_index);
            person.Rank = downPerson.Rank;
            downPerson.Rank = actual_rank;
            m_Context.Staffcomission.Update(person);
            m_Context.Staffcomission.Update(downPerson);
            m_Context.SaveChanges();
            //somesing doing
            return new ObjectResult(Keys.SUCCESS_SHORT + ":Успешно изменен порядок штатной комиссии");
        }

        [HttpPost("Remove")]
        public IActionResult RemovePersonToStaffComission([FromBody] Staffcomission person)
        {
            if (!ChechAccseseble(Request))
                return new ObjectResult(Keys.ERROR_SHORT + ":Отказано в доступе");
            pmContext m_Context = m_repository.GetContext();
            List<Staffcomission> all_staff = m_Context.Staffcomission.ToList(),
                new_staff = new List<Staffcomission>();
            all_staff.Sort((a, b) => a.Rank.CompareTo(b.Rank));
            Staffcomission dell = new Staffcomission();
            bool flag = false;
            for(int i = 0; i < all_staff.Count; i++)
            {
                if(all_staff.ElementAt<Staffcomission>(i).Id == person.Id)
                {
                    dell = all_staff.ElementAt<Staffcomission>(i);
                    flag = true;
                    continue;
                }
                if (flag)
                {
                    new_staff.Add(all_staff.ElementAt<Staffcomission>(i));
                    new_staff.Last().Rank -= 1;
                } else
                {
                    new_staff.Add(all_staff.ElementAt<Staffcomission>(i));
                }
            }
            m_Context.Staffcomission.Remove(dell);
            m_Context.Staffcomission.UpdateRange(new_staff);
            m_Context.SaveChanges();
            //somesing doing
            return new ObjectResult(Keys.SUCCESS_SHORT + ":Успешно изменен порядок штатной комиссии");
        }
    }
}
