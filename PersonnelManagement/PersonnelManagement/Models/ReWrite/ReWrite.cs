using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonnelManagement.USERS;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace PersonnelManagement.Models
{
    public class ReWrite
    {
        private orgContext orgContext { get; set; }
        public USERS.User user { get; set; }

        public ReWrite(ref Repository repository)
        {
            orgContext = repository.GetContext();
        }

        public void Worker()
        {
            if (user.Id != 1)
                return;
            Thread tr_s = new Thread(structureSubject);
            Thread tr_p = new Thread(positionSubject);
            tr_s.Priority = ThreadPriority.Highest;
            tr_p.Priority = ThreadPriority.Highest;

            tr_s.Start();
            tr_s.Join();

            tr_p.Start();
            tr_p.Join();
        }

        private void structureSubject()
        {
            List<Structure> structures = orgContext.Structure.Where(r => r.Subject1 != 0 && r.Subjectindex == 0).ToList();
            List<Elementsubject> elementsubject = new List<Elementsubject>();
            for(int i = 0; i < structures.Count; i++)
            {
                elementsubject.Add(new Elementsubject(structures.ElementAt(i)));
            }
            orgContext.Elementsubject.AddRange(elementsubject);
            orgContext.SaveChanges();
            for (int i = 0; i < structures.Count; i++)
            {
                structures.ElementAt(i).Subjectindex = elementsubject.ElementAt(i).Id;
            }
            orgContext.Structure.UpdateRange(structures);
            orgContext.SaveChanges();
        }

        private void positionSubject()
        {
            List<Position> positions = orgContext.Position.Where(r => r.Subjectindex == 0 && r.Subject1 != 0).ToList();
            List<Elementsubject> elementsubject = new List<Elementsubject>();
            for (int i = 0; i < positions.Count; i++)
            {
                elementsubject.Add(new Elementsubject(positions.ElementAt(i)));
            }
            orgContext.Elementsubject.AddRange(elementsubject);
            orgContext.SaveChanges();
            for (int i = 0; i < positions.Count; i++)
            {
                positions.ElementAt(i).Subjectindex = elementsubject.ElementAt(i).Id;
            }
            orgContext.Position.UpdateRange(positions);
            orgContext.SaveChanges();
        }
    }
}
