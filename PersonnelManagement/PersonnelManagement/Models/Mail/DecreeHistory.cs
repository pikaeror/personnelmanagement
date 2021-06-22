using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class DecreeHistory
    {
        private Repository m_repository { get; set; }
        public User m_user { get; set; }
        private Dictionary<int, string> action = new Dictionary<int, string>();
        public DecreeHistory(Repository repo, User user)
        {
            m_repository = repo;
            m_user = user;

            action.Add(Keys.PERSONDECREE_MANAGEMENT_NEWDECREE, "создан");
            action.Add(Keys.PERSONDECREE_MANAGEMENT_DECLINEDECREE, "отменен");
            action.Add(Keys.PERSONDECREE_MANAGEMENT_ACCEPTDECREE, "подписан");
            action.Add(Keys.PERSONDECREE_MANAGEMENT_UPDATEDECREEINFO, "обновлен");
            action.Add(Keys.PERSONDECREE_MANAGEMENT_PRINTDECREE, "распечатан");
            action.Add(Keys.PERSONDECREE_MANAGEMENT_CHANGEOWNER, "перенаправлен");
            action.Add(Keys.PERSONDECREE_MANAGEMENT_CHANGEOWNER + 10, "открыт");
            action.Add(Keys.PERSONDECREE_MANAGEMENT_CHANGEOWNER + 11, "создан объединением");
            action.Add(1 + 20, "добавлен блок");
            action.Add(2 + 20, "удален блок");
            action.Add(1 + 30, "добавлена новая запись в приказ");
            action.Add(2 + 30, "Дудалена запись из приказа");
            action.Add(3 + 30, "изменена запись приказа");
        }

        public void setAction(Persondecree decree, int flag)
        {
            Persondecreeuserhistory element = new Persondecreeuserhistory()
            {
                Decree = decree.Id,
                User = m_user.Id,
                Status = action.ContainsKey(flag) ? action[flag] : "стороннее действие",
                Date = DateTime.Now
            };
            pmContext context = m_repository.GetContext();
            context.Persondecreeuserhistory.Add(element);
            context.SaveChanges();
        }

        public void setAction(int id, int flag)
        {
            Persondecreeuserhistory element = new Persondecreeuserhistory()
            {
                Decree = id,
                User = m_user.Id,
                Status = action.ContainsKey(flag) ? action[flag] : "стороннее действие",
                Date = DateTime.Now
            };
            pmContext context = m_repository.GetContext();
            context.Persondecreeuserhistory.Add(element);
            context.SaveChanges();
        }

        public List<PersonDecreeHistory> getHistory(Persondecree decree)
        {
            List<PersonDecreeHistory> output = new List<PersonDecreeHistory>();
            pmContext context = m_repository.GetContext();
            Dictionary<int, User> user = context.User.ToDictionary(r => r.Id);
            PersonDecreeHistory time_value;
            User time_user;
            foreach(Persondecreeuserhistory iterator in context.Persondecreeuserhistory.Where(r => r.Decree == decree.Id))
            {
                time_user = user[iterator.User];
                time_value = new PersonDecreeHistory()
                {
                    name = time_user.Firstname + " " + time_user.Name + " " + time_user.Surname,
                    date = iterator.Date.GetValueOrDefault(),
                    action = iterator.Status
                };
                output.Add(time_value);
            }
            output.Sort((x, y) => x.date.Ticks.CompareTo(y.date.Ticks));
            return output;
        }

        public List<PersonDecreeHistory> getHistory(int decree)
        {
            return this.getHistory(m_repository.PersondecreesLocal()[decree]);
        }
    }
}
