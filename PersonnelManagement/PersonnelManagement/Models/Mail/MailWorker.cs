using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class MailWorker
    {
        private User m_user { get; set; }
        private Repository m_repository { get; set; }
        private pmContext m_context { get; set; }

        private Dictionary<int, Mailexplorer> exp { get; set; }
        private Dictionary<int, Persondecree> decrees { get; set; }

        public MailWorker(User user, Repository repository)
        {
            m_user = user;
            m_repository = repository;

            m_context = m_repository.GetContext();
            exp = m_context.Mailexplorer.ToDictionary(r => r.Id);
            decrees = m_context.Persondecree.ToDictionary(r => r.Id);
        }

        public List<PersondecreeManagement> getUnopenDecree()
        {
            List<PersondecreeManagement> output = new List<PersondecreeManagement>() { };

            foreach (Persondecree decree in decrees.Values)
            {
                if (parseMailexplorer(decree, exp))
                {
                    output.Add(new PersondecreeManagement(decree));
                }
            }

            return output;
        }

        public void open_unreed_decree(int id)
        {
            Persondecree decree_actual = decrees.GetValue(id);
            Mailexplorer explorer_actual = m_context.Mailexplorer.First(r => r.Id == decree_actual.Mailexplorerid);
            if (explorer_actual.LastCountOwner == null)
                return;
            List<string> ides = explorer_actual.LastCountOwner.Split('|').ToList(),
                dates = explorer_actual.LastDateOpen.Split('|').ToList();
            string id_string = m_user.Id.ToString();
            int index = ides.IndexOf(id_string);
            string date = dates.ElementAt(index);
            if (date == " ")
                dates[index] = m_user.Date.GetValueOrDefault().ToString("dd:MM:yyyy");
            explorer_actual.LastDateOpen = String.Join("|", dates);
            m_context.Mailexplorer.Update(explorer_actual);
            m_context.SaveChanges();
        }

        private bool parseMailexplorer(Persondecree persondecree, Dictionary<int, Mailexplorer> exp)
        {
            Mailexplorer explorer = exp[persondecree.Mailexplorerid];
            if (explorer.LastCountOwner == null || explorer.LastDateOpen == null)
                return false;
            List<string> ides = explorer.LastCountOwner.Split('|').ToList(),
                dates = explorer.LastDateOpen.Split('|').ToList();
            string id_string = m_user.Id.ToString();
            int index = ides.IndexOf(id_string);
            if (index == -1)
                return false;
            string date = dates.ElementAt(index);
            if (date == " ")
                return true;
            List<string> indexis = date.Split(':').ToList();
            if (new DateTime(int.Parse(indexis.ElementAt(2)), int.Parse(indexis.ElementAt(1)), int.Parse(indexis.ElementAt(0))).Ticks <= m_user.Date.GetValueOrDefault().Ticks)
                return false;
            return true;
        }
    }
}
