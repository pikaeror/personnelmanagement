using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class UserManager : User
    {

        public string StructureString { get; set; }
        public string StructureTreeString { get; set; }
        public string PositionString { get; set; }

        public UserManager(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Admin = user.Admin;
            Firstname = user.Firstname;
            Surname = user.Surname;
            Patronymic = user.Patronymic;
            Structure = user.Structure;
            Positiontype = user.Positiontype;

            StructureString = "";
            StructureTreeString = "";
        }

        public UserManager()
        {

        }

        public static List<UserManager> UsersToUserManagers(Repository repository, User userBase, IEnumerable<User> users)
        {
            List<UserManager> userManagers = new List<UserManager>();
            foreach (User user in users)
            {
                userManagers.Add(repository.GetUserManager(userBase, user));
            }
            return userManagers;
        }

    }
}
