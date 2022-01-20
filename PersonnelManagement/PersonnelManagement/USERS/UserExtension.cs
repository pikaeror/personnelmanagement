using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.USERS
{
    public partial class User
    {

        [NotMapped]
        public Rights Rights { get; set; }
        [NotMapped]
        public int Logined { get; set; } = 0;

        /// <summary>
        /// Возвращает специальную версию для фронта, где не хранится пароль и соль, зато хранится объект Rights
        /// </summary>
        /// <returns></returns>
        public User GetUserModel(userContext repository)
        {
            User userModel = new User();
            // Ставим, что залогинен
            userModel.Logined = 1;
            userModel.Id = Id;
            userModel.Name = Name;
            // Временно передаем также и старый правовой режим. Режим админа, чтение/запись оргштата и т.п.
            userModel.Admin = Admin;
            userModel.Structure = Structure;
            userModel.Structureeditor = Structureeditor;
            userModel.Masterpersonneleditor = Masterpersonneleditor;
            userModel.Personneleditor = Personneleditor;
            userModel.Decree = Decree;
            userModel.Positioncompact = Positioncompact;
            userModel.Date = Date;
            userModel.Sidebardisplay = Sidebardisplay;
            userModel.Currentstructuretree = Currentstructuretree;
            userModel.Structureread = Structureread;
            userModel.Personnelread = Personnelread;
            userModel.Mode = Mode;
            userModel.Firstname = Firstname;
            userModel.Surname = Surname;
            userModel.Patronymic = Patronymic;
            userModel.Positiontype = Positiontype;
            userModel.Fullmode = Fullmode;

            Rights rights = repository.Rights.FirstOrDefault(r => r.User == userModel.Id);
            if (rights != null)
            {
                userModel.Rights = rights;
            } else
            {
                userModel.Rights = repository.GenerateBaseUserRights(this);
            }

            return userModel;
        }

        public Rights GetRights(userContext repository)
        {
            Rights rights = repository.Rights.FirstOrDefault(r => r.User == Id);
            if (rights != null)
            {
                return rights;
            }
            else
            {
                return repository.GenerateBaseUserRights(this);
            }
        }
    }
}
