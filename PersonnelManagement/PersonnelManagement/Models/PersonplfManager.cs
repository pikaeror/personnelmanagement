using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class PersonplfManager
    {

        public int Id { get; set; }
        public int Person { get; set; }
        public DateTime Date { get; set; }
        public byte[] Document { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Person64 { get; set; }
        public string Person64header { get; set; }

        public PersonplfManager()
        {

        }

        public PersonplfManager(Personpfl personpfl)
        {
            Id = personpfl.Id;
            Person = personpfl.Person;
            Date = personpfl.Date;
            //Photo = personphoto.Photo;
            Description = personpfl.Description;
            Name = personpfl.Name;
            Person64 = Convert.ToBase64String(personpfl.Document);
            Person64header = personpfl.Person64header;
        }

        //public Personplf ToPersonpfl()
        //{
        //    Personphoto personphoto = new Personphoto();
        //    personphoto.Id = Id;
        //    personphoto.Person = Person;
        //    personphoto.Date = Date;
        //    personphoto.Photo = Photo;
        //    personphoto.Description = Description;
        //    personphoto.Name = Name;
        //    try
        //    {
        //        if (Photo64 != null && Photo64.Length > 0)
        //        {
        //            string[] splitted = Photo64.Split(',');
        //            personphoto.Person64header = splitted[0];
        //            personphoto.Photo = Convert.FromBase64String(splitted[1]);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Exception exception = ex;
        //        return personphoto;
        //    }

        //    return personphoto;
        //}

    }
}
