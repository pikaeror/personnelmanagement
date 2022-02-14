using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace PersonnelManagement.Models
{
    public class DecreeWorker
    {
        public DecreeWorker(ref Repository repo)
        {
            m_repository = repo;
            decrees = m_repository.GetContext().Decree;
        }

        public Repository m_repository { get; private set; }
        public IEnumerable<Decree> decrees { get; set; }
        public List<Decree> FinderByFinder(DecreeFinder decreeFinder)
        {
            List<Decree> output = decrees.Where(r => this.byFinder(r, ref decreeFinder)).ToList();
            this.sorted(ref output);
            return output;
        }

        private void sorted(ref List<Decree> decrees)
        {
            decrees.Sort((x, y) => y.Dateactive.GetValueOrDefault().Ticks.CompareTo(x.Dateactive.GetValueOrDefault().Ticks));
            return;
        }

        private bool byFinder(Decree decree, ref DecreeFinder decreeFinder)
        {
            if (decree.Signed == 0)
                return false;
            else if (!decree.Name.Contains(decreeFinder.name))
                return false;
            else if (!decree.Number.Contains(decreeFinder.number))
                return false;
            else if (!decree.Nickname.Contains(decreeFinder.nickname))
                return false;
            else if (!decreeFinder.dates.Includes(decree.Dateactive))
                return false;
            else if (!decreeFinder.date_started.Includes(decree.Datesigned))
                return false;
            return true;
        }


        public void reWriteDecreesNull(USERS.User user, bool doing = false)
        {
            if (user.Id != 1 && !doing)
                return;
            orgContext context = m_repository.GetContext();
            IQueryable<Decree> decrees = context.Decree;
            foreach (Decree d in decrees)
            {
                if (d.Name == null)
                    d.Name = "";
                if (d.Nickname == null)
                    d.Nickname = "";
                if (d.Number == null)
                    d.Number = "";
            }

            context.Decree.UpdateRange(decrees);
            context.SaveChanges();
            m_repository.UpdateDecreesLocal();

            DecreeOperationWorker decreeWorker = new DecreeOperationWorker(m_repository);
            decreeWorker.user = user;
            DecreeOperationWorker decreeWorker_s = new DecreeOperationWorker(m_repository);
            decreeWorker_s.user = user;
            Thread tr_p = new Thread(decreeWorker.partial_decreeoperations_pos);
            Thread tr_s = new Thread(decreeWorker_s.partial_decreeoperations_str);
            tr_p.Priority = ThreadPriority.Highest;
            tr_s.Priority = ThreadPriority.Highest;
            tr_p.Start();
            System.Threading.Thread.Sleep(10000);
            tr_s.Start();
            tr_p.Join();
            tr_s.Join();
        }
    }
}
