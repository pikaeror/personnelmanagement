using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class SubjectWorker
    {
        Repository repository { get; set; }
        public SubjectWorker(Repository repository)
        {
            this.repository = repository;
        }

        public void SaveSubject(Elementsubject elementsubject)
        {
            //repository.GetContext().Elementsubject.Add(new Elementsubject(elementsubject));
            repository.SaveChanges();
        }

        public Elementsubject GetSubjectElement(int elementSubject)
        {
            return repository.GetContext().Elementsubject.FirstOrDefault(el => el.Id == elementSubject);
        }
    }
}
