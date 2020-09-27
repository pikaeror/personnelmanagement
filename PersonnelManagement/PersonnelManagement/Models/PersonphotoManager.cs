using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class PersonphotoManager
    {
        public int Id { get; set; }
        public int Person { get; set; }
        public DateTime Date { get; set; }
        public byte[] Photo { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Photo64 { get; set; }
        public string Photo64header { get; set; }

        public PersonphotoManager()
        {

        }

        public PersonphotoManager(Personphoto personphoto)
        {
            Id = personphoto.Id;
            Person = personphoto.Person;
            Date = personphoto.Date;
            //Photo = personphoto.Photo;
            Description = personphoto.Description;
            Name = personphoto.Name;
            Photo64 = Convert.ToBase64String(personphoto.Photo);
            Photo64header = personphoto.Person64header;
        }

        public Personphoto ToPersonphoto()
        {
            Personphoto personphoto = new Personphoto();
            personphoto.Id = Id;
            personphoto.Person = Person;
            personphoto.Date = Date;
            personphoto.Photo = Photo;
            personphoto.Description = Description;
            personphoto.Name = Name;
            try
            {
                if (Photo64 != null && Photo64.Length > 0)
                {
                    string[] splitted = Photo64.Split(',');
                    personphoto.Person64header = splitted[0];
                    personphoto.Photo = Convert.FromBase64String(splitted[1]);
                }
            } catch (Exception ex)
            {
                Exception exception = ex;
                return personphoto;
            }
            
            return personphoto;
        }
    }
}
